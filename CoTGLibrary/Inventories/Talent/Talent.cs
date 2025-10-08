using System;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Scripting.CSharp;
using static CoTGEnumNetwork.Content.HashFunctions;

namespace CoTG.CoTGServer.GameObjects;

public class Talent
{
    public string Id { get; }
    public byte Rank { get; }
    public ITalentScript Script => ScriptInternal;
    internal ITalentScriptInternal ScriptInternal { get; }
    public TalentData TalentData { get; }
    public uint ScriptNameHash { get; private set; }
    public IEventSource? ParentScript => null;

    public Talent(TalentData data, byte level)
    {
        Id = data.Id;
        Rank = Math.Min(level, data.MaxLevel);
        ScriptInternal = Game.ScriptEngine.CreateObject<ITalentScriptInternal>("Talents", $"Talent_{Id}", Game.Config.SupressScriptNotFound) ?? new EmptyTalentScript();
        ScriptNameHash = HashString(Id.ToString());
    }
}
