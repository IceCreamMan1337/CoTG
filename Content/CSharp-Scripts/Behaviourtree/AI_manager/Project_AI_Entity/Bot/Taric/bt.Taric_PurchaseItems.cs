namespace BehaviourTrees.all;


class Taric_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool Taric_PurchaseItems(
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
                                          16,
                                          1028,
                                          3067,
                                          1004,
                                          3037,
                                          3099,
                                          1028,
                                          3105,
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
                                          1042,
                                          1052,
                                          3114,
                                          1001,
                                          1005,
                                          1033,
                                          3028,
                                          3158,
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
                                          22,
                                          3105,
                                          1042,
                                          1042,
                                          3101,
                                          3115,
                                          1027,
                                          1028,
                                          3010,
                                          1057,
                                          3102,
                                          1028,
                                          3067,
                                          3099,
                                          default,
                                         default,
                                        default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          1052,
                                          1005,
                                          3108,
                                          3158,
                                          1028,
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
                                          22,
                                          3099,
                                          1031,
                                          1027,
                                          3024,
                                          1005,
                                          1052,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1052,
                                          3145,
                                          3152,
                                              default,
                                    default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3117,
                                          1004,
                                          1004,
                                          3037,
                                          1028,
                                          3067,
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
                                          21,
                                          1027,
                                          3057,
                                          1026,
                                          1033,
                                          3100,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1031,
                                          3024,
                                               default,
                                         default,
                                    default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3117,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1052,
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
                                          21,
                                          1027,
                                          3057,
                                          1026,
                                          1033,
                                          3100,
                                          1052,
                                          1052,
                                          3145,
                                          1026,
                                          3152,
                                          1031,
                                          3024,
                                               default,
                                         default,
                                    default,
                                   default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1063,
                                          1001,
                                          3117,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1052,
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

