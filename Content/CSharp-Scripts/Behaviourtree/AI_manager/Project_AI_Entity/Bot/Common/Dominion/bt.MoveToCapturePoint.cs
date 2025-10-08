using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class MoveToCapturePointClass : AI_Characters
{
    protected bool TryMovetoCapturePointSequence(
        out float _PreviousSpellCastTime,
out int _CurrentSpellCast,
out AttackableUnit _CurrentSpellCastTarget,
out float _CastSpellTimeThreshold,
out bool _SpellStall,
object procedureObject,
float CastSpellTimeThreshold,

int PreviousSpellCast,
AttackableUnit PreviousSpellCastTarget,

float PreviousSpellCastTime,
AttackableUnit Self,
 bool SpellStall
)
    {

        var CurrentSpellCast = PreviousSpellCast;
        var CurrentSpellCastTarget = PreviousSpellCastTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
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
                       CastSpellTimeThreshold,
                       CurrentSpellCast,
                       CurrentSpellCastTarget,
                           PreviousSpellCast,
                                          PreviousSpellCastTarget,
                           PreviousSpellCastTime,
                           Self,
                           SpellStall);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                _CastSpellTimeThreshold = (float)outputs[0];
                _CurrentSpellCast = (int)outputs[1];
                _CurrentSpellCastTarget = (AttackableUnit)outputs[2];
                _PreviousSpellCastTime = (float)outputs[3];

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
    private SummonerGhostClass summonerGhost = new();
    public bool MoveToCapturePoint(
          out string _ActionPerformed,
          out int __PreviousSpellCast,


        out float __CastSpellTimeThreshold,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out float __PreviousSpellCastTime,
     out bool __SpellStall,


     AttackableUnit Self,
     int GhostSlot,
     float CastSpellTimeThreshold,
     AttackableUnit PreviousSpellCastTarget,
     float PreviousSpellCastTime,
     bool SpellStall,
     string Champion,
     object MoveToCapturePointAbilities//,
                                       //   
        )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;
        string ActionPerformed = default;
        int PreviousSpellCast = default;

        bool result =
                    // Sequence name :MoveToCapturePoint

                    TestUnitHasBuff(
                          Self,
                          default,
                          "OdinRecall",
                          false) &&
                    // Sequence name :NotAlreadyCapturingPoint
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "OdinCaptureChannel",
                                false)
                          ||
                          TestUnitIsChanneling(
                                Self,
                                false)
                    ) &&
                    ClearUnitAIAttackTarget() &&
                    TestUnitAIHasTask(
                          true) &&
                    GetUnitAITaskPosition(
                          out TaskPosition) &&
                    // Sequence name :Selector
                    (
                          // Sequence name :Sequence
                          (
                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
                                CountUnitsInTargetArea(
                                      out UnitsNearby,
                                      Self,
                                      SelfPosition,
                                      1500,
                                      AffectEnemies | AffectHeroes,
                                      "") &&
                                UnitsNearby == 0 &&
                                DistanceBetweenObjectAndPoint(
                                      out DistanceToPoint,
                                      Self,
                                      TaskPosition) &&
                                GreaterEqualFloat(
                                      DistanceToPoint,
                                      1000) &&
                              TryMovetoCapturePointSequence(

                                    out CastSpellTimeThreshold,
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out SpellStall,
                                    MoveToCapturePointAbilities,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    Self,
                                    SpellStall)
                          ) ||
                          // Sequence name :UseGhostAtBeginningOfGame
                          (
                                GetGameTime(
                                      out GameTime) &&
                                LessFloat(
                                      GameTime,
                                      110) &&
                                DistanceBetweenObjectAndPoint(
                                      out DistanceToPoint,
                                      Self,
                                      TaskPosition) &&
                                GreaterEqualFloat(
                                      DistanceToPoint,
                                      2500) &&
                               summonerGhost.SummonerGhost(
                                      Self,
                                      GhostSlot)
                          ) ||
                                // Sequence name :GotoTaskPosition

                                IssueMoveToPositionOrder(
                                      TaskPosition)

                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "MoveToCapturePoint")

              ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        _ActionPerformed = ActionPerformed;
        __PreviousSpellCast = PreviousSpellCast;

        return result;
    }
}

