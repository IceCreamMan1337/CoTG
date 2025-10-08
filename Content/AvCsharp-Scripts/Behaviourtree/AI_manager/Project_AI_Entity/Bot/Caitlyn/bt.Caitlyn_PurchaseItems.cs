using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Caitlyn_PurchaseItemsClass : AI_Characters 
{

    private PurchaseItemsClass purchaseItems = new PurchaseItemsClass();
  

     public bool Caitlyn_PurchaseItems(
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
                                          3044,
                                          1011,
                                          3022,
                                          1042,
                                          3006,
                                          1053,
                                          1038,
                                          3072,
                                          1037,
                                          3035,
                                              default,
                                      default,
                                        default,
                                      default,
                                    default,
                                  default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1055,
                                          1005,
                                          1027,
                                          3070,
                                          1036,
                                          3004,
                                          1001,
                                          1028,
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
                                          20,
                                          3044,
                                          1011,
                                          3022,
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
                                          1028,
                                          3010,
                                          3102,
                                            default,
                                          default,
                                      default,
                                    default,
                                  default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1055,
                                          1001,
                                          1038,
                                          1042,
                                          3006,
                                          1053,
                                          3072,
                                          1028,
                                          1036,
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
                                          1053,
                                          1028,
                                          3067,
                                          3050,
                                          1018,
                                          3046,
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
                                          3006,
                                          1028,
                                          1043,
                                          3178,
                                          1036,
                                          1036,
                                          3134,
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
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3006,
                                          1038,
                                          1053,
                                          3181,
                                          1038,
                                          1042,
                                          3071,
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
                                          16,
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3006,
                                          1038,
                                          1053,
                                          3181,
                                          1038,
                                          1042,
                                          3071,
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

