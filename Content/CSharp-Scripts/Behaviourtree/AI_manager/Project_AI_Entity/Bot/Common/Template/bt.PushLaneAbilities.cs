namespace BehaviourTrees.all;


//PushLaneAbilitiesClass
class PushLaneAbilitiesClass : AI_Characters
{
    private AcquireTargetClass acquireTarget = new();
    private AutoAttack_MeleeClass autoAttack_Melee = new();
    private AutoAttackClass autoAttack = new();

    public bool PushLaneAbilities(
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

        //custom 
        AttackableUnit _Target = default;
        bool _TargetValid = false;
        bool _DeAggro = false;
        Vector3 _TargetAcquiredPosition = default;
        float _DeAggroTime = default;

        bool result =
                // Sequence name :ReturnFailure

                //hack for pushlanetask.xml not in file 
                DebugAction("AcquireTarget") &&
                acquireTarget.AcquireTarget(
                          out _TargetValid,
                          out _Target,
                          out _DeAggro,
                          out _TargetAcquiredPosition,
                          out _DeAggroTime,
                          TargetValid,
                          Target,
                          TargetAcquiredPosition,
                          default,
                          SelfPosition,
                          Self,
                          false,
                          default,
                          default) &&

                          (
                             (DebugAction("isdistance") &&
                          IsMelee(Self) == false
                          &&
                         autoAttack.AutoAttack(
                                    out IssuedAttack,
                                    out IssuedAttackTarget,
                                    _Target,
                                    Self,
                                    IssuedAttack,
                                    IssuedAttackTarget))
                         ||
                            (DebugAction("IsMelee") &&
                         autoAttack_Melee.AutoAttack_Melee(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              _Target,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget))
                         )
            ;
        /*  SetVarBool(
      out Run,
      false) &&
Run == true*/


        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;
        return result;
    }
}

