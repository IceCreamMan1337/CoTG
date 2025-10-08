namespace BehaviourTrees.all;


class Udyr_PushLaneClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();

    public bool Udyr_PushLane(
           out float __CastSpellTimeThreshold,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      float CastSpellTimeThreshold,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      bool SpellStall
         )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;

        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;

        bool result =
                  // Sequence name :CastE

                  GetUnitPARRatio(
      out SelfPAR_Ratio,
      Self,
  PrimaryAbilityResourceType.MANA) &&
                  GreaterFloat(
                        SelfPAR_Ratio,
                        0.8f) &&
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
                        CastSpellTimeThreshold, default
                        ,
                        false) &&
                  SetVarBool(
                        out SpellStall,
                        true)

            ;


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        return result;

    }
}

