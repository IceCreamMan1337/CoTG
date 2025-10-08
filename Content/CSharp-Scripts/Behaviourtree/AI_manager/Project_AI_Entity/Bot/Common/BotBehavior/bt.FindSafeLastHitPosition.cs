using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class FindSafeLastHitPositionClass : AI_Characters
{


    public bool FindSafeLastHitPosition(
        out Vector3 _SafePosition,
     AttackableUnit Self
        )
    {

        Vector3 SafePosition = Vector3.Zero;
        bool result =
                    // Sequence name :FindSafeLastHitPosition

                    FindFirstAllyMinionNearby(
                          out FirstMinion,
                          Self,
                          800) &&
                    FindLastAllyMinionNearby(
                          out LastMinion,
                          Self,
                          800) &&
                    GetDistanceBetweenUnits(
                          out DistanceBetweenMinions,
                          LastMinion,
                          FirstMinion) &&
                    GetUnitPosition(
                          out MinionPosition,
                          FirstMinion) &&
                    CountUnitsInTargetArea(
                          out FriendlyMinionsByFirstMinion,
                          Self,
                          MinionPosition,
                          300,
                          AffectFriends | AffectMinions,
                          "") &&
                    // Sequence name :PickReferenceMinion
                    (
                          // Sequence name :UseFirstMinionIfDistance&gt,800AndFirstMinionNotSolo
                          (
                                GreaterFloat(
                                      DistanceBetweenMinions,
                                      800) &&
                                GreaterInt(
                                      FriendlyMinionsByFirstMinion,
                                      1) &&
                                SetVarAttackableUnit(
                                      out _Minion,
                                      FirstMinion)
                          ) ||
                          SetVarAttackableUnit(
                                out _Minion,
                                LastMinion)
                    ) &&
                    GetUnitAIClosestLaneID(
                          out LaneID,
                          Self) &&
                    GetUnitAIBasePosition(
                          out BasePosition,
                          Self) &&
                    GetUnitTeam(
                          out Team,
                          Self) &&
                    // Sequence name :GetNexus
                    (
                          // Sequence name :Order
                          (
                                Team == TeamId.TEAM_ORDER &&
                                GetNexus(
                                      out _Nexus,
                                     TeamId.TEAM_ORDER)
                          ) ||
                          // Sequence name :Chaos
                          (
                                Team == TeamId.TEAM_CHAOS &&
                                GetNexus(
                                      out _Nexus,
                                      TeamId.TEAM_CHAOS)
                          )
                    ) &&
                    // Sequence name :MaskFailure
                    (
                                // Sequence name :UpdateBasePositionIfMinionsTooFar

                                // Sequence name :BotLane
                                (
                                      LaneID == 0 &&
                                      MakeVector(
                                            out BotLaneCorner,
                                            11427,
                                            43,
                                            1335) &&
                                      DistanceBetweenObjectAndPoint(
                                            out CornerToNexus,
                                            _Nexus,
                                            BotLaneCorner) &&
                                      GetDistanceBetweenUnits(
                                            out MinionToNexus,
                                            _Nexus,
                                            _Minion) &&
                                      GreaterFloat(
                                            MinionToNexus,
                                            CornerToNexus) &&
                                      SetVarVector(
                                            out BasePosition,
                                            BotLaneCorner) &&
                                      MakeVector(
                                            out BotLaneCorner,
                                            13066,
                                            45,
                                            3300) &&
                                      DistanceBetweenObjectAndPoint(
                                            out CornerToNexus,
                                            _Nexus,
                                            BotLaneCorner) &&
                                      GetDistanceBetweenUnits(
                                            out MinionToNexus,
                                            _Nexus,
                                            _Minion) &&
                                      GreaterFloat(
                                            MinionToNexus,
                                            CornerToNexus) &&
                                      SetVarVector(
                                            out BasePosition,
                                            BotLaneCorner)
                                ) ||
                                // Sequence name :TopLane
                                (
                                      LaneID == 2 &&
                                      MakeVector(
                                            out TopLaneCorner,
                                            1280,
                                            25,
                                            11877) &&
                                      DistanceBetweenObjectAndPoint(
                                            out CornerToNexus,
                                            _Nexus,
                                            TopLaneCorner) &&
                                      GetDistanceBetweenUnits(
                                            out MinionToNexus,
                                            _Nexus,
                                            _Minion) &&
                                      GreaterFloat(
                                            MinionToNexus,
                                            CornerToNexus) &&
                                      SetVarVector(
                                            out BasePosition,
                                            TopLaneCorner) &&
                                      MakeVector(
                                            out TopLaneCorner,
                                            2699,
                                            22,
                                            13195) &&
                                      DistanceBetweenObjectAndPoint(
                                            out CornerToNexus,
                                            _Nexus,
                                            TopLaneCorner) &&
                                      GetDistanceBetweenUnits(
                                            out MinionToNexus,
                                            _Nexus,
                                            _Minion) &&
                                      GreaterFloat(
                                            MinionToNexus,
                                            CornerToNexus) &&
                                      SetVarVector(
                                            out BasePosition,
                                            TopLaneCorner)
                                )
                           || MaskFailure()
                    ) &&
                    GetUnitPosition(
                          out MinionPosition,
                          _Minion) &&
                    CalculatePointOnLine(
                          out SafePosition,
                          MinionPosition,
                          BasePosition,
                          200)

              ;

        _SafePosition = SafePosition;
        return result;

    }
}

