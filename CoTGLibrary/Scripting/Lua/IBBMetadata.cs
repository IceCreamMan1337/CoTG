using MoonSharp.Interpreter;

namespace CoTG.CoTGServer.Scripting.Lua;

public interface IBBMetadata
{
    public void Parse(Table dynValue);
}