using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class CanCastChampionAbilityClass : AI_Characters 
{
    

     public bool CanCastChampionAbility(
           AttackableUnit Self,
      int ChampionAbilityNumber,
      float LastCastTime,
      float CastTimeThreshold,
      AttackableUnit PreviousSpellCastTarget,
      AttackableUnit Target,
      int PreviousSpellNumber,
      bool IgnoreCastTimeThreshold,
      bool IgnorePARChecks
      )
      {
        return
              // Sequence name :CanCastAChampionAbility
              (
               DebugAction("CanCastAChampionAbility") &&
                    // Sequence name :SameSpellOrTimeCastThresholdOver
                    (
                   
                          IgnoreCastTimeThreshold == true ||
                          // Sequence name :CheckThatEntityCanCastNextSpellBasedOnTime
                          (
                                GetGameTime(
                                      out CurrentTime) &&
                                SubtractFloat(
                                      out TimeDiff,
                                      CurrentTime,
                                      LastCastTime) &&
                                GreaterEqualFloat(
                                      TimeDiff,
                                      CastTimeThreshold)
                          ) ||
                          // Sequence name :SameSpellSameTarget
                          (
                                GreaterEqualInt(
                                      PreviousSpellNumber,
                                      0) &&
                                PreviousSpellNumber == ChampionAbilityNumber &&
                                PreviousSpellCastTarget == Target
                          )
                    ) &&
                     DebugAction("CanCastAbility") &&
                    // Sequence name :CanCastAbility
                    (
                          // Sequence name :Ability0
                          (
                                ChampionAbilityNumber == 0 &&
                                TestCanCastSpell(
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      0,
                                      true) &&
                                GetUnitSpellCost(
                                      out Cost,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      0) &&
                                GetSpellSlotCooldown(
                                      out Cooldown,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      0)
                          ) ||
                          // Sequence name :Ability1
                          (
                                ChampionAbilityNumber == 1 &&
                                TestCanCastSpell(
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      1,
                                      true) &&
                                GetUnitSpellCost(
                                      out Cost,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      1) &&
                                GetSpellSlotCooldown(
                                      out Cooldown,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      1)
                          ) ||
                          // Sequence name :Ability2
                          (
                                ChampionAbilityNumber == 2 &&
                                TestCanCastSpell(
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      2,
                                      true) &&
                                GetUnitSpellCost(
                                      out Cost,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      2) &&
                                GetSpellSlotCooldown(
                                      out Cooldown,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      2)
                          ) ||
                          // Sequence name :Ability3
                          (
                                ChampionAbilityNumber == 3 &&
                                TestCanCastSpell(
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      3,
                                      true) &&
                                GetUnitSpellCost(
                                      out Cost,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      3) &&
                                GetSpellSlotCooldown(
                                      out Cooldown,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      3)
                          )
                    ) &&
                    LessEqualFloat(
                          Cooldown,
                          0) &&
                    GetUnitPARType(
                          out PAR_Type,
                          Self) &&
                                         DebugAction("PARChecks") &&
                    // Sequence name :PARChecks
                    (
                          IgnorePARChecks == true ||
                          // Sequence name :PAR_Check
                          (
                                GetUnitPARType(
                                      out PAR_Type,
                                      Self) &&
                                GetUnitCurrentPAR(
                                      out CurrentPAR,
                                      Self,
                                      PAR_Type) &&
                                GreaterEqualFloat(
                                      CurrentPAR,
                                      Cost)

                          )
                    )
              );


      }
}

