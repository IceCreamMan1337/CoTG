namespace BehaviourTrees.all;


class LevelUpAbilitiesClass : AI_Characters
{


    public bool LevelUpAbilities(
         AttackableUnit Self,
     int UnitLevel
        )
    {
        return
                    // Sequence name :ReturnFailure

                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              ;
    }
}

