using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using System.Collections.Generic;

namespace MapScripts.GameModes;

public class DefaultGamemode : IGameModeScript
{
    protected Dictionary<ObjectType, List<MapObject>> _mapObjects = new();
    public virtual MapScriptMetadata MapScriptMetadata { get; } = new();
    public ILevelScript? LevelScript;
    public virtual void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> objects)
    {
        LevelScript = ApiMapFunctionManager.GetLevelScript();
        _mapObjects = objects;
    }

    public virtual void OnMatchPreStart()
    {
    }

    public virtual void OnMatchStart()
    {
    }

    public virtual void OnUpdate()
    {
    }

    //Other callbacks?
}