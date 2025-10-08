using CoTG.CoTGServer.Content.FileSystem;

namespace CoTG.CoTGServer.Content;
public class TalentData
{
    public string Id { get; init; }
    public byte MaxLevel { get; init; }

    public TalentData(string name)
    {
        RFile? f = Cache.GetFile(name);
        MaxLevel = (byte?)f?.GetValue("SpellData", "Ranks", 1) ?? 1;

        Id = name;
        MaxLevel = 1;
    }
}
