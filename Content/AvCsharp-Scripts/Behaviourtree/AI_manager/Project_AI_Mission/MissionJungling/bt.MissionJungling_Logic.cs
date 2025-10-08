using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using System.Numerics;

namespace BehaviourTrees;


class MissionJungling_LogicClass : AImission_bt
{

      public bool MissionJungling_Logic() {


        var missionJungling_Chaos_PositionDefinition = new MissionJungling_Chaos_PositionDefinitionClass();
        return
            // Sequence name :TaskAssignment
            (
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Initialize
                        (
                              TestAIFirstTime(
                                    true) &&
                              GetGameTime(
                                    out LastUpdateTime) &&
                              GetAIMissionSelf(
                                    out ThisMission)
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  GetAIMissionSquadMembers(
                        out SquadMembers,
                        ThisMission) &&
                  ForEach(SquadMembers, Entity => (
                        // Sequence name :Find_Reference_Unit
                        (
                              SetVarAttackableUnit(
                                    out ReferenceUnit,
                                    Entity)
                        )
                  )) &&
                  missionJungling_Chaos_PositionDefinition.MissionJungling_Chaos_PositionDefinition(
                        out PositionWolf,
                        out PositionGolem,
                        out PositionWraith,
                        out PositionAncientGolem,
                        out PositionLizardElder) &&
                  // Sequence name :KillMissions
                  (
                        // Sequence name :Wolves
                        (
                              GetUnitsInTargetArea(
                                    out NeutralWolves,
                                    ReferenceUnit,
                                    PositionWolf,
                                    500,
                                    AffectMinions | AffectNeutral) &&
                              ForEach(NeutralWolves, Entity => (

                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    )
                              )) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                               AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default,
                                                0,
                                                true) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.KILL,
                                                      ReferenceNeutral,
                                                      (Vector3)default
                                                ) &&
                                                AssignAITask(
                                                      Entity,
                                                      KillTask)
                                          )
                                    )
                              ))
                        ) ||
                        // Sequence name :Wraith
                        (
                              GetUnitsInTargetArea(
                                    out Neutrals,
                                    ReferenceUnit,
                                    PositionWraith,
                                    500,
                                    AffectMinions | AffectNeutral) &&
                              ForEach(Neutrals, Entity => (
                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    )
                              )) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                                AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default,
                                                0,
                                                true) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.KILL,
                                                      ReferenceNeutral,
                                                      (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity,
                                                      KillTask)
                                          )
                                    )
                              ))
                        ) ||
                        // Sequence name :Wraith
                        (
                              GetUnitsInTargetArea(
                                    out Neutrals,
                                    ReferenceUnit,
                                    PositionWraith,
                                    500,
                                    AffectMinions | AffectNeutral) &&
                              ForEach(Neutrals, Entity => (
                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    ))
                              ) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                                AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default,
                                                0,
                                                true) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.KILL,
                                                      ReferenceNeutral,
                                                      (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity,
                                                      KillTask)
                                          )
                                    )
                              ))
                        ) ||
                        // Sequence name :Wraith
                        (
                              GetUnitsInTargetArea(
                                    out Neutrals,
                                    ReferenceUnit,
                                    PositionGolem,
                                    500,
                                    AffectMinions | AffectNeutral) &&
                              ForEach(Neutrals, Entity => (
                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    ))
                              ) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                                AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default,
                                                0,
                                                true) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.KILL,
                                                      ReferenceNeutral,
                                                      (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity,
                                                      KillTask)
                                          )
                                    )
                              ))
                        ) ||
                        // Sequence name :Wraith
                        (
                              GetUnitsInTargetArea(
                                    out Neutrals,
                                    ReferenceUnit,
                                    PositionLizardElder,
                                    500,
                                    AffectMinions | AffectNeutral) &&
                              ForEach(Neutrals, Entity => (
                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    ))
                              ) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                                AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default,
                                                0,
                                                true) ||
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                      AITaskTopicType.KILL,
                                                      ReferenceNeutral,
                                                      (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity,
                                                      KillTask)
                                          )
                                    )
                              ))
                        ) ||
                        // Sequence name :Wraith
                        (
                              GetUnitsInTargetArea(
                                    out Neutrals,
                                    ReferenceUnit,
                                    PositionAncientGolem,
                                    500,
                                    AffectMinions | AffectNeutral
                                    ) &&
                              ForEach(Neutrals, Entity => (
                                    // Sequence name :Find_Reference_Neutral
                                    (
                                          SetVarAttackableUnit(
                                                out ReferenceNeutral,
                                                Entity)
                                    )
                              )) &&
                              ForEach(SquadMembers, Entity => (
                                    // Sequence name :IssueKillNeutrals
                                    (
                                          TestAIEntityHasTask(
                                                Entity,
                                                AITaskTopicType.KILL,
                                                ReferenceNeutral,
                                                (Vector3)default, 
                                                0, 
                                                true) ||                                    
                                          // Sequence name :AssignTask
                                          (
                                                CreateAITask(
                                                      out KillTask,
                                                     AITaskTopicType.KILL, 
                                                      ReferenceNeutral,
                                                       (Vector3)default
                                                      ) &&
                                                AssignAITask(
                                                      Entity, 
                                                      KillTask)

                                          )
                                    )
                              ))
                        )
                  )
            );



      }
}

