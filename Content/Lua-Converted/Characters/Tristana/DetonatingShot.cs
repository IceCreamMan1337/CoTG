namespace Spells
{
    public class DetonatingShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "RocketTristana", },
        };

        private readonly int[] dotDamageByLevel = { 22, 28, 34, 40, 46 };
        private const int Duration = 5;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            // Trigger auto-attack on the target
            IssueOrder(owner, OrderType.AttackTo, default, target);

            // Remove existing DetonatingShot buff from the owner
            SpellBuffRemove(owner, nameof(Buffs.DetonatingShot), owner, 0);

            // Apply the damage over time (DoT) debuff
            int dotDamage = dotDamageByLevel[level - 1];
            AddBuff(attacker, target, new Buffs.ExplosiveShotDebuff(dotDamage), 1, 1, Duration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 1, true, false, false);

            // Apply Internal buff and Grievous Wound debuff to the target
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, Duration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, Duration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}

namespace Buffs
{
    public class DetonatingShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Detonating Shot",
            BuffTextureName = "Tristana_ExplosiveShot.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private readonly int[] aoeDamageByLevel = { 50, 75, 100, 125, 150 };

        public override void OnKill(AttackableUnit target)
        {
            // Check if the target has the DetonatingShot_Target buff
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.DetonatingShot_Target)) > 0)
            {
                SpellEffectCreate(out _, out _, "DetonatingShot_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);

                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float aoeDamage = aoeDamageByLevel[level - 1];

                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    SpellEffectCreate(out _, out _, "tristana_explosiveShot_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, aoeDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
                }
            }
        }

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && owner.Team != target.Team)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.DetonatingShot_Target(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace PreLoads
{
    public class DetonatingShot : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("detonatingshot_target");
            PreloadParticle("detonatingshot_buf.troy");
            PreloadParticle("tristana_explosiveshot_unit_tar.troy");
            PreloadSpell("detonatingshot");
            PreloadSpell("explosiveshotdebuff");
            PreloadSpell("internal_50ms");
            PreloadSpell("grievouswound");
        }
    }
}