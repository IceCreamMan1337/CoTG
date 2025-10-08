#nullable enable

using CoTG.CoTGServer.Scripting.CSharp;

namespace CoTG.CoTGServer.Scripting.Lua;

public class BBBuffScriptEmpty : BuffGameScript
{
    private readonly BuffScriptMetaData _metadata;
    public override BuffScriptMetaData BuffMetaData => _metadata;

    public BBBuffScriptEmpty()
    {
        _metadata = Functions.NextBBBuffScriptCtrArgs ?? base.BuffMetaData;
    }
}