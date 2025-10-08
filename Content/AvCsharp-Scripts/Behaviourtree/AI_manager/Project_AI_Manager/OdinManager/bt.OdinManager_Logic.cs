using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using System.Numerics;

namespace BehaviourTrees;


class OdinManager_LogicClass : bt_OdinManager
{

    public bool OdinManager_Logic() { 
      
        return 
            // Sequence name :AIManager_Logic
            (
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :RunFirstTimeOnly
                        (
                              TestAIFirstTime(
                                    true) &&
                              CreateAISquad(
                                    out Squad_PushBot,
                                     "",
                                    5) &&
                              CreateAIMission(
                                    out Mission_PushBot,
                                    AIMissionTopicType.PUSH, 
                                    null, 
                                    (Vector3)default, 
                                    0) &&
                              AssignAIMission(
                                    Squad_PushBot, 
                                    Mission_PushBot)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :HasEntities
                  (
                        GetAIManagerEntities(
                              out AllEntities) &&
                        ForEach(AllEntities ,Entity => (
                              SetVarAttackableUnit(
                                    out ReferenceUnit, 
                                    Entity)
                        ))
                  ) &&
                  ForEach(AllEntities,Entity => (
                        // Sequence name :AssignToPushSquad
                        (
                              AddAIEntityToSquad(
                                    Entity, 
                                    Squad_PushBot)

                        ))
                  )
            );
      }
}

