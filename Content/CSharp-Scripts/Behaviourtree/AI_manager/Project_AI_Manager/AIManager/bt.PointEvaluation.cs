using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees;


class PointEvaluationClass : CommonAI
{


    public bool PointEvaluation(
               out float EnemyStrength,
     out float FriendlyStrength,
   Vector3 CenterPoint,
     AttackableUnit ReferenceUnit,
   float Radius,
   float ChampionPointValue,
   float MinionPointValue)
    {
        float _EnemyStrength = default;
        float _FriendlyStrength = default;
        bool result =
                  // Sequence name :Evaluate_Strength_Of_Units_In_Area

                  GetUnitsInTargetArea(
                        out TargetCollection,
                        ReferenceUnit,
                        CenterPoint,
                        Radius,
                        AffectEnemies | AffectFriends | AffectHeroes | AffectMinions) &&
                  SetVarFloat(
                        out _EnemyStrength,
                        0) &&
                  SetVarFloat(
                        out _FriendlyStrength,
                        0) &&
                  GetUnitTeam(
                        out MyTeam,
                        ReferenceUnit) &&
                  ForEach(TargetCollection, Unit =>
                              // Sequence name :Sequence

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
                                                out _FriendlyStrength,
                                                _FriendlyStrength,
                                                Strength)
                                    ) ||
                                    // Sequence name :OtherTeam
                                    (
                                          NotEqualUnitTeam(
                                                MyTeam,
                                                UnitTeam) &&
                                          AddFloat(
                                                out _EnemyStrength,
                                                _EnemyStrength,
                                                Strength)

                                    )
                              )

                  )
            ;
        FriendlyStrength = _FriendlyStrength;
        EnemyStrength = _EnemyStrength;
        return result;
    }
}

