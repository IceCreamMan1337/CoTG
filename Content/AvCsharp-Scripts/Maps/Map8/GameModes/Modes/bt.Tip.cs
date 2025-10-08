using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class TipClass : OdinLayout
{

    float lastTimeExecuted_EP_Tip;
    public bool Tip(
              float ActivationTime,
  float DeactivationTime,
  string TipString,
  string TipTitle,
  string BehaviorTreeToDisable
        )
    {
        var for_EP_Tip = new TipClass();
        List<Func<bool>> EP_Tip = new List<Func<bool>>
{ () =>

        {
            return for_EP_Tip.For_EP_Tip(
                                      ActivationTime,
   DeactivationTime,
   TipString,
   TipTitle,
   BehaviorTreeToDisable);
        } };


        return (
              ExecutePeriodically(
                  ref lastTimeExecuted_EP_Tip,
                  EP_Tip,
                    true,
                    1)
              );

            }


public bool For_EP_Tip(float ActivationTime,
  float DeactivationTime,
  string TipString,
  string TipTitle,
  string BehaviorTreeToDisable
        )

// Sequence name :MaskFailure
{
    return
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
                                                false)
                                    ) ||
                                    // Sequence name :Activate
                                    (
                                          QuestActive == false &&
                                          GreaterFloat(
                                                CurrentTime, 
                                                ActivationTime) &&
                                          SetVarInt(
                                                out TipIndex, 
                                                0) &&
                                          GetChampionCollection(
                                                out AllChamps) &&
                                          ForEach(AllChamps, Champ => (                                     
                                                ActivateTip(
                                                      out TipID, 
                                                      Champ, 
                                                      "TipString", 
                                                      "TipTitle")
                                          ) &&
                                          SetVarBool(
                                                out QuestActive, 
                                                true)
                                    ) ||
                                    // Sequence name :Deactivate
                                    (
                                          QuestActive == true &&
                                          GreaterFloat(
                                                CurrentTime, 
                                                DeactivationTime) &&
                                          RemoveTip(
                                                TipID) &&
                                          SetBTInstanceStatus(
                                                false, 
                                                "BehaviorTreeToDisable")

                                    )
                              )
                        )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

