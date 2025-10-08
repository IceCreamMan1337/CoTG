using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Swain_KillChampionAttackSequenceClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerIgniteClass summonerIgnite = new();
    public bool Swain_KillChampionAttackSequence(
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
                  // Sequence name :SwainKillChampion

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :SwainCastSpell
                        (
                                    // Sequence name :ToggleOffConditions

                                    // Sequence name :NoValidTarget
                                    (
                                          GetUnitPosition(
                                                out SelfPosition,
                                                Self) &&
                                          CountUnitsInTargetArea(
                                                out UnitsNearby,
                                                Self,
                                                SelfPosition,
                                                700,
                                                AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                                "") &&
                                          UnitsNearby == 0 &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                    // Sequence name :Health&gt,60%Mana&lt,20%
                                    (
                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          GreaterFloat(
                                                HP_Ratio,
                                                0.6f) &&
                                          GetUnitPARRatio(
                                                out SelfPAR_Ratio,
                                                Self,
                                                 PrimaryAbilityResourceType.MANA) &&
                                          LessFloat(
                                                SelfPAR_Ratio,
                                                0.2f) &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                    // Sequence name :Health&gt,80%
                                    (
                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          GreaterFloat(
                                                HP_Ratio,
                                                0.8f) &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                               ||
                              // Sequence name :RavenousFlock
                              (
                                          // Sequence name :Health&lt,70%Mana&gt,25%

                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          LessFloat(
                                                HP_Ratio,
                                                0.7f) &&
                                          GetUnitPARRatio(
                                                out SelfPAR_Ratio,
                                                Self,
                                                 PrimaryAbilityResourceType.MANA) &&
                                          GreaterEqualFloat(
                                                SelfPAR_Ratio,
                                                0.25f)
                                     &&
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
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          600) &&
                                          // Sequence name :ToggleSpell

                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "SwainMetamorphism",
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

                              ) ||
                              // Sequence name :DecrepifyNearUnit
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          200) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
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
                                          0,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :NevermoveHighValue
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
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
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :TormentHighValue
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :IgniteHighValue
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                   summonerIgnite.SummonerIgnite(
                                          Self,
                                          Target,
                                          IgniteSlot)
                              ) ||
                                     // Sequence name :AutoAttackOnlyOnKill

                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)

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
                        // Sequence name :SwainCastSpell
                        (
                                    // Sequence name :ToggleOffConditions

                                    // Sequence name :NoValidTarget
                                    (
                                          GetUnitPosition(
                                                out SelfPosition,
                                                Self) &&
                                          CountUnitsInTargetArea(
                                                out UnitsNearby,
                                                Self,
                                                SelfPosition,
                                                700,
                                                AffectEnemies | AffectHeroes | AffectMinions | AffectNeutral,
                                                "") &&
                                          UnitsNearby == 0 &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                    // Sequence name :Health&gt,60%Mana&lt,20%
                                    (
                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          GreaterFloat(
                                                HP_Ratio,
                                                0.6f) &&
                                          GetUnitPARRatio(
                                                out SelfPAR_Ratio,
                                                Self,
                                                 PrimaryAbilityResourceType.MANA) &&
                                          LessFloat(
                                                SelfPAR_Ratio,
                                                0.2f) &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                    // Sequence name :Health&gt,80%
                                    (
                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          GreaterFloat(
                                                HP_Ratio,
                                                0.8f) &&
                                          GetUnitPosition(
                                                out SelfPosition,
                                                Self) &&
                                                // Sequence name :ToggleSpell

                                                TestUnitHasBuff(
                                                      Self, default
                                                      ,
                                                      "SwainMetamorphism",
                                                      true) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                               ||
                              // Sequence name :RavenousFlock
                              (
                                          // Sequence name :Health&lt,70%Mana&gt,25%

                                          GetUnitHealthRatio(
                                                out HP_Ratio,
                                                Self) &&
                                          LessFloat(
                                                HP_Ratio,
                                                0.7f) &&
                                          GetUnitPARRatio(
                                                out SelfPAR_Ratio,
                                                Self,
                                                 PrimaryAbilityResourceType.MANA) &&
                                          GreaterEqualFloat(
                                                SelfPAR_Ratio,
                                                0.25f)
                                     &&
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          600) &&
                                          // Sequence name :ToggleSpell

                                          TestUnitHasBuff(
                                                Self, default
                                                ,
                                                "SwainMetamorphism",
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

                              ) ||
                              // Sequence name :DecrepifyHighValue
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
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
                                          0,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :TormentHighValue
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
                                          2,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :NevermoveHighValue
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
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
                                          1000,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          0.3f) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :IgniteHighValue
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                  summonerIgnite.SummonerIgnite(
                                          Self,
                                          Target,
                                          IgniteSlot)
                              ) ||
                              // Sequence name :DecrepifyNearUnit
                              (
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Target,
                                          Self) &&
                                    LessFloat(
                                          Distance,
                                          200) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
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
                                          0,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :NevermoveSlowedTarget
                              (
                                    TestUnitHasBuff(
                                          Target, default
                                          ,
                                          "SwainBeamDamage",
                                          true) &&
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
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
                                          1000,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          0.3f) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                              // Sequence name :Torment
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          2,
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
                                          2,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :Decrepify
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          0,
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
                                          0,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold, default
                                          ,
                                          false)
                              ) ||
                              // Sequence name :Nevermove
                              (
                                    canCastChampionAbilityClass.CanCastChampionAbility(
                                          Self,
                                          1,
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
                                          1000,
                                          0.25f) &&
                                    GetRandomPositionBetweenTwoPoints(
                                          out SkillShotCastPosition,
                                          TargetPosition,
                                          SkillShotCastPosition,
                                          0.3f) &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out CastSpellTimeThreshold,
                                          Self,
                                          Target,
                                          1,
                                          1,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          SkillShotCastPosition,
                                          true)
                              ) ||
                                     // Sequence name :AutoAttackOnlyOnKill

                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)


                        )
                  )
            ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __IssuedAttackTarget = _IssuedAttackTarget;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __IssuedAttack = _IssuedAttack;
        return result;



    }
}

