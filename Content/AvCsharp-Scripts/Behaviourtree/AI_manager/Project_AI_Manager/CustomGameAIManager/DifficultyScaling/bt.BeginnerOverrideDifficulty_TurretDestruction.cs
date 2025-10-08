/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class BeginnerOverrideDifficulty_TurretDestruction : BehaviourTree 
{
      out bool OverrideDifficulty;
      out bool IsDifficultySet;
      bool OverrideDifficulty;
      TeamEnum ReferenceTeam;
      float DynamicDistributionStartTime;
      bool IsDifficultySet;

      bool BeginnerOverrideDifficulty_TurretDestruction()
      {
      return
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        OverrideDifficulty == False &&
                        GetGameTime(
                              out CurrentGameTime, 
                              out CurrentGameTime) &&
                        LessFloat(
                              CurrentGameTime, 
                              DynamicDistributionStartTime) &&
                        GetTurretCollection(
                              out Turrets, 
                              out Turrets) &&
                        SetVarInt(
                              out TurretCount, 
                              0) &&
                        Turrets.ForEach( Turret => (                              // Sequence name :Sequence
                              (
                                    GetUnitTeam(
                                          out TurretTeam, 
                                          Turret) &&
                                    TurretTeam == ReferenceTeam &&
                                    AddInt(
                                          out TurretCount, 
                                          TurretCount, 
                                          1)
                              )
                        ) &&
                        LessInt(
                              TurretCount, 
                              12) &&
                        SetVarBool(
                              out OverrideDifficulty, 
                              true) &&
                        SetVarBool(
                              out IsDifficultySet, 
                              False)

                  )
            );
      }
}

*/