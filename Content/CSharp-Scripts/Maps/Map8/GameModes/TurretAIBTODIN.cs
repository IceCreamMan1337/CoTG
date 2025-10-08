namespace BehaviourTrees.Map8.AI_manager;

class TurretAIBTODIN : BehaviourTree
{
    TeamId currentteam;
    AttackableUnit Self;
    Vector3 Position;
    TeamId tempTeam; // Temporary variable for GetUnitTeam
                     // private OdinNeutralGuardianBehaviorClass odinNeutralGuardianBehaviorClass = new OdinNeutralGuardianBehaviorClass();
    private OdinGuardianBehaviorClass odinGuardianBehaviorClass;
    private OdinTowerBehaviorClass odinTowerBehaviorClass;

    public TurretAIBTODIN()
    {
        // Initialize instances even in the default constructor
        odinGuardianBehaviorClass = new OdinGuardianBehaviorClass();
        odinTowerBehaviorClass = new OdinTowerBehaviorClass();
    }

    public TurretAIBTODIN(Minion owner) : base(owner)
    {
        currentteam = owner.Team;
        Self = owner;
        Position = owner.Position3D;

        // Create unique instances for this unit
        odinGuardianBehaviorClass = new OdinGuardianBehaviorClass();
        odinTowerBehaviorClass = new OdinTowerBehaviorClass();

        //  AddBuff("OdinParticlePHBuff", 25000, 1, null, owner, null);
        //   owner.Spells.Passive.Activate();
        //   Console.WriteLine("BT loaded of TUREEEEEET");
    }

    // Method to initialize the owner after creation by ScriptEngine
    public void InitializeOwner(Minion owner)
    {
        if (Owner == null)
        {
            Owner = owner;
            currentteam = owner.Team;
            Self = owner;
            Position = owner.Position3D;
        }
    }



    public override void Update()
    {

        base.Update();
        turretBehavior();


    }
    bool turretBehavior()
    {
        return

              (DebugAction("Turretbehavior readed") &&
              GetUnitTeam(out tempTeam, this.Owner) &&
              SetVarUnitTeam(out currentteam, tempTeam) &&
              currentteam == tempTeam

              &&
                    currentteam == TeamId.TEAM_NEUTRAL
                    && odinGuardianBehaviorClass.OdinGuardianBehavior(this.Owner))



                 ||
                 (
                    currentteam != TeamId.TEAM_NEUTRAL
                     && odinTowerBehaviorClass.OdinTowerBehavior(this.Owner)



                 )
        ;



    }
}