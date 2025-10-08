using ChildrenOfTheGraveEnumNetwork.Enums;
using System.Collections.Generic;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

public class Quest
{
    private static int nextQuestId = 0;

    private static Dictionary<int, Quest> questList = new();
    public int QuestID { get; set; }

    public string Objective { get; set; }

    public string Tooltip { get; set; }

    public string Reward { get; set; }

    public QuestType Type { get; set; }

    public QuestEvent Command { get; set; }

    public bool HandleRollovers { get; set; }

    public TeamId Team { get; set; }
    public Quest(string _objective, string _tooltip, string _reward, QuestType _Type, QuestEvent _command, TeamId _team)
    {
        var id = nextQuestId++;

        QuestID = id;
        Objective = _objective;
        Tooltip = _tooltip;
        Reward = _reward;
        Type = _Type;
        Command = _command;
        Team = _team;

        questList.Add(id, this);
        ActivateQuest(id);
    }
    public static Quest FindByID(int questId)
    {
        if (questList.ContainsKey(questId))
        {
            return questList[questId];
        }
        else
        {
            return null;
        }
    }

    public void ActivateQuest(int questId)
    {

        HandleQuestUpdateNotify(questList[questId], 0);

    }

    public static void loseQuest(int questId)
    {

        HandleQuestUpdateNotify(questList[questId], 1);

    }
    public static void winQuest(int questId)
    {

        HandleQuestUpdateNotify(questList[questId], 2);

    }
}

