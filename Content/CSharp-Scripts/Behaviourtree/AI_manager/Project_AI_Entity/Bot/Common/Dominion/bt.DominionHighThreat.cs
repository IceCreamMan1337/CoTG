using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class DominionHighThreatClass : AI_Characters
{

    private HighThreatEvaluationClass highThreatEvaluation = new();
    private SummonerExhaustClass summonerExhaust = new();
    private SummonerGhostClass summonerGhost = new();
    private SummonerFlashClass summonerFlash = new();
    private DominionRetreatClass dominionRetreat = new();
    public bool DominionHighThreat(

     out bool __TargetValid,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
       out float __CastSpellTimeThreshold,
         out bool __SpellStall,
     out string _ActionPerformed,
     out Vector3 __RetreatSafePosition,
     out float __RetreatPositionStartTime,
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
     Vector3 RetreatSafePosition,
     float RetreatPositionStartTime,
     object HighThreatManagementSpells, //function
     int FlashSlot
        )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
        bool _TargetValid = TargetValid;
        string ActionPerformed = default;
        Vector3 _RetreatSafePosition = RetreatSafePosition;
        float _RetreatPositionStartTime = RetreatPositionStartTime;

        bool result =
                   // Sequence name :RetreatHighThreat

                   highThreatEvaluation.HighThreatEvaluation(
                          out boolIsHighBurst,
                          out IsLowHP,
                          Self,
                          DamageOverTime,
                          DamageRatioThreshold,
                          LowHealthPercentThreshold) &&
                    // Sequence name :InHighThreatScenario
                    (
                          boolIsHighBurst == true
                          ||
                          IsLowHP == true
                    ) &&
                    SetVarBool(
                          out _TargetValid,
                          false) &&
                    TestUnitHasBuff(
                          Self,
                          default,
                          "OdinRecall",
                          false) &&
                    // Sequence name :Management
                    (
                                // Sequence name :NoEnemiesNearbyAndCapturingPoint

                                (CountUnitsInTargetArea(
                                      out NearbyEnemies,
                                      Self,
                                      SelfPosition,
                                      1200,
                                      AffectEnemies | AffectHeroes | AffectMinions,
                                      "") &&
                                NearbyEnemies == 0 &&
                                TestUnitHasBuff(
                                      Self,
                                      default,
                                      "OdinCaptureChannel",
                                      true)
                          /* ||
                        CallProcedureVariable(
                              out           <OutParameter Name=, 
                              out           <OutParameter Name=, 
                              out           <OutParameter Name=, 
                              out           <OutParameter Name=, 
                              out           <OutParameter Name=, 
                              out CastSpellTimeThreshold, 
                              out PreviousSpellCastTime, 
                              out CurrentSpellCast, 
                              out CurrentSpellCastTarget, 
                              out SpellStall, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                                        <Parameter Name=, 
                              Self, 
                              SelfPosition, 
                              CastSpellTimeThreshold, 
                              PreviousSpellCast, 
                              PreviousSpellCastTarget, 
                              PreviousSpellCastTime, 
                              ExhaustSlot, 
                              GhostSlot, 
                              SpellStall)         */
                          &&// Sequence name :SummonerSpells

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
                                           summonerFlash.SummonerFlash(
                                                  Self,
                                                  FlashSlot,
                                                  CurrentClosestTarget,
                                                  false)
                                      )
                                ))
                           ||
                          // Sequence name :HighBurst_MicroRetreat
                          (
                                boolIsHighBurst == true &&
                              dominionRetreat.DominionRetreat(
                                      out _RetreatSafePosition,
                                      out _RetreatPositionStartTime,
                                      Self,
                                      RetreatSafePosition,
                                      RetreatPositionStartTime,
                                      SelfPosition)
                          )
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "HighThreatManagement")

              ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;
        __TargetValid = _TargetValid;
        _ActionPerformed = ActionPerformed;
        __RetreatSafePosition = _RetreatSafePosition;
        __RetreatPositionStartTime = _RetreatPositionStartTime;
        return result;

    }
}

