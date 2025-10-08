using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class CenterRelicHelperClass : OdinLayout
{


    public bool CenterRelicHelper(
               out float __CenterRelicSpawnTime,
     out int __CenterRelicSquadID,
     out bool __CenterRelicParticleAttached,
   float CenterRelicSpawnTime,
   int CenterRelicSquadID,
   int RelicEncounterCenter,
   Vector3 CenterRelicPosition,
     bool CenterRelicParticleAttached,
   string CenterRelicSquadName,
   TeamId CenterRelicTeam
         )
    {

        float _CenterRelicSpawnTime = CenterRelicSpawnTime;
        int _CenterRelicSquadID = CenterRelicSquadID;
        bool _CenterRelicParticleAttached = CenterRelicParticleAttached;
        bool result =
                          // Sequence name :MaskFailure

                          // Sequence name :Sequence
                          (
                                GetGameTime(
                                      out CurrentTime) &&
                                // Sequence name :Spawn_Or_CheckSpawn
                                (
                                      // Sequence name :Spawn
                                      (
                                            LessEqualFloat(
                                                  _CenterRelicSpawnTime,
                                                  CurrentTime) &&
                                            AddFloat(
                                                  out _CenterRelicSpawnTime,
                                                  CurrentTime,
                                                  50000) &&
                                            SpawnSquadFromEncounter(
                                                  out _CenterRelicSquadID,
                                                  RelicEncounterCenter,
                                                  CenterRelicPosition,
                                                  CenterRelicTeam,
                                                  "CenterRelicSquadName"
                                                  ) &&
                                            SetVarBool(
                                                  out _CenterRelicParticleAttached,
                                                  false)
                                      ) ||
                                      // Sequence name :CheckSpawn
                                      (
                                            AddFloat(
                                                  out UpperBound,
                                                  CurrentTime,
                                                  25000) &&
                                            GreaterFloat(
                                                  _CenterRelicSpawnTime,
                                                  UpperBound) &&
                                            GreaterEqualInt(
                                                  _CenterRelicSquadID,
                                                  0) &&
                                            TestSquadIsAlive(
                                                  _CenterRelicSquadID,
                                                  false) &&
                                            SetVarInt(
                                                  out _CenterRelicSquadID,
                                                  -1) &&
                                            GenerateRandomFloat(
                                                  out NewCenterRelicSpawnTime,
                                                  180,
                                                  180) &&
                                            AddFloat(
                                                  out _CenterRelicSpawnTime,
                                                  NewCenterRelicSpawnTime,
                                                  CurrentTime)
                                      ) ||
                                      // Sequence name :AttachParticles
                                      (
                                            GreaterEqualInt(
                                                  _CenterRelicSquadID,
                                                  0) &&
                                            _CenterRelicParticleAttached == false &&
                                            TestSquadIsAlive(
                                                  _CenterRelicSquadID,
                                                  true) &&
                                            GetTurret(
                                                  out OrderShrineTurret,
                                                  TeamId.TEAM_ORDER,
                                                  0,
                                                  1) &&
                                            GetUnitsInTargetArea(
                                                  out UnitsToSearch,
                                                  OrderShrineTurret,
                                                  CenterRelicPosition,
                                                  300,
                                                  AffectEnemies | AffectFriends | AffectMinions | AffectUseable) &&
                                            ForEach(UnitsToSearch, Unit =>
                                                        // Sequence name :Sequence

                                                        GetSquadNameOfUnit(
                                                              out UnitSquadName,
                                                              Unit) &&
                                                        UnitSquadName == CenterRelicSquadName &&
                                                        AttachPARToIdleParticles(
                                                              Unit
                                                              ) &&
                                                        SetVarBool(
                                                              out _CenterRelicParticleAttached,
                                                              true)


                                            )
                                      )
                                )
                          )
                          ||
                                       DebugAction("MaskFailure")
                    ;
        __CenterRelicSpawnTime = _CenterRelicSpawnTime;
        __CenterRelicSquadID = _CenterRelicSquadID;
        __CenterRelicParticleAttached = _CenterRelicParticleAttached;
        return result;
    }
}

