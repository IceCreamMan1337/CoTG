using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class AttackTargetClass : AI_Characters 
{
     

     public bool AttackTarget(
          out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out bool __SpellStall,
      AttackableUnit ToAttack,
      AttackableUnit Self,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      bool SpellStall
         )
    {
         bool _IssuedAttack = IssuedAttack;
       AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;   
       float _PreviousSpellCastTime = PreviousSpellCastTime;
       float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
       bool _SpellStall = SpellStall;

        bool result =
              // Sequence name :ReturnFailure
              (
                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              );

         __IssuedAttack = _IssuedAttack;
         __IssuedAttackTarget = _IssuedAttackTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __SpellStall = _SpellStall;

        return result;
    }
}

