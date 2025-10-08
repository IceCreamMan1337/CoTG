namespace BehaviourTrees.all;


class ItemBuildPurchaseClass : AI_Characters
{


    public bool ItemBuildPurchase(
          out int __ItemPurchaseIndex,
     out bool __FinishedItemBuild,
     AttackableUnit Self,
     int ItemPurchaseIndex,
     bool IsDominionGameMode,
     bool FinishedItemBuild
        )
    {
        int _ItemPurchaseIndex = ItemPurchaseIndex;
        bool _FinishedItemBuild = FinishedItemBuild;


        bool result =
                    // Sequence name :ReturnFailure

                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              ;

        __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        return result;
    }
}

