namespace BehaviourTrees.all;


class DominionAtBaseHealAndBuyClass : AI_Characters
{
    protected bool TryCallItemBuildPurchase(
 out int __ItemPurchaseIndex,
 out bool __FinishedItemBuild,
 object procedureObject,
 AttackableUnit Self,
 bool IsDominionGameMode,
 int ItemPurchaseIndex,
 bool FinishedItemBuild
  )
    {
        __ItemPurchaseIndex = ItemPurchaseIndex;
        __FinishedItemBuild = FinishedItemBuild;

        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
            Self,
            IsDominionGameMode,
            ItemPurchaseIndex,
            FinishedItemBuild);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                __ItemPurchaseIndex = (int)outputs[0];
                __FinishedItemBuild = (bool)outputs[1];
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool DominionAtBaseHealAndBuy(
         out int __PotionsToBuy,
      out int __ItemPurchaseIndex,
      out bool __FinishedItemBuild,

      AttackableUnit Self,
      int PotionsToBuy,
      string Champion,
      int ItemPurchaseIndex,
      bool IsDominionGameMode,
      object ItemBuildPurchase,//function
      bool FinishedItemBuild
         )
    {
        int _ItemPurchaseIndex = ItemPurchaseIndex;
        bool _FinishedItemBuild = FinishedItemBuild;
        int _PotionsToBuy = PotionsToBuy;
        bool result =
                    // Sequence name :AtBaseHealAndBuy

                    GetUnitAIBasePosition(
                          out BaseLocation,
                          Self) &&
                    DistanceBetweenObjectAndPoint(
                          out Distance,
                          Self,
                          BaseLocation) &&
                    LessEqualFloat(
                          Distance,
                          1000) &&
                    // Sequence name :HealOrBuy
                    (
                          // Sequence name :Heal
                          (
                                GetUnitMaxHealth(
                                      out MaxHealth,
                                      Self) &&
                                GetUnitCurrentHealth(
                                      out CurrentHealth,
                                      Self) &&
                                DivideFloat(
                                      out Health_Ratio,
                                      CurrentHealth,
                                      MaxHealth) &&
                                LessFloat(
                                      Health_Ratio,
                                      0.95f)
                          ) ||
                          (TryCallItemBuildPurchase(
                             out _ItemPurchaseIndex,
                             out _FinishedItemBuild,
                             ItemBuildPurchase,
                             Self,
                             IsDominionGameMode,
                             ItemPurchaseIndex,
                             FinishedItemBuild
                            ) &&
                                // Sequence name :GetHealthPotions

                                TestEntityDifficultyLevel(
                                      false,
                                 EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                GreaterInt(
                                      PotionsToBuy,
                                      0) &&
                                TestChampionHasItem(
                                      Self,
                                      2003,
                                      false) &&
                                TestUnitAICanBuyItem(
                                      2003) &&
                                UnitAIBuyItem(
                                      2003) &&
                                SubtractInt(
                                      out _PotionsToBuy,
                                      PotionsToBuy,
                                      1))


                    )
              ;
        __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        __PotionsToBuy = _PotionsToBuy;
        return result;
    }
}

