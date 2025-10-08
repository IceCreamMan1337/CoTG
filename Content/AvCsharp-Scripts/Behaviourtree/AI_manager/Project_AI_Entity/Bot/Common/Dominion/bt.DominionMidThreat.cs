using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionMidThreatClass : AI_Characters 
{

    private DominionRetreatFromEnemyCapturePointClass dominionRetreatFromEnemyCapturePoint = new DominionRetreatFromEnemyCapturePointClass();

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
    public bool DominionMidThreat(
           
     
     
     



      out string _ActionPerformed,
       out float __CastSpellTimeThreshold,
       out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,
       out float __PreviousSpellCastTime,
           out bool __SpellStall,
            out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,

      out float __RetreatFromCP_RetreatUntilTime,
      out Vector3 __RetreatSafePosition,
      out float __RetreatPositionStartTime,


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
      float RetreatFromCP_RetreatUntilTime,
      Vector3 RetreatSafePosition,
      float RetreatPositionStartTime,
      object KillChampionAttackSequence,
   //   AttackableUnit Target,
     // int KillChampionScore,
      int FlashSlot
         //,bool IsDominionGameMode
         
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
        float _RetreatFromCP_RetreatUntilTime = RetreatFromCP_RetreatUntilTime;
        Vector3 _RetreatSafePosition = RetreatSafePosition;
        float _RetreatPositionStartTime = RetreatPositionStartTime;

      bool result =
            // Sequence name :Sequence
            (
                  GetUnitTeam(
                        out SelfTeam, 
                        Self) &&
                  GetAITaskFromEntity(
                        out Task, 
                        Self) &&
                  GetAITaskPosition(
                        out TaskPosition, 
                        Task) &&
                  GetUnitsInTargetArea(
                        out CPCollection, 
                        Self, 
                        TaskPosition, 
                        300, 
                        AffectMinions|AffectUseable) &&
                  ForEach(CPCollection, Point => (       
                  SetVarAttackableUnit(
                              out _CapturePoint, 
                              Point)
                  )) &&
                  GetDistanceBetweenUnits(
                        out DistanceToCP, 
                        Self,
                        _CapturePoint) &&
                  GetUnitTeam(
                        out CPTeam,
                        _CapturePoint) &&
                  // Sequence name :TargetAcquisition
                  (
                        // Sequence name :NearSelf
                        (
                              GetUnitAIClosestTargetInArea(
                                    out ToInterrupt, 
                                    Self, 
                                   default, 
                                    true, 
                                    SelfPosition, 
                                    900, 
                                    AffectEnemies|AffectHeroes)
                        ) ||
                        // Sequence name :NearCapturePoint
                        (
                              GetUnitsInTargetArea(
                                    out EnemyChampsByPoint, 
                                    Self, 
                                    TaskPosition, 
                                    1200, 
                                    AffectEnemies | AffectHeroes
                                    ) &&
                              GetCollectionCount(
                                    out EnemyChampCount, 
                                    EnemyChampsByPoint) &&
                              GreaterInt(
                                    EnemyChampCount, 
                                    0) &&
                              ForEach(EnemyChampsByPoint, EnemyChamp => (
                                    SetVarAttackableUnit(
                                          out ToInterrupt, 
                                          EnemyChamp) 
                              ) )
                        )
                  ) &&
                  SetVarInt(
                        out KillChampionScore, 
                        0) &&
                  SetVarBool(
                        out IsDominionGameMode, 
                        true) &&
                  // Sequence name :Selector
                  (
                        // Sequence name :DefendingPoint
                        (
                              CPTeam == SelfTeam &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :AttackIfNearPointOrReallyFarAway
                                    (
                                          // Sequence name :CloseToPointOrReallyFar
                                          (
                                                LessFloat(
                                                      DistanceToCP, 
                                                      650)                                      
                                                ||
                                                GreaterFloat(
                                                      DistanceToCP, 
                                                      1200)
                                          ) &&
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
                                                InverseKillChampionScore,
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
                                    ) ||
                                    // Sequence name :ElseMoveTowardPoint
                                    (
                                          IssueMoveToUnitOrder(
                                                _CapturePoint)
                                    )
                              )
                        ) ||
                        // Sequence name :NeutralPoint
                        (
                              CPTeam == TeamId.TEAM_NEUTRAL   &&
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
                                                InverseKillChampionScore,
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
                        ) ||
                        // Sequence name :EnemyPoint
                        (
                              // Sequence name :Selector
                              (
                                    // Sequence name :Sequence
                                    (
                                          LessFloat(
                                                DistanceToCP, 
                                                650) &&
                                          GetUnitPosition(
                                                out SelfPosition, 
                                                Self) &&
                                       dominionRetreatFromEnemyCapturePoint.   DominionRetreatFromEnemyCapturePoint(
                                                out _RetreatFromCP_RetreatUntilTime, 
                                                out ActionPerformed, 
                                                out _RetreatSafePosition, 
                                                out _RetreatPositionStartTime, 
                                                Self, 
                                                SelfPosition, 
                                                RetreatFromCP_RetreatUntilTime, 
                                                RetreatSafePosition, 
                                                RetreatPositionStartTime)
                                    ) ||
                                    // Sequence name :Sequence
                                    (
                                          DistanceBetweenObjectAndPoint(
                                                out EnemyDistanceToCP, 
                                                ToInterrupt, 
                                                TaskPosition) &&
                                          GreaterFloat(
                                                EnemyDistanceToCP, 
                                                750) &&
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
                                                InverseKillChampionScore,
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
                                    )
                              )
                        )
                  ) &&
                  SetVarString(
                        out ActionPerformed, 
                        "MidThreat")

            );
         __IssuedAttack = _IssuedAttack;
         __IssuedAttackTarget = _IssuedAttackTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __SpellStall = _SpellStall;
         _ActionPerformed = ActionPerformed;
         __RetreatFromCP_RetreatUntilTime = _RetreatFromCP_RetreatUntilTime;
         __RetreatSafePosition = _RetreatSafePosition;
         __RetreatPositionStartTime = _RetreatPositionStartTime;
        return result;
      }
}

