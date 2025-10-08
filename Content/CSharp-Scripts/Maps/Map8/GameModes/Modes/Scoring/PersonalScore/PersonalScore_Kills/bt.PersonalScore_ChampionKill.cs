using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees;


class PersonalScore_ChampionKillClass : OdinLayout
{


    public bool PersonalScore_ChampionKill(
           out bool __IsFirstBlood,
     AttackableUnit DeadChampion,
     AttackableUnit Killer,
     IEnumerable<AttackableUnit> Assist,
   TeamId CapturePointOwnerA,
   TeamId CapturePointOwnerB,
   TeamId CapturePointOwnerC,
   TeamId CapturePointOwnerD,
   TeamId CapturePointOwnerE,
   TeamId PreviousOwner_A,
   TeamId PreviousOwner_B,
   TeamId PreviousOwner_C,
   TeamId PreviousOwner_D,
   TeamId PreviousOwner_E,
   float Score_LastStand,
   float Score_ChampionKill,
   float Score_ChampionAssist,
   float Score_OffensiveKill,
   float Score_OffensiveAssist,
   float Score_DefensiveAssist,
   float Score_DefensiveKill,
   float Score_Ace,
   float Score_Duelist,
   float Score_Avenger,
   float Score_Guardian,
   float Score_Payback,
   bool IsFirstBlood,
   float Score_FirstBlood,
   int KillingSpreeBeforeDeath,
   float Score_SpreeShutdown0,
   float Score_SpreeShutdown1,
   float Score_SpreeShutdown2,
   bool EnableSecondaryCallout
         )
    {
        bool _IsFirstBlood = IsFirstBlood;
        var findClosestCapturePointByPosition = new FindClosestCapturePointByPositionClass();
        var getGuardian = new GetGuardianClass();
        var personalScore_KillingSpree = new PersonalScore_KillingSpreeClass();
        var personalScore_KillingSpreeShutdown = new PersonalScore_KillingSpreeShutdownClass();
        var personalScore_Ace = new PersonalScore_AceClass();

        bool result =
                          // Sequence name :Sequence

                          GetUnitType(
                                out KillerType,
                                Killer) &&
                          KillerType == HERO_UNIT &&
                          GetUnitTeam(
                                out KillerTeam,
                                Killer) &&
                          GetUnitTeam(
                                out DeadChampionTeam,
                                DeadChampion) &&
                          findClosestCapturePointByPosition.FindClosestCapturePointByPosition(
                                out ClosestCapturePointIndex,
                                DeadChampion) &&
                          getGuardian.GetGuardian(
                                out ClosestGuardian,
                                ClosestCapturePointIndex) &&
                          SetVarInt(
                                out CashedCapturePointIndex,
                                ClosestCapturePointIndex) &&
                          // Sequence name :OnPoint/OffPoint
                          (
                                // Sequence name :Dead Champion Is On Point
                                (
                                      GetDistanceBetweenUnits(
                                            out Distance,
                                            ClosestGuardian,
                                            DeadChampion) &&
                                      LessEqualFloat(
                                            Distance,
                                            900) &&
                                      AddFloat(
                                            out TotalScore,
                                            Score_ChampionKill,
                                            Score_ChampionKill) &&
                                      IncrementPlayerScore(
                                            Killer,
                                          ScoreCategory.Combat,
                                          ScoreEvent.OffensiveKill,
                                            TotalScore
                                            ) &&
                                      SetVarInt(
                                            out NumberOfKillers,
                                            1) &&
                                      AddFloat(
                                            out TotalScore,
                                            Score_ChampionAssist,
                                            Score_ChampionAssist) &&
                                      ForEach(Assist, IndividualAssist =>
                                                  // Sequence name :Sequence

                                                  IncrementPlayerScore(
                                                        IndividualAssist,
                                                       ScoreCategory.Objective,
                                                       ScoreEvent.OffensiveAssist,
                                                        TotalScore
                                                        ) &&
                                                  AddInt(
                                                        out NumberOfKillers,
                                                        NumberOfKillers,
                                                        1)

                                      ) &&
                                      MultiplyFloat(
                                            out LastStandScore,
                                            NumberOfKillers,
                                            Score_LastStand) &&
                                      AddFloat(
                                            out LastStandScore,
                                            LastStandScore,
                                            Score_LastStand) &&
                                      IncrementPlayerScore(
                                            DeadChampion,
                                           ScoreCategory.Combat,
                                          ScoreEvent.LastStand,
                                            LastStandScore
                                            ) &&
                                      IncrementPlayerStat(
                                            DeadChampion,
                                           StatEvent.LastStand)
                                ) ||
                                // Sequence name :Dead Champion Is Not On Point
                                (
                                      // Sequence name :Part 1: Award points to Killer, based on position
                                      (
                                            // Sequence name :Killer Is On Point, award double points
                                            (
                                                  findClosestCapturePointByPosition.FindClosestCapturePointByPosition(
                                                        out ClosestCapturePointIndex,
                                                        Killer) &&
                                                  // Sequence name :MaskFailure
                                                  (
                                                        // Sequence name :Find the closest guardian if we're checking a different point than last check
                                                        (
                                                              NotEqualInt(
                                                                    CashedCapturePointIndex,
                                                                    ClosestCapturePointIndex) &&
                                                              SetVarInt(
                                                                    out CashedCapturePointIndex,
                                                                    ClosestCapturePointIndex) &&
                                                             getGuardian.GetGuardian(
                                                                    out ClosestGuardian,
                                                                    ClosestCapturePointIndex)
                                                        )
                                                        ||
                                       DebugAction("MaskFailure")
                                                  ) &&
                                                  GetDistanceBetweenUnits(
                                                        out Distance,
                                                        ClosestGuardian,
                                                        Killer) &&
                                                  LessEqualFloat(
                                                        Distance,
                                                        900) &&
                                                  AddFloat(
                                                        out TotalScore,
                                                        Score_ChampionKill,
                                                        Score_ChampionKill) &&
                                                  IncrementPlayerScore(
                                                        Killer,
                                                       ScoreCategory.Combat,
                                                      ScoreEvent.OffensiveKill,
                                                        TotalScore
                                                        )
                                            ) ||
                                                  // Sequence name :Killer Is Off Point, award single points

                                                  IncrementPlayerScore(
                                                        Killer,
                                                         ScoreCategory.Combat,
                                                      ScoreEvent.ChampionKill,
                                                        Score_ChampionKill
                                                        )

                                      ) &&
                                            // Sequence name :Part 2: Award points to assisters

                                            AddFloat(
                                                  out TotalScore,
                                                  Score_ChampionAssist,
                                                  Score_ChampionAssist) &&
                                            ForEach(Assist, IndividualAssist =>
                                                        // Sequence name :Get distances between Assister and the point, Assister and the dead champion

                                                        findClosestCapturePointByPosition.FindClosestCapturePointByPosition(
                                                              out ClosestCapturePointIndex,
                                                              IndividualAssist) &&
                                                        // Sequence name :MaskFailure
                                                        (
                                                              // Sequence name :Find the closest guardian if we're checking a different point than last check
                                                              (
                                                                    NotEqualInt(
                                                                          CashedCapturePointIndex,
                                                                          ClosestCapturePointIndex) &&
                                                                    SetVarInt(
                                                                          out CashedCapturePointIndex,
                                                                          ClosestCapturePointIndex) &&
                                                                    getGuardian.GetGuardian(
                                                                          out ClosestGuardian,
                                                                          ClosestCapturePointIndex)
                                                              )
                                                              ||
                                       DebugAction("MaskFailure")
                                                        ) &&
                                                        // Sequence name :Is assister near point and near dead champ?
                                                        (
                                                              // Sequence name :Assister is near point and near dead champion
                                                              (
                                                                    GetDistanceBetweenUnits(
                                                                          out DistanceAssisterGuardian,
                                                                          ClosestGuardian,
                                                                          IndividualAssist) &&
                                                                    LessEqualFloat(
                                                                          DistanceAssisterGuardian,
                                                                          900) &&
                                                                    IncrementPlayerScore(
                                                                          IndividualAssist,
                                                                         ScoreCategory.Objective,
                                                                         ScoreEvent.OffensiveAssist,
                                                                         TotalScore
                                                                          )
                                                              ) ||
                                                              IncrementPlayerScore(
                                                                    IndividualAssist,
                                                                   ScoreCategory.Combat,
                                                                  ScoreEvent.ChampionAssist,
                                                                    Score_ChampionAssist
                                                                    )
                                                        )

                                            )

                                )
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :FirstBlood
                                (
                                      _IsFirstBlood == true &&
                                      IncrementPlayerScore(
                                            Killer,
                                           ScoreCategory.Objective,
                                          ScoreEvent.FirstBlood,
                                          Score_FirstBlood
                                            ) &&
                                      SetVarBool(
                                            out _IsFirstBlood,
                                            false)
                                )
                                ||
                                       DebugAction("MaskFailure")
                          ) &&
                          personalScore_KillingSpree.PersonalScore_KillingSpree(
                                Killer,
                                10,
                                15,
                                20) &&
                          personalScore_KillingSpreeShutdown.PersonalScore_KillingSpreeShutdown(
                                KillingSpreeBeforeDeath,
                                DeadChampion,
                                Killer,
                                Score_SpreeShutdown2,
                                Score_SpreeShutdown1,
                                Score_SpreeShutdown0) &&
                          personalScore_Ace.PersonalScore_Ace(
                                Killer,
                                Score_Ace,
                                DeadChampion)

                    ;
        __IsFirstBlood = _IsFirstBlood;
        return result;
    }
}

