namespace BehaviourTrees.all;


class AttackClass : AI_Characters
{
    private AcquireTargetClass acquireTarget = new();
    private SummonerPromoteClass summonerPromote = new();



    protected bool TryCallAttack(
     out bool _IssuedAttack,
     out AttackableUnit _IssuedAttackTarget,
     out int CurrentSpellCast,
    out AttackableUnit CurrentSpellCastTarget,
       out float _CastSpellTimeThreshold,
    out float _PreviousSpellCastTime,
    object procedureObject,
    AttackableUnit Target,
    AttackableUnit Self,
    bool IssuedAttack,
    AttackableUnit IssuedAttackTarget,
   int PreviousSpellCast,
   AttackableUnit PreviousSpellCastTarget,
    float CastSpellTimeThreshold,
    float PreviousSpellCastTime
   )
    {
        _IssuedAttack = IssuedAttack;
        _IssuedAttackTarget = IssuedAttackTarget;
        CurrentSpellCast = PreviousSpellCast;
        CurrentSpellCastTarget = PreviousSpellCastTarget;
        _PreviousSpellCastTime = PreviousSpellCastTime;
        _CastSpellTimeThreshold = CastSpellTimeThreshold;

        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
             Target,
                                Self,
                                IssuedAttack,
                                IssuedAttackTarget,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                CastSpellTimeThreshold,
                                PreviousSpellCastTime
                                );

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                _IssuedAttack = (bool)outputs[0];
                _IssuedAttackTarget = (AttackableUnit)outputs[1];
                CurrentSpellCast = (int)outputs[2];
                CurrentSpellCastTarget = (AttackableUnit)outputs[3];
                _CastSpellTimeThreshold = (float)outputs[4];
                _PreviousSpellCastTime = (float)outputs[5];

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    public bool Attack(




      out bool __DeAggro,
      out AttackableUnit __Target,
      out Vector3 __TargetAcquiredPosition,
      out bool __TargetValid,
      out float __DeAggroTime,
      out Vector3 __LastKnownPosition,
      out float __LastKnownTime,
        out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,
      out float __PreviousSpellCastTime,

      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out bool __SpellStall,

      out string _ActionPerformed,
           out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      Vector3 SelfPosition,
      bool DeAggro,
      float DeAggroTimeThreshold,
      AttackableUnit Target,
      Vector3 TargetAcquiredPosition,
      bool TargetValid,
      float DeAggroDistance,
      float DeAggroTime,
      Vector3 LastKnownPosition,
      float LastKnownTime,
      float LastKnownTimeThreshold,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      string Champion,
      bool SpellStall,
      object AttackTarget, //function


      int PromoteSlot
         )
    {

        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
        bool _DeAggro = DeAggro;
        AttackableUnit _Target = Target;
        Vector3 _TargetAcquiredPosition = TargetAcquiredPosition;
        bool _TargetValid = TargetValid;
        float _DeAggroTime = DeAggroTime;
        Vector3 _LastKnownPosition = LastKnownPosition;
        float _LastKnownTime = LastKnownTime;
        string ActionPerformed = default;

        AttackableUnit ToAttack = default;


        bool result =
                    // Sequence name :Attack

                    acquireTarget.AcquireTarget(
                          out _TargetValid,
                          out _Target,
                          out _DeAggro,
                          out _TargetAcquiredPosition,
                          out _DeAggroTime,
                          TargetValid,
                          Target,
                          TargetAcquiredPosition,
                          DeAggroTime,
                          SelfPosition,
                          Self,
                          DeAggro,
                          DeAggroDistance,
                          DeAggroTimeThreshold) &&
                    // Sequence name :PromoteOrGotoLastKnownPositionOrAttack
                    (
                          // Sequence name :Promote
                          (
                                GreaterEqualInt(
                                      PromoteSlot,
                                      0) &&
                              summonerPromote.SummonerPromote(
                                      PromoteSlot,
                                      Self)
                          ) ||
                           // Sequence name :LastKnownPosition this purpose an bug
                           /*     (
                                DebugAction("LastKnownPosition ") &&
                                     TestUnitIsVisibleToTeam(
                                           Self,
                                           Target,
                                           false) &&
                                     IssueMoveToPositionOrder(
                                           LastKnownPosition)
                           ) ||
                           */
                           (DebugAction("TryCallAttack ") &&
                          TryCallAttack(
                                out _IssuedAttack,
                                out _IssuedAttackTarget,
                                out CurrentSpellCast,
                                out CurrentSpellCastTarget,
                                out _CastSpellTimeThreshold,
                                out _PreviousSpellCastTime,

                                AttackTarget,
                                _Target,
                                Self,
                                _IssuedAttack,
                                _IssuedAttackTarget,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                _CastSpellTimeThreshold,
                                _PreviousSpellCastTime
                                ))
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "Attack")

              ;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;

        __DeAggro = _DeAggro;
        __Target = _Target;
        __TargetAcquiredPosition = _TargetAcquiredPosition;
        __TargetValid = _TargetValid;
        __DeAggroTime = _DeAggroTime;
        __LastKnownPosition = _LastKnownPosition;
        __LastKnownTime = _LastKnownTime;
        _ActionPerformed = ActionPerformed;


        return result;
    }
}

