using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class HelpKillChampionClass : AI_Characters
{
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
            Console.WriteLine("[TryKillChampionScoreModifier] procedureObject null");
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
            Console.WriteLine("[TryKillChampionAttackSequence] procedureObject null");
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
    public bool HelpKillChampion(





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



      Vector3 _SelfPosition,
      AttackableUnit Self,
      bool DeAggro,
      bool TargetValid,
      AttackableUnit Target,
      Vector3 TargetAcquiredPosition,
      int KillChampionScore,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PrevRetreatTime,
      Vector3 LastKnownPosition,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      int ExhaustSlot,
      int FlashSlot,
      int GhostSlot,
      int IgniteSlot,
      bool SpellStall,
      object KillChampionAttackSequence,
      //    AttackableUnit Self,

      //  bool IsDominionGameMode,
      object KillChampionScoreModifier,

      int SurgeSlot,
      AttackableUnit EventTarget

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
        Vector3 SelfPosition = Vector3.Zero;
        AttackableUnit _Self = Self;

        AttackableUnit TempTarget = default;

        bool result =
                    // Sequence name :KillChampion

                    TestUnitCondition(
                          EventTarget,
                          true) &&
                    GetDistanceBetweenUnits(
                          out DistanceToEventTarget,
                          Self,
                          EventTarget) &&
                    LessFloat(
                          DistanceToEventTarget,
                          1500) &&
                    GetUnitPosition(
                          out EventTargetPosition,
                          EventTarget) &&
                    CountUnitsInTargetArea(
                          out NearbyEnemyTurrets,
                          Self,
                          EventTargetPosition,
                          1000,
                          AffectEnemies | AffectTurrets,
                          "") &&
                    NearbyEnemyTurrets == 0 &&
                    CountUnitsInTargetArea(
                          out NumEnemyChamps,
                          Self,
                          SelfPosition,
                          900,
                          AffectEnemies | AffectHeroes,
                          "") &&
                    CountUnitsInTargetArea(
                          out NumFriendlyChamps,
                          Self,
                          SelfPosition,
                          900,
                          AffectFriends | AffectHeroes | AlwaysSelf,
                          "") &&
                    SubtractInt(
                          out NumChampDifferential,
                          NumEnemyChamps,
                          NumFriendlyChamps) &&
                    LessEqualInt(
                          NumChampDifferential,
                          0) &&
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
                    SetVarBool(
                          out AggressiveState,
                          false) &&
                    SetVarAttackableUnit(
                          out TempTarget,
                          EventTarget) &&
                          // Sequence name :KillChampionBlock

                          SetVarInt(
                                out KillChampionScore,
                                5) &&
                          // Sequence name :MaskFailure
                          (
                                      // Sequence name :Selector

                                      // Sequence name :TooCloseToEnemyBase?
                                      (
                                            GetUnitTeam(
                                                  out Team,
                                                  Self) &&
                                            // Sequence name :SetEnemyBasePoint
                                            (
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
                                                  out KillChampionScore,
                                                  0)
                                      ) ||
                                      TryKillChampionScoreModifier(
                                          out _KillChampionScore,
                                          KillChampionScoreModifier,
                                          Self,
                                          TempTarget,
                                          KillChampionScore)
                                 || MaskFailure()
                          ) &&
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
                                // Sequence name :SummonerSpells
                                (
                                      GreaterInt(
                                            KillChampionScore,
                                            5) &&
                                      // Sequence name :Difficulty
                                      (
                                            TestEntityDifficultyLevel(
                                                  true,
                                               EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) || TestEntityDifficultyLevel(
                                                  true,
                                                EntityDiffcultyType.DIFFICULTY_ADVANCED)
                                      ) &&
                                      GetUnitHealthRatio(
                                            out TargetHealthRatio,
                                            TempTarget) &&
                                      // Sequence name :UseSummonerSpells
                                      (
                                            // Sequence name :Exhaust
                                            (
                                                  LessFloat(
                                                        TargetHealthRatio,
                                                        0.3f) &&
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
                                                IsDominionGameMode) // IsDominionGameMode = false pour le mode normal
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
        _SelfPosition = SelfPosition;
        // __Self = _Self;


        return result;

    }
}

