namespace BehaviourTrees.all;


class XinZhao_PurchaseItemsClass : AI_Characters
{

    private PurchaseItemsClass purchaseItems = new();


    public bool XinZhao_PurchaseItems(
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
                                          1042,
                                          3006,
                                          1042,
                                          1038,
                                          3071,
                                          1007,
                                          3083,
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
                                          1055,
                                          1036,
                                          1029,
                                          3106,
                                          1001,
                                          1028,
                                          3067,
                                          1033,
                                          3065,
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
                                          1042,
                                          1038,
                                          3071,
                                          1011,
                                          3022,
                                          1037,
                                          3126,
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
                                          1036,
                                          1029,
                                          3106,
                                          1042,
                                          3006,
                                          1036,
                                          1028,
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
                                          20,
                                          3142,
                                          1043,
                                          1033,
                                          3091,
                                          1042,
                                          1051,
                                          3086,
                                          1018,
                                          3046,
                                          1033,
                                          3028,
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
                                          1036,
                                          1036,
                                          3134,
                                          1051,
                                          3093,
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
                                          3142,
                                          1043,
                                          1033,
                                          3091,
                                          1038,
                                          1042,
                                          3071,
                                          1028,
                                          3044,
                                          3022,
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
                                          1036,
                                          1036,
                                          3134,
                                          1051,
                                          3093,
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
                                          19,
                                          3142,
                                          1043,
                                          1033,
                                          3091,
                                          1038,
                                          1042,
                                          3071,
                                          1028,
                                          3044,
                                          3022,
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
                                          1036,
                                          1036,
                                          3134,
                                          1051,
                                          3093,
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

