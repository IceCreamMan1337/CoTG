using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class MonkeyKing_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttack_MeleeClass autoAttack_Melee = new();

    public bool MonkeyKing_AttackTarget(
       out bool __IssuedAttack,
     out AttackableUnit __IssuedAttackTarget,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
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
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
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
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio,
                                    Self,
                                     PrimaryAbilityResourceType.MANA) &&
                              GreaterFloat(
                                    SelfPAR_Ratio,
                                    0.8f) &&
                              // Sequence name :CastSpells
                              (
                                    // Sequence name :SpinToWin
                                    (
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "MonkeyKingSpinToWin",
                                                true) &&
                                       autoAttack_Melee.AutoAttack_Melee(
                                                out IssuedAttack,
                                                out IssuedAttackTarget,
                                                ToAttack,
                                                Self,
                                                IssuedAttack,
                                                IssuedAttackTarget)
                                    ) ||
                                    // Sequence name :CastQ
                                    (
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
                                                Self,
                                                0,
                                                0.9f,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    )
                              )
                        ) ||
                    autoAttack_Melee.AutoAttack_Melee(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              ToAttack,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget)

                  )
            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;



    }
}

