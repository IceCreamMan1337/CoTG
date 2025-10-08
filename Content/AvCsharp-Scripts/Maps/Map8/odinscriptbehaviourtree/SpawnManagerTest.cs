using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class SpawnManagerTestClass : OdinLayout 
{

      public bool SpawnManagerTest() {
        var spawnWave = new SpawnWaveClass();
        return
            // Sequence name :SpawnManagerTest
            (
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              __IsFirstRun == true &&
                              // Sequence name :InitVars
                              (
                                    MakeVector(
                                          out SpawnPointAlpha, 
                                          0, 
                                          0, 
                                          0) &&
                                    CreateEncounterFromDefinition(
                                          out waveEncounterId,
                                          "RegularWave"
                                          )
                              ) &&
                              // Sequence name :InitQuests
                              (
                                    GetTutorialPlayer(
                                          out Player) &&
                                    ActivateQuest(
                                          out Lane0Quest,
                                          "Spawn Lane 0", 
                                          Player, 
                                         QuestType.Primary, 
                                          false, 
                                          "") &&
                                    ActivateQuest(
                                          out Lane1Quest,
                                          "Spawn Lane 1", 
                                          Player,
                                           QuestType.Primary,
                                          false, 
                                          "") &&
                                    ActivateQuest(
                                          out Lane2Quest,
                                         " Spawn Lane 2", 
                                          Player,
                                           QuestType.Primary,
                                          false, 
                                          "") &&
                                    ActivateQuest(
                                          out Lane0QuestReverse,
                                          "Reverse Lane 0", 
                                          Player, 
                                         QuestType.Secondary, 
                                          false, 
                                          "") &&
                                    ActivateQuest(
                                          out Lane1QuestReverse,
                                          "Lane 1 Reverse", 
                                          Player,
                                         QuestType.Secondary,
                                          false, 
                                          "") &&
                                    ActivateQuest(
                                          out Lane2QuestReverse,
                                          "Lane 2 Reverse", 
                                          Player,
                                          QuestType.Secondary,
                                          false, 
                                          "") &&
                                    GetTurret(
                                          out OrderTurretE, 
                                          TeamId.TEAM_ORDER, 
                                          1, 
                                          6) &&
                                    GetTurret(
                                          out OrderTurretA, 
                                          TeamId.TEAM_ORDER, 
                                          1, 
                                          2) &&
                                    GetTurret(
                                          out OrderTurretB, 
                                          TeamId.TEAM_ORDER, 
                                          1, 
                                          3) &&
                                    GetTurret(
                                          out OrderTurretC, 
                                          TeamId.TEAM_ORDER, 
                                          1, 
                                          4) &&
                                    GetTurret(
                                          out OrderTurretD, 
                                          TeamId.TEAM_ORDER, 
                                          1, 
                                          5) &&
                                    GetUnitPosition(
                                          out CapturePointEPos, 
                                          OrderTurretE) &&
                                    GetUnitPosition(
                                          out CapturePointAPos, 
                                          OrderTurretA) &&
                                    GetUnitPosition(
                                          out CapturePointBPos, 
                                          OrderTurretB) &&
                                    GetUnitPosition(
                                          out CapturePointCPos, 
                                          OrderTurretC) &&
                                    GetUnitPosition(
                                          out CapturePointDPos, 
                                          OrderTurretD)
                              )
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :Game Loop
                  (
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave0
                              (
                                    TestQuestClicked(
                                          Lane0Quest, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointAPos, 
                                          0, 
                                          false, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave1
                              (
                                    TestQuestClicked(
                                          Lane1Quest, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointAPos, 
                                          1, 
                                          false, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave2
                              (
                                    TestQuestClicked(
                                          Lane2Quest, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointCPos, 
                                          2, 
                                          false, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave0-Reverse
                              (
                                    TestQuestClicked(
                                          Lane0QuestReverse, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointEPos, 
                                          0, 
                                          true, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave1-Reverse
                              (
                                    TestQuestClicked(
                                          Lane1QuestReverse, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointEPos, 
                                          1, 
                                          true, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)
                              )
                              ||
                               DebugAction("MaskFailure")
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :SpawnWave2-Reverse
                              (
                                    TestQuestClicked(
                                          Lane2QuestReverse, 
                                          true) &&
                                    spawnWave.SpawnWave(
                                          CapturePointDPos, 
                                          2, 
                                          true, 
                                          TeamId.TEAM_ORDER, 
                                          waveEncounterId)

                              )
                              ||
                               DebugAction("MaskFailure")
                        )
                  )
            );
      }
}

