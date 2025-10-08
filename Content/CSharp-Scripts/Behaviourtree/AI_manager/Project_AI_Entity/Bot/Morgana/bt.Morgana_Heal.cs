using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Morgana_HealClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();



    public bool Morgana_Heal(
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
                  // Sequence name :HealFriendlies

                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitAILowestHPTargetInArea(
                        out SelectedFriendlyTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        1000,
                        0.3f,
                        false,
                        true,
                        AffectFriends | AffectHeroes) &&
                  GetUnitPosition(
                        out SelectedFriendlyTargetPosition,
                        SelectedFriendlyTarget) &&
                        // Sequence name :CastE

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
                              SelectedFriendlyTarget,
                              2,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold, default
                              ,
                              false)


            ;


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;
    }
}

