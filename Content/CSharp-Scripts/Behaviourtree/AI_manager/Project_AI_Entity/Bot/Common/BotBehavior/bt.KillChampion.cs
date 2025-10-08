using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class KillChampionClass : AI_Characters
{

    private KillChampionAcquisitionClass killChampionAcquisition = new();
    private KillChampionEvaluatorClass killChampionEvaluator = new();
    private SummonerExhaustClass summonerExhaust = new();
    private SummonerGhostClass summonerGhost = new();
    private SummonerIgniteClass summonerIgnite = new();
    private SummonerSurgeClass summonerSurge = new();


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
        out AttackableUnit _IssuedAttackTarget,
        out bool _IssuedAttack,
        out AttackableUnit CurrentSpellCastTarget,
        out int CurrentSpellCast,
        out float _CastSpellTimeThreshold,
        out float _PreviousSpellCastTime,
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
             IsDominionGameMode);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {

                _IssuedAttackTarget = (AttackableUnit)outputs[0];
                _IssuedAttack = (bool)outputs[1];

                CurrentSpellCastTarget = (AttackableUnit)outputs[2];
                CurrentSpellCast = (int)outputs[3];

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
    public bool KillChampion(

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
      object KillChampionAttackSequence,

      // bool IsDominionGameMode,
      object KillChampionScoreModifier,

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

        AttackableUnit TempTarget = default;

        bool result =
                    // Sequence name :KillChampion

                    GetGameTime(
                          out CurrentTime) &&
                    SubtractFloat(
                          out TimeDiff,
                          CurrentTime,
                          PrevRetreatTime) &&
                    SetVarBool(
                          out Run,
                          true) &&
                    GetUnitAttackRange(
                          out AttackRange,
                          Self) &&
                    SetVarBool(
                          out IsDominionGameMode,
                          false) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Sequence
                          (
                                SetVarBool(
                                      out AggressiveState,
                                      false) &&
                                GetUnitLevel(
                                      out Level,
                                      Self) &&
                                GreaterEqualInt(
                                      Level,
                                      2) &&
                                SetVarBool(
                                      out AggressiveState,
                                      true)
                          )
                          || MaskFailure()
                    ) &&
                    // Sequence name :RangedOrMelee
                    (
                          // Sequence name :Ranged
                          (
                                GreaterFloat(
                                      AttackRange,
                                      200) &&
                                SetVarBool(
                                      out _IsMelee,
                                      false)
                          ) ||
                          // Sequence name :Melee
                          (
                                LessEqualFloat(
                                      AttackRange,
                                      200) &&
                                SetVarBool(
                                      out _IsMelee,
                                      true)
                          )
                    ) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :DontRe-EvaluateIfNearTower
                          (
                                LessFloat(
                                      TimeDiff,
                                      2) &&
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
                          ) || MaskFailure()
                    ) &&
                      // Sequence name :KillChampionBlock

                      DebugAction("KillChampionAcquisition test ") &&
                          Run == true &&
                        killChampionAcquisition.KillChampionAcquisition(
                                out TempTarget,
                                Self,
                                SelfPosition,
                                TargetValid,
                                Target,
                                LastKnownPosition,
                                TargetAcquisitionRange) &&
                               DebugAction("KillChampionAcquisition passed ") &&
                          SetVarInt(
                                out PrevKillChampionScore,
                                KillChampionScore) &&
                          SetVarInt(
                                out _KillChampionScore,
                                0) &&
                         killChampionEvaluator.KillChampionEvaluator(
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
                                AggressiveState,
                                _IsMelee) &&

                                        DebugAction("KillChampionEvaluator passed + " + _KillChampionScore) &&
                                // Sequence name :MaskFailure

                                // Sequence name :Selector
                                (
                                      // Sequence name :TooCloseToEnemyBase?
                                      (
                                            GetUnitTeam(
                                                  out Team,
                                                  Self) &&
                                            // Sequence name :SetEnemyBasePoint
                                            (
                                                  //need edit that for TT 

                                                  // Sequence name :ChaosTeam
                                                  (
                                                        Team == TeamId.TEAM_CHAOS &&
                                                        MakeVector(
                                                              out EnemyBase,
                                                              27,
                                                              175,
                                                              284)
                                                  ) ||
                                                  // Sequence name :OrderTeam
                                                  (
                                                        Team == TeamId.TEAM_ORDER &&
                                                        MakeVector(
                                                              out EnemyBase,
                                                              13955,
                                                              176,
                                                              14215)
                                                  )
                                            ) &&
                                            DistanceBetweenObjectAndPoint(
                                                  out TargetDistanceToBase,
                                                  TempTarget,
                                                  EnemyBase) &&
                                            LessFloat(
                                                  TargetDistanceToBase,
                                                  1000) &&
                                            SetVarInt(
                                                  out _KillChampionScore,
                                                  0)
                                      ) ||


                                    TryKillChampionScoreModifier(
                                          out _KillChampionScore,
                                          KillChampionScoreModifier,
                                          Self,
                                          TempTarget,
                                          _KillChampionScore)

                                    || MaskFailure()
                                )
                           &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :IfRetreat_setTime
                                (
                                      _KillChampionScore == 0 &&
                                      GreaterInt(
                                            PrevKillChampionScore,
                                            0) &&
                                      SetVarFloat(
                                            out PrevRetreatTime,
                                            CurrentTime)
                                ) || MaskFailure()
                          ) &&


                           DebugAction("GreaterEqualInt KillChampionScore + " + _KillChampionScore) &&
                          GreaterEqualInt(
                                _KillChampionScore,
                                5)
                          && DebugAction("GreaterEqualInt SUCCESS + " + _KillChampionScore)
                          &&
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
                                      && DebugAction("TargetIsInvisible")
                                ) ||
                                // Sequence name :SummonerSpells
                                (
                                      GreaterInt(
                                            KillChampionScore,
                                            5) &&
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
                                      GetUnitHealthRatio(
                                            out TargetHealthRatio,
                                            TempTarget) &&
                                      // Sequence name :UseSummonerSpells
                                      (
                                              (DebugAction("UseSummonerSpells") &&
                                                  // Sequence name :Exhaust

                                                  LessFloat(
                                                        TargetHealthRatio,
                                                        0.3f) &&
                                                  NotEqualInt(
                                                        ExhaustSlot,
                                                        -1) &&
                                               summonerExhaust.SummonerExhaust(
                                                        Self,
                                                        TempTarget,
                                                        ExhaustSlot))
                                             ||
                                            // Sequence name :Ghost
                                            (
                                                  LessFloat(
                                                        TargetHealthRatio,
                                                        0.2f) &&
                                                  NotEqualInt(
                                                        GhostSlot,
                                                        -1) &&
                                               summonerGhost.SummonerGhost(
                                                        Self,
                                                        GhostSlot)
                                            ) ||
                                            // Sequence name :Ignite
                                            (
                                                  LessFloat(
                                                        TargetHealthRatio,
                                                        0.3f) &&
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
                                                  LessFloat(
                                                        TargetHealthRatio,
                                                        0.4f) &&
                                                  NotEqualInt(
                                                        SurgeSlot,
                                                        -1) &&
                                               summonerSurge.SummonerSurge(
                                                        Self,
                                                        SurgeSlot)
                                            )
                                      )
                                ) ||

                                   (DebugAction("TryKillChampionAttackSequence") &&

                              TryKillChampionAttackSequence(


                                                out _IssuedAttackTarget,
                                                  out _IssuedAttack,
                                                         out _CurrentSpellCastTarget,
                                                out _CurrentSpellCast,

                                                   out _CastSpellTimeThreshold,
                                                out _PreviousSpellCastTime,
                                                //  out SpellStall, 
                                                KillChampionAttackSequence,
                                                Self,
                                                TempTarget,
                                                _KillChampionScore,
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
                                                IsDominionGameMode))  // IsDominionGameMode = false pour le mode normal
                          ) &&
                          SetVarBool(
                                out DeAggro,
                                false) &&
                          SetVarBool(
                                out TargetValid,
                                true) &&
                          SetVarVector(
                                out TargetAcquiredPosition,
                                SelfPosition) &&
                          SetVarAttackableUnit(
                                out Target,
                                TempTarget)
                     &&
                    SetVarString(
                          out ActionPerformed,
                          "KillChampion")

              ;

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

        return result;

    }
}

