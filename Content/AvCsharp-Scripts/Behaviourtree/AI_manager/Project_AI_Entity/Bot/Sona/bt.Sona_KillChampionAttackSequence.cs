using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Sona_KillChampionAttackSequenceClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();
     public bool Sona_KillChampionAttackSequence(
         out AttackableUnit __IssuedAttackTarget,
      out bool __IssuedAttack,
      out AttackableUnit _CurrentSpellCastTarget,
      out int _CurrentSpellCast,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      AttackableUnit Target,
      int KillChampionScore,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int FlashSlot,
      int GhostSlot,
      int IgniteSlot,
      bool IsDominionGameMode
         )
      {
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        bool _IssuedAttack = IssuedAttack;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
            // Sequence name :SonaKillChampion
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :SonaKillChampion
                        (
                              // Sequence name :PowerChordDamage
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaHymnOfValorCheck",
                                          true) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :CastR
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Target,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          3,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :CastQ
                              (
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
                                          Target,
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :PowerChordSlow
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaSongOfDiscordCheck",
                                          true) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
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
                                          Target,
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :AutoAttackOnlyOnKill
                              (
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              )
                        )
                  ) ||
                  // Sequence name :OtherSpells
                  (
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        // Sequence name :SonaKillChampion
                        (
                              // Sequence name :PowerChordDamage
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaHymnOfValorCheck",
                                          true) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :CastQ
                              (
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
                                          Target,
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :PowerChordSlow
                              (
                                    TestUnitHasBuff(
                                          Self, default
                                          ,
                                          "SonaSongOfDiscordCheck",
                                          true) &&
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)
                              ) ||
                              // Sequence name :CastR
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          3,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          PreviousSpellCastTarget,
                                          Target,
                                          PreviousSpellCast,
                                          false,
                                          false) &&
                                    PredictLineMissileCastPosition(
                                          out SkillShotCastPosition,
                                          Target,
                                          SelfPosition,
                                          1800,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition, default
                                          ) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          3,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :CastE
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
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
                                          Target,
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :AutoAttackOnlyOnKill
                              (
                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)

                              )
                        )
                  )
            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;


    }
}

