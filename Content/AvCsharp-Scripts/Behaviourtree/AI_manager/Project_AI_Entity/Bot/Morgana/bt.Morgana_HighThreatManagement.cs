using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Morgana_HighThreatManagementClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Morgana_HighThreatManagement(
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
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  // Sequence name :CastSpells
                  (
                        // Sequence name :UseQ
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
                              TestLineMissilePathIsClear(
                                    Self,
                                    CurrentClosestTarget,
                                    70,
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    CurrentClosestTarget,
                                    SelfPosition,
                                    1600,
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
                        // Sequence name :UseE
                        (
                              CountUnitsInTargetArea(
                                    out UnitsNearby,
                                    Self,
                                    SelfPosition,
                                    550,
                                    AffectEnemies | AffectHeroes,
                                    "") &&
                              GreaterEqualInt(
                                    UnitsNearby,
                                    2) &&
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
                        ) ||
                        // Sequence name :UseGhost
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.35f) &&
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

