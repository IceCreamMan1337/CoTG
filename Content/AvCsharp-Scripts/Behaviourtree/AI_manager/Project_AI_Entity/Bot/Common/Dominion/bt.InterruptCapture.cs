using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class InterruptCaptureClass : AI_Characters 
{
    protected bool TryKillChampionAttackSequence(
    out bool _IssuedAttack,
   out AttackableUnit _IssuedAttackTarget,
   out int CurrentSpellCast,
  out AttackableUnit CurrentSpellCastTarget,
  out float _PreviousSpellCastTime,
 out float _CastSpellTimeThreshold,
object procedureObject,
                       AttackableUnit Self,
                                    AttackableUnit TempTarget,
                                         int InverseKillChampionScore,
                                             bool IssuedAttack,
  AttackableUnit IssuedAttackTarget,
 int PreviousSpellCast,
 AttackableUnit PreviousSpellCastTarget,
  float CastSpellTimeThreshold,
  float PreviousSpellCastTime,
                                          int ExhaustSlot,
                                           int FlashSlot,
                                           int GhostSlot,
                                          int IgniteSlot,
bool IsDominionGameMode
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
             PreviousSpellCastTime);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                _IssuedAttack = (bool)outputs[0];
                _IssuedAttackTarget = (AttackableUnit)outputs[1];
                CurrentSpellCast = (int)outputs[2];
                CurrentSpellCastTarget = (AttackableUnit)outputs[3];
                _PreviousSpellCastTime = (float)outputs[4];
                _CastSpellTimeThreshold = (float)outputs[5];
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

    private SummonerGarrisonClass summonerGarrison = new SummonerGarrisonClass();

     public bool InterruptCapture(
      out string _ActionPerformed,
      out float __CastSpellTimeThreshold,
        out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
       out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,
          out float __PreviousSpellCastTime,
      out bool __SpellStall,

      AttackableUnit Self,
      float CastSpellTimeThreshold,
      string Champion,
      int ExhaustSlot,
      int GhostSlot,
      int IgniteSlot,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      bool SpellStall,
      int GarrisonSlot,
      float LastUseSpellTime,
      float CurrentGameTime,
      object KillChampionAttackSequence,
  //    AttackableUnit Target,
   //   int KillChampionScore,   
      int FlashSlot
      //bool IsDominionGameMode
         )
    {

        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
       float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
        string ActionPerformed = default;
            
        bool result =
              // Sequence name :InterruptCapture
              (
                    SetVarBool(
                          out CPBeingTaken,
                          false) &&
                    SetVarAttackableUnit(
                          out ToInterrupt,
                          Self) &&
                    GetAITaskFromEntity(
                          out DefendTask,
                          Self) &&
                    GetAITaskPosition(
                          out DefendPosition,
                          DefendTask) &&
                    GetUnitsInTargetArea(
                          out EnemyChampsByDefendPoint,
                          Self,
                          DefendPosition,
                          1000,
                          AffectEnemies | AffectHeroes) &&
                    ForEach(EnemyChampsByDefendPoint, EnemyChamp => (
                          // Sequence name :Sequence
                          (
                                TestUnitHasBuff(
                                      EnemyChamp,
                                      default,
                                      "OdinCaptureChannel",
                                      true) &&
                                SetVarAttackableUnit(
                                      out ToInterrupt,
                                      EnemyChamp) &&
                                SetVarBool(
                                      out CPBeingTaken,
                                      true)
                          ))
                    ) &&
                    CPBeingTaken == true &&
                    SetVarInt(
                          out KillChampionScore,
                          0) &&
                    SetVarBool(
                          out IsDominionGameMode,
                          true) &&
                    GetDistanceBetweenUnits(
                          out DistanceToInterrupt,
                          Self,
                          ToInterrupt) &&
                    // Sequence name :Selector
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "OdinCaptureChannel",
                                true)
                          ||
                          summonerGarrison.SummonerGarrison(
                                Self,
                                GarrisonSlot,
                                DefendPosition)
                          ||
                          // Sequence name :NeutralPoint
                          (
                                SubtractFloat(
                                      out TimePassed,
                                      CurrentGameTime,
                                      LastUseSpellTime) &&
                                GreaterEqualFloat(
                                      TimePassed,
                                      4) &&
                                GetUnitsInTargetArea(
                                      out CapturePoints,
                                      Self,
                                      DefendPosition,
                                      1200,
                                      AffectMinions | AffectNeutral | AffectUseable) &&
                                ForEach(CapturePoints, Point => (                                    // Sequence name :Sequence
                                      (
                                            GetUnitBuffCount(
                                                  out Count,
                                                  Point,
                                                  "OdinGuardianStatsByLevel") &&
                                            GreaterInt(
                                                  Count,
                                                  0)
                                      ))
                                ) &&
                                GetDistanceBetweenUnits(
                                      out DistanceToCapturePoint,
                                      Point,
                                      Self) &&
                                LessFloat(
                                      DistanceToCapturePoint,
                                      DistanceToInterrupt) &&
                                TestCanUseObject(
                                      Self,
                                      Point) &&
                                IssueUseObjectOrder(
                                      Point)
                          ) ||
                          // Sequence name :MoveToPoint
                          (
                                GreaterFloat(
                                      DistanceToInterrupt,
                                      700) &&
                                IssueMoveToUnitOrder(
                                      ToInterrupt)
                          ) ||
                         TryKillChampionAttackSequence(

                                                out _IssuedAttack,
                                                out _IssuedAttackTarget,

                                                out _CurrentSpellCast,
                                                out _CurrentSpellCastTarget,
                                                   out _CastSpellTimeThreshold,
                                                out _PreviousSpellCastTime,
                                                //  out SpellStall, 
                                                KillChampionAttackSequence,
                                                Self,
                                                ToInterrupt,
                                                KillChampionScore,
                                                IssuedAttack,
                                                IssuedAttackTarget,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTime,
                                                ExhaustSlot,
                                                FlashSlot,
                                                GhostSlot,
                                                IgniteSlot,
                                                //  SpellStall, 
                                                IsDominionGameMode)
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "InterruptCapture")

              );
         __IssuedAttack = _IssuedAttack;
         __IssuedAttackTarget = _IssuedAttackTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __SpellStall = _SpellStall;
         _ActionPerformed = ActionPerformed;
        return result;
      }
}

