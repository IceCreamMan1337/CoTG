using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Shen_DominionHighThreatManagementClass : AI_Characters 
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private IsInFrontClass isInFront = new IsInFrontClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();

    public bool Shen_DominionHighThreatManagement(
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
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool result =
            // Sequence name :Sequence
            (
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  // Sequence name :UseAbilities
                  (
                        // Sequence name :UseFeint
                        (
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    450,
                                    AffectEnemies | AffectHeroes) &&
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
                        // Sequence name :ShadowDash
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.5f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    450,
                                    AffectEnemies | AffectHeroes) &&
                      //  IsTargetInFront(
                      isInFront.IsInFront(
                                    out IsTargetInFront,
                                    Self,
                                    CurrentClosestTarget) &&
                              IsTargetInFront == false &&
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Self,
                                    CurrentClosestTarget) &&
                              LessEqualFloat(
                                    DistanceToTarget,
                                    500) &&
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
                              ClearUnitAISpellPosition() &&
                              ComputeUnitAISpellPosition(
                                    Self,
                                    CurrentClosestTarget,
                                    600,
                                    false) &&
                              GetUnitAISpellPosition(
                                    out TauntPosition) &&
                              SetAIUnitSpellTargetLocation(
                                    TauntPosition,
                                    SPELLBOOK_CHAMPION,
                                    2) &&
                              ClearUnitAISpellPosition() &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out PreviousSpellCastTime,
                                    Self,
                                    CurrentClosestTarget,
                                    2,
                                    0.8f,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    TauntPosition,
                                    true)
                        ) ||
                        // Sequence name :UseGhost
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.25f) &&
                              GetUnitAIClosestTargetInArea(
                                    out CurrentClosestTarget,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    800,
                                    AffectEnemies | AffectHeroes) &&
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

