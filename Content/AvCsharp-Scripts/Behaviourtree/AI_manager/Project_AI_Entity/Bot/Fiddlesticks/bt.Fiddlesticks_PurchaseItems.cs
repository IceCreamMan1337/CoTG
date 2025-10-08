using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Fiddlesticks_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Fiddlesticks_PurchaseItems(
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
                                          19,
                                          3116,
                                          1058,
                                          1026,
                                          3089,
                                          1052,
                                          3108,
                                          3174,
                                          1028,
                                          3010,
                                          3102,
                                       default,
                                         default,
                                        default,
                                      default,
                                      default,
                                       default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          1005,
                                          1033,
                                          3028,
                                          3020,
                                          1026,
                                          1052,
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
                                          1052,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1026,
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
                                          1063,
                                          1001,
                                          3158,
                                          1028,
                                          1052,
                                          3136,
                                          1026,
                                          3170,
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
                                          17,
                                          1005,
                                          1052,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1026,
                                          3135,
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
                                          1058,
                                          1026,
                                          3089,
                                          1058,
                                          1031,
                                          3157,
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
                                          17,
                                          1005,
                                          1052,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1026,
                                          3135,
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
                                          1058,
                                          1026,
                                          3089,
                                          1058,
                                          1031,
                                          3157,
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

