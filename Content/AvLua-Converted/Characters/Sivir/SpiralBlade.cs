

namespace Spells
{
    public class SpiralBlade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            targetPos = GetCastSpellTargetPos(spell);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}