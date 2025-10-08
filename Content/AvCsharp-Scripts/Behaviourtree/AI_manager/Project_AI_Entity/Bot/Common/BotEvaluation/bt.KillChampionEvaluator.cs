using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using MoonSharp.Interpreter;

namespace BehaviourTrees.all;


class KillChampionEvaluatorClass : AI_Characters 
{
    private IsCrowdControlledClass isCrowdControlled = new IsCrowdControlledClass();  
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
   private EvalAbilitiesClass evalAbilities = new EvalAbilitiesClass();
    private HighLowBurstScoreClass highLowBurstScore = new HighLowBurstScoreClass();
    private MinValueClass minValue = new MinValueClass();  

    public bool KillChampionEvaluator(
           out int _KillChampionScore,
      float StrengthRatio,
      AttackableUnit Self,
      Vector3 SelfPosition,
      float DamageOverTime,
      AttackableUnit Target,
      float ValueOfAbility0,
      float ValueOfAbility1,
      float ValueOfAbility2,
      float ValueOfAbility3,
      int CC_AbilityNumber,
      float AbilityReadinessMinScore,
      float AbilityReadinessMaxScore,
      float AbilityNotReadyMinScore,
      float AbilityNotReadyMaxScore,
      bool AggressiveState,
      bool IsMelee
         )
    {

        int KillChampionScore = default;


        bool result =
              // Sequence name :Evaluator
              (
                    GetUnitHealthRatio(
                          out MyHealthRatio,
                          Self) &&
                    GetUnitHealthRatio(
                          out TargetHealthRatio,
                          Target) &&
                         DebugAction("KillChampionEvaluator") && 
                    CreateFuzzyFunctor(
                          out FuzzyFunctor_NoTowerNoCC,
                          "NoTowerNoCC",
                          3) &&
                    CreateFuzzyFunctor(
                          out FuzzyFunctor_NoTowerHasCC,
                          "NoTowerCC",
                          3) &&
                    SetVarBool(
                          out HasStun,
                          false) &&
                    SetVarBool(
                          out InEnemyTowerRange,
                          false) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :StunnedOrHasStun
                          (
                                // Sequence name :HasCC
                                (
                                      TestUnitHasBuff(
                                            Self,
                                            default,
                                            "Pyromania_particle",
                                            true) &&
                                      SetVarBool(
                                            out HasStun,
                                            true)
                                ) ||
                              ( isCrowdControlled.IsCrowdControlled(
                                      out HasStun,
                                      Target) 
                               && 
                               HasStun == true )
                               
                               ||
                                // Sequence name :CCAbilityReady
                                (
                                      GreaterEqualInt(
                                            CC_AbilityNumber,
                                            0) &&
                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                            Self,
                                            CC_AbilityNumber,
                                            default,
                                            default,
                                            default,
                                            default,
                                           default,
                                            true,
                                            false) &&
                                      SetVarBool(
                                            out HasStun,
                                            true)
                                )
                          )
                          || MaskFailure()
                    )


                 && DebugAction("InEnemyTowerRange") 
                    &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :InEnemyTowerRange
                          (
                                CountUnitsInTargetArea(
                                      out TurretsCount,
                                      Self,
                                      SelfPosition,
                                      800,
                                      AffectEnemies | AffectTurrets,
                                      "") &&
                                GreaterInt(
                                      TurretsCount,
                                      0) &&
                                SetVarBool(
                                      out InEnemyTowerRange,
                                      true)
                          )
                          || MaskFailure()
                    ) &&

                      DebugAction("FuzzyInputs") &&

                    CreateFloatCollection(
                          out FuzzyInputs,
                          10,
                          0) &&
                    // Sequence name :EvalValuesBasedOnDifficulty
                    (
                          // Sequence name :Non-Aggressive
                          (


                                AggressiveState == false &&
                                // Sequence name :MeleeOrRanged_Passive
                                (
                                DebugAction("Non-Aggressive") &&
                                      // Sequence name :Melee
                                      (

                                         DebugAction("Melee") &&

                                            IsMelee == true &&
                                            // Sequence name :Passive_Melee
                                            (
                                                  // Sequence name :EasyPassive
                                                  (
                                                        TestEntityDifficultyLevel(
                                                              true,
                                                           EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              0.7f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              0.7f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                    evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                              ValueOfAbility0,
                                                              ValueOfAbility1,
                                                              ValueOfAbility2,
                                                              ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                    evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                               false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              5,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              5,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                  highLowBurstScore.HighLowBurstScore(
                                                              out IsHighBurst,
                                                              true,
                                                              DamageOverTime,
                                                              Self) &&
                                                    highLowBurstScore.HighLowBurstScore(
                                                              out IsLowBurst,
                                                              false,
                                                              DamageOverTime,
                                                              Self)
                                                    && DebugAction("COMPLETE")
                                                  ) ||
                                                  // Sequence name :NotEasyPassive
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
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0.1f,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0.1f,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0.1f,
                                                              0.7f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0.1f,
                                                              0.7f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                   evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                   evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                               false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              5,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              5,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        GetUnitCurrentHealth(
                                                              out MyHealth,
                                                              Self) &&
                                                        DivideFloat(
                                                              out DamageRatio,
                                                              DamageOverTime,
                                                              MyHealth) &&
                                                        InterpolateLine(
                                                              out IsLowBurst,
                                                              0,
                                                              0.25f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              DamageRatio) &&
                                                        InterpolateLine(
                                                              out IsHighBurst,
                                                              0,
                                                              0.25f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              DamageRatio)
                                                  ) && DebugAction("COMPLETE")
                                            )
                                      ) ||
                                      // Sequence name :Ranged
                                      (
                                       DebugAction("Ranged") &&

                                            IsMelee == false &&
                                            // Sequence name :Passive_Ranged
                                            (
                                                  // Sequence name :EasyPassive
                                                  (
                                                        TestEntityDifficultyLevel(
                                                              true,
                                                            EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              0.7f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              0.7f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                         evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                      evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                               false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              5,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              5,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                             highLowBurstScore.HighLowBurstScore(
                                                              out IsHighBurst,
                                                              true,
                                                              DamageOverTime,
                                                              Self) &&
                                                            highLowBurstScore.HighLowBurstScore(
                                                              out IsLowBurst,
                                                              false,
                                                              DamageOverTime,
                                                              Self)
                                                            && DebugAction("COMPLETE")
                                                  ) ||
                                                  // Sequence name :NotEasyPassive
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
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              0.7f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              0.7f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                     evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                   evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                              false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              5,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              5,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        GetUnitCurrentHealth(
                                                              out MyHealth,
                                                              Self) &&
                                                        DivideFloat(
                                                              out DamageRatio,
                                                              DamageOverTime,
                                                              MyHealth) &&
                                                        InterpolateLine(
                                                              out IsLowBurst,
                                                              0,
                                                              0.25f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              DamageRatio) &&
                                                        InterpolateLine(
                                                              out IsHighBurst,
                                                              0,
                                                              0.25f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              DamageRatio)
                                                  ) && DebugAction("COMPLETE")
                                            )
                                      )
                                )
                          ) ||
                          // Sequence name :Aggressive
                          (
                                AggressiveState == true &&
                                // Sequence name :MeleeOrRanged
                                (
                                DebugAction("Aggressive") &&
                                      // Sequence name :Melee
                                      (
                                            IsMelee == true &&
                                              DebugAction("IsMelee") &&
                                            // Sequence name :Aggressive_Melee
                                            (
                                                  // Sequence name :Easy
                                                  (
                                                        TestEntityDifficultyLevel(
                                                              true,
                                                            EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                      evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                   evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                               false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              6,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              6,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                         highLowBurstScore.HighLowBurstScore(
                                                              out IsHighBurst,
                                                              true,
                                                              DamageOverTime,
                                                              Self) &&
                                                              highLowBurstScore.HighLowBurstScore(
                                                              out IsLowBurst,
                                                              false,
                                                              DamageOverTime,
                                                              Self)
                                                              && DebugAction("COMPLETE")
                                                  ) ||
                                                  // Sequence name :NotEasy
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
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0.1f,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0.1f,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0.1f,
                                                              0.9f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0.1f,
                                                              0.9f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                    evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                   evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                               false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              1.5f,
                                                              7.5f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              1.5f,
                                                              7.5f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        GetUnitCurrentHealth(
                                                              out MyHealth,
                                                              Self) &&
                                                        DivideFloat(
                                                              out DamageRatio,
                                                              DamageOverTime,
                                                              MyHealth) &&
                                                        InterpolateLine(
                                                              out IsLowBurst,
                                                              0,
                                                              0.4f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              DamageRatio) &&
                                                        InterpolateLine(
                                                              out IsHighBurst,
                                                              0,
                                                              0.4f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              DamageRatio) 
                                                  ) && DebugAction("COMPLETE")
                                            )
                                      ) ||
                                      // Sequence name :Ranged
                                      (
                                            IsMelee == false &&
                                                DebugAction("Ranged") &&
                                            // Sequence name :Aggressive_Ranged
                                            (
                                                  // Sequence name :EasyAggressive
                                                  (
                                                        TestEntityDifficultyLevel(
                                                              true,
                                                           EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                         evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                              false) &&
                                                         evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                              false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              0,
                                                              6,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              0,
                                                              6,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                          highLowBurstScore.HighLowBurstScore(
                                                              out IsHighBurst,
                                                              true,
                                                              DamageOverTime,
                                                              Self) &&
                                                         highLowBurstScore.HighLowBurstScore(
                                                              out IsLowBurst,
                                                              false,
                                                              DamageOverTime,
                                                              Self)
                                                         && DebugAction("COMPLETE")
                                                  ) ||
                                                  // Sequence name :NotEasyAggressive
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
                                                        InterpolateLine(
                                                              out IsHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              MyHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetHealthyScore,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        InterpolateLine(
                                                              out IsTargetUnhealthyScore,
                                                              0,
                                                              1,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              TargetHealthRatio) &&
                                                        evalAbilities.EvalAbilities(
                                                              out HasMana,
                                                              Self,
                                                              true,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityReadinessMinScore,
                                                              AbilityReadinessMaxScore,
                                                               false) &&
                                                    evalAbilities.EvalAbilities(
                                                              out NoMana,
                                                              Self,
                                                              false,
                                                             ValueOfAbility0,
                                                             ValueOfAbility1,
                                                             ValueOfAbility2,
                                                             ValueOfAbility3,
                                                              AbilityNotReadyMinScore,
                                                              AbilityNotReadyMaxScore,
                                                              false) &&
                                                        InterpolateLine(
                                                              out SafetyScore,
                                                              3,
                                                              9,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        InterpolateLine(
                                                              out UnsafetyScore,
                                                              3,
                                                              9,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              StrengthRatio) &&
                                                        GetUnitCurrentHealth(
                                                              out MyHealth,
                                                              Self) &&
                                                        DivideFloat(
                                                              out DamageRatio,
                                                              DamageOverTime,
                                                              MyHealth) &&
                                                        InterpolateLine(
                                                              out IsLowBurst,
                                                              0,
                                                              0.4f,
                                                              1,
                                                              0,
                                                              0,
                                                              1,
                                                              DamageRatio) &&
                                                        InterpolateLine(
                                                              out IsHighBurst,
                                                              0,
                                                              0.4f,
                                                              0,
                                                              1,
                                                              0,
                                                              1,
                                                              DamageRatio)
                                                  )
                                            ) && DebugAction("COMPLETE")
                                      )
                                )
                          )
                    ) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          0,
                          IsLowBurst) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          1,
                          IsHighBurst) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          2,
                          IsHealthyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          3,
                          IsUnhealthyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          4,
                          IsTargetHealthyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          5,
                          IsTargetUnhealthyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          6,
                          SafetyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          7,
                          UnsafetyScore) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          8,
                          HasMana) &&
                    SetVarFloatCollectionItem(
                          out FuzzyInputs,
                          FuzzyInputs,
                          9,
                          NoMana) &&
                    SetVarFloat(
                          out PokeScore,
                          0) &&
                    SetVarFloat(
                          out RetreatScore,
                          0) &&
                    SetVarFloat(
                          out KillScore,
                          0) &&


                          DebugAction("FuzzyInputs correctly implemented") &&
                    // Sequence name :StunAndTurretEval
                    (
                          // Sequence name :NoStunHasTurret
                          (
                                HasStun == false &&
                                InEnemyTowerRange == true &&
                                SetVarFloat(
                                      out RetreatScore,
                                      1)
                            && DebugAction("NoStunHasTurret   ") 
                          ) ||
                          // Sequence name :HasStunHasTurret
                          (
                                HasStun == true &&
                                InEnemyTowerRange == true &&
                                 minValue.MinValue(
                                      out tempScore,
                                       IsLowBurst,
                                       IsHealthyScore,
                                       IsTargetUnhealthyScore,
                                       SafetyScore,
                                       HasMana) &&
                                MaxFloat(
                                      out KillScore,
                                      tempScore,
                                      KillScore) &&
                                // Sequence name :LGBGB
                                (
                                      minValue.MinValue(
                                            out tempScore1,
                                             IsLowBurst,
                                             IsHealthyScore,
                                             IsTargetUnhealthyScore,
                                             SafetyScore,
                                             NoMana) &&
                                      MaxFloat(
                                            out KillScore,
                                            tempScore1,
                                            KillScore)
                                ) &&
                                // Sequence name :HBBGG
                                (
                                    minValue.MinValue(
                                            out tempScore2,
                                             IsHighBurst,
                                             IsUnhealthyScore,
                                             IsTargetUnhealthyScore,
                                            SafetyScore,
                                            HasMana) &&
                                      MaxFloat(
                                            out KillScore,
                                            tempScore2,
                                            KillScore)
                                ) &&
                                // Sequence name :!(LBGBB_OR_LGBGB_OR_HBBGG)
                                (
                                      MaxFloat(
                                            out temp,
                                            tempScore,
                                            tempScore1) &&
                                      MaxFloat(
                                            out temp,
                                            temp,
                                            tempScore2) &&
                                      SubtractFloat(
                                            out RetreatScore,
                                            1,
                                            temp)


                                ) && DebugAction("HasStunHasTurret   ")
                          ) ||
                          // Sequence name :NoTurretHasCC
                          (
                                HasStun == true &&
                                InEnemyTowerRange == false &&
                                EvalFuzzyFunctor(
                                      out FuzzyOutputs,
                                      FuzzyFunctor_NoTowerHasCC,
                                      FuzzyInputs) &&
                                GetVarFloatCollectionItem(
                                      out KillScore,
                                      FuzzyOutputs,
                                      0) &&
                                GetVarFloatCollectionItem(
                                      out PokeScore,
                                      FuzzyOutputs,
                                      1) &&
                                GetVarFloatCollectionItem(
                                      out RetreatScore,
                                      FuzzyOutputs,
                                      2)

                                 && DebugAction("NoTurretHasCC   ")
                          ) ||
                          // Sequence name :NoTurretNoStun
                          (
                                HasStun == false &&
                                InEnemyTowerRange == false &&
                                EvalFuzzyFunctor(
                                      out FuzzyOutputs,
                                      FuzzyFunctor_NoTowerNoCC,
                                      FuzzyInputs) &&
                                GetVarFloatCollectionItem(
                                      out KillScore,
                                      FuzzyOutputs,
                                      0) &&
                                GetVarFloatCollectionItem(
                                      out PokeScore,
                                      FuzzyOutputs,
                                      1) &&
                                GetVarFloatCollectionItem(
                                      out RetreatScore,
                                      FuzzyOutputs,
                                      2)
                                                && DebugAction("NoTurretNoStun   ")
                          )
                    )
                      && DebugAction("stun or turret eval passed ") 
                    &&
                    // Sequence name :Select_Action
                    (
                          // Sequence name :SelectKill
                          (
                                // Sequence name :ContinueIF
                                (
                                      // Sequence name :I am at or above level 6
                                      (
                                            GetUnitLevel(
                                                  out UnitLevel,
                                                  Self) &&
                                            GreaterEqualInt(
                                                  UnitLevel,
                                                  6)
                                      ) ||
                                      // Sequence name :NoTurretsNearby
                                      (
                                            CountUnitsInTargetArea(
                                                  out NearbyTurretCount,
                                                  Self,
                                                  SelfPosition,
                                                  980,
                                                  AffectEnemies | AffectTurrets,
                                                  "") &&
                                                        DebugAction("TurretsNearby =   " + NearbyTurretCount) &&
                                            LessInt(
                                                  NearbyTurretCount,
                                                  1)
                                      )
                                ) &&
                                GreaterEqualFloat(
                                      KillScore,
                                      PokeScore) &&
                                GreaterEqualFloat(
                                      KillScore,
                                      RetreatScore) &&
                                SetVarInt(
                                      out KillChampionScore,
                                      10)
                          ) ||
                          // Sequence name :SelectPoke
                          (
                                // Sequence name :ContinueIF
                                (
                                      // Sequence name :I am at or above level 6
                                      (
                                            GetUnitLevel(
                                                  out UnitLevel,
                                                  Self) &&
                                            GreaterEqualInt(
                                                  UnitLevel,
                                                  6)
                                      ) ||
                                      // Sequence name :NoTurretsNearby
                                      (
                                            CountUnitsInTargetArea(
                                                  out NearbyTurretCount,
                                                  Self,
                                                  SelfPosition,
                                                  980,
                                                  AffectEnemies | AffectTurrets,
                                                  "") &&
                                                       DebugAction("TurretsNearby =   " + NearbyTurretCount) &&
                                            LessInt(
                                                  NearbyTurretCount,
                                                  1)
                                      )
                                ) &&
                                GreaterEqualFloat(
                                      PokeScore,
                                      KillScore) &&
                                GreaterEqualFloat(
                                      PokeScore,
                                      RetreatScore) &&
                                SetVarInt(
                                      out KillChampionScore,
                                      5)
                          ) ||
                          // Sequence name :Sequence
                          (
                            DebugAction("KillChampionScore = 0   ") &&
                                SetVarInt(
                                      out KillChampionScore,
                                      0)
                          )
                    )
                     && DebugAction("has selected action now go for kill  ")
                    &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :OnlyGoForTheKillInFavorableConditions
                          (
                                GreaterInt(
                                      KillChampionScore,
                                      0) &&
                                LessEqualFloat(
                                      MyHealthRatio,
                                      0.25f) &&
                                // Sequence name :OverwriteOrNot
                                (
                                      // Sequence name :DoNotOverwrite
                                      (
                                            GreaterFloat(
                                                  MyHealthRatio,
                                                  TargetHealthRatio) &&
                                            CountUnitsInTargetArea(
                                                  out EnemyChampCount,
                                                  Self,
                                                  SelfPosition,
                                                  900,
                                                  AffectEnemies | AffectHeroes,
                                                  "") &&
                                            CountUnitsInTargetArea(
                                                  out FriendlyChampCount,
                                                  Self,
                                                  SelfPosition,
                                                  900,
                                                  AffectFriends | AffectHeroes,
                                                  "") &&
                                            LessEqualInt(
                                                  EnemyChampCount,
                                                  FriendlyChampCount)
                                      ) ||
                                      SetVarInt(
                                            out KillChampionScore,
                                            0)

                                )
                          )
                          || MaskFailure()
                              && DebugAction("MaskFailure OnlyGoForTheKillInFavorableConditions  ")
                    )
              );
         _KillChampionScore = KillChampionScore;
        return result;
        

    }
}

