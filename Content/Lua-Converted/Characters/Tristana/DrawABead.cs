namespace Spells
{
    public class DrawABead : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}

namespace Buffs
{
    public class DrawABead : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Draw a Bead",
            BuffTextureName = "Tristana_DrawABead.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private float lastTooltip;
        private float lastTimeExecuted;
        private readonly int[] attackRangeIncreaseByLevel = { 0, 9, 18, 27, 36, 45, 54, 63, 72, 81, 90, 99, 108, 117, 126, 135, 144, 153, 162 };

        public override void OnActivate()
        {
            lastTooltip = 0;
        }

        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float tooltipAmount = attackRangeIncreaseByLevel[level - 1];
                if (tooltipAmount > lastTooltip)
                {
                    lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }

        private void ResetCooldownIfApplicable()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown > 0)
                {
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }

        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                ResetCooldownIfApplicable();
            }
        }

        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (target is Champion)
            {
                ResetCooldownIfApplicable();
            }
        }
    }
}