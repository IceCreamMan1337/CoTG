using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MalzaharBehaviorClass : AI_Characters 
{
    private InitializationClass initialization = new InitializationClass();
    private ReferenceUpdateClass referenceUpdate = new ReferenceUpdateClass();
    private PassiveActionsClass passiveActions = new PassiveActionsClass();
    private TargetVisibilityUpdatesClass targetVisibilityUpdates = new TargetVisibilityUpdatesClass();
    private ActionsClass actions = new ActionsClass();
bool Malzahar_Behavior() { 
          return   (
                  // Sequence name :Initialization
                  (
                        TestAIFirstTime(
                              true) &&
                   initialization. Initialization(
                              out Self, 
                              out DeaggroDistance, 
                              out Damage, 
                              out PrevHealth, 
                              out PrevTime, 
                              out LostAggro, 
                              out StrengthRatioOverTime, 
                              out LowThreatMode, 
                              out PotionsToBuy, 
                              out TeleportHome, 
                              out SelfPosition, 
                              out Target, 
                              out TargetAcquiredPosition, 
                              out TargetValid, 
                              out TargetDeAggro, 
                              out TargetDeAggroTime, 
                              out LastKnownTime, 
                              out LastKnownPosition, 
                              out KillChampionScore, 
                              out IssuedAttack, 
                              out IssuedAttackTarget, 
                              out PreviousSpellCastNumber, 
                              out PreviousSpellCastTarget, 
                              out PrevRetreatTime, 
                              out LastKnownHealthRatio, 
                              out CastTimeThreshold, 
                              out PreviousSpellCastTime, 
                              out PrevKillChampTarget, 
                              out PrevKillChampTargetHealth, 
                              out PrevKillChampDamageTime, 
                              out ClaritySlot, 
                              out ExhaustSlot, 
                              out GhostSlot, 
                              out HealSlot, 
                              out IgniteSlot, 
                              out TeleportSlot, 
                              out SpellStall, 
                              out PurchaseItemIndex, 
                              out IsDominionGameMode, 
                              out _AttackTarget, 
                              out _HealAbilities, 
                              out HighThreatManagementSpells, 
                              out _ItemBuildPurchase, 
                              out _KillChampionAttackSequence, 
                              out _KillChampionScoreModifier, 
                              out _LevelUpAbilities, 
                              out _PostActionBehavior, 
                              out _PushLaneAbilities, 
                              out BeginnerScaling, 
                              out PromoteSlot, 
                              out CleanseSlot, 
                              out FlashSlot, 
                              out SurgeSlot, 
                              out _GlobalAbilities, 
                              out PreviousActionPerformed, 
                              out LastIssuedEventTime, 
                              out FinishedItemBuild, 
                              out ActionDebugText, 
                              out TaskDebugText, 
                              out ExtraItem, 
                              out ExtraItemPurchased
                           ) &&
                        // Sequence name :Initialization_Malzahar
                        (
                              SetVarString(
                                    out _Champion,
                                    "Malzahar") &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :SummonerSpells
                                    (
                                          // Sequence name :Intermediate
                                          (
                                                TestEntityDifficultyLevel(
                                                      true, 
                                                     EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) &&
                                                SetVarInt(
                                                      out ExhaustSlot, 
                                                      0) &&
                                                SetVarInt(
                                                      out GhostSlot, 
                                                      1)
                                          ) ||
                                          // Sequence name :Advanced
                                          (
                                                 TestEntityDifficultyLevel(
                                                      true,
                                                      EntityDiffcultyType.DIFFICULTY_ADVANCED) &&
                                                SetVarInt(
                                                      out IgniteSlot, 
                                                      0) &&
                                                SetVarInt(
                                                      out ClaritySlot, 
                                                      1)
                                          )
                                    )
                              ) &&
                              // Sequence name :KillChampionFuzzyLogic
                              (
                                    SetVarFloat(
                                          out ValueOfAbility0, 
                                          0.25f) &&
                                    SetVarFloat(
                                          out ValueOfAbility1, 
                                          0.5f) &&
                                    SetVarFloat(
                                          out ValueOfAbility2, 
                                          0.5f) &&
                                    SetVarFloat(
                                          out ValueOfAbility3, 
                                          1) &&
                                    SetVarInt(
                                          out CC_AbilityNumber, 
                                          3) &&
                                    SetVarFloat(
                                          out AbilityNotReadyMaxScore, 
                                          1) &&
                                    SetVarFloat(
                                          out AbilityNotReadyMinScore, 
                                          0) &&
                                    SetVarFloat(
                                          out AbilityReadinessMaxScore, 
                                          1) &&
                                    SetVarFloat(
                                          out AbilityReadinessMinScore, 
                                          0)
                              ) &&
                              // Sequence name :HighThreatManagementFuzzyLogic
                              (
                                    SetVarFloat(
                                          out DamageRatioThreshold, 
                                          0.15f) &&
                                    SetVarFloat(
                                          out LowHealthPercentThreshold, 
                                          0.25f)
                              ) &&
                              // Sequence name :BehaviourTrees
                              (
                                    SetProcedureVariable(
      out _AttackTarget, "AttackTarget") &&
                                    SetProcedureVariable(
                                           out _HighThreatManagementSpells, "HighThreatManagement") &&//function 
                                     SetProcedureVariable(
                                          out _ItemBuildPurchase, "PurchaseItems") && //function 
                                    SetProcedureVariable(
                                          out _KillChampionAttackSequence, "KillChampionAttackSequence") && //function 
                                    SetProcedureVariable(
                                         out _LevelUpAbilities, "LevelUp") //function 
                              )
                        )
                  ) ||
                  // Sequence name :Behavior
                  (
                        referenceUpdate. ReferenceUpdate(
                              out SelfPosition, 
                              out Damage, 
                              out StrengthRatioOverTime, 
                              out PrevHealth, 
                              out PrevTime, 
                              out TeleportHome, 
                              out _BeginnerScaling, 
                              PrevTime, 
                              Damage, 
                              StrengthRatioOverTime, 
                              PrevHealth, 
                              Self, 
                              TeleportHome) &&
                        targetVisibilityUpdates.TargetVisibilityUpdates(
                              out TargetValid, 
                              out LastKnownPosition, 
                              out LastKnownTime, 
                              out LastKnownHealthRatio, 
                              TargetValid, 
                              LastKnownPosition, 
                              LastKnownTime, 
                              10, 
                              LastKnownHealthRatio, 
                              Target, 
                              Self) &&
                        // Sequence name :Malzahar_Actions
                        (
                                               passiveActions.PassiveActions(
                        out PotionsToBuy,
                        out IssuedAttack,
                        out IssuedAttackTarget,
                        out PurchaseItemIndex,
                        out FinishedItemBuild,
                        out ExtraItem,
                        out ExtraItemPurchased,
                        Self,
                        PotionsToBuy,
                        IssuedAttack,
                        IssuedAttackTarget,
                        _Champion,
                        PurchaseItemIndex,
                        IsDominionGameMode,
                        _ItemBuildPurchase,
                        _LevelUpAbilities,
                        BeginnerScaling,
                        FinishedItemBuild,
                        ExtraItem,
                        ExtraItemPurchased)
                 ||
                actions.Actions(
                        out TeleportHome,
                        out LowThreatMode,
                        out TargetDeAggro,
                        out TargetDeAggroTime,
                        out Target,
                        out TargetAcquiredPosition,
                        out TargetValid,
                        out LastKnownPosition,
                        out LastKnownTime,
                        out KillChampionScore,
                        out IssuedAttack,
                        out IssuedAttackTarget,
                        out PrevRetreatTime,
                        out PreviousSpellCastTime,
                        out CastTimeThreshold,
                        out PreviousSpellCastNumber,
                        out PreviousSpellCastTarget,
                        out SpellStall,
                        out PreviousActionPerformed,
                        out LastIssuedEventTime,
                        out ActionDebugText,
                        out TaskDebugText,
                        Self,
                        SelfPosition,
                        TeleportHome,
                        LowThreatMode,
                        StrengthRatioOverTime,
                        Damage,
                        TargetDeAggro,
                        TargetDeAggroTime,
                        Target,
                        TargetAcquiredPosition,
                        TargetValid,
                        LastKnownPosition,
                        LastKnownTime,
                        KillChampionScore,
                        IssuedAttack,
                        IssuedAttackTarget,
                        PreviousSpellCastNumber,
                        PreviousSpellCastTarget,
                        PrevRetreatTime,
                        PreviousSpellCastTime,
                        CastTimeThreshold,
                        _Champion,
                       ValueOfAbility0,
                       ValueOfAbility1,
                       ValueOfAbility2,
                       ValueOfAbility3,
                        CC_AbilityNumber,
                        AbilityReadinessMinScore,
                        AbilityReadinessMaxScore,
                        AbilityNotReadyMinScore,
                        AbilityNotReadyMaxScore,
                        ClaritySlot,
                        ExhaustSlot,
                        GhostSlot,
                        HealSlot,
                        IgniteSlot,
                        TeleportSlot,
                        DamageRatioThreshold,
                        LowHealthPercentThreshold,
                        10,
                        SpellStall,
                        _AttackTarget,
                        _HealAbilities,
                        _HighThreatManagementSpells,
                        _KillChampionAttackSequence,
                        _KillChampionScoreModifier,
                        _PostActionBehavior,
                        _PushLaneAbilities,
                        BeginnerScaling,
                        SurgeSlot,
                        CleanseSlot,
                        PromoteSlot,
                        FlashSlot,
                        _GlobalAbilities,
                        PreviousActionPerformed,
                        LastIssuedEventTime,
                        ActionDebugText,
                        TaskDebugText)
                )
            )
      
);
      }
}

