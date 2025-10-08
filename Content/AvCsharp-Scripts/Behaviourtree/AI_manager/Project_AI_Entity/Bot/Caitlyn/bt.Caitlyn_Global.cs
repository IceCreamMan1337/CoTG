using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Caitlyn_GlobalClass : AI_Characters 
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();

    public bool Caitlyn_Global(
               out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      bool SpellStall
         )
    {

        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
      bool _SpellStall = SpellStall;

        bool result =
              // Sequence name :Sequence
              (
                    GetUnitPosition(
                          out CaitlynPosition,
                          Self) &&
                    GetUnitSpellCastRange(
                          out UltRange,
                          Self,
                          SPELLBOOK_CHAMPION,
                          3) &&
                    SubtractFloat(
                          out UltRange,
                          UltRange,
                          100) &&
                    GetUnitsInTargetArea(
                          out EnemyChampionsInArea,
                          Self,
                          CaitlynPosition,
                          UltRange,
                          AffectEnemies | AffectHeroes) &&
                    GetUnitSpellLevel(
                          out UltLevel,
                          Self,
                          SPELLBOOK_CHAMPION,
                          3) &&
                    GreaterInt(
                          UltLevel,
                          0) &&
                    GetSpellSlotCooldown(
                          out UltCooldown,
                          Self,
                          SPELLBOOK_CHAMPION,
                          3) &&
                    LessEqualFloat(
                          UltCooldown,
                          0) &&
                    ForEach(EnemyChampionsInArea, Target => (
                          // Sequence name :UseUltimate
                          (
                                TestUnitIsVisibleToTeam(
                                      Self,
                                      Target,
                                      true) &&
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
                                GetUnitCurrentHealth(
                                      out TargetHealth,
                                      Target) &&
                                GetDistanceBetweenUnits(
                                      out DistanceToTarget,
                                      Self,
                                      Target) &&
                                GreaterFloat(
                                      DistanceToTarget,
                                      600) &&
                                GetUnitPosition(
                                      out TargetPosition,
                                      Target) &&
                                CountUnitsInTargetArea(
                                      out FriendlyChampCount,
                                      Self,
                                      TargetPosition,
                                      1000,
                                      AffectFriends | AffectHeroes | NotAffectSelf,
                                      "") &&
                                SetVarFloat(
                                      out UltThreshold,
                                      0) &&
                                // Sequence name :KillConditions
                                (
                                      // Sequence name :Level1Ult
                                      (
                                            UltLevel == 1 &&
                                            MultiplyFloat(
                                                  out UltThresholdOffset,
                                                  FriendlyChampCount,
                                                  30) &&
                                            AddFloat(
                                                  out UltThreshold,
                                                  UltThresholdOffset,
                                                  250)
                                      ) ||
                                      // Sequence name :Level2Ult
                                      (
                                            UltLevel == 2 &&
                                            MultiplyFloat(
                                                  out UltThresholdOffset,
                                                  FriendlyChampCount,
                                                  40) &&
                                            AddFloat(
                                                  out UltThreshold,
                                                  UltThresholdOffset,
                                                  475)
                                      ) ||
                                      // Sequence name :Level3Ult
                                      (
                                            UltLevel == 3 &&
                                            MultiplyFloat(
                                                  out UltThresholdOffset,
                                                  FriendlyChampCount,
                                                  50) &&
                                            AddFloat(
                                                  out UltThreshold,
                                                  UltThresholdOffset,
                                                  700)
                                      )
                                ) &&
                                LessFloat(
                                      TargetHealth,
                                      UltThreshold) &&
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
                                     default,
                                      false) &&
                                SetVarBool(
                                      out SpellStall,
                                      true)

                          ))
                    )
              );

         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __SpellStall = _SpellStall;
        return result;
    }
}

