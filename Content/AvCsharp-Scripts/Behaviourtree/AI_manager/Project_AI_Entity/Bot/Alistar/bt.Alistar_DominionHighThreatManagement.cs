using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_DominionHighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


    public bool Alistar_DominionHighThreatManagement(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      Vector3 SelfPosition,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int GhostSlot
         )
    {


        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool result =
            // Sequence name :HighThreatManagement
            (
                  // Sequence name :CastW
                  (
                        GetUnitSpellCastRange(
                              out _Range,
                              Self,
                              SPELLBOOK_CHAMPION,
                              1) &&
                        GetUnitAIClosestTargetInArea(
                              out CurrentClosestTarget,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              _Range,
                              AffectEnemies | AffectHeroes) &&
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              1,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTarget,
                              CurrentClosestTarget,
                              PreviousSpellCast,
                              false,
                              false) &&
                       castTargetAbility.CastTargetAbility(
                              out CurrentSpellCast,
                              out CurrentSpellCastTarget,
                              out _PreviousSpellCastTime,
                              out _CastSpellTimeThreshold,
                              Self,
                              CurrentClosestTarget,
                              1,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              default,
                              false)
                  ) ||
                  // Sequence name :CastQ
                  (
                        GetUnitAIClosestTargetInArea(
                              out CurrentClosestTarget,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              350,
                              AffectEnemies |  AffectHeroes) &&
                        canCastChampionAbilityClass.CanCastChampionAbility(
                              Self,
                              0,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTarget,
                              CurrentClosestTarget,
                              PreviousSpellCast,
                              false,
                              false) &&
                       castTargetAbility.CastTargetAbility(
                              out CurrentSpellCast,
                              out CurrentSpellCastTarget,
                              out _PreviousSpellCastTime,
                              out _CastSpellTimeThreshold,
                              Self,
                              CurrentClosestTarget,
                              0,
                              1,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              PreviousSpellCastTime,
                              CastSpellTimeThreshold,
                              default,
                              false)
                  ) ||
                  // Sequence name :UseShurelyas
                  (
                        TestUnitAICanUseItem(
                              3069) &&
                        GetUnitHealthRatio(
                              out SelfHealthRatio,
                              Self) &&
                        LessFloat(
                              SelfHealthRatio,
                              0.2f) &&
                        IssueUseItemOrder(
                              3069,
                              default)

                  )
            );

         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
        return result;
      }
}

