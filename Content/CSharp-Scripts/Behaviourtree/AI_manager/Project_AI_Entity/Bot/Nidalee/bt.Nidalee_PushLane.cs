using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Nidalee_PushLaneClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Nidalee_PushLane(
               out float __CastSpellTimeThreshold,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      AttackableUnit Target,
      float CastSpellTimeThreshold,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      bool SpellStall
         )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;


        bool result =
                    // Sequence name :CastW

                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    // Sequence name :MovementSpells
                    (
                          // Sequence name :IsNearEnemyAndIsCougar
                          (
                                GetUnitAIClosestTargetInArea(
                                      out CurrentClosestTarget,
                                      Self,
                                     default,
                                      true,
                                      SelfPosition,
                                      1400,
                                      AffectEnemies | AffectHeroes) &&
                                TestUnitHasBuff(
                                      Self,
                                     default,
                                      "AspectOfTheCougar",
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
                                      false) &&
                                SetVarBool(
                                      out SpellStall,
                                      true)
                          ) ||
                          // Sequence name :IsNotCougar
                          (
                                TestUnitHasBuff(
                                      Self,
                                     default,
                                      "AspectOfTheCougar",
                                      false) &&
                                CountUnitsInTargetArea(
                                      out EnemyChampCount,
                                      Self,
                                      SelfPosition,
                                      1000,
                                      AffectEnemies | AffectHeroes,
                                      "") &&
                                EnemyChampCount == 0 &&
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
                                      false) &&
                                SetVarBool(
                                      out SpellStall,
                                      true)
                          ) ||
                          // Sequence name :IsCougar
                          (
                                TestUnitHasBuff(
                                      Self,
                                     default,
                                      "AspectOfTheCougar",
                                      true) &&
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
                                      CastSpellTimeThreshold,
                                     default,
                                      false) &&
                                SetVarBool(
                                      out SpellStall,
                                      true)

                          )
                    )
              ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        return result;
    }
}

