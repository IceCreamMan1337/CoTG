using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Irelia_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Irelia_PurchaseItems(
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
                                          1033,
                                          3065,
                                          1036,
                                          1036,
                                          3134,
                                          1057,
                                          3109,
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
                                          1043,
                                          1033,
                                          3091,
                                          1028,
                                          3067,
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
                                          3065,
                                          1028,
                                          1036,
                                          3044,
                                          1027,
                                          3057,
                                          3078,
                                          1057,
                                          3109,
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
                                          3111,
                                          1043,
                                          1033,
                                          3091,
                                          1028,
                                          3067,
                                          1033,
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
                                          16,
                                          3134,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
                                          1057,
                                          3109,
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
                                          1043,
                                          1033,
                                          3091,
                                          1036,
                                          1036,
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
                                          1027,
                                          1052,
                                          3057,
                                          1051,
                                          3086,
                                          3078,
                                          1057,
                                          1007,
                                          3109,
                                          1028,
                                          3067,
                                          3065,
                                             default,
                                    default,
                                   default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1043,
                                          1033,
                                          3091,
                                          1028,
                                          1036,
                                          3044,
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
                                          21,
                                          1027,
                                          1052,
                                          3057,
                                          1051,
                                          3086,
                                          3078,
                                          1057,
                                          1007,
                                          3109,
                                          1028,
                                          3067,
                                          3065,
                                           default,
                                    default,
                                   default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1043,
                                          1033,
                                          3091,
                                          1028,
                                          1036,
                                          3044,
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

