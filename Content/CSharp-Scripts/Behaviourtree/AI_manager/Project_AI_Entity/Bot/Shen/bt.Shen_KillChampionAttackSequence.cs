using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Shen_KillChampionAttackSequenceClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();
    private SummonerGhostClass summonerGhost = new();

    // private Shen_CanCastChampionAbilityClass shen_CanCastChampionAbility = new Shen_CanCastChampionAbilityClass()

    public bool Shen_KillChampionAttackSequence(
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
                  // Sequence name :Selector

                  // Sequence name :BeginnerSpellsDominion
                  (
                        TestEntityDifficultyLevel(
                              true,
                              EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                        IsDominionGameMode == true &&
                        // Sequence name :ShenKillChampion
                        (
                              // Sequence name :Ghost
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.4f) &&
                                  summonerGhost.SummonerGhost(
                                          Self,
                                          GhostSlot)
                              ) ||
                              // Sequence name :ShadowDash
                              (
                                          // Sequence name :EitherHighEnergyOrKill

                                          GreaterInt(
                                                KillChampionScore,
                                                5)
                                          &&
                                                // Sequence name :HighHP

                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                GetUnitHealthRatio(
                                                      out MyHealthRatio,
                                                      Self) &&
                                                SubtractFloat(
                                                      out HealthRatioDiff,
                                                      MyHealthRatio,
                                                      TargetHealthRatio) &&
                                                GreaterEqualFloat(
                                                      HealthRatioDiff,
                                                      0.2f) &&
                                                GetUnitPARRatio(
                                                      out PAR_Ratio,
                                                      Self,
                                                      PrimaryAbilityResourceType.Energy) &&
                                                GreaterFloat(
                                                      PAR_Ratio,
                                                      0.7f)

                                     &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessEqualFloat(
                                          DistanceToTarget,
                                          400) &&
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
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          150,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out TauntPosition) &&
                                    SetAIUnitSpellTargetLocation(
                                          TauntPosition,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    ClearUnitAISpellPosition() &&
                                  castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out PreviousSpellCastTime,
                                          Self,
                                          Target,
                                          2,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          TauntPosition,
                                          true)
                              ) ||
                              // Sequence name :Feint
                              (
                                          // Sequence name :GetRandomChance

                                          GenerateRandomFloat(
                                                out RandomFloat,
                                                0,
                                                100) &&
                                          LessFloat(
                                                RandomFloat,
                                                25)
                                     &&
                                    GetUnitPARRatio(
                                          out PAR_Ratio,
                                          Self,
                                          PrimaryAbilityResourceType.Energy) &&
                                    GreaterFloat(
                                          PAR_Ratio,
                                          0.6f) &&
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
                              // Sequence name :VorpalStrike
                              (
                                          // Sequence name :EitherHighEnergyOrKill

                                          GreaterInt(
                                                KillChampionScore,
                                                5)
                                          &&
                                                // Sequence name :HighEnergy

                                                GetUnitPARRatio(
                                                      out PAR_Ratio,
                                                      Self,
                                                      PrimaryAbilityResourceType.Energy) &&
                                                GreaterFloat(
                                                      PAR_Ratio,
                                                      0.7f)

                                     &&
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

                              // Sequence name :ShenKillChampion

                              // Sequence name :Ghost
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.4f) &&
                               summonerGhost.SummonerGhost(
                                          Self,
                                          GhostSlot)
                              ) ||
                              // Sequence name :Feint
                              (
                                    GreaterInt(
                                          KillChampionScore,
                                          5) &&
                                    GetUnitPARRatio(
                                          out PAR_Ratio,
                                          Self,
                                          PrimaryAbilityResourceType.Energy) &&
                                    GreaterFloat(
                                          PAR_Ratio,
                                          0.6f) &&
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
                              // Sequence name :ShadowDash
                              (
                                          // Sequence name :EitherHighEnergyOrKill

                                          GreaterInt(
                                                KillChampionScore,
                                                5)
                                          &&
                                                // Sequence name :HighHP

                                                GetUnitHealthRatio(
                                                      out TargetHealthRatio,
                                                      Target) &&
                                                GetUnitHealthRatio(
                                                      out MyHealthRatio,
                                                      Self) &&
                                                SubtractFloat(
                                                      out HealthRatioDiff,
                                                      MyHealthRatio,
                                                      TargetHealthRatio) &&
                                                GreaterEqualFloat(
                                                      HealthRatioDiff,
                                                      0.2f) &&
                                                GetUnitPARRatio(
                                                      out PAR_Ratio,
                                                      Self,
                                                      PrimaryAbilityResourceType.Energy) &&
                                                GreaterFloat(
                                                      PAR_Ratio,
                                                      0.7f)

                                     &&
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Self,
                                          Target) &&
                                    LessEqualFloat(
                                          DistanceToTarget,
                                          400) &&
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
                                    ClearUnitAISpellPosition() &&
                                    ComputeUnitAISpellPosition(
                                          Target,
                                          Self,
                                          150,
                                          false) &&
                                    GetUnitAISpellPosition(
                                          out TauntPosition) &&
                                    SetAIUnitSpellTargetLocation(
                                          TauntPosition,
                                          SPELLBOOK_CHAMPION,
                                          2) &&
                                    ClearUnitAISpellPosition() &&
                                   castTargetAbility.CastTargetAbility(
                                          out CurrentSpellCast,
                                          out CurrentSpellCastTarget,
                                          out PreviousSpellCastTime,
                                          out PreviousSpellCastTime,
                                          Self,
                                          Target,
                                          2,
                                          0.8f,
                                          PreviousSpellCast,
                                          PreviousSpellCastTarget,
                                          PreviousSpellCastTime,
                                          CastSpellTimeThreshold,
                                          TauntPosition,
                                          true)
                              ) ||
                              // Sequence name :VorpalStrike
                              (
                                          // Sequence name :EitherHighEnergyOrKill

                                          GreaterInt(
                                                KillChampionScore,
                                                5) &&
                                                // Sequence name :HighEnergy

                                                GetUnitPARRatio(
                                                      out PAR_Ratio,
                                                      Self,
                                                      PrimaryAbilityResourceType.Energy) &&
                                                GreaterFloat(
                                                      PAR_Ratio,
                                                      0.7f)

                                     &&
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
                                     // Sequence name :AutoAttackOnlyOnKill

                                     autoAttack.AutoAttack(
                                          out IssuedAttack,
                                          out IssuedAttackTarget,
                                          Target,
                                          Self,
                                          IssuedAttack,
                                          IssuedAttackTarget)




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

