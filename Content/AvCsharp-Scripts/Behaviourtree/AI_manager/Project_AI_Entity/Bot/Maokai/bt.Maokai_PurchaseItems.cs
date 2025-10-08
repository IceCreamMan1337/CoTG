using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Maokai_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Maokai_PurchaseItems(
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
                                          16,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
                                          3110,
                                          1052,
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
                                          1028,
                                          1052,
                                          3136,
                                          1001,
                                          1026,
                                          1058,
                                          3089,
                                          3020,
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
                                          21,
                                          1031,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1027,
                                          3010,
                                          1057,
                                          3102,
                                          1011,
                                          3068,
                                         default,
                                        default,
                                         default,
                                       default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1054,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          1029,
                                          1033,
                                          3105,
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
                                          3010,
                                          1026,
                                          3027,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
                                          3110,
                                          1052,
                                          3057,
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
                                          1004,
                                          1007,
                                          3096,
                                          3173,
                                          1028,
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
                                          22,
                                          1027,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1052,
                                          1027,
                                          3057,
                                          1028,
                                          3044,
                                          3078,
                                          1057,
                                          3109,
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
                                          1026,
                                          3027,
                                          1031,
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
                                          22,
                                          1027,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1052,
                                          1027,
                                          3057,
                                          1028,
                                          3044,
                                          3078,
                                          1057,
                                          3109,
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
                                          1026,
                                          3027,
                                          1031,
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

