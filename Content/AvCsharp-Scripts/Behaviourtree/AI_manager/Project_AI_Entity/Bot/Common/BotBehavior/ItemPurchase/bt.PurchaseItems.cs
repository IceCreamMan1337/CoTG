using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class PurchaseItemsClass : AI_Characters 
{
      

     public bool PurchaseItems(
         out int __ItemPurchasedIndex,
      out bool __FinishedItemBuild,
      int ItemListLength,
      int ItemID_10,
      int ItemID_11,
      int ItemID_12,
      int ItemID_13,
      int ItemID_14,
      int ItemID_15,
      int ItemID_16,
      int ItemID_17,
      int ItemID_18,
      int ItemID_19,
      int ItemID_20,
      int ItemID_21,
      int ItemID_22,
      int ItemID_23,
      int ItemID_24,
      int ItemID_25,
      int ItemPurchasedIndex,
      AttackableUnit Self,
      int ItemID_01,
      int ItemID_02,
      int ItemID_03,
      int ItemID_04,
      int ItemID_05,
      int ItemID_06,
      int ItemID_07,
      int ItemID_08,
      int ItemID_09,
      bool FinishedItemBuild
         )
    {
        int _ItemPurchasedIndex = ItemPurchasedIndex;
        bool _FinishedItemBuild = FinishedItemBuild;

        bool result =
            // Sequence name :PurchaseItems
            (
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :DonePurchasingItems?
                        (
                              GreaterInt(
                                    ItemPurchasedIndex,
                                    ItemListLength) &&
                              SetVarBool(
                                    out _FinishedItemBuild,
                                    true)
                        ) || MaskFailure()
                  ) &&
                  LessEqualInt(
                        ItemPurchasedIndex,
                        ItemListLength) &&
                  SetVarBool(
                        out _FinishedItemBuild,
                        false) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :1-10
                        (
                              LessEqualInt(
                                    ItemPurchasedIndex,
                                    10) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Item1
                                    (
                                          ItemPurchasedIndex == 1 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_01) &&
                                          UnitAIBuyItem(
                                                ItemID_01)
                                    ) ||
                                    // Sequence name :Item2
                                    (
                                          ItemPurchasedIndex == 2 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_02
                                                ) &&
                                          UnitAIBuyItem(
                                                ItemID_02)
                                    ) ||
                                    // Sequence name :Item3
                                    (
                                          ItemPurchasedIndex == 3 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_03
                                                ) &&
                                          UnitAIBuyItem(
                                                ItemID_03)
                                    ) ||
                                    // Sequence name :Item4
                                    (
                                          ItemPurchasedIndex == 4 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_04
                                                ) &&
                                          UnitAIBuyItem(
                                                ItemID_04)
                                    ) ||
                                    // Sequence name :Item5
                                    (
                                          ItemPurchasedIndex == 5 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_05,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_05)
                                    ) ||
                                    // Sequence name :Item6
                                    (
                                          ItemPurchasedIndex == 6 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_06,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_06)
                                    ) ||
                                    // Sequence name :Item7
                                    (
                                          ItemPurchasedIndex == 7 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_07,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_07)
                                    ) ||
                                    // Sequence name :Item8
                                    (
                                          ItemPurchasedIndex == 8 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_08,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_08)
                                    ) ||
                                    // Sequence name :Item9
                                    (
                                          ItemPurchasedIndex == 9 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_09,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_09)
                                    ) ||
                                    // Sequence name :Item10
                                    (
                                          ItemPurchasedIndex == 10 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_10,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_10)
                                    )
                              )
                        ) ||
                        // Sequence name :11-20
                        (
                              LessEqualInt(
                                    ItemPurchasedIndex,
                                    20) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Item11
                                    (
                                          ItemPurchasedIndex == 11 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_11,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_11)
                                    ) ||
                                    // Sequence name :Item12
                                    (
                                          ItemPurchasedIndex == 12 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_12,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_12)
                                    ) ||
                                    // Sequence name :Item13
                                    (
                                          ItemPurchasedIndex == 13 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_13,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_13)
                                    ) ||
                                    // Sequence name :Item14
                                    (
                                          ItemPurchasedIndex == 14 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_14,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_14)
                                    ) ||
                                    // Sequence name :Item15
                                    (
                                          ItemPurchasedIndex == 15 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_15,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_15)
                                    ) ||
                                    // Sequence name :Item16
                                    (
                                          ItemPurchasedIndex == 16 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_16,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_16)
                                    ) ||
                                    // Sequence name :Item17
                                    (
                                          ItemPurchasedIndex == 17 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_17,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_17)
                                    ) ||
                                    // Sequence name :Item18
                                    (
                                          ItemPurchasedIndex == 18 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_18,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_18)
                                    ) ||
                                    // Sequence name :Item19
                                    (
                                          ItemPurchasedIndex == 19 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_19,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_19)
                                    ) ||
                                    // Sequence name :Item20
                                    (
                                          ItemPurchasedIndex == 20 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_20,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_20)
                                    )
                              )
                        ) ||
                        // Sequence name :21-25
                        (
                              LessEqualInt(
                                    ItemPurchasedIndex,
                                    25) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Item21
                                    (
                                          ItemPurchasedIndex == 21 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_21,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_21)
                                    ) ||
                                    // Sequence name :Item22
                                    (
                                          ItemPurchasedIndex == 22 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_22,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_22)
                                    ) ||
                                    // Sequence name :Item23
                                    (
                                          ItemPurchasedIndex == 23 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_23,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_23)
                                    ) ||
                                    // Sequence name :Item24
                                    (
                                          ItemPurchasedIndex == 24 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_24,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_24)
                                    ) ||
                                    // Sequence name :Item25
                                    (
                                          ItemPurchasedIndex == 25 &&
                                          TestUnitAICanBuyItem(
                                                ItemID_25,
                                                true) &&
                                          UnitAIBuyItem(
                                                ItemID_25)
                                    )
                              )
                        )
                  ) &&
                  AddInt(
                        out _ItemPurchasedIndex,
                        ItemPurchasedIndex,
                        1)

            );
         __ItemPurchasedIndex = _ItemPurchasedIndex;
         __FinishedItemBuild = _FinishedItemBuild;

        return result;

      }
}

