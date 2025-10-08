using System;
using System.Numerics;
using CoTGEnumNetwork.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CoTG.CoTGServer.Scripting.CSharp;
public class CampData
{
    public int GroupNumber { get; init; }
    public string TimerType { get; init; } = "";
    public AudioVOComponentEvent RevealEvent { get; init; }
    public float SpawnDuration { get; init; }
    public float RespawnTime { get; init; }
    public List<Vector3> Positions { get; init; } = new();
    public string MinimapIcon { get; init; } = "";
    public TeamId GroupBuffSide { get; init; }
    public float GroupDelaySpawnTime { get; init; }
    public Action? Timer { get; set; }
    public List<List<KeyValuePair<string, string>>> Groups { get; init; } = new();
    public List<List<string>> UniqueNames { get; init; } = new();

    public List<Dictionary<string, bool>> AliveTracker { get; init; } = new();
    public List<Vector3> FacePositions { get; init; } = new();
    public string AIScript = "";

    public static bool CampDataExists(List<CampData> campDataList, int groupNumber)
    {
        return campDataList.Any(campData => campData.GroupNumber == groupNumber);
    }

}
