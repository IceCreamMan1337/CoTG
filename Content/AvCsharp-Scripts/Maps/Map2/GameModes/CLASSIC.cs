namespace MapScripts.Map2.GameModes;

class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> objects)
    {
        base.OnLevelLoad(objects);

        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        CreateTimedEvent(() => AnnounceStartGameMessage(1, 1), 30);
        CreateTimedEvent(() => { AnnounceStartGameMessage(2, 1); }, 60);
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 90);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}
