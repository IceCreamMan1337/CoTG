namespace BehaviourTrees.all;


class LevelUpClass : AI_Characters
{
    protected bool TryCallLevelUp(
    object procedureObject,
    AttackableUnit Self,
    int UnitLevel
    )
    {


        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
            Self,
            UnitLevel);

        if (callSuccess)
        {
            return true;
        }
        return false;
    }
    public bool LevelUp(
        AttackableUnit Self,
        string Champion,
        object LevelUpAbilities,
        int UnitLevel
    )
    {
        return
                GetUnitSkillPoints(out SkillPoints, Self) &&
                GreaterInt(SkillPoints, 0) &&
                GetUnitLevel(out UnitLevel, Self) &&
                //LevelUpAbilities
                TryCallLevelUp(LevelUpAbilities, Self, UnitLevel)
               ;
    }
}

