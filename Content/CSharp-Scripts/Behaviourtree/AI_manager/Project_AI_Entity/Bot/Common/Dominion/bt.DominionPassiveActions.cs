namespace BehaviourTrees.all;


class DominionPassiveActionsClass : AI_Characters
{
    private DominionAtBaseHealAndBuyClass dominionAtBaseHealAndBuy = new();
    private DominionLevelUpClass dominionLevelUp = new();

    private TauntedClass taunted = new();
    public bool DominionPassiveActions(
     //    out int __ItemPurchaseIndex,
     out int __PotionsToBuy,
     out bool _IssuedAttack,
     out AttackableUnit _IssuedAttackTarget,
     out int __PurchaseItemIndex,
     out bool __FinishedItemBuild,
      AttackableUnit Self,
     int PotionsToBuy,
     bool PrevIssuedAttack,
     AttackableUnit PrevIssuedAttackTarget,
     string Champion,
     int PurchaseItemIndex,
     bool IsDominionGameMode,
     object ItemBuildPurchase, //function 
                               //   int ItemPurchaseIndex,

     object LevelUpAbilities, // function 
       bool FinishedItemBuild
        //  int UnitLevel
        )
    {
        // int _ItemPurchaseIndex = ItemPurchaseIndex;
        int _PotionsToBuy = PotionsToBuy;
        bool IssuedAttack = default;
        AttackableUnit IssuedAttackTarget = default;
        int _PurchaseItemIndex = PurchaseItemIndex;
        bool _FinishedItemBuild = FinishedItemBuild;

        bool result =
                    // Sequence name :PassiveActions

                    (taunted.Taunted(
                          out IssuedAttack,
                          out IssuedAttackTarget,
                          Self,
                          PrevIssuedAttack,
                          PrevIssuedAttackTarget) &&
                          // Sequence name :Fear

                          TestUnitHasBuff(
                                Self,
                                default,
                                "Fear",
                                true) &&
                          IssueWanderOrder(



                                ))
                     ||
                    // Sequence name :Flee
                    (
                          TestUnitHasBuff(
                                Self,
                                default
                                ,
                                "Flee",
                                true) &&
                          GetUnitAIFleePoint(
                                out FleePoint) &&
                          IssueMoveToPositionOrder(
                                FleePoint)
                    ) ||
                    (dominionAtBaseHealAndBuy.DominionAtBaseHealAndBuy(
                          out _PotionsToBuy,
                          out _PurchaseItemIndex,
                          out _FinishedItemBuild,
                          Self,
                          PotionsToBuy,
                          "Champion",
                          PurchaseItemIndex,
                          IsDominionGameMode,
                          ItemBuildPurchase,
                          FinishedItemBuild)
                    &&
                    dominionLevelUp.DominionLevelUp(
                          Self,
                          "Champion",
                          LevelUpAbilities,
                          UnitLevel))

              ;

        // __ItemPurchaseIndex = _ItemPurchaseIndex;
        __PotionsToBuy = _PotionsToBuy;
        _IssuedAttack = IssuedAttack;
        _IssuedAttackTarget = IssuedAttackTarget;
        __PurchaseItemIndex = _PurchaseItemIndex;
        __FinishedItemBuild = _FinishedItemBuild;


        return result;

    }
}

