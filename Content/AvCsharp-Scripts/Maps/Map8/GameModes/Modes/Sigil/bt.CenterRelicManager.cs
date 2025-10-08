using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class CenterRelicManagerClass : OdinLayout 
{


     public bool CenterRelicManager(
                out float __CenterRelicSpawnTime_A,
      out float __CenterRelicSpawnTime_B,
      out int __CenterRelicSquadID_A,
      out int __CenterRelicSquadID_B,
      out bool __CenterRelicParticleAttached1,
      out bool __CenterRelicParticleAttached2,
    float CenterRelicSpawnTime_A,
    float CenterRelicSpawnTime_B,
    int CenterRelicSquadID_A,
    int CenterRelicSquadID_B,
    int RelicEncounterCenter,
    int RelicEncounterCenter2,
    Vector3 CenterRelicPositionA,
      Vector3 CenterRelicPositionB,
      bool CenterRelicParticleAttached1,
    bool CenterRelicParticleAttached2
          )
      {

        float _CenterRelicSpawnTime_A = CenterRelicSpawnTime_A;
        float _CenterRelicSpawnTime_B = CenterRelicSpawnTime_B;
        int _CenterRelicSquadID_A = CenterRelicSquadID_A;
        int _CenterRelicSquadID_B = CenterRelicSquadID_B;
        bool _CenterRelicParticleAttached1 = CenterRelicParticleAttached1;
        bool _CenterRelicParticleAttached2 = CenterRelicParticleAttached2;

        var centerRelicHelper = new CenterRelicHelperClass();

bool result = 
              // Sequence name :MaskFailure
              (
                  // Sequence name :Sequence
                  (
                        centerRelicHelper.CenterRelicHelper(
                              out _CenterRelicSpawnTime_A, 
                              out _CenterRelicSquadID_A, 
                              out _CenterRelicParticleAttached1, 
                              CenterRelicSpawnTime_A, 
                              CenterRelicSquadID_A, 
                              RelicEncounterCenter, 
                              CenterRelicPositionA, 
                              CenterRelicParticleAttached1,
                              "CenterRelic1", 
                              TeamId.TEAM_ORDER) &&
                         centerRelicHelper.CenterRelicHelper(
                              out _CenterRelicSpawnTime_B, 
                              out _CenterRelicSquadID_B, 
                              out _CenterRelicParticleAttached2, 
                              CenterRelicSpawnTime_B, 
                              CenterRelicSquadID_B, 
                              RelicEncounterCenter2, 
                              CenterRelicPositionB, 
                              CenterRelicParticleAttached2,
                              "CenterRelic2", 
                              TeamId.TEAM_CHAOS)

                  )
                  ||
                               DebugAction("MaskFailure")
            );
         __CenterRelicSpawnTime_A = _CenterRelicSpawnTime_A;
         __CenterRelicSpawnTime_B = _CenterRelicSpawnTime_B;
         __CenterRelicSquadID_A = _CenterRelicSquadID_A;
         __CenterRelicSquadID_B = _CenterRelicSquadID_B;
         __CenterRelicParticleAttached1 = _CenterRelicParticleAttached1;
         __CenterRelicParticleAttached2 = _CenterRelicParticleAttached2;

        return result;
      }
}

