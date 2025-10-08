using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Rammus_HighThreatManagementClass : AI_Characters 
{
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    private SummonerGhostClass summonerGhost = new SummonerGhostClass();


    public bool Rammus_HighThreatManagement(
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
            // Sequence name :Sequence
            (
                  TestUnitHasBuff(
                        Self, default
                        ,
                        "DefensiveBallCurl",
                        false) &&
                  TestUnitIsChanneling(
                        Self,
                        false) &&
                  // Sequence name :UseAbilities
                  (
                        // Sequence name :UseW
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "Powerball",
                                    false) &&
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
                              CastUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    1,
                                   default, default
                                    )
                        ) ||
                        // Sequence name :Sequence
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "Powerball",
                                    true) &&
                              GetUnitAIClosestTargetInArea(
                                    out NearestEnemyToAttack,
                                    Self,
                                   default,
                                    true,
                                    SelfPosition,
                                    275,
                                    AffectEnemies | AffectHeroes) &&
                              TestLineMissilePathIsClear(
                                    Self,
                                    NearestEnemyToAttack,
                                    120,
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                              SetUnitAIAttackTarget(
                                    NearestEnemyToAttack) &&
                              IssueChaseOrder()
                        ) ||
                        // Sequence name :UseQ
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "Powerball",
                                    false) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Self,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              CastUnitSpell(
                                    Self,
                                    SPELLBOOK_CHAMPION,
                                    0,
                                   default, default
                                    )
                        ) ||
                        // Sequence name :UseGhost
                        (
                              GetUnitHealthRatio(
                                    out HealthRatio,
                                    Self) &&
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

