namespace BehaviourTrees.all;


class Lux_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool Lux_PurchaseItems(
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
                                          15,
                                          1028,
                                          3067,
                                          1028,
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
                                  default,
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1052,
                                          1026,
                                          3135,
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
                                          23,
                                          1052,
                                          3174,
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
                                          ItemPurchaseIndex,
                                          Self,
                                          1056,
                                          1001,
                                          3020,
                                          1005,
                                          1033,
                                          3028,
                                          1052,
                                          1005,
                                          3108,
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
                                          1052,
                                          3136,
                                          1052,
                                          1027,
                                          3057,
                                          1026,
                                          3100,
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
                                          3020,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
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
                                          18,
                                          1026,
                                          3089,
                                          1052,
                                          1027,
                                          3057,
                                          1026,
                                          3100,
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
                                          18,
                                          1026,
                                          3089,
                                          1052,
                                          1027,
                                          3057,
                                          1026,
                                          3100,
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
                                          3020,
                                          1028,
                                          1027,
                                          3010,
                                          1026,
                                          3027,
                                          1058,
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

