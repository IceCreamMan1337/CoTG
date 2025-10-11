namespace Spells
{
    public class CannonBarrageBall : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };

        private readonly int[] damageAmounts = { 75, 120, 165 };

        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);

            Minion spawnedMinion = SpawnMinion(
                "HiddenMinion",
                "TestCube",
                "idle.lua",
                targetPos,
                teamOfOwner,
                false,
                true,
                false,
                true,
                true,
                true,
                0,
                false,
                true
            );

            float damageAmount = damageAmounts[level - 1];
            AddBuff(
                attacker,
                spawnedMinion,
                new Buffs.CannonBarrageBall(damageAmount),
                1,
                1,
                0.5f,
                BuffAddType.REPLACE_EXISTING,
                BuffType.INTERNAL,
                0,
                true,
                false,
                false
            );
        }
    }
}

namespace Buffs
{
    public class CannonBarrageBall : BuffScript
    {
        private readonly float damageAmount;

        public CannonBarrageBall(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }

        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);

            Vector3 ownerPos = GetUnitPosition(owner);
            TeamId teamID = GetTeamID_CS(owner);

            SpellEffectCreate(
                out _,
                out _,
                "pirate_cannonBarrage_point.troy",
                default,
                teamID,
                225,
                0,
                TeamId.TEAM_UNKNOWN,
                default,
                owner,
                false,
                default,
                default,
                ownerPos,
                target,
                default,
                default,
                true,
                false,
                false,
                false,
                false
            );
        }

        public override void OnDeactivate(bool expired)
        {
            Vector3 ownerPos = GetUnitPosition(owner);
            TeamId teamID = GetTeamID_CS(owner);

            ObjAIBase gangplank = GetChampionBySkinName("Gangplank", teamID);

            SpellEffectCreate(
                out _,
                out _,
                "pirate_cannonBarrage_tar.troy",
                default,
                teamID,
                225,
                0,
                TeamId.TEAM_UNKNOWN,
                default,
                owner,
                false,
                default,
                default,
                ownerPos,
                target,
                default,
                default,
                true,
                false,
                false,
                false,
                false
            );

            foreach (AttackableUnit unit in GetUnitsInArea(
                gangplank,
                owner.Position3D,
                265,
                SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes,
                default,
                true
            ))
            {
                BreakSpellShields(unit);
                ApplyDamage(
                    gangplank,
                    unit,
                    damageAmount,
                    DamageType.DAMAGE_TYPE_MAGICAL,
                    DamageSource.DAMAGE_SOURCE_SPELLAOE,
                    1,
                    0.2f,
                    1,
                    false,
                    false,
                    gangplank
                );
            }

            SetTargetable(owner, true);
            ApplyDamage(
                (ObjAIBase)owner,
                owner,
                1000,
                DamageType.DAMAGE_TYPE_TRUE,
                DamageSource.DAMAGE_SOURCE_INTERNALRAW,
                1,
                0.8f,
                1,
                false,
                false,
                gangplank
            );
        }

        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}

namespace PreLoads
{
    public class CannonBarrageBall : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("pirate_cannonbarrage_point.troy");
            PreloadCharacter("gangplank");
            PreloadParticle("pirate_cannonbarrage_tar.troy");
            PreloadCharacter("testcube");
        }
    }
}