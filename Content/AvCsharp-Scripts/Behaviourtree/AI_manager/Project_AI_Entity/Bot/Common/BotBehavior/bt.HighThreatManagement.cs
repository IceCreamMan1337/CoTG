using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class HighThreatManagementClass : AI_Characters 
{

    private SummonerExhaustClass summonerExhaust = new SummonerExhaustClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();
    private SummonerFlashClass summonerFlash = new SummonerFlashClass();
    private MicroRetreatClass microRetreat = new MicroRetreatClass();
    private HighThreatEvaluationClass highThreatEvaluation = new HighThreatEvaluationClass();

    protected bool TryHighThreatManagement(
             out int CurrentSpellCast,
     out AttackableUnit CurrentSpellCastTarget,
     out float _CastSpellTimeThreshold,
     out float _PreviousSpellCastTime,

     object procedureObject,
     AttackableUnit Self,
     Vector3 SelfPosition,
          float CastSpellTimeThreshold,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,

     float PreviousSpellCastTime,
     int ExhaustSlot,
     int GhostSlot


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
                                 Self,
                                 SelfPosition,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 PreviousSpellCastTime,
                                 ExhaustSlot,
                                 GhostSlot);

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
    public bool HighThreatManagement(
      

        out bool __TargetValid,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
       out float __PreviousSpellCastTime,
       out float __CastSpellTimeThreshold,
      out string _ActionPerformed,
      out bool __SpellStall,
    

      AttackableUnit Self,
      float DamageOverTime,
      float DamageRatioThreshold,
      float LowHealthPercentThreshold,
      bool TargetValid,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      Vector3 SelfPosition,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      int ExhaustSlot,
      int GhostSlot,
      string Champion,
      bool SpellStall,
      object HighThreatManagementSpells, //function
     // AttackableUnit Self,
      int FlashSlot,
      string PreviousActionPerformed
         )
    {
      
      float _CastSpellTimeThreshold = CastSpellTimeThreshold;
      float _PreviousSpellCastTime = PreviousSpellCastTime;
      int CurrentSpellCast = default;
      AttackableUnit CurrentSpellCastTarget = default;
      bool _SpellStall = SpellStall;
      bool _TargetValid = TargetValid;
        string ActionPerformed = default;


        bool result =
            // Sequence name :RetreatHighThreat
            (
                 highThreatEvaluation. HighThreatEvaluation(
                        out IsHighBurstbool,
                        out IsLowHP,
                        Self,
                        DamageOverTime,
                        DamageRatioThreshold,
                        LowHealthPercentThreshold) &&
                  // Sequence name :InHighThreatScenario
                  (
                        IsHighBurstbool == true &&
                        IsLowHP == true
                        &&// Sequence name :WasHighThreatEnemyStillNear
                        (
                              PreviousActionPerformed == "HighThreatManagement" &&
                              CountUnitsInTargetArea(
                                    out NearbyEnemies,
                                    Self,
                                    SelfPosition,
                                    1700,
                                    AffectEnemies | AffectHeroes,
                                    "") &&
                              GreaterInt(
                                    NearbyEnemies,
                                    0)
                        )
                  ) &&
                  SetVarBool(
                        out _TargetValid,
                        false) &&
                  // Sequence name :Management
                  (
                        TryHighThreatManagement(

                                 out CurrentSpellCast, 
                                 out CurrentSpellCastTarget,
                                 out _CastSpellTimeThreshold,
                                 out _PreviousSpellCastTime,
                               //  out _SpellStall, 
                                 HighThreatManagementSpells,
                                 Self, 
                                 SelfPosition, 
                                 CastSpellTimeThreshold, 
                                 PreviousSpellCast, 
                                 PreviousSpellCastTarget, 
                                 PreviousSpellCastTime, 
                                 ExhaustSlot, 
                                 GhostSlot
                                // SpellStall
                                 ) ||
                        // Sequence name :SummonerSpells
                        (
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              LessFloat(
                                    SelfHealthRatio,
                                    0.3f) &&
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
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    450,
                                    AffectEnemies | AffectHeroes) &&
                              // Sequence name :UseSummonerSpells
                              (
                                    // Sequence name :Exhaust
                                    (
                                          NotEqualInt(
                                                ExhaustSlot,
                                                -1) &&
                                         summonerExhaust.SummonerExhaust(
                                                Self,
                                                CurrentClosestTarget,
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
                                    // Sequence name :Flash
                                    (
                                          NotEqualInt(
                                                FlashSlot,
                                                -1) &&
                                          GetUnitAIClosestTargetInArea(
                                                out CurrentClosestTarget,
                                                Self,
                                                default,
                                                true,
                                                SelfPosition,
                                                700,
                                                AffectEnemies | AffectHeroes) &&
                                          TestUnitHasAnyBuffOfType(
                                                CurrentClosestTarget,
                                                BuffType.STUN,
                                                false) &&
                                          TestUnitHasAnyBuffOfType(
                                                CurrentClosestTarget,
                                                 BuffType.SNARE,
                                                false) &&
                                          TestUnitHasAnyBuffOfType(
                                                CurrentClosestTarget,
                                                BuffType.CHARM,
                                                false) &&
                                          TestUnitHasAnyBuffOfType(
                                                CurrentClosestTarget,
                                                 BuffType.SUPPRESSION,
                                                false) &&
                                          TestUnitHasAnyBuffOfType(
                                                CurrentClosestTarget,
                                               BuffType.FEAR,
                                                false) &&
                                          GetUnitAIBasePosition(
                                                out BasePosition,
                                                Self) &&
                                          DistanceBetweenObjectAndPoint(
                                                out SelfDistanceToBase,
                                                Self,
                                                BasePosition) &&
                                          DistanceBetweenObjectAndPoint(
                                                out TargetDistanceToBase,
                                                CurrentClosestTarget,
                                                BasePosition) &&
                                          LessFloat(
                                                SelfDistanceToBase,
                                                TargetDistanceToBase) &&
                                     summonerFlash.SummonerFlash(
                                                Self,
                                                FlashSlot,
                                                CurrentClosestTarget,
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :MicroRetreat
                        (
                              // Sequence name :HighBurstOrBeingChased
                              (
                                    IsHighBurstbool == true &&                    
                                    // Sequence name :WasHighThreatEnemyStillNear
                                    (
                                          PreviousActionPerformed == "HighThreatManagement" &&
                                          CountUnitsInTargetArea(
                                                out NearbyEnemies,
                                                Self,
                                                SelfPosition,
                                                1700,
                                                AffectEnemies | AffectHeroes,
                                                "") &&
                                          GreaterInt(
                                                NearbyEnemies,
                                                0)
                                    )
                              ) &&
                            microRetreat.MicroRetreat(
                                    Self)
                        )
                  ) &&
                  SetVarString(
                        out _ActionPerformed,
                        "HighThreatManagement")

            );


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;
        __TargetValid = _TargetValid;
        _ActionPerformed = ActionPerformed;


        return result;
    }
}

