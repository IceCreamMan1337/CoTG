using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;


class StrengthEvaluatorClass : AI_Characters
{

    private StrengthEvaluator_ChampionClass strengthEvaluator_Champion = new();
    public bool StrengthEvaluator(
        out float _StrengthValue,
     IEnumerable<AttackableUnit> UnitsToEvaluate,
     AttackableUnit ReferenceUnit
        )
    {

        float StrengthValue = default;
        bool result =
                    // Sequence name :EvaluateStrengthOfAllUnits

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
                    ForEach(UnitsToEvaluate, Unit =>
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

                                            AddInt(
                                                  out EnemyChamps,
                                                  EnemyChamps,
                                                  1)

                                )

                          ) || MaskFailure()

                    //hack??? 





                    ) &&
                    ForEach(UnitsToEvaluate, Unit =>
                                // Sequence name :Sequence

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
                                GetUnitType(
                                      out UnitType,
                                      Unit) &&
                                GetUnitHealthRatio(
                                      out HealthRatio,
                                      Unit) &&

                                // Sequence name :MinionHeroOrTurret
                                (
                                      // Sequence name :Minion
                                      (
                                          DebugAction("UnitType" + UnitType) &&
                                            UnitType == MINION_UNIT &&
                                                DebugAction("PASSED") &&
                                            SetVarFloat(
                                                  out StrengthToAdd,
                                                  14) &&
                                            DivideFloat(
                                                  out ScaleFactor,
                                                  ReferenceUnitLevel,
                                                  2) &&
                                            SubtractFloat(
                                                  out StrengthToAdd,
                                                  StrengthToAdd,
                                                  ScaleFactor) &&
                                            // Sequence name :MaskFailure
                                            (
                                                  // Sequence name :Sequence
                                                  (
                                                        LessInt(
                                                              ReferenceUnitLevel,
                                                              9) &&
                                                        NotEqualUnitTeam(
                                                              UnitTeam,
                                                              MyTeam) &&
                                                        AddFloat(
                                                              out StrengthToAdd,
                                                              StrengthToAdd,
                                                              3)
                                                  ) || MaskFailure()
                                            )
                                      )
                                      ||
                                      // Sequence name :Hero
                                      (

                                            UnitType == HERO_UNIT &&

                                           strengthEvaluator_Champion.StrengthEvaluator_Champion(
                                                  out StrengthToAdd,
                                                  Unit,
                                                  ReferenceUnit,
                                                  25,
                                                  FriendlyChamps,
                                                  EnemyChamps)
                                      ) ||
                                      // Sequence name :Turret
                                      (

                                            UnitType == TURRET_UNIT &&

                                            // Sequence name :Selector
                                            (
                                                  // Sequence name :SameTeam
                                                  (
                                                        MyTeam == UnitTeam &&
                                                        SetVarFloat(
                                                              out StrengthToAdd,
                                                              110) &&
                                                        MultiplyFloat(
                                                              out StrengthToAdd,
                                                              StrengthToAdd,
                                                              HealthRatio)
                                                  ) ||
                                                  // Sequence name :DifferentTeam
                                                  (
                                                        SetVarFloat(
                                                              out StrengthToAdd,
                                                              75) &&
                                                        // Sequence name :MaskFailure
                                                        (
                                                              // Sequence name :IfLevel&gt,11
                                                              (
                                                                    GreaterInt(
                                                                          ReferenceUnitLevel,
                                                                          11) &&
                                                                    SubtractFloat(
                                                                          out StrengthToAdd,
                                                                          StrengthToAdd,
                                                                          ReferenceUnitLevel)
                                                              ) || MaskFailure()
                                                        ) &&
                                                        // Sequence name :MaskFailure
                                                        (
                                                              // Sequence name :IfLevel&lt,6
                                                              (
                                                                    GetUnitLevel(
                                                                          out UnitLevel,
                                                                          ReferenceUnit) &&
                                                                    LessInt(
                                                                          UnitLevel,
                                                                          6) &&
                                                                    SetVarFloat(
                                                                          out StrengthToAdd,
                                                                          200)
                                                              ) || MaskFailure()
                                                        )
                                                  )
                                            )
                                          && DebugAction("StrengthToAdd" + StrengthToAdd)
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
                                      ) || MaskFailure()
                                ) &&
                                AddFloat(
                                      out StrengthValue,
                                      StrengthValue,
                                      StrengthToAdd)


                    )
              ;
        _StrengthValue = StrengthValue;
        return result;
    }
}

