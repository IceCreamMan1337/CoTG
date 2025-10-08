/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class LanePresence : BehaviourTree 
{
      out float StrengthIndicator;
      float EnemyStrength;
      float FriendlyStrength;

      bool LanePresence()
      {
      return
            // Sequence name :Calculates_Lane_Presence
            (
                  SubtractFloat(
                        out Presence, 
                        FriendlyStrength, 
                        EnemyStrength) &&
                  DivideFloat(
                        out Presence, 
                        Presence, 
                        4000) &&
                  MaxFloat(
                        out Presence, 
                        Presence, 
                        -1) &&
                  MinFloat(
                        out Presence, 
                        Presence, 
                        1) &&
                  SetVarFloat(
                        out StrengthIndicator, 
                        Presence)

            );
      }
}

*/