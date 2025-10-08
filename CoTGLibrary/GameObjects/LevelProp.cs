using System.Collections.Generic;
using System.Numerics;
using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.Content;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

using static PacketVersioning.PktVersioning;

namespace CoTG.CoTGServer.GameObjects;

//Requires further research
public class LevelProp : GameObject
{
    public byte NetNodeID { get; set; }
    public float Height { get; set; }
    public Vector3 PositionOffset { get; set; }
    public Vector3 Scale { get; set; }
    public byte SkillLevel { get; set; }
    public byte Rank { get; set; }
    public byte Type { get; set; }
    string CurrentAnimationName { get; set; }
    string SkinName { get; set; }
    public override bool IsAffectedByFoW => false;
    public string Model;
    //?
    uint CurrentAnimationFlags { get; set; }
    string IdleAnimation;
    string ActiveAnimation;
    bool ShouldRandomizeIdleAnimationPhase;
    bool DoubleSided;

    public static List<LevelProp> ListLevelprop { get; set; } = new List<LevelProp>();

    public LevelProp(
        byte netNodeId,
        string name,
        string model,
        Vector3 position,
        Vector3 direction,
        Vector3 posOffset,
        Vector3 scale,
        int skinId = 0,
        byte skillLevel = 0,
        byte rank = 0,
        byte type = 2,
        uint netId = 64
    ) : base(new Vector2(position.X, position.Y), name, 0, 0, 0, netId, TeamId.TEAM_NEUTRAL)
    {
        NetNodeID = netNodeId;
        Height = position.Y;
        Direction = direction;
        PositionOffset = posOffset;
        Scale = scale;
        SkillLevel = skillLevel;
        Rank = rank;
        Type = type;
        Model = model;
        Game.ObjectManager.AddObject(this);
        ListLevelprop.Add(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        SpawnLevelPropNotify(this, userId, team);
    }

    internal override void OnAfterSync()
    {
    }

    protected override void OnSync(int userId, TeamId team)
    {
    }

    internal override void LateUpdate()
    {
    }
}