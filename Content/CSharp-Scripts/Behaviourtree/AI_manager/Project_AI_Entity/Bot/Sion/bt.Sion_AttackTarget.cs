using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class Sion_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();

    public bool Sion_AttackTarget(
       out bool __IssuedAttack,
     out AttackableUnit __IssuedAttackTarget,
     out int CurrentSpellCast,
     out AttackableUnit CurrentSpellCastTarget,
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
   AttackableUnit ToAttack,
   AttackableUnit Self,
   bool IssuedAttack,
   AttackableUnit IssuedAttackTarget,
   int PreviousSpellCast,
   AttackableUnit PreviousSpellCastTarget,
   float CastSpellTimeThreshold,
   float PreviousSpellCastTime
       )
    {
        int _CurrentSpellCast = default;
        AttackableUnit _CurrentSpellCastTarget = default;
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;


        bool result =
                  // Sequence name :AttackTarget

                  GetUnitTeam(
                        out TargetTeam,
                        ToAttack) &&
                  GetUnitTeam(
                        out SelfTeam,
                        Self) &&
                  NotEqualUnitTeam(
                        TargetTeam,
                        SelfTeam) &&
                  GetUnitType(
                        out TargetType,
                        ToAttack) &&
                  // Sequence name :ChampMinion
                  (
                        // Sequence name :Minion
                        (
                              TargetType == MINION_UNIT &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    ToAttack) &&
                              // Sequence name :Enrage
                              (
                                    // Sequence name :ToggleOn
                                    (
                                          LessFloat(
                                                TargetHealthRatio,
                                                0.25f) &&
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "Enrage",
                                                false) &&
                                          TestCanCastSpell(
                                                Self,
                                                SPELLBOOK_CHAMPION,
                                                2,
                                                true) &&
                                          CastUnitSpell(
                                                Self,
                                                SPELLBOOK_CHAMPION,
                                                2, default
                                                , default
                                                )
                                    ) ||
                                    // Sequence name :ToggleOff
                                    (
                                          GreaterFloat(
                                                TargetHealthRatio,
                                                0.25f) &&
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "Enrage",
                                                true) &&
                                          TestCanCastSpell(
                                                Self,
                                                SPELLBOOK_CHAMPION,
                                                2,
                                                true) &&
                                          CastUnitSpell(
                                                Self,
                                                SPELLBOOK_CHAMPION,
                                                2, default
                                                , default
                                                )
                                    )
                              )
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio,
                                    Self,
                                     PrimaryAbilityResourceType.MANA) &&
                              GreaterFloat(
                                    SelfPAR_Ratio,
                                    0.4f) &&
                                    // Sequence name :CastQ

                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          ToAttack,
                                          PreviousSpellCast,
                                          true,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          ToAttack,
                                          0,
                                          0.9f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)

                        ) ||
                         autoAttack.AutoAttack(
                                out _IssuedAttack,
                                out _IssuedAttackTarget,
                                ToAttack,
                                Self,
                                IssuedAttack,
                                IssuedAttackTarget)

                    )
              ;
        CurrentSpellCast = _CurrentSpellCast;
        CurrentSpellCastTarget = _CurrentSpellCastTarget;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;
    }
}

