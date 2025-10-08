using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Brand_HighThreatManagementClass : AI_Characters 
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Brand_HighThreatManagement(
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
                        Self,
                        default,
                        true,
                        SelfPosition,
                        800,
                        AffectEnemies | AffectHeroes) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  // Sequence name :UseSpells
                  (
                        // Sequence name :Cast R
                        (
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    CurrentClosestTarget) &&
                              LessFloat(
                                    TargetHealthRatio,
                                    0.5f) &&
                              GetUnitPosition(
                                    out TargetPosition,
                                    CurrentClosestTarget) &&
                              CountUnitsInTargetArea(
                                    out UnitsNearby,
                                    Self,
                                    TargetPosition,
                                    300,
                                    AffectEnemies | AffectHeroes | AffectMinions,
                                    "") &&
                              GreaterEqualInt(
                                    UnitsNearby,
                                    2) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    3,
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
                                    3,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :UseE
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
                        // Sequence name :UseQ
                        (
                              TestUnitHasBuff(
                                    CurrentClosestTarget,
                                   default,
                                    "BrandAblaze",
                                    true) &&
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
                                    60,
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    SelfPosition,
                                    TargetPosition, default
                                    ) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    CurrentClosestTarget,
                                    SelfPosition,
                                    1600,
                                    0.25f) &&
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
                        // Sequence name :UseGhost
                        (
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Self) &&
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

