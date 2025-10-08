using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class PassiveActionsClass : AI_Characters
{

    private TauntedClass taunted = new TauntedClass();
    private AtBaseHealAndBuyClass atBaseHealAndBuy = new AtBaseHealAndBuyClass();
    private LevelUpClass levelUp = new LevelUpClass();
    public bool PassiveActions(
      //out int __ItemPurchaseIndex,
      out int __PotionsToBuy,
      out bool _IssuedAttack,
      out AttackableUnit _IssuedAttackTarget,
      out int __PurchaseItemIndex,
      out bool __FinishedItemBuild,
      out string __ExtraItem,
      out bool __ExtraItemPurchased,
      AttackableUnit Self,
      int PotionsToBuy,
      bool PrevIssuedAttack,
      AttackableUnit PrevIssuedAttackTarget,
      string Champion,
      int PurchaseItemIndex,
      bool IsDominionGameMode,
      object ItemBuildPurchase,
      //  int ItemPurchaseIndex,

      object LevelUpAbilities,
      // int UnitLevel,
      bool BeginnerScaling,
       bool FinishedItemBuild,
      string ExtraItem,
      bool ExtraItemPurchased
         )
    {
        bool _FinishedItemBuild = FinishedItemBuild;
        int _PotionsToBuy = PotionsToBuy;
        bool IssuedAttack = default;
        AttackableUnit IssuedAttackTarget = default;
        int _PurchaseItemIndex = PurchaseItemIndex;
        string _ExtraItem = ExtraItem;
        bool _ExtraItemPurchased = ExtraItemPurchased;
        Vector3 FleePoint;

        bool result =
              // Sequence name :PassiveActions
              (
                    taunted.Taunted(
                          out _IssuedAttack,
                          out _IssuedAttackTarget,
                          Self,
                          PrevIssuedAttack,
                          PrevIssuedAttackTarget) ||
                    // Sequence name :Fear
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "Fear",
                                true) &&
                          IssueWanderOrder(

                                )
                    ) ||
                    // Sequence name :Flee
                    (
                          TestUnitHasBuff(
                                Self,
                               default,
                                "Flee",
                                true) &&
                          GetUnitAIFleePoint(
                                out FleePoint) &&
                          IssueMoveToPositionOrder(
                                FleePoint)
                    ) ||
                  atBaseHealAndBuy.AtBaseHealAndBuy(
                          out _PotionsToBuy,
                          out _FinishedItemBuild,
                          out _PurchaseItemIndex,

                          out _ExtraItem,
                          out _ExtraItemPurchased,
                          Self,
                          PotionsToBuy,
                          Champion,
                          PurchaseItemIndex,
                          IsDominionGameMode,
                          ItemBuildPurchase,
                          FinishedItemBuild,
                          ExtraItem,
                          ExtraItemPurchased)
                  ||
                levelUp.LevelUp(
                          Self,
                          Champion,
                          LevelUpAbilities, 1)

              );


        __FinishedItemBuild = _FinishedItemBuild;
        __PotionsToBuy = _PotionsToBuy;
        _IssuedAttack = IssuedAttack;
        _IssuedAttackTarget = IssuedAttackTarget;
        __PurchaseItemIndex = _PurchaseItemIndex;
        __ExtraItem = _ExtraItem;
        __ExtraItemPurchased = _ExtraItemPurchased;

        return result;
    }
}

