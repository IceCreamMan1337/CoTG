namespace BehaviourTrees.all;


class MissFortune_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool MissFortune_PurchaseItems(
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
                                          20,
                                          3086,
                                          1042,
                                          1018,
                                          3046,
                                          1042,
                                          3006,
                                          1053,
                                          1038,
                                          3072,
                                          1036,
                                          3106,
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
                                          1042,
                                          1051,
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
                                          1018,
                                          3031,
                                          1037,
                                          1036,
                                          3035,
                                          1036,
                                          3004,
                                          1042,
                                          3086,
                                          3046,
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
                                          1027,
                                          1005,
                                          3070,
                                          1042,
                                          3006,
                                          1038,
                                          1037,
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
                                          1043,
                                          1028,
                                          3178,
                                          1005,
                                          1027,
                                          3070,
                                          1036,
                                          3004,
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
                                          1062,
                                          1001,
                                          3117,
                                          1051,
                                          1042,
                                          3086,
                                          1018,
                                          1042,
                                          3046,
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
                                          1038,
                                          1018,
                                          1037,
                                          3031,
                                          1038,
                                          1042,
                                          3071,
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
                                          3117,
                                          1051,
                                          1042,
                                          3086,
                                          1018,
                                          1042,
                                          3046,
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
                                          1038,
                                          1018,
                                          1037,
                                          3031,
                                          1038,
                                          1042,
                                          3071,
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
                                          3117,
                                          1051,
                                          1042,
                                          3086,
                                          1018,
                                          1042,
                                          3046,
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

