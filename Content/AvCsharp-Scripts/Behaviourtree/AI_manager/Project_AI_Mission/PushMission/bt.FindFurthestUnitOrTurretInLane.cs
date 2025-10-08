using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class FindFurthestUnitOrTurretInLaneClass : AImission_bt 
{
      

     public bool FindFurthestUnitOrTurretInLane(
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
            DebugAction("FindFurthestUnitOrTurretInLane: Début") &&
            // Sequence name :SearchForTheFurthestUnit
            (
                  SetVarFloat(
                        out _FurthestDistance, 
                        0) &&
                        DebugAction("FindFurthestUnitOrTurretInLane: _FurthestDistance initialisé à 0") &&
                           DebugAction("GetUnitAIBasePosition ") &&
                  GetUnitAIBasePosition(
                        out BasePosition, 
                        ReferenceUnit) &&
                        DebugAction($"FindFurthestUnitOrTurretInLane: BasePosition = {BasePosition}") &&
                          DebugAction("SearchForUnitsInTheSameLane " + EntitiesToSearch.Count()) &&
                 ForEach(EntitiesToSearch ,Entity => (      
                  // Sequence name :SearchForUnitsInTheSameLane
                        (
                              DebugAction($"FindFurthestUnitOrTurretInLane: Traitement de {Entity?.Name}") &&
                              // Sequence name :CheckAgainstReferenceUnit
                              (
                                    DebugAction($"FindFurthestUnitOrTurretInLane: CheckAgainstReferenceUnit = {CheckAgainstReferenceUnit}") &&
                                    CheckAgainstReferenceUnit == false   ||                    
                                    // Sequence name :CheckReferenceUnit
                                    (
                                          CheckAgainstReferenceUnit == true &&
                                          NotEqualUnit(
                                                ReferenceUnit, 
                                                Entity) &&
                                          DebugAction($"FindFurthestUnitOrTurretInLane: Entity {Entity?.Name} différente de ReferenceUnit")
                                    )
                              ) &&
                              DistanceBetweenObjectAndPoint(
                                    out EntityDistance, 
                                    Entity, 
                                    BasePosition) &&
                              DebugAction($"FindFurthestUnitOrTurretInLane: EntityDistance = {EntityDistance}") &&
                              GreaterFloat(
                                    EntityDistance,
                                    _FurthestDistance) &&
                              DebugAction($"FindFurthestUnitOrTurretInLane: EntityDistance > _FurthestDistance ({EntityDistance} > {_FurthestDistance})") &&
                              GetUnitAIClosestLaneID(
                                    out EntityLaneID, 
                                    Entity) &&
                              DebugAction($"FindFurthestUnitOrTurretInLane: EntityLaneID = {EntityLaneID}, LaneID recherché = {LaneID}") &&
                              LaneID == EntityLaneID &&
                              DebugAction($"FindFurthestUnitOrTurretInLane: LaneID match! Mise à jour _FurthestUnit et _FurthestDistance") &&
                              SetVarFloat(
                                    out _FurthestDistance, 
                                    EntityDistance) &&
                              SetVarAttackableUnit(
                                    out _FurthestUnit, 
                                    Entity) &&
                              DebugAction($"FindFurthestUnitOrTurretInLane: _FurthestUnit = {_FurthestUnit?.Name}, _FurthestDistance = {_FurthestDistance}")
                        )
                  ) ) &&
                  DebugAction($"FindFurthestUnitOrTurretInLane: Fin de la boucle, _FurthestDistance = {_FurthestDistance}") &&
                  DebugAction($"FindFurthestUnitOrTurretInLane: Vérification GreaterFloat({_FurthestDistance}, 0)") &&
                  GreaterFloat(
                        _FurthestDistance, 
                        0) &&
                  DebugAction($"FindFurthestUnitOrTurretInLane: _FurthestDistance > 0, SUCCESS")

            );

        FurthestUnit = _FurthestUnit;
        FurthestDistance = _FurthestDistance;
        DebugAction($"FindFurthestUnitOrTurretInLane: Retour - FurthestUnit = {FurthestUnit?.Name}, FurthestDistance = {FurthestDistance}, result = {result}");
        
        // TEMPORAIRE: Forcer le retour à true pour tester
        if (FurthestUnit != null && FurthestDistance > 0)
        {
            DebugAction($"FindFurthestUnitOrTurretInLane: FORCING RETURN TO TRUE (test temporaire)");
            return true;
        }
        
        return result;  
      }
}

