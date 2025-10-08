namespace BehaviourTrees.all;


class DominionLevelUpClass : AI_Characters
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
    public bool DominionLevelUp(
     AttackableUnit Self,
      string Champion,
      object LevelUpAbilities, //function
      int UnitLevel
    )
    {
        return
                GetUnitSkillPoints(out SkillPoints, Self) &&
                GreaterInt(SkillPoints, 0) &&
                GetUnitLevel(out UnitLevel, Self) &&
                TryCallLevelUp(LevelUpAbilities, Self, UnitLevel)
              ;
    }
}

