using System;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using static ChildrenOfTheGraveEnumNetwork.Content.HashFunctions;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

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
