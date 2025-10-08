using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Ezreal_KillChampionAttackSequenceClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    private SummonerIgniteClass summonerIgnite = new();
    private SummonerExhaustClass summonerExhaust = new();
    public bool Ezreal_KillChampionAttackSequence(
         out AttackableUnit __IssuedAttackTarget,
      out bool __IssuedAttack,
      out AttackableUnit _CurrentSpellCastTarget,
      out int _CurrentSpellCast,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
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
      bool IsDominionGameMode
         )
    {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
                  // Sequence name :EzrealKillChampion

                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        Target) &&
                  // Sequence name :EzrealKillChampion
                  (
                        // Sequence name :UseUltimate
                        (
                              GreaterInt(
                                    KillChampionScore,
                                    5) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              LessFloat(
                                    TargetHealthRatio,
                                    0.4f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    3,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    Target,
                                    SelfPosition,
                                    2000,
                                    0.5f) &&
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
                                    Target,
                                    3,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true) &&
                              SetVarBool(
                                    out SpellStall,
                                    true)
                        ) ||
                        // Sequence name :CastE
                        (
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              SubtractFloat(
                                    out HealthRatioDelta,
                                    SelfHealthRatio,
                                    TargetHealthRatio) &&
                              GreaterFloat(
                                    HealthRatioDelta,
                                    0.15f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              ComputeUnitAISpellPosition(
                                    Target,
                                    Self,
                                    150,
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
                                    Target,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SpellPosition,
                                    true)
                        ) ||
                        // Sequence name :AutoAttackIf5Stacks
                        (
                              GetUnitBuffCount(
                                    out RisingSpellForceCount,
                                    Self,
                                    "EzrealRisingSpellForce") &&
                              RisingSpellForceCount == 5 &&
                               autoAttack.AutoAttack(
                                    out IssuedAttack,
                                    out IssuedAttackTarget,
                                    Target,
                                    Self,
                                    IssuedAttack,
                                    IssuedAttackTarget)
                        ) ||
                        // Sequence name :CastQ
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              TestLineMissilePathIsClear(
                                    Self,
                                    Target,
                                    60,
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    Target,
                                    SelfPosition,
                                    2000,
                                    0.25f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out NewSkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition, default
                                    ) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Target,
                                    0,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    NewSkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :CastW
                        (
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Target,
                                    Self) &&
                              LessFloat(
                                    DistanceToTarget,
                                    950) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    Target,
                                    SelfPosition,
                                    1600,
                                    0.25f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out NewSkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition, default
                                    ) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Target,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    NewSkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :UseIgnite
                        (
                              GreaterInt(
                                    KillChampionScore,
                                    5) &&
                          summonerIgnite.SummonerIgnite(
                                    Self,
                                    Target,
                                    IgniteSlot)
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              GreaterInt(
                                    KillChampionScore,
                                    5) &&
                          summonerExhaust.SummonerExhaust(
                                    Self,
                                    Target,
                                    ExhaustSlot)
                        ) ||
                               // Sequence name :AutoAttackOnlyOnKill

                               autoAttack.AutoAttack(
                                    out IssuedAttack,
                                    out IssuedAttackTarget,
                                    Target,
                                    Self,
                                    IssuedAttack,
                                    IssuedAttackTarget)


                  )
            ;


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __IssuedAttack = _IssuedAttack;
        return result;
    }
}

