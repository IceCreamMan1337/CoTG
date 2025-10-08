#nullable enable

using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp
{
    public interface ISpellScript
    {
        public SpellScriptMetadata ScriptMetadata { get; }
    }
    internal interface ISpellScriptInternal : ISpellScript
    {
        public void Init(Spell spell, ObjAIBase owner);
        /// <summary>
        /// Happens when the spell is assigned to the Owner
        /// </summary>
        public void OnActivate() { }
        /// <summary>
        /// Happens when the spell is replaced
        /// </summary>
        public void OnDeactivate() { }
        /// <summary>
        /// Happens when a Missile, Sector, Melee Attack hits an unit
        /// </summary>
        /// <param name="target"></param>
        /// <param name="missile"></param>
        /// <param name="sector"></param>
        public void OnSpellHit(AttackableUnit target, SpellMissile? missile) { }
        /// <summary>
        /// Happens after the spell is requested to cast, before cheking mana or energy
        /// </summary>
        /// <param name="target"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void OnSpellPreCast(AttackableUnit? target, Vector2 start, Vector2 end) { }
        /// <summary>
        /// Happens after starting the casting delay time, before creating the missile
        /// </summary>
        public void OnSpellCast() { }
        /// <summary>
        /// Happens after the casting delay time, after creating the missile
        /// </summary>
        public void OnSpellPostCast() { }
        /// <summary>
        /// Happens after the cast is completed
        /// </summary>
        public void OnSpellChannel() { }
        /// <summary>
        /// Happens if the channeling is canceled
        /// </summary>
        public void OnSpellChannelCancel(ChannelingStopSource reason) { }
        /// <summary>
        /// Happens after the channeling is completed, or when the player cast to finishe early
        /// </summary>
        public void OnSpellPostChannel() { }
        public void OnUpdate() { }
        public void OnUpdateStats() { }
        public void OnMissileUpdate(SpellMissile missile) { }
        public void OnMissileEnd(SpellMissile missile) { }
        public bool CanCast() { return true; }
        public void SelfExecute() { }
        public void TargetExecute(AttackableUnit target, SpellMissile? missile, ref HitResult hitResult) { }
        public void AdjustCastInfo() { }
        public float AdjustCooldown()
        {
            return float.NaN;
        }
        public void ChannelingStart() { }
        public void ChannelingStop() { }
        public void ChannelingCancelStop() { }
        public void ChannelingSuccessStop() { }
    }
    public class SpellScript : ISpellScriptInternal
    {
        public static readonly SpellDataFlags Minions = SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectMinions | SpellDataFlags.AffectMinions;
        public static readonly SpellDataFlags Champions = SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectHeroes;
        public static readonly SpellDataFlags AllUnits = Minions | Champions;
        public static readonly SpellDataFlags AllNonTurrets = AllUnits | SpellDataFlags.AffectWards;
        public static readonly SpellDataFlags AllNonBuilding = AllNonTurrets | SpellDataFlags.AffectTurrets;
        public static readonly SpellDataFlags All = AllNonBuilding | SpellDataFlags.AffectBuildings;

        public Spell Spell { get; private set; } = null!;
        public ObjAIBase Owner { get; private set; } = null!;
        public int SpellLevel => Spell.Level;
        public int SpellLevelMinusOne => Spell.LevelMinusOne;

        public virtual SpellScriptMetadata ScriptMetadata { get; } = new();

        public void Init(Spell spell, ObjAIBase owner)
        {
            Spell = spell;
            Owner = owner;
        }

        public virtual void OnActivate()
        {
        }
        public virtual void OnDeactivate()
        {
        }
        public virtual void OnSpellHit(AttackableUnit target, SpellMissile? missile)
        {
        }
        public virtual void OnSpellPreCast(AttackableUnit? target, Vector2 start, Vector2 end)
        {
        }
        public virtual void OnSpellCast()
        {
        }
        public virtual void OnSpellPostCast()
        {
        }
        public virtual void OnSpellChannel()
        {
        }
        public virtual void OnSpellChannelCancel(ChannelingStopSource reason)
        {
        }
        public virtual void OnSpellPostChannel()
        {
        }
        public virtual void OnUpdateStats()
        {
        }
        public virtual void OnUpdate()
        {
        }
        public virtual void OnMissileUpdate(SpellMissile missile)
        {
        }
        public virtual void OnMissileEnd(SpellMissile missile)
        {
        }
        public virtual bool CanCast()
        {
            return true;
        }
    }
}
