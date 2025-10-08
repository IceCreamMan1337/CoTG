/*


SAME THAN AIMANAGER/PointEvaluation

using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class PointEvaluation : BehaviourTree 
{
      out float EnemyStrength;
      out float FriendlyStrength;
      Vector3CenterPoint;
      AttackableUnit ReferenceUnit;
      float Radius;
      float ChampionPointValue;
      float MinionPointValue;

      bool PointEvaluation()
      {
      return
            // Sequence name :Evaluate_Strength_Of_Units_In_Area
            (
                  GetUnitsInTargetArea(
                        out TargetCollection, 
                        ReferenceUnit, 
                        CenterPoint, 
                        Radius, 
                        AffectEnemies,AffectFriends,AffectHeroes,AffectMinions, 
                        "") &&
                  SetVarFloat(
                        out EnemyStrength, 
                        0) &&
                  SetVarFloat(
                        out FriendlyStrength, 
                        0) &&
                  GetUnitTeam(
                        out MyTeam, 
                        ReferenceUnit) &&
                  TargetCollection.ForEach( Unit => (
                        // Sequence name :Sequence
                        (
                              GetUnitTeam(
                                    out UnitTeam, 
                                    Unit) &&
                              // Sequence name :MinionHeroOrTurret
                              (
                                    // Sequence name :Minion
                                    (
                                          GetUnitType(
                                                out UnitType, 
                                                Unit) &&
                                          UnitType == MINION_UNIT &&
                                          SetVarFloat(
                                                out Strength, 
                                                MinionPointValue)
                                    ) ||
                                    // Sequence name :Hero
                                    (
                                          GetUnitType(
                                                out UnitType, 
                                                Unit) &&
                                          UnitType == HERO_UNIT &&
                                          SetVarFloat(
                                                out Strength, 
                                                ChampionPointValue)
                                    )
                              ) &&
                              // Sequence name :MyTeamOrEnemyTeam
                              (
                                    // Sequence name :MyTeam
                                    (
                                          MyTeam == UnitTeam &&
                                          AddFloat(
                                                out FriendlyStrength, 
                                                FriendlyStrength, 
                                                Strength)
                                    ) ||
                                    // Sequence name :OtherTeam
                                    (
                                          NotEqualUnitTeam(
                                                MyTeam, 
                                                UnitTeam) &&
                                          AddFloat(
                                                out EnemyStrength, 
                                                EnemyStrength, 
                                                Strength)

                                    )
                              )
                        )
                  )
            );
      }
}

*/