using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Maokai_HighThreatManagementClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();


     public bool Maokai_HighThreatManagement(
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
            // Sequence name :UseSpells
            (
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        450,
                        AffectEnemies | AffectHeroes) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :R
                        (
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio,
                                    Self,
                                     PrimaryAbilityResourceType.MANA) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :RIsUPandMaokaiHasMana
                                    (
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "MaokaiDrain",
                                                true) &&
                                          GreaterFloat(
                                                SelfPAR_Ratio,
                                                0.3f) &&
                                          LessFloat(
                                                MaokaiPAR_Ratio,
                                                0.3f)
                                    ) ||
                                    // Sequence name :RIsNotUP
                                    (
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "MaokaiDrain",
                                                false) &&
                                          GreaterFloat(
                                                SelfPAR_Ratio,
                                                0.4f) &&
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
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)
                                    ) ||
                                    // Sequence name :RIsUPandMaokaiDoesn'tHaveMana
                                    (
                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "MaokaiDrain",
                                                true) &&
                                          LessFloat(
                                                SelfPAR_Ratio,
                                                0.3f) &&
                                          canCastChampionAbilityClass.CanCastChampionAbility(
                                                Self,
                                                3,
                                                PreviousSpellCastTime,
                                                CastSpellTimeThreshold,
                                                PreviousSpellCastTarget,
                                                Self,
                                                PreviousSpellCast,
                                                true,
                                                false) &&
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
                                                CastSpellTimeThreshold, default
                                                ,
                                                false)

                                    )
                              )
                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        
       _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
      
        return result;

    }
}

