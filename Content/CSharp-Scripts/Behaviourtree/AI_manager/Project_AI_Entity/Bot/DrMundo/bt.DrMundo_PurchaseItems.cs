namespace BehaviourTrees.all;


class DrMundo_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool DrMundo_PurchaseItems(
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
                                          18,
                                          3067,
                                          1042,
                                          1053,
                                          3050,
                                          1029,
                                          1006,
                                          3097,
                                          1028,
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
                                          1054,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
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
                                          19,
                                          3068,
                                          1057,
                                          1007,
                                          1007,
                                          3109,
                                          1011,
                                          1028,
                                          3083,
                                          1031,
                                          3005,
                                           default,
                                         default,
                                        default,
                                      default,
                                      default,
                                       default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1028,
                                          1001,
                                          1033,
                                          3111,
                                          1011,
                                          1007,
                                          3083,
                                          1031,
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
                                          18,
                                          3067,
                                          1042,
                                          1053,
                                          3050,
                                          1029,
                                          1006,
                                          3097,
                                          1028,
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
                                          1062,
                                          1001,
                                          1033,
                                          3111,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
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
                                          23,
                                          3065,
                                          1011,
                                          3022,
                                          1031,
                                          1011,
                                          3068,
                                          1031,
                                          1006,
                                          3082,
                                          1028,
                                          3132,
                                          3143,
                                          1053,
                                          3050,
                                              default,
                                    default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1028,
                                          1001,
                                          1033,
                                          3111,
                                          1036,
                                          3044,
                                          1028,
                                          3067,
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
                                          23,
                                          3065,
                                          1011,
                                          3022,
                                          1031,
                                          1011,
                                          3068,
                                          1031,
                                          1006,
                                          3082,
                                          1028,
                                          3132,
                                          3143,
                                          1053,
                                          3050,
                                            default,
                                    default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1028,
                                          1001,
                                          1033,
                                          3111,
                                          1036,
                                          3044,
                                          1028,
                                          3067,
                                          1033,
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

