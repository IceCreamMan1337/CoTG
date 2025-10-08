using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class BeginnerOverrideDifficulty_TurretDestructionClass : AI_DifficultyScaling
{


     public bool BeginnerOverrideDifficulty_TurretDestruction(
      out bool OverrideDifficulty,
      out bool IsDifficultySet,
      bool __OverrideDifficulty,
      TeamId ReferenceTeam,
      float DynamicDistributionStartTime,
      bool __IsDifficultySet)
    {

        bool _OverrideDifficulty = __OverrideDifficulty;
        bool _IsDifficultySet = __IsDifficultySet;


        bool result =
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        __OverrideDifficulty == false &&
                        GetGameTime(
                              out CurrentGameTime) &&
                        LessFloat(
                              CurrentGameTime, 
                              DynamicDistributionStartTime) &&
                        GetTurretCollection(
                              out Turrets) &&
                        SetVarInt(
                              out TurretCount, 
                              0) &&
                        ForEach(Turrets ,Turret => (                              // Sequence name :Sequence
                              (
                                    GetUnitTeam(
                                          out TurretTeam, 
                                          Turret) &&
                                    TurretTeam == ReferenceTeam &&
                                    AddInt(
                                          out TurretCount, 
                                          TurretCount, 
                                          1)
                              ) ) 
                        ) &&
                        LessInt(
                              TurretCount, 
                              12) &&
                        SetVarBool(
                              out _OverrideDifficulty, 
                              true) &&
                        SetVarBool(
                              out _IsDifficultySet, 
                              false)

                  )
                  ||
                               DebugAction("MaskFailure")
            );
        OverrideDifficulty = _OverrideDifficulty;
        IsDifficultySet = _IsDifficultySet;
        return result;
      }
}

