using MoonSharp.Interpreter;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;

public interface IBBMetadata
{
    public void Parse(Table dynValue);
}