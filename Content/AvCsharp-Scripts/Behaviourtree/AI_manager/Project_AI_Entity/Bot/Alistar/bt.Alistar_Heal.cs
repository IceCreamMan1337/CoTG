using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


    public bool Alistar_Heal(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :AlistarHeal
                  (
                        GetUnitCurrentHealth(
                              out Health,
                              Self) &&
                        GetUnitMaxHealth(
                              out MaxHealth,
                              Self) &&
                        DivideFloat(
                              out HP_Ratio,
                              Health,
                              MaxHealth) &&
                        LessFloat(
                              HP_Ratio,
                              0.55f) &&
                        // Sequence name :CastE
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
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
                                    2,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    default,
                                    false)
                        )
                  ) ||
                  // Sequence name :HealFriendlies
                  (
                        GetUnitPosition(
                              out AlistarPosition,
                              Self) &&
                        GetUnitAILowestHPTargetInArea(
                              out SelectedFriendlyTarget,
                              Self,
                              default,
                              true,
                              AlistarPosition,
                              1000,
                              0.6f,
                              false,
                              true,
                              AffectFriends | AffectHeroes | NotAffectSelf) &&
                        // Sequence name :Heal
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    2,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    SelectedFriendlyTarget,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    SelectedFriendlyTarget,
                                    2,
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
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
        return result;
    }
}

