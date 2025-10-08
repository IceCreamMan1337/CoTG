namespace BehaviourTrees.all;


class Graves_Behavior_DominionClass : AI_Characters
{
    private InitializationClass initializationClass = new();
    private DominionInitializationClass dominionInitialization = new();
    private DominionTaskInitializationClass dominionTaskInitialization = new();
    private DominionReferenceUpdateClass dominionReferenceUpdate = new();
    private TargetVisibilityUpdatesClass targetVisibilityUpdates = new();
    private DominionPassiveActionsClass dominionPassiveActions = new();
    private ActionsDominionClass actionsDominion = new();

    bool Graves_Behavior_Dominion()
    {
        return
                      // Sequence name :Graves_Behavior_Dominion

                      // Sequence name :Initialization
                      (
                            TestAIFirstTime(
                                  true) &&
                            initializationClass.Initialization(
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
                                  out _AttackTarget, //function AttackTarget
                                  out _HealAbilities,   //function
                                  out HighThreatManagementSpells,
                                  out _ItemBuildPurchase,   //function
                                  out _KillChampionAttackSequence,
                                  out _KillChampionScoreModifier,   //function
                                  out _LevelUpAbilities,   //function
                                  out _PostActionBehavior,   //function
                                  out _PushLaneAbilities,   //function
                                  out BeginnerScaling,
                                  out PromoteSlot,
                                  out CleanseSlot,
                                  out FlashSlot,
                                  out SurgeSlot,
                                  out _GlobalAbilities,   //function
                                  out PreviousActionPerformed,
                                  out LastIssuedEventTime,
                                  out FinishedItemBuild,
                                  out ActionDebugText,
                                  out TaskDebugText,
                                  out ExtraItem,
                                  out ExtraItemPurchased
                               ) &&
                            dominionInitialization.DominionInitialization(
                                  out LastUseSpellTime,
                                  out PreviousTask,
                                  out RetreatPositionStartTime,
                                  out RetreatSafePosition,
                                  out RetreatFromCP_RetreatUntilTime,
                                  out WanderUntilTime,
                                  out IsDominionGameMode,
                                  out BeginnerWaitInBaseTime,
                                  out NextActionTime,
                                  out _DominionAttackMinion,  //function 
                                  out _MoveToCapturePointAbilities,  //function
                                  out GarrisonSlot,
                                  Self) &&
                                  // Sequence name :Initialization_Graves

                                  SetVarString(
                                        out _Champion,
                                        "Graves") &&
                                        // Sequence name :MaskFailure

                                        // Sequence name :SummonerSpells
                                        (
                                              // Sequence name :Intermediate
                                              (
                                                    TestEntityDifficultyLevel(
                                                          true,
                                                         EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) &&
                                                    SetVarInt(
                                                          out GhostSlot,
                                                          0) &&
                                                    SetVarInt(
                                                          out SurgeSlot,
                                                          1)
                                              ) ||
                                              // Sequence name :Advanced
                                              (
                                                     TestEntityDifficultyLevel(
                                                          true,
                                                          EntityDiffcultyType.DIFFICULTY_ADVANCED) &&
                                                    SetVarInt(
                                                          out GhostSlot,
                                                          0) &&
                                                    SetVarInt(
                                                          out IgniteSlot,
                                                          1)
                                              )
                                        )
                                   &&
                                        // Sequence name :KillChampionFuzzyLogic

                                        SetVarFloat(
                                              out ValueOfAbility0,
                                              0.5f) &&
                                        SetVarFloat(
                                              out ValueOfAbility1,
                                              0.3f) &&
                                        SetVarFloat(
                                              out ValueOfAbility2,
                                              0.25f) &&
                                        SetVarFloat(
                                              out ValueOfAbility3,
                                              1) &&
                                        SetVarInt(
                                              out CC_AbilityNumber,
                                              -1) &&
                                        SetVarFloat(
                                              out AbilityNotReadyMaxScore,
                                              0.55f) &&
                                        SetVarFloat(
                                              out AbilityNotReadyMinScore,
                                              0) &&
                                        SetVarFloat(
                                              out AbilityReadinessMaxScore,
                                              1) &&
                                        SetVarFloat(
                                              out AbilityReadinessMinScore,
                                              0.25f)
                                   &&
                                        // Sequence name :HighThreatManagementFuzzyLogic

                                        SetVarFloat(
                                              out DamageRatioThreshold,
                                              0.15f) &&
                                        SetVarFloat(
                                              out LowHealthPercentThreshold,
                                              0.25f)
                                   &&
                                        // Sequence name :BehaviourTrees

                                        SetProcedureVariable(
                                              out _DominionAttackMinion, "DominionAttackMinion") && //function
                                        SetProcedureVariable(
                                               out _HighThreatManagementSpells, "HighThreatManagement") &&//function 
                                         SetProcedureVariable(
                                              out _ItemBuildPurchase, "PurchaseItems") && //function 
                                        SetProcedureVariable(
                                              out _KillChampionAttackSequence, "KillChampionAttackSequence") && //function 
                                        SetProcedureVariable(
                                             out _LevelUpAbilities, "LevelUp") //function 


                      ) ||
                      // Sequence name :Behavior
                      (
                            dominionTaskInitialization.DominionTaskInitialization(
                                  out CurrentTask,
                                  out RunBT_SuppressCapturePoint,
                                  out RunBT_KillChampion,
                                  out RunBT_HighThreat,
                                  out RunBT_LowThreat,
                                  out RunBT_CapturePoint,
                                  out RunBT_ReturnToBase,
                                  out RunBT_Attack,
                                  out RunBT_Move,
                                  out RunBT_Wander,
                                  out PreviousTask,
                                  out RunBT_InterruptCapture,
                                  out KillChampionAggressiveState,
                                  out RunBT_RetreatFromEnemyCapturePoint,
                                  out RunBT_MidThreat,
                                  out RunBT_NinjaCapturePoint,
                                  Self,
                                  PreviousTask) &&
                           dominionReferenceUpdate.DominionReferenceUpdate(
                                  out SelfPosition,
                                  out Damage,
                                  out StrengthRatioOverTime,
                                  out PrevHealth,
                                  out PrevTime,
                                  out TeleportHome,
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
                                 // Sequence name :Graves_Actions

                                 dominionPassiveActions.DominionPassiveActions(
                                        out PotionsToBuy,
                                        out IssuedAttack,
                                        out IssuedAttackTarget,
                                        out PurchaseItemIndex,
                                        out FinishedItemBuild,
                                        Self,
                                        PotionsToBuy,
                                        IssuedAttack,
                                        IssuedAttackTarget,
                                        "Champion",
                                        PurchaseItemIndex,
                                        IsDominionGameMode,
                                        _ItemBuildPurchase,
                                        _LevelUpAbilities,
                                        FinishedItemBuild)
                                  &&
                                 actionsDominion.ActionsDominion(
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
                                        out LastUseSpellTime,
                                        out RetreatPositionStartTime,
                                        out RetreatSafePosition,
                                        out RetreatFromCP_RetreatUntilTime,
                                        out WanderUntilTime,
                                        out PrevKillChampTarget,
                                        out PrevKillChampTargetHealth,
                                        out PrevKillChampDamageTime,
                                        out BeginnerWaitInBaseTime,
                                        out NextActionTime,
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
                                        "Champion",
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
                                        RunBT_KillChampion,
                                        RunBT_HighThreat,
                                        RunBT_LowThreat,
                                        RunBT_CapturePoint,
                                        RunBT_ReturnToBase,
                                        RunBT_Attack,
                                        RunBT_Move,
                                        RunBT_Wander,
                                        RunBT_SuppressCapturePoint,
                                        LastUseSpellTime,
                                        RetreatPositionStartTime,
                                        RetreatSafePosition,
                                        RunBT_InterruptCapture,
                                        KillChampionAggressiveState,
                                        RunBT_RetreatFromEnemyCapturePoint,
                                        RetreatFromCP_RetreatUntilTime,
                                        WanderUntilTime,
                                        PrevKillChampTarget,
                                        PrevKillChampTargetHealth,
                                        PrevKillChampDamageTime,
                                        RunBT_MidThreat,
                                        GarrisonSlot,
                                        PromoteSlot,
                                        RunBT_NinjaCapturePoint,
                                        BeginnerWaitInBaseTime,
                                        NextActionTime,
                                        _HealAbilities, //function 
                                        _KillChampionAttackSequence,
                                        _KillChampionScoreModifier,
                                        HighThreatManagementSpells,
                                        _DominionAttackMinion,
                                        _MoveToCapturePointAbilities,
                                        _PostActionBehavior,
                                        SurgeSlot,
                                        CleanseSlot,
                                        FlashSlot,
                                        _GlobalAbilities,
                                        PreviousActionPerformed,
                                        LastIssuedEventTime,
                                        ActionDebugText,
                                        TaskDebugText)
                      )
                ;
    }
}

