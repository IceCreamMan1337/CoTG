namespace Spells
{
    public class VayneSilveredBolts : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "", },
        };
    }
}
namespace Buffs
{
    public class VayneSilveredBolts : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "VayneSilverBolts",
            BuffTextureName = "Vayne_SilveredBolts.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        float[] effect0 = { 0.04f, 0.05f, 0.06f, 0.07f, 0.08f };
        int[] effect1 = { 20, 30, 40, 50, 60 };

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {

            // Only trigger on auto-attacks, not on abilities or misses
            if (damageSource != DamageSource.DAMAGE_SOURCE_ATTACK ||
                target is not ObjAIBase ||
                target is LaneTurret ||
                hitResult == HitResult.HIT_Dodge ||
                hitResult == HitResult.HIT_Miss)
            {
                return;
            }

            // Cast to ObjAIBase since we verified it's safe above
            var aiTarget = (ObjAIBase)target;

            int count = GetBuffCountFromCaster(target, owner, nameof(Buffs.VayneSilveredDebuff));

            if (count == 2)
            {
                // Third hit - trigger Silver Bolts                
                TeamId teamID = GetTeamID_CS((ObjAIBase)owner);
                TeamId teamIDTarget = GetTeamID_CS(aiTarget);

                // Visual effect for proc
                SpellEffectCreate(out _, out _, "vayne_W_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position3D, target, default, default, true, false, false, false, false);

                // Get W spell level for damage calculation
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

                // Clear the stacks
                SpellBuffClear(target, nameof(Buffs.VayneSilveredDebuff));

                // Calculate damage
                float tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                float rankScaling = effect0[level - 1];
                float flatScaling = effect1[level - 1];
                float damageToDeal = (tarMaxHealth * rankScaling) + flatScaling;

                // Cap damage against neutral monsters
                if (teamIDTarget == TeamId.TEAM_NEUTRAL)
                {
                    damageToDeal = Math.Min(damageToDeal, 200);
                }


                ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
            else
            {
                // Add/refresh stack
                AddBuff((ObjAIBase)owner, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}