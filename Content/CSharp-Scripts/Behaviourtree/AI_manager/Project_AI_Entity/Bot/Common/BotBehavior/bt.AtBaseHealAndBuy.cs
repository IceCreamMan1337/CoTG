namespace BehaviourTrees.all;


class AtBaseHealAndBuyClass : AI_Characters
{
    private BuyExtraItemClass buyExtraItem = new();


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
            Console.WriteLine("[TryCallItemBuildPurchase] procedureObject null");
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
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    public bool AtBaseHealAndBuy(
         out int __ItemPurchaseIndex,
      out bool __FinishedItemBuild,
      out int __PotionsToBuy,
      out string __ExtraItem,
      out bool __ExtraItemPurchased,
      AttackableUnit Self,
      int PotionsToBuy,
      string Champion,
      int ItemPurchaseIndex,
      bool IsDominionGameMode,
      object ItemBuildPurchase,
      bool FinishedItemBuild,
      string ExtraItem,
      bool ExtraItemPurchased
         )
    {

        int _ItemPurchaseIndex = ItemPurchaseIndex;
        bool _FinishedItemBuild = FinishedItemBuild;
        int _PotionsToBuy = PotionsToBuy;
        string _ExtraItem = ExtraItem;
        bool _ExtraItemPurchased = ExtraItemPurchased;

        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        float UnitAttackSpeed;




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
                        (DebugAction("CallProcedureVariable ItemBuildPurchase") &&
                        TryCallItemBuildPurchase(
                             out _ItemPurchaseIndex,
                             out _FinishedItemBuild,
                             ItemBuildPurchase,
                             Self,
                             IsDominionGameMode,
                             ItemPurchaseIndex,
                             FinishedItemBuild
                            )
                         && DebugAction(" ItemBuildPurchase apply"))
                        ||
                        // Sequence name :ExtraItem
                        (
                              TestEntityDifficultyLevel(
                                    false,
                                 EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              FinishedItemBuild == true &&
                              ExtraItemPurchased == false &&
                          buyExtraItem.BuyExtraItem(
                                    out _ExtraItem,
                                    out _ExtraItemPurchased,
                                    Self,
                                    ExtraItem,
                                    ExtraItemPurchased)
                        ) ||
                        // Sequence name :Elixirs
                        (
                              TestEntityDifficultyLevel(
                                    false,
                                  EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              ExtraItemPurchased == true &&
                              // Sequence name :BuyElixirs
                              (
                                    // Sequence name :ElixirOfFortitude
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "PotionOfGiantStrength",
                                                false) &&
                                          UnitAIBuyItem(
                                                2037) &&
                                          IssueUseItemOrder(
                                                2037, default
                                                )
                                    ) ||
                                    // Sequence name :ElixirOfAgilityFirstIfAS&gt,0.9
                                    (
                                          GetUnitAttackSpeed(
                                                out UnitAttackSpeed,
                                                Self) &&
                                          GreaterFloat(
                                                UnitAttackSpeed,
                                                0.9f) &&
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "PotionOfElusiveness",
                                                false) &&
                                          UnitAIBuyItem(
                                                2038) &&
                                          IssueUseItemOrder(
                                                2038, default
                                                )
                                    ) ||
                                    // Sequence name :ElixirOfBrilliance
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "PotionOfBrilliance",
                                                false) &&
                                          UnitAIBuyItem(
                                                2039) &&
                                          IssueUseItemOrder(
                                                2039, default
                                                )
                                    ) ||
                                    // Sequence name :ElixirOfAgility
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "PotionOfElusiveness",
                                                false) &&
                                          UnitAIBuyItem(
                                                2038) &&
                                          IssueUseItemOrder(
                                                2038, default
                                                )
                                    )
                              )
                        ) ||
                        // Sequence name :GetHealthPotions
                        (
                              GreaterInt(
                                    PotionsToBuy,
                                    0) &&
                              TestChampionHasItem(
                                    Self,
                                    2003,
                                    false) &&
                              TestUnitAICanBuyItem(
                                    2003,
                                    true) &&
                              UnitAIBuyItem(
                                    2003) &&
                              SubtractInt(
                                    out _PotionsToBuy,
                                    PotionsToBuy,
                                    1)

                        )
                  )
            ;



        __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        __PotionsToBuy = _PotionsToBuy;
        __ExtraItem = _ExtraItem;
        __ExtraItemPurchased = _ExtraItemPurchased;

        return result;
    }
}

