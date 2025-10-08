namespace Buffs
{
    public class CrestofUnyieldingStone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Unyielding Stone",
            BuffTextureName = "PlantKing_AnimateEntangler.dds",
            NonDispellable = true,
        };

        private Particle buffParticle;
        private float bonusDamage;
        private float bonusResist;
        private const float MaxResist = 80f;
        private const float MinResist = 40f;
        private const float MaxDamage = 0.4f;
        private const float MinDamage = 0.2f;
        private const float ResistMultiplier = 0.0666f;
        private const float DamageMultiplier = 0.000333f;

        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "Global_Invulnerability.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            CalculateAndApplyBonuses();
            UpdateBuffToolTip();
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            RemoveBonuses();
        }

        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (attacker is Champion && !IsDead(attacker))
            {
                AddBuff(attacker, attacker, new Buffs.CrestofUnyieldingStone(), 1, 1, 120, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            }
        }

        private void CalculateAndApplyBonuses()
        {
            float gameTimeSec = GetGameTime();
            float calculatedResist = ResistMultiplier * gameTimeSec;
            float calculatedDamage = DamageMultiplier * gameTimeSec;

            bonusResist = Math.Clamp(calculatedResist, MinResist, MaxResist);
            bonusDamage = Math.Clamp(calculatedDamage, MinDamage, MaxDamage);

            ApplyBonusesToUnits(bonusDamage, bonusResist);
        }

        private void RemoveBonuses()
        {
            ApplyBonusesToUnits(-bonusDamage, -bonusResist);
        }

        private void ApplyBonusesToUnits(float damageBonus, float resistBonus)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectTurrets))
            {
                IncPermanentPercentPhysicalDamageMod(unit, damageBonus);
                IncPermanentFlatArmorMod(unit, resistBonus);
                IncPermanentFlatSpellBlockMod(unit, resistBonus);
            }
        }

        private void UpdateBuffToolTip()
        {
            float tooltipDamage = 100 * bonusDamage;
            SetBuffToolTipVar(1, tooltipDamage);
            SetBuffToolTipVar(2, bonusResist);
        }
    }
}