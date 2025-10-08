namespace BehaviourTrees.all;


class HighThreatManagementSpellsClass : AI_Characters
{


    public bool HighThreatManagementSpells(
              out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out bool __SpellStall,
     AttackableUnit Self,
     Vector3 SelfPosition,
     float CastSpellTimeThreshold,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float PreviousSpellCastTime,
     int ExhaustSlot,
     int GhostSlot,
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

