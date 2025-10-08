using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Nidalee_KillChampionAttackSequenceClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();


    public bool Nidalee_KillChampionAttackSequence(
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
    int GhostSlot,
      int FlashSlot,
      int ExhaustSlot,
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
            // Sequence name :NidaleeSpells
            (
                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                           EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :NidaleeKillChampion
                        (
                              GetDistanceBetweenUnits(
                                    out DistanceToTarget,
                                    Target,
                                    Self) &&
                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              GetUnitPARRatio(
                                    out SelfPAR_Ratio,
                                    Self,
                                     PrimaryAbilityResourceType.MANA) &&
                              GetUnitPosition(
                                    out SelfPosition,
                                    Self) &&
                              GetUnitPosition(
                                    out TargetPosition,
                                    Target) &&
                              // Sequence name :NidaleeSpells
                              (
                                    // Sequence name :NidaleeHumanSequence
                                    (
                                          // Sequence name :HumanConditions
                                          (
                                                GreaterFloat(
                                                      DistanceToTarget,
                                                      325)
                                          ) &&
                                          // Sequence name :CastSpell
                                          (
                                                // Sequence name :IsCougar
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
                                                      GreaterFloat(
                                                            SelfPAR_Ratio,
                                                            0.1f) &&
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
                                                // Sequence name :CastQ
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            false) &&
                                                      TestLineMissilePathIsClear(
                                                            Self,
                                                            Target,
                                                            60,
                                                            AffectEnemies | AffectHeroes | AffectMinions) &&

                                                      canCastChampionAbilityClass.CanCastChampionAbility(
                                                            Self,
                                                            0,
                                                            PreviousSpellCastTime,
                                                            CastSpellTimeThreshold,
                                                            PreviousSpellCastTarget,
                                                            Target,
                                                            PreviousSpellCast,
                                                            true,
                                                            false) &&
                                                      PredictLineMissileCastPosition(
                                                            out SkillShotCastPosition,
                                                            Target,
                                                            SelfPosition,
                                                            1300,
                                                            0.25f) &&
                                                      GetRandomPositionBetweenTwoPoints(
                                                            out NewSkillShotCastPosition,
                                                            TargetPosition,
                                                            SkillShotCastPosition,default
                                                            ) &&
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
                                                            CastSpellTimeThreshold,
                                                            NewSkillShotCastPosition,
                                                            true)
                                                ) ||
                                                // Sequence name :Cast W
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            false) &&
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
                                                            CastSpellTimeThreshold,
                                                            TargetPosition,
                                                            true)
                                                ) ||
                                                // Sequence name :Cast E
                                                (
                                                      GreaterFloat(
                                                            SelfPAR_Ratio,
                                                            0.8f) &&
                                                      GreaterFloat(
                                                            SelfHealthRatio,
                                                            0.8f) &&
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
                                    ) ||
                                    // Sequence name :NidaleeCougarSequence
                                    (
                                          // Sequence name :CougarConditions
                                          (
                                                LessEqualFloat(
                                                      DistanceToTarget,
                                                      325)
                                                ||
                                                LessEqualFloat(
                                                      SelfPAR_Ratio,
                                                      0.2f)
                                          ) &&
                                          // Sequence name :CastSpell
                                          (
                                                // Sequence name :IsNotCougar
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
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
                                                            CastSpellTimeThreshold,
                                                            default,
                                                            false)
                                                ) ||
                                                // Sequence name :Cast W
                                                (
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
                                                      GreaterFloat(
                                                            DistanceToTarget,
                                                            200) &&
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
                                                            CastSpellTimeThreshold,default
                                                            ,
                                                            false)
                                                ) ||
                                                // Sequence name :Cast E
                                                (
                                                      // Sequence name :GetRandomChance
                                                      (
                                                            GenerateRandomFloat(
                                                                  out RandomFloat,
                                                                  100,
                                                                  0) &&
                                                            LessFloat(
                                                                  RandomFloat,
                                                                  50)
                                                      ) &&
                                                      TestUnitHasBuff(
                                                            Self,
                                                            default,
                                                            "AspectOfTheCougar",
                                                            true) &&
                                                      LessEqualFloat(
                                                            DistanceToTarget,
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
                                                )
                                          )
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
                  ) ||
                  // Sequence name :NidaleeKillChampion
                  (
                        GetDistanceBetweenUnits(
                              out DistanceToTarget,
                              Target,
                              Self) &&
                        GetUnitHealthRatio(
                              out TargetHealthRatio,
                              Target) &&
                        GetUnitHealthRatio(
                              out SelfHealthRatio,
                              Self) &&
                        GetUnitPARRatio(
                              out SelfPAR_Ratio,
                              Self,
                               PrimaryAbilityResourceType.MANA) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        // Sequence name :NidaleeSpells
                        (
                              // Sequence name :NidaleeHumanSequence
                              (
                                    // Sequence name :HumanConditions
                                    (
                                          GreaterFloat(
                                                DistanceToTarget,
                                                325)
                                    ) &&
                                    // Sequence name :CastSpell
                                    (
                                          // Sequence name :IsCougar
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                     default,
                                                      "AspectOfTheCougar",
                                                      true) &&
                                                GreaterFloat(
                                                      SelfPAR_Ratio,
                                                      0.1f) &&
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
                                                      CastSpellTimeThreshold,
                                                      default,
                                                      false)
                                          ) ||
                                          // Sequence name :CastQ
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "AspectOfTheCougar",
                                                      false) &&
                                                TestLineMissilePathIsClear(
                                                      Self,
                                                      Target,
                                                      60,
                                                      AffectEnemies | AffectHeroes | AffectMinions) &&
                                                canCastChampionAbilityClass.CanCastChampionAbility(
                                                      Self,
                                                      0,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      PreviousSpellCastTarget,
                                                      Target,
                                                      PreviousSpellCast,
                                                      true,
                                                      false) &&
                                                PredictLineMissileCastPosition(
                                                      out SkillShotCastPosition,
                                                      Target,
                                                      SelfPosition,
                                                      1300,
                                                      0.25f) &&
                                                GetRandomPositionBetweenTwoPoints(
                                                      out NewSkillShotCastPosition,
                                                      TargetPosition,
                                                      SkillShotCastPosition,
                                                      0.2f) &&
                                               castTargetAbility.CastTargetAbility(
                                                      out CurrentSpellCast,
                                                      out CurrentSpellCastTarget,
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      0,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      NewSkillShotCastPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :Cast W
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
                                                      "AspectOfTheCougar",
                                                      false) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Target,
                                                      1,
                                                      1,
                                                      PreviousSpellCast,
                                                      PreviousSpellCastTarget,
                                                      PreviousSpellCastTime,
                                                      CastSpellTimeThreshold,
                                                      TargetPosition,
                                                      true)
                                          ) ||
                                          // Sequence name :Cast E
                                          (
                                                GreaterFloat(
                                                      SelfPAR_Ratio,
                                                      0.8f) &&
                                                GreaterFloat(
                                                      SelfHealthRatio,
                                                      0.8f) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                              ) ||
                              // Sequence name :NidaleeCougarSequence
                              (
                                    // Sequence name :CougarConditions
                                    (
                                          LessEqualFloat(
                                                DistanceToTarget,
                                                325)
                                          ||
                                          LessEqualFloat(
                                                SelfPAR_Ratio,
                                                0.2f)
                                    ) &&
                                    // Sequence name :CastSpell
                                    (
                                          // Sequence name :IsNotCougar
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                      default,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          ) ||
                                          // Sequence name :Cast W
                                          (
                                                TestUnitHasBuff(
                                                      Self,
                                                     default,
                                                      "AspectOfTheCougar",
                                                      true) &&
                                                GreaterFloat(
                                                      DistanceToTarget,
                                                      200) &&
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
                                                      Self,
                                                      Self,
                                                      1,
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
                                                LessEqualFloat(
                                                      DistanceToTarget,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                                      out _PreviousSpellCastTime,
                                                      out _CastSpellTimeThreshold,
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
                                          )
                                    )
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

         __IssuedAttackTarget = _IssuedAttackTarget;
         __IssuedAttack = _IssuedAttack;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;


      }
}

