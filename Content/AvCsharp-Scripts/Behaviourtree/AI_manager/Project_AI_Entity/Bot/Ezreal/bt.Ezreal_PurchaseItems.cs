using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Ezreal_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Ezreal_PurchaseItems(
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
                                          1029,
                                          1036,
                                          3106,
                                          1053,
                                          3154,
                                          1038,
                                          1042,
                                          3071,
                                          1038,
                                          3072,
                                                 default,
                                         default,
                                              default,
                                         default,
                                        default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1001,
                                          1042,
                                          3006,
                                          1055,
                                          1027,
                                          1005,
                                          3070,
                                          1036,
                                          3004,
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
                                          1038,
                                          1053,
                                          3072,
                                          1038,
                                          1053,
                                          3072,
                                          1057,
                                          3102,
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
                                          1055,
                                          1001,
                                          1042,
                                          3006,
                                          1029,
                                          1036,
                                          3106,
                                          1053,
                                          3154,
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
                                          18,
                                          1036,
                                          1036,
                                          3134,
                                          1028,
                                          3067,
                                          1053,
                                          3050,
                                          1037,
                                          3035,
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
                                          1042,
                                          3006,
                                          1027,
                                          1005,
                                          3070,
                                          1036,
                                          3004,
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
                                          3086,
                                          1028,
                                          1036,
                                          3044,
                                          3078,
                                          1038,
                                          1053,
                                          3181,
                                          1038,
                                          1037,
                                          3031,
                                          1037,
                                          3035,
                                        default,
                                   default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1042,
                                          3006,
                                          1027,
                                          1052,
                                          3057,
                                          1051,
                                          1042,
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
                                          3086,
                                          1028,
                                          1036,
                                          3044,
                                          3078,
                                          1038,
                                          1053,
                                          3181,
                                          1038,
                                          1037,
                                          3031,
                                          1037,
                                          3035,
                                          default,
                                   default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1042,
                                          3006,
                                          1027,
                                          1052,
                                          3057,
                                          1051,
                                          1042,
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

