using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nasus_HighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

     public bool Nasus_HighThreatManagement(
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
            (
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        800,
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :UseW
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.7f) &&
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
                        // Sequence name :CastQ
                        (
                              // Sequence name :Attack
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SiphoningStrikeNew",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelfPosition,
                                          350,
                                          AffectEnemies | AffectHeroes) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          NearestEnemyToAttack) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
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
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          NearestEnemyToAttack) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.3f) &&
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
                        ) ||
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
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
       
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
      
        return result;


    }
}

