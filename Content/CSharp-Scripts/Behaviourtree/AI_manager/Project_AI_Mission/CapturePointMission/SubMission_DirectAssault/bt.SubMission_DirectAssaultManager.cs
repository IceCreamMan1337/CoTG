namespace BehaviourTrees;


class SubMission_DirectAssaultManagerClass : AImission_bt
{


    public bool SubMission_DirectAssaultManager(
               out int __DirectAssaultState,
     out Vector3 __RendezvousPosition,
     Vector3 RendezvousPosition,
   IEnumerable<AttackableUnit> SquadMembers,
   int DirectAssaultState,
   int CapturePointIndex,
   AttackableUnit CapturePoint,
   float DefenderStrength
         )
    {
        int _DirectAssaultState = DirectAssaultState;
        Vector3 _RendezvousPosition = RendezvousPosition;
        var getAssaultRendezvousPosition = new GetAssaultRendezvousPositionClass();
        var assignTaskWithPosition = new AssignTaskWithPositionClass();
        var strengthEvaluation_ChampionHealth = new StrengthEvaluation_ChampionHealthClass();


        bool result =
                  // Sequence name :Selector

                  // Sequence name :DirectAssaultCaptureInit
                  (
                        LessInt(
                              DirectAssaultState,
                              0) &&
                        ForEach(SquadMembers, Member =>                               // Sequence name :Sequence

                                    GetUnitTeam(
                                          out ReferenceTeam,
                                          Member)

                        ) &&
                        getAssaultRendezvousPosition.GetAssaultRendezvousPosition(
                              out _RendezvousPosition,
                              CapturePointIndex,
                              ReferenceTeam) &&
                        SetVarInt(
                              out _DirectAssaultState,
                              0)
                  ) ||
                  // Sequence name :GoingTowards
                  (
                        DirectAssaultState == 0 &&
                        GetUnitTeam(
                              out CapturePointTeam,
                              CapturePoint) &&
                        NotEqualUnitTeam(
                              CapturePointTeam,
                              TeamId.TEAM_NEUTRAL) &&
                        SetVarInt(
                              out ChampsNearRendezvousPoint,
                              0) &&
                        SetVarFloat(
                              out AttackersStrengthAtRendezvous,
                              0) &&
                        ForEach(SquadMembers, Member =>                               // Sequence name :Selector

                                    // Sequence name :NearRendezvousPosition
                                    (
                                          DistanceBetweenObjectAndPoint(
                                                out Distance,
                                                Member,
                                                RendezvousPosition) &&
                                          LessEqualFloat(
                                                Distance,
                                                800) &&
                                          assignTaskWithPosition.AssignTaskWithPosition(
                                               AITaskTopicType.DOMINION_WAIT,
                                                RendezvousPosition,
                                                Member) &&
                                          AddInt(
                                                out ChampsNearRendezvousPoint,
                                                ChampsNearRendezvousPoint,
                                                1) &&
                                          strengthEvaluation_ChampionHealth.StrengthEvaluation_ChampionHealth(
                                                out ModifiedChampionValue,
                                                Member,
                                                25) &&
                                          AddFloat(
                                                out AttackersStrengthAtRendezvous,
                                                AttackersStrengthAtRendezvous,
                                                ModifiedChampionValue)
                                    ) ||
                                    assignTaskWithPosition.AssignTaskWithPosition(
                                          AITaskTopicType.DOMINION_GOTO,
                                          RendezvousPosition,
                                          Member)

                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    // Sequence name :EitherAllMembersAvail_Or_StrengthSufficient
                                    (
                                          // Sequence name :AllGathered
                                          (
                                                GetCollectionCount(
                                                      out SquadCount,
                                                      SquadMembers) &&
                                                SquadCount == ChampsNearRendezvousPoint
                                          ) ||
                                          // Sequence name :SufficientStrengthToAssault
                                          (
                                                MultiplyFloat(
                                                      out ModifiedDefStrength,
                                                      1.4f,
                                                      DefenderStrength) &&
                                                GreaterEqualFloat(
                                                      AttackersStrengthAtRendezvous,
                                                      ModifiedDefStrength)
                                          )
                                    ) &&
                                    SetVarInt(
                                          out _DirectAssaultState,
                                          1)
                              )
                              ||
                               DebugAction("MaskFailure")
                        )
                  ) ||
                  // Sequence name :Assault
                  (
                        SetVarInt(
                              out _DirectAssaultState,
                              1) &&
                        GetUnitPosition(
                              out CapturePointPosition,
                              CapturePoint) &&
                        ForEach(SquadMembers, Member =>
                              assignTaskWithPosition.AssignTaskWithPosition(
                                    AITaskTopicType.ASSAULT_CAPTURE_POINT,
                                    CapturePointPosition,
                                    Member)

                        )
                  )
            ;
        __DirectAssaultState = _DirectAssaultState;
        __RendezvousPosition = _RendezvousPosition;

        return result;


    }
}

