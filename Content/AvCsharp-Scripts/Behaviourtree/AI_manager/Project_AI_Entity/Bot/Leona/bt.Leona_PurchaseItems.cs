using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Leona_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Leona_PurchaseItems(
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
                                          19,
                                          3110,
                                          1033,
                                          3111,
                                          1028,
                                          3067,
                                          1004,
                                          3037,
                                          3099,
                                          1028,
                                          3105,
                                            default,
                                           default,
                                        default,
                                        default,
                                        default,
                                         default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1054,
                                          1005,
                                          1033,
                                          3028,
                                          1001,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
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
                                          22,
                                          1057,
                                          3102,
                                          1031,
                                          1011,
                                          3068,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1033,
                                          3026,
                                          1029,
                                          3105,
                                           default,
                                        default,
                                        default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          1033,
                                          3111,
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
                                          15,
                                          3057,
                                          1031,
                                          1011,
                                          3068,
                                          1031,
                                          3005,
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
                                          1062,
                                          1001,
                                          3111,
                                          1028,
                                          1029,
                                          1033,
                                          3105,
                                          1052,
                                          1027,
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
                                          18,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1033,
                                          3105,
                                          1027,
                                          3057,
                                          3078,
                                         default,
                                        default,
                                    default,
                                   default,
                                      default,
                                   default,
                                 default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1011,
                                          1031,
                                          3068,
                                          1027,
                                          1031,
                                          3024,
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
                                          18,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1033,
                                          3105,
                                          1027,
                                          3057,
                                          3078,
                                               default,
                                         default,
                                    default,
                                   default,
                                      default,
                                   default,
                                 default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1011,
                                          1031,
                                          3068,
                                          1027,
                                          1031,
                                          3024,
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

