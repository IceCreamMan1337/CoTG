using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.Scripting.CSharp;
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