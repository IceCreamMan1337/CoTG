namespace BehaviourTrees.all;


class KillChampionAttackSequenceClass : AI_Characters
{

    public bool KillChampionAttackSequence(

     out bool __IssuedAttack,
     out AttackableUnit __IssuedAttackTarget,
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out bool __SpellStall,
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
     int GhostSlot,
     int IgniteSlot,
     bool SpellStall,
     int FlashSlot,
     bool IsDominionGameMode
        )
    {


        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _SpellStall = SpellStall;
        bool result =
                    // Sequence name :ReturnFailure

                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              ;
        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __SpellStall = _SpellStall;

        return result;
    }
}

