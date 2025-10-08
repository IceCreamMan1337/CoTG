using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Ezreal_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerExhaustClass summonerExhaust = new();


    public bool Ezreal_HighThreatManagement(
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
                  // Sequence name :UseSpells

                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :CastQIfInRange
                        (
                              GetUnitSpellCastRange(
                                    out QCastRange,
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    0) &&
                              MultiplyFloat(
                                    out QCastRange,
                                    QCastRange,
                                    0.9f) &&
                              GetUnitAIClosestTargetInArea(
                                    out NearestEnemyToAttack,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    QCastRange,
                                    AffectEnemies | AffectHeroes) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Self,
                                    NearestEnemyToAttack) &&
                              GreaterFloat(
                                    DistanceToTarget,
                                    350) &&
                              GetUnitPosition(
                                    out TargetPosition,
                                    NearestEnemyToAttack) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    NearestEnemyToAttack,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              TestLineMissilePathIsClear(
                                    Self,
                                    NearestEnemyToAttack,
                                    60,
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    SelfPosition,
                                    TargetPosition, default
                                    ) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    NearestEnemyToAttack,
                                    SelfPosition,
                                    1600,
                                    0.25f) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    NearestEnemyToAttack,
                                    0,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :CastWIfInRange
                        (
                              GetUnitSpellCastRange(
                                    out WCastRange,
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1) &&
                              MultiplyFloat(
                                    out WCastRange,
                                    WCastRange,
                                    0.9f) &&
                              GetUnitAIClosestTargetInArea(
                                    out NearestEnemyToAttack,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    WCastRange,
                                    AffectEnemies | AffectHeroes) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Self,
                                    NearestEnemyToAttack) &&
                              GreaterFloat(
                                    DistanceToTarget,
                                    350) &&
                              GetUnitPosition(
                                    out TargetPosition,
                                    NearestEnemyToAttack) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    NearestEnemyToAttack,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    SelfPosition,
                                    TargetPosition, default
                                    ) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    NearestEnemyToAttack,
                                    SelfPosition,
                                    1600,
                                    0.25f) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    NearestEnemyToAttack,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :UseE
                        (
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    650,
                                    AffectEnemies | AffectHeroes) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    false,
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
                              ClearUnitAISpellPosition() &&
                              ComputeUnitAISpellPosition(
                                    Self,
                                    CurrentClosestTarget,
                                    800,
                                    false) &&
                              GetUnitAISpellPosition(
                                    out RocketJumpPosition) &&
                              SetAIUnitSpellTargetLocation(
                                    RocketJumpPosition,
                                    SPELLBOOK_CHAMPION,
                                    2) &&
                              ClearUnitAISpellPosition() &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out PreviousSpellCastTime,
                                    Self,
                                    CurrentClosestTarget,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    RocketJumpPosition,
                                    true)
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    450,
                                    AffectEnemies | AffectHeroes) &&
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
            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;
    }
}

