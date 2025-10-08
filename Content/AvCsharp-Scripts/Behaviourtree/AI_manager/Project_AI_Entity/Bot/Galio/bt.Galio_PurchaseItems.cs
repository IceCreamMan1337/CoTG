using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Galio_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Galio_PurchaseItems(
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
                                          17,
                                          3028,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1057,
                                          3001,
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
                                          3009,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
                                          1005,
                                          1033,
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
                                          17,
                                          1057,
                                          3102,
                                          1057,
                                          1026,
                                          3001,
                                          1011,
                                          3068,
                                          3174,
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
                                          3111,
                                          1005,
                                          1033,
                                          3028,
                                          1028,
                                          1027,
                                          3010,
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
                                          17,
                                          3028,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1057,
                                          3001,
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
                                          3009,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
                                          1005,
                                          1033,
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
                                          17,
                                          1026,
                                          3001,
                                          1005,
                                          1033,
                                          3028,
                                          1052,
                                          3145,
                                          3152,
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
                                          3111,
                                          1028,
                                          1027,
                                          3010,
                                          1057,
                                          3180,
                                          1057,
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
                                          17,
                                          1026,
                                          3001,
                                          1005,
                                          1033,
                                          3028,
                                          1052,
                                          3145,
                                          3152,
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
                                          3111,
                                          1028,
                                          1027,
                                          3010,
                                          1057,
                                          3180,
                                          1057,
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

