namespace MapScripts.Map4.GameModes;

public class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> objects)
    {
        base.OnLevelLoad(objects);

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        CreateTimedEvent(() => AnnounceStartGameMessage(1, 4), 30);
        CreateTimedEvent(() => { AnnounceStartGameMessage(2, 4); }, 60);
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 90);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}