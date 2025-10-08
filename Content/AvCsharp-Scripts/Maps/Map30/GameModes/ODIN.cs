using ChildrenOfTheGrave.ChildrenOfTheGraveServer;
using BehaviourTrees;

namespace MapScripts.Map30.GameModes;

public class ODIN : DefaultGamemode
{
    bool OdinOpeningCeremony_bool = true;
    private SummonerGamePointManagerClass SummonerGamePointManagerclass = new SummonerGamePointManagerClass();
    public override MapScriptMetadata MapScriptMetadata { get; } = new()
    {
        RecallSpellItemId = 2007,
        InitialLevel = 6,
    };

    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> mapObjects)
    {
        base.OnLevelLoad(mapObjects);

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);
        // CreateLevelProps.CreateProps();
        // Welcome to the Howling Abyss 
        CreateTimedEvent(() => SummonerGamePointManagerclass.SummonerGamePointManager(), 5);
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 3), 30);
        // Minions have spawned
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 60);
    }

    public override void OnMatchStart()
    {

        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();

        foreach (var champion in GetAllPlayers())
        {
            AddBuff("SummonersGamePlayerBuff", 25000, 1, null, champion, null);
        }

    }

    public override void OnUpdate()
    {




    }

}