using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using System.Numerics;

namespace BehaviourTrees;


class NexusVerticalBeamManagerClass : OdinLayout
{


    public bool NexusVerticalBeamManager(
               out bool __NexusVerticalBeamOrder,
     out bool __NexusVerticalBeamChaos,
     out uint __NexusVerticalBeamOrder_1,
     out uint __NexusVerticalBeamOrder_2,
     out uint __NexusVerticalBeamChaos_1,
     out uint __NexusVerticalBeamChaos_2,
   bool NexusVerticalBeamOrder,
   bool NexusVerticalBeamChaos,
   uint NexusVerticalBeamOrder_1,
   uint NexusVerticalBeamOrder_2,
   uint NexusVerticalBeamChaos_1,
   uint NexusVerticalBeamChaos_2,
   int NumCP_Order,
   int NumCP_Chaos,
   GameObject OrderLevelProp,
   GameObject ChaosLevelProp,
   bool GameOver
         )
    {

        bool _NexusVerticalBeamOrder = NexusVerticalBeamOrder;
        bool _NexusVerticalBeamChaos = NexusVerticalBeamChaos;
        uint _NexusVerticalBeamOrder_1 = NexusVerticalBeamOrder_1;
        uint _NexusVerticalBeamOrder_2 = NexusVerticalBeamOrder_2;
        uint _NexusVerticalBeamChaos_1 = NexusVerticalBeamChaos_1;
        uint _NexusVerticalBeamChaos_2 = NexusVerticalBeamChaos_2;
        bool result =
                    // Sequence name :Sequence
                    (
                          // Sequence name :WhichSideIsLosing?
                          (
                                // Sequence name :GameOver_RemoveAllParticles
                                (
                                      GameOver == true &&
                                      SetVarBool(
                                            out OrderLosing,
                                            false) &&
                                      SetVarBool(
                                            out ChaosLosing,
                                            false)
                                ) ||
                                // Sequence name :OrderLosing
                                (
                                      LessInt(
                                            NumCP_Order,
                                            NumCP_Chaos) &&
                                      SetVarBool(
                                            out OrderLosing,
                                            true) &&
                                      SetVarBool(
                                            out ChaosLosing,
                                            false)
                                ) ||
                                // Sequence name :ChaosLosing
                                (
                                      LessInt(
                                            NumCP_Chaos,
                                            NumCP_Order) &&
                                      SetVarBool(
                                            out OrderLosing,
                                            false) &&
                                      SetVarBool(
                                            out ChaosLosing,
                                            true)
                                ) ||
                                // Sequence name :Draw
                                (
                                      SetVarBool(
                                            out OrderLosing,
                                            false) &&
                                      SetVarBool(
                                            out ChaosLosing,
                                            false)
                                )
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      NexusVerticalBeamOrder == true &&
                                      OrderLosing == false &&
                                      RemoveParticle(
                                            NexusVerticalBeamOrder_1) &&
                                      RemoveParticle(
                                            NexusVerticalBeamOrder_2)
                                )
                                ||
                               DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Sequence
                                (
                                      NexusVerticalBeamChaos == true &&
                                      ChaosLosing == false &&
                                      RemoveParticle(
                                            NexusVerticalBeamChaos_1) &&
                                      RemoveParticle(
                                            NexusVerticalBeamChaos_2)
                                )
                                ||
                               DebugAction("MaskFailure")
                          ) &&
                          // Sequence name :MaskFailure
                          (
                                // Sequence name :Selector
                                (//todo : i have fix rapidly but i think it's false 
                                 // Sequence name :OrderLosing
                                      (
                                            OrderLosing == true &&
                                            NexusVerticalBeamOrder == false &&
                                            CreateUnitParticle(
                                                  out _NexusVerticalBeamOrder_1,

                                                  OrderLevelProp as AttackableUnit,
                                                  "center_crystal",
                                                  "odin_hurtbeam_green.troy",
                                                  null
                                                  ,
                                                  "",
                                                  (Vector3)default,
                                                  (Vector3)default,
                                                  null,
                                                  TeamId.TEAM_ORDER,
                                                  0,
                                                  TeamId.TEAM_ORDER,
                                                  false) &&
                                               CreateUnitParticle(out _NexusVerticalBeamOrder_2,
                                               OrderLevelProp as AttackableUnit,
                                                  "center_crystal",

                                                  "odin_hurtbeam_red.troy",
                                                  null
                                                  ,
                                                  "",
                                                  (Vector3)default,
                                                  (Vector3)default,
                                                  null,
                                                  TeamId.TEAM_ORDER,
                                                  0,
                                                  TeamId.TEAM_ORDER,
                                                  false)
                                      ) ||



                                      // Sequence name :ChaosLosing
                                      (
                                            ChaosLosing == true &&
                                            NexusVerticalBeamChaos == false &&
                                            CreateUnitParticle(
                                                  out _NexusVerticalBeamChaos_1,
                                                  ChaosLevelProp as AttackableUnit,
                                                  "center_crystal",
                                                  "odin_hurtbeam_green.troy",
                                                  null,
                                                  "",
                                                 (Vector3)default,
                                                  (Vector3)default,
                                                  null,
                                                  TeamId.TEAM_CHAOS,
                                                  0,
                                                  TeamId.TEAM_CHAOS,
                                                  false) &&
                                                  CreateUnitParticle(
                                                  out _NexusVerticalBeamChaos_2,
                                                  ChaosLevelProp as AttackableUnit,
                                                   "center_crystal",
                                                  "odin_hurtbeam_red.troy",
                                                  null,
                                                  "",
                                                 (Vector3)default,
                                                  (Vector3)default,
                                                  null,
                                                  TeamId.TEAM_CHAOS,
                                                  0,
                                                  TeamId.TEAM_CHAOS,
                                                  false)
                                      )
                                )
                                ||
                               DebugAction("MaskFailure")
                          ) &&
                          SetVarBool(
                                out _NexusVerticalBeamChaos,
                                ChaosLosing) &&
                          SetVarBool(
                                out _NexusVerticalBeamOrder,
                                OrderLosing)

                    );

         __NexusVerticalBeamOrder = _NexusVerticalBeamOrder;
         __NexusVerticalBeamChaos = _NexusVerticalBeamChaos;
         __NexusVerticalBeamOrder_1 = _NexusVerticalBeamOrder_1;
         __NexusVerticalBeamOrder_2 = _NexusVerticalBeamOrder_2;
         __NexusVerticalBeamChaos_1 = _NexusVerticalBeamChaos_1;
         __NexusVerticalBeamChaos_2 = _NexusVerticalBeamChaos_2;
        return result;
    }

}

