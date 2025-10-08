using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MissFortune_HighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private SummonerExhaustClass summonerExhaust = new SummonerExhaustClass();

     public bool MissFortune_HighThreatManagement(
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
            // Sequence name :UseAbilities
            (
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :CastQIfInRange
                        (
                              GetUnitSpellCastRange(
                                    out QCastRange, 
                                    Self, 
                                    SPELLBOOK_CHAMPION, 
                                    0) &&
                              MultiplyFloat(
                                    out QCastRange, 
                                    QCastRange, 
                                    0.9f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget, 
                                    Self, 
                                   default , 
                                    true, 
                                    SelfPosition, 
                                    QCastRange, 
                                    AffectEnemies | AffectHeroes) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget, 
                                    CurrentClosestTarget, 
                                    Self) &&
                              GreaterFloat(
                                    DistanceToTarget, 
                                    350) &&
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
                                   default , 
                                    false)
                        ) ||
                        // Sequence name :E
                        (
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget, 
                                    Self, 
                                   default , 
                                    true, 
                                    SelfPosition, 
                                    550, 
                                    AffectEnemies | AffectHeroes) &&
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
                                   default , 
                                    false)
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              GetUnitAIClosestTargetInArea(
                                    out NearestEnemyToAttack, 
                                    Self, 
                                   default , 
                                    true, 
                                    SelfPosition, 
                                    350, 
                                    AffectEnemies | AffectHeroes) &&
                              GetUnitHealthRatio(
                                    out HealthRatio, 
                                    Self) &&
                              LessFloat(
                                    HealthRatio, 
                                    0.35f) &&
                             summonerExhaust. SummonerExhaust(
                                    Self, 
                                    CurrentClosestTarget, 
                                    ExhaustSlot)

                        )
                  )
            );
             _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result ;
      }
}

