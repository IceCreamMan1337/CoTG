using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Shyvana_HighThreatManagementClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();

    public bool Shyvana_HighThreatManagement(
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
                  // Sequence name :RetreatHighThreat

                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :UseW
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.5f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
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
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                              // Sequence name :CastE

                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaDoubleAttack",
                                          true) &&
                                    GreaterFloat(
                                          HealthRatio,
                                          0.4f) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    SetUnitAIAttackTarget(
                                          NearestEnemyToAttack) &&
                                    IssueChaseOrder()
                              ) ||
                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaDoubleAttackDragon",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
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
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          false) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    GreaterFloat(
                                          HealthRatio,
                                          0.4f) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :Cast
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "ShyvanaTransform",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              )
                         ||
                        // Sequence name :UseGhost
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.2f) &&
                           summonerGhost.SummonerGhost(
                                    Self,
                                    GhostSlot)

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

