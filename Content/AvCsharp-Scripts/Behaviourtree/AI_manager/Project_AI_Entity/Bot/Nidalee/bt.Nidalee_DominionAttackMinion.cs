using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nidalee_DominionAttackMinionClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
 

     public bool Nidalee_DominionAttackMinion(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      AttackableUnit Target,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {

        int CurrentSpellCast = default;
      AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
       float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        bool result =
            // Sequence name :SpellKillMinion
            (
                  GetUnitPARRatio(
      out SelfPAR_Ratio,
      Self,
  PrimaryAbilityResourceType.MANA) &&
                  // Sequence name :Cast Spells
                  (
                        // Sequence name :TurnIntoCougarIfNoNearbyEnemyChampions
                        (
                              GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              CountUnitsInTargetArea(
                                    out CollectionCount,
                                    Self,
                                    SelfPosition,
                                    1000,
                                    AffectEnemies | AffectHeroes,
                                    "") &&
                              CollectionCount == 0 &&
                              // Sequence name :IsNotCougar
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "AspectOfTheCougar",
                                          false) &&
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
                              )
                        ) ||
                        // Sequence name :Cast Q
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "AspectOfTheCougar",
                                    true) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
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
                                    0,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
                        ) ||
                        // Sequence name :Cast E
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "AspectOfTheCougar",
                                    true) &&
                              GetDistanceBetweenUnits(
                                    out DistanceToMinion,
                                    Target,
                                    Self) &&
                              LessEqualFloat(
                                    DistanceToMinion,
                                    225) &&
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
                                   default,
                                    false)

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

