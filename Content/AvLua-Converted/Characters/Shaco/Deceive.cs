namespace Spells
{
    public class Deceive : SpellScript
    {
        private readonly int[] cooldownReductionByLevel = { 11, 11, 11, 11, 11 };
        private readonly float[] critDamageBonusByLevel = { -0.6f, -0.4f, -0.2f, 0, 0.2f };
        private const float MaxTeleportDistance = 500f;

        public override void SelfExecute()
        {
            int cooldownReduction = cooldownReductionByLevel[level - 1];
            float critDamageBonus = critDamageBonusByLevel[level - 1];

            SpellEffectCreate(out _, out _, "jackintheboxpoof2.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);

            Vector3 targetPosition = GetSpellTargetPos(spell);
            Vector3 ownerPosition = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPosition, targetPosition);

            if (distance > MaxTeleportDistance)
            {
                FaceDirection(owner, targetPosition);
                targetPosition = GetPointByUnitFacingOffset(owner, MaxTeleportDistance, 0);
            }

            SealSpellSlot(0, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);

            AddBuff(owner, owner, new Buffs.DeceiveFade(cooldownReduction, targetPosition, critDamageBonus), 1, 1, 0.05f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);

            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
        }
    }
}

namespace Buffs
{
    public class Deceive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deceive",
            BuffTextureName = "Jester_ManiacalCloak2.dds",
            IsDeathRecapSource = true,
        };

        private readonly float cooldownReduction;

        public Deceive(float cooldownReduction = default)
        {
            this.cooldownReduction = cooldownReduction;
        }

        public override void OnActivate()
        {
            SetStealthed(owner, true);
        }

        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PushCharacterFade(owner, 1, 0);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);

            float cooldownStat = GetPercentCooldownMod(owner);
            float newCooldown = cooldownReduction * (1 + cooldownStat);

            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }

        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth || !spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }

        public override void OnPreAttack(AttackableUnit target)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}