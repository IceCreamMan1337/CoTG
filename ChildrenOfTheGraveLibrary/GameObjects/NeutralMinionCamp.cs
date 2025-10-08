using System.Numerics;
using System.Collections.Generic;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using static PacketVersioning.PktVersioning;
namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;

public class NeutralMinionCamp : GameObject
{
    private TeamId[] _playerTeams = new TeamId[] { TeamId.TEAM_ORDER, TeamId.TEAM_CHAOS };
    private Dictionary<TeamId, bool> _teamSawLastDeath = new()
    {
        { TeamId.TEAM_ORDER, true },
        { TeamId.TEAM_CHAOS, true },
    };
    private Dictionary<TeamId, bool> _isAliveForTeam = new()
    {
        { TeamId.TEAM_ORDER, false },
        { TeamId.TEAM_CHAOS, false },
    };
    private Dictionary<int, bool> _isAliveForPlayer = new();

    public Vector3 CampPosition { get; private set; } = Vector3.Zero;
    public string CampIcon { get; private set; } = "";
    public List<Minion> Minions { get; private set; } = new();
    public int CampIndex { get; private set; } = 0;
    public TeamId BuffSide { get; internal set; } = TeamId.TEAM_UNKNOWN;
    internal AudioVOComponentEvent RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS;
    internal int TimerType = 0;
    //Dictionary<TeamId, bool>?
    internal bool KnownToBeActive = false;
    internal InterestComponent InterestComponent = new();
    internal float SpawnEndTime = 0;
    internal bool SpawnFinished = true;
    private int _epicness = 0;
    public override bool IsAffectedByFoW => true;

    //Custom?
    internal float SpawnDuration;
    internal float RespawnTime;

    public NeutralMinionCamp(
        string name,
        Vector3 worldPos,
        int epicness,
        short epicnessa,
        int campIndex

    ) : base(worldPos.ToVector2(), name, 0, 0, 0, team: TeamId.TEAM_NEUTRAL)
    {
        CampPosition = worldPos;
        _epicness = epicness;
        CampIndex = campIndex;
        CampIcon = GetDefaultMinimapIcon(epicnessa);
    }

    public NeutralMinionCamp(
        string name,
        Vector3 worldPos,
        string icon,
        string icona,
        int campIndex
    ) : base(worldPos.ToVector2(), name, 0, 0, 0, team: TeamId.TEAM_NEUTRAL)
    {
        CampPosition = worldPos;
        CampIndex = campIndex;
        CampIcon = icon;
    }

    internal static string GetDefaultMinimapIcon(int epicness)
    {
        return epicness switch
        {
            0 => "",
            1 => "Camp",
            2 => "Epic",
            3 => "HealthPack",
            4 => "Shrine",
            _ => ""
        };
    }

    internal void Init(Vector3 worldPos, string minimapIcon, int index, TeamId teamSide, AudioVOComponentEvent revealEvent, int timerType)
    {
        CampPosition = worldPos;
        CampIcon = minimapIcon;
        CampIndex = index;
        Position = CampPosition.ToVector2();
        Team = TeamId.TEAM_NEUTRAL;
        BuffSide = teamSide;
        RevealEvent = revealEvent;
        //SetObjFlags(this, this->ObjFlags | 2);
        TimerType = timerType;
        SpawnFinished = true;
        SpawnEndTime = 0.0f;
    }

    internal void InitPimpl()
    {
        KnownToBeActive = false;
        SpawnFinished = true;
        SpawnEndTime = 0.0f;
    }

    internal void AddMinion(Minion minion, Vector3 position)
    {
        minion.SetPosition(position.ToVector2());
        Minions.Add(minion);


        //hack for show the iconcamp
        //TODO: show the icon on minimap when the camp spawns without creating a region

        var bubble = new Region(
            TeamId.TEAM_ORDER, position.ToVector2(),
            visionTarget: minion,
            visionRadius: 10.0f,
            revealStealth: false,
            lifetime: 0.5f
        );

        var bubble2 = new Region(
           TeamId.TEAM_CHAOS, position.ToVector2(),
            visionTarget: minion,
            visionRadius: 10.0f,
            revealStealth: false,
            lifetime: 0.5f
       );
    }

    internal bool IsMinionInCamp(Minion minion)
    {
        return Minions.Contains(minion);
    }

    internal void AddMinionPimpl(Minion minion, Vector3 position, bool isMinionCamp, float spawnDuration)
    {
        //?
    }

    internal void KillMinion(Minion minion, ObjAIBase killer)
    {
        Minions.Remove(minion);
        if (Minions.Count is 0)
        {
            SetSpawnEndTime(Game.Time.GameTime + RespawnTime - SpawnDuration);
            Neutral_Camp_EmptyNotify(this, killer);
        }
    }

    internal void SetSpawnEndTime(float spawnEndTime)
    {
        SpawnFinished = false;
        SpawnEndTime = spawnEndTime;
    }

    //check
    internal override void Update()
    {
        if (!KnownToBeActive)
        {
            float currentTime = Game.Time.GameTime;
            if (currentTime > SpawnEndTime)
            {
                SpawnFinished = true;
                KnownToBeActive = true;
            }
        }
    }

    Champion? GetRevealHero(float unk, TeamId team)
    {
        float closestDistance = float.MaxValue;
        Champion? toReturn = null;

        foreach (Champion ch in Game.ObjectManager.GetAllChampions())
        {
            if (ch.Team == team)
            {
                float distance = Vector2.Distance(Position, ch.Position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    toReturn = ch;
                }
            }
        }

        return toReturn;
    }

    internal float GetTimerExpiry(/*int unk*/)
    {
        return SpawnEndTime - Game.Time.GameTime; //Check 
    }

    internal override void LateUpdate()
    {
        base.LateUpdate();
        foreach (TeamId team in _playerTeams)
        {
            if (IsVisibleByTeam(team))
            {
                //_isAliveForTeam[team] = IsAlive;
            }
        }
    }
    public override void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {
        base.Sync(userId, team, visible, forceSpawn);

        bool isAliveForTeam = _isAliveForTeam[team];
        bool isAliveForPlayer = _isAliveForPlayer.GetValueOrDefault(userId, false);
        if
        (
            (forceSpawn && isAliveForPlayer) // Reconnect
            || (isAliveForPlayer != isAliveForTeam
                // TODO: Handle based on vision radius, not status (also handle null peer info)
                && (Game.PlayerManager.GetPeerInfo(userId).Champion.Status & StatusFlags.NearSighted) == 0)
        )
        {
            if (_isAliveForPlayer[userId] == isAliveForTeam)
            {
                //activate minioncamp not used in Chronoshift replay 
                ActivateMinionCampNotify(this, userId);
                //Game.PacketNotifier126.NotifyS2C_Neutral_Camp_Empty(this, userId: userId);
            }
            else
            {
                //Game.PacketNotifier126.NotifyS2C_ActivateMinionCamp(this, userId);
                Neutral_Camp_EmptyNotify(this, userId: userId);
            }
        }
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision = false)
    {
        CreateMinionCampNotify(this, -1, team);

    }
    protected override void OnEnterVision(int userId, TeamId team)
    {
    }
    protected override void OnLeaveVision(int userId, TeamId team)
    {
    }

    public override void SetTeam(TeamId team)
    {
        Team = team;
    }
}

class InterestComponent
{
    //int (**_vptr$InterestComponent)(void); unk
    //?
    GameObject Owner;
    float LastUpdateTime;
    float DampeningTime;
    //EventSystem::GlobalEventDispatcher::Callback mDestroyAllObjectsCallback;
}
