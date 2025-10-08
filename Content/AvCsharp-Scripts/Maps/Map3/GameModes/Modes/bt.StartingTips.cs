using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map3;


class StartingTipsClass : BehaviourTree 
{
    /*
      bool StartingTips()
    {

    return 
      (
            ExecutePeriodically(
                  true, 
                  1)
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              GetGameTime(
                                    out CurrentTime) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Initialization
                                    (
                                          __IsFirstRun == true &&
                                          SetVarBool(
                                                out QuestActive, 
                                                False) &&
                                          SetVarBool(
                                                out QuestActive2, 
                                                False)
                                    ) ||
                                    // Sequence name :Activate
                                    (
                                          QuestActive == False &&
                                          GreaterEqualFloat(
                                                CurrentTime, 
                                                5) &&
                                          SetVarInt(
                                                out TipIndex, 
                                                0) &&
                                          GetChampionCollection(
                                                out AllChamps, 
                                                out AllChamps) &&
                                          AllChamps.ForEach( Champ => (                                                // Sequence name :Sequence
                                                (
                                                      ActivateTip(
                                                            out TipId2, 
                                                            Champ, 
                                                            game_aram_tip_text_buying, 
                                                            game_aram_tip_title_buying)
                                                )
                                          ) &&
                                          SetVarBool(
                                                out QuestActive, 
                                                true) &&
                                          AddFloat(
                                                out DeactivationTime, 
                                                CurrentTime, 
                                                45)
                                    ) ||
                                    // Sequence name :Activate
                                    (
                                          QuestActive2 == False &&
                                          GreaterEqualFloat(
                                                CurrentTime, 
                                                10) &&
                                          SetVarInt(
                                                out TipIndex, 
                                                0) &&
                                          GetChampionCollection(
                                                out AllChamps, 
                                                out AllChamps) &&
                                          AllChamps.ForEach( Champ => (                                                // Sequence name :Sequence
                                                (
                                                      ActivateTip(
                                                            out TipID, 
                                                            Champ, 
                                                            game_aram_tip_text_noheal, 
                                                            game_aram_tip_title_noheal)
                                                )
                                          ) &&
                                          SetVarBool(
                                                out QuestActive2, 
                                                true)
                                    ) ||
                                    // Sequence name :Deactivate
                                    (
                                          QuestActive == true &&
                                          GreaterEqualFloat(
                                                CurrentTime, 
                                                DeactivationTime) &&
                                          RemoveTip(
                                                TipID) &&
                                          RemoveTip(
                                                TipId2) &&
                                          SetBTInstanceStatus(
                                                False, 
                                                StartingTips)

                                    )
                              )
                        )
                        ||
                               DebugAction("MaskFailure")
                  )
            ); 
      }
    */
}

