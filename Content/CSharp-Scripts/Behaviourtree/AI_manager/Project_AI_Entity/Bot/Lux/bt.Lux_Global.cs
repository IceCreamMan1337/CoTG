using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Lux_GlobalClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    public bool Lux_Global(
               out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      bool SpellStall
         )
    {

        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = default;
        bool result =
                  // Sequence name :Sequence

                  GetUnitPosition(
                        out LuxPosition,
                        Self) &&
                  GetUnitSpellCastRange(
                        out UltRange,
                        Self,
                        SPELLBOOK_CHAMPION,
                        3) &&
                  SubtractFloat(
                        out UltRange,
                        UltRange,
                        100) &&
                  GetUnitsInTargetArea(
                        out EnemyChampionsInArea,
                        Self,
                        LuxPosition,
                        UltRange,
                        AffectEnemies | AffectHeroes) &&
                  GetUnitSpellLevel(
                        out UltLevel,
                        Self,
                        SPELLBOOK_CHAMPION,
                        3) &&
                  GreaterInt(
                        UltLevel,
                        0) &&
                  GetSpellSlotCooldown(
                        out UltCooldown,
                        Self,
                        SPELLBOOK_CHAMPION,
                        3) &&
                  LessEqualFloat(
                        UltCooldown,
                        0) &&
                  ForEach(EnemyChampionsInArea, Target =>
                              // Sequence name :UseUltimate

                              TestUnitIsVisibleToTeam(
                                    Self,
                                    Target,
                                    true) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    3,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              GetUnitCurrentHealth(
                                    out TargetHealth,
                                    Target) &&
                              SetVarFloat(
                                    out UltThreshold,
                                    0) &&
                              // Sequence name :KillConditions
                              (
                                    // Sequence name :Level1Ult
                                    (
                                          UltLevel == 1 &&
                                          SetVarFloat(
                                                out UltThreshold,
                                                250)
                                    ) ||
                                    // Sequence name :Level2Ult
                                    (
                                          UltLevel == 2 &&
                                          SetVarFloat(
                                                out UltThreshold,
                                                400)
                                    ) ||
                                    // Sequence name :Level3Ult
                                    (
                                          UltLevel == 3 &&
                                          SetVarFloat(
                                                out UltThreshold,
                                                550)
                                    )
                              ) &&
                              LessFloat(
                                    TargetHealth,
                                    UltThreshold) &&
                              GetUnitPosition(
                                    out TargetPosition,
                                    Target) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    Target,
                                    LuxPosition,
                                    10000,
                                    0.5f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition,
                                    0.7f) &&
                              CalculatePointOnLine(
                                    out NewCastPosition,
                                    LuxPosition,
                                    SkillShotCastPosition,
                                    500) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Target,
                                    3,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    NewCastPosition,
                                    true) &&
                              SetVarBool(
                                    out SpellStall,
                                    true)


                  )
            ;


        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;


        return result;
    }
}

