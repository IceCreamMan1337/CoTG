using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class HealAbilitiesClass : AI_Characters 
{
     
     public bool HealAbilities(
          out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget
         )
    {
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
      

        bool result =
            // Sequence name :ReturnFailure
            (
                  SetVarBool(
                        out Run,
                        false) &&
                  Run == true

            );

        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
       

        return result;
    }
}

