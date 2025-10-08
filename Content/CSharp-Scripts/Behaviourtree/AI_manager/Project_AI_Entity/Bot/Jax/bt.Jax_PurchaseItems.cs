namespace BehaviourTrees.all;


class Jax_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool Jax_PurchaseItems(
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
                                          17,
                                          3057,
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
                                          1052,
                                          3145,
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
                                          1001,
                                          1029,
                                          3047,
                                          1037,
                                          1026,
                                          3124,
                                          1027,
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
                                          21,
                                          1053,
                                          1037,
                                          3144,
                                          3146,
                                          1027,
                                          1052,
                                          3057,
                                          1028,
                                          3044,
                                          3078,
                                          1057,
                                          3102,
                                        default,
                                        default,
                                       default,
                                     default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1055,
                                          1001,
                                          3111,
                                          1037,
                                          1026,
                                          3124,
                                          1052,
                                          1052,
                                          3145,
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
                                          17,
                                          3057,
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
                                          1052,
                                          3145,
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
                                          1033,
                                          3111,
                                          1037,
                                          1026,
                                          3124,
                                          1027,
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
                                          21,
                                          1053,
                                          1037,
                                          3144,
                                          3146,
                                          1027,
                                          1052,
                                          3057,
                                          1028,
                                          3044,
                                          3078,
                                          1031,
                                          3005,
                                              default,
                                    default,
                                   default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1037,
                                          1026,
                                          3124,
                                          1052,
                                          1052,
                                          3145,
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
                                          1053,
                                          1037,
                                          3144,
                                          3146,
                                          1027,
                                          1052,
                                          3057,
                                          1028,
                                          3044,
                                          3078,
                                          1031,
                                          3005,
                                              default,
                                    default,
                                   default,
                                      default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1062,
                                          1001,
                                          3111,
                                          1037,
                                          1026,
                                          3124,
                                          1052,
                                          1052,
                                          3145,
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

