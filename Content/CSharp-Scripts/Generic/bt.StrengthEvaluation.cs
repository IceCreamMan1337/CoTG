using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map8;


class StrengthEvaluationClass : CommonAI
{


    public bool StrengthEvaluation(
    out float EnemyStrength,
     out float FriendlyStrength,
   Vector3 Position,
     AttackableUnit ReferenceUnit,
   float Radius)
    {
        float _EnemyStrength = default;
        float _FriendlyStrength = default;
        var getUnitHealthPercentage = new GetUnitHealthPercentageClass();
        bool result =
                  // Sequence name :Sequence

                  GetUnitsInTargetArea(
                        out UnitsToEval,
                        ReferenceUnit,
                        Position,
                        Radius,
                        AffectEnemies | AffectFriends | AffectHeroes | AffectMinions
                        ) &&
                  SetVarFloat(
                        out _EnemyStrength,
                        1) &&
                  SetVarFloat(
                        out _FriendlyStrength,
                        1) &&
                  GetUnitTeam(
                        out MyTeam,
                        ReferenceUnit) &&
                  ForEach(UnitsToEval, Unit =>
                              // Sequence name :Sequence

                              SetVarFloat(
                                    out UnitScore,
                                    0) &&
                              GetUnitType(
                                    out UnitType,
                                    Unit) &&
                              GetUnitTeam(
                                    out UnitTeam,
                                    Unit) &&
                              // Sequence name :UnitScoreSetting
                              (
                                    // Sequence name :Hero
                                    (
                                          UnitType == HERO_UNIT &&
                                          getUnitHealthPercentage.GetUnitHealthPercentage(
                                                out CurrentHealth,
                                                out MaxHealth,
                                                out HealthPercent,
                                                Unit) &&
                                          MultiplyFloat(
                                                out UnitScore,
                                                HealthPercent,
                                                15) &&
                                          AddFloat(
                                                out UnitScore,
                                                UnitScore,
                                                5)
                                    ) ||
                                    // Sequence name :Minion
                                    (
                                          UnitType == MINION_UNIT &&
                                          SetVarFloat(
                                                out UnitScore,
                                                2)
                                    )
                              ) &&
                              // Sequence name :Enemy_Friendly_Score
                              (
                                    // Sequence name :Friendly
                                    (
                                          UnitTeam == MyTeam &&
                                          AddFloat(
                                                out _FriendlyStrength,
                                                UnitScore,
                                                _FriendlyStrength)
                                    ) ||
                                    // Sequence name :Enemy
                                    (
                                          NotEqualUnitTeam(
                                                MyTeam,
                                                UnitTeam) &&
                                          AddFloat(
                                                out _EnemyStrength,
                                                UnitScore,
                                                _EnemyStrength)

                                    )
                              )

                  )
            ;


        EnemyStrength = _EnemyStrength;
        FriendlyStrength = _FriendlyStrength;
        return result;
    }
}

