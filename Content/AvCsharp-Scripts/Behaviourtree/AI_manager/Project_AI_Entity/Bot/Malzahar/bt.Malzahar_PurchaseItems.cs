using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Malzahar_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Malzahar_PurchaseItems(
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
                                          1033,
                                          3028,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          3158,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
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
                                          16,
                                          1026,
                                          3089,
                                          1011,
                                          1026,
                                          3116,
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
                                          1056,
                                          1001,
                                          3020,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1058,
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
                                          1033,
                                          3028,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3158,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
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
                                          15,
                                          3128,
                                          1011,
                                          1026,
                                          3116,
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
                                       default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3020,
                                          1058,
                                          1026,
                                          3089,
                                          1052,
                                          3098,
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
                                          15,
                                          3128,
                                          1011,
                                          1026,
                                          3116,
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
                                       default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3020,
                                          1058,
                                          1026,
                                          3089,
                                          1052,
                                          3098,
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

