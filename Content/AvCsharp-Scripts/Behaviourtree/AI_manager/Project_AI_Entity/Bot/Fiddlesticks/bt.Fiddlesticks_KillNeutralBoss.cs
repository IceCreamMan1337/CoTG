using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Fiddlesticks_KillNeutralBossClass : AI_Characters 
{
    private AutoAttackClass autoAttack = new AutoAttackClass();

      bool Fiddlesticks_KillNeutralBoss() {
        return
      (
            // Sequence name :Fiddlesticks_Behavior
            (
                  // Sequence name :RunOnce
                  (
                        TestAIFirstTime(
                              true) &&
                        GetUnitAISelf(
                              out Self) &&
                        SetVarFloat(
                              out DeaggroDistance,
                              1200) &&
                        SetVarFloat(
                              out Damage,
                              0) &&
                        GetUnitCurrentHealth(
                              out PrevHealth,
                              Self) &&
                        GetGameTime(
                              out PrevTime) &&
                        SetVarBool(
                              out LostAggro,
                              false) &&
                        SetVarFloat(
                              out StrengthRatioOverTime,
                              1) &&
                        SetVarBool(
                              out LowThreatMode,
                              false) &&
                        SetVarInt(
                              out PotionsToBuy,
                              4) &&
                        SetVarBool(
                              out TeleportHome,
                              false) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        SetVarAttackableUnit(
                              out Target,
                              Self) &&
                        SetVarVector(
                              out TargetAcquiredPosition,
                              SelfPosition) &&
                        SetVarBool(
                              out TargetValid,
                              false) &&
                        SetVarBool(
                              out TargetDeAggro,
                              false) &&
                        SetVarFloat(
                              out TargetDeAggroTime,
                              0) &&
                        SetVarFloat(
                              out LastKnownTime,
                              0) &&
                        GetUnitPosition(
                              out LastKnownPosition,
                              Self) &&
                        SetVarInt(
                              out KillChampionScore,
                              0) &&
                        SetVarBool(
                              out IssuedAttack,
                              false) &&
                        SetVarAttackableUnit(
                              out IssuedAttackTarget,
                              Self) &&
                        SetVarInt(
                              out PreviousSpellCastNumber,
                              -1) &&
                        SetVarAttackableUnit(
                              out PreviousSpellCastTarget,
                              Self) &&
                        SetVarFloat(
                              out PrevRetreatTime,
                              0)
                  ) ||
                  // Sequence name :AttackNeutrals
                  (
                        GetUnitAISelf(
                              out Self) &&
                        /*   DebugAction(
                                 0, 
                                 SUCCESS, 
                                 GetTaskPosition) &&*/
                        GetAITaskFromEntity(
                              out _CurrentTask,
                              Self) &&
                        GetAITaskTarget(
                              out Target,
                              _CurrentTask) &&
                        SetUnitAIAttackTarget(
                              Target) &&
                         autoAttack.AutoAttack(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              Target,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget)

                  )
                  )
            );
      }
}

