namespace BehaviourTrees.all;


class Shen_HealClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();



    public bool Shen_Heal(
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
     AttackableUnit IssuedAttackTarget
        )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
                        // Sequence name :Shen Heal

                        // Sequence name :Cast R

                        GetChampionCollection(
                              out ChampionCollection) &&
                        GetUnitTeam(
                              out SelfTeam,
                              Self) &&
                        ForEach(ChampionCollection, IterateUnit =>
                                    // Sequence name :Sequence

                                    GetUnitTeam(
                                          out IterateTeam,
                                          IterateUnit) &&
                                    SelfTeam == IterateTeam &&
                                    GetUnitHealthRatio(
                                          out IterateHealthRatio,
                                          IterateUnit) &&
                                    LessFloat(
                                          IterateHealthRatio,
                                          0.3f) &&
                                          // Sequence name :CastR

                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                IterateUnit,
                                                PreviousSpellCast,
                                                false,
                                                false) &&
                                         castTargetAbility.CastTargetAbility(
                                                out CurrentSpellCast,
                                                out CurrentSpellCastTarget,
                                                out PreviousSpellCastTime,
                                                out CastSpellTimeThreshold,
                                                Self,
                                                IterateUnit,
                                                3,
                                                1,
                                                PreviousSpellCast,
                                                PreviousSpellCastTarget,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                default,
                                                false)



                        )

            ;


        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        return result;

    }
}

