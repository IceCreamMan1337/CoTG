namespace MapScripts.Map10.GameModes;

class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> objects)
    {
        base.OnLevelLoad(objects);

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        CreateTimedEvent(() => AnnounceStartGameMessage(1, 10), 30);
        CreateTimedEvent(() => { AnnounceStartGameMessage(3, 10); AnnouceNexusCrystalStart(); }, 75);
        CreateTimedEvent(() => AnnounceStartGameMessage(2, 10), 150);
        CreateTimedEvent(() => AnnounceStartGameMessage(4, 10), 180);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}