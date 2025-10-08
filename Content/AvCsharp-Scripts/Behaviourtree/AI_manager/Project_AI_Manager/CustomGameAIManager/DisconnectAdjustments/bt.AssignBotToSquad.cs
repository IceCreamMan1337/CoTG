/*using static GameServerCore.Enums.SpellDataFlags;
using static GameServerCore.Enums.SpellbookType;
using static GameServerCore.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class AssignBotToSquad : BehaviourTree 
{
      AttackableUnitCollection BotsCollection;
      int BotIndex;
      AISquad SquadToAssign;

      bool AssignBotToSquad()
      {
      return
            // Sequence name :Sequence
            (
                  SetVarInt(
                        out Count, 
                        -1) &&
                  BotsCollection.ForEach( Unit => (
                        // Sequence name :Sequence
                        (
                              AddInt(
                                    out Count, 
                                    Count, 
                                    1) &&
                              // ✅ SUPPRIMÉ : Count == BotIndex (limitait à 1 seul bot)
                              // ✅ MAINTENANT : Ajoute TOUS les bots au squad
                              AddAIEntityToSquad(
                                    Unit, 
                                    SquadToAssign)

                        )
                  )
            );
      }
}

*/