using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nasus_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Nasus_PurchaseItems(
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
                                          3068,
                                          1057,
                                          1007,
                                          3109,
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
                                          1054,
                                          1001,
                                          1033,
                                          3111,
                                          1027,
                                          1052,
                                          3057,
                                          1011,
                                          1031,
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
                                          1011,
                                          1031,
                                          3068,
                                          1057,
                                          1007,
                                          3109,
                                          1031,
                                          3005,
                                          default,
                                         default,
                                        default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1054,
                                          1001,
                                          1033,
                                          3111,
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
                                          3068,
                                          1057,
                                          1007,
                                          3109,
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
                                          1033,
                                          3111,
                                          1027,
                                          1052,
                                          3057,
                                          1011,
                                          1031,
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
                                          1011,
                                          1031,
                                          3068,
                                          1057,
                                          1007,
                                          3109,
                                          1031,
                                          3005,
                                           default,
                                    default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1033,
                                          3111,
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
                                          1011,
                                          1031,
                                          3068,
                                          1057,
                                          1007,
                                          3109,
                                          1031,
                                          3005,
                                         default,
                                    default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1033,
                                          3111,
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

