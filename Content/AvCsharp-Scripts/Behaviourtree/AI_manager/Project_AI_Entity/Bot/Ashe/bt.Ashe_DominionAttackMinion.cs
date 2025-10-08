using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Ashe_DominionAttackMinionClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
 

     public bool Ashe_DominionAttackMinion(
         out int __CurrentSpellCast,
      out AttackableUnit __CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      AttackableUnit Target,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {

        int CurrentSpellCast = default;
      AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
       float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :ToggleOffFrostShot
                  (
                        TestUnitHasBuff(
                              Self,
                              default,
                              "FrostShot",
                              true) &&
                        TestCanCastSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0,
                              true) &&
                        CastUnitSpell(
                              Self,
                              SPELLBOOK_CHAMPION,
                              0,
                               default, default
                              )
                  ) ||
                  // Sequence name :SpellKillMinion
                  (
                        GetUnitHealthRatio(
                              out TargetHealthRatio,
                              Target) &&
                        LessFloat(
                              TargetHealthRatio,
                              0.3f) &&
                        GetUnitPARRatio(
                              out SelfPAR_Ratio,
                              Self,
                               PrimaryAbilityResourceType.MANA) &&
                        GreaterFloat(
                              SelfPAR_Ratio,
                              0.5f) &&
                        // Sequence name :UseVolley
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Target,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Target,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)

                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        __CurrentSpellCast = CurrentSpellCast;
        __CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;


    }
}

