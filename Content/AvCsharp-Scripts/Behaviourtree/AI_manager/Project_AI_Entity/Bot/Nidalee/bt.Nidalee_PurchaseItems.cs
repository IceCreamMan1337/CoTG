using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nidalee_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Nidalee_PurchaseItems(
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
                                          3145,
                                          1026,
                                          1052,
                                          3135,
                                          1031,
                                          3026,
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
                                          1056,
                                          1001,
                                          1033,
                                          3111,
                                          1026,
                                          1037,
                                          3124,
                                          1052,
                                          1052,
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
                                          3057,
                                          1026,
                                          1033,
                                          3100,
                                          1026,
                                          1052,
                                          3135,
                                          1031,
                                          3026,
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
                                          1033,
                                          3111,
                                          1058,
                                          1026,
                                          3089,
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
                                          18,
                                          3145,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          1033,
                                          3111,
                                          1026,
                                          1037,
                                          3124,
                                          1052,
                                          1052,
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
                                          3057,
                                          1033,
                                          1026,
                                          3100,
                                          1026,
                                          1052,
                                          3135,
                                          1031,
                                          3157,
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
                                          1033,
                                          3111,
                                          1058,
                                          1026,
                                          3089,
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
                                          18,
                                          3057,
                                          1033,
                                          1026,
                                          3100,
                                          1026,
                                          1052,
                                          3135,
                                          1031,
                                          3157,
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
                                          1033,
                                          3111,
                                          1058,
                                          1026,
                                          3089,
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

