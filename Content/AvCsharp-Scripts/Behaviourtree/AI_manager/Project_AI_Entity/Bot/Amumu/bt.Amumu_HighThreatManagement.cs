using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Amumu_HighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


     public bool Amumu_HighThreatManagement(
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
            (
                  canCastChampionAbilityClass.CanCastChampionAbility(
                        Self, 
                        0, 
                        default, 
                        default,
                        default,
                        default,
                        default,
                        true, 
                        false) &&
                  GetUnitSpellCastRange(
                        out _Range, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        0) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget, 
                        Self, 
                        default, 
                        true, 
                        SelfPosition, 
                        250, 
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :Cast Spells
                  (
                        // Sequence name :Q
                        (
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
                              TestLineMissilePathIsClear(
                                    Self, 
                                    CurrentClosestTarget, 
                                    80, 
                                    AffectEnemies | AffectHeroes | AffectMinions) &&
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
                                   default , 
                                    false)
                        ) ||
                        // Sequence name :E
                        (
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
                                    CastSpellTimeThreshold, 
                                   default , 
                                    false)

                        )
                  )
            );
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;
    }
}

