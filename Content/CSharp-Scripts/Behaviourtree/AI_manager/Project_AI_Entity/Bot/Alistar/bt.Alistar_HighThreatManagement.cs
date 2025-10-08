using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Alistar_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Alistar_HighThreatManagement(
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
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
                  // Sequence name :HighThreatManagement

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
                        GetUnitAIBasePosition(
                              out BasePosition,
                              Self) &&
                        DistanceBetweenObjectAndPoint(
                              out SelfDistanceToBase,
                              Self,
                              BasePosition) &&
                        DistanceBetweenObjectAndPoint(
                              out TargetDistanceToBase,
                              CurrentClosestTarget,
                              BasePosition) &&
                        SubtractFloat(
                              out TargetDistanceToBase,
                              TargetDistanceToBase,
                              75) &&
                        LessFloat(
                              SelfDistanceToBase,
                              TargetDistanceToBase) &&
                       castTargetAbility.CastTargetAbility(
                              out CurrentSpellCast,
                              out CurrentSpellCastTarget,
                              out PreviousSpellCastTime,
                              out CastSpellTimeThreshold,
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
                              AffectEnemies | AffectHeroes) &&
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
                              out PreviousSpellCastTime,
                              out CastSpellTimeThreshold,
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
                              3069
                              ) &&
                        GetUnitHealthRatio(
                              out SelfHealthRatio,
                              Self) &&
                        LessFloat(
                              SelfHealthRatio,
                              0.2f) &&
                        IssueUseItemOrder(
                              3069, default
                              )

                  )
             ;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;
    }
}

