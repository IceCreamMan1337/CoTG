

namespace Spells
{
    public class Ricochet : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            // TriggersSpellCasts = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        //    public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
        // ref HitResult hitResult)
        public override void SelfExecute()
        {
            int nextBuffVars_LastLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Ricochet)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Ricochet), (ObjAIBase)owner);
            }
            else
            {
                nextBuffVars_LastLevel = level;
                AddBuff((ObjAIBase)owner, owner, new Buffs.Ricochet(nextBuffVars_LastLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
            }
        }
    }
}
namespace Buffs
{
    public class Ricochet : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Ricochet",
            BuffTextureName = "Sivir_Ricochet.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        int lastLevel;
        public Ricochet(int lastLevel = default)
        {
            this.lastLevel = lastLevel;
        }
        public override void OnActivate()
        {
            int level;
            //RequireVar(this.lastLevel);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, level, false);
            SetBuffToolTipVar(1, level);

            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
        }
        //  public override void OnUpdateActions()
        //  {
        public override void OnLaunchAttack(AttackableUnit target)
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level != this.lastLevel)
            {
                OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, level, false);
                this.lastLevel = level;
                SetBuffToolTipVar(1, level);
            }
        }
    }
}