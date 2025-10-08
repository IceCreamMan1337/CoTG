using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Shyvana_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Shyvana_PurchaseItems(
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
                                          15,
                                          3172,
                                          1037,
                                          1026,
                                          3124,
                                          1043,
                                          3178,
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
                                          1001,
                                          1042,
                                          3006,
                                          1028,
                                          1036,
                                          3044,
                                          1018,
                                          1042,
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
                                          3057,
                                          1051,
                                          1042,
                                          3086,
                                          3078,
                                          1043,
                                          1028,
                                          3178,
                                          1038,
                                          1053,
                                          3072,
                                          1051,
                                          3086,
                                          3046,
                                             default,
                                         default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1054,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          1036,
                                          3044,
                                          1052,
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
                                          15,
                                          3172,
                                          1037,
                                          1026,
                                          3124,
                                          1043,
                                          3178,
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
                                          1042,
                                          3006,
                                          1028,
                                          1036,
                                          3044,
                                          1018,
                                          1042,
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
                                          3057,
                                          1051,
                                          1042,
                                          3086,
                                          3078,
                                          1043,
                                          1028,
                                          3178,
                                          1038,
                                          1053,
                                          3181,
                                          1051,
                                          3086,
                                          3046,
                                           default,
                                    default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          1036,
                                          3044,
                                          1052,
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
                                          23,
                                          3057,
                                          1051,
                                          1042,
                                          3086,
                                          3078,
                                          1043,
                                          1028,
                                          3178,
                                          1038,
                                          1053,
                                          3181,
                                          1051,
                                          3086,
                                          3046,
                                            default,
                                    default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          1036,
                                          3044,
                                          1052,
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

