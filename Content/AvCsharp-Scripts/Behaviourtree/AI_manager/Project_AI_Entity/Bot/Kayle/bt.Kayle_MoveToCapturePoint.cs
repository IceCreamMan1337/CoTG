using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Kayle_MoveToCapturePointClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();

    public bool Kayle_MoveToCapturePoint(
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
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;

        bool result =
              // Sequence name :CastW
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
                          1,
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
                          1,
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

              );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __SpellStall = _SpellStall;
        __CurrentSpellCast = CurrentSpellCast;
        __CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
      
        return result;
      }
}

