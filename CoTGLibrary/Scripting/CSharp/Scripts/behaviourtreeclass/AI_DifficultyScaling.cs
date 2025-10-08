using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using System.Collections.Generic;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class AI_DifficultyScaling : CommonAI

{


    public float CurrentGameTime;


    public IEnumerable<LaneTurret> Turrets;

    public int TurretCount;

    public TeamId TurretTeam;

    public int Count;

    public bool IsDifficultyInitialized;


    public bool PrevWinState;

    public int NumKillsChaos;

    public int NumKillsOrder;




    public TeamId EntityTeam;

    public int Kills;

    public int KillsDiff;


    public IEnumerable<Champion> AllEntities;










}

