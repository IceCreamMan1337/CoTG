using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinOpeningCeremonyClass_forscript : OdinLayout 
{

      public bool OdinOpeningCeremony_forscript(
          bool __IsFirstRun) { 
      return
            // Sequence name :Selector
            (
                  // Sequence name :Initialization
                  (
                        __IsFirstRun == true &&
                        SetVarFloat(
                              out SpawnBarrierRadius, 
                              1200) &&
                        GetWorldLocationByName(
                              out OrderStartPoint,
                              "OrderStartPoint") &&
                        GetWorldLocationByName(
                              out ChaosStartPoint,
                              "ChaosStartPoint") &&
                        MakeColor(
                              out RedColor, 
                              255, 
                              0, 
                              0, 
                              35) &&
                        AddDebugCircle(
                              out DebugCircleId, 
                              null, 
                              ChaosStartPoint, 
                              SpawnBarrierRadius, 
                              RedColor) &&
                        AddDebugCircle(
                              out OrderDebugCircleId, 
                              null, 
                              OrderStartPoint, 
                              SpawnBarrierRadius, 
                              RedColor) &&
                        GetChampionCollection(
                              out ChampionCollection) &&
                        ForEach(ChampionCollection,LocalHero => (
                              // Sequence name :Selector
                              (
                                    // Sequence name :TestForOrder
                                    (
                                          GetUnitTeam(
                                                out LocalHeroTeam, 
                                                LocalHero) &&
                                          LocalHeroTeam == TeamId.TEAM_ORDER &&
                                          SetUnitCircularMovementRestriction(
                                                LocalHero, 
                                                OrderStartPoint, 
                                                SpawnBarrierRadius, 
                                                false)
                                    ) ||
                                    SetUnitCircularMovementRestriction(
                                          LocalHero, 
                                          ChaosStartPoint, 
                                          SpawnBarrierRadius, 
                                          false)
                              ))
                        )
                  ) ||
                  // Sequence name :Action
                  (
                        GetGameTime(
                              out CurrentTime) &&
                              
                        GreaterFloat(
                              CurrentTime, 
                              45) &&
                        RemoveDebugCircle(
                              DebugCircleId) &&
                        RemoveDebugCircle(
                              OrderDebugCircleId) &&
                        Announcement_OnStartGame(
                              1,
                              false) &&
                        ForEach(ChampionCollection,LocalHero => (                       
                        SetUnitCircularMovementRestriction(
                                    LocalHero, 
                                    ChaosStartPoint, 
                                    0, 
                                    false) )
                        ) &&
                        // Sequence name :SpawnShrines
                        (
                              MakeVector(
                                    out Shrine1Position, 
                                    7378, 
                                    12, 
                                    9813) &&
                              MakeVector(
                                    out Shrine2Position, 
                                    7621, 
                                    13, 
                                    4675) &&
                              CreateEncounterFromDefinition(
                                    out ShrineEncounterID,
                                    "Shrine"
                                    ) &&
                              SpawnSquadFromEncounter(
                                    out Shrine1SquadID, 
                                    ShrineEncounterID, 
                                    Shrine1Position, 
                                    TeamId.TEAM_NEUTRAL, 
                                    "") &&
                              SpawnSquadFromEncounter(
                                    out Shrine2SquadID, 
                                    ShrineEncounterID, 
                                    Shrine2Position, 
                                    TeamId.TEAM_NEUTRAL, 
                                    "")
                        ) &&
                        SetBTInstanceStatus(
                              false,
                              "OdinOpeningCeremony_forscript")

                  )
            );
      }
}

