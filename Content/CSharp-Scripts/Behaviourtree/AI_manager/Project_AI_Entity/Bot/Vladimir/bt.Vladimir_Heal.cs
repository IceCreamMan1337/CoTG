using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Vladimir_HealClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();

    public bool Vladimir_Heal(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
       bool IssuedAttack,
      AttackableUnit IssuedAttackTarget)
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
                  // Sequence name :VladimirHeal

                  GetUnitHealthRatio(
                        out SelfHealthRatio,
                        Self) &&
                  LessFloat(
                        SelfHealthRatio,
                        0.4f) &&
                  GetUnitSpellCastRange(
                        out SpellRangeQ,
                        Self,
                        SPELLBOOK_CHAMPION,
                        0) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  // Sequence name :GetClosestEnemyUnitInQRangePrioritizeChampions
                  (
                        GetUnitAIClosestTargetInArea(
                              out ClosestEnemyUnit,
                              Self,
                             default,
                              true,
                              SelfPosition,
                              SpellRangeQ,
                              AffectEnemies | AffectHeroes) ||
                              GetUnitAIClosestTargetInArea(
                              out ClosestEnemyUnit,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              SpellRangeQ,
                              AffectEnemies | AffectMinions)
                  ) &&
                  canCastChampionAbilityClass.CanCastChampionAbility(
                        Self,
                        0,
                        PreviousSpellCastTime,
                        CastSpellTimeThreshold,
                        PreviousSpellCastTarget,
                        ClosestEnemyUnit,
                        PreviousSpellCast,
                        false,
                        false) &&
                 castTargetAbility.CastTargetAbility(
                        out CurrentSpellCast,
                        out CurrentSpellCastTarget,
                        out PreviousSpellCastTime,
                        out CastSpellTimeThreshold,
                        Self,
                        ClosestEnemyUnit,
                        0,
                        1,
                                        PreviousSpellCast,
                PreviousSpellCastTarget,
                PreviousSpellCastTime,
                CastSpellTimeThreshold,
                default,
                false)

    ;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;
    }
}

