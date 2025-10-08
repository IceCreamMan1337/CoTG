using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Alistar_KillChampionAttackSequenceClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    private AutoAttackClass autoAttack = new AutoAttackClass();

    public bool Alistar_KillChampionAttackSequence(
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

        bool result =// Sequence name :AlistarKillChampion
        (
                  // Sequence name :Headbutt
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
                        // Sequence name :Conditions
                        (
                              // Sequence name :KnockEnemyTowardBase
                              (
                                    GetUnitAIBasePosition(
                                          out BasePosition,
                                          Self) &&
                                    DistanceBetweenObjectAndPoint(
                                          out SelfDistanceToBase,
                                          Self,
                                          BasePosition) &&
                                    DistanceBetweenObjectAndPoint(
                                          out TargetDistanceToBase,
                                          Target,
                                          BasePosition) &&
                                    AddFloat(
                                          out TargetDistanceToBase,
                                          TargetDistanceToBase,
                                          75) &&
                                    GreaterFloat(
                                          SelfDistanceToBase,
                                          TargetDistanceToBase)
                              ) ||
                              // Sequence name :CanComboWithQ
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
                                    GetDistanceBetweenUnits(
                                          out DistanceToTarget,
                                          Target,
                                          Self) &&
                                    GreaterFloat(
                                          DistanceToTarget,
                                          350) &&
                                    GetUnitSpellCost(
                                          out QCost,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          0) &&
                                    GetUnitSpellCost(
                                          out WCost,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1) &&
                                    AddFloat(
                                          out ComboCost,
                                          QCost,
                                          WCost) &&
                                    GetUnitCurrentPAR(
                                          out CurrentMana,
                                          Self,
                                        PrimaryAbilityResourceType.MANA) &&
                                    GreaterEqualFloat(
                                          CurrentMana,
                                          ComboCost)
                              ) ||
                              // Sequence name :CanKillTarget
                              (
                                    GetUnitSpellLevel(
                                          out WLevel,
                                          Self,
                                          SPELLBOOK_CHAMPION,
                                          1) &&
                                    MultiplyFloat(
                                          out WKillThreshold,
                                          WLevel,
                                          50) &&
                                    GetUnitCurrentHealth(
                                          out TargetCurrentHealth,
                                          Target) &&
                                    LessFloat(
                                          TargetCurrentHealth,
                                          WKillThreshold)
                              )
                        ) &&
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
                              default,
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
                              CastSpellTimeThreshold,
                              default,
                              false)
                  ) ||
                  // Sequence name :RepositionIfTargetStunnedAndHeadbuttUp
                  (
                        TestUnitHasAnyBuffOfType(
                              Target,
                            BuffType.STUN  ,
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
                        GetUnitAIBasePosition(
                              out BasePosition,
                              Self) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        CalculatePointOnLine(
                              out MoveToPosition,
                              TargetPosition,
                              BasePosition,
                              -100) &&
                        IssueMoveToPositionOrder(
                              MoveToPosition)
                  ) ||
                  // Sequence name :UseUltimate
                  (
                        GreaterInt(
                              KillChampionScore,
                              5) &&
                        GetDistanceBetweenUnits(
                              out Distance,
                              Target,
                              Self) &&
                        LessEqualFloat(
                              Distance,
                              300) &&
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
                  // Sequence name :UseShurelyas
                  (
                        GreaterInt(
                              KillChampionScore,
                              5) &&
                        TestUnitAICanUseItem(
                              3069) &&
                        GetUnitHealthRatio(
                              out TargetHealthRatio,
                              Target) &&
                        LessFloat(
                              TargetHealthRatio,
                              0.2f) &&
                        IssueUseItemOrder(
                              3069,default
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

