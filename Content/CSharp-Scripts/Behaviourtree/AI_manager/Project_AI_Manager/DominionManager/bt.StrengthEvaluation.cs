using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees;


class ODINStrengthEvaluationClass : CommonAI
{


    public bool StrengthEvaluation(
               out float EnemyStrength,
     out float FriendlyStrength,
   Vector3 PointPosition,
   AttackableUnit ReferenceUnit,
   float Radius,
   float MinionPointValue,
   float ChampionPointValue,
   float CPPointValue

         )
    {
        float _EnemyStrength = default;
        float _FriendlyStrength = default;



        bool result =
                      // Sequence name :Evaluate_Strength_Of_Units_In_Area

                      GetUnitsInTargetArea(
                            out TargetCollection,
                            ReferenceUnit,
                            PointPosition,
                            Radius,
                            AffectEnemies | AffectFriends | AffectHeroes | AffectMinions | AffectUseable | NotAffectSelf) &&
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
                                        // Sequence name :MinionOrCapturePoint
                                        (
                                              GetUnitType(
                                                    out UnitType,
                                                    Unit) &&
                                              UnitType == MINION_UNIT &&
                                              // Sequence name :Selector
                                              (
                                                    // Sequence name :CapturePoint
                                                    (
                                                          GetUnitBuffCount(
                                                                out IsCP,
                                                                Unit,
                                                                "OdinGuardianStatsByLevel") &&
                                                          GreaterInt(
                                                                IsCP,
                                                                0) &&
                                                          SetVarFloat(
                                                                out Strength,
                                                                CPPointValue)
                                                    ) ||
                                                    SetVarFloat(
                                                          out Strength,
                                                          MinionPointValue)
                                              )
                                        ) ||
                                        // Sequence name :Hero
                                        (
                                              GetUnitType(
                                                    out UnitType,
                                                    Unit) &&
                                              UnitType == HERO_UNIT &&
                                              TestAIEntityHasTask(
                                                    Unit,
                                                    AITaskTopicType.DOMINION_RETURN_TO_BASE,
                                                    null,
                                                    default,
                                                    0,
                                                    false) &&
                                              SetVarFloat(
                                                    out Strength,
                                                    ChampionPointValue) &&
                                              // Sequence name :MaskFailure
                                              (
                                                    // Sequence name :Sequence
                                                    (
                                                          SetVarBool(
                                                                out Run,
                                                                false) &&
                                                          Run == true &&
                                                          TestUnitIsVisibleToTeam(
                                                                ReferenceUnit,
                                                                Unit,
                                                                false) &&
                                                          DivideFloat(
                                                                out Strength,
                                                                Strength,
                                                                2)
                                                    )
                                                    ||
                                   DebugAction("MaskFailure")
                                              )
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
        EnemyStrength = _EnemyStrength;
        FriendlyStrength = _FriendlyStrength;
        return result;
    }
}

