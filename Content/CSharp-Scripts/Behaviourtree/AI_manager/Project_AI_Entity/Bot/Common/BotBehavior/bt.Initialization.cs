namespace BehaviourTrees.all;


class InitializationClass : AI_Characters
{
    private GetSpellCastDelayClass getSpellCastDelay = new();
    public bool Initialization(
          out AttackableUnit __Self,
     out float __DeaggroDistance,
     out float __Damage,
     out float __PrevHealth,
     out float __PrevTime,
     out bool __LostAggro,
     out float __StrengthRatioOverTime,
     out bool __LowThreatMode,
     out int __PotionsToBuy,
     out bool __TeleportHome,
     out Vector3 __SelfPosition,
     out AttackableUnit __Target,
     out Vector3 __TargetAcquiredPosition,
     out bool __TargetValid,
     out bool __TargetDeAggro,
     out float __TargetDeAggroTime,
     out float __LastKnownTime,
     out Vector3 __LastKnownPosition,
     out int __KillChampionScore,
     out bool __IssuedAttack,
     out AttackableUnit __IssuedAttackTarget,
     out int __PreviousSpellCastNumber,
     out AttackableUnit __PreviousSpellCastTarget,
     out float __PrevRetreatTime,
     out float __LastKnownHealthRatio,
     out float __CastTimeThreshold,
     out float __PreviousSpellCastTime,
     out AttackableUnit __PrevKillChampTarget,
     out float __PrevKillChampTargetHealth,
     out float __PrevKillChampDamageTime,
     out int __ClaritySlot,
     out int __ExhaustSlot,
     out int __GhostSlot,
     out int __HealSlot,
     out int __IgniteSlot,
     out int __TeleportSlot,
     out bool __SpellStall,
     out int __PurchaseItemIndex,
     out bool __IsDominionGameMode,
     out Object __AttackTarget,
     out Object __HealAbilities,
     out Object __HighThreatManagementSpells,
     out Object __ItemBuildPurchase,
     out Object __KillChampionAttackSequence,

      //  out bool __IssuedAttack,
      //  out AttackableUnit __IssuedAttackTarget,
      //  out float __PreviousSpellCastTime,
      //  out float __CastSpellTimeThreshold,
      //  out int __CurrentSpellCast,
      //  out AttackableUnit __CurrentSpellCastTarget,
      out Object __KillChampionScoreModifier,
     //  out int _KillChampionScore,
     out Object __LevelUpAbilities,
     out Object __PostActionBehavior,
     out Object __PushLaneAbilities,
       out bool __BeginnerScaling,
     out int __PromoteSlot,
     out int __CleanseSlot,
     out int __FlashSlot,
     out int __SurgeSlot,
     out Object __GlobalAbilities,
       out string __PreviousActionPerformed,
     out float __LastIssuedEventTime,
       out bool __FinishedItemBuild,
          out int __ActionDebugText,
     out int __TaskDebugText,
     out string __ExtraItem,
     out bool __ExtraItemPurchased
        // out bool _SpellStall,

        //   out float __CastSpellTimeThreshold,
        // out float __PreviousSpellCastTime,
        //  out int __CurrentSpellCast,
        //  out AttackableUnit _CurrentSpellCastTarget,

        /*   out float _CastSpellTimeThreshold,
           out float _PreviousSpellCastTime,
           out int _CurrentSpellCast,
           out AttackableUnit _CurrentSpellCastTarget,
           out bool _SpellStall,*/

        // out int __ItemPurchaseIndex,


        /*  out bool _IssuedAttack,
          out AttackableUnit _IssuedAttackTarget,
          out float _CastSpellTimeThreshold,
          out float _PreviousSpellCastTime,
          out int _CurrentSpellCast,
          out AttackableUnit _CurrentSpellCastTarget,
          out bool _SpellStall,*/

        /* out float _CastSpellTimeThreshold,
         out int _CurrentSpellCast,
         out AttackableUnit _CurrentSpellCastTarget,
         out float _PreviousSpellCastTime,
         out bool _SpellStall,*/

        /*  out float _CastSpellTimeThreshold,
          out float _PreviousSpellCastTime,
          out int _CurrentSpellCast,
          out AttackableUnit _CurrentSpellCastTarget,*/

        // out bool _FinishedItemBuild,

        /*   AttackableUnit ToAttack,
           AttackableUnit Self,
           bool IssuedAttack,
           AttackableUnit IssuedAttackTarget,
           int PreviousSpellCast,
           AttackableUnit PreviousSpellCastTarget,
           float PreviousSpellCastTime,
           float CastSpellTimeThreshold,
           bool SpellStall,
           AttackableUnit Self,
           int PreviousSpellCast,
           AttackableUnit PreviousSpellCastTarget,
           float CastSpellTimeThreshold,
           float PreviousSpellCastTime,
           bool IssuedAttack,
           AttackableUnit IssuedAttackTarget,
           AttackableUnit Self, 
           Vector3 SelfPosition,
          float CastSpellTimeThreshold,
           int PreviousSpellCast,
           AttackableUnit PreviousSpellCastTarget,
           float PreviousSpellCastTime, 
           int ExhaustSlot,
           int GhostSlot,
           bool SpellStall,
          AttackableUnit Self,
           int ItemPurchaseIndex,
           bool IsDominionGameMode,
           bool FinishedItemBuild,
           AttackableUnit Self,
           AttackableUnit Target,
           int KillChampionScore,
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
          bool SpellStall,
           bool IsDominionGameMode,
           AttackableUnit Self, 
           AttackableUnit TempTarget,
          // int KillChampionScore,
          // AttackableUnit Self,
           int UnitLevel,
         //  AttackableUnit Self,
           string ActionPerformed
           float CastSpellTimeThreshold,
           int PreviousSpellCast,
           AttackableUnit PreviousSpellCastTarget,
           float PreviousSpellCastTime,
           AttackableUnit Self,
           bool SpellStall,
           AttackableUnit Self,
           int PreviousSpellCast,
           AttackableUnit PreviousSpellCastTarget,
           float CastSpellTimeThreshold,
           float PreviousSpellCastTime,
           bool IssuedAttack,
           AttackableUnit IssuedAttackTarget */

        )
    {

        AttackableUnit _Self = default;
        float _DeaggroDistance = default;
        float _Damage = default;
        float _PrevHealth = default;
        float _PrevTime = default;
        bool _LostAggro = default;
        float _StrengthRatioOverTime = default;
        bool _LowThreatMode = default;
        int _PotionsToBuy = default;
        bool _TeleportHome = default;
        Vector3 _SelfPosition = default;
        AttackableUnit _Target = default;
        Vector3 _TargetAcquiredPosition = default;
        bool _TargetValid = default;
        bool _TargetDeAggro = default;
        float _TargetDeAggroTime = default;
        float _LastKnownTime = default;
        Vector3 _LastKnownPosition = default;
        int _KillChampionScore = default;
        bool _IssuedAttack = default;
        AttackableUnit _IssuedAttackTarget = default;
        int _PreviousSpellCastNumber = default;
        AttackableUnit _PreviousSpellCastTarget = default;
        float _PrevRetreatTime = default;
        float _LastKnownHealthRatio = default;
        float _CastTimeThreshold = default;
        float _PreviousSpellCastTime = default;
        AttackableUnit _PrevKillChampTarget = default;
        float _PrevKillChampTargetHealth = default;
        float _PrevKillChampDamageTime = default;
        int _ClaritySlot = default;
        int _ExhaustSlot = default;
        int _GhostSlot = default;
        int _HealSlot = default;
        int _IgniteSlot = default;
        int _TeleportSlot = default;
        bool _SpellStall = default;
        int _PurchaseItemIndex = default;
        bool _IsDominionGameMode = default;
        Object _AttackTarget = default;
        float _CastSpellTimeThreshold = default;
        int _CurrentSpellCast = default;
        AttackableUnit _CurrentSpellCasTarget = default;
        Object _HealAbilities = default;
        Object _HighThreatManagementSpells = default;
        Object _ItemBuildPurchase = default;
        int _ItemPurchaseIndex = default;
        bool _FinishedItemBuild = default;
        Object _KillChampionAttackSequence = default;
        Object _KillChampionScoreModifier = default;
        Object _LevelUpAbilities = default;
        Object _PostActionBehavior = default;
        Object _PushLaneAbilities = default;
        bool _BeginnerScaling = default;
        int _PromoteSlot = default;
        int _CleanseSlot = default;
        int _FlashSlot = default;
        int _SurgeSlot = default;
        Object _GlobalAbilities = default;
        string _PreviousActionPerformed = default;
        float _LastIssuedEventTime = default;
        int _ActionDebugText = default;
        int _TaskDebugText = default;
        string _ExtraItem = default;
        bool _ExtraItemPurchased = default;

        bool result =
                     // Sequence name :RunOnce

                     GetUnitAISelf(
                           out _Self) &&
                     SetVarFloat(
                           out _DeaggroDistance,
                           1200) &&
                     SetVarFloat(
                           out _Damage,
                           0) &&
                     GetUnitCurrentHealth(
                           out _PrevHealth,
                           _Self) &&
                     GetGameTime(
                           out _PrevTime) &&
                     SetVarBool(
                           out _LostAggro,
                           false) &&
                     SetVarFloat(
                           out _StrengthRatioOverTime,
                           1) &&
                     SetVarBool(
                           out _LowThreatMode,
                           false) &&
                     SetVarInt(
                           out _PotionsToBuy,
                           4) &&
                     SetVarBool(
                           out _TeleportHome,
                           false) &&
                     GetUnitPosition(
                           out _SelfPosition,
                           _Self) &&
                     SetVarAttackableUnit(
                           out _Target,
                           Self) &&
                     SetVarVector(
                           out _TargetAcquiredPosition,
                           SelfPosition) &&
                     SetVarBool(
                           out _TargetValid,
                           false) &&
                     SetVarBool(
                           out _TargetDeAggro,
                           false) &&
                     SetVarFloat(
                           out _TargetDeAggroTime,
                           0) &&
                     SetVarFloat(
                           out _LastKnownTime,
                           0) &&
                     GetUnitPosition(
                           out _LastKnownPosition,
                           _Self) &&
                     SetVarInt(
                           out _KillChampionScore,
                           0) &&
                     SetVarBool(
                           out _IssuedAttack,
                           false) &&
                     SetVarAttackableUnit(
                           out _IssuedAttackTarget,
                           _Self) &&
                     SetVarInt(
                           out _PreviousSpellCastNumber,
                           -1) &&
                     SetVarAttackableUnit(
                           out _PreviousSpellCastTarget,
                           _Self) &&
                     SetVarFloat(
                           out _PrevRetreatTime,
                           0) &&
                     SetVarFloat(
                           out _LastKnownHealthRatio,
                           1) &&
                     getSpellCastDelay.GetSpellCastDelay(
                           out _CastTimeThreshold) &&
                     SetVarFloat(
                           out _PreviousSpellCastTime,
                           0) &&
                     SetVarAttackableUnit(
                           out _PrevKillChampTarget,
                           _Self) &&
                     SetVarFloat(
                           out _PrevKillChampTargetHealth,
                           0) &&
                     GetGameTime(
                           out _PrevKillChampDamageTime) &&
                     SetVarFloat(
                           out _LastIssuedEventTime,
                           0) &&
                     SetVarInt(
                           out _ClaritySlot,
                           -1) &&
                     SetVarInt(
                           out _CleanseSlot,
                           -1) &&
                     SetVarInt(
                           out _ExhaustSlot,
                           -1) &&
                     SetVarInt(
                           out _FlashSlot,
                           -1) &&
                     SetVarInt(
                           out _GhostSlot,
                           -1) &&
                     SetVarInt(
                           out _HealSlot,
                           -1) &&
                     SetVarInt(
                           out _IgniteSlot,
                           -1) &&
                     SetVarInt(
                           out _PromoteSlot,
                           -1) &&
                     SetVarInt(
                           out _SurgeSlot,
                           -1) &&
                     SetVarInt(
                           out _TeleportSlot,
                           -1) &&
                     SetVarBool(
                           out _SpellStall,
                           false) &&
                     SetVarBool(
                           out _BeginnerScaling,
                           false) &&
                     SetVarInt(
                           out _PurchaseItemIndex,
                           1) &&
                     SetVarString(
                           out _PreviousActionPerformed,
                            ""
                           ) &&
                     SetVarBool(
                           out _IsDominionGameMode,
                           false) &&
                     SetVarBool(
                           out _FinishedItemBuild,
                           false) &&
                     SetVarString(
                           out _ExtraItem,
                           "NotSet") &&
                     SetVarBool(
                           out _ExtraItemPurchased,
                           false) &&
                     SetProcedureVariable(
                           out _AttackTarget, "AttackTarget") &&
                     SetProcedureVariable(
                           out _HealAbilities, "Heal") &&
                     SetProcedureVariable(
                           out _HighThreatManagementSpells, "HighThreatManagement") &&
                     SetProcedureVariable(
                           out _ItemBuildPurchase, "PurchaseItems") &&
                     SetProcedureVariable(
                           out _KillChampionAttackSequence, "KillChampionAttackSequence") &&
                     SetProcedureVariable(
                           out _KillChampionScoreModifier, "KillChampionScoreModifier") &&
                     SetProcedureVariable(
                           out _LevelUpAbilities, "LevelUp") &&
                     SetProcedureVariable(
                           out _PostActionBehavior, "PostAction") &&
                     SetProcedureVariable(
                           out _PushLaneAbilities, "PushLane") &&
                     SetProcedureVariable(
                           out _GlobalAbilities, "Global") &&
                     SetVarInt(
                           out _ActionDebugText,
                           -1) &&
                     SetVarInt(
                           out _TaskDebugText,
                           -1)

               ;

        __Self = _Self;
        __DeaggroDistance = _DeaggroDistance;
        __Damage = _Damage;
        __PrevHealth = _PrevHealth;
        __PrevTime = _PrevTime;
        __LostAggro = _LostAggro;
        __StrengthRatioOverTime = _StrengthRatioOverTime;
        __LowThreatMode = _LowThreatMode;
        __PotionsToBuy = _PotionsToBuy;
        __TeleportHome = _TeleportHome;
        __SelfPosition = _SelfPosition;
        __Target = _Target;
        __TargetAcquiredPosition = _TargetAcquiredPosition;
        __TargetValid = _TargetValid;
        __TargetDeAggro = _TargetDeAggro;
        __TargetDeAggroTime = _TargetDeAggroTime;
        __LastKnownTime = _LastKnownTime;
        __LastKnownPosition = _LastKnownPosition;
        __KillChampionScore = _KillChampionScore;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __PreviousSpellCastNumber = _PreviousSpellCastNumber;
        __PreviousSpellCastTarget = _PreviousSpellCastTarget;
        __PrevRetreatTime = _PrevRetreatTime;
        __LastKnownHealthRatio = _LastKnownHealthRatio;
        __CastTimeThreshold = _CastTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __PrevKillChampTarget = _PrevKillChampTarget;
        __PrevKillChampTargetHealth = _PrevKillChampTargetHealth;
        __PrevKillChampDamageTime = _PrevKillChampDamageTime;
        __ClaritySlot = _ClaritySlot;
        __ExhaustSlot = _ExhaustSlot;
        __GhostSlot = _GhostSlot;
        __HealSlot = _HealSlot;
        __IgniteSlot = _IgniteSlot;
        __TeleportSlot = _TeleportSlot;
        __SpellStall = _SpellStall;
        __PurchaseItemIndex = _PurchaseItemIndex;
        __IsDominionGameMode = _IsDominionGameMode;
        __AttackTarget = _AttackTarget;
        //  __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        //   __CurrentSpellCast = _CurrentSpellCast;
        //   __CurrentSpellCastTarget = _CurrentSpellCasTarget;
        __HealAbilities = _HealAbilities;
        __HighThreatManagementSpells = _HighThreatManagementSpells;
        __ItemBuildPurchase = _ItemBuildPurchase;
        //    __ItemPurchaseIndex = _ItemPurchaseIndex;
        __FinishedItemBuild = _FinishedItemBuild;
        __KillChampionAttackSequence = _KillChampionAttackSequence;
        __KillChampionScoreModifier = _KillChampionScoreModifier;
        __LevelUpAbilities = _LevelUpAbilities;
        __PostActionBehavior = _PostActionBehavior;
        __PushLaneAbilities = default;
        __BeginnerScaling = default;
        __PromoteSlot = default;
        __CleanseSlot = default;
        __FlashSlot = default;
        __SurgeSlot = default;
        __GlobalAbilities = default;
        __PreviousActionPerformed = default;
        __LastIssuedEventTime = default;
        __ActionDebugText = default;
        __TaskDebugText = default;
        __ExtraItem = default;
        __ExtraItemPurchased = default;

        return result;
    }
}

