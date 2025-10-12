namespace Spells
{
    public class Crystallize : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };

        // UNUSED
        // int meltingTime;
        // int halfLength;
        // bool foundFirstPos;

        int[] wallSegmentsPerLevel = { 4, 5, 6, 7, 8 };
        int[] wallWidthPerLevel = { 400, 500, 600, 700, 800 };
        int[] wallHalfLengthPerLevel = { 200, 250, 300, 350, 400 };

        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Crystallize)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

                // UNUSED
                // meltingTime = 5;
                // int halfLength = wallHalfLengthPerLevel[level - 1];

                Vector3 targetPos = GetSpellTargetPos(spell);
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos);
                TeamId teamID = GetTeamID_CS(owner);

                int numSegments = wallSegmentsPerLevel[level - 1];
                float lineWidth = wallWidthPerLevel[level - 1];
                Vector3 facingDirection = GetPointByUnitFacingOffset(owner, 9999, 0);

                foreach (Vector3 position in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, numSegments))
                {

                    Minion iceBlock = SpawnMinion("IceBlock", "AniviaIceblock", "idle.lua", position, teamID, true, true, true, true, false, true, 0, false, false);

                    FaceDirection(iceBlock, facingDirection);
                    AddBuff(owner, iceBlock, new Buffs.Crystallize(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SetGhostProof(iceBlock, true);

                }
            }
        }
    }
}

namespace Buffs
{
    public class Crystallize : BuffScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);

            var unitsInArea = GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false);
            foreach (AttackableUnit unit in unitsInArea)
            {
                bool isGhosted = GetGhosted(unit);
                float pushDistance = (unit is Champion) ? 120 : 250;

                Vector3 targetPos = IsBehind(owner, unit)
                    ? GetPointByUnitFacingOffset(owner, pushDistance, 180)
                    : GetPointByUnitFacingOffset(owner, pushDistance, 0);

                if (attacker.Team != unit.Team)
                {
                    ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                }

                if (!isGhosted)
                {
                    AddBuff(attacker, unit, new Buffs.GlobalWallPush(targetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }

        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, target, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }

        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}

namespace PreLoads
{
    public class Crystallize : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("crystallizepush");
            PreloadSpell("crystallize");
            PreloadSpell("iceblock");
            PreloadCharacter("aniviaiceblock");
        }
    }
}