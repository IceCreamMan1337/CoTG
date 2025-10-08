using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Sona_HealClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
    private LastHitClass lastHit = new LastHitClass();

    public bool Sona_Heal(
          out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget

         )
    {
        int CurrentSpellCast = default;
       AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;

        bool result =
            // Sequence name :Selector
            (
                  // Sequence name :SonaHeal
                  (
                        GetUnitCurrentHealth(
                              out Health,
                              Self) &&
                        GetUnitMaxHealth(
                              out MaxHealth,
                              Self) &&
                        DivideFloat(
                              out HP_Ratio,
                              Health,
                              MaxHealth) &&
                        LessFloat(
                              HP_Ratio,
                              0.55f) &&
                        GetUnitPosition(
                              out SonaPosition,
                              Self) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :CastW
                              (
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :PowerChordDebuff
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaAriaOfPerseveranceCheck",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SonaPosition,
                                          600,
                                          AffectEnemies | AffectHeroes) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          NearestEnemyToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :PowerChordSlow
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaSongOfDiscordCheck",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SonaPosition,
                                          600,
                                          AffectEnemies | AffectHeroes) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          NearestEnemyToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :CastE
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              )
                        )
                  ) ||
                  // Sequence name :HealFriendlies
                  (
                        GetUnitPosition(
                              out SonaPosition,
                              Self) &&
                        GetUnitAILowestHPTargetInArea(
                              out SelectedFriendlyTarget,
                              Self, default
                              ,
                              true,
                              SonaPosition,
                              1000,
                              0.6f,
                              false,
                              true,
                              AffectFriends | AffectHeroes | NotAffectSelf) &&
                        GetUnitPosition(
                              out SelectedFriendlyTargetPosition,
                              SelectedFriendlyTarget) &&
                        // Sequence name :Selector
                        (
                              // Sequence name :CastW
                              (
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
                                          SelectedFriendlyTarget,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :PowerChordDebuff
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaAriaOfPerseveranceCheck",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          400,
                                          AffectEnemies | AffectHeroes) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          NearestEnemyToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :PowerChordSlow
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaSongOfDiscordCheck",
                                          true) &&
                                    GetUnitAIClosestTargetInArea(
                                          out NearestEnemyToAttack,
                                          Self, default
                                          ,
                                          true,
                                          SelectedFriendlyTargetPosition,
                                          400,
                                          AffectEnemies | AffectHeroes) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          NearestEnemyToAttack,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :CastE
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
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)

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

