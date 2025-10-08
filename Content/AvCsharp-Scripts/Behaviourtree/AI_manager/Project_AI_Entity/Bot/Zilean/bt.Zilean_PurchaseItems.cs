using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Zilean_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Zilean_PurchaseItems(
         out int __ItemPurchaseIndex,
      out bool __FinishedItemBuild,
      AttackableUnit Self,
      bool IsDominionGameMode,
      int ItemPurchaseIndex,
      bool FinishedItemBuild
         )
    {
        int _ItemPurchaseIndex = ItemPurchaseIndex;
        bool _FinishedItemBuild = FinishedItemBuild;

        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :Classic
                  (
                        IsDominionGameMode == false &&
                        // Sequence name :Selector
                        (
                              // Sequence name :BeginnerItems
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          21,
                                          1027,
                                          3070,
                                          1026,
                                          3003,
                                          1005,
                                          1052,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1057,
                                          3001,
                                           default,
                                         default,
                                        default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          3020,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1005,
                                          FinishedItemBuild)
                              ) ||
                              // Sequence name :IntermediateItems
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          16,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1026,
                                          3135,
                                        default,
                                         default,
                                        default,
                                      default,
                                       default,
                                        default,
                                         default,
                                        default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          3020,
                                          1058,
                                          1026,
                                          3089,
                                          1026,
                                          1057,
                                          3001,
                                          FinishedItemBuild)
                              ) ||
                              // Sequence name :Advanced
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_ADVANCED) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          0,
                                            default,
                                       default,
                                    default,
                                    default,
                                   default,
                                     default,
                                      default,
                                    default,
                                     default,
                                     default,
                                     default,
                                      default,
                                      default,
                                    default,
                                      default,
                                     default,
                                          ItemPurchaseIndex,
                                          Self,
                                            default,
                                    default,
                                   default,
                                      default,
                                   default,
                                 default,
                                   default,
                                   default,
                                   default,
                                          FinishedItemBuild)
                              )
                        )
                  ) ||
                  // Sequence name :DominionPurchase
                  (
                        IsDominionGameMode == true &&
                        // Sequence name :Selector
                        (
                              // Sequence name :BeginnerItems
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          19,
                                          1027,
                                          3070,
                                          1026,
                                          3003,
                                          1028,
                                          3067,
                                          1028,
                                          3099,
                                          1026,
                                          3001,
                                       default,
                                    default,
                                    default,
                                   default,
                                      default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3020,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1005,
                                          FinishedItemBuild)
                              ) ||
                              // Sequence name :IntermediateItems
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          16,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1026,
                                          3135,
                                         default,
                                    default,
                                   default,
                                      default,
                                    default,
                                    default,
                                   default,
                                      default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3020,
                                          1058,
                                          1026,
                                          3089,
                                          1026,
                                          1057,
                                          3001,
                                          FinishedItemBuild)
                              ) ||
                              // Sequence name :Advanced
                              (
                                    TestEntityDifficultyLevel(
                                          true,
                                          EntityDiffcultyType.DIFFICULTY_ADVANCED) &&
                                    purchaseItems.PurchaseItems(
                                          out _ItemPurchaseIndex,
                                          out _FinishedItemBuild,
                                          16,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1026,
                                          3135,
                                          default,
                                    default,
                                   default,
                                      default,
                                    default,
                                    default,
                                   default,
                                      default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3020,
                                          1058,
                                          1026,
                                          3089,
                                          1026,
                                          1057,
                                          3001,
                                          FinishedItemBuild)

                              )
                        )
                  )
            );

        __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        return result;


    }
}

