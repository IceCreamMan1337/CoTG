using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Ziggs_GlobalClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();

    public bool Ziggs_Global(
               out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      bool SpellStall
         )
    {

        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = default;
        bool result =
                  // Sequence name :Sequence

                  GetUnitPosition(
                        out ZiggsPosition,
                        Self) &&
                  GetUnitsInTargetArea(
                        out EnemyChampionsInArea,
                        Self,
                        ZiggsPosition,
                        5000,
                        AffectEnemies | AffectHeroes) &&
                  ForEach(EnemyChampionsInArea, GlobalTarget =>
                              // Sequence name :Sequence

                              TestUnitIsVisibleToTeam(
                                    Self,
                                    GlobalTarget,
                                    true) &&
                              GetUnitHealthRatio(
                                    out GlobalTargetHealthRatio,
                                    GlobalTarget) &&
                              // Sequence name :DominionConditions
                              (
                                    // Sequence name :VeryLowHealth
                                    (
                                          LessFloat(
                                                GlobalTargetHealthRatio,
                                                0.15f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                GlobalTarget,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                          GetUnitPosition(
                                                out SelfPosition,
                                                Self) &&
                                          GetUnitPosition(
                                                out TargetPosition,
                                                GlobalTarget) &&
                                          PredictLineMissileCastPosition(
                                                out SkillShotCastPosition,
                                                GlobalTarget,
                                                SelfPosition,
                                                5000,
                                                0.25f) &&
                                          GetRandomPositionBetweenTwoPoints(
                                                out SkillShotCastPosition,
                                                TargetPosition,
                                                SkillShotCastPosition,
                                                0.3f) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                GlobalTarget,
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                SkillShotCastPosition,
                                                true)
                                    ) ||
                                    // Sequence name :LowHealthAndCC'd
                                    (
                                          LessFloat(
                                                GlobalTargetHealthRatio,
                                                0.4f) &&
                                          // Sequence name :Conditions
                                          (
                                                TestUnitHasBuff(
                                                      GlobalTarget,
                                                      default,
                                                      "SummonerExhaust",
                                                      true)
                                                ||
                                                TestUnitHasAnyBuffOfType(
                                                      GlobalTarget,
                                                      BuffType.STUN,
                                                      true)
                                                ||
                                                TestUnitHasAnyBuffOfType(
                                                      GlobalTarget,
                                                       BuffType.SNARE,
                                                      true)
                                                ||
                                                TestUnitHasAnyBuffOfType(
                                                      GlobalTarget,
                                                      BuffType.CHARM,
                                                      true)
                                                ||
                                                TestUnitHasAnyBuffOfType(
                                                      GlobalTarget,
                                                       BuffType.SUPPRESSION,
                                                      true)
                                          ) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                GlobalTarget,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                GlobalTarget,
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                default,
                                                false)

                                    )
                              )

                  )
            ;


        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;


        return result;
    }
}

