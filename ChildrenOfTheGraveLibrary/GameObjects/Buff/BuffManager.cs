#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGraveLibrary.Scripting.Lua.Scripts;
using log4net;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

public class BuffManager
{
    private static ILog _logger = LoggerProvider.GetLogger();

    private AttackableUnit owner;
    private List<Slot> slots = new();

    private partial class Slot
    {
        private AttackableUnit owner;
        public int Index;
        public string Name;
        public ObjAIBase? Attacker;
        public List<Buff> Stacks = new();
        public bool PersistsThroughDeath => Stacks.Any(
            buff => buff.BuffScript.MetaData.PersistsThroughDeath
        );

        private bool IsAdded = false;

        public Slot(AttackableUnit owner, int slotIndex, string name, ObjAIBase? attacker)
        {
            this.owner = owner;
            Index = slotIndex;
            Name = name;
            Attacker = attacker;
        }

        public void RemoveStack(bool notify)
        {
            int last = Stacks.Count - 1;
            if (last >= 0)
            {
                var buff = Stacks[last];
                if (buff.BuffType != BuffType.COUNTER || (buff.BuffType == BuffType.COUNTER && Stacks.Count > 1))
                {
                    Stacks.RemoveAt(last);
                }
                // buff.Deactivate(false); was here before , but at first view need send packet before apply deactivated method 

                // CECI EST DEGUEULASSE MAIS ON AS UN SOUCIS 
                // le broadcast part trop tard et ducoup cela ne laisse pas le temps de tuer le packet pour le client , ce qui empeche les nouvelle notification 
                // FAUT TROUVER UNE SOLUTION 

                //this is broke entire server sadly , need find another way 
                // _ = Task.Run(async () =>
                // {
                //Console.WriteLine($"[DEBUG] Wait before Deactivate at {DateTime.Now:HH:mm:ss.fff}");
                //   await Task.Delay(200);

                buff.Deactivate(false);
                // });

                if (notify)
                {
                    Notify();


                }
            }
        }
    }

    public void UpdateCDStack(string buff, float duration)
    {
        //hack find better way to do that 
        List<Slot> slot = GetSlots(buff);
        foreach (var s in slot)
        {
            s.Renew(duration, true);
        }
    }
    public void RemoveStack(Buff buff, bool expired = false)
    {
        foreach (var slot in slots)
        {
            if (slot.Index == buff.Slot)
            {
                if (buff.BuffType == BuffType.COUNTER && !expired)
                {
                    slot.Stacks.Remove(buff);
                }
                if (buff.BuffType != BuffType.COUNTER)
                {
                    slot.Stacks.Remove(buff);
                }
                buff.Deactivate(expired);
                slot.Notify();
                return;
            }
        }
        throw new Exception();
    }

    private partial class Slot
    {
        public void RemoveStacks(int count, bool notify)
        {
            for (; count > 0; count--)
            {
                RemoveStack(false);
            }
            if (notify)
            {
                Notify();
            }
        }

        public void RemoveAllStacks(bool notify)
        {
            RemoveStacks(Stacks.Count, false);
            if (notify)
            {
                Notify();
            }
        }

        public void Renew(float duration, bool notify)
        {
            foreach (var buff in Stacks)
            {
                buff.Duration = duration;
                buff.ResetTimeElapsed();
            }
            if (notify)
            {
                Notify();
            }
        }

        public bool Has(BuffType type)
        {
            foreach (var buff in Stacks)
            {
                if (buff.BuffType == type)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasNegative()
        {
            foreach (var buff in Stacks)
            {
                if (
                    false
                    || buff.BuffType == BuffType.INTERNAL
                    //|| buff.BuffType == BuffType.AURA
                    //|| buff.BuffType == BuffType.COMBAT_ENCHANCER
                    || buff.BuffType == BuffType.COMBAT_DEHANCER
                    //|| buff.BuffType == BuffType.SPELL_SHIELD
                    || buff.BuffType == BuffType.STUN
                    //|| buff.BuffType == BuffType.INVISIBILITY
                    || buff.BuffType == BuffType.SILENCE
                    || buff.BuffType == BuffType.TAUNT
                    || buff.BuffType == BuffType.POLYMORPH
                    || buff.BuffType == BuffType.SLOW
                    || buff.BuffType == BuffType.SNARE
                    || buff.BuffType == BuffType.DAMAGE
                    //|| buff.BuffType == BuffType.HEAL
                    //|| buff.BuffType == BuffType.HASTE
                    //|| buff.BuffType == BuffType.SPELL_IMMUNITY
                    //|| buff.BuffType == BuffType.PHYSICAL_IMMUNITY
                    //|| buff.BuffType == BuffType.INVULNERABILITY
                    || buff.BuffType == BuffType.SLEEP
                    || buff.BuffType == BuffType.NEAR_SIGHT
                    || buff.BuffType == BuffType.FRENZY
                    || buff.BuffType == BuffType.FEAR
                    || buff.BuffType == BuffType.CHARM
                    || buff.BuffType == BuffType.POISON
                    || buff.BuffType == BuffType.SUPPRESSION
                    || buff.BuffType == BuffType.BLIND
                    //|| buff.BuffType == BuffType.COUNTER
                    || buff.BuffType == BuffType.SHRED
                    || buff.BuffType == BuffType.FLEE
                    //|| buff.BuffType == BuffType.KNOCKUP
                    //|| buff.BuffType == BuffType.KNOCKBACK
                    || buff.BuffType == BuffType.DISARM
                )
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsActive()
        {
            return GetStartTime() > Game.Time.GameTime / 1000f;
        }

        public float GetRemainingDuration()
        {
            if (Stacks.Count > 0)
                return Stacks.Max(buff => buff.RemainingDuration);
            return -1; //TODO: Verify
        }

        public float GetStartTime()
        {
            return Stacks.Min(buff => buff.StartTime);
        }

        public void Notify()
        {
            if (Stacks.Count > 0)
            {
                var expiringFirst = Stacks.MinBy(buff => buff.RemainingDuration)!;
                if (!IsAdded)
                {
                    bool isInternal = !Stacks.Any(buff => !buff.IsInternal);
                    if (!isInternal)
                    {
                        IsAdded = true;
                        NPC_BuffAdd2Notify(
                            expiringFirst, Stacks.Count
                        );
                        //Console.WriteLine($"ADD {Index} {expiringFirst.BuffType} {Stacks.Count} {expiringFirst.IsHidden} \"{expiringFirst.Name}\" {expiringFirst.TimeElapsed}s/{expiringFirst.Duration}s");
                    }
                }
                else
                {
                    NPC_BuffUpdateCountNotify(
                        expiringFirst, Stacks.Count
                    );
                    //Console.WriteLine($"UPD {Index} {Stacks.Count} {expiringFirst.TimeElapsed}s/{expiringFirst.Duration}s");
                }
            }
            else if (IsAdded)
            {
                IsAdded = false;
                NPC_BuffRemove2Notify(
                    owner, Index, Name
                );
                //Console.WriteLine($"REM {Index} \"{Name}\"");
            }
        }
    }

    internal BuffManager(AttackableUnit owner)
    {
        this.owner = owner;
    }

    /// <returns>
    /// Returns the slot with the requested name and attacker.
    /// The returned slot may not contain stacks.
    /// </returns>
    private Slot? GetSlot(string name, ObjAIBase? attacker = null)
    {
        return slots.Find(
            slot => slot.Name == name && slot.Attacker == attacker
        );
    }

    /// <returns>
    /// Returns non-empty slot with the specified attacker
    /// or non-empty slots with any attackers if null is passed.
    /// </returns>
    private List<Slot> GetSlots(string name, ObjAIBase? attacker = null)
    {
        return slots.FindAll(
            slot => slot.Name == name && (
                slot.Attacker == null || attacker == null || slot.Attacker == attacker
            ) && slot.Stacks.Count > 0
        );
    }

    private Slot? GetSlot(int slotIndex, ObjAIBase? attacker = null)
    {
        return slots.Find(
            slot => slot.Index == slotIndex && (
                slot.Attacker == null || attacker == null || slot.Attacker == attacker
            ) && slot.Stacks.Count > 0
        );
    }
    private Slot? GetSingleSlot(string name, ObjAIBase? attacker = null)
    {
        var slots = GetSlots(name, attacker);
        //   Debug.Assert(slots.Count <= 1);
        return slots.FirstOrDefault();
    }

    public void Add(
        string name,
        float duration,
        int stacks,
        Spell? originspell,
        AttackableUnit onto,
        ObjAIBase from,
        IEventSource? parent = null
    )
    {
        var script = Game.ScriptEngine.CreateObject<IBuffScriptInternal>(
            "Buffs", name, Game.Config.SupressScriptNotFound
        );

        if (script == null)
        {
            if (LuaScriptEngine.HasBBScript(name))
            {
                script = new BBBuffScript
                (
                    new BBScriptCtrReqArgs
                    (
                        name,
                        onto,
                        (onto as Minion)?.Owner as Champion
                    ),
                    from
                );
            }
            else
            {
                script = new BuffScriptEmpty();
            }
        }


        var m = script.BuffMetaData ?? new();

        if (!script.OnAllowAdd(from, m.BuffType, name, m.MaxStacks, ref duration))
            return;

        foreach (var buff in All())
        {
            if (!buff.Elapsed && !buff.ScriptInternal.OnAllowAdd(from, m.BuffType, name, m.MaxStacks, ref duration))
            {
                return;
            }
        }

        Add(
            from,
            script,
            m.MaxStacks, stacks,
            duration,
            m.BuffAddType, m.BuffType,
            m.TickRate,
            m.StacksExclusive, m.CanMitigateDuration, m.IsHidden,
            originspell
        );
    }

    public void Add
    (
        ObjAIBase attacker,
        IBuffScript script,
        int maxStack = 0,
        int stacks = 1,
        float duration = 0,

        BuffAddType addType = BuffAddType.REPLACE_EXISTING,
        BuffType type = BuffType.INTERNAL,
        float tickRate = 0,
        bool stacksExclusive = false,
        bool canMitigateDuration = false, //TODO:
        bool isHiddenOnClient = false,

        Spell? spell = null
    )
    {
        if (this.owner.Stats.IsDead) return;
        var name = script.Name;




        foreach (var buff in All())
        {
            if (!buff.Elapsed && !buff.ScriptInternal.OnAllowAdd(attacker, type, name, maxStack, ref duration))
            {
                return;
            }
        }



        var exclusiveAttacker = attacker;
        if (!stacksExclusive)
        {
            exclusiveAttacker = null;
        }

        int slotIndex = 0;
        var slot = GetSlot(name, exclusiveAttacker);
        if (slot == null)
        {
            for (; slotIndex < 256; slotIndex++)
            {
                if (slots.Find(slot => slot.Index == slotIndex) == null)
                {
                    break;
                }
            }
            if (slotIndex == 256)
            {
                throw new Exception();
            }
            slot = new Slot(owner, slotIndex, name, exclusiveAttacker);
            slots.Add(slot);
        }
        slotIndex = slot.Index;

        bool hasBuffBefore = slot.Stacks.Count > 0;

        switch (addType)
        {
            case BuffAddType.REPLACE_EXISTING:
                {
                    stacks = Math.Min(stacks, maxStack);
                    slot.RemoveAllStacks(false);
                    AddStacks();
                }
                break;
            case BuffAddType.RENEW_EXISTING:
                {
                    stacks = Math.Min(stacks, maxStack);
                    if (hasBuffBefore)
                    {
                        slot.Renew(duration, false);
                    }
                    else
                    {
                        AddStacks();
                    }
                }
                break;
            //at first view this is totally fucked 
            case BuffAddType.STACKS_AND_RENEWS:
                {
                    // Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    slot.Renew(duration, false);

                    if (type == BuffType.COUNTER)
                    {
                        AddStacks();
                        //AddContinuingStacks(duration);
                    }
                    else
                    {
                        AddStacks();
                    }

                }
                break;

            case BuffAddType.STACKS_AND_CONTINUE:
                {

                    //Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    AddContinuingStacks(duration);
                }
                break;
            case BuffAddType.STACKS_AND_OVERLAPS:
                {
                    //Debug.Assert(maxStack >= slot.Stacks.Count);
                    stacks = Math.Min(stacks, maxStack - slot.Stacks.Count);
                    AddStacks();
                }
                break;
        }

        slot.Notify();

        void AddStacks()
        {
            for (int i = 0; i < stacks; i++)
            {
                AddStack(i, duration);
            }
        }

        void AddContinuingStacks(float duration)
        {
            float prevStacksDuration = Math.Max(0, slot.GetRemainingDuration());
            for (int i = 0; i < stacks; i++)
            {
                var durationToAdd = prevStacksDuration + (i * duration);
                if (durationToAdd == 0)
                {
                    durationToAdd = duration;
                }
                AddStack(i, durationToAdd);
            }
        }

        void AddStack(int i, float duration)
        {
            if (i > 0)
            {
                script = (IBuffScript)script.Clone();
            }
            var buff = new Buff(slotIndex, name, duration, 1, spell, owner, attacker, null, script, type, isHiddenOnClient, tickRate);
            slot.Stacks.Add(buff);
            buff.Activate();
        }
    }

    public int Count(string name, ObjAIBase? attacker = null)
    {
        /*
        if(attacker != null)
        {
            return
                (GetSlot(name, attacker)?.stacks.Count ?? 0) +
                (GetSlot(name, null)?.stacks.Count(buff => buff.SourceUnit == attacker) ?? 0);
        }
        else
        */
        return GetSlots(name, attacker).Sum(slot => slot.Stacks.Count);
    }

    public float GetRemainingDuration(string name, ObjAIBase? attacker = null)
    {
        var slots = GetSlots(name, attacker);
        if (slots.Count > 0)
            return slots.Max(slot => slot.GetRemainingDuration());
        return -1; //TODO: Verify
    }

    public float GetStartTime(string name, ObjAIBase? attacker = null)
    {
        return GetSlots(name, attacker).Min(slot => slot.GetStartTime());
    }

    public bool Has(string name, ObjAIBase? attacker = null)
    {
        return Count(name, attacker) > 0;
    }

    public bool Has(BuffType type)
    {
        foreach (var slot in slots)
        {
            if (slot.Has(type))
            {
                return true;
            }
        }
        return false;
    }

    #region
    // Functions that strictly require the presence or absence of an attacker
    // depending on whether the buff stacks exclusively or not.
    public void RemoveStack(string name, ObjAIBase? attacker = null)
    {
        GetSingleSlot(name, attacker)?.RemoveStack(true);
    }
    public void RemoveStack(int buffSlot, ObjAIBase? attacker = null)
    {
        var value = GetSlot(buffSlot, attacker);
        value?.RemoveStack(true);
    }
    public void RemoveStacks(string name, ObjAIBase? attacker = null, int count = 0)
    {
        GetSingleSlot(name, attacker)?.RemoveStacks(count, true);
    }
    public void RemoveAllStacks(string name, ObjAIBase? attacker = null)
    {
        GetSingleSlot(name, attacker)?.RemoveAllStacks(true);
    }
    public void Renew(string name, ObjAIBase? attacker = null, float duration = 0)
    {
        GetSingleSlot(name, attacker)?.Renew(duration, true);
    }

    // For compatibility with the legacy LS-Scripts
    public List<Buff>? GetStacks(string name, ObjAIBase? attacker = null)
    {
        return GetSingleSlot(name, attacker)?.Stacks;
    }
    #endregion

    public void RemoveType(BuffType type)
    {
        foreach (var slot in new List<Slot>(slots))
        {
            if (slot.Has(type))//&& slot.IsActive())
            {
                //there is an issue , in case an buff have  SpellBuffRemoveType and have same type than the mentionned , he remove it itself >< 
                //so this is an hack for pet , possibly it will be more interesting to check originbuff.type != type to remove
                if (owner is Pet && type == BuffType.COMBAT_ENCHANCER)
                {

                }
                else
                {
                    slot.RemoveAllStacks(true);
                }


            }
        }
    }

    public void DispellNegative()
    {
        //TODO: Maybe it should not remove the entire slot,
        //TODO: but only matching stacks?
        foreach (var slot in new List<Slot>(slots))
        {
            if (slot.HasNegative() && slot.IsActive())
            {
                slot.RemoveAllStacks(true);
            }
        }
    }

    public void RemoveNotLastingThroughDeath()
    {
        foreach (var slot in new List<Slot>(slots))
        {
            if (!slot.PersistsThroughDeath)
            {
                slot.RemoveAllStacks(true);
            }
        }
    }

    public IEnumerable<Buff> All()
    {
        foreach (var slot in slots)
        {
            foreach (var buff in slot.Stacks)
            {
                yield return buff;
            }
        }
    }

    private List<Slot> temp0 = new();
    private List<Buff> temp1 = new();
    private void ForEach(Action<Buff> action)
    {
        temp0.AddRange(slots);
        foreach (var slot in temp0)
        {
            if (slot?.Stacks != null)
            {
                temp1.AddRange(slot.Stacks);
                foreach (var buff in temp1)
                {
                    if (buff != null)
                    {
                        action(buff);
                    }
                }
                temp1.Clear();
            }
        }
        temp0.Clear();
    }

    internal void UpdateStats()
    {
        ForEach(buff => buff.UpdateStats());
    }

    internal void Update()
    {
        ForEach(buff => buff.Update());
    }

    internal void ReloadScripts()
    {
        ForEach(buff => buff.LoadScript());
    }
}