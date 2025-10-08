using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;

/*
class FindFurthestUnitOrTurretInLaneClass : AImission_bt
{


      bool FindFurthestUnitOrTurretInLane(
                out AttackableUnit FurthestUnit,
      out float FurthestDistance,
    IEnumerable<AttackableUnit> EntitiesToSearch,
    int LaneID,
    AttackableUnit ReferenceUnit,
    bool CheckAgainstReferenceUnit
          
          )


      {

        AttackableUnit _FurthestUnit = default;
        float _FurthestDistance = default;


        bool result =
              // Sequence name :SearchForTheFurthestUnit
              (
                  SetVarFloat(
                        out _FurthestDistance, 
                        0) &&
                  GetUnitAIBasePosition(
                        out BasePosition, 
                        ReferenceUnit) &&
                  ForEach(EntitiesToSearch ,Entity => (                      
                  // Sequence name :SearchForUnitsInTheSameLane
                        (
                              // Sequence name :CheckAgainstReferenceUnit
                              (
                                    CheckAgainstReferenceUnit == false    ||                             
                                    // Sequence name :CheckReferenceUnit
                                    (
                                          CheckAgainstReferenceUnit == true &&
                                          NotEqualUnit(
                                                ReferenceUnit, 
                                                Entity)
                                    )
                              ) &&
                              DistanceBetweenObjectAndPoint(
                                    out EntityDistance, 
                                    Entity, 
                                    BasePosition) &&
                              GreaterFloat(
                                    EntityDistance,
                                    _FurthestDistance) &&
                              GetUnitAIClosestLaneID(
                                    out EntityLaneID, 
                                    Entity) &&
                              LaneID == EntityLaneID &&
                              SetVarFloat(
                                    out _FurthestDistance, 
                                    EntityDistance) &&
                              SetVarAttackableUnit(
                                    out _FurthestUnit, 
                                    Entity)
                        )
                  )) &&
                  GreaterFloat(
                        _FurthestDistance, 
                        0)

            );
        FurthestUnit = _FurthestUnit;
        FurthestDistance = _FurthestDistance;
        return result;
      }
}

*/