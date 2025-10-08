using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class PersonalScore_ManagerClass : OdinLayout 
{

    float lastTimeExecuted_EP_PersonalScore_CapturePointTicking;
    public bool PersonalScore_Manager(
                TeamId CapturePointOwnerA,
    TeamId CapturePointOwnerB,
    TeamId CapturePointOwnerC,
    TeamId CapturePointOwnerD,
    TeamId CapturePointOwnerE,
    float Score_SmallRelic,
    float Score_BigRelic,
    float Score_Interrupt,
    float Score_Survivor,
    float Score_Angel,
    float Score_ArchAngel,
    bool EnableSecondaryCallout
          )
      {

      
        var personalScore_PointDefenseTicking = new PersonalScore_PointDefenseTickingClass();

        var for_EP_PersonalScore_CapturePointTicking = new PersonalScore_CapturePointTickingClass();


        List<Func<bool>> EP_PersonalScore_CapturePointTicking = new List<Func<bool>> {
            () => {
                return
                for_EP_PersonalScore_CapturePointTicking.For_EP_PersonalScore_CapturePointTicking(
                CapturePointOwnerA,
                CapturePointOwnerB,
                CapturePointOwnerC,
                CapturePointOwnerD,
                CapturePointOwnerE);
            }
        };

        return
            // Sequence name :Sequence
            (
                  GetGameTime(
                        out CurrentGameTime) &&
                  GreaterEqualFloat(
                        CurrentGameTime, 
                        80) &&
                  GetChampionCollection(
                        out AllChampions) &&
                 ForEach(AllChampions, Champ => (                     
                 // Sequence name :Sequence
                        (
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :ChannelInterrupt
                                    (
                                          GetUnitBuffCount(
                                                out Count, 
                                                Champ,
                                                "OdinCaptureInterrupt") &&
                                          GreaterInt(
                                                Count, 
                                                0) &&
                                          GetUnitBuffCaster(
                                                out ChampWhoInterrupted, 
                                                Champ,
                                                "OdinCaptureInterrupt") &&
                                          IncrementPlayerScore(
                                                ChampWhoInterrupted, 
                                                ScoreCategory.Objective, 
                                               ScoreEvent.Counter,
                                                Score_Interrupt
                                                ) &&
                                          SpellBuffClear(
                                                Champ,
                                                "OdinCaptureInterrupt")
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :SmallRelic
                                          (
                                                TestUnitHasBuff(
                                                      Champ, 
                                                      null,
                                                      "OdinScoreSmallRelic", 
                                                      true) &&
                                                IncrementPlayerScore(
                                                      Champ,
                                                      ScoreCategory.Objective,
                                                     ScoreEvent.ScavengerHunt,
                                                      Score_SmallRelic
                                                      ) &&
                                                SpellBuffClear(
                                                      Champ,
                                                      "OdinScoreSmallRelic")
                                          ) ||
                                          // Sequence name :BigRelic
                                          (
                                                TestUnitHasBuff(
                                                      Champ, 
                                                      null,
                                                      "OdinScoreBigRelic", 
                                                      true) &&
                                                IncrementPlayerScore(
                                                      Champ,
                                                      ScoreCategory.Objective,
                                                     ScoreEvent.MajorRelicPickup,
                                                      Score_BigRelic
                                                      ) &&
                                                SpellBuffClear(
                                                      Champ,
                                                      "OdinScoreBigRelic")
                                          )
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :Sequence
                                    (
                                          EnableSecondaryCallout == true &&
                                          TestUnitHasBuff(
                                                Champ, 
                                                null,
                                                "OdinScoreSurvivor", 
                                                true) &&
                                          IncrementPlayerScore(
                                                Champ,
                                               ScoreCategory.Combat, 
                                               ScoreEvent.Survivor,
                                                Score_Survivor
                                                ) &&
                                          SpellBuffClear(
                                                Champ,
                                                "OdinScoreSurvivor")
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :ArchAngel
                                    (
                                          EnableSecondaryCallout == true &&
                                          TestUnitHasBuff(
                                                Champ, 
                                                null,
                                                "OdinScoreArchAngel", 
                                                true) &&
                                          IncrementPlayerScore(
                                                Champ, 
                                                ScoreCategory.Combat, 
                                                ScoreEvent.ArchAngel ,
                                                Score_ArchAngel
                                                ) &&
                                          SpellBuffClear(
                                                Champ,
                                                "OdinScoreArchAngel")
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :Angel
                                    (
                                          TestUnitHasBuff(
                                                Champ, 
                                                null,
                                                "OdinScoreAngel", 
                                                true) &&
                                          GreaterFloat(
                                                Score_Angel, 
                                                0) &&
                                          IncrementPlayerScore(
                                                Champ,
                                                ScoreCategory.Combat,
                                               ScoreEvent.Angel,
                                               Score_Angel
                                                ) &&
                                          SpellBuffClear(
                                                Champ,
                                                "OdinScoreAngel")
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              )
                        ))
                  ) &&
                  ExecutePeriodically(
                       ref lastTimeExecuted_EP_PersonalScore_CapturePointTicking,
                        EP_PersonalScore_CapturePointTicking,
                                true,
                                10) &&
                 personalScore_PointDefenseTicking.PersonalScore_PointDefenseTicking()

            );





      }
}


