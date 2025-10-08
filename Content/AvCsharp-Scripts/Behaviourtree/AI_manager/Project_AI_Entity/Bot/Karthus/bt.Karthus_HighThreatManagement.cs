using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Karthus_HighThreatManagementClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();


    public bool Karthus_HighThreatManagement(
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
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        800,
                        AffectEnemies | AffectHeroes) &&
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  GetDistanceBetweenUnits(
                        out DistanceToTarget,
                        CurrentClosestTarget,
                        Self) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  // Sequence name :UseSpells
                  (
                        // Sequence name :DeathDefied
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "DeathDefiedBuff",
                                    true) &&
                              // Sequence name :CastSpells
                              (
                                    // Sequence name :ToggleEOn
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "Defile",
                                                false) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                2,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                Self,
                                                2,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                0,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                CurrentClosestTarget,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                CurrentClosestTarget,
                                                0,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :CastQ
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    CurrentClosestTarget,
                                    SelfPosition,
                                    1000,
                                    0.25f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition, default
                                    ) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    CurrentClosestTarget,
                                    0,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :CastW
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.3f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              ComputeUnitAISpellPosition(
                                    CurrentClosestTarget,
                                    Self,
                                    50,
                                    true) &&
                              GetUnitAISpellPosition(
                                    out SpellPosition) &&
                              ClearUnitAISpellPosition() &&
                              ClearUnitAISpellTarget(
                                    1) &&
                              SetUnitAISpellTargetLocation(
                                    SpellPosition,
                                    1) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    CurrentClosestTarget,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SpellPosition,
                                    true)
                        ) ||
                        // Sequence name :ToggleEOnIfEnemyCloseAnd&lt,40%HP
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "Defile",
                                    false) &&
                              GetUnitSpellRadius(
                                    out DefileRadius,
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    2) &&
                              LessFloat(
                                    DistanceToTarget,
                                    DefileRadius) &&
                              GetUnitHealthRatio(
                                    out EnemyHealthRatio,
                                    CurrentClosestTarget) &&
                              LessFloat(
                                    EnemyHealthRatio,
                                    0.4f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Self,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Self,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)

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

