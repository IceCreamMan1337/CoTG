using ChildrenOfTheGraveEnumNetwork.Enums;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;

public class SpellTargetMissile : SpellMissile
{
    public override MissileType Type => MissileType.Target;
    public SpellTargetMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        // Debug.Assert(TargetUnit != null);
    }
    internal override void Update()
    {
        base.Update();
        if (TargetUnit != null)
        {
            if (LinearMoveTo(TargetUnit!.Position))
            {
                SpellOrigin.ApplyEffects(TargetUnit, this, Flags);
                SetToRemove();
            }
        }

    }
}