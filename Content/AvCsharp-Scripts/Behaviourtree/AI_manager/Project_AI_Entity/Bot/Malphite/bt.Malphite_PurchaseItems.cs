using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Malphite_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Malphite_PurchaseItems(
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
                                          1031,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1036,
                                          1036,
                                          3134,
                                          1031,
                                          3005,
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
                                          1029,
                                          3047,
                                          1028,
                                          1033,
                                          1029,
                                          3105,
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
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1033,
                                          3105,
                                          1031,
                                          3005,
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
                                          1029,
                                          3047,
                                          1011,
                                          1031,
                                          3068,
                                          1027,
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
                                          1031,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1036,
                                          1036,
                                          3134,
                                          1031,
                                          3005,
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
                                          1029,
                                          3047,
                                          1028,
                                          1033,
                                          1029,
                                          3105,
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
                                          20,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1036,
                                          3044,
                                          1011,
                                          3022,
                                          1031,
                                          3005,
                                           default,
                                    default,
                                   default,
                                      default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1029,
                                          3047,
                                          1011,
                                          1031,
                                          3068,
                                          1027,
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
                                          20,
                                          3024,
                                          1029,
                                          1029,
                                          3110,
                                          1028,
                                          1036,
                                          3044,
                                          1011,
                                          3022,
                                          1031,
                                          3005,
                                              default,
                                    default,
                                   default,
                                      default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          1029,
                                          3047,
                                          1011,
                                          1031,
                                          3068,
                                          1027,
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

