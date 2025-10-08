using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class SummonerFortifyClass : AI_Characters 
{
  

     public bool SummonerFortify(
             out string _ActionPerformed,
      AttackableUnit Self,
      int FortifySlot
         )
      {
        string ActionPerformed = default;

        bool result =
              // Sequence name :CheckForTowersToUseFortify
              (
                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                           EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) ||
                                TestEntityDifficultyLevel(
                                true,
                            EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    NotEqualInt(
                          FortifySlot,
                          -1) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          FortifySlot,
                          true) &&
                    GetUnitTeam(
                          out MyTeam,
                          Self) &&
                    GetTurretCollection(
                          out AllTurrets) &&
                    ForEach(AllTurrets, Turret => (
                          // Sequence name :FindATurretInDanger
                          (
                                GetUnitTeam(
                                      out TurretTeam,
                                      Turret) &&
                                MyTeam == TurretTeam &&
                                GetUnitPosition(
                                      out TurretPosition,
                                      Turret) &&
                                CountUnitsInTargetArea(
                                      out EnemyCount,
                                      Self,
                                      TurretPosition,
                                      650,
                                      AffectEnemies | AffectHeroes | AffectMinions,
                                      "") &&
                                GreaterEqualInt(
                                      EnemyCount,
                                      3) &&
                                // Sequence name :Selector
                                (
                                      // Sequence name :NoFriendlyUnits
                                      (
                                            CountUnitsInTargetArea(
                                                  out FriendsCount,
                                                  Self,
                                                  TurretPosition,
                                                  650,
                                                  AffectFriends | AffectHeroes | AffectMinions,
                                                  "") &&
                                            LessEqualInt(
                                                  FriendsCount,
                                                  1)
                                      ) ||
                                      // Sequence name :TurretLowHealth
                                      (
                                            GetUnitHealthRatio(
                                                  out TurretHealthRatio,
                                                  Turret) &&
                                            LessEqualFloat(
                                                  TurretHealthRatio,
                                                  0.2f)
                                      )
                                )
                          ))
                    ) &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          FortifySlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          FortifySlot,
                          default, default
                          ) &&
                    SetVarString(
                          out ActionPerformed,
                          "SummonerFortify")

              );


        _ActionPerformed = ActionPerformed;
        return result;

      }
}

