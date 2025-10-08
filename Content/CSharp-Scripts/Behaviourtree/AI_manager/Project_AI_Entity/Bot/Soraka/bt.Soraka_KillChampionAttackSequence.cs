namespace BehaviourTrees.all;


class Soraka_KillChampionAttackSequenceClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    public bool Soraka_KillChampionAttackSequence(
        out AttackableUnit __IssuedAttackTarget,
     out bool __IssuedAttack,
     out AttackableUnit _CurrentSpellCastTarget,
     out int _CurrentSpellCast,
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     AttackableUnit Self,
     AttackableUnit Target,
     int KillChampionScore,
     bool IssuedAttack,
     AttackableUnit IssuedAttackTarget,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float CastSpellTimeThreshold,
     float PreviousSpellCastTime,
     int ExhaustSlot,
     int FlashSlot,
     int GhostSlot,
     int IgniteSlot,
     bool IsDominionGameMode
        )
    {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
                  // Sequence name :SorakaKillChampion

                  // Sequence name :CastE
                  (
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              2,
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
                              2,
                              0.8f,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold, default
                              ,
                              false)
                  ) ||
                  // Sequence name :CastQ
                  (
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              0,
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
                              0,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold, default
                              ,
                              false)
                  ) ||
                         // Sequence name :AutoAttackOnlyOnKill

                         autoAttack.AutoAttack(
                              out IssuedAttack,
                              out IssuedAttackTarget,
                              Target,
                              Self,
                              IssuedAttack,
                              IssuedAttackTarget)


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

