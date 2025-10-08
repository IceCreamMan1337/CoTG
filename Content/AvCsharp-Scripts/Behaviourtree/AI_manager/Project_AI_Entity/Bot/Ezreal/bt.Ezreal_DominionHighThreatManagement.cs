using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Ezreal_DominionHighThreatManagementClass : AI_Characters 
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerExhaustClass summonerExhaust = new SummonerExhaustClass();
    public bool Ezreal_DominionHighThreatManagement(
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
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool result =
            // Sequence name :UseSpells
            (
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        650,
                        AffectEnemies | AffectHeroes) &&
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
                                    1) &&
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

