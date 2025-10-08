namespace BehaviourTrees.all;


class DominionGlobalClass : AI_Characters
{
    protected bool TryGlobalDominionSequence(
  out int CurrentSpellCast,
 out AttackableUnit CurrentSpellCastTarget,
 out float _PreviousSpellCastTime,
out float _CastSpellTimeThreshold,
out bool _SpellStall,
object procedureObject,
AttackableUnit Self,
int PreviousSpellCast,
AttackableUnit PreviousSpellCastTarget,
 float CastSpellTimeThreshold,
 float PreviousSpellCastTime,
bool IssuedAttack,
 AttackableUnit IssuedAttackTarget,
   bool SpellStall
)
    {
        CurrentSpellCast = PreviousSpellCast;
        CurrentSpellCastTarget = PreviousSpellCastTarget;
        _PreviousSpellCastTime = PreviousSpellCastTime;
        _CastSpellTimeThreshold = CastSpellTimeThreshold;
        _SpellStall = SpellStall;


        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
                           Self,
                           PreviousSpellCast,
                           PreviousSpellCastTarget,
                           CastSpellTimeThreshold,
                           PreviousSpellCastTime,
                           IssuedAttack,
                           IssuedAttackTarget,
                           SpellStall);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                CurrentSpellCast = (int)outputs[0];
                CurrentSpellCastTarget = (AttackableUnit)outputs[1];
                _PreviousSpellCastTime = (float)outputs[2];
                _CastSpellTimeThreshold = (float)outputs[3];
                _SpellStall = (bool)outputs[4];

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


    public bool DominionGlobal(
         out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out bool __SpellStall,
     out string _ActionPerformed,
     AttackableUnit Self,
     string Champion,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float CastSpellTimeThreshold,
     float PreviousSpellCastTime,
     bool IssuedAttack,
     AttackableUnit IssuedAttackTarget,
     bool SpellStall,
     object GlobalAbilities // function



        )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
        string ActionPerformed = default;


        bool result =
                     // Sequence name :Sequence

                     TryGlobalDominionSequence(

                            out CurrentSpellCast,
                            out CurrentSpellCastTarget,
                              out _PreviousSpellCastTime,
                            out _CastSpellTimeThreshold,
                            out _SpellStall,
                            //   out ActionPerformed,
                            GlobalAbilities,
                            Self,
                            PreviousSpellCast,
                            PreviousSpellCastTarget,
                            CastSpellTimeThreshold,
                            PreviousSpellCastTime,
                            IssuedAttack,
                            IssuedAttackTarget,
                            SpellStall) &&
                    SetVarString(
                          out ActionPerformed,
                          "Global")

              ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;
        _ActionPerformed = ActionPerformed;


        return result;
    }


}

