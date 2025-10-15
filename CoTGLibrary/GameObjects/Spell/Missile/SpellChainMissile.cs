using System.Diagnostics;
using System.Collections.Generic;
using CoTGEnumNetwork;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.Scripting.CSharp;
using static PacketVersioning.PktVersioning;
using CoTG.CoTGServer.API;
using System;

namespace CoTG.CoTGServer.GameObjects.SpellNS.Missile;

public class SpellChainMissile : SpellMissile
{
    private int Hits = 0;
    private HashSet<AttackableUnit> _unitsHit = new();
    private MissileParameters mp;
    public override MissileType Type => MissileType.Chained;
    AttackableUnit? lastTarget = null;
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

            if (Hits >= mp.MaximumHits[SpellOrigin.Level - 1])
            {
                SetToRemove();
                return;
            }

            SpellDataFlags f = SpellOrigin.SpellData.Flags;
            if (mp.CanHitCaster)
            {
                f |= SpellDataFlags.AlwaysSelf;
            }

            List<AttackableUnit> units = ApiFunctionManager.UnitsInRange(SpellOrigin.Caster, Position, SpellOrigin.SpellData.BounceRadius);

            if (units.Count == 0)
            {
                SetToRemove();
                return;
            }

            for (int i = 0; i < units.Count; i++)
            {
                //Prevent hitting the same target twice in a row if CanHitSameTargetConsecutively
                //Also prevent from hitting the same target twice if there's more targets available
                //Otherwise it would just hit the same target over and over again since it will be always the closest one
                if (units[i] == lastTarget && (!mp.CanHitSameTargetConsecutively || units.Count > 1))
                {
                    continue;
                }

                if (units[i].Team == SpellOrigin.Caster.Team && !mp.CanHitFriends)
                {
                    if (units[i] != SpellOrigin.Caster && !mp.CanHitCaster)
                    {
                        continue;
                    }
                }

                if (units[i].Team != SpellOrigin.Caster.Team && !mp.CanHitEnemies)
                {
                    continue;
                }

                //Prevent hitting already hit targets if not allowed
                if (_unitsHit.Contains(units[i]) && !mp.CanHitSameTarget)
                {
                    continue;
                }

                //Since FilterUnitsInRange returns a sorted list by distance, the first valid unit is the closest one, so we good
                TargetUnit = units[i];

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
                _unitsHit.Add(units[i]);
                lastTarget = units[i];
                return;
            }
            SetToRemove();
        }
    }
}