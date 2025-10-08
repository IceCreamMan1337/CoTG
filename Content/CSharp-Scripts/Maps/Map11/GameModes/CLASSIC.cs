namespace MapScripts.Map11.GameModes;

public class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> mapObjects)
    {
        base.OnLevelLoad(mapObjects);

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        // Welcome to Summoners Rift
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 11), 30);
        // 30 seconds until minions spawn
        CreateTimedEvent(() => AnnounceStartGameMessage(2, 11), 60);
        // Minions have spawned
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 90);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}
