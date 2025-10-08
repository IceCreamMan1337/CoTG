using System.Diagnostics;
using System.Collections.Generic;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using F = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.Functions_CS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;

public class SpellChainMissile : SpellMissile
{
    private int Hits = 0;
    private HashSet<AttackableUnit> _unitsHit = new();
    private MissileParameters mp;
    public override MissileType Type => MissileType.Chained;
    public SpellChainMissile(Spell spell, CastInfo castInfo, SpellDataFlags flags) : base(spell, castInfo, flags)
    {
        mp = SpellOrigin.Script.ScriptMetadata.MissileParameters!;
        Debug.Assert(TargetUnit != null);
        Debug.Assert(mp != null);



    }

    internal override void Update()
    {
        base.Update();

        ChainMissileSyncNotify(this);

        if (LinearMoveTo(TargetUnit!.Position))
        {
            SpellOrigin.ApplyEffects(TargetUnit!, this, Flags);
            _unitsHit.Add(TargetUnit!);
            Hits++;

            if (mp.MaximumHitsByLevel != null && SpellOrigin.Name == "RicochetAttack")
            {
                // Console.WriteLine(mp.MaximumHitsByLevel[SpellOrigin.Level - 1]);
                // Console.WriteLine(mp.MaximumHitsByLevel[SpellOrigin.Level - 1]);
                mp.MaximumHits = mp.MaximumHitsByLevel[SpellOrigin.Level - 1];
            }


            if (Hits < mp.MaximumHits)
            {
                var units = F.GetClosestUnitsInArea(
                    SpellOrigin.Caster,
                    Position.ToVector3(0),
                    SpellOrigin.Data.BounceRadius,
                    Flags, //TODO: CanHitEnemies and CanHitFriends
                    int.MaxValue
                );
                if (FoundTarget()) return;
                bool FoundTarget()
                {
                    foreach (var unit in units)
                        if (!_unitsHit.Contains(unit) && (unit != SpellOrigin.Caster || mp.CanHitCaster))
                        {
                            TargetUnit = unit;

                            // Does not work
                            //Game.PacketNotifier126.NotifyS2C_ChainMissileSync(this);
                            //Game.PacketNotifier126.NotifyS2C_UpdateBounceMissile(this);

                            //HACK: (Inherited from the previous implementation)
                            StartPoint = Position3D;
                            EndPoint = TargetUnit.Position3D;
                            CastInfo.Targets.Clear();
                            CastInfo.Targets.Add(new CastTarget(TargetUnit, HitResult.HIT_Normal));
                            CastInfo.TargetPosition = CastInfo.TargetPositionEnd = EndPoint;
                            CastInfo.SpellCastLaunchPosition = StartPoint;
                            MissileReplicationNotify(this);

                            _unitsHit.Add(unit);
                            Hits++;

                            return true;
                        }
                    return false;
                }
                if (mp.CanHitSameTarget)
                {
                    _unitsHit.Clear();
                    _unitsHit.Add(TargetUnit!);
                    Hits++;
                    if (FoundTarget()) return;
                }
                //TODO: CanHitSameTargetConsecutively
            }
            SetToRemove();
        }
    }
}