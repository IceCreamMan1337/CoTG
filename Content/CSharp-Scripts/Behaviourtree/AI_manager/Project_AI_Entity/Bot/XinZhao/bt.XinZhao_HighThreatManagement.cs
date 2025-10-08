using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class XinZhao_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerExhaustClass summonerExhaust = new();

    public bool XinZhao_HighThreatManagement(
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
                  // Sequence name :UseSpellsOrBeAMan

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
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :UseE
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.2f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectHeroes) &&
                              GetUnitAIClosestTargetInArea(
                                    out ClosestEnemyMinion,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectMinions) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToClosestEnemyHero,
                                    Self,
                                    CurrentClosestTarget) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToClosestEnemyMinion,
                                    Self,
                                    ClosestEnemyMinion) &&
                              GetDistanceBetweenUnits(
                                    out DistanceFromMinionToEnemyHero,
                                    ClosestEnemyMinion,
                                    CurrentClosestTarget) &&
                              GreaterFloat(
                                    DistanceFromMinionToEnemyHero,
                                    DistanceToClosestEnemyMinion) &&
                              SubtractFloat(
                                    out DistanceDifference,
                                    DistanceToClosestEnemyMinion,
                                    DistanceToClosestEnemyHero) &&
                              GreaterEqualFloat(
                                    DistanceDifference,
                                    200) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    ClosestEnemyMinion,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    ClosestEnemyMinion,
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.3f) &&
                          summonerExhaust.SummonerExhaust(
                                    Self,
                                    CurrentClosestTarget,
                                    ExhaustSlot)
                        ) ||
                              // Sequence name :CastQ

                              // Sequence name :Attack
                              (
                                    // Sequence name :HasQOn
                                    (
                                          TestUnitHasBuff(
                                                Self,
                                              default,
                                                "XenZhaoComboTarget",
                                                true)

                                          ||
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "XenZhaoComboAuto",
                                                true)
                                          ||
                                          TestUnitHasBuff(
                                                Self,
                                                default,
                                                "XenZhaoComboFinish",
                                                true)
                                    ) &&
                                    LessFloat(
                                          HealthRatio,
                                          0.6f) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self,
                                          default,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    SetUnitAIAttackTarget(
                                          NearestEnemyToAttack) &&
                                    IssueChaseOrder()
                              ) ||
                              // Sequence name :Cast
                              (
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self,
                                          default,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    LessFloat(
                                          HealthRatio,
                                          0.6f) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Self,
                                          PreviousSpellCast,
                                          false,
                                          true) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Self,
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          default,
                                          false)

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

