using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionKillChampionClass : AI_Characters 
{
    private KillChampionAcquisitionClass killChampionAcquisition = new KillChampionAcquisitionClass();
    private DominionKillChampionEvaluatorClass dominionKillChampionEvaluator = new DominionKillChampionEvaluatorClass();
    private SummonerExhaustClass summonerExhaust = new SummonerExhaustClass(); 
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();
    private SummonerIgniteClass summonerIgnite = new SummonerIgniteClass();
    private SummonerSurgeClass summonerSurge = new SummonerSurgeClass();
    protected bool TryKillChampionScoreModifier(
 out int _KillChampionScore,
    object procedureObject,
   AttackableUnit Self,
   AttackableUnit TempTarget,
   int KillChampionScore
)
    {
        _KillChampionScore = KillChampionScore;


        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,
             Self,
             TempTarget,
             KillChampionScore
          );

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {
                _KillChampionScore = (int)outputs[0];

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
    public bool DominionKillChampion(
   
     
      
     



      out bool __DeAggro,
      out bool __TargetValid,
      out AttackableUnit __Target,
      out Vector3 __TargetAcquiredPosition,
       out int __KillChampionScore,
        out AttackableUnit __IssuedAttackTarget,
           out bool __IssuedAttack,
             out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,

      out float __PrevRetreatTime,
         out float __PreviousSpellCastTime,
         out float __CastSpellTimeThreshold,


      out bool __SpellStall,
      out string _ActionPerformed,
      out AttackableUnit __PrevKillChampTarget,
      out float __PrevKillChampTargetHealth,
      out float __PrevKillChampDamageTime,




      float StrengthRatioOverTime,
      Vector3 SelfPosition,
      AttackableUnit Self,
      bool DeAggro,
      bool TargetValid,
      AttackableUnit Target,
      Vector3 TargetAcquiredPosition,
      int KillChampionScore,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      float DamageOverTime,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PrevRetreatTime,
      Vector3 LastKnownPosition,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      string Champion,
      float TargetAcquisitionRange,
      float ValueOfAbility0,
      float ValueOfAbility1,
      float ValueOfAbility2,
      float ValueOfAbility3,
      int CC_AbilityNumber,
      float AbilityReadinessMinScore,
      float AbilityReadinessMaxScore,
      float AbilityNotReadyMinScore,
      float AbilityNotReadyMaxScore,
      int ExhaustSlot,
      int FlashSlot,
      int GhostSlot,
      int IgniteSlot,
      bool SpellStall,
      bool AggressiveState,
      AttackableUnit PrevKillChampTarget,
      float PrevKillChampTargetHealth,
      float PrevKillChampDamageTime,
      object KillChampionAttackSequence, //function
   
     // bool IsDominionGameMode,
      object KillChampionScoreModifier, //function 
    //  
      int SurgeSlot
         )
    {
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
       bool _SpellStall = SpellStall;
        int _KillChampionScore = KillChampionScore;
        bool _DeAggro = DeAggro;
       bool _TargetValid = TargetValid;
        AttackableUnit _Target = Target;
        Vector3 _TargetAcquiredPosition = TargetAcquiredPosition;
        float _PrevRetreatTime = PrevRetreatTime;
        string ActionPerformed = default;
        AttackableUnit _PrevKillChampTarget = PrevKillChampTarget;
        float _PrevKillChampTargetHealth = PrevKillChampTargetHealth;
       float _PrevKillChampDamageTime = PrevKillChampDamageTime;


        AttackableUnit TempTarget = default;

        bool result =
              // Sequence name :KillChampion
              (
                    GetGameTime(
                          out CurrentTime) &&
                    SubtractFloat(
                          out TimeDiff,
                          CurrentTime,
                          PrevRetreatTime) &&
                    SetVarBool(
                          out IsDominionGameMode,
                          true) &&
                    SetVarBool(
                          out Run,
                          true) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :DontRe-EvaluateIfNearTower
                          (
                                LessFloat(
                                      TimeDiff,
                                      3) &&
                                // Sequence name :Selector
                                (
                                      // Sequence name :Summoner'sRift
                                      (
                                            CountUnitsInTargetArea(
                                                  out TurretCount,
                                                  Self,
                                                  SelfPosition,
                                                  1500,
                                                  AffectEnemies | AffectTurrets,
                                                  "") &&
                                            GreaterInt(
                                                  TurretCount,
                                                  0)
                                      ) ||
                                      // Sequence name :Dominion
                                      (
                                            CountUnitsInTargetArea(
                                                  out TurretCount,
                                                  Self,
                                                  SelfPosition,
                                                  1500,
                                                  AffectEnemies | AffectMinions | AffectUseable,
                                                  "OdinGuardianStatsByLevel") &&
                                            GreaterInt(
                                                  TurretCount,
                                                  0)
                                      )
                                ) &&
                                SetVarBool(
                                      out Run,
                                      false)
                          )
                    ) &&
                    // Sequence name :KillChampionEval
                    (
                          Run == true &&
                          killChampionAcquisition.KillChampionAcquisition(
                                out TempTarget,
                                Self,
                                SelfPosition,
                                TargetValid,
                                Target,
                                LastKnownPosition,
                                TargetAcquisitionRange) &&
                          SetVarInt(
                                out PrevKillChampionScore,
                                KillChampionScore) &&
                          SetVarInt(
                                out _KillChampionScore,
                                0) &&
                          GetUnitAttackRange(
                                out AttackRange,
                                Self) &&
                          // Sequence name :KillChampionEvaluator
                          (
                                // Sequence name :InAggressiveState?
                                (
                                      AggressiveState == true &&
                                      // Sequence name :Dominion
                                      (
                                            CountUnitsInTargetArea(
                                                  out TurretCount,
                                                  Self,
                                                  SelfPosition,
                                                  1200,
                                                  AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable,
                                                  "OdinGuardianStatsByLevel") &&
                                            GreaterInt(
                                                  TurretCount,
                                                  0)
                                      ) &&
                                      SetVarInt(
                                            out KillChampionScore,
                                            10)
                                ) ||
                               dominionKillChampionEvaluator.DominionKillChampionEvaluator(
                                      out _KillChampionScore,
                                      StrengthRatioOverTime,
                                      Self,
                                      SelfPosition,
                                      DamageOverTime,
                                      TempTarget,
                                     ValueOfAbility0,
                                     ValueOfAbility1,
                                     ValueOfAbility2,
                                     ValueOfAbility3,
                                      CC_AbilityNumber,
                                      AbilityReadinessMinScore,
                                      AbilityReadinessMaxScore,
                                      AbilityNotReadyMinScore,
                                      AbilityNotReadyMaxScore,
                                      AggressiveState)
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Selector
                                (
                                      // Sequence name :TooCloseToEnemyBase?
                                      (
                                            GetUnitTeam(
                                                  out Team,
                                                  Self) &&
                                            // Sequence name :SetEnemyBaseEntrancePoints
                                            (
                                                  // Sequence name :ChaosTeam
                                                  (
                                                        Team == TeamId.TEAM_CHAOS &&
                                                        MakeVector(
                                                              out UpperBaseEntrance,
                                                              1606,
                                                              -189,
                                                              5893) &&
                                                        MakeVector(
                                                              out LowerBaseEntrance,
                                                              2359,
                                                              -187,
                                                              3314)
                                                  ) ||
                                                  // Sequence name :OrderTeam
                                                  (
                                                        Team == TeamId.TEAM_ORDER &&
                                                        MakeVector(
                                                              out UpperBaseEntrance,
                                                              12315,
                                                              -189,
                                                              5881) &&
                                                        MakeVector(
                                                              out LowerBaseEntrance,
                                                              11537,
                                                              -187,
                                                              3325)
                                                  )
                                            ) &&
                                            DistanceBetweenObjectAndPoint(
                                                  out TargetDistanceToUpperBaseEntrance,
                                                  TempTarget,
                                                  UpperBaseEntrance) &&
                                            DistanceBetweenObjectAndPoint(
                                                  out TargetDistanceToLowerBaseEntrance,
                                                  TempTarget,
                                                  LowerBaseEntrance) &&
                                            // Sequence name :TooCloseToEitherEntrance
                                            (
                                                  LessFloat(
                                                        TargetDistanceToUpperBaseEntrance,
                                                        300)
                                                  ||
                                                  LessFloat(
                                                        TargetDistanceToLowerBaseEntrance,
                                                        300)
                                            ) &&
                                            SetVarInt(
                                                  out _KillChampionScore,
                                                  0)
                                      ) ||
                                      // Sequence name :FarFromPointWithHighHealthRatio?
                                      (
                                            KillChampionScore == 0 &&
                                            GetUnitAITaskPosition(
                                                  out TaskPosition) &&
                                            DistanceBetweenObjectAndPoint(
                                                  out DistanceToTaskPosition,
                                                  Self,
                                                  TaskPosition) &&
                                            GreaterFloat(
                                                  DistanceToTaskPosition,
                                                  1600) &&
                                            GetUnitHealthRatio(
                                                  out SelfHealthRatio,
                                                  Self) &&
                                            GreaterFloat(
                                                  SelfHealthRatio,
                                                  0.65f) &&
                                            SetVarInt(
                                                  out _KillChampionScore,
                                                  5)
                                      ) ||
                                    TryKillChampionScoreModifier(
                                          out _KillChampionScore, 
                                          KillChampionScoreModifier,
                                          Self, 
                                          TempTarget, 
                                          KillChampionScore)
                                )
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Selector
                                (
                                      // Sequence name :IfPrevKillChampTarget!=TempTarget
                                      (
                                            NotEqualUnit(
                                                  PrevKillChampTarget,
                                                  TempTarget) &&
                                            SetVarAttackableUnit(
                                                  out _PrevKillChampTarget,
                                                  TempTarget) &&
                                            GetGameTime(
                                                  out _PrevKillChampDamageTime) &&
                                            GetUnitCurrentHealth(
                                                  out _PrevKillChampTargetHealth,
                                                  TempTarget)
                                      ) ||
                                      // Sequence name :Else
                                      (
                                            GetUnitCurrentHealth(
                                                  out KillChampTargetHealth,
                                                  TempTarget) &&
                                            // Sequence name :Selector
                                            (
                                                  // Sequence name :IfKillChampTargetHealth&lt,PrevKillChampTargetHealth
                                                  (
                                                        LessFloat(
                                                              KillChampTargetHealth,
                                                              PrevKillChampTargetHealth) &&
                                                        GetGameTime(
                                                              out _PrevKillChampDamageTime) &&
                                                        SetVarFloat(
                                                              out _PrevKillChampTargetHealth,
                                                              KillChampTargetHealth)
                                                  ) ||
                                                  // Sequence name :Else
                                                  (
                                                        SubtractFloat(
                                                              out DamageTimeDiff,
                                                              CurrentTime,
                                                              PrevKillChampDamageTime) &&
                                                        SetVarFloat(
                                                              out AntiLeashThreshold,
                                                              4) &&
                                                        SetVarFloat(
                                                              out DisableAntiLeashTime,
                                                              6) &&
                                                        // Sequence name :Selector
                                                        (
                                                              // Sequence name :IfDamageTimeDiff&lt,AntiLeashThreshold
                                                              (
                                                                    LessFloat(
                                                                          DamageTimeDiff,
                                                                          AntiLeashThreshold) &&
                                                                    SetVarFloat(
                                                                          out _PrevKillChampTargetHealth,
                                                                          KillChampTargetHealth)
                                                              ) ||
                                                              // Sequence name :IfDamageTimeDiff&gt,DisableAntiLeashTime,ResetLeashCheck
                                                              (
                                                                    GreaterFloat(
                                                                          DamageTimeDiff,
                                                                          DisableAntiLeashTime) &&
                                                                    SetVarAttackableUnit(
                                                                          out _PrevKillChampTarget,
                                                                          Self)
                                                              ) ||
                                                              // Sequence name :ElseBreakLeash!
                                                              (
                                                                    SetVarInt(
                                                                          out _KillChampionScore,
                                                                          0)
                                                              )
                                                        )
                                                  )
                                            )
                                      )
                                )
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :IfRetreat_setTime
                                (
                                      KillChampionScore == 0 &&
                                      GreaterInt(
                                            PrevKillChampionScore,
                                            0) &&
                                      SetVarFloat(
                                            out _PrevRetreatTime,
                                            CurrentTime)
                                )
                          ) &&
                          GreaterEqualInt(
                                KillChampionScore,
                                5) &&
                          // Sequence name :EitherAttackOrGotoLastKnownPosition
                          (
                                // Sequence name :TargetIsInvisible
                                (
                                      TargetValid == true &&
                                      TestUnitIsVisibleToTeam(
                                            Self,
                                            TempTarget,
                                            false) &&
                                      TempTarget == Target &&
                                      IssueMoveToPositionOrder(
                                            LastKnownPosition)
                                ) ||
                                // Sequence name :Attack
                                (
                                      // Sequence name :Beginner
                                      (
                                            TestEntityDifficultyLevel(
                                                  true,
                                                EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                            SubtractInt(
                                                  out InverseKillChampionScore,
                                                  10,
                                                  KillChampionScore) &&
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
                                                TempTarget, 
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
                                      // Sequence name :NotBeginner
                                      (
                                            // Sequence name :Difficulty
                                            (
                                                  TestEntityDifficultyLevel(
                                                        true,
                                                     EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                                                  ||
                                                  TestEntityDifficultyLevel(
                                                        true,
                                                        EntityDiffcultyType.DIFFICULTY_ADVANCED)
                                            ) &&
                                            // Sequence name :Action
                                            (
                                                  // Sequence name :SummonerSpells
                                                  (
                                                        GreaterInt(
                                                              KillChampionScore,
                                                              5) &&
                                                        GetUnitHealthRatio(
                                                              out TargetHealthRatio,
                                                              TempTarget) &&
                                                        LessFloat(
                                                              TargetHealthRatio,
                                                              0.3f) &&
                                                        // Sequence name :UseSummonerSpells
                                                        (
                                                              // Sequence name :Exhaust
                                                              (
                                                                    NotEqualInt(
                                                                          ExhaustSlot,
                                                                          -1) &&
                                                                   summonerExhaust.SummonerExhaust(
                                                                          Self,
                                                                          TempTarget,
                                                                          ExhaustSlot)
                                                              ) ||
                                                              // Sequence name :Ghost
                                                              (
                                                                    NotEqualInt(
                                                                          GhostSlot,
                                                                          -1) &&
                                                                   summonerGhost.SummonerGhost(
                                                                          Self,
                                                                          GhostSlot)
                                                              ) ||
                                                              // Sequence name :Ignite
                                                              (
                                                                    NotEqualInt(
                                                                          IgniteSlot,
                                                                          -1) &&
                                                                   summonerIgnite.SummonerIgnite(
                                                                          Self,
                                                                          TempTarget,
                                                                          IgniteSlot)
                                                              ) ||
                                                              // Sequence name :Surge
                                                              (
                                                                    NotEqualInt(
                                                                          SurgeSlot,
                                                                          -1) &&
                                                                 summonerSurge.SummonerSurge(
                                                                          Self,
                                                                          SurgeSlot)
                                                              )
                                                        )
                                                  ) ||
                                                   TryKillChampionAttackSequence(
                                                        out _IssuedAttack,
                                                      out _IssuedAttackTarget,

                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                            out _CastSpellTimeThreshold,
                                                      out _PreviousSpellCastTime,


                                                      KillChampionAttackSequence,
                                                      Self,
                                                      TempTarget,
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

                                                      IsDominionGameMode)
                                            )
                                      )
                                )
                          ) &&
                          SetVarBool(
                                out _DeAggro,
                                false) &&
                          SetVarBool(
                                out _TargetValid,
                                true) &&
                          SetVarVector(
                                out _TargetAcquiredPosition,
                                SelfPosition) &&
                          SetVarAttackableUnit(
                                out Target,
                                TempTarget)
                    ) &&
                    SetVarString(
                          out _ActionPerformed,
                          "KillChampion")
               
              );

         __IssuedAttack = _IssuedAttack;
         __IssuedAttackTarget = _IssuedAttackTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __SpellStall = _SpellStall;
         __KillChampionScore = _KillChampionScore;
         __DeAggro = _DeAggro;
         __TargetValid = _TargetValid;
         __Target = _Target;
         __TargetAcquiredPosition = _TargetAcquiredPosition;
         __PrevRetreatTime = _PrevRetreatTime;
         _ActionPerformed = ActionPerformed;
         __PrevKillChampTarget = _PrevKillChampTarget;
         __PrevKillChampTargetHealth = _PrevKillChampTargetHealth;
         __PrevKillChampDamageTime = _PrevKillChampDamageTime;
        return result;
      }
}

