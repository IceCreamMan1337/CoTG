using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionAttackClass : AI_Characters 
{
      private SummonerPromoteClass summonerPromote = new SummonerPromoteClass();

    protected bool TryCallDominionAttackMinion(
   out int CurrentSpellCast,
  out AttackableUnit CurrentSpellCastTarget,
  out float _PreviousSpellCastTime,
 out float _CastSpellTimeThreshold,
  object procedureObject,
    AttackableUnit Self,
  AttackableUnit Target,
 int PreviousSpellCast,
 AttackableUnit PreviousSpellCastTarget,
  float CastSpellTimeThreshold,
  float PreviousSpellCastTime
 )
    {
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
             PreviousSpellCastTime);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {

                CurrentSpellCast = (int)outputs[0];
                CurrentSpellCastTarget = (AttackableUnit)outputs[1];
                _PreviousSpellCastTime = (float)outputs[2];
                _CastSpellTimeThreshold = (float)outputs[3];
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



    public bool DominionAttack(

               out string _ActionPerformed,
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,

      AttackableUnit Self,
      Vector3 SelfPosition,
      int PromoteSlot,
      string ChampionName,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      object DominionAttackMinion
  //    AttackableUnit Target

         )
    {
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
       string ActionPerformed = default;
        bool result =
            // Sequence name :DominionAttack
            (
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
                  // Sequence name :Conditions
                  (
                        // Sequence name :DominionAttack_DefendMission
                        (
                              TestAIEntityHasTask(
                                    Self,
                                  AITaskTopicType.DEFEND_CAPTURE_POINT,
                                  default,
                                  default,
                                  default,
                                    true) &&
                              GetAITaskFromEntity(
                                    out Task,
                                    Self) &&
                              GetAITaskPosition(
                                    out TaskPosition,
                                    Task) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceToPoint,
                                    Self,
                                    TaskPosition) &&
                              LessFloat(
                                    DistanceToPoint,
                                    1000)
                        ) ||
                        TestAIEntityHasTask(
                              Self,
                            AITaskTopicType.DEFEND_CAPTURE_POINT,
                            default,
                            default,
                            default,
                              false)
                  ) &&
                  // Sequence name :AttackOrPromote
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
                        // Sequence name :AttackMinions
                        (
                              GetUnitAIClosestTargetInArea(
                                    out Target,
                                    Self,
                                    default,
                                    true,
                                    SelfPosition,
                                    1000,
                                    AffectEnemies | AffectMinions) &&
                              // Sequence name :Selector
                              (
                                      TryCallDominionAttackMinion(
                                out CurrentSpellCast,
                                out CurrentSpellCastTarget,
                                out _PreviousSpellCastTime,
                                out _CastSpellTimeThreshold,
                                DominionAttackMinion,
                                Target,
                                Self,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                CastSpellTimeThreshold,
                                PreviousSpellCastTime
                                ) &&
                                    // Sequence name :AutoAttack
                                    (
                                          SetUnitAIAttackTarget(
                                                Target) &&
                                          IssueChaseOrder()
                                    )
                              )
                        )
                  ) &&
                  SetVarString(
                        out ActionPerformed,
                        "Attack")

            );

         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         _ActionPerformed = ActionPerformed;
        return result;
    }
}

