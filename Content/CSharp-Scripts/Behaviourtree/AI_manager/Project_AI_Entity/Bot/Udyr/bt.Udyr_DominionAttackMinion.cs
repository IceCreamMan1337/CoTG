namespace BehaviourTrees.all;


class Udyr_DominionAttackMinionClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Udyr_DominionAttackMinion(
        out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
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
                  // Sequence name :SpellKillMinion

                  GetUnitHealthRatio(
                        out TargetHealthRatio,
                        Target) &&
                  LessFloat(
                        TargetHealthRatio,
                        0.3f) &&
                  GetUnitHealthRatio(
                        out SelfHealthRatio,
                        Self) &&
                  GreaterFloat(
                        SelfHealthRatio,
                        0.6f) &&
                  GetUnitPARRatio(
      out SelfPAR_Ratio,
      Self,
  PrimaryAbilityResourceType.MANA) &&
                  GreaterFloat(
                        SelfPAR_Ratio,
                        0.7f) &&
                  canCastChampionAbilityClass.CanCastChampionAbility(
                        Self,
                        3,
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
                        3,
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

