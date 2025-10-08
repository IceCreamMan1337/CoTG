namespace BehaviourTrees;


class AssignBotToSquadClass : AI_DifficultyScaling
{


    public bool AssignBotToSquad(
      IEnumerable<AttackableUnit> BotsCollection,
      int BotIndex,
      AISquadClass SquadToAssign)
    {
        return
                   // Sequence name :Sequence

                   DebugAction("AddAIEntityToSquad") &&
                   DebugAction(BotsCollection.Count() + "total of team ") &&
                    SetVarInt(
                          out Count,
                          -1) &&
                    ForEach(BotsCollection, Unit =>
                                // Sequence name :Sequence

                                AddInt(
                                      out Count,
                                      Count,
                                      1) &&
                                      DebugAction(Unit.Model + "will be added") &&
                                //  Count == BotIndex &&  todo get correct bot  index 
                                AddAIEntityToSquad(
                                      Unit,
                                      SquadToAssign)


                    )
              ;
    }
}

