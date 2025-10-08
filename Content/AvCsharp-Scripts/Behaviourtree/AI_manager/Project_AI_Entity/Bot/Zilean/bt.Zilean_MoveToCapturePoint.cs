using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Zilean_MoveToCapturePointClass : AI_Characters 
{

    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();
    public bool Zilean_MoveToCapturePoint(
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
        int _CurrentSpellCast = CurrentSpellCast;
        AttackableUnit _CurrentSpellCastTarget = CurrentSpellCastTarget;
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;

        bool result =
            // Sequence name :CastE
            (
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

            );


        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
       
        __CurrentSpellCast = CurrentSpellCast;
        __CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall =_SpellStall;
        return result;
    }
}

