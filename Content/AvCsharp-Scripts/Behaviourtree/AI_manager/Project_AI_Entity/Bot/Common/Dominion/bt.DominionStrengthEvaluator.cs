using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionStrengthEvaluatorClass : AI_Characters 
{
    private StrengthEvaluator_ChampionClass strengthEvaluator_Champion = new StrengthEvaluator_ChampionClass();

     public bool DominionStrengthEvaluator(
          out float _StrengthValue,
      IEnumerable<AttackableUnit> UnitsToEvaluate,
      AttackableUnit ReferenceUnit
         )
      {

        float StrengthValue = default;


        bool result =
              // Sequence name :EvaluateStrengthOfAllUnits
              (
                    GetUnitAIBasePosition(
                          out ReferencePoint,
                          ReferenceUnit) &&
                    DistanceBetweenObjectAndPoint(
                          out DistanceToReferencePoint,
                          ReferenceUnit,
                          ReferencePoint) &&
                    SetVarFloat(
                          out StrengthValue,
                          1) &&
                    GetUnitTeam(
                          out MyTeam,
                          ReferenceUnit) &&
                    GetUnitLevel(
                          out ReferenceUnitLevel,
                          ReferenceUnit) &&
                    SetVarInt(
                          out FriendlyChamps,
                          0) &&
                    SetVarInt(
                          out EnemyChamps,
                          0) &&
                    ForEach(UnitsToEvaluate, Unit => (                     
                    // Sequence name :CountChamps
                          (
                                GetUnitType(
                                      out UnitType,
                                      Unit) &&
                                UnitType == HERO_UNIT &&
                                GetUnitTeam(
                                      out UnitTeam,
                                      Unit) &&
                                // Sequence name :WhichTeam?
                                (
                                      // Sequence name :MyTeam
                                      (
                                            UnitTeam == MyTeam &&
                                            AddInt(
                                                  out FriendlyChamps,
                                                  FriendlyChamps,
                                                  1)
                                      ) ||
                                      // Sequence name :NotMyTeam
                                      (
                                            AddInt(
                                                  out EnemyChamps,
                                                  EnemyChamps,
                                                  1)
                                      )
                                )
                          ))
                    ) &&
                    ForEach(UnitsToEvaluate, Unit => (
                          // Sequence name :Sequence
                          (
                                TestUnitIsVisibleToTeam(
                                      ReferenceUnit,
                                      Unit,
                                      true) &&
                                SetVarFloat(
                                      out StrengthToAdd,
                                      0) &&
                                GetUnitTeam(
                                      out UnitTeam,
                                      Unit) &&
                                // Sequence name :MinionHeroOrTurret
                                (
                                      // Sequence name :Turret
                                      (
                                            GetUnitBuffCount(
                                                  out Count,
                                                  Unit,
                                                  "OdinGuardianStatsByLevel") &&
                                            GreaterInt(
                                                  Count,
                                                  0) &&
                                            // Sequence name :Selector
                                            (
                                                  // Sequence name :DifferentTeamButSuppressed
                                                  (
                                                        NotEqualUnitTeam(
                                                              MyTeam,
                                                              UnitTeam) &&
                                                        GetUnitBuffCount(
                                                              out Count,
                                                              Unit,
                                                              "OdinGuardianSuppression") &&
                                                        GreaterInt(
                                                              Count,
                                                              0) &&
                                                        SetVarFloat(
                                                              out StrengthToAdd,
                                                              10)
                                                  ) ||
                                                  // Sequence name :SameTeam
                                                  (
                                                        MyTeam == UnitTeam &&
                                                        SetVarFloat(
                                                              out StrengthToAdd,
                                                              110)
                                                  ) ||
                                                  // Sequence name :DifferentTEam
                                                  (
                                                        SetVarFloat(
                                                              out StrengthToAdd,
                                                              60)
                                                  )
                                            )
                                      ) ||
                                      // Sequence name :Minion
                                      (
                                            GetUnitType(
                                                  out UnitType,
                                                  Unit) &&
                                            UnitType == MINION_UNIT &&
                                            GetUnitBuffCount(
                                                  out Count,
                                                  Unit,
                                                  "OdinMinionTaunt") &&
                                            LessEqualInt(
                                                  Count,
                                                  0) &&
                                            SetVarFloat(
                                                  out StrengthToAdd,
                                                  12) &&
                                            DivideFloat(
                                                  out StrengthModifier,
                                                  ReferenceUnitLevel,
                                                  2) &&
                                            SubtractFloat(
                                                  out StrengthToAdd,
                                                  StrengthToAdd,
                                                  StrengthModifier)
                                      ) ||
                                      // Sequence name :Hero
                                      (
                                            GetUnitType(
                                                  out UnitType,
                                                  Unit) &&
                                            UnitType == HERO_UNIT &&
                                            strengthEvaluator_Champion.StrengthEvaluator_Champion(
                                                  out StrengthToAdd,
                                                  Unit,
                                                  ReferenceUnit,
                                                  25,
                                                  FriendlyChamps,
                                                  EnemyChamps)
                                      )
                                ) &&
                                DistanceBetweenObjectAndPoint(
                                      out UnitDistanceToRefPoint,
                                      Unit,
                                      ReferencePoint) &&
                                // Sequence name :MaskFailure
                                (
                                      // Sequence name :IsUnitBehindMe?
                                      (
                                            MyTeam == UnitTeam &&
                                            LessFloat(
                                                  UnitDistanceToRefPoint,
                                                  DistanceToReferencePoint) &&
                                            MultiplyFloat(
                                                  out StrengthToAdd,
                                                  StrengthToAdd,
                                                  0.65f)
                                      )
                                ) &&
                                AddFloat(
                                      out StrengthValue,
                                      StrengthValue,
                                      StrengthToAdd)

                          ))
                    )
              );
        _StrengthValue = StrengthValue;
        return result;
      }
}

