namespace BehaviourTrees.all;


class PurchaseItemClass : AI_Characters
{


    public bool PurchaseItem(AttackableUnit Self,
     int ItemID)
    {
        return
                    // Sequence name :HaveOrPurchaseItem

                    TestChampionHasItem(
                          Self,
                          ItemID,
                          true) ||
                    // Sequence name :AttemptPurchase
                    (
                          TestUnitAICanBuyItem(
                                ItemID,
                                true) &&
                          UnitAIBuyItem(
                                ItemID)

                    )
              ;
    }
}

