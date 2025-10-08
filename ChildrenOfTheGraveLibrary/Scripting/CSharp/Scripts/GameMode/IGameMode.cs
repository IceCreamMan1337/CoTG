using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;
using System.Collections.Generic;

namespace MapScripts.GameModes;

public interface IGameModeScript
{
    MapScriptMetadata MapScriptMetadata { get; }
    void OnLevelLoad(Dictionary<ObjectType, List<MapObject>> objects)
    {
    }

    void OnMatchPreStart()
    {
    }

    void OnMatchStart()
    {
    }

    void OnUpdate()
    {
    }

    //Other callbacks?
}