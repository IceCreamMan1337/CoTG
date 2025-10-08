using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerGarrisonClass : AI_Characters 
{
      

     public bool SummonerGarrison(
         AttackableUnit Self,
      int GarrisonSlot,
      Vector3 TargetPosition
         )
      {
        return
              // Sequence name :Sequence
              (
                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                               EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                          ||
                          TestEntityDifficultyLevel(
                                true,
                                EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    GreaterEqualInt(
                          GarrisonSlot,
                          0) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          GarrisonSlot,
                          true) &&
                    GetUnitsInTargetArea(
                          out Minions,
                          Self,
                          TargetPosition,
                          500,
                          AffectEnemies | AffectFriends | AffectMinions | AffectUseable) &&
                    ForEach(Minions, Minion => (
                          // Sequence name :Sequence
                          (
                                GetUnitBuffCount(
                                      out Count,
                                      Minion,
                                      "OdinGuardianStatsByLevel") &&
                                GreaterInt(
                                      Count,
                                      0) &&
                                SetVarAttackableUnit(
                                      out TargetCapturePoint,
                                      Minion)
                          ))
                    ) &&
                    GetUnitTeam(
                          out CapturePointTeam,
                          TargetCapturePoint) &&
                    GetUnitTeam(
                          out MyTeam,
                          Self) &&
                    // Sequence name :Selector
                    (
                          // Sequence name :DefensiveUse
                          (
                                MyTeam == CapturePointTeam &&
                                GetUnitMaxPAR(
                                      out MaxPAR,
                                      TargetCapturePoint,
                                     PrimaryAbilityResourceType.MANA) &&
                                GetUnitCurrentPAR(
                                      out CurrentPAR,
                                      TargetCapturePoint,
                                      PrimaryAbilityResourceType.MANA) &&
                                DivideFloat(
                                      out CaptureRate,
                                      CurrentPAR,
                                      MaxPAR) &&
                                LessFloat(
                                      CaptureRate,
                                      0.5f)
                          ) ||
                          // Sequence name :OffensiveUse
                          (
                                NotEqualUnitTeam(
                                      MyTeam,
                                      CapturePointTeam) &&
                                NotEqualUnitTeam(
                                      CapturePointTeam,
                                     TeamId.TEAM_NEUTRAL) &&
                                GetUnitPosition(
                                      out TargetPosition,
                                      TargetCapturePoint) &&
                                CountUnitsInTargetArea(
                                      out EnemyCount,
                                      Self,
                                      TargetPosition,
                                      900,
                                      AffectEnemies | AffectHeroes,
                                      "") &&
                                CountUnitsInTargetArea(
                                      out AllyCount,
                                      Self,
                                      TargetPosition,
                                      900,
                                      AffectFriends | AffectHeroes | AlwaysSelf,
                                      "") &&
                                AllyCount == EnemyCount
                          )
                    ) &&
                    GetUnitBuffCount(
                          out GarrisonBuffCount,
                          TargetCapturePoint,
                          "SummonerOdinGarrison") &&
                    GetUnitBuffCount(
                          out GarrisonDebuffCount,
                          TargetCapturePoint,
                          "SummonerOdinGarrisonDebuff") &&
                    AddInt(
                          out TotalGarrisonBuffCount,
                          GarrisonDebuffCount,
                          GarrisonBuffCount) &&
                    TotalGarrisonBuffCount == 0 &&
                    SetAIUnitSpellTarget(
                          TargetCapturePoint,
                          SPELLBOOK_SUMMONER,
                          GarrisonSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          GarrisonSlot,
                          default,
                          default)

              );
      }
}

