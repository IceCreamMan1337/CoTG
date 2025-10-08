namespace BehaviourTrees;


class ObeliskCapturePointHelperClass : OdinLayout
{
    private CapturePointCapturedAnnouncerClass capturePointCapturedAnnouncer = new();
    private PersonalScore_CaptureClass personalScore_Capture = new();
    private ChangeScoreFlatCapClass changeScoreFlatCap = new();
    private ObeliskCapturePointGoldRewardsClass obeliskCapturePointGoldRewards = new();
    private PersonalScore_NeutralizeClass personalScore_Neutralize = new();
    private MinionSpawnTimeSetClass minionSpawnTimeSet = new();

    public bool ObeliskCapturePointHelper(
    out TeamId __CapturePointOwner,
    out float __MinionSpawnTimeToLane1,
    out float __MinionSpawnTimeToLane2,
    out float __MinionSpawnTimeFromLane1,
    out float __MinionSpawnTimeFromLane2,
    out float __ChaosScore,
    out float __OrderScore,
    out TeamId __PreviousCapturePointOwner,
    TeamId CapturePointOwner,
    int CapturePointIndex,
    TeamId AdjacentCapturePointOwner1,
    TeamId AdjacentCapturePointOwner2,
    float MinionSpawnTimeToLane1,
    float MinionSpawnTimeToLane2,
    float MinionSpawnTimeFromLane1,
    float MinionSpawnTimeFromLane2,
    float ChaosScore,
    float OrderScore,
    TeamId PreviousCapturePointOwner,
    float ScoringFloor,
    float Score_PointCapture,
    float Score_PointNeutralize,
    float Score_PointAssist,
    float Score_Strategist,
    float Score_Opportunist,
    AttackableUnit Guardian,
    float MinionSpawnRate_Seconds,
    bool EnableSecondaryCallouts,
    AttackableUnit ChaosShrineTurret,
    AttackableUnit OrderShrineTurret)
    {

        TeamId _CapturePointOwner = CapturePointOwner;
        float _MinionSpawnTimeToLane1 = MinionSpawnTimeToLane1;
        float _MinionSpawnTimeToLane2 = MinionSpawnTimeToLane2;
        float _MinionSpawnTimeFromLane1 = MinionSpawnTimeFromLane1;
        float _MinionSpawnTimeFromLane2 = MinionSpawnTimeFromLane2;
        float _ChaosScore = ChaosScore;
        float _OrderScore = OrderScore;
        TeamId _PreviousCapturePointOwner = PreviousCapturePointOwner;



        bool result =
                  // Sequence name :MaskFailure

                  // Sequence name :Sequence
                  (
                        SetVarFloat(
                              out Radius,
                              900) &&
                        SetVarBool(
                              out AwardCapturePoint,
                              true) &&
                        GetUnitPosition(
                              out GuardianPosition,
                              Guardian) &&
                        GetUnitMaxPAR(
                              out MaxHealth,
                              Guardian,
                             PrimaryAbilityResourceType.MANA) &&
                        GetUnitCurrentPAR(
                              out CurrentHealth,
                              Guardian,
                              PrimaryAbilityResourceType.MANA) &&
                        DivideFloat(
                              out HealthPercent,
                              CurrentHealth,
                              MaxHealth) &&
                        // Sequence name :Neutral_Or_OrderChaos
                        (
                              // Sequence name :Neutral
                              (
                                    _CapturePointOwner == TeamId.TEAM_NEUTRAL &&
                                    // Sequence name :GoingOrderOrChaos
                                    (
                                          // Sequence name :GoingToOrder
                                          (
                                                GreaterEqualFloat(
                                                      HealthPercent,
                                                      0.98f) &&
                                                SetVarUnitTeam(
                                                      out ToSet,
                                                      TeamId.TEAM_ORDER) &&
                                                SetUnitTeam(
                                                      Guardian,
                                                      ToSet) &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Sequence
                                                      (
                                                            AwardCapturePoint == true &&
                                                            GreaterFloat(
                                                                  ChaosScore,
                                                                  ScoringFloor) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_friendly_kill",
                                                                  TeamId.TEAM_ORDER,
                                                                  -2) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_enemy_kill",
                                                                  TeamId.TEAM_CHAOS,
                                                                  -2)
                                                      )
                                                      ||
                               DebugAction("MaskFailure")
                                                ) &&
                                                capturePointCapturedAnnouncer.CapturePointCapturedAnnouncer(
                                                      CapturePointIndex,
                                                      OrderShrineTurret,
                                                      Radius,
                                                      Guardian)
                                          ) ||
                                          // Sequence name :GoingToChaos
                                          (
                                                LessEqualFloat(
                                                      HealthPercent,
                                                      0.02f) &&

                                                SetVarUnitTeam(
                                                      out ToSet,
                                                      TeamId.TEAM_CHAOS) &&
                                                SetUnitTeam(
                                                      Guardian,
                                                      TeamId.TEAM_CHAOS) &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Sequence
                                                      (
                                                            AwardCapturePoint == true &&
                                                            GreaterFloat(
                                                                  OrderScore,
                                                                  ScoringFloor) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_friendly_kill",
                                                                  TeamId.TEAM_CHAOS,
                                                                  -2) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_enemy_kill",
                                                                  TeamId.TEAM_ORDER,
                                                                  -2)
                                                      )
                                                      ||
                               DebugAction("MaskFailure")
                                                ) &&
                                                capturePointCapturedAnnouncer.CapturePointCapturedAnnouncer(
                                                      CapturePointIndex,
                                                      ChaosShrineTurret,
                                                      Radius,
                                                      Guardian)
                                          )
                                    ) &&

                                    personalScore_Capture.PersonalScore_Capture(
                                          Radius,
                                          1500,
                                          Guardian,
                                          Score_PointAssist,
                                          Score_PointCapture,
                                          Score_Strategist,
                                          EnableSecondaryCallouts) &&
                                    SetVarUnitTeam(
                                          out _PreviousCapturePointOwner,
                                          _CapturePointOwner) &&
                                    SetVarUnitTeam(
                                          out _CapturePointOwner,
                                          ToSet) &&
                                    IncPARbt(
                                          Guardian,
                                          MaxHealth,
                                          PrimaryAbilityResourceType.MANA) &&

                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                AwardCapturePoint == true &&
                                                changeScoreFlatCap.ChangeScoreFlatCap(
                                                      out _OrderScore,
                                                      out _ChaosScore,
                                                      out ScoreChanged,
                                                      OrderScore,
                                                      ChaosScore,
                                                      false,
                                                      ScoringFloor,
                                                      2,
                                                      ToSet)
                                          )
                                          ||
                               DebugAction("MaskFailure")
                                    ) &&
                                    obeliskCapturePointGoldRewards.ObeliskCapturePointGoldRewards(
                                          ToSet,
                                          GuardianPosition)
                              ) ||
                              // Sequence name :OrderOrChaos_Neutralized
                              (
                                    LessEqualFloat(
                                          HealthPercent,
                                          0.02f) &&
                                    SetVarFloat(
                                          out PercentToHealTo,
                                          0.5f) &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :ChaosBeingNeutralized
                                          (
                                                _CapturePointOwner == TeamId.TEAM_CHAOS &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Sequence
                                                      (
                                                            AwardCapturePoint == true &&
                                                            GreaterFloat(
                                                                  ChaosScore,
                                                                  ScoringFloor) &&
                                                            changeScoreFlatCap.ChangeScoreFlatCap(
                                                                  out _OrderScore,
                                                                  out _ChaosScore,
                                                                  out ScoreChanged,
                                                                  OrderScore,
                                                                  ChaosScore,
                                                                  false,
                                                                  ScoringFloor,
                                                                 3,
                                                                  TeamId.TEAM_ORDER) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_friendly_kill",
                                                                  TeamId.TEAM_ORDER,
                                                                  -3) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_enemy_kill",
                                                                  TeamId.TEAM_CHAOS,
                                                                  -3)
                                                      )
                                                      ||
                               DebugAction("MaskFailure")
                                                ) &&
                                                Announcement_OnCapturePointNeutralized(
                                                      OrderShrineTurret,
                                                      CapturePointIndex) &&
                                                personalScore_Neutralize.PersonalScore_Neutralize(
                                                      900,
                                                      1500,
                                                      Guardian,
                                                      Score_PointAssist,
                                                      Score_PointNeutralize,
                                                      Score_Opportunist,
                                                      EnableSecondaryCallouts) &&
                                                obeliskCapturePointGoldRewards.ObeliskCapturePointGoldRewards(
                                                      TeamId.TEAM_ORDER,
                                                      GuardianPosition)
                                          ) ||
                                          // Sequence name :OrderBeingNeutralized
                                          (
                                                _CapturePointOwner == TeamId.TEAM_ORDER &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Sequence
                                                      (
                                                            AwardCapturePoint == true &&
                                                            GreaterFloat(
                                                                  OrderScore,
                                                                  ScoringFloor) &&
                                                            changeScoreFlatCap.ChangeScoreFlatCap(
                                                                  out _OrderScore,
                                                                  out _ChaosScore,
                                                                  out ScoreChanged,
                                                                  OrderScore,
                                                                  ChaosScore,
                                                                  false,
                                                                  ScoringFloor,
                                                                  3,
                                                                  TeamId.TEAM_CHAOS) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_friendly_kill",
                                                                  TeamId.TEAM_CHAOS,
                                                                  -3) &&
                                                            PlayFloatingTextOnUnitForTeam(
                                                                  Guardian,
                                                                  "game_floating_enemy_kill",
                                                                  TeamId.TEAM_ORDER,
                                                                  -3)
                                                      )
                                                      ||
                               DebugAction("MaskFailure")
                                                ) &&
                                                Announcement_OnCapturePointNeutralized(
                                                      ChaosShrineTurret,
                                                      CapturePointIndex) &&
                                                personalScore_Neutralize.PersonalScore_Neutralize(
                                                      900,
                                                      1500,
                                                      Guardian,
                                                      Score_PointAssist,
                                                      Score_PointNeutralize,
                                                      Score_Opportunist,
                                                      EnableSecondaryCallouts) &&
                                                obeliskCapturePointGoldRewards.ObeliskCapturePointGoldRewards(
                                                      TeamId.TEAM_CHAOS,
                                                      GuardianPosition)
                                          )
                                    ) &&

                                    SetUnitTeam(
                                          Guardian,
                                          TeamId.TEAM_NEUTRAL) &&
                                    SetVarUnitTeam(
                                          out _PreviousCapturePointOwner,
                                          _CapturePointOwner) &&
                                    SetVarUnitTeam(
                                          out _CapturePointOwner,
                                          TeamId.TEAM_NEUTRAL) &&
                                    MultiplyFloat(
                                          out HalfHealth,
                                          MaxHealth,
                                          PercentToHealTo) &&
                                    SubtractFloat(
                                          out AmountToHeal,
                                          HalfHealth,
                                          CurrentHealth) &&
                                    IncPARbt(
                                          Guardian,
                                          AmountToHeal,
                                          PrimaryAbilityResourceType.MANA)
                              )
                        ) &&
                        minionSpawnTimeSet.MinionSpawnTimeSet(
                              out _MinionSpawnTimeToLane1,
                              _CapturePointOwner,
                              AdjacentCapturePointOwner1,
                              _MinionSpawnTimeToLane1,
                              MinionSpawnRate_Seconds)
                        &&
                        minionSpawnTimeSet.MinionSpawnTimeSet(
                              out _MinionSpawnTimeToLane2,
                              _CapturePointOwner,
                              AdjacentCapturePointOwner2,
                              _MinionSpawnTimeToLane2,
                              MinionSpawnRate_Seconds)
                        &&
                        minionSpawnTimeSet.MinionSpawnTimeSet(
                              out _MinionSpawnTimeFromLane1,
                              AdjacentCapturePointOwner1,
                              _CapturePointOwner,
                              _MinionSpawnTimeFromLane1,
                              MinionSpawnRate_Seconds)
                        &&
                        minionSpawnTimeSet.MinionSpawnTimeSet(
                              out _MinionSpawnTimeFromLane2,
                              AdjacentCapturePointOwner2,
                              _CapturePointOwner,
                              _MinionSpawnTimeFromLane2,
                              MinionSpawnRate_Seconds)

                  )
                  ||
                               DebugAction("MaskFailure")
            ;

        __CapturePointOwner = _CapturePointOwner;
        __MinionSpawnTimeToLane1 = _MinionSpawnTimeToLane1;
        __MinionSpawnTimeToLane2 = _MinionSpawnTimeToLane2;
        __MinionSpawnTimeFromLane1 = _MinionSpawnTimeFromLane1;
        __MinionSpawnTimeFromLane2 = _MinionSpawnTimeFromLane2;
        __ChaosScore = _ChaosScore;
        __OrderScore = _OrderScore;
        __PreviousCapturePointOwner = _PreviousCapturePointOwner;

        return result;
    }
}

