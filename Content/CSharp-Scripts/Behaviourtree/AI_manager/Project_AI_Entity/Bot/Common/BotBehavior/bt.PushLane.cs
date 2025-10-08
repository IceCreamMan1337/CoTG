namespace BehaviourTrees.all;


class PushLaneClass : AI_Characters
{
    protected bool TryPushLaneSequence(
        out float _CastSpellTimeThreshold,
 out int CurrentSpellCast,
out AttackableUnit CurrentSpellCastTarget,
out float _PreviousSpellCastTime,

out bool _SpellStall,
object procedureObject,
AttackableUnit Self,
float CastSpellTimeThreshold,
int PreviousSpellCast,
AttackableUnit PreviousSpellCastTarget,

float PreviousSpellCastTime,

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
            CastSpellTimeThreshold,
            PreviousSpellCast,
            PreviousSpellCastTarget,
            PreviousSpellCastTime,
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
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private SummonerTeleportClass summonerTeleport = new();

    private PushLaneAbilitiesClass pushLaneAbilities = new();
    public bool PushLane(
      out string _ActionPerformed,
      out float __CastSpellTimeThreshold,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      int TeleportSlot,
      float CastSpellTimeThreshold,
      string Champion,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      bool SpellStall,
      object PushLaneAbilities) // function
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;
        string ActionPerformed = default;

        // Variables locales pour la logique de PushLane

        bool result =
                // Sequence name :PushLane

                ClearUnitAIAttackTarget() &&
                // Sequence name :GotoUnlessInAssist
                (
                      TestAIEntityHasTask(
                        Self,
                        AITaskTopicType.ASSIST,
                        default,
                        default,
                        default,
                        true)
                      ||
                      // Sequence name :GotoTaskPosition
                      (
                        GetUnitAITaskPosition(out Vector3 TaskPosition)
                        && DistanceBetweenObjectAndPoint(out float DistanceToTaskPosition, Self, TaskPosition)
                        // Sequence name :GetToPosition
                        && (
                              TryPushLaneSequence(
                                out _CastSpellTimeThreshold,
                                out CurrentSpellCast,
                                out CurrentSpellCastTarget,
                                out _PreviousSpellCastTime,
                                out _SpellStall,
                                PushLaneAbilities,
                                Self, // just return false is useless
                                CastSpellTimeThreshold,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                PreviousSpellCastTime,
                                SpellStall)

                              // Sequence name :UseSummonerTeleport
                              || (
                                    GreaterFloat(DistanceToTaskPosition, 5000)
                                    && GetGameTime(out float GameTime)
                                    && GreaterFloat(GameTime, 180)
                                    && summonerTeleport.SummonerTeleport(Self, TaskPosition, TeleportSlot)
                              )
                              || IssueMoveToPositionOrder(TaskPosition)
                            )
                        )
                    )
                    && SetVarString(out ActionPerformed, "PushLane")
                ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        _ActionPerformed = ActionPerformed;

        return result;
    }
}

