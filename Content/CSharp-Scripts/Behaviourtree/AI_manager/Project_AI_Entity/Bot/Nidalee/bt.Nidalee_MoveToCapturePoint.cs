using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Nidalee_MoveToCapturePointClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();


    public bool Nidalee_MoveToCapturePoint(
         out float __CastSpellTimeThreshold,
      out int __CurrentSpellCast,
      out AttackableUnit __CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      float CastSpellTimeThreshold,
      int CurrentSpellCast,
      AttackableUnit CurrentSpellCastTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      AttackableUnit Self,
      bool SpellStall
         )
    {


        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int _CurrentSpellCast = CurrentSpellCast;
        AttackableUnit _CurrentSpellCastTarget = CurrentSpellCastTarget;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = SpellStall;


        bool result =
                          // Sequence name :CastW

                          // Sequence name :MovementSpells

                          // Sequence name :IsNearEnemyAndIsCougar
                          (
                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
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

              ;

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __CurrentSpellCast = _CurrentSpellCast;
        __CurrentSpellCastTarget = _CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;
        return result;
    }
}

