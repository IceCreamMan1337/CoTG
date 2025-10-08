namespace BehaviourTrees.all;


class Sion_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool Sion_PurchaseItems(
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
                                          1005,
                                          1052,
                                          3108,
                                          1028,
                                          3067,
                                          3050,
                                          3003,
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
                                          1027,
                                          1052,
                                          3057,
                                          3020,
                                          1027,
                                          1005,
                                          3070,
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
                                          1052,
                                          1005,
                                          3108,
                                          1052,
                                          3098,
                                          3165,
                                          1058,
                                          1026,
                                          3089,
                                          1028,
                                          3010,
                                          3102,
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
                                          1027,
                                          1028,
                                          3010,
                                          1026,
                                          3027,
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
                                          1005,
                                          1052,
                                          3108,
                                          1028,
                                          3067,
                                          3050,
                                          3003,
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
                                          1027,
                                          1052,
                                          3057,
                                          3020,
                                          1027,
                                          1005,
                                          3070,
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
                                          3044,
                                          1058,
                                          1026,
                                          3089,
                                          1051,
                                          3086,
                                          3078,
                                          1011,
                                          1031,
                                          3068,
                                          1026,
                                          3001,
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
                                          1027,
                                          1052,
                                          3057,
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
                                          21,
                                          3044,
                                          1058,
                                          1026,
                                          3089,
                                          1051,
                                          3086,
                                          3078,
                                          1011,
                                          1031,
                                          3068,
                                          1026,
                                          3001,
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
                                          1027,
                                          1052,
                                          3057,
                                          1028,
                                          1036,
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

