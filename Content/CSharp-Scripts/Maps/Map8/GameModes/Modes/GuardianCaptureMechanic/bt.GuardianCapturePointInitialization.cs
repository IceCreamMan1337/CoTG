namespace BehaviourTrees;


class GuardianCapturePointInitializationClass : OdinLayout
{
    private modesGetCapturePointsClass getCapturePoints = new();
    public bool GuardianCapturePointInitialization()
    {



        return
                  // Sequence name :CreateInitialCapturePointGuardians

                  CreateEncounterFromDefinition(
                        out OdinCapturePointNeutralEncounter,
                        "OdinCapturePointNeutral",
                        1
                        ) &&
                  getCapturePoints.GetCapturePoints(
                        out CapturePointAPos,
                        out CapturePointBPos,
                        out CapturePointCPos,
                        out CapturePointDPos,
                        out CapturePointEPos) &&
                  SpawnSquadFromEncounter(
                        out CapturePointA_SquadID,
                        OdinCapturePointNeutralEncounter,
                        CapturePointAPos,
                        TeamId.TEAM_NEUTRAL,
                        "CaptureGuardian0"
                        ) &&
                  SpawnSquadFromEncounter(
                        out CapturePointB_SquadID,
                        OdinCapturePointNeutralEncounter,
                        CapturePointBPos,
                        TeamId.TEAM_NEUTRAL,
                        "CaptureGuardian1"
                        ) &&
                  SpawnSquadFromEncounter(
                        out CapturePointC_SquadID,
                        OdinCapturePointNeutralEncounter,
                        CapturePointCPos,
                        TeamId.TEAM_NEUTRAL,
                        "CaptureGuardian2"
                        ) &&
                  SpawnSquadFromEncounter(
                        out CapturePointD_SquadID,
                        OdinCapturePointNeutralEncounter,
                        CapturePointDPos,
                        TeamId.TEAM_NEUTRAL,
                        "CaptureGuardian3"
                        ) &&
                  SpawnSquadFromEncounter(
                        out CapturePointE_SquadID,
                        OdinCapturePointNeutralEncounter,
                        CapturePointEPos,
                        TeamId.TEAM_NEUTRAL,
                        "CaptureGuardian4"
                        )

            ;
    }
}

