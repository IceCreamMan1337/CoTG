namespace BehaviourTrees.all;


class MoveToCapturePointAbilitiesClass : AI_Characters
{


    public bool MoveToCapturePointAbilities(
              out float __CastSpellTimeThreshold,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out float __PreviousSpellCastTime,
     out bool __SpellStall,
     float CastSpellTimeThreshold,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float PreviousSpellCastTime,
     AttackableUnit Self,
     bool SpellStall
        )
    {

        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
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
        __SpellStall = _SpellStall;
        return result;
    }
}

