using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class CapturePointMission_LogicClass : AImission_bt
{

    public bool CapturePointMission_Logic()
    {


        var capturePointIndex = new GetNearestCapturePointClass();
        var getCapturePoints = new GetCapturePointsClass();
        var subMission_ReturnToBase = new SubMission_ReturnToBaseClass();
        var getDefendersAtPoint = new GetDefendersAtPointClass();
        var assignTaskWithPosition = new AssignTaskWithPositionClass();
        var subMission_DirectAssaultManager = new SubMission_DirectAssaultManagerClass();
        var subMission_PushToCapture = new SubMission_PushToCaptureClass();

        return

                  // Sequence name :Selector

                  // Sequence name :Initialize
                  (
                        TestAIFirstTime(
                              true) &&
                        GetAIMissionSelf(
                              out ThisMission) &&
                        GetAIMissionPosition(
                              out ThisMissionPosition,
                              ThisMission) &&
                        SetVarBool(
                              out InitCapturePoint,
                              false) &&
                        SetVarInt(
                              out SubMission_AssaultModeState,
                              -1)
                  ) ||
                  // Sequence name :FindCapturePointTarget
                  (
                        InitCapturePoint == false &&
                        GetAIMissionSquadMembers(
                              out SquadMembers,
                              ThisMission) &&
                        ForEach(SquadMembers, ReferenceUnit =>
                        SetVarAttackableUnit(
                                    out ReferenceUnit,
                                    ReferenceUnit)
                         &&
                        GetUnitsInTargetArea(
                              out CapturePoints,
                              ReferenceUnit,
                              ThisMissionPosition,
                              200,
                              AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable
                              ) &&
                 ForEach(CapturePoints, CapturePoint => (
                        SetVarAttackableUnit(
                                    out CapturePoint,
                                    CapturePoint)
                         &&
                        capturePointIndex.GetNearestCapturePoint(
                              out CapturePointIndex,
                              out ClosestCapturePointPosition,
                              out ClosestDistance,
                              CapturePoint) &&
                        getCapturePoints.GetCapturePoints(
                              out CapturePointA,
                              out CapturePointB,
                              out CapturePointC,
                              out CapturePointD,
                              out CapturePointE,
                              ReferenceUnit) &&
                        SetVarBool(
                              out InitCapturePoint,
                              true) &&
                        GetUnitTeam(
                              out CapturePointPreviousState,
                              CapturePoint)
                  ) ||
                     // Sequence name :DetermineSubMissionAndExecute
                     (
                        InitCapturePoint == true &&
                        SetVarBool(
                              out ResetParameters,
                              false) &&
                        GetAIMissionSquadMembers(
                              out SquadMembers,
                              ThisMission) &&
                        SetVarFloat(
                              out RetreatHealthRatioModifier,
                              0) &&
                        // Sequence name :HasSquadMember
                        (
                              ForEach(SquadMembers, ReferenceUnit => //  ||

                                    SetVarAttackableUnit(
                                          out ReferenceUnit,
                                          ReferenceUnit)
                              ) ||
                              SetVarBool(
                                    out ResetParameters,
                                    true)
                        ) &&
                        // Sequence name :CheckForStateChange
                        (



                              // Sequence name :CheckStateChange
                              (
                                    GetUnitTeam(
                                          out CurrentCapturePointState,
                                          CapturePoint) &&
                                    CurrentCapturePointState == CapturePointPreviousState
                              ) ||
                              SetVarBool(
                                    out ResetParameters,
                                    true)
                        ) &&
                        // Sequence name :ResetParameters_Or_RunSubMissions
                        (
                              // Sequence name :Reset
                              (
                                    ResetParameters == true &&
                                    GetUnitTeam(
                                          out CapturePointPreviousState,
                                          CapturePoint) &&
                                    SetVarInt(
                                          out SubMission_AssaultModeState,
                                          -1)
                              ) ||
                              // Sequence name :SubMissions
                              (
                                    GetUnitTeam(
                                          out CapturePointTeam,
                                          CapturePoint) &&
                                    GetUnitTeam(
                                          out ReferenceTeam,
                                          ReferenceUnit) &&
                                    getDefendersAtPoint.GetDefendersAtPoint(
                                          out NumDefenders,
                                          out StrOfDef,
                                          out DefenderStrength_Normalized,
                                          CapturePointIndex,
                                          ReferenceUnit,
                                          CapturePoint) &&
                                    SetVarInt(
                                          out NumAttackers,
                                          0) &&
                                    SetVarFloat(
                                          out StrOfAttackers,
                                          0) &&
                                    ForEach(SquadMembers, SquadMember =>
                                                // Sequence name :Sequence

                                                TestUnitCondition(
                                                      SquadMember) &&
                                                AddInt(
                                                      out NumAttackers,
                                                      NumAttackers,
                                                      1) &&
                                                      // Sequence name :StrOfAttackers

                                                      GetUnitCurrentHealth(
                                                            out CurrentHealth,
                                                            SquadMember) &&
                                                      GetUnitMaxHealth(
                                                            out MaxHealth,
                                                            SquadMember) &&
                                                      DivideFloat(
                                                            out HealthRatio,
                                                            CurrentHealth,
                                                            MaxHealth) &&
                                                      AddFloat(
                                                            out StrOfAttackers,
                                                            StrOfAttackers,
                                                            HealthRatio)


                                    ) &&
                                    // Sequence name :SubMissions
                                    (
                                          // Sequence name :Defend
                                          (
                                                CapturePointTeam == ReferenceTeam &&
                                                ForEach(SquadMembers, Entity =>
                                                            // Sequence name :ReturnToBase or Defend

                                                            // Sequence name :ReturnToBase
                                                            (
                                                                  NumDefenders == 0 &&
                                                                  GetUnitCurrentHealth(
                                                                        out CurrentHealth,
                                                                        Entity) &&
                                                                  GetUnitMaxHealth(
                                                                        out MaxHealth,
                                                                        Entity) &&
                                                                  DivideFloat(
                                                                        out HealthRatio,
                                                                        CurrentHealth,
                                                                        MaxHealth) &&
                                                                  AddFloat(
                                                                        out HealthRatio,
                                                                        HealthRatio,
                                                                        RetreatHealthRatioModifier) &&
                                                                  LessFloat(
                                                                        HealthRatio,
                                                                        0.25f) &&
                                                                  subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                        Entity)
                                                            ) ||
                                                                  // Sequence name :RTB Num Def &gt; 0

                                                                  // Sequence name :StrOfDef&gt;StrOfAttackers
                                                                  (
                                                                        GreaterFloat(
                                                                              StrOfDef,
                                                                              StrOfAttackers) &&
                                                                        GreaterInt(
                                                                              NumDefenders,
                                                                              0) &&
                                                                        GetUnitCurrentHealth(
                                                                              out CurrentHealth,
                                                                              Entity) &&
                                                                        GetUnitMaxHealth(
                                                                              out MaxHealth,
                                                                              Entity) &&
                                                                        DivideFloat(
                                                                              out HealthRatio,
                                                                              CurrentHealth,
                                                                              MaxHealth) &&
                                                                        AddFloat(
                                                                              out HealthRatio,
                                                                              HealthRatio,
                                                                              RetreatHealthRatioModifier) &&
                                                                        LessFloat(
                                                                              HealthRatio,
                                                                              0.15f) &&
                                                                        subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                              Entity)
                                                                  ) ||
                                                                  // Sequence name :StrOfAttackers&gt;StrOfDef
                                                                  (
                                                                        GreaterInt(
                                                                              NumDefenders,
                                                                              0) &&
                                                                        GetUnitCurrentHealth(
                                                                              out CurrentHealth,
                                                                              Entity) &&
                                                                        GetUnitMaxHealth(
                                                                              out MaxHealth,
                                                                              Entity) &&
                                                                        DivideFloat(
                                                                              out HealthRatio,
                                                                              CurrentHealth,
                                                                              MaxHealth) &&
                                                                        AddFloat(
                                                                              out HealthRatio,
                                                                              HealthRatio,
                                                                              RetreatHealthRatioModifier) &&
                                                                        LessFloat(
                                                                              HealthRatio,
                                                                              0.2f) &&
                                                                        SubtractFloat(
                                                                              out StrOfAttackers,
                                                                              StrOfAttackers,
                                                                              HealthRatio) &&
                                                                        LessEqualFloat(
                                                                              StrOfDef,
                                                                              StrOfAttackers) &&
                                                                        subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                              Entity)
                                                                  )
                                                             ||
                                                            // Sequence name :Defend
                                                            (
                                                                  TestUnitIsChanneling(
                                                                        Entity, false) &&
                                                                  assignTaskWithPosition.AssignTaskWithPosition(
                                                                       AITaskTopicType.DEFEND_CAPTURE_POINT,
                                                                        ClosestCapturePointPosition,
                                                                        Entity)
                                                            )

                                                ) &&
                                                SetVarInt(
                                                      out SubMission_AssaultModeState,
                                                      -1)
                                          ) ||
                                          // Sequence name :Ninja_NoDefenders
                                          (
                                                LessEqualInt(
                                                      NumDefenders,
                                                      0) &&
                                                ForEach(SquadMembers, Entity =>                                                       // Sequence name :Sequence

                                                            TestUnitIsChanneling(
                                                                  Entity, false) &&
                                                            assignTaskWithPosition.AssignTaskWithPosition(
                                                                  AITaskTopicType.NINJA_CAPTURE_POINT,
                                                                  ClosestCapturePointPosition,
                                                                  Entity)

                                                ) &&
                                                SetVarInt(
                                                      out SubMission_AssaultModeState,
                                                      -1)
                                          ) ||
                                          // Sequence name :PushToCapture_Attackers&lt;1.45xDefenders
                                          (
                                                NotEqualUnitTeam(
                                                      CapturePointTeam,
                                                      TeamId.TEAM_NEUTRAL) &&
                                                MultiplyFloat(
                                                      out DefendersValue,
                                                      NumDefenders,
                                                      1.45f) &&
                                                AddFloat(
                                                      out AttackersValue,
                                                      NumAttackers,
                                                      0) &&
                                                LessFloat(
                                                      AttackersValue,
                                                      DefendersValue) &&
                                                ForEach(SquadMembers, Entity =>
                                                            // Sequence name :ReturnToBase or PushToCapture

                                                            // Sequence name :ReturnToBase
                                                            (
                                                                  NumDefenders == 0 &&
                                                                  GetUnitCurrentHealth(
                                                                        out CurrentHealth,
                                                                        Entity) &&
                                                                  GetUnitMaxHealth(
                                                                        out MaxHealth,
                                                                        Entity) &&
                                                                  DivideFloat(
                                                                        out HealthRatio,
                                                                        CurrentHealth,
                                                                        MaxHealth) &&
                                                                  AddFloat(
                                                                        out HealthRatio,
                                                                        HealthRatio,
                                                                        RetreatHealthRatioModifier) &&
                                                                  LessFloat(
                                                                        HealthRatio,
                                                                        0.2f) &&
                                                                  subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                        Entity) &&
                                                                  SetVarInt(
                                                                        out SubMission_AssaultModeState,
                                                                        -1)
                                                            ) ||
                                                            // Sequence name :RTB Num Def &gt; 0
                                                            (
                                                                  GreaterInt(
                                                                        NumDefenders,
                                                                        0) &&
                                                                  GetUnitCurrentHealth(
                                                                        out CurrentHealth,
                                                                        Entity) &&
                                                                  GetUnitMaxHealth(
                                                                        out MaxHealth,
                                                                        Entity) &&
                                                                  DivideFloat(
                                                                        out HealthRatio,
                                                                        CurrentHealth,
                                                                        MaxHealth) &&
                                                                  AddFloat(
                                                                        out HealthRatio,
                                                                        HealthRatio,
                                                                        RetreatHealthRatioModifier) &&
                                                                  LessFloat(
                                                                        HealthRatio,
                                                                        0.25f) &&
                                                                  GreaterFloat(
                                                                        StrOfDef,
                                                                        HealthRatio) &&
                                                                  subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                        Entity) &&
                                                                  SetVarInt(
                                                                        out SubMission_AssaultModeState,
                                                                        -1)
                                                            ) ||
                                                            // Sequence name :PushToCapture
                                                            (
                                                                  TestUnitIsChanneling(
                                                                        Entity, false) &&
                                                                  subMission_PushToCapture.SubMission_PushToCapture(
                                                                        CapturePointA,
                                                                        CapturePointB,
                                                                        CapturePointC,
                                                                        CapturePointD,
                                                                        CapturePointE,
                                                                        CapturePointIndex,
                                                                        SquadMembers,
                                                                        CapturePoint) &&
                                                                  SetVarInt(
                                                                        out SubMission_AssaultModeState,
                                                                        -1)
                                                            )

                                                )
                                          ) ||
                                                // Sequence name :DirectAssaultCapture

                                                ForEach(SquadMembers, Entity =>
                                                            // Sequence name :ReturnToBase or DirectAssault

                                                            // Sequence name :ReturnToBase
                                                            (
                                                                  NumDefenders == 0 &&
                                                                  GetUnitCurrentHealth(
                                                                        out CurrentHealth,
                                                                        Entity) &&
                                                                  GetUnitMaxHealth(
                                                                        out MaxHealth,
                                                                        Entity) &&
                                                                  DivideFloat(
                                                                        out HealthRatio,
                                                                        CurrentHealth,
                                                                        MaxHealth) &&
                                                                  AddFloat(
                                                                        out HealthRatio,
                                                                        HealthRatio,
                                                                        RetreatHealthRatioModifier) &&
                                                                  LessFloat(
                                                                        HealthRatio,
                                                                        0.2f) &&
                                                                  subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                        Entity)
                                                            ) ||
                                                                  // Sequence name :RTB Num Def &gt; 0

                                                                  // Sequence name :StrOfDef&gt;StrOfAttackers
                                                                  (
                                                                        GreaterFloat(
                                                                              StrOfDef,
                                                                              StrOfAttackers) &&
                                                                        GreaterInt(
                                                                              NumDefenders,
                                                                              0) &&
                                                                        GetUnitCurrentHealth(
                                                                              out CurrentHealth,
                                                                              Entity) &&
                                                                        GetUnitMaxHealth(
                                                                              out MaxHealth,
                                                                              Entity) &&
                                                                        DivideFloat(
                                                                              out HealthRatio,
                                                                              CurrentHealth,
                                                                              MaxHealth) &&
                                                                        AddFloat(
                                                                              out HealthRatio,
                                                                              HealthRatio,
                                                                              RetreatHealthRatioModifier) &&
                                                                        LessFloat(
                                                                              HealthRatio,
                                                                              0.25f) &&
                                                                        subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                              Entity)
                                                                  ) ||
                                                                  // Sequence name :StrOfAttackers&gt;StrOfDef
                                                                  (
                                                                        GreaterInt(
                                                                              NumDefenders,
                                                                              0) &&
                                                                        GetUnitCurrentHealth(
                                                                              out CurrentHealth,
                                                                              Entity) &&
                                                                        GetUnitMaxHealth(
                                                                              out MaxHealth,
                                                                              Entity) &&
                                                                        DivideFloat(
                                                                              out HealthRatio,
                                                                              CurrentHealth,
                                                                              MaxHealth) &&
                                                                        AddFloat(
                                                                              out HealthRatio,
                                                                              HealthRatio,
                                                                              RetreatHealthRatioModifier) &&
                                                                        LessFloat(
                                                                              HealthRatio,
                                                                              0.2f) &&
                                                                        SubtractFloat(
                                                                              out StrOfAttackers,
                                                                              StrOfAttackers,
                                                                              HealthRatio) &&
                                                                        LessEqualFloat(
                                                                              StrOfDef,
                                                                              StrOfAttackers) &&
                                                                        subMission_ReturnToBase.SubMission_ReturnToBase(
                                                                              Entity)
                                                                  )
                                                             ||
                                                            // Sequence name :DirectAssault
                                                            (
                                                                  TestUnitIsChanneling(
                                                                        Entity, false) &&

                                                                        subMission_DirectAssaultManager.SubMission_DirectAssaultManager(
                                                                        out SubMission_AssaultModeState,
                                                                        out RendezvousPosition,
                                                                        RendezvousPosition,
                                                                        SquadMembers,
                                                                        SubMission_AssaultModeState,
                                                                        CapturePointIndex,
                                                                        CapturePoint,
                                                                        DefenderStrength_Normalized)

                                                            )

                                                )

                                    )
                              )
                        )
                  )

            //todo check parenthesis 
            )));
    }
}

