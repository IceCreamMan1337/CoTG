using System.IO;
using System.Numerics;
using System.Text;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveLibrary.Extensions;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;

public class MapObject
{
    //Original game's file ID and version (Patch 4.17)
    internal const uint kFileID = 1296126031u;
    internal const uint kFileVer = 2u;

    public string Name { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector3 Position3D { get; private set; }
    public ObjectType Type { get; private set; } = (ObjectType)(-1);
    internal byte IgnoreCollisionOnPlacement { get; private set; } //bool?
    internal Vector3 Rotation { get; private set; } = Vector3.Zero;
    internal Vector3 Scale { get; private set; } = Vector3.One;
    internal Vector3 BoundingBoxMin { get; private set; } = Vector3.Zero;
    internal Vector3 BoundingBoxMax { get; private set; } = Vector3.Zero;
    internal int SkinID { get; private set; } = 0;

    //Custom
    public TeamId Team = TeamId.TEAM_UNKNOWN;
    public Lane Lane = Lane.LANE_Unknown;

    internal MapObject(BinaryReader br)
    {
        Name = new string(Encoding.ASCII.GetString(br.ReadBytes(62))).TrimEnd('\0');
        Type = (ObjectType)br.ReadByte();
        IgnoreCollisionOnPlacement = br.ReadByte();
        Position3D = br.ReadVector3();
        Rotation = br.ReadVector3();
        Scale = br.ReadVector3();
        BoundingBoxMin = br.ReadVector3();
        BoundingBoxMax = br.ReadVector3();
        SkinID = br.ReadInt32();

        Position = Position3D.ToVector2();
        GetTeam();
        GetLane();
    }

    internal MapObject(string name, Vector3 point)
    {
        Name = name;
        Position3D = point;
        Position = point.ToVector2();
        GetObjType();
        GetTeam();

        GetLane();

    }

    //TODO: deprecate
    public TeamId GetOpposingTeamID()
    {
        return
        (Team == TeamId.TEAM_ORDER)
        ?
            TeamId.TEAM_CHAOS
        :
        (Team == TeamId.TEAM_CHAOS)
        ?
            TeamId.TEAM_ORDER
        :
        TeamId.TEAM_NEUTRAL
        ;
    }

    private void GetTeam()
    {
        if (Name.Contains("T1") || Name.ToLower().Contains("order"))
        {
            Team = TeamId.TEAM_ORDER;
        }
        else if (Name.Contains("T2") || Name.ToLower().Contains("chaos"))
        {
            Team = TeamId.TEAM_CHAOS;
        }
    }

    private void GetLane()
    {
        if (Game.Config.GameConfig.Map != 8)
        {

            if (Type == ObjectType.Barracks)
            {
                if (Name.Contains("__L"))
                {
                    Lane = Lane.LANE_L;
                }
                else if (Name.Contains("__C"))
                {
                    Lane = Lane.LANE_C;
                }
                else if (Name.Contains("__R"))
                {
                    Lane = Lane.LANE_R;
                }
            }
            else
            {
                if (Name.Contains("_L"))
                {
                    Lane = Lane.LANE_L;
                }
                //Using just _C would cause files with "_Chaos" to be mistakenly assigned as MidLane
                else if (Name.Contains("_C0") || Name.Contains("_C1") || Name.Contains("_C_"))
                {
                    Lane = Lane.LANE_C;
                }
                else if (Name.Contains("_R"))
                {
                    Lane = Lane.LANE_R;
                }
            }

        }
        else
        {
            if (Name.Contains("_0_"))
            {
                Lane = Lane.LANE_R;
            }
            //Using just _C would cause files with "_Chaos" to be mistakenly assigned as MidLane
            else if (Name.Contains("_1_"))
            {
                Lane = Lane.LANE_C;
            }
            else if (Name.Contains("_2_"))
            {
                Lane = Lane.LANE_L;
            }
            else if (Name.Contains("_3_"))
            {
                Lane = Lane.LANE_3;
            }
            else if (Name.Contains("_4_"))
            {
                Lane = Lane.LANE_4;
            }
            else if (Name.Contains("_5_"))
            {
                Lane = Lane.LANE_5;
            }
            else if (Name.Contains("_6_"))
            {
                Lane = Lane.LANE_6;
            }
            else if (Name.Contains("_7_"))
            {
                Lane = Lane.LANE_7;
            }
            else if (Name.Contains("_8_"))
            {
                Lane = Lane.LANE_8;
            }
            else if (Name.Contains("_9_"))
            {
                Lane = Lane.LANE_9;
            }

        }

    }

    private void GetObjType()
    {
        if (Name.Contains("Spawn_Barracks"))
        {
            Type = ObjectType.Barracks;
        }
        else if (Name.Contains("Info_"))
        {
            Type = ObjectType.InfoPoint;
        }
        else if (Name.Contains("Spawn"))
        {
            Type = ObjectType.SpawnPoint;
        }
        //LevelSize?
        else if (Name.Contains("Barracks"))
        {
            Type = ObjectType.Inhibitor;
        }
        else if (Name.Contains("HQ"))
        {
            Type = ObjectType.Nexus;
        }
        else if (Name.Contains("Turret"))
        {
            Type = ObjectType.Turret;
        }
        else if (Name.Contains("Shop"))
        {
            Type = ObjectType.Shop;
        }
        else if (Name.Contains("Lake"))
        {
            Type = ObjectType.Lake;
        }
        else if (Name.Contains("__NAV"))
        {
            Type = ObjectType.NavPoint;
        }
        else if (Name.Contains("LevelProp"))
        {
            Type = ObjectType.LevelProp;
        }
    }
}