namespace BehaviourTrees.all;


class Graves_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool Graves_PurchaseItems(
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
                                          1053,
                                          1028,
                                          3067,
                                          1042,
                                          3050,
                                          1037,
                                          1036,
                                          3035,
                                          1043,
                                          3126,
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
                                          1042,
                                          3006,
                                          1005,
                                          1027,
                                          3070,
                                          1036,
                                          3004,
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
                                          3086,
                                          1018,
                                          1042,
                                          3046,
                                          1038,
                                          1053,
                                          3072,
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
                                          1055,
                                          1001,
                                          1042,
                                          3006,
                                          1038,
                                          1042,
                                          3071,
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
                                          18,
                                          1037,
                                          1043,
                                          3186,
                                          1053,
                                          1028,
                                          3067,
                                          3050,
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
                                          1042,
                                          3006,
                                          1005,
                                          1027,
                                          3070,
                                          1036,
                                          3004,
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
                                          3086,
                                          1018,
                                          1042,
                                          3046,
                                          1038,
                                          1053,
                                          3181,
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
                                          1042,
                                          3006,
                                          1038,
                                          1042,
                                          3071,
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
                                          18,
                                          3086,
                                          1018,
                                          1042,
                                          3046,
                                          1038,
                                          1053,
                                          3181,
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
                                          1042,
                                          3006,
                                          1038,
                                          1042,
                                          3071,
                                          1051,
                                          1042,
                                          FinishedItemBuild)

                              )
                        )
                  )
            ;

        __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        return result;

    }
}

