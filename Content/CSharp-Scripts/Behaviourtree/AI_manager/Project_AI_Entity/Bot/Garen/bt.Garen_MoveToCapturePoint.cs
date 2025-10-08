namespace BehaviourTrees.all;


class Garen_MoveToCapturePointClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Garen_MoveToCapturePoint(
          out float __CastSpellTimeThreshold,
      out int __CurrentSpellCast,
      out AttackableUnit __CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      float CastSpellTimeThreshold,
      int CurrentSpellCast,
      AttackableUnit CurrentSpellCastTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      AttackableUnit Self,
      bool SpellStall
         )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int _CurrentSpellCast = CurrentSpellCast;
        AttackableUnit _CurrentSpellCastTarget = CurrentSpellCastTarget;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;


        bool result =
                    // Sequence name :CastW

                    canCastChampionAbilityClass.CanCastChampionAbility(
                          Self,
                          0,
                          PreviousSpellCastTime,
                          CastSpellTimeThreshold,
                          PreviousSpellCastTarget,
                          Self,
                          PreviousSpellCast,
                          false,
                          true) &&
                   castTargetAbility.CastTargetAbility(
                          out CurrentSpellCast,
                          out CurrentSpellCastTarget,
                          out PreviousSpellCastTime,
                          out CastSpellTimeThreshold,
                          Self,
                          Self,
                          0,
                          1,
                          PreviousSpellCast,
                          PreviousSpellCastTarget,
                          PreviousSpellCastTime,
                          CastSpellTimeThreshold,
                          default,
                          false) &&
                    SetVarBool(
                          out SpellStall,
                          true)

              ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __CurrentSpellCast = _CurrentSpellCast;
        __CurrentSpellCastTarget = _CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        return result;
    }
}

