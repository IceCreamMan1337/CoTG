using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Rammus_HighThreatManagementClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();


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
            ;


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;

    }
}

