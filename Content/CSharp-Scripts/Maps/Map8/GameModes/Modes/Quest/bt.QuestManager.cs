namespace BehaviourTrees;


class QuestManagerClass : OdinLayout
{
    private CapturePointOwnerByIndexClass capturePointOwnerByIndex = new();

    private ChangeScoreClass changeScore = new();

    private PersonalScore_QuestCompleteClass personalScore_QuestComplete = new();

    private GetGuardianHasReferenceClass getGuardianHasReference = new();

    private CalculatePointValuesClass calculatePointValues = new();

    private CountPointsClass countPoints = new();

    private GetScoreFromPointDeltaClass getScoreFromPointDelta = new();

    private QuestObjectiveSelectorClass questObjectiveSelector = new();

    private CapturePointIndexToNameClass capturePointIndexToName = new();

    private QuestActivationClass questActivation = new();

    public bool QuestManager(
           out int __QuestID_Chaos,
      out int __QuestID_Order,
      out float __PreviousQuestCompleteTime,
      out int __QuestObjective_Order,
      out int __QuestObjective_Chaos,
      out float __OrderScore,
      out float __ChaosScore,
      out int __QuestIconSquadID_Order,
      out int __QuestIconSquadID_Chaos,
      out int __QuestRewardsID_Chaos,
      out int __QuestRewardsID_Order,
    TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    int QuestID_Chaos,
    int QuestID_Order,
    float PreviousQuestCompleteTime,
    int QuestObjective_Order,
    int QuestObjective_Chaos,
    AttackableUnit CapturePointGuardian0,
    AttackableUnit CapturePointGuardian1,
    AttackableUnit CapturePointGuardian2,
    AttackableUnit CapturePointGuardian3,
    AttackableUnit CapturePointGuardian4,
    float OrderScore,
    float ChaosScore,
    int QuestBuffGiverEncounterID,
    int QuestIconEncounterID,
    int QuestIconSquadID_Order,
    int QuestIconSquadID_Chaos,
    int QuestIconEncounterID2,
    int QuestRewardsID_Chaos,
    int QuestRewardsID_Order)
    {

        int _QuestID_Chaos = QuestID_Chaos;
        int _QuestID_Order = QuestID_Order;
        float _PreviousQuestCompleteTime = PreviousQuestCompleteTime;
        int _QuestObjective_Order = QuestObjective_Order;
        int _QuestObjective_Chaos = QuestObjective_Chaos;
        float _OrderScore = OrderScore;
        float _ChaosScore = ChaosScore;
        int _QuestIconSquadID_Order = QuestIconSquadID_Order;
        int _QuestIconSquadID_Chaos = QuestIconSquadID_Chaos;
        int _QuestRewardsID_Chaos = QuestRewardsID_Chaos;
        int _QuestRewardsID_Order = QuestRewardsID_Order;



        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        SetVarBool(
                              out Run,
                              true) &&
                        Run == true &&
                        // Sequence name :Selector
                        (
                              // Sequence name :ActiveQuest
                              (
                                    GreaterEqualInt(
                                          _QuestID_Chaos,
                                          0) &&
                                    GreaterEqualInt(
                                          _QuestID_Order,
                                          0) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                capturePointOwnerByIndex.CapturePointOwnerByIndex(
                                                      out ObjectiveOwner_Order,
                                                      out ObjectiveOwner_Order,
                                                     QuestObjective_Order,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE) &&
                                                ObjectiveOwner_Order == TeamId.TEAM_ORDER &&
                                                CompleteQuest(
                                                      QuestID_Order,
                                                      true) &&
                                                RemoveQuest(
                                                      QuestRewardsID_Order) &&
                                                SetVarInt(
                                                      out _QuestID_Order,
                                                      -1) &&
                                                SetVarInt(
                                                      out _QuestRewardsID_Order,
                                                      -1) &&
                                                GetGameTime(
                                                      out _PreviousQuestCompleteTime) &&
                                               changeScore.ChangeScore(
                                                      out _ChaosScore,
                                                      out _OrderScore,
                                                      20,
                                                      ChaosScore,
                                                      OrderScore,
                                                      TeamId.TEAM_ORDER,
                                                      false) &&
                                                personalScore_QuestComplete.PersonalScore_QuestComplete(
                                                      TeamId.TEAM_ORDER,
                                                      20) &&
                                                getGuardianHasReference.GetGuardianHasReference(
                                                      out ObjectiveGuardian,
                                                      QuestObjective_Order,
                                                      CapturePointGuardian0,
                                                      CapturePointGuardian1,
                                                      CapturePointGuardian2,
                                                      CapturePointGuardian3,
                                                      CapturePointGuardian4) &&
                                                GetUnitPosition(
                                                      out UnitPosition,
                                                      ObjectiveGuardian) &&
                                                SpawnSquadFromEncounter(
                                                      out SquadId,
                                                      QuestBuffGiverEncounterID,
                                                      UnitPosition,
                                                      TeamId.TEAM_ORDER,
                                                      ""
                                                      ) &&
                                                DebugAction(

                                                      "spawned") &&
                                                KillSquad(
                                                      QuestIconSquadID_Order) &&
                                                SetVarInt(
                                                      out _QuestIconSquadID_Order,
                                                      -1) &&
                                                Say(
                                                      ObjectiveGuardian,
                                                      "Order Quest Completed")
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                capturePointOwnerByIndex.CapturePointOwnerByIndex(
                                                      out ObjectiveOwner_Chaos,
                                                      out ObjectiveOwner_Chaos,
                                                      QuestObjective_Chaos,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE) &&
                                                ObjectiveOwner_Chaos == TeamId.TEAM_CHAOS &&
                                                CompleteQuest(
                                                      QuestID_Chaos,
                                                      true) &&
                                                RemoveQuest(
                                                      QuestRewardsID_Chaos) &&
                                                SetVarInt(
                                                      out _QuestID_Chaos,
                                                      -1) &&
                                                SetVarInt(
                                                      out _QuestRewardsID_Chaos,
                                                      -1) &&
                                                GetGameTime(
                                                      out _PreviousQuestCompleteTime) &&
                                                changeScore.ChangeScore(
                                                      out _ChaosScore,
                                                      out _OrderScore,
                                                      20,
                                                      _ChaosScore,
                                                      _OrderScore,
                                                      TeamId.TEAM_CHAOS,
                                                      false) &&
                                                personalScore_QuestComplete.PersonalScore_QuestComplete(
                                                      TeamId.TEAM_CHAOS,
                                                      20) &&
                                                getGuardianHasReference.GetGuardianHasReference(
                                                      out ObjectiveGuardian,
                                                      QuestObjective_Chaos,
                                                      CapturePointGuardian0,
                                                      CapturePointGuardian1,
                                                      CapturePointGuardian2,
                                                      CapturePointGuardian3,
                                                      CapturePointGuardian4) &&
                                                GetUnitPosition(
                                                      out UnitPosition,
                                                      CapturePointGuardian0) &&
                                                SpawnSquadFromEncounter(
                                                      out SquadId,
                                                      QuestBuffGiverEncounterID,
                                                      UnitPosition,
                                                      TeamId.TEAM_CHAOS,
                                                      ""
                                                      ) &&
                                                KillSquad(
                                                      QuestIconSquadID_Chaos) &&
                                                SetVarInt(
                                                      out _QuestIconSquadID_Chaos,
                                                      -1) &&
                                                Say(
                                                      ObjectiveGuardian,
                                                      "Chaos Quest Completed")
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                LessInt(
                                                      QuestID_Order,
                                                      0) &&
                                                GreaterEqualInt(
                                                      QuestID_Chaos,
                                                      0) &&
                                                CompleteQuest(
                                                      QuestID_Chaos,
                                                      false) &&
                                                CompleteQuest(
                                                      QuestRewardsID_Chaos,
                                                      false) &&
                                                SetVarInt(
                                                      out _QuestID_Chaos,
                                                      -1) &&
                                                SetVarInt(
                                                      out _QuestRewardsID_Chaos,
                                                      -1) &&
                                                KillSquad(
                                                      QuestIconSquadID_Chaos) &&
                                                SetVarInt(
                                                      out _QuestIconSquadID_Chaos,
                                                      -1)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                LessInt(
                                                      QuestID_Chaos,
                                                      0) &&
                                                GreaterEqualInt(
                                                      QuestID_Order,
                                                      0) &&
                                                CompleteQuest(
                                                      QuestID_Order,
                                                      false) &&
                                                CompleteQuest(
                                                      QuestRewardsID_Order,
                                                      false) &&
                                                SetVarInt(
                                                      out _QuestID_Order,
                                                      -1) &&
                                                SetVarInt(
                                                      out _QuestRewardsID_Order,
                                                      -1) &&
                                                KillSquad(
                                                      QuestIconSquadID_Order) &&
                                                SetVarInt(
                                                      out _QuestIconSquadID_Order,
                                                      -1)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    )
                              ) ||
                              // Sequence name :SelectNewQuest
                              (
                              DebugAction("SelectNewQuest") &&
                                    LessInt(
                                          QuestID_Chaos,
                                          0) &&
                                    LessInt(
                                          QuestID_Order,
                                          0) &&
                                          DebugAction("SelectNewQuest 1 ") &&
                                    GreaterFloat(
                                          OrderScore,
                                          150) && //for test : 1 
                                    GreaterFloat(
                                          ChaosScore,
                                          150) && //for test : 1 
                                          DebugAction("SelectNewQuest 2") &&
                                    GetGameTime(
                                          out CurrentTime) &&
                                          DebugAction($"{CurrentTime}") &&
                                    SubtractFloat(
                                          out TimePassed,
                                          CurrentTime,
                                          PreviousQuestCompleteTime)
                                    &&
                                          ///
                                          AddFloat(
                                          out TimePassed,
                                          TimePassed,
                                          11f) //hack
                                    ///
                                    &&
                                          DebugAction($"{TimePassed}")
                                          &&
                                    GreaterEqualFloat(
                                          TimePassed,
                                          300) && // for test : 90 
                                           DebugAction($"GreaterEqualFloat(TimePassed,") &&
                                    AddFloat(
                                          out _PreviousQuestCompleteTime,
                                          PreviousQuestCompleteTime,
                                          10) &&
                                          DebugAction("SelectNewQuest 3") &&
                                    calculatePointValues.CalculatePointValues(
                                          out CalculatedValue_PointA_Order,
                                          out CalculatedValue_PointB_Order,
                                          out CalculatedValue_PointC_Order,
                                          out CalculatedValue_PointD_Order,
                                          out CalculatedValue_PointE_Order,
                                          CapturePointOwnerA,
                                          CapturePointOwnerB,
                                          CapturePointOwnerC,
                                          CapturePointOwnerD,
                                          CapturePointOwnerE,
                                          TeamId.TEAM_ORDER,
                                          0,
                                          0,
                                          2,
                                          4,
                                          3) &&
                                    calculatePointValues.CalculatePointValues(
                                          out CalculatedValue_PointA_Chaos,
                                          out CalculatedValue_PointB_Chaos,
                                          out CalculatedValue_PointC_Chaos,
                                          out CalculatedValue_PointD_Chaos,
                                          out CalculatedValue_PointE_Chaos,
                                          CapturePointOwnerA,
                                          CapturePointOwnerB,
                                          CapturePointOwnerC,
                                          CapturePointOwnerD,
                                          CapturePointOwnerE,
                                          TeamId.TEAM_CHAOS,
                                          3,
                                          4,
                                          2,
                                          0,
                                          0) &&
                                           DebugAction("SelectNewQuest 4") &&
                                    SubtractFloat(
                                          out CombinedValueA,
                                          CalculatedValue_PointA_Order,
                                          CalculatedValue_PointA_Chaos) &&
                                    SubtractFloat(
                                          out CombinedValueB,
                                          CalculatedValue_PointB_Order,
                                          CalculatedValue_PointB_Chaos) &&
                                    SubtractFloat(
                                          out CombinedValueC,
                                          CalculatedValue_PointC_Order,
                                          CalculatedValue_PointC_Chaos) &&
                                    SubtractFloat(
                                          out CombinedValueD,
                                          CalculatedValue_PointD_Order,
                                          CalculatedValue_PointD_Chaos) &&
                                    SubtractFloat(
                                          out CombinedValueE,
                                          CalculatedValue_PointE_Order,
                                          CalculatedValue_PointE_Chaos) &&
                                          // Sequence name :MaskFailure

                                          // Sequence name :Sequence

                                          DebugAction("SelectNewQuest 5") &&
                                                SetVarFloat(
                                                      out TempOrderScore,
                                                      OrderScore) &&
                                                SetVarFloat(
                                                      out TempChaosScore,
                                                      ChaosScore) &&
                                                countPoints.CountPoints(
                                                      out TotalOrderPoint,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE,
                                                    TeamId.TEAM_ORDER) &&
                                                countPoints.CountPoints(
                                                      out TotalChaosPoint,
                                                      CapturePointOwnerA,
                                                      CapturePointOwnerB,
                                                      CapturePointOwnerC,
                                                      CapturePointOwnerD,
                                                      CapturePointOwnerE,
                                                      TeamId.TEAM_CHAOS) &&
                                                      DebugAction("SelectNewQuest 6") &&

                                                SubtractInt(
                                                      out TotalPointDiff,
                                                      TotalChaosPoint,
                                                      TotalOrderPoint) &&
                                                getScoreFromPointDelta.GetScoreFromPointDelta(
                                                      out ScoreChange,
                                                      TotalPointDiff) &&
                                                MultiplyFloat(
                                                      out ScoreChange,
                                                      ScoreChange,
                                                      32.4f) &&

                                                // Sequence name :Selector
                                                (
                                                      // Sequence name :MoreOrderPoints
                                                      (
                                                            GreaterInt(
                                                                  TotalOrderPoint,
                                                                  TotalChaosPoint) &&
                                                            AddFloat(
                                                                  out TempOrderScore,
                                                                  TempOrderScore,
                                                                  ScoreChange)
                                                      ) ||
                                                      // Sequence name :MoreChaosPoints
                                                      (
                                                            GreaterInt(
                                                                  TotalChaosPoint,
                                                                  TotalOrderPoint) &&
                                                            AddFloat(
                                                                  out TempChaosScore,
                                                                  TempChaosScore,
                                                                  ScoreChange)
                                                      )

                                                /*  || 
                                                  DebugAction("force quest ") &&
                                                  AddFloat(
                                                              out TempChaosScore,
                                                              TempChaosScore,
                                                              ScoreChange)*/

                                                )

                                     &&
                                     DebugAction("SelectNewQuest 7") &&


                                    questObjectiveSelector.QuestObjectiveSelector(
                                          out _QuestObjective_Order,
                                          out _QuestObjective_Chaos,
                                          CapturePointOwnerA,
                                          CapturePointOwnerB,
                                          CapturePointOwnerC,
                                          CapturePointOwnerD,
                                          CapturePointOwnerE,
                                          default,
                                          default,
                                          default,
                                          default,
                                          default,
                                          TempOrderScore,
                                          TempChaosScore,
                                          CapturePointGuardian0,
                                          CapturePointGuardian1,
                                          CapturePointGuardian2,
                                          CapturePointGuardian3,
                                          CapturePointGuardian4) &&


                                    //sure jusqu'as la 

                                    GetTurret(
                                          out OrderShrineTurret,
                                          TeamId.TEAM_ORDER,
                                          0,
                                          1) &&
                                    GetTurret(
                                          out ChaosShrineTurret,
                                          TeamId.TEAM_CHAOS,
                                          0,
                                          1) &&
                                          DebugAction("SelectNewQuest 8") &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :NeutralPoint
                                          (
                                                QuestObjective_Chaos == QuestObjective_Order &&
                                                capturePointIndexToName.CapturePointIndexToName(
                                                      out CapturePointName,
                                                      QuestObjective_Order) &&
                                                AddString(
                                                      out QuestText,
                                                      "Capture",
                                                      CapturePointName) &&
                                                AddString(
                                                      out QuestText,
                                                      QuestText,
                                                       "before the enemy") &&
                                                       DebugAction("SelectNewQuest 9") &&
                                                ActivateQuestForTeam(
                                                      out _QuestID_Order,
                                                      QuestText,
                                                      TeamId.TEAM_ORDER,
                                                      QuestType.Primary,
                                                      false,
                                                      "",
                                                      "") &&
                                                ActivateQuestForTeam(
                                                      out _QuestID_Chaos,
                                                      QuestText,
                                                      TeamId.TEAM_CHAOS,
                                                      QuestType.Primary,
                                                      false,
                                                      "",
                                                      "") &&
                                                ActivateQuestForTeam(
                                                      out _QuestRewardsID_Order,
                                                      "Reward: Team Buff &amp; Nexus Damage",
                                                      TeamId.TEAM_ORDER,
                                                      QuestType.Primary,
                                                      false,
                                                      "",
                                                      "") &&
                                                ActivateQuestForTeam(
                                                      out _QuestRewardsID_Chaos,
                                                      "Reward: Team Buff &amp; Nexus Damage",
                                                      TeamId.TEAM_CHAOS,
                                                      QuestType.Primary,
                                                      false,
                                                      "",
                                                      "") &&
                                                      DebugAction("SelectNewQuest 10") &&
                                                Announcement_OnVictoryPointThreshold(
                                                      OrderShrineTurret,
                                                      4) &&



                                                getGuardianHasReference.GetGuardianHasReference(
                                                      out ObjectiveGuardian,
                                                      QuestObjective_Order,
                                                      CapturePointGuardian0,
                                                      CapturePointGuardian1,
                                                      CapturePointGuardian2,
                                                      CapturePointGuardian3,
                                                      CapturePointGuardian4) &&
                                                GetUnitPosition(
                                                      out UnitPosition,
                                                      ObjectiveGuardian) &&
                                                SpawnSquadFromEncounter(
                                                      out _QuestIconSquadID_Order,
                                                      QuestIconEncounterID,
                                                      UnitPosition,
                                                      TeamId.TEAM_ORDER,
                                                      "QuestIcon"
                                                      ) &&
                                                SpawnSquadFromEncounter(
                                                      out _QuestIconSquadID_Chaos,
                                                      QuestIconEncounterID2,
                                                      UnitPosition,
                                                      TeamId.TEAM_CHAOS,
                                                      "QuestIcon"
                                                      ) &&


                                                      DebugAction("SelectNewQuest 11") &&
                                                PingMinimapLocation(
                                                      OrderShrineTurret,
                                                      UnitPosition,
                                                      true) &&
                                                PingMinimapLocation(
                                                      ChaosShrineTurret,
                                                      UnitPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :DifferentPoints
                                          (

                                          DebugAction("SelectNewQuest 9") &&
                                                NotEqualInt(
                                                      QuestObjective_Chaos,
                                                      QuestObjective_Order) &&
                                                capturePointIndexToName.CapturePointIndexToName(
                                                      out CapturePointNameOrder,
                                                      QuestObjective_Order) &&
                                                capturePointIndexToName.CapturePointIndexToName(
                                                      out CapturePointNameChaos,
                                                      QuestObjective_Chaos) &&
                                                AddString(
                                                      out QuestText,
                                                      "Capture",
                                                     CapturePointNameOrder) &&
                                                AddString(
                                                      out QuestText,
                                                      QuestText,
                                                       "before enemy captures") &&
                                                AddString(
                                                      out QuestText,
                                                     QuestText,
                                                      CapturePointNameChaos) &&
                                                questActivation.QuestActivation(
                                                      out _QuestID_Order,
                                                      QuestObjective_Order,
                                                      true,
                                                      TeamId.TEAM_ORDER) &&
                                                questActivation.QuestActivation(
                                                      out _QuestID_Chaos,
                                                      QuestObjective_Chaos,
                                                      true,
                                                      TeamId.TEAM_CHAOS) &&
                                                questActivation.QuestActivation(
                                                      out _QuestRewardsID_Order,
                                                      QuestObjective_Chaos,
                                                      false,
                                                      TeamId.TEAM_ORDER) &&

                                                      DebugAction("SelectNewQuest 10") &&
                                                questActivation.QuestActivation(
                                                      out _QuestRewardsID_Chaos,
                                                      QuestObjective_Order,
                                                      false,
                                                      TeamId.TEAM_CHAOS) &&
                                                AddString(
                                                      out QuestText,
                                                      "Capture",
                                                      CapturePointNameChaos) &&
                                                AddString(
                                                      out QuestText,
                                                      QuestText,
                                                       "before enemy captures") &&
                                                AddString(
                                                      out QuestText,
                                                      QuestText,
                                                      CapturePointNameOrder) &&
                                                Announcement_OnVictoryPointThreshold(
                                                      OrderShrineTurret,
                                                      4) &&
                                                getGuardianHasReference.GetGuardianHasReference(
                                                      out ObjectiveGuardian,
                                                      QuestObjective_Order,
                                                      CapturePointGuardian0,
                                                      CapturePointGuardian1,
                                                      CapturePointGuardian2,
                                                      CapturePointGuardian3,
                                                      CapturePointGuardian4) &&
                                                GetUnitPosition(
                                                      out UnitPosition,
                                                      ObjectiveGuardian) &&
                                                SpawnSquadFromEncounter(
                                                      out _QuestIconSquadID_Order,
                                                      QuestIconEncounterID,
                                                      UnitPosition,
                                                      TeamId.TEAM_ORDER,
                                                      "QuestIcon"
                                                      ) &&

                                                      DebugAction("SelectNewQuest 11") &&
                                                getGuardianHasReference.GetGuardianHasReference(
                                                      out ObjectiveGuardian,
                                                      QuestObjective_Chaos,
                                                      CapturePointGuardian0,
                                                      CapturePointGuardian1,
                                                      CapturePointGuardian2,
                                                      CapturePointGuardian3,
                                                      CapturePointGuardian4) &&
                                                PingMinimapLocation(
                                                      OrderShrineTurret,
                                                      UnitPosition,
                                                      true) &&
                                                GetUnitPosition(
                                                      out UnitPosition,
                                                      ObjectiveGuardian) &&
                                                SpawnSquadFromEncounter(
                                                      out _QuestIconSquadID_Chaos,
                                                      QuestIconEncounterID2,
                                                      UnitPosition,
                                                      TeamId.TEAM_CHAOS,
                                                      "QuestIcon"
                                                      ) &&
                                                PingMinimapLocation(
                                                      ChaosShrineTurret,
                                                      UnitPosition,
                                                      true)

                                          )
                                    )
                              )
                        )
                  )
                  ||
                               DebugAction("MaskFailure")
            ;
        __QuestID_Chaos = _QuestID_Chaos;
        __QuestID_Order = _QuestID_Order;
        __PreviousQuestCompleteTime = _PreviousQuestCompleteTime;
        __QuestObjective_Order = _QuestObjective_Order;
        __QuestObjective_Chaos = _QuestObjective_Chaos;
        __OrderScore = _OrderScore;
        __ChaosScore = _ChaosScore;
        __QuestIconSquadID_Order = _QuestIconSquadID_Order;
        __QuestIconSquadID_Chaos = _QuestIconSquadID_Chaos;
        __QuestRewardsID_Chaos = _QuestRewardsID_Chaos;
        __QuestRewardsID_Order = _QuestRewardsID_Order;
        return result;
    }
}

