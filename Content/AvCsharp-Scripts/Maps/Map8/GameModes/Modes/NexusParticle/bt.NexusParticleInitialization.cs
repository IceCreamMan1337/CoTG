using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class NexusParticleInitializationClass : OdinLayout 
{

     public bool NexusParticleInitialization(
                out bool NexusParticleEnabledOrder,
      out bool NexusParticleEnabledChaos,
      out uint NexusParticleOrder_1,
      out uint NexusParticleOrder_2,
      out uint NexusParticleChaos_1,
      out uint NexusParticleChaos_2,
      out GameObject SoGLevelProp_Order,
      out GameObject SoGLevelProp_Chaos

          )
      {
        bool _NexusParticleEnabledOrder = default;
        bool _NexusParticleEnabledChaos = default;
        uint _NexusParticleOrder_1 = default;
        uint _NexusParticleOrder_2 = default;
        uint _NexusParticleChaos_1 = default;
        uint _NexusParticleChaos_2 = default;
        GameObject _SoGLevelProp_Order = default;
        GameObject _SoGLevelProp_Chaos = default;


bool result = 
              // Sequence name :Sequence
              (
                  SetVarBool(
                        out _NexusParticleEnabledOrder, 
                        false) &&
                  SetVarBool(
                        out _NexusParticleEnabledChaos, 
                        false) &&
                  GetPropByName(
                        out _SoGLevelProp_Order,
                        "LevelProp_Odin_SoG_Order") &&
                  GetPropByName(
                        out _SoGLevelProp_Chaos,
                        "LevelProp_Odin_SoG_Chaos")

            );

        NexusParticleEnabledOrder = _NexusParticleEnabledOrder;
        NexusParticleEnabledChaos = _NexusParticleEnabledChaos;
         NexusParticleOrder_1 = _NexusParticleOrder_1;
         NexusParticleOrder_2 = _NexusParticleOrder_2;
         NexusParticleChaos_1 = _NexusParticleChaos_1;
         NexusParticleChaos_2 = _NexusParticleChaos_2;
         SoGLevelProp_Order = _SoGLevelProp_Order;
         SoGLevelProp_Chaos = _SoGLevelProp_Chaos;



        return result;
      }
}

