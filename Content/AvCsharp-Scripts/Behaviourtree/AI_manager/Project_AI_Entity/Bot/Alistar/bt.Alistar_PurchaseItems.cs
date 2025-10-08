using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
     public bool Alistar_PurchaseItems(
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
                                          21,
                                          3027,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1027,
                                          3024,
                                          1057,
                                          1026,
                                          3001,
                                          1052,
                                          3057,
                                          default,
                                          default,
                                          default,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          3158,
                                          1027,
                                          1028,
                                          3010,
                                          1026,
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
                                          3027,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1027,
                                          3024,
                                          1057,
                                          1029,
                                          3110,
                                          1026,
                                          3001,
                                          1052,
                                          3057,
                                          3100,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          3158,
                                          1027,
                                          1028,
                                          3010,
                                          1026,
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
                                          21,
                                          3027,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1027,
                                          3024,
                                          1057,
                                          1026,
                                          3001,
                                          1052,
                                          3057,
                                          default,
                                          default,
                                          default,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          3158,
                                          1027,
                                          1028,
                                          3010,
                                          1026,
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
                                          3027,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1027,
                                          3024,
                                          1057,
                                          1029,
                                          3110,
                                          1026,
                                          3001,
                                          1052,
                                          3057,
                                          3100,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          3158,
                                          1027,
                                          1028,
                                          3010,
                                          1026,
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
                                          3027,
                                          1028,
                                          3067,
                                          3069,
                                          1031,
                                          1027,
                                          3024,
                                          1057,
                                          1029,
                                          3110,
                                          1026,
                                          3001,
                                          1052,
                                          3057,
                                          3100,
                                          default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1007,
                                          1001,
                                          1004,
                                          3096,
                                          3158,
                                          1027,
                                          1028,
                                          3010,
                                          1026,
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

