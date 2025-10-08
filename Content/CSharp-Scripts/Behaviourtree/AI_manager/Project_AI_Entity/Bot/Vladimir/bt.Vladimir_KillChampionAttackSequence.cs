using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class Vladimir_KillChampionAttackSequenceClass : AI_Characters
{
    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private AutoAttackClass autoAttack = new();
    private LastHitClass lastHit = new();

    public bool Vladimir_KillChampionAttackSequence(
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
                  // Sequence name :VladimirKillChampion

                  // Sequence name :UseUltimate
                  (
                        GreaterInt(
                              KillChampionScore,
                              5) &&
                        GetUnitSpellRadius(
                              out SpellRadiusR,
                              Self,
                              SPELLBOOK_CHAMPION,
                              3) &&
                        GetUnitPosition(
                              out TargetPosition,
                              Target) &&
                        // Sequence name :Conditions
                        (
                              // Sequence name :MultipleChampionsNearby
                              (
                                    CountUnitsInTargetArea(
                                          out EnemyChampCount,
                                          Self,
                                          TargetPosition,
                                          SpellRadiusR,
                                          AffectEnemies | AffectHeroes,
                                          "") &&
                                    GreaterInt(
                                          EnemyChampCount,
                                          1)
                              ) ||
                              // Sequence name :TargetIs&lt,50%Health
                              (
                                    GetUnitHealthRatio(
                                          out TargetHealthRatio,
                                          Target) &&
                                    LessFloat(
                                          TargetHealthRatio,
                                          0.5f)
                              )
                        ) &&
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
                  // Sequence name :CastE
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
                              CastSpellTimeThreshold, default
                              ,
                              false)
                  ) ||
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
                              // Sequence name :TargetIs&lt,25%Health

                              GetUnitHealthRatio(
                                    out TargetHealthRatio,
                                    Target) &&
                              LessFloat(
                                    TargetHealthRatio,
                                    0.25f)
                         &&
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
                  // Sequence name :ChaseWithSanguinePool
                  (
                        TestUnitHasBuff(
                              Self, default
                              ,
                              "VladimirSanguinePool",
                              true) &&
                        IssueMoveToUnitOrder(
                              Target)
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

