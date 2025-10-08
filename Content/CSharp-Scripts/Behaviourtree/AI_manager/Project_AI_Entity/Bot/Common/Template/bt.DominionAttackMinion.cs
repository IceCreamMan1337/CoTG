namespace BehaviourTrees.all;


class DominionAttackMinionClass : AI_Characters
{


    public bool DominionAttackMinion(
          out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out float __PreviousSpellCastTime,
     out float __CastSpellTimeThreshold,
     AttackableUnit Self,
     AttackableUnit Target,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float CastSpellTimeThreshold,
     float PreviousSpellCastTime
        )
    {

        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;


        bool result =
                  // Sequence name :ReturnFailure

                  SetVarBool(
                        out Run,
                        false) &&
                  Run == true

            ;


        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;


        return result;
    }
}

