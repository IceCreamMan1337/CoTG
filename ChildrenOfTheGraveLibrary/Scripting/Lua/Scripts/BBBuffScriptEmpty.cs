#nullable enable

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;

public class BBBuffScriptEmpty : BuffGameScript
{
    private readonly BuffScriptMetaData _metadata;
    public override BuffScriptMetaData BuffMetaData => _metadata;

    public BBBuffScriptEmpty()
    {
        _metadata = Functions.NextBBBuffScriptCtrArgs ?? base.BuffMetaData;
    }
}