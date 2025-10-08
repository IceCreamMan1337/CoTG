using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Renekton_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Renekton_PurchaseItems(
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
                                          13,
                                          1029,
                                          3105,
                                          1042,
                                          3071,
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
                                          1055,
                                          1028,
                                          3132,
                                          1001,
                                          1036,
                                          1036,
                                          3134,
                                          3158,
                                          1028,
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
                                          3026,
                                          1038,
                                          1042,
                                          3071,
                                          1011,
                                          3068,
                                          3142,
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
                                          1055,
                                          1001,
                                          1036,
                                          1036,
                                          3134,
                                          3158,
                                          1031,
                                          1029,
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
                                          15,
                                          3134,
                                          1031,
                                          1011,
                                          3068,
                                          1037,
                                          3035,
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
                                          1038,
                                          1042,
                                          3071,
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
                                          18,
                                          1011,
                                          3068,
                                          1036,
                                          1028,
                                          3044,
                                          1011,
                                          3022,
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
                                          1033,
                                          3111,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
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
                                          18,
                                          1011,
                                          3068,
                                          1036,
                                          1028,
                                          3044,
                                          1011,
                                          3022,
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
                                          1033,
                                          3111,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
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

