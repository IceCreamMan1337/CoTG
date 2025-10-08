using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Leona_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerGhostClass summonerGhost = new();

    public bool Leona_HighThreatManagement(
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

                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  // Sequence name :CastSpells
                  (
                        // Sequence name :Cast W
                        (
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectHeroes) &&
                              LessFloat(
                                    HealthRatio,
                                    0.6f) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
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
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                              // Sequence name :Cast Q

                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "LeonaShieldOfDaybreak",
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
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "Haste",
                                    false) &&
                            summonerGhost.SummonerGhost(
                                    Self,
                                    GhostSlot)
                        ) ||
                        // Sequence name :UseShurelyas
                        (
                              TestUnitAICanUseItem(
                                    3069) &&
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "SummonerHaste",
                                    false) &&
                              LessFloat(
                                    HealthRatio,
                                    0.2f) &&
                              IssueUseItemOrder(
                                    3069, default
                                    )

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

