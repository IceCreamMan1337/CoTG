#nullable enable

using CoTGEnumNetwork.Enums;
using System.Collections.Generic;
using CoTG.CoTGServer.API;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using FCS = CoTG.CoTGServer.Scripting.CSharp.Converted.Functions_CS;

namespace CoTG.CoTGServer.Scripting.CSharp.Converted;

public class CBuffScript : CScript, IBuffScriptInternal
{
    public virtual BuffScriptMetadataUnmutable MetaData { get; } = new();
    public virtual BuffScriptMetaData BuffMetaData => _metadata;
    //TODO Change?
    public string Name => this.GetType().Name;
    private BuffScriptMetaData _metadata = null!;

    private List<Particle> EffectsCreatedOnActivation = new();

    protected float lifeTime => _buff.TimeElapsed; //TODO: Verify
    protected new AttackableUnit owner => _buff.TargetUnit;
    protected new ObjAIBase attacker => _buff.SourceUnit;
    protected new AttackableUnit target => _buff.TargetUnit;
    protected ObjAIBase caster => _buff.SourceUnit;
    public Buff Buff => _buff;



    protected Buff _buff = null!;
    public void Init(Buff buff)
    {
        base.Init(buff.TargetUnit, buff.SourceUnit, buff.OriginSpell);
        _metadata = new();
        _buff = buff;
    }

    public virtual void UpdateBuffs() { }
    public virtual void OnUpdateAmmo() { }
    public virtual bool OnAllowAdd(ObjAIBase attacker, BuffType type,
        string scriptName,
        int maxStack, ref float duration)
    {
        return true;
    }
    public new void Activate()
    {
        ApplyAutoActivateEffects();
        base.Activate();
        OnActivate();

    }
    public virtual void OnActivate()
    {


    }
    private void ApplyAutoActivateEffects()
    {
        var m = MetaData;
        for (int i = 0; i < m.AutoBuffActivateEffect.Length; i++)
        {
            var effect = m.AutoBuffActivateEffect[i];
            if (!string.IsNullOrEmpty(effect))
            {
                var bone = (
                    //TODO: GetValueOrDefault for Lists?
                    (m.AutoBuffActivateAttachBoneName.Length > i) ?
                    m.AutoBuffActivateAttachBoneName[i] : null
                ) ?? "";
                /* var part = new Particle(
                     effect , owner,
                     default, owner, bone,
                     default, owner, bone
                 );
                */
                //TODO: don't add particles in the constructor!
                //Game.ObjectManager.AddObject(part);
                //    EffectsCreatedOnActivation.Add(part);
            }
        }
    }
    public void Deactivate(bool expired)
    {
        if (_buff.BuffType == BuffType.COUNTER && expired)
        {
            OnUpdateAmmo();
        }
        else
        {
            OnDeactivate(expired);
            if (_buff.BuffType != BuffType.COUNTER)
            {

                ApiEventManager.RemoveAllListenersForOwner(this);
                foreach (var effect in EffectsCreatedOnActivation)
                {

                    effect.SetToRemove();

                }

                Cleanup();
            }
        }



    }
    public virtual void OnDeactivate(bool expired)
    {
    }
    public void OnStackUpdate(int prevStack, int newStack)
    {
    }

    private float onUpdateTrackTime = 0;
    public override void OnUpdate()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateTrackTime, true))
        {
            OnUpdateActions();
        }
    }
    private float onUpdateStatsTrackTime = 0;
    public override void UpdateStats()
    {
        if (FCS.ExecutePeriodically(_buff.TickRate, ref onUpdateStatsTrackTime, true))
        {
            OnUpdateStats();
        }
    }

    // Functions that require the current buff
    protected ObjAIBase GetBuffCasterUnit()
    {
        return caster;
    }
    protected void SpellBuffRemoveCurrent(AttackableUnit target)
    {
        /* if (target.GetBuffWithName(_buff.Name).BuffScript.MetaData.SpellToggleSlot != null && _buff.OriginSpell != null )
         {
             if(target.GetBuffWithName(_buff.Name).BuffScript.MetaData.SpellToggleSlot == 1)
             Game.PacketNotifier.NotifyChangeSlotSpellData((int)target.NetId,
                (target as ObjAIBase),
                _buff.OriginSpell.Slot,
                ChangeSlotSpellDataType.
                SpellName,
                false,
                TargetingType.Self,
                _buff.OriginSpell.Name);
         }
        */
        target.Buffs.RemoveStack(_buff.Slot);

    }
    public void SetBuffToolTipVar(int index, float value)
    {
        _buff.SetToolTipVar(index - 1, value);
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}