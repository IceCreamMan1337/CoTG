using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Swain_HighThreatManagementClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();

    public bool Swain_HighThreatManagement(
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

                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self,
                        default,
                        true,
                        SelfPosition,
                        600,
                        AffectEnemies | AffectHeroes) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  GetUnitHealthRatio(
                        out SelfHealthRatio,
                        Self) &&
                  // Sequence name :UseSpells
                  (
                        // Sequence name :Decrepify
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
                                    0.8f,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :W
                        (
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
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :SwainHeal
                        (
                              LessEqualFloat(
                                    SelfHealthRatio,
                                    0.4f) &&
                                    // Sequence name :ToggleSpell

                                    TestUnitHasBuff(
                                          Self,
                                          default,
                                          "SwainMetamorphism",
                                          false) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
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
                                          3,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)

                        ) ||
                        // Sequence name :CastZhonyas
                        (
                              TestUnitAICanUseItem(
                                    3157) &&
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "SwainMetamorphism",
                                    true) &&
                              LessEqualFloat(
                                    SelfHealthRatio,
                                    0.25f) &&
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
                                                0.5f)

                              ) &&
                              IssueUseItemOrder(
                                    3157, default
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

