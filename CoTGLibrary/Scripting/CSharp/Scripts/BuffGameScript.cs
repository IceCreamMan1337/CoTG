using System;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.SpellNS;
using CoTG.CoTGServer.GameObjects.StatsNS;

namespace CoTG.CoTGServer.Scripting.CSharp
{
    public interface IBuffScript : ICloneable
    {
        public BuffScriptMetadataUnmutable MetaData { get; }
        public BuffScriptMetaData BuffMetaData { get; }
        public string Name { get; }
    }
    internal interface IBuffScriptInternal : IBuffScript
    {
        public StatsModifier StatsModifier { get; }
        public void Init(Buff buff);
        public void Activate();
        public void Deactivate(bool expired);
        public void OnStackUpdate(int prevStack, int newStack);
        public void OnUpdate();
        public void UpdateStats();
        public bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration);
    }
    public class BuffGameScript : IBuffScriptInternal
    {
        public Buff Buff { get; private set; }
        public AttackableUnit Target => Buff.TargetUnit;
        public ObjAIBase Owner => Buff.SourceUnit;
        public Spell Spell => Buff.OriginSpell;

        public virtual BuffScriptMetadataUnmutable MetaData { get; } = new();
        public virtual BuffScriptMetaData BuffMetaData { get; } = new();
        //TODO: Change?
        public string Name => this.GetType().Name;
        public virtual StatsModifier StatsModifier { get; protected set; } = new();

        public void Init(Buff buff)
        {
            Buff = buff;
        }

        public void Activate()
        {
            OnActivate();
        }
        public virtual void OnActivate()
        {
        }
        public void Deactivate(bool expired)
        {
            OnDeactivate();
        }
        public virtual void OnDeactivate()
        {
        }
        public virtual void OnStackUpdate(int prevStack, int newStack)
        {
        }
        public virtual void UpdateStats()
        {
        }

        public bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            return true;
        }

        public virtual void OnUpdate()
        {
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
