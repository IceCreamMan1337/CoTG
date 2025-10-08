using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DrMundo_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


     public bool DrMundo_Heal(
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
            // Sequence name :DrMundoHeal
            (
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
                  GetUnitHealthRatio(
                        out HP_Ratio, 
                        Self) &&
                  GetUnitSpellLevel(
                        out RLevel, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3) &&
                  MultiplyFloat(
                        out Threshold, 
                        RLevel, 
                        0.15f) &&
                  SubtractFloat(
                        out Threshold, 
                        0.8f, 
                        Threshold) &&
                  LessFloat(
                        HP_Ratio, 
                        Threshold) &&
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

    );
 _CurrentSpellCast = CurrentSpellCast;
 _CurrentSpellCastTarget = CurrentSpellCastTarget;
 __PreviousSpellCastTime = _PreviousSpellCastTime;
 __CastSpellTimeThreshold = _CastSpellTimeThreshold;

return result;
      }
}

