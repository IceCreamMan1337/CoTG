using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Blitzcrank_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Blitzcrank_PurchaseItems(
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
                                          14,
                                          1007,
                                          1028,
                                          3083,
                                          1029,
                                          3105,
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
                                          1054,
                                          1005,
                                          1033,
                                          3028,
                                          1001,
                                          1031,
                                          1027,
                                          3024,
                                          3158,
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
                                          23,
                                          1029,
                                          3110,
                                          1057,
                                          1027,
                                          1028,
                                          3010,
                                          3102,
                                          1028,
                                          3044,
                                          1052,
                                          3057,
                                          3078,
                                          1026,
                                          3116,
                                          default,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1027,
                                          1001,
                                          1031,
                                          3024,
                                          3158,
                                          1028,
                                          1033,
                                          1029,
                                          3105,
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
                                          3028,
                                          1027,
                                          1031,
                                          3024,
                                          1011,
                                          3068,
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
                                          1063,
                                          1001,
                                          3158,
                                          1029,
                                          1033,
                                          1028,
                                          3105,
                                          1033,
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
                                          24,
                                          1052,
                                          3057,
                                          1028,
                                          1036,
                                          3044,
                                          1051,
                                          3086,
                                          3078,
                                          1029,
                                          3110,
                                          1026,
                                          1011,
                                          3116,
                                          1058,
                                          3089,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          3010,
                                          1001,
                                          3158,
                                          1057,
                                          3180,
                                          1027,
                                          1031,
                                          3024,
                                          1027,
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
                                          24,
                                          1052,
                                          3057,
                                          1028,
                                          1036,
                                          3044,
                                          1051,
                                          3086,
                                          3078,
                                          1029,
                                          3110,
                                          1026,
                                          1011,
                                          3116,
                                          1058,
                                          3089,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          3010,
                                          1001,
                                          3158,
                                          1057,
                                          3180,
                                          1027,
                                          1031,
                                          3024,
                                          1027,
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

