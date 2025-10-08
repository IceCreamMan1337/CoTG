using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DrMundo_AttackTargetClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();

     public bool DrMundo_AttackTarget(
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
            (
         
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
                        // Sequence name :BurningAgony
                        (
                              TargetType == MINION_UNIT &&
                              GetUnitPosition(
                                    out SelfPosition, 
                                    Self) &&
                              GetUnitLevel(
                                    out SelfLevel, 
                                    Self) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio, 
                                    Self) &&
                              // Sequence name :Toggle
                              (
                                    // Sequence name :ToggleOn
                                    (
                                          TestUnitHasBuff(
                                                Self, 
                                                default, 
                                                "BurningAgony", 
                                                false) &&
                                          GreaterFloat(
                                                SelfHealthRatio, 
                                                0.7f) &&
                                          GreaterInt(
                                                SelfLevel, 
                                                9) &&
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
                                                CastSpellTimeThreshold, default
                                                , 
                                                false)
                                    )
                              )
                        ) ||
                        // Sequence name :HarassChampion
                        (
                              TargetType == HERO_UNIT &&
                        
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self, 
                                    0, 
                                    PreviousSpellCastTime, 
                                    CastSpellTimeThreshold, 
                                    PreviousSpellCastTarget, 
                                    ToAttack, 
                                    PreviousSpellCast, 
                                    false, 
                                    false) &&
                               
                              TestLineMissilePathIsClear(
                                    Self, 
                                    ToAttack, 
                                    125, 
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
                                   
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
                                    CastSpellTimeThreshold, 
                                   default , 
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
              );
        CurrentSpellCast = _CurrentSpellCast;
        CurrentSpellCastTarget = _CurrentSpellCastTarget;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;
      }
}

