using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Caitlyn_HighThreatManagementClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private SummonerIgniteClass summonerIgnite = new SummonerIgniteClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private SummonerExhaustClass summonerExhaust = new SummonerExhaustClass();


    public bool Caitlyn_HighThreatManagement(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      Vector3 SelfPosition,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int GhostSlot
         )
      {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
            // Sequence name :HighThreatManagement
            (
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self,default
                        ,
                        true,
                        SelfPosition,
                        450,
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :UseSpells
                  (
                        // Sequence name :E
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    true,
                                    false) &&
                              GetUnitAIBasePosition(
                                    out BasePosition,
                                    Self) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceSelfToBase,
                                    Self,
                                    BasePosition) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceTargetToBase,
                                    CurrentClosestTarget,
                                    BasePosition) &&
                              LessFloat(
                                    DistanceSelfToBase,
                                    DistanceTargetToBase) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    CurrentClosestTarget,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :W
                        (
                              GetUnitAttackRange(
                                    out EnemyAttackRange,
                                    CurrentClosestTarget) &&
                              LessFloat(
                                    EnemyAttackRange,
                                    200) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    true,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Self,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :KiteWithFrozenMallet
                        (
                              TestChampionHasItem(
                                    Self,
                                    3022,
                                    true) &&
                              GetUnitAttackRange(
                                    out SelfAttackRange,
                                    Self) &&
                              MultiplyFloat(
                                    out SelfAttackRange,
                                    SelfAttackRange,
                                    0.95f) &&
                              GetUnitAIClosestTargetInArea(
                                    out TargetToKite,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    SelfAttackRange,
                                    AffectEnemies | AffectHeroes) &&
                              TestUnitHasAnyBuffOfType(
                                    TargetToKite,
                                    BuffType.SLOW,
                                    false) &&
                              SetUnitAIAttackTarget(
                                    TargetToKite) &&
                              IssueAttackOrder()
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Self) &&
                              LessFloat(
                                    HealthRatio,
                                    0.35f) &&
                           summonerExhaust.SummonerExhaust(
                                    Self,
                                    CurrentClosestTarget,
                                    ExhaustSlot)

                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        return result;

    }
}

