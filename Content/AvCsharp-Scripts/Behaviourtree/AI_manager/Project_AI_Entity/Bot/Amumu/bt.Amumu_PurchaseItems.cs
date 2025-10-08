using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Amumu_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Amumu_PurchaseItems(
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
                                          17,
                                          3110,
                                          1033,
                                          3111,
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
                                          18,
                                          3068,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
                                          3110,
                                          1026,
                                          3001,
                                          3174,
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
                                          1005,
                                          1033,
                                          3028,
                                          1033,
                                          3111,
                                          1011,
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
                                          3020,
                                          1028,
                                          1033,
                                          1029,
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
                                          18,
                                          1031,
                                          3068,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
                                          3110,
                                          1033,
                                          3105,
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
                                          3020,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1011,
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
                                          1031,
                                          3068,
                                          1031,
                                          1027,
                                          3024,
                                          1029,
                                          3110,
                                          1033,
                                          3105,
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
                                          3020,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1011,
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

