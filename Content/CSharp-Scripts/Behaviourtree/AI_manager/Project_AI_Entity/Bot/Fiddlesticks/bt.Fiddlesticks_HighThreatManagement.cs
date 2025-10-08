using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Fiddlesticks_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Fiddlesticks_HighThreatManagement(
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
                  // Sequence name :ManageHighThreat

                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self,
                        default,
                        true,
                        SelfPosition,
                        450,
                        AffectEnemies | AffectHeroes) &&
                  CountUnitsInTargetArea(
                        out EnemyChampCount,
                        Self,
                        SelfPosition,
                        500,
                        AffectEnemies | AffectHeroes,
                        "") &&
                  GetUnitHealthRatio(
                        out SelfHealthRatio,
                        Self) &&
                  // Sequence name :CastSpells
                  (
                        // Sequence name :Q
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
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
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
                                    false,
                                    false) &&
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
                              EnemyChampCount == 1 &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    CurrentClosestTarget) &&
                              SubtractFloat(
                                    out HealthRatioDelta,
                                    SelfHealthRatio,
                                    TargetHealthRatio) &&
                              GreaterEqualFloat(
                                    HealthRatioDelta,
                                    -0.2f) &&
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
                                   default,
                                    false)
                        ) ||
                        // Sequence name :CastZhonyas
                        (
                              TestUnitAICanUseItem(
                                    3157) &&
                              LessEqualFloat(
                                    SelfHealthRatio,
                                    0.25f) &&
                              LessEqualInt(
                                    EnemyChampCount,
                                    2) &&
                              GetUnitsInTargetArea(
                                    out EnemyChampionsInArea,
                                    Self,
                                    SelfPosition,
                                    500,
                                    AffectEnemies | AffectHeroes) &&
                              ForEach(EnemyChampionsInArea, LowHealthEnemyChampion =>                                     // Sequence name :LowHealthTarget?

                                          GetUnitHealthRatio(
                                                out IteratorUnitHealthRatio,
                                                LowHealthEnemyChampion) &&
                                          LessEqualFloat(
                                                IteratorUnitHealthRatio,
                                                0.4f)

                               &&
                              IssueUseItemOrder(
                                    3157, default
                                    )

                        )
                  )
                  )
            ;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;
    }
}

