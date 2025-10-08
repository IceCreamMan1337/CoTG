namespace Buffs
{
    public class CrestOfFlowingWater : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Flowing Water",
            BuffTextureName = "WaterWizard_Typhoon.dds",
            NonDispellable = true,
        };

        Particle buffParticle;

        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "invis_runes_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncPercentMovementSpeedMod(owner, 0.3f);
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            IncPercentMovementSpeedMod(owner, -0.3f);
        }

        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            bool shouldApplyBuff = false;
            float buffDuration = 60f;

            if (attacker is Champion && !IsDead(attacker))
            {
                shouldApplyBuff = true;
            }
            else if (GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers)) > 0)
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    attacker = caster;
                    shouldApplyBuff = true;
                }
            }

            if (shouldApplyBuff)
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                {
                    buffDuration *= 1.2f;
                }
                AddBuff(attacker, attacker, new Buffs.CrestOfFlowingWater(), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
