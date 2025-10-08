//#nullable enable

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Tips;
using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Enums;
using ChildrenOfTheGraveLibrary.GameObjects;
using log4net;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.API.ApiFunctionManager;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Spell;
using static PacketVersioning.PktVersioning;
using apimap = ChildrenOfTheGrave.ChildrenOfTheGraveServer.API.ApiMapFunctionManager;
using Color = ChildrenOfTheGraveEnumNetwork.Content.Color;
using Extensions = ChildrenOfTheGraveEnumNetwork.Extensions;
using FCS = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted.Functions_CS;
using FLS = ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions;
using gameannouncement = ChildrenOfTheGrave.ChildrenOfTheGraveServer.API.GameAnnouncementManager;
using VisionManager = ChildrenOfTheGraveLibrary.Vision.VisionManager;

//using static LENet.LList<T>;
//using static LENet.LList<T>;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class TeamMessage
{
    public AttackableUnit SourceUnit { get; set; }
    public AttackableUnit TargetUnit { get; set; }
    public AITaskTopicType AITaskTopic { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}

public class BehaviourTree
{
    public ObjAIBase Owner { get; set; }
    public readonly Vector3 defaultposition = Vector3.One;
    private int timesExecuted = 0;
    public List<AttackableUnit> AIEntitiesAssociated { get; set; } = new List<AttackableUnit>();

    private int count = 0;
    private bool _firstTimeExecuted = false;

    // Dictionary to store tasks assigned to champions
    private static readonly Dictionary<string, List<AITask>> _assignedTasks = new();
    private static readonly Dictionary<AITask, Dictionary<string, object>> _taskBehaviorTrees = new();
    private static readonly Dictionary<AIMissionClass, Dictionary<string, object>> _missionBehaviorTrees = new();

    // Queue to store team messages
    private readonly Queue<TeamMessage> _teamMessageQueue = new();

    public TeamId aimanagerTeam = TeamId.TEAM_UNKNOWN;

    private ILog _logger = LoggerProvider.GetLogger();

    public BehaviourTree(ObjAIBase owner = default)
    {
        Owner = owner;
    }

    /// <summary>
    /// Sets the owner and propagates it to child nodes if necessary
    /// </summary>
    public void SetOwner(ObjAIBase owner)
    {
        Owner = owner;
        // Propagate the owner to child nodes if this class has child nodes
        // Only propagate if it's not a map behaviour tree (avoid conflicts)
        if (!IsMapBehaviourTree() && owner is Champion)
        {
            PropagateOwnerToChildren(owner);
        }
    }

    /// <summary>
    /// Determines if this behaviour tree is used for maps (and not for champions)
    /// </summary>
    protected virtual bool IsMapBehaviourTree()
    {
        // By default, we consider it's a map behaviour tree if the namespace contains "Map"
        var type = GetType();
        var namespaceName = type.Namespace ?? "";
        return namespaceName.Contains("Map") && !namespaceName.Contains("all");
    }

    /// <summary>
    /// Propagates the owner to child nodes. To be overridden in derived classes if necessary.
    /// </summary>
    protected virtual void PropagateOwnerToChildren(ObjAIBase owner)
    {
        // Use reflection to find all private fields that are instances of BehaviourTree
        var type = this.GetType();
        var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        foreach (var field in fields)
        {
            var value = field.GetValue(this);
            if (value is BehaviourTree btInstance && btInstance.Owner == null)
            {
                btInstance.Owner = owner;
            }
        }
    }

    public virtual void Update()
    {
        timesExecuted++;
    }

    public char GetCharFromInt(int value)
    {
        // Assurez-vous que la valeur est dans la plage souhait�e
        if (value >= 0 && value <= 25)
        {
            // Convertissez la valeur en caract�re en utilisant la table ASCII
            // 'A' a une valeur ASCII de 65, donc ajoutez simplement la valeur pour obtenir le caract�re correspondant
            return (char)('A' + value);
        }
        else
        {
            // G�rez le cas o� la valeur est en dehors de la plage souhait�e (par exemple, renvoyez un caract�re par d�faut)
            return ' ';
        }
    }
    public BehaviourTree InitMapBTScript(string _namespace, string _name)
    {
        var BTScript = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}.{_namespace}", _name) ?? new BehaviourTree();
        return BTScript;
    }

    //TODO: Rename
    protected bool ForEach<T>(IEnumerable<T> Collection, Func<T, bool>? Child0 = null)
    {
        if (Collection != null && Child0 != null)
        {
            bool allSucceeded = true;
            foreach (var item in Collection)
            {
                bool result = Child0(item);
                if (!result)
                {
                    allSucceeded = false;
                }
            }
            return allSucceeded;  // Returns true only if all iterations succeed
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Creates a dynamic BTInstance based on the name of the provided tree.
    /// </summary>
    /// <remarks>
    /// For now this should generally be used for one-off tress that delete themselves since references outside of this function are difficult
    /// </remarks>
    /// <param name="TreeName">Name of the tree to instantiate</param>
    /// <param name="Type">The type of the instance.
    /// For now I strongly recommend only using DELETE_SELF.</param>
    //protected   bool CreateDynamicBTInstance(string TreeName, BTInstanceType Type)
    //{
    //    return false;
    //}

    /// <summary>
    /// Debug node used to return an explicit value and write a string to log.
    /// </summary>
    /// <param name="String">The string that should be outputted to log</param>
    protected bool DebugAction(string String)
    {
        return true;
    }

    /// <summary>
    /// Decorator that allows user to specify the number of times the subtree will run.
    /// </summary>
    /// <remarks>
    /// Need comment
    /// </remarks>
    /// <param name="RunningLimit">The number of times the sub tree is to be executed</param>
    protected async Task<bool> LoopNTimes(int RunningLimit, Func<Task<bool>>? Child0 = null)
    {
        return false;
    }

    /// <summary>
    /// Return RUNNING for X seconds after first tick.
    /// </summary>
    /// <remarks>
    /// This is a blocking delay and it uses the real timer not the game timer, so it is unaffected by pause.
    /// </remarks>
    /// <param name="DelayAmount">The amount of time to delay after first tick.</param>
    protected async Task<bool> DelayNSecondsBlocking(float DelayAmount)
    {
        return false;
    }


    /// <summary>
    /// Enable/Disable a quest by name
    /// </summary>
    /// <remarks>
    /// This will fail if the quest does not exist
    /// </remarks>
    /// <param name="Enabled">Should the quest be enabled or disabled?</param>
    /// <param name="Name">The name of the quest to adjust</param>


    protected bool SetBTInstanceStatus(bool Enabled, string Name)
    {
        return true;
    }

    /// <summary>
    /// Enable/Disable a quest by name
    /// </summary>
    /// <remarks>
    /// This will fail if the quest does not exist
    /// </remarks>
    /// <param name="Enabled">Should the quest be enabled or disabled?</param>
    /// <param name="Name">The bool of the quest to adjust</param>


    protected bool SetBTInstanceStatusbool(out bool Enabled, bool Name)
    {
        Enabled = Name;
        return true;
    }


    /// <summary>
    /// Set all map barracks active/inactive
    /// </summary>
    /// <remarks>
    /// This functionally is the same as the kill minions cheat
    /// </remarks>
    /// <param name="Enable">The status of the barracks</param>
    protected bool SetBarrackStatus(bool Enable)
    {
        return false;
    }

    /// <summary>
    /// Display objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    protected bool ShowObjectiveText(string String)
    {
        return false;
    }

    /// <summary>
    /// Hide objective text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no objective text is displayed
    /// </remarks>
    protected bool HideObjectiveText()
    {
        return false;
    }

    /// <summary>
    /// Display auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="String">The localized string to display.</param>
    protected bool ShowAuxiliaryText(string String)
    {
        return false;
    }

    /// <summary>
    /// Hide auxiliary text using the Tutorial1 flash element
    /// </summary>
    /// <remarks>
    /// Will always return success even if no auxiliary text is displayed
    /// </remarks>
    protected bool HideAuxiliaryText()
    {
        return false;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for bool References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarBool(out bool Output, bool Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarAttackableUnit(out AttackableUnit Output, AttackableUnit Input)
    {
        Output = Input;



        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarInt(out int Output, int Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for int References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarDWORD(out uint Output, uint Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for string References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarString(out string Output, string Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for float References
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarFloat(out float Output, float Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Sets OutputRef with the value of Input
    /// </summary>
    /// <remarks>
    /// This version is for Vector References.
    /// If you want to make a vector out of 3 floats, use MakeVector.
    /// </remarks>
    /// <param name="Output">Destination Reference</param>
    /// <param name="Input">Source Reference</param>
    protected bool SetVarVector(out Vector3 Output, Vector3 Input)
    {
        Output = Input;
        return true;
    }

    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool AddInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide + RightHandSide;
        return true;
    }

    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool SubtractInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide - RightHandSide;
        return true;
    }

    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MultiplyInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide * RightHandSide;
        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool DivideInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide / RightHandSide;
        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool ModulusInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        Output = LeftHandSide % RightHandSide;
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MinInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        // Initialize the Output variable with a default value (typically 0 for integers).
        Output = default;

        // Compare the LeftHandSide and RightHandSide values.
        if (LeftHandSide < RightHandSide)
        {
            // If LeftHandSide is less than RightHandSide, assign the value of LeftHandSide to Output.
            Output = LeftHandSide;
        }
        else
        {
            // If RightHandSide is less than or equal to LeftHandSide, assign the value of RightHandSide to Output.
            Output = RightHandSide;
        }

        // Return a boolean value indicating that the operation was successful.
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Ints.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MaxInt(out int Output, int LeftHandSide, int RightHandSide)
    {
        // Initialize the Output variable with a default value (typically 0 for integers).
        Output = default;

        // Compare the LeftHandSide and RightHandSide values.
        if (LeftHandSide > RightHandSide)
        {
            // If LeftHandSide is greater than RightHandSide, assign the value of LeftHandSide to Output.
            Output = LeftHandSide;
        }
        else
        {
            // If RightHandSide is greater than or equal to LeftHandSide, assign the value of RightHandSide to Output.
            Output = RightHandSide;
        }

        // Return a boolean value indicating that the operation was successful.
        return true;
    }

    /// <summary>
    /// Adds the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool AddFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide + RightHandSide;
        return true;
    }

    /// <summary>
    /// Subtracts the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool SubtractFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide - RightHandSide;
        return true;
    }

    /// <summary>
    /// Multiplies the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MultiplyFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide * RightHandSide;

        return true;
    }

    /// <summary>
    /// Divides the LeftHandSide to the RightHandSide and places the result in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool DivideFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        Output = LeftHandSide / RightHandSide;
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the lesser value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MinFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        // Initialize the Output variable with a default value (typically 0.0 for floats).
        Output = default;

        // Compare the LeftHandSide and RightHandSide values.
        if (LeftHandSide < RightHandSide)
        {
            // If LeftHandSide is less than RightHandSide, assign the value of LeftHandSide to Output.
            Output = LeftHandSide;
        }
        else
        {
            // If RightHandSide is less than or equal to LeftHandSide, assign the value of RightHandSide to Output.
            Output = RightHandSide;
        }

        // Return a boolean value indicating that the operation was successful.
        return true;
    }

    /// <summary>
    /// Compares LeftHandSide to the RightHandSide and places the greater value in Output
    /// </summary>
    /// <remarks>
    /// This version is for Floats.
    /// This will always return SUCCESS. 
    /// </remarks>
    /// <param name="Output">Output reference of the operation</param>
    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    protected bool MaxFloat(out float Output, float LeftHandSide, float RightHandSide)
    {
        // Initialize the Output variable with a default value (typically 0.0 for floats).
        Output = default;

        // Compare the LeftHandSide and RightHandSide values.
        if (LeftHandSide > RightHandSide)
        {
            // If LeftHandSide is greater than RightHandSide, assign the value of LeftHandSide to Output.
            Output = LeftHandSide;
        }
        else
        {
            // If RightHandSide is greater than or equal to LeftHandSide, assign the value of RightHandSide to Output.
            Output = RightHandSide;
        }

        // Return a boolean value indicating that the operation was successful.
        return true;
    }

    /// <summary>
    /// Gets a handle to the player and puts it in OutputRef
    /// </summary>
    /// <remarks>
    /// Only works in Tutorial, or other situation where there's only one player.
    /// Works by getting the first player in the roster that has a legal client ID.
    /// </remarks>
    /// <param name="Output">Destination reference; holds a hero object handle</param>
    protected bool GetTutorialPlayer(out AttackableUnit Output)
    {

        Output = default;
        return false;
    }

    /// <summary>
    /// Returns a handle to a collection containing all champions in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains champions who have disconnected or are played by bots.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all champions in the game.</param>
    protected bool GetChampionCollection(out IEnumerable<Champion> Output)
    {
        Output = Game.PlayerManager.GetPlayers(true).Select(info => info.Champion);
        // Output = Game.ObjectManager.GetAllChampions();
        return true;
    }

    /// <summary>
    /// Returns a handle to a collection containing all turrets alive in the game.
    /// </summary>
    /// <remarks>
    /// This is an unfiltered collection, so it contains turrets on both teams.
    /// </remarks>
    /// <param name="Output">Destination reference; holds the collection of all turrets alive in the game.</param>
    protected bool GetTurretCollection(out IEnumerable<LaneTurret> Output)
    {
        Output = LaneTurret.GetLivingTurrets();
        return true;
    }

    /// <summary>
    /// Gets a handle to the turret in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the turret is not alive, should confirm.
    /// </remarks>
    /// <param name="Turret">Destination Reference; holds a turret object handle</param>
    /// <param name="Team">Team of the turrets to be checked.</param>
    /// <param name="Lane">Lane of the turret.
    /// Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.
    /// Check the level script for the enum.</param>
    protected bool GetTurret(out AttackableUnit Turret, TeamId Team, int _Lane, int Position)
    {


        Turret = LaneTurret.GetTurret(Team, (Lane)_Lane, Position);
        return true;

    }

    /// <summary>
    /// Gets a handle to the inhibitor in a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the inhibitor is not alive, should confirm.
    /// </remarks>
    /// <param name="Inhibitor">Destination Reference; holds an inhibitor object handle</param>
    /// <param name="Team">Team of the inhibitor to be checked.</param>
    /// <param name="Lane">Lane of the inhibitor.
    /// Check the level script for the enum.</param>
    protected bool GetInhibitor(out AttackableUnit Inhibitor, TeamId Team, int Lane)
    {
        //todo : possibly used for mapscript BT , for moment not used in garen bt 

        Inhibitor = default;
        return false;
    }

    /// <summary>
    /// Gets a handle to the nexus on a specific teamin a specific lane
    /// </summary>
    /// <remarks>
    /// I think this will return FAILURE if the Nexus is not alive, should confirm.
    /// </remarks>
    /// <param name="Nexus">Destination Reference; holds a nexus object handle</param>
    /// <param name="Team">Team of the nexus to return.</param>
    protected bool GetNexus(out AttackableUnit Nexus, TeamId Team)
    {
        //todo : possibly used for mapscript BT , for moment not used in garen bt 
        Nexus = default;
        return false;
    }

    /// <summary>
    /// Returns the current position of a specific unit
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current position of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitPosition(out Vector3 Output, AttackableUnit Unit)
    {
        Output = Unit.Position3D;
        return true;
    }

    /// <summary>
    /// Returns the current elapsed game time.
    /// This will be affected by pausing, cheats, or other things.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the currently elapsed game time.</param>
    protected bool GetGameTime(out float Output)
    {
        //Output = Game.Time / 1000f;

        Output = Game.Time.GameTime / 1000f;

        return true;
    }

    /// <summary>
    /// Returns the lane position of a turret.
    /// </summary>
    /// <remarks>
    /// This position is defined in the level script and is map specific.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the integer position of the turret.
    /// This is defined in the level script.</param>
    /// <param name="Turret">Turret to poll.</param>
    protected bool GetTurretPosition(out int Output, AttackableUnit Turret)
    {
        LaneTurret _turret = Turret as LaneTurret;
        Output = _turret.TurretIndex;
        return true;
    }

    /// <summary>
    /// Returns the max health of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the max health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitMaxHealth(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.HealthPoints.Total;
        return true;
    }

    /// <summary>
    /// Returns the health ratio of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the max health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitHealthRatio(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.CurrentHealth / Unit.Stats.HealthPoints.Total;
        return true;
    }

    /// <summary>
    /// Returns the health ratio of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the max health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitPARRatio(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType par)
    {
        Output = Unit.Stats.CurrentMana / Unit.Stats.ManaPoints.Total;
        return true;
    }

    /// <summary>
    /// Returns the current health of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current health of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitCurrentHealth(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.CurrentHealth;
        return true;
    }

    /// <summary>
    /// Returns the current Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT health
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current Primary Ability Resource value of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    protected bool GetUnitCurrentPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType)
    {
        switch (PrimaryAbilityResourceType)
        {
            case PrimaryAbilityResourceType.MANA:
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Energy:
                // Assume that there is a property in AttackableUnit to get the current energy value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Shield:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Wind:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Battlefury:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Dragonfury:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Rage:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Heat:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.Ferocity:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            case PrimaryAbilityResourceType.BloodWell:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.CurrentMana;
                break;
            // Add cases for other PrimaryAbilityResourceType values if needed
            default:
                Output = Unit.Stats.CurrentHealth;
                break;
        }
        return true;
    }

    /// <summary>
    /// Returns the maximum Primary Ability Resource of a specific unit
    /// </summary>
    /// <remarks>
    /// MAX PAR
    /// </remarks>
    /// <param name="Output">Destination reference; contains the maximum Primary Ability Resource value of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource type.</param>
    protected bool GetUnitMaxPAR(out float Output, AttackableUnit Unit, PrimaryAbilityResourceType PrimaryAbilityResourceType)
    {
        switch (PrimaryAbilityResourceType)
        {
            case PrimaryAbilityResourceType.MANA:
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Energy:
                // Assume that there is a property in AttackableUnit to get the current energy value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Shield:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Wind:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Battlefury:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Dragonfury:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Rage:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Heat:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.Ferocity:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            case PrimaryAbilityResourceType.BloodWell:
                // Assume that there is a property in AttackableUnit to get the current shield value
                Output = Unit.Stats.ManaPoints.Total;
                break;
            // Add cases for other PrimaryAbilityResourceType values if needed
            default:
                Output = Unit.Stats.HealthPoints.Total;
                break;
        }
        return true;
    }

    /// <summary>
    /// Returns the current armor of a specific unit
    /// </summary>
    /// <remarks>
    /// CURRENT armor
    /// </remarks>
    /// <param name="Output">Destination reference; contains the current armor of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitArmor(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.Armor.Total;
        return true;
    }

    /// <summary>
    /// Returns the number of discrete elements contained within the collection.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the number of elements in the collection.</param>
    /// <param name="Collection">Collection to count.</param>
    protected bool GetCollectionCount(out int Output, IEnumerable<AttackableUnit> Collection)
    {
        Output = Collection.Count();
        return true;
    }

    /// <summary>
    /// Returns the current skin of a specific unit
    /// </summary>
    /// <remarks>
    /// Since buildings don't hame skins, it will return the name of the building.
    /// </remarks>
    /// <param name="Output">Destination reference; contains the skin name of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitSkinName(out string Output, AttackableUnit Unit)
    {
        if (Unit is Champion)
        {
            Champion _champion = Unit as Champion;
            if (_champion.HasSkins)
            {
                //todo : 
                // Skin.Skinname.search(x => x == _champion.SkinID) 

            }
            //else 
            Output = _champion.Name;
        }
        else
        {
            Output = Unit.Name;
        }
        return true;
    }

    /// <summary>
    /// Turns on or off the highlight of a unit.
    /// </summary>
    /// <remarks>
    /// Creates a unit highlight akin to what is used in the tutorial.
    /// This highlight is by default blue.
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Enable">Should the Highlight be turned on or turned off?</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    protected bool ToggleUnitHighlight(bool Enable, AttackableUnit TargetUnit)
    {
        //TODO: Implement hightlight packet .
        return true;
    }

    /// <summary>
    /// Pings a unit on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.
    /// Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.
    /// Important for team coloration and chat info.</param>
    /// <param name="TargetUnit">Unit to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    protected bool PingMinimapUnit(AttackableUnit PingingUnit, AttackableUnit TargetUnit, bool PlayAudio)
    {
        OnMapPingNotify(TargetUnit.Position, Pings.PING_DEFAULT, PingingUnit.NetId);
        return true;
    }

    /// <summary>
    /// Pings a location on the minimap.
    /// </summary>
    /// <remarks>
    /// Which team receives the ping is determined by the PingingUnit.
    /// Currently this block can not ping for both teams simultaneously.
    /// </remarks>
    /// <param name="PingingUnit">Unit originating the ping.
    /// Important for team coloration and chat info.</param>
    /// <param name="TargetPosition">Location to be pinged.</param>
    /// <param name="PlayAudio">Play audio with ping?</param>
    protected bool PingMinimapLocation(AttackableUnit PingingUnit, Vector3 TargetPosition, bool PlayAudio)
    {
        OnMapPingNotify(TargetPosition.ToVector2(), Pings.PING_DANGER, PingingUnit.NetId);
        return true;
    }

    /// <summary>
    /// Create a new quest and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="QuestId">Gives a unique identifier to refer back to this quest</param>
    /// <param name="String">The localized string to display.</param>
    /// <param name="Player">The player whose quest you want to activate</param>
    /// <param name="QuestType">Quest type; which quest tracker you want the quest to be added to</param>
    /// <param name="HandleRollOver">OPTIONAL. Should we handle the mousing rolling over and rolling out from this quest?</param>
    /// <param name="Tooltip">Optional: The tooltip to display on rollover of the quest.</param>
    protected bool ActivateQuest(out int QuestId, string String, AttackableUnit Player, QuestType QuestType, bool HandleRollOver, string Tooltip)
    {
        //todo : implement quest system with help of behaviourtree 
        QuestId = default;
        return false;
    }



    /// <summary>
    /// Removes quest from the HUD immediately
    /// </summary>
    /// <remarks>
    /// Used on quest ids returned by the ActivateQuest node; there is no ceremony involved in quest removal
    /// </remarks>
    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    protected bool RemoveQuest(int QuestId)
    {
        //todo : really necessary ? 
        return true;
    }


    /// <summary>
    /// Test to see if the quest is being clicked right now with the mouse down over it.
    /// </summary>
    /// <remarks>
    /// Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.
    /// </remarks>
    /// <param name="QuestId">Which Quest should we check?</param>
    protected bool TestQuestClicked(int QuestId)
    {
        //todo : implement quest system with help of behaviourtree 
        return false;
    }

    /// <summary>
    /// Create a new Tip and display it in the TipTracker
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip.</param>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipCategory">The localized string for the Tip Category.</param>
    protected bool ActivateTip(out int TipId, AttackableUnit Player, string TipName, string TipCategory)
    {
        Champion _champion = Player as Champion;
        TipParameters tips = new(_champion.ClientId, TipName, "", TipCategory);
        TipParameters.SendNormalTip(tips, _champion.ClientId, TipCommand.Activate);
        TipId = (int)tips.ID;
        return true;
    }

    /// <summary>
    /// Removes Tip from the Tip Tracker immediately
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip node; there is no ceremony involved in Tip removal
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip</param>
    protected bool RemoveTip(int TipId)
    {
        TipParameters foundTip = TipParameters.FindTipByID((uint)TipId);
        if (foundTip != null)
        {
            TipParameters.SendNormalTip(foundTip, foundTip.PlayerNetworkID, TipCommand.Remove);
        }
        return true;
    }

    /// <summary>
    /// Enables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to enable</param>
    protected bool EnableTipEvents(AttackableUnit Player)
    {
        //todo tipstracker 
        return false;
    }

    /// <summary>
    /// Disables mouse events in the Tip Tracker
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Tracker you want to disable</param>
    protected bool DisableTipEvents(AttackableUnit Player)
    {
        //todo tipstracker
        return false;
    }

    /// <summary>
    /// Tests to see if a Tip in the Tip Tracker or a Tip Dialogue has been clicked by the user
    /// </summary>
    /// <remarks>
    /// Used on Tip Ids returned by the ActivateTip and ActivateTipDialogue nodes. Use ReturnSuccessIf to control the output.
    /// This will return as if the Tip has NOT been clicked if the Tip Id is invalid.
    /// </remarks>
    /// <param name="TipId">Unique identfier used to refer to the Tip; returned by ActivateTip or ActivateTipDialogue</param>
    protected bool TestTipClicked(int TipId)
    {
        //todo tipstracker
        return false;
    }

    /// <summary>
    /// Create a new Tip Dialogue and display it in the HUD
    /// </summary>
    /// <remarks>
    /// This should only accept localized strings
    /// </remarks>
    /// <param name="TipId">Gives a unique identifier to refer back to this Tip Dialogue.</param>
    /// <param name="Player">The player whose tip you want to activate.</param>
    /// <param name="TipName">The localized string for the Tip Name.</param>
    /// <param name="TipBody">The localized string for the Tip Body.</param>
    /// <param name="TipImage">Optional. The path+filename of the image to display in the tap dialog.</param>
    protected bool ActivateTipDialogue(out int TipId, AttackableUnit Player, string TipName, string TipBody, string TipImage)
    {
        Champion _champion = Player as Champion;
        TipParameters tips = new(_champion.ClientId, TipName, TipBody, TipImage);
        TipParameters.SendNormalTip(tips, _champion.ClientId, TipCommand.Activate);
        TipId = (int)tips.ID;
        return true;
    }

    /// <summary>
    /// Enables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to enable</param>
    protected bool EnableTipDialogueEvents(AttackableUnit Player)
    {
        //todo tipstracker
        return false;
    }

    /// <summary>
    /// Disables mouse events in the Tip Dialogue
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="Player">The player whose Tip Dialogue you want to disable</param>
    protected bool DisableTipDialogueEvents(AttackableUnit Player)
    {
        //todo tipstracker
        return false;
    }

    /// <summary>
    /// Creates a vector from three   components
    /// </summary>
    /// <remarks>
    /// If you want to copy a Vector, use SetVarVector.
    /// </remarks>
    /// <param name="Vector">OutputVector</param>
    /// <param name="X">X component</param>
    /// <param name="Y">Y component</param>
    /// <param name="Z">Z component</param>
    protected bool MakeVector(out Vector3 Vector, float X, float Y, float Z)
    {
        Vector = new Vector3(X, Y, Z);
        return true;
    }

    /// <summary>
    /// Turn on or off a UI highlight for a specific UI Element
    /// </summary>
    /// <remarks>
    /// Set the enabled flag to control whether this node turns the element on or off
    /// </remarks>
    /// <param name="UIElement">UIElement; which element on the minimap do you want to highlight</param>
    /// <param name="Enabled">If true, turns on the UI Highlight, if false then turns off the UI Highlight</param>
    protected bool ToggleUIHighlight(UIElement UIElement, bool Enabled)
    {
        //TODO: Implement hightlight packet .
        return false;
    }

    /// <summary>
    /// Keeps track whether a player has opened his scoreboard.
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Output">Destination Reference; holds whether the scoreboard has been opened since the tree was enabled.</param>
    /// <param name="Unit">Handle of the attacking unit</param>
    protected bool RegisterScoreboardOpened(out bool Output, AttackableUnit Unit)
    {
        // need more understand about this 
        // Champion _champion = Player as Champion;
        // Game.PlayerManager.GetPeerInfo(_champion.ClientId).Name
        //HandleScoreboard.HandlePacket(_champion.ClientId, )
        Output = default;
        return false;
    }

    /// <summary>
    /// Keeps track of the number of minions (not neutrals) not on the attacker's team killed by an attacker
    /// </summary>
    /// <remarks>
    /// Ticking this registers with the event system; disabling the tree unregisters the callback and clears the count
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of units killed by the attacker</param>
    /// <param name="Unit">Handle of the attacking unit</param>
    protected bool RegisterMinionKillCounter(out int Output, AttackableUnit Unit)
    {

        Output = (Unit as Champion)?.ChampionStatistics.MinionsKilled ?? 0;
        return true;
    }

    /// <summary>
    /// Returns an int containing the number of kills the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of champions killed by the attacker</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected bool GetChampionKills(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Kills ?? 0;
        return true;
    }

    /// <summary>
    /// Returns an int containing the number of deaths the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of times the champion has been killed.</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected bool GetChampionDeaths(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Deaths ?? 0;
        return true;
    }
    /// <summary>Returns an int containing the number of kills in a spree the champion has. Always returns SUCCESS.  Will return 0 for non-hero units</summary>

    /// <param name="Unit">Handle of the champion to poll</param>
    /// <param name="Output">Destination Reference; holds the number of champions killed in a spree by the attacker</param>

    protected bool GetKillingSpreeNumber(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion).ChampionStatistics.CurrentKillingSpree;
        return true;
    }
    /// <summary>
    /// Returns an int containing the number of assists the champion has.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the number of assists the champion has earned.</param>
    /// <param name="Unit">Handle of the champion to poll</param>
    protected bool GetChampionAssists(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as Champion)?.ChampionStatistics.Assists ?? 0;
        return true;
    }

    /// <summary>
    /// Gives target champion a variable amount of gold.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// TODO: Convert this into Hero only
    /// </remarks>
    /// <param name="Unit">Handle of the champion to give gold to.</param>
    /// <param name="GoldAmount">Amount of gold to give the champion.</param>
    protected bool GiveChampionGold(AttackableUnit Unit, float GoldAmount)
    {
        //TODO: Test for Champion?
        Unit.GoldOwner.AddGold(GoldAmount);
        return true;
    }
    /// <summary>Gives target champion a variable amount of exp. Always returns SUCCESS.  TODO: Convert this into Hero only.</summary>

    /// <param name="Unit">Handle of the champion to give exp to.</param>
    /// <param name="ExpAmount">Amount of exp to give the champion.</param>

    protected bool GiveChampionExp(AttackableUnit Unit, float ExpAmount)
    {
        (Unit as Champion).Experience.AddEXP(ExpAmount);
        return true;
    }
    /// <summary>
    /// Orders a unit to stop its movement.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// How this block will interract with forced move orders (say from a skill) is currently untested.
    /// </remarks>
    /// <param name="Unit">Handle of the champion to order.</param>
    protected bool StopUnitMovement(AttackableUnit Unit)
    {
        //TODO: Implement.
        ObjAIBase _unit = Unit as ObjAIBase;
        var currentState = _unit.GetAIState();
        if (currentState is AIState.AI_STOP)
        {
            _unit.UpdateMoveOrder(OrderType.Hold);
            return true;
        }

        else
        {
            _unit.UpdateMoveOrder(OrderType.Hold);
            _unit.SetAIState(AIState.AI_STOP);
        }
        return true;
    }

    /// <summary>
    /// Test if a hero has a specific item.
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.
    /// This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    /// <param name="ItemID">Numerical ID of the item to look for.</param>
    /// /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has the item; if False, returns SUCCESS if unit does not.</param>
    protected bool TestChampionHasItem(AttackableUnit Unit, int ItemID, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (Unit == null)
            {
                return false; // Or any other handling for invalid input
            }

            ObjAIBase _unit = Unit as ObjAIBase;
            return _unit.ItemInventory.HasItem(ItemID);
        }
        else
        {
            if (Unit == null)
            {
                return true; // Or any other handling for invalid input
            }

            ObjAIBase _unit = Unit as ObjAIBase;
            return !_unit.ItemInventory.HasItem(ItemID);


        }

    }

    /// <summary>
    /// Pause or unpause the game.
    /// </summary>
    /// <remarks>
    /// Be careful using this!  It is not fully protected for use in a production environment!
    /// </remarks>
    /// <param name="Pause">Pause or unpause the game.</param>
    protected bool SetGamePauseState(bool Pause)
    {
        Game.StateHandler.Pause();
        return true;
    }

    /// <summary>
    /// Pan the camera from its current position to a target point.
    /// </summary>
    /// <remarks>
    /// Once the pan starts this node will return RUNNING until the pan completes.
    /// After the pan completes the node will always return SUCCESS. This node locks camera movement while panning, and returns camera movement state to what it was before the pan started.
    /// Be careful if you change camera movement locking state while panning, because it will not stick.
    /// </remarks>
    /// <param name="Unit">The unit whose camera is being manipulated.</param>
    /// <param name="TargetPosition">3D Point containing the target camera position.</param>
    /// <param name="Time">The amount of time the pan should take; this will scale the pan speed. </param>
    protected async Task<bool> PanCameraFromCurrentPositionToPoint(AttackableUnit Unit, Vector3 TargetPosition, float Time)
    {
        //todo:
        return false;
    }
    /// <summary>Pan the camera from its current position to a target point. Once the pan starts this node will return RUNNING until the pan completes.  After the pan completes the node will always return SUCCESS. This node locks camera movement while panning, and returns camera movement state to what it was before the pan started.  Be careful if you change camera movement locking state while panning, because it will not stick.</summary>

    /// <param name="TargetPosition">3D Point containing the target camera position.</param>
    /// <param name="Time">The amount of time the pan should take; this will scale the pan speed.</param>

    protected bool PanCameraFromCurrentPositionToPointAllHeroes(Vector3 TargetPosition, float Time = 1f)
    {
        //todo:
        return false;
    }



    /// <summary>
    /// Returns the number of item slots filled for a particular champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">The number of items in the target's inventory.</param>
    /// <param name="Unit">Handle of the unit whose inventory you want to check.</param>
    protected bool GetNumberOfInventorySlotsFilled(out int Output, AttackableUnit Unit)
    {
        Output = (Unit as ObjAIBase).ItemInventory.GetItems().Count();
        return true;
    }

    /// <summary>
    /// Returns the level of the target unit.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">The level of the target unit.</param>
    /// <param name="unit">Handle of the unit whose level you want to check.</param>
    /// <returns>Returns a boolean indicating whether the operation was successful (always returns true).</returns>
    protected bool GetUnitLevel(out int Output, AttackableUnit unit)
    {
        if (unit is Champion champion)
        {
            Output = champion.Experience.Level;
        }
        else if (unit is Minion minion)
        {
            Output = minion.MinionLevel;
        }
        else
        {
            Output = 0;
        }

        return true;
    }

    /// <summary>
    /// Returns the current XP total of the target champion.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// Returns 0 if unit is not champion.
    /// </remarks>
    /// <param name="Output">The current XP of the target unit.</param>
    /// <param name="Unit">Handle of the unit whose XP total you want to get.</param>
    protected bool GetUnitXP(out float Output, AttackableUnit Unit)
    {
        if (Unit is Champion champion)
        {
            Output = (int)champion.Experience.Exp;
        }

        else
        {
            Output = 0;
        }

        return true;
    }


    /// <summary>
    /// Returns the distance between the Unit and the Point
    /// </summary>
    /// <remarks>
    /// Distance is measured from the edge of the unit's bounding box
    /// </remarks>
    /// <param name="Output">Destination Reference; holds the distance from the unit to the point</param>
    /// <param name="Unit">Handle of the unit</param>
    /// <param name="Point">Point</param>
    protected bool DistanceBetweenObjectAndPoint(out float Output, AttackableUnit Unit, Vector3 Point)
    {
        // todo: Distance is measured from the edge of the unit's bounding box
        Output = Vector3.Distance(Unit.Position3D, Point);
        return true;
    }

    /// <summary>
    /// Returns a collection of units in the target area.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// Uses the reference unit for enemy/ally checks; must be present!
    /// </remarks>
    /// <param name="Output">Destination Reference; holds a collection of units discovered</param>
    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    protected bool GetUnitsInTargetArea(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit, Vector3 TargetLocation, float Radius, SpellDataFlags SpellFlags)
    {

        var units = FCS.GetUnitsInArea(
             Unit, TargetLocation, Radius, SpellFlags, "", false
         );
        Output = units;
        return true;
    }

    /// <summary>
    /// Returns a collection of units in the target area.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// Uses the reference unit for enemy/ally checks; must be present!
    /// </remarks>
    /// <param name="Output">Destination Reference; holds a collection of units discovered</param>
    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    protected bool GetUnitsInTargetAreaWithBuff(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit, Vector3 TargetLocation, float Radius, SpellDataFlags SpellFlags, string Buffname)
    {
        var units = FCS.GetUnitsInArea(
             Unit, TargetLocation, Radius, SpellFlags, Buffname, true
         );

        Output = units;
        return true;
    }

    /// <summary>
    /// Test to see if unit is alive
    /// </summary>
    /// <remarks>
    /// Unit is not alive if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is alive; if False, returns SUCCESS if unit is dead or does not exist</param>
    protected bool TestUnitCondition(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (Unit != null)
            {
                return !Unit.Stats.IsDead;

            }

            return false;
        }
        else
        {
            if (Unit != null)
            {
                return Unit.Stats.IsDead;

            }

            return true;
        }


    }

    /// <summary>
    /// Test to see if unit is invulnerable
    /// </summary>
    /// <remarks>
    /// Unit is not invulnerable if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is invulnerable; if False, returns SUCCESS if unit is not invulnerable or does not exist</param>

    protected bool TestUnitIsInvulnerable(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (Unit != null)
            {
                return (Unit.Status & StatusFlags.Invulnerable) != 0;
            }

            return false;
        }
        else
        {
            if (Unit != null)
            {
                return (Unit.Status & StatusFlags.Invulnerable) == 0;
            }

            return true;
        }

    }

    /// <summary>
    /// Test to see if unit is in brush
    /// </summary>
    /// <remarks>
    /// Unit is not in brush if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Unit">Unit to be tested</param>
    /// /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is in brush; if False, returns SUCCESS if unit is not in brush or does not exist</param>

    protected bool TestUnitInBrush(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (Unit != null)
            {
                return Game.Map.NavigationGrid.IsBush(Unit.Position);
            }

            return false;
        }
        else
        {
            if (Unit != null)
            {
                return !Game.Map.NavigationGrid.IsBush(Unit.Position);
            }

            return true;
        }

    }

    /// <summary>
    /// Test to see if unit has a specific buff
    /// </summary>
    /// <remarks>
    /// Unit does not have buff if it does not exist; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="TargetUnit">Unit to be tested</param>
    /// <param name="CasterUnit">OPTIONAL.
    /// Additional filter to check if buff was cast by a specific unit</param>
    /// <param name="BuffName">Name of buff to be tested</param>
    ///  <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has buff; if False, returns SUCCESS if unit does not have buff or does not exist</param>

    protected bool TestUnitHasBuff(AttackableUnit TargetUnit, AttackableUnit CasterUnit, string BuffName, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (TargetUnit != null)
            {
                if (!TargetUnit.HasBuff(BuffName))
                {
                    return false;
                }

                return TargetUnit.HasBuff(BuffName); ;
            }

            return false;
        }
        else
        {
            if (TargetUnit != null)
            {
                return !TargetUnit.HasBuff(BuffName); ;
            }

            return true;
        }


    }

    /// <summary>
    /// Test to see if a one unit has visibility of another unit
    /// </summary>
    /// <remarks>
    /// If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test
    /// </remarks>
    /// <param name="Viewer">Can this unit see the other?</param>
    /// <param name="TargetUnit">Is this unit visible to the viewer unit?</param>
    /// /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the target is visible to the viewer; if False, returns SUCCESS if unit is not visible to the viewer or does not exist.</param>

    protected bool TestUnitVisibility(AttackableUnit Viewer, AttackableUnit TargetUnit, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {
            if (TargetUnit != null && Viewer != null)
            {
                return VisionManager.UnitHasVisionOn(Viewer, TargetUnit);
            }

            return false;
        }
        else
        {
            if (TargetUnit == null || Viewer == null)
            {
                return true;
            }

            return true;
        }

    }

    /// <summary>
    /// Disabled or Enables all user input
    /// </summary>
    /// <remarks>
    /// Disables or Enables all user input, for all users.
    /// </remarks>
    /// <param name="Enabled">If False disables all input for all users. If True, enables it.</param>
    protected bool ToggleUserInput(bool Enabled)
    {
        //todo: 
        return false;
    }

    /// <summary>
    /// Disabled or Enables the texture for fog of war for all users.
    /// </summary>
    /// <remarks>
    /// This will not reveal any units in the fog of war; perception bubbles are necessary for that.
    /// </remarks>
    /// <param name="Enabled">If False disables the texture for all users for all users. If True, enables it.</param>
    protected bool ToggleFogOfWarTexture(bool Enabled)
    {
        //todo: 
        return false;
    }
    /// <summary>
    /// Plays a localized VO event
    /// </summary>
    /// <remarks>
    /// Event is a 2D one-shot audio event.
    /// A bad event name fails without complaint.
    /// This node always returns SUCCESS.
    /// </remarks>
    /// <param name="EventID">FMOD event ID</param>
    /// <param name="FolderName">Folder the FMOD event is in in the Dialogue folder of the VO sound bank</param>
    /// <param name="FireAndForget">If true, plays sound as fire-and-forget and the node will return SUCCESS immediately.
    /// If false, node will return RUNNING until the client tells the server that the VO is finished.</param>
    protected bool PlayVOAudioEvent(string EventID, string FolderName, bool FireAndForget)
    {
        PlayVOAudioEventtask(EventID, FolderName, FireAndForget);
        return true;
    }
    /// <summary>
    /// Plays a localized VO event
    /// </summary>
    /// <remarks>
    /// Event is a 2D one-shot audio event.
    /// A bad event name fails without complaint.
    /// This node always returns SUCCESS.
    /// </remarks>
    /// <param name="EventID">FMOD event ID</param>
    /// <param name="FolderName">Folder the FMOD event is in in the Dialogue folder of the VO sound bank</param>
    /// <param name="FireAndForget">If true, plays sound as fire-and-forget and the node will return SUCCESS immediately.
    /// If false, node will return RUNNING until the client tells the server that the VO is finished.</param>
    protected async Task<bool> PlayVOAudioEventtask(string EventID, string FolderName, bool FireAndForget)
    {
        //TODO: Implement.
        return true;
    }

    /// <summary>
    /// Returns the attack range for unit
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitAttackRange(out float Output, AttackableUnit Unit)
    {
        if (Unit != null)
        {
            Output = Unit.Stats.Range.Total;
            return true;
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Disables or Enables initial neutral minion spawn.
    /// </summary>
    /// <remarks>
    /// Once neutral minion spawning has begun, this node no longer has any effect.
    /// </remarks>
    /// <param name="Enabled">If True, enables neutral minion spawning; if False, delays neutral minion spawning.</param>
    protected bool SetNeutralSpawnEnabled(bool Enabled)
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Returns the amount of gold the unit has
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitGold(out float Output, AttackableUnit Unit)
    {
        if (Unit != null && Unit is Champion)
        {
            Champion _unit = Unit as Champion;
            Output = _unit.GoldOwner.Gold;
            return true;
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns unit unspent skill points
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitSkillPoints(out int Output, AttackableUnit Unit)
    {
        if (Unit != null && Unit is Champion)
        {
            Champion _unit = Unit as Champion;
            Output = _unit.Experience.SpellTrainingPoints.TrainingPoints;
            return true;
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Adds a unit Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    /// <param name="TargetUnit">Unit to attach the Perception Bubble to.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.
    /// Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.
    /// Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    protected bool AddUnitPerceptionBubble(out uint BubbleID, AttackableUnit TargetUnit, float Radius, float Duration, TeamId Team, bool RevealStealth, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly)
    {
        var bubble = new Region(
            Team, TargetUnit.Position,
            collisionUnit: TargetUnit, //HACK: to force the region to follow the unit
            visionTarget: RevealSpecificUnitOnly ?? SpecificUnitsClientOnly,
            visionRadius: Radius,
            revealStealth: RevealStealth,
            lifetime: Duration
        );

        BubbleID = bubble.NetId;
        return true;
    }

    /// <summary>
    /// Adds a position Perception Bubble
    /// </summary>
    /// <remarks>
    /// Returns a BubbleID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble</param>
    /// <param name="Position">Position of the Perception Bubble.</param>
    /// <param name="Radius">Radius of Perception Bubble. If set to 0, the bubble visibility radius matches the visibility radius of the target unit.</param>
    /// <param name="Duration">Duration of Perception Bubble in seconds.
    /// Bubbles can be removed earlier by using the RemovePerceptionBubble node.</param>
    /// <param name="Team">Team ID that has visibility of this bubble.</param>
    /// <param name="RevealStealth">If this is true then the bubble will reveal stealth for anything inside of that bubble.</param>
    /// <param name="SpecificUnitsClientOnly">OPTIONAL. If specified a client specific message will be sent only to this client about this bubble.
    /// Only that client will have that visiblity.</param>
    /// <param name="RevealSpecificUnitOnly">OPTIONAL. If set then only a units that have the RevealSpecificUnit state on are seeable by this bubble.</param>
    protected bool AddPositionPerceptionBubble(out uint BubbleID, Vector3 Position, float Radius, float Duration, TeamId Team, bool RevealStealth, AttackableUnit SpecificUnitsClientOnly, AttackableUnit RevealSpecificUnitOnly)
    {

        var bubble = new Region(
            Team, Position.ToVector2(),
            visionTarget: SpecificUnitsClientOnly,
            visionRadius: Radius,
            revealStealth: RevealStealth,
            lifetime: Duration
        );

        BubbleID = bubble.NetId;
        return true;
    }

    /// <summary>
    /// Removes Perception Bubble
    /// </summary>
    /// <remarks>
    /// Used on Bubble IDs returned by the AddUnitPerceptionBubble and AddPositionPerceptionBubble
    /// </remarks>
    /// <param name="BubbleID">Unique identfier used to refer to the Perception Bubble; returned by AddPerceptionBubble nodes</param>
    protected bool RemovePerceptionBubble(uint BubbleID)
    {
        Region foundRegion = Region.GetRegionByNetId(BubbleID);
        foundRegion.SetToRemove();
        return true;
    }

    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    /// <param name="BindObject">Unit to attach the particle effect to.</param>
    /// <param name="BoneName">OPTIONAL. Name of the bone to attach the particle effect to.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.
    /// Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.
    /// Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.
    /// If used, only this team will see the particle.
    /// Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.
    /// The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.
    /// If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.
    /// Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.
    /// Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    protected bool CreateUnitParticle(out uint EffectID, AttackableUnit BindObject, string BoneName, string EffectName, AttackableUnit TargetObject, string TargetBoneName, Vector3 TargetPosition, Vector3 OrientTowards, AttackableUnit SpecificUnitOnly, TeamId SpecificTeamOnly, float FOWVisibilityRadius, TeamId FOWTeam, bool SendIfOnScreenOrDiscard)
    {
        /*var p1 = new Particle(
        EffectName ?? "",
        BindObject,
        OrientTowards.ToVector2(),
        BindObject,
        BoneName ?? "",
        default,
        TargetObject,
        TargetBoneName ?? "",
        25000,
        particleEnemyName: "",
        followGroundTilt: SendIfOnScreenOrDiscard,
        flags: FXFlags.BindDirection,
        forceTeam: FOWTeam);
       
        EffectID = p1.NetId;*/

        // AddParticleBind(null, EffectName, BindObject, lifetime: 25000, bindBone: BoneName, forceTeam: SpecificTeamOnly);
        AddParticleLink(null, EffectName, BindObject, TargetObject, 25000.0f, 1, default, BoneName, TargetBoneName, "", forceTeam: SpecificTeamOnly);
        //  AddParticleLink(null, "odin_crystal_beam_green", Stairs[0], OrderCrystal, 25000.0f, 1, default, $"Crystal_r_{boneNum}_aim", "center_crystal", "odin_crystal_beam_red", forceTeam: TeamId.TEAM_ORDER);

        EffectID = default;
        return true;
    }

    /// <summary>
    /// Adds a unit particle effect
    /// </summary>
    /// <remarks>
    /// Returns an EffectID which you can use to remove the perception bubble
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; used to remove particle.</param>
    /// <param name="Position">Position of the particle effect.</param>
    /// <param name="EffectName">File name of the particle effect file to use.</param>
    /// <param name="TargetObject">OPTIONAL. Unit to attach the far end of a beam particle to.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="TargetBoneName">OPTIONAL. Name of the bone to attach the far end of a beam particle to.
    /// Used in conjunction with TargetObject.</param>
    /// <param name="TargetPosition">OPTIONAL. A fixed position for the far end of a beam particle.
    /// Use either TargetObject or TargetPosition; if you have both, TargetObject wins.</param>
    /// <param name="OrientTowards">OPTIONAL. Particle effect will orient to face this point.</param>
    /// <param name="SpecificUnitOnly">OPTIONAL. If used, only sends this particle to this unit.
    /// Otherwise, all units will see the particle.</param>
    /// <param name="SpecificTeamOnly">OPTIONAL.
    /// If used, only this team will see the particle.
    /// Otherwise, all teams will see the particle.</param>
    /// <param name="FOWVisibilityRadius">Used with FOWTeam to determine particle visibility in the FoW.
    /// The particle will be visible if a unit has visibility into the area defined by this radius and the center of the particle.</param>
    /// <param name="FOWTeam">OPTIONAL.
    /// If the viewing unit is on the same team as set by this variable, that unit will see this particle even if it's in the Fog of War.
    /// Only used if FOWVisibilityRadius is non-zero.</param>
    /// <param name="SendIfOnScreenOrDiscard">If true, will only try to send the particle if a unit can see it when the particle spawns.
    /// Use for one-shot particles; saves a lot of bandwidth, so use as often as possible.</param>
    protected bool CreatePositionParticle(out uint EffectID, Vector3 Position, string EffectName, AttackableUnit TargetObject, string TargetBoneName, Vector3 TargetPosition, Vector3 OrientTowards, AttackableUnit SpecificUnitOnly, TeamId SpecificTeamOnly, float FOWVisibilityRadius, TeamId FOWTeam, bool SendIfOnScreenOrDiscard)
    {
        //todo : check if correct
        var p1 = new Particle(
           EffectName ?? "",
           TargetObject,
           Position.ToVector2(),
           null,
           "",
           TargetPosition.ToVector2(),
           TargetObject,
           TargetBoneName ?? "",
           25000,
           particleEnemyName: "",
           followGroundTilt: false,
           flags: FXFlags.BindDirection,
           forceTeam: FOWTeam
       );

        EffectID = p1.NetId;
        return false;
    }

    /// <summary>
    /// Removes Particle
    /// </summary>
    /// <remarks>
    /// Used on Effect IDs returned by the CreateUnitParticle and CreatePositionParticle
    /// </remarks>
    /// <param name="EffectID">Unique identfier used to refer to the particle effect; returned by CreateParticle nodes</param>
    protected bool RemoveParticle(uint EffectID)
    {

        var p1 = Game.ObjectManager.GetObjectById(EffectID);
        if (p1 != null)
        {
            (p1 as Particle).SetToRemove();
            return true;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Returns unit Team ID
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if unit is invalid
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitTeam(out TeamId Output, AttackableUnit Unit)
    {
        if (Unit != null)
        {
            Output = Unit.Team;
            return true;
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Sets unit state DisableAmbientGold.
    /// If disabled, unit does not get ambient gold gain (but still gets gold/5 from runes).
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// </remarks>
    /// <param name="Unit">Sets state of this unit.</param>
    /// <param name="Disabled">If true, ambient gold gain is disabled.</param>
    protected bool SetStateDisableAmbientGold(AttackableUnit Unit, bool Disabled)
    {
        //possibly used in mapscript bt 
        return false;
    }

    /// <summary>
    /// Sets unit level cap.
    /// Level cap 0 means no cap.
    /// Otherwise unit will earn experience up to one XP less than the level cap.
    /// </summary>
    /// <remarks>
    /// Returns FAILURE if the unit is not valid.
    /// If unit is already higher than the cap, it will earn 0 XP.
    /// </remarks>
    /// <param name="Unit">Sets level cap of this unit.</param>
    /// <param name="LevelCap">If 0, no level cap; otherwise unit cannot get higher than this level.</param>
    protected bool SetUnitLevelCap(AttackableUnit Unit, int LevelCap)
    {
        //possibly used in mapscript bt 
        return false;
    }

    /// <summary>
    /// Locks all player cameras to their champions.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Lock">If true, locks all player cameras to their champions.
    /// If false, unlocks all player cameras from their champions.</param>
    protected bool LockAllPlayerCameras(bool Lock = true)
    {
        //TODO: Implement.
        return false;
    }

    /// <summary>
    /// Test to see if Player has camera locking enabled (camera locked to hero).
    /// </summary>
    /// <remarks>
    /// Use ReturnSuccessIf to control the output.
    /// This will return FAILURE if any parameters are incorrect.
    /// </remarks>
    /// <param name="Player">Player to test.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the the player has camera locking enabled; if False, returns SUCCESS if player does not have camera locking enabled, or if the player does not exist.</param>

    protected bool TestPlayerCameraLocked(AttackableUnit Player, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {

            return true;
        }//TODO: Implement.
        else
        {
            return false;
        }

    }

    /// <summary>
    /// A Procedure call
    /// </summary>
    /// <remarks>
    /// Procedure
    /// </remarks>
    /// <param name="Output1">Destination reference contains float value.</param>
    /// <param name="Output2">Destination reference contains UnitType value.</param>
    /// <param name="PocedureName"> can not be empty </param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ChatMessage">Chat string</param>
    protected bool Procedure2To2(out string Output1, out UnitType Output2, string PocedureName, AttackableUnit Unit, string ChatMessage)
    {
        //???
        Output1 = default;
        Output2 = default;
        return false;
    }

    /// <summary>
    /// Test if game started
    /// </summary>
    /// <remarks>
    /// Tests if game started. True if game started. False if not
    /// </remarks>
    protected bool TestGameStarted(bool ReturnSuccessIf = true)
    {
        if (Game.StateHandler.State == GameState.SPAWN || Game.StateHandler.State == GameState.GAMELOOP)
        {
            if (ReturnSuccessIf)
                return true;
            else { return false; }
        }
        else
        {
            if (!ReturnSuccessIf)
                return true;
            else { return false; }
        }
    }

    /// <summary>
    /// Tests if the specified unit is under attack
    /// </summary>
    /// <remarks>
    /// Tests if the specified unit is under attack. May gather enemies of given unit to figure out if under attack
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is under attack; if False, returns SUCCESS if unit is not under attack</param>

    protected bool TestUnitUnderAttack(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {

        return Unit.IsInDistress();
    }

    /// <summary>
    /// Returns the type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit type
    /// </remarks>
    /// <param name="Output">Destination reference contains the type of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitType(out UnitType Output, AttackableUnit Unit)
    {
        if (Unit is Minion or LaneMinion)
        {
            Output = UnitType.MINION_UNIT;
            return true;
        }
        else if (Unit is Champion)
        {
            Output = UnitType.HERO_UNIT;
            return true;
        }
        else if (Unit is BaseTurret or LaneTurret)
        {
            Output = UnitType.TURRET_UNIT;
            return true;
        }
        //todo: create CapturePoint
        /*  else if (Unit is OnCaptureAltar)
          {
              Output = UnitType.CAPTURE_POINT_UNIT ;
              return true;
          }
        */
        else if (Unit is Inhibitor)
        {
            Output = UnitType.INHIBITOR_UNIT;
            return true;
        }
        else if (Unit is Nexus)
        {
            Output = UnitType.HQ_UNIT;
            return true;
        }
        else
        {
            Output = UnitType.UNKNOWN_UNIT;
            return true;
        }


    }



    /// <summary>
    /// Returns the creature type of a specific unit
    /// </summary>
    /// <remarks>
    /// Unit creature type
    /// </remarks>
    /// <param name="Output">Destination reference contains the creature type of the unit.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitCreatureType(out CreatureType Output, AttackableUnit Unit)
    {
        //todo: what is that ? 
        Output = CreatureType.BRAWLER;
        return true;
    }

    /// <summary>
    /// Tests if the specified unit can use the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can cast spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can use spell</param>
    protected bool TestCanCastSpell(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex, bool successif = true)
    {
        if (Unit == null)
        {
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                /* if (_unit.Spells[SlotIndex].Enabled == true && _unit.Spells[SlotIndex].State == SpellState.READY)
                 {
                     //todo: add more test
                 if (Game.Config.EnableLogBehaviourTree)
                    {
                     Console.WriteLine("TestCanCastSpell returned true");
                }
                     return true;
                 }*/
                if (_unit.Spells[SlotIndex].Level > 0)
                {
                    //todo: add more test
                    return true;
                }
            }
            return false;
        }
    }




    /// <summary>
    /// Set ignore visibility for a specific spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="IgnoreVisibility">Ignore visibility ?</param>
    protected bool SetUnitSpellIgnoreVisibity(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex, bool IgnoreVisibility)
    {
        //todo: 
        if (Unit == null)
        {
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                if (_unit.Spells[SlotIndex].Enabled && _unit.Spells[SlotIndex].State == SpellState.READY)
                {

                    _unit.SetSpellToCast(_unit.Spells[SlotIndex]);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Set specified Spell target position
    /// </summary>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool SetUnitAISpellTargetLocation(Vector3 TargetLocation, int SlotIndex)
    {

        Champion Unit = this.Owner as Champion;
        if (Unit == null)
        {
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit;

                if (_unit.Spells[SlotIndex].Enabled && _unit.Spells[SlotIndex].State == SpellState.READY)
                {

                    _unit.Spells[SlotIndex].SetCastArgsPosition(TargetLocation);
                    _unit.Spells[SlotIndex].SetCastArgsEndPos(TargetLocation);

                    _unit.SetSpellToCast(_unit.Spells[SlotIndex], _unit.Spells[SlotIndex]._CastArgs);

                    _unit.SetSpellPos(TargetLocation);

                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Set specified Spell target
    /// </summary>
    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool SetUnitAISpellTarget(AttackableUnit TargetUnit, int SlotIndex)
    {
        if (this.Owner == null)
        {
            return false;
        }
        else
        {
            if (this.Owner is Champion)
            {
                var _unit = this.Owner as Champion;

                // if (_unit.Spells[SlotIndex].Enabled && _unit.Spells[SlotIndex].State == SpellState.READY)
                if (_unit.Spells[SlotIndex].Level > 0)

                {
                    _unit.Spells[SlotIndex].SetCastArgsTarget(TargetUnit);
                    _unit.Spells[SlotIndex].SetCastArgsEndPos(TargetUnit.Position3D);
                    _unit.SetSpellToCast(_unit.Spells[SlotIndex], _unit.Spells[SlotIndex]._CastArgs);
                    FLS.FaceDirection(_unit, TargetUnit.Position3D);

                    _unit.SetTargetUnit(TargetUnit);

                    return true;



                }
            }
        }
        return false;
    }

    /// <summary>
    /// Clears specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool ClearUnitAISpellTarget(int SlotIndex)
    {
        if (this.Owner == null)
        {
            return false;
        }
        else
        {
            if (this.Owner is Champion)
            {
                var _unit = this.Owner as Champion;

                if (_unit.Spells[SlotIndex].Enabled && _unit.Spells[SlotIndex].State == SpellState.READY)
                {
                    _unit.Spells[SlotIndex].SetCastArgsTarget(null);
                    _unit.SetSpellToCast(_unit.Spells[SlotIndex], _unit.Spells[SlotIndex]._CastArgs);

                    _unit.SetTargetUnit(null);

                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Test validity of specified Spell target
    /// </summary>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool TestUnitAISpellTargetValid(int SlotIndex)
    {
        if (this.Owner == null)
        {
            return false;
        }
        else
        {
            if (this.Owner is Champion)
            {
                var _unit = this.Owner as Champion;

                if (_unit.Spells[SlotIndex].Enabled && _unit.Spells[SlotIndex].State == SpellState.READY)
                {
                    //todo: understand 

                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Output">Destination reference contains cooldown</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool GetSpellSlotCooldown(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        if (Unit == null)
        {
            Output = 0f;
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;
                Output = _unit.Spells[SlotIndex].CurrentCooldown;
                return true;
            }
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets the cooldown value for the spell in a given slot
    /// </summary>
    /// <remarks>
    /// Cooldown for spell in given slot
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Cooldown">Slot cooldown</param>
    protected bool SetSpellSlotCooldown(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex, float Cooldown)
    {
        if (Unit == null)
        {

            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                _unit.Spells[SlotIndex].SetCooldown(Cooldown);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns the PAR type for specified unit
    /// </summary>
    /// <remarks>
    /// PAR Type
    /// </remarks>
    /// <param name="Output">Destination reference.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitPARType(out PrimaryAbilityResourceType Output, AttackableUnit Unit)
    {

        if (Unit == null)
        {
            Output = default;
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                Output = _unit.Stats.PrimaryAbilityResourceType;
                return true;
            }
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the cost for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cost
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool GetUnitSpellCost(out float Output, AttackableUnit Unit, SpellbookType Spellbook = SpellbookType.SPELLBOOK_UNKNOWN, int SlotIndex = 0)
    {
        if (Unit == null)
        {
            Output = 0f;
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;
                Output = _unit.Spells[SlotIndex].ManaCost;
                return true;
            }
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the cast range for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell cast range
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool GetUnitSpellCastRange(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        if (Unit == null)
        {
            Output = 0f;
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;
                //hack for range 
                if (_unit.Spells[SlotIndex].CastRange < 100.0f)
                {
                    Output = 100.0f;
                }
                else
                {
                    Output = _unit.Spells[SlotIndex].CastRange;
                }

                return true;
            }
            else
            {
                var _unit = Owner;
                Output = _unit.Spells[SlotIndex].CastRange;
                return true;
            }
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the level for spell specified slot
    /// </summary>
    /// <remarks>
    /// Spell level
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool GetUnitSpellLevel(out int Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        if (Unit == null)
        {
            Output = 0;
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                Output = _unit.Spells[SlotIndex].Level;
                return true;
            }
        }
        Output = default;
        return false;
    }

    /// <summary>
    /// Levels up a specified spell
    /// </summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool LevelUpUnitSpell(AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        if (Unit == null)
        {
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;

                _unit.Spells[SlotIndex].LevelUp(1);
                _unit.Experience.SpellTrainingPoints.SpendTrainingPoint();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Tests if the specified unit can level up the specified spell
    /// </summary>
    /// <remarks>
    /// Uses specified spellbook and specified spell to figure out if unit can level up spell
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool TestUnitCanLevelUpSpell(AttackableUnit Unit, int SlotIndex, bool succesif = true)
    {
        if (Unit is not Champion champ)
            return false;

        int currentSpellLevel = champ.Spells[SlotIndex].Level;
        int currentLevel = champ.Experience.Level;
        int availablePoints = champ.Experience.SpellTrainingPoints.TrainingPoints;

        // No points to spend or spell already at max level
        if (availablePoints <= 0 || currentSpellLevel >= 5)
            return false;

        // Si c'est l'ultime (slot 3)
        if (SlotIndex == 3)
        {
            // Can level up to 6 → ult level 1
            // Can level up to 11 → ult level 2
            // Can level up to 16 → ult level 3
            if ((currentLevel >= 6 && currentSpellLevel < 1) ||
                (currentLevel >= 11 && currentSpellLevel < 2) ||
                (currentLevel >= 16 && currentSpellLevel < 3))
            {
                return true;
            }

            return false;
        }

        // Pour les sorts de base (slots 0,1,2)

        // Évite qu’un bot monte un sort de niveau 1 à 3 directement (au level 3)
        if (currentLevel == 3 && currentSpellLevel == 2)
            return false;

        // Évite qu’un bot monte un sort de niveau 3 à 5 directement (au level 5)
        if (currentLevel == 5 && currentSpellLevel == 3)
            return false;

        // Empêche d’augmenter deux fois d'affilée le même sort s’il y a des alternatives disponibles
        if (succesif)
        {
            // Si **au moins un** des autres sorts a un niveau STRICTEMENT inférieur,
            // alors on **ne doit pas** monter le sort courant (on préfère équilibrer).
            for (int i = 0; i <= 2; i++)
            {
                if (i == SlotIndex) continue;

                if (champ.Spells[i].Level < currentSpellLevel)
                {
                    return false;
                }
            }
        }

        // Si aucune règle ne bloque, on peut level up ce sort
        return true;
    }

    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    /// <param name="Output">Destination reference; holds a AI object handle</param>
    protected bool GetUnitAISelf(out AttackableUnit Output)
    {

        Output = Owner;
        return true;
    }

    /// <summary>
    /// Gets a handle to the the unit running the behavior tree in OutputRef
    /// </summary>
    /// <remarks>
    /// Gets a handle to the the unit running the behavior tree
    /// </remarks>
    /// <param name="Output">Destination reference; holds a AI object handle</param>
    protected bool SetUnitAISelf(AttackableUnit input)
    {

        this.Owner = (ObjAIBase)input;
        return true;
    }


    /// <summary>
    /// Unit run logic for first time
    /// </summary>
    protected bool TestUnitAIFirstTime(bool successif = true)
    {
        return timesExecuted == 1;
    }

    /// <summary>
    /// Sets unit to assist
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target unit</param>
    protected bool SetUnitAIAssistTarget(AttackableUnit TargetUnit)
    {
        //todo:
        return true;
    }

    /// <summary>
    /// Sets unit to target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Source Reference</param>
    protected bool SetUnitAIAttackTarget(AttackableUnit TargetUnit)
    {

        var _unit = Owner;
        _unit.UpdateMoveOrder(OrderType.AttackTo);
        _unit.GetTargetPriority(TargetUnit);
        _unit.TargetUnit = TargetUnit;
        _unit.SetTargetUnit(TargetUnit);
        return true;
    }

    /// <summary>
    /// Gets unit being assisted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    protected bool GetUnitAIAssistTarget(out AttackableUnit Output)
    {
        //todo : 
        Output = default;
        return false;
    }

    /// <summary>
    /// Gets unit being targeted
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="Output">Destination reference</param>
    protected bool GetUnitAIAttackTarget(out AttackableUnit Output)
    {
        var _unit = Owner;
        Output = _unit.TargetUnit;

        return true;
    }

    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    protected bool IssueMoveOrder()
    {
        //for test 

        var _unit = Owner;
        switch ((this.Owner as Champion).player_id)
        {
            case -1:
                if (Game.Map.Id == 1 || Game.Map.Id == 11)
                {
                    this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_C].Select(x => x.Position)));
                    this.Owner.SetupLane(Lane.LANE_C);
                    break;
                }
                else
                {
                    this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_R].Select(x => x.Position)));
                    this.Owner.SetupLane(Lane.LANE_R);
                    break;
                }
            case -2:
                this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_L].Select(x => x.Position)));
                this.Owner.SetupLane(Lane.LANE_L);
                break;
            case -3:
                this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_R].Select(x => x.Position)));
                this.Owner.SetupLane(Lane.LANE_R);
                break;
            case -4:
                this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_L].Select(x => x.Position)));
                this.Owner.SetupLane(Lane.LANE_L);
                break;
            case -5:
                this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_R].Select(x => x.Position)));
                this.Owner.SetupLane(Lane.LANE_R);
                break;
        }


        if (this.Owner.lanetofollow == null)
        {
            return false;
        }


        /*  if(Game.Time.GameTime /1000.0f >= 20000.0f)
          {
              if (Game.Map.Id == 1 || Game.Map.Id == 11)
              {
                  this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_C].Select(x => x.Position)));
                  this.Owner.SetupLane(Lane.LANE_C);

              }
              else
              {
                  this.Owner.Setupwaypointlane(new(Game.Map.NavigationPoints[Lane.LANE_R].Select(x => x.Position)));
                  this.Owner.SetupLane(Lane.LANE_R);

              }
          } */
        this.Owner.UpdateMoveOrder(OrderType.MoveTo);
        this.Owner.IssueOrDelayOrder(OrderType.AttackTo, null, this.Owner.Closestwaypointofthelist(), fromAiScript: true);
        // this.Owner.IssueMovementOrder(OrderType.MoveTo, null, default, this.Owner.lanetofollow);

        // _unit.Move();
        return true;
    }

    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="TargetUnit">Target Unit.</param>
    protected bool IssueMoveToUnitOrder(AttackableUnit TargetUnit)
    {
        var _unit = Owner;
        //   if (_unit.Status != StatusFlags.Taunted)
        //   {
        _unit.UpdateMoveOrder(OrderType.MoveTo);

        //var augmentposfordoesntcollidehack = TargetUnit.Position + 10.0f;
        this.Owner.IssueOrDelayOrder(OrderType.MoveTo, null, TargetUnit.Position, fromAiScript: true);
        //    }
        //    else
        //     {
        //        _unit.UpdateMoveOrder(OrderType.Stop);

        //var augmentposfordoesntcollidehack = TargetUnit.Position + 10.0f;
        //        this.Owner.IssueOrDelayOrder(OrderType.Stop, null, _unit.Position, fromAiScript: true);
        //    }
        return true;
    }


    /// <summary>
    /// Issue Move Order ( for minion ) 
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    protected bool IssueMoveToPositionOrderminion()
    {

        var _unit = this.Owner as Minion;
        if ((_unit.Status & StatusFlags.Taunted) == 0)
        {
            _unit.SetAIState(AIState.AI_MOVE, true);
            //   _unit.UpdateMoveOrder(OrderType.MoveTo);

            _unit.IssueOrDelayOrder(OrderType.AttackTo, null, this.Owner.Closestwaypointofthelist(), fromAiScript: true);
        }
        else
        {
            _unit.UpdateMoveOrder(OrderType.Stop);

            //var augmentposfordoesntcollidehack = TargetUnit.Position + 10.0f;
            this.Owner.IssueOrDelayOrder(OrderType.Stop, null, _unit.Position, fromAiScript: true);
        }




        return true;
    }



    /// <summary>
    /// Issue Move Order
    /// </summary>
    /// <remarks>
    /// Move
    /// </remarks>
    /// <param name="Location">Position to move to</param>
    protected bool IssueMoveToPositionOrder(Vector3 Location)
    {
        ObjAIBase _unit = this.Owner;
        AISquadClass themission = default;
        GetAISquadFromEntity(out themission, _unit);
        if (themission == null || themission.AssignedMission == null)
        {
            _unit.IssueOrDelayOrder(OrderType.MoveTo, null, Location.ToVector2(), fromAiScript: true);
            return true;
        }
        if (themission.AssignedMission.Topic != AIMissionTopicType.PUSH)
            _unit.IssueOrDelayOrder(OrderType.MoveTo, null, Location.ToVector2(), fromAiScript: true);
        else
        {
            var unit = GetClosestUnitInRange(Location.ToVector2(), 100.0f, true, _unit.Team);
            _unit.IssueOrDelayOrder(OrderType.AttackTo, unit, Location.ToVector2(), fromAiScript: true);
        }
        return true;
        /*   if(Location != Vector3.Zero)
           {

               var _unit = this.Owner as ObjAIBase;
               _unit.UpdateMoveOrder(OrderType.MoveTo);
               _unit.Waypoints.Clear();


               var pathtofollow = Game.Map.NavigationGrid.GetPath(this.Owner.Position, Location.ToVector2(), this.Owner);
               if (pathtofollow != null)
               {
                   _unit.Waypoints.AddRange(pathtofollow);

               }
               else
               {
                   _unit.Waypoints.Add(_unit.Position);
               }
               //_unit.Waypoints.Add(Location.ToVector2());

                this.Owner.IssueOrDelayOrder(OrderType.MoveTo, null, Location.ToVector2(), fromAiScript: true);
               return true;
           }
           else
           {

               var _unit = this.Owner as ObjAIBase;
               if (_unit.lanetofollow != null)
               {
                   _unit.UpdateMoveOrder(OrderType.MoveTo);
                   this.Owner.IssueOrDelayOrder(OrderType.AttackTo, null, this.Owner.Closestwaypointofthelist(), fromAiScript: true);
                   //_unit.Waypoints.Add(Location.ToVector2());

                   //  this.Owner.IssueOrDelayOrder(OrderType.MoveTo, null, Location.ToVector2(), fromAiScript: true);

               }
               else
               {
                   _unit.Waypoints.Add(_unit.Position);
               }
               return true;
           }*/

    }

    /// <summary>
    /// Issue Chase Order
    /// </summary>
    /// <remarks>
    /// Chase
    /// </remarks>
    protected bool IssueChaseOrder()
    {
        var _unit = Owner;
        _unit.IssueOrDelayOrder(OrderType.AttackTo, _unit.TargetUnit, _unit.TargetUnit.Position, fromAiScript: true);
        return true;
    }

    /// <summary>
    /// Issue Attack Order
    /// </summary>
    /// <remarks>
    /// Attack
    /// </remarks>
    protected bool IssueAttackOrder()
    {
        var _unit = Owner;
        _unit.IssueOrDelayOrder(OrderType.AttackTo, _unit.TargetUnit, _unit.TargetUnit.Position, fromAiScript: true);
        _unit.SetTargetUnit(_unit.TargetUnit, true);
        return true;
    }

    /// <summary>
    /// Issue Wander order
    /// </summary>
    /// <remarks>
    /// Wander
    /// </remarks>
    protected bool IssueWanderOrder()
    {
        var _unit = Owner;
        //todo check if we implement TaskWander lua ?? for moment i copy paste value from the task 

        var positionsafe = FLS.GetRandomPointInAreaPosition(_unit.Position3D, 250, _unit.PathfindingRadius);
        this.Owner.IssueOrDelayOrder(OrderType.MoveTo, null, positionsafe.ToVector2(), fromAiScript: true);


        return true;
    }
    protected bool IssueWanderOrder(Vector3 safeposition, float radius)
    {
        var _unit = Owner;

        var positionsafe = FLS.GetRandomPointInAreaPosition(safeposition, radius, _unit.PathfindingRadius);
        this.Owner.IssueOrDelayOrder(OrderType.MoveTo, null, positionsafe.ToVector2(), fromAiScript: true);

        return true;
    }

    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="EmoteIndex">Emote ID</param>
    protected bool IssueAIEmoteOrder(uint EmoteIndex)
    {
        //not used 


        var _unit = Owner;
        _unit.UpdateMoveOrder(OrderType.Stop);

        return true;
    }

    /// <summary>
    /// Issue Emote Order
    /// </summary>
    /// <remarks>
    /// Emote
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="EmoteIndex">Emote ID</param>
    protected bool IssueGloabalEmoteOrder(AttackableUnit Unit, uint EmoteIndex)
    {
        //todo : 
        return true;
    }

    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    /// protected   bool IssueAIChatOrder(String ChatMessage = Bot Chat, String ChatRcvr = / all)
    protected bool IssueAIChatOrder(string ChatMessage, string ChatRcvr)
    {
        //todo : 
        return true;
    }

    /// <summary>
    /// Issue Chat Order
    /// </summary>
    /// <remarks>
    /// AI caht
    /// </remarks>
    /// <param name="ChatMessage">Chat message</param>
    /// <param name="ChatRcvr">Chat receiver</param>
    protected bool IssueImmediateChatOrder(string ChatMessage, string ChatRcvr)
    {
        //todo : 
        return true;
    }

    /// <summary>
    /// Issue disable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    protected bool IssueAIDisableTaskOrder()
    {
        var _unit = Owner;
        _unit.UpdateMoveOrder(OrderType.Stop);
        return true;

    }

    /// <summary>
    /// Issue enable task
    /// </summary>
    /// <remarks>
    /// AI task
    /// </remarks>
    protected bool IssueAIEnableTaskOrder()
    {


        var _unit = Owner;
        _unit.UpdateMoveOrder(OrderType.MoveTo);
        return true;
    }

    /// <summary>
    /// Clear AI Attack target
    /// </summary>
    protected bool ClearUnitAIAttackTarget()
    {
        var _unit = Owner;
        _unit.UpdateMoveOrder(OrderType.Hold);
        _unit.TargetUnit = null;
        return true;
    }
    /// <summary>
    /// Clear AI Attack target
    /// </summary>
    protected bool ClearUnitAIAttackTarget2()
    {
        var _unit = Owner;
        // _unit.UpdateMoveOrder(OrderType.MoveTo);
        _unit.GetTargetPriority(null);
        _unit.TargetUnit = null;
        return true;
    }
    /// <summary>
    /// Clear AI assist target
    /// </summary>
    protected bool ClearUnitAIAssistTarget()
    {
        //todo
        return false;
    }

    /// <summary>
    /// Teleport To base
    /// </summary>
    /// <remarks>
    /// Used for Teleporting home
    /// </remarks>
    protected bool IssueTeleportToBaseOrder()
    {
        /*this.Owner.Spells.BluePill.SetCastArgsTarget(this.Owner);
        this.Owner.Spells.BluePill.SetCastArgsPosition(this.Owner.Position3D);
        this.Owner.Spells.BluePill.SetCastArgsEndPos(this.Owner.Position3D);
        this.Owner.SetSpellToCast(Owner.Spells.BluePill, this.Owner.Spells.BluePill._CastArgs);*/

        // if (this.Owner.SafePointZone != Vector3.One)
        //  {

        //  this.Owner.SetChannelSpell(this.Owner.Spells.BluePill);

        (this.Owner as Champion).Stats.SetSpellEnabled(10, true);
        SpellCast((this.Owner as Champion).Spells[10], Owner.Position, Owner.Position, false, isForceCastingOrChanneling: true);
        // }


        return true;
    }

    /// <summary>
    /// Returns the number of discrete attackers.
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference; contains collection of attacking units.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitAIAttackers(out IEnumerable<AttackableUnit> Output, AttackableUnit Unit)
    {
        //TODO: Implement.
        Output = new AttackableUnit[0];
        return true;
    }

    /// <summary>
    /// Unit can buy next recommended item
    /// </summary>
    protected bool TestUnitAICanBuyRecommendedItem()
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Buy next recommended item
    /// </summary>
    protected bool UnitAIBuyRecommendedItem()
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Unit can buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    protected bool TestUnitAICanBuyItem(int ItemID, bool boolean = true)
    {
        if (GetItemData(ItemID) != null) //hack need check all item of behaviourtree 
        {
            if (this.Owner.GoldOwner.Gold < GetItemData(ItemID).Price)
            {
                return false;
            }
            else
            {
                if ((this.Owner.Stats.CurrentHealth / this.Owner.Stats.HealthPoints.Total) >= 0.95f)
                {
                    return true;
                }
                else { return false; }
            }
        }
        else
        {
            return false;
        }


    }

    /// <summary>
    /// Buy item
    /// </summary>
    /// <param name="ItemID">Item to buy.</param>
    protected bool UnitAIBuyItem(int ItemID)
    {
        this.Owner.ItemInventory.BuyItem((uint)ItemID);
        return true;
    }

    /// <summary>
    /// Computes a position for spell cast
    /// </summary>
    /// <param name="TargetUnit">target unit</param>
    /// <param name="ReferenceUnit">Reference unit</param>
    /// <param name="Range">Spell range</param>
    /// <param name="UnitSide">Which side of target are we going to (in between our out)</param>
    protected bool ComputeUnitAISpellPosition(AttackableUnit TargetUnit, AttackableUnit ReferenceUnit, float Range, bool UnitSide)
    {
        this.Owner.IssueOrDelayOrder(OrderType.AttackMove, ReferenceUnit, TargetUnit.Position, fromAiScript: true);
        //todo : 
        return true;
    }

    /// <summary>
    /// Retrieves a position for spell cast
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected bool GetUnitAISpellPosition(out Vector3 Output)
    {
        if ((this.Owner as Champion).SpellToCastArguments != null && (this.Owner as Champion).SpellToCastArguments.EndPos != null)
        {
            Output = (Vector3)(this.Owner as Champion).SpellToCastArguments.EndPos;
            return true;
        }
        Output = this.Owner.Position3D;
        return false;
    }

    /// <summary>
    /// Clears position for spell cast
    /// </summary>
    protected bool ClearUnitAISpellPosition()
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Unit precomputed cast location valid 
    /// </summary>
    protected bool TestUnitAISpellPositionValid(bool ReturnSuccessIf = true)
    {
        //todo:

        // (this.Owner as Champion).SpellToCastArguments.EndPos



        return Game.Map.NavigationGrid.IsWalkable(((Vector3)(this.Owner as Champion).SpellToCastArguments.EndPos).ToVector2()) == ReturnSuccessIf;
    }

    /// <summary>
    /// Unit at precomputed spell cast location
    /// </summary>
    /// <remarks>
    /// Unit at precomputed spell location
    /// </remarks>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Location">Source Reference</param>
    /// <param name="Error">Accepted error</param>
    protected bool TestUnitAtLocation(AttackableUnit Unit, Vector3 Location, float Error)
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Unit in safe range
    /// </summary>
    /// <param name="Range">Unit in safe Range</param>
    protected bool TestUnitAIIsInSafeRange(float Range)
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Computes a safe position for AI unit
    /// </summary>
    /// <param name="Range">safe range</param>
    /// <param name="UseDefender">If True, use defenders in search</param>
    /// <param name="UseEnemy">If True, use enemies to guide in search</param>
    protected bool ComputeUnitAISafePosition(float Range, bool UseDefender, bool UseEnemy)
    {

        if (this.Owner.SafePointZone != this.Owner.Position3D)
        {
            return false;
        }
        Vector3 safezone = Game.Map.SpawnPoints[this.Owner.Team].Position3D;

        //usedefender and useenemy seem never used

        this.Owner._currentWaypointIndex = 0;
        /*if (UseDefender)
        {
            var units = GetClosestUnitInRange(this.Owner.Position, Range, true, TeamId.TEAM_NEUTRAL | FCS.GetEnemyTeam(this.Owner.Team));

            if (units == null)
            {
                for (int i = 5; i >= 0; i--)
                {
                    if (LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i) != null)
                    {
                        if (!LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i).Stats.IsDead)
                        {
                            var calculbeforepathing = Extensions.GetClosestCircleEdgePoint(this.Owner.Position, LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i).Position, LaneTurret.GetTurret(Owner.Team, Owner.Currentlane, i).CollisionRadius + 10.0f);
                            safezone = calculbeforepathing.ToVector3(0);
                            this.Owner.FindSafeZone(safezone);
                            break;
                        }

                    }
                }

            }
            else
            {
                var calculbeforepathing = Extensions.GetClosestCircleEdgePoint(this.Owner.Position, units.Position, units.CollisionRadius + 10.0f);
                safezone = calculbeforepathing.ToVector3(0);
                this.Owner.FindSafeZone(safezone);
            }
        }
        if (UseEnemy)
        { //todo : for moment i use turret for retreat , bad method 

            for (int i = 5; i >= 0; i--)
            {
                if (LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i) != null)
                {
                    if (!LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i).Stats.IsDead)
                    {
                        var calculbeforepathing = Extensions.GetClosestCircleEdgePoint(this.Owner.Position, LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i).Position, LaneTurret.GetTurret(this.Owner.Team, this.Owner.Currentlane, i).CollisionRadius + 10.0f);
                        safezone = calculbeforepathing.ToVector3(0);
                        this.Owner.FindSafeZone(safezone);
                        break;
                    }

                }
            }



        }
        else
        {*/
        AttackableUnit safefriend;

        if (GetUnitAIClosestTargetInArea(out safefriend, Owner, null, Radius: Range, SpellFlags: SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
        {
            GetRandomPositionInCircle(out safezone, safefriend.Position3D, 100 + safefriend.CollisionRadius);

            this.Owner.FindSafeZone(safezone);
        }
        else
        {
            GetUnitAIClosestTargetInArea(out safefriend, Owner, null, Radius: 20000, SpellFlags: SpellDataFlags.AffectFriendly | SpellDataFlags.AffectTurrets);
            GetRandomPositionInCircle(out safezone, safefriend.Position3D, 100 + safefriend.CollisionRadius);

            this.Owner.FindSafeZone(safezone);
        }
        // }

        return true;
    }

    /// <summary>
    /// Retrieves a safe position for AI unit
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected bool GetUnitAISafePosition(out Vector3 Output)
    {

        Output = this.Owner.SafePointZone;

        return true;
    }

    /// <summary>
    /// Clears position for safe
    /// </summary>
    protected bool ClearUnitAISafePosition()
    {
        this.Owner.clearSafeZone();
        return true;
    }

    /// <summary>
    /// Unit precomputed safe location valid 
    /// </summary>
    protected bool TestUnitAISafePositionValid()
    {

        if (Game.Map.NavigationGrid.IsWalkable(this.Owner.SafePointZone.ToVector2()) && this.Owner.SafePointZone != Vector3.One)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns the base location of a given unit
    /// </summary>
    /// <remarks>
    /// Return SUCCES if we can find the base
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Unit to poll.</param>
    protected bool GetUnitAIBasePosition(out Vector3 Output, AttackableUnit Unit)
    {

        if (Unit != null)
        {
            Output = Game.Map.SpawnPoints[Unit.Team].Position3D;
            return true;
        }

        Output = default;
        return false;
    }

    /// <summary>
    /// Returns the radius AOE of spell in a given slot
    /// </summary>
    /// <remarks>
    /// Always returns SUCCESS.
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    protected bool GetUnitSpellRadius(out float Output, AttackableUnit Unit, SpellbookType Spellbook, int SlotIndex)
    {
        //TODO: Implement.
        Output = default;
        return true;
    }

    /// <summary>
    /// Returns distance between 2 units
    /// </summary>
    /// <remarks>
    /// takes into account their BB
    /// </remarks>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="SourceUnit">Source unit</param>
    /// <param name="DestinationUnit">Destination unit</param>
    protected bool GetDistanceBetweenUnits(out float Output, AttackableUnit SourceUnit, AttackableUnit DestinationUnit) // HACK to make Garen attack and follow
    {
        var calculbeforepathing = DestinationUnit.Position;
        if (DestinationUnit.capturepointid != -1 && SourceUnit.capturepointid != -1)
        {
            calculbeforepathing = Extensions.GetClosestCircleEdgePoint(SourceUnit.Position, DestinationUnit.Position, SourceUnit.CollisionRadius);
        }
        Output = Vector2.Distance(SourceUnit.Position, DestinationUnit.Position);
        return true;
    }

    /// <summary>   
    /// Unit target is in range
    /// </summary>
    /// <param name="Error">Accepted error for unit location</param>
    protected bool TestUnitAIAttackTargetInRange(float Error)
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Unit has valid target
    /// </summary>
    /// <remarks>
    /// Unit has valid target, use before getting attack target.
    /// </remarks>
    protected bool TestUnitAIAttackTargetValid(bool successif = true)
    {
        if (this.Owner.TargetUnit != null)
        {
            if (!this.Owner.TargetUnit.Stats.IsDead && this.Owner.TargetUnit.capturepointid == -1 && !this.Owner.TargetUnit.Name.Contains("Shrine"))
            {
                return true;
            }
        }
        return false;

    }

    /// <summary>
    /// Unit can see target
    /// </summary>
    /// <param name="Unit">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    protected bool TestUnitIsVisible(AttackableUnit Unit, AttackableUnit TargetUnit, bool successif = true)
    {
        if (TargetUnit != null)
        {

            if (TargetUnit.IsVisibleByTeam(Unit.Team))
            {
                return true;
            }

        }
        return false;
    }

    /// <summary>
    /// Sets item target
    /// </summary>
    /// <remarks>
    /// This version is for AttackableUnit References
    /// </remarks>
    /// <param name="TargetUnit">Target</param>
    /// <param name="ItemID">Item ID</param>
    protected bool SetUnitAIItemTarget(AttackableUnit TargetUnit, int ItemID)
    {
        //todo
        Owner.SetTargetUnit(TargetUnit);

        return true;
    }

    /// <summary>
    /// Clears item target
    /// </summary>
    protected bool ClearUnitAIItemTarget()
    {
        //todo:
        return false;
    }

    /// <summary>
    /// Unit can use item
    /// </summary>
    /// <param name="ItemID">Item ID</param>
    protected bool TestUnitAICanUseItem(int ItemID)
    {

        //todo complete branch bt useitem 
        return Owner.ItemInventory.HasItem(ItemID);

        //  return false;
    }


    /// <summary>
    /// Tests if specified slot has spell toggled ON
    /// </summary>
    /// <param name="Unit">Unit to poll</param>
    /// <param name="SlotIndex">spell slot ID</param>
    protected bool TestUnitSpellToggledOn(AttackableUnit Unit, int SlotIndex)
    {

        return (Unit as Champion).Spells[SlotIndex].Toggle;
    }

    /// <summary>
    /// Tests if unit is channeling

    /// </summary>
    /// <param name="Unit">Unit to poll</param>
    /// <parem name="ReturnSuccessIf"> todo : complete definition </parem>
    protected bool TestUnitIsChanneling(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        if (ReturnSuccessIf)
        {

            if ((Unit as Champion).ChannelSpell != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if ((Unit as Champion).ChannelSpell != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    /// <summary>
    /// Returns unit that casted a buff on input unit
    /// </summary>
    /// <param name="Output">Destination reference;.</param>
    /// <param name="Unit">Source unit</param>
    /// <param name="BuffName">Buff name</param>
    protected bool GetUnitBuffCaster(out AttackableUnit Output, AttackableUnit Unit, string BuffName)
    {

        Output = Unit.GetBuffWithName(BuffName).SourceUnit;
        return true;
    }

    /// <summary>
    /// AI Unit has an assigned task
    /// </summary>
    protected bool TestUnitAIHasTask(bool successif = true)
    {
        return false;
    }




    /// <summary>
    /// Returns position computed by a task assigned to the unit
    /// </summary>
    /// <param name="Output">Destination reference</param>
    protected bool GetUnitAITaskPosition(out Vector3 Output)
    {
        try
        {
            if (Owner is not Champion champion)
            {
                Output = Owner.Position3D;
                return true;
            }

            GetAITaskFromEntity(out AITask task, champion);
            if (task is null)
            {
                Output = Owner.Position3D;
                return true;
            }

            if (task.Topic is AITaskTopicType.PUSHLANE)
            {
                var unitfound = GetClosestUnitInRange(Owner.Position, 1200, true, Owner.Team);
                if (unitfound is null)
                {
                    Output = Owner.Closestwaypointofthelist2().ToVector3(100);
                    return true;
                }
                if (unitfound.Team != TeamId.TEAM_NEUTRAL)
                {
                    Owner.IssueOrDelayOrder(OrderType.AttackTo, unitfound, unitfound.Position, fromAiScript: true);
                    Output = unitfound.Position3D;
                    return true;
                }
            }
            Output = task.TargetLocation;
            return true;
        }
        catch (Exception e)
        {
            Output = Owner?.Position3D ?? Vector3.Zero;
            return false;
        }
    }

    /// <summary>
    /// Permanently modifies a target unit's armor.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected bool IncPermanentFlatArmorMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.Armor.IncFlatBonus(Delta);
        return true;
    }

    /// <summary>
    /// Permanently modifies a target unit's magic resistance.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected bool IncPermanentFlatMagicResistanceMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.MagicResist.IncFlatBonus(Delta);
        return true;
    }

    /// <summary>
    /// Permanently modifies a target unit's max health.
    /// This will heal the target.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// Further, this later needs to be converted to a non-healing implementation; it is using the healing approach until Kuo fixes a bug.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected bool IncPermanentFlatMaxHealthMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.HealthPoints.IncFlatBonus(Delta);
        return true;
    }

    /// <summary>
    /// Permanently modifies a target unit's attack damage.
    /// </summary>
    /// <remarks>
    /// This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.
    /// </remarks>
    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>
    protected bool IncPermanentFlatAttackDamageMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.AttackDamage.IncFlatBonus(Delta);
        return true;
    }



    //todo complete all summary beside this 


    /// <summary>Selector blocks will tick their children in order until one returns a SUCCESS, at which point the node will return SUCCESS.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return FAILURE the node will return FAILURE. Generally alternate these with sequence nodes</summary>


    protected bool SelectorServer()
    {
        return false;
    }

    /// <summary>Decorator that has the subtree run once every X seconds. The subtree will execute once immediately, and continue executing until it returns either Success or Failure. Then it will wait the period in seconds before executing again.</summary>

    /// <param name="UseMissionTime">Should we use Mission Time or Real Time? Using mission time will make it not execute while paused.</param>
    /// <param name="ExecutionPeriod">The number of seconds that should pass between executions. Note that this will be ignored until the subtree returns SUCCESS or FAILURE.</param>
    protected bool ExecutePeriodically(ref float _lastTimeExecuted, List<Func<bool>> functiontoEP, bool UseMissionTime = true, float ExecutionPeriod = 1.0f)
    {
        bool anyFalse = false;

        if (FCS.ExecutePeriodically(ExecutionPeriod, ref _lastTimeExecuted, true))
        {

            foreach (var function in functiontoEP)
            {
                bool result = function.Invoke();
                if (!result)
                {
                    anyFalse = true;
                }
            }
        }

        return true;
    }





    /// <summary>Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns FAILURE. Right now this only supports AttackableUnit collections.  This will return SUCCESS if all children return SUCCESS and FAILURE if one child returns FAILURE.</summary>

    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>

    protected bool IterateUntilFailureDecorator(out AttackableUnit Output, IEnumerable<AttackableUnit> Collection)
    {
        Output = default;
        return false;
    }
    /// <summary>Decorator that will iterate through a collection, looping its children for each entry.  Iteration will stop when a child returns SUCCESS. Right now this only supports AttackableUnit collections.  This will return SUCCESS if a child returns SUCCESS and FAILURE if all children return FAILURE.</summary>

    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>

    protected bool IterateUntilSuccessDecorator(out AttackableUnit Output, IEnumerable<AttackableUnit> Collection)
    {
        Output = default;
        return false;
    }


    /// <summary>Sets OutputRef with the value of Input. This version is for float collection.</summary>

    /// <param name="Size">Source Reference</param>
    /// <param name="Default">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool CreateFloatCollection(out IEnumerable<float> Output, int Size = 0, float Default = 0)
    {
        Output = Enumerable.Repeat(Default, Size);
        return true;
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for float collection.</summary>

    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool SetVarFloatCollection(out IEnumerable<float> Output, IEnumerable<float> Input)
    {
        Output = Input;
        return true;
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for float collection item.</summary>

    /// <param name="Collection">Source Reference</param>
    /// <param name="ItemValue">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool AddItemToFloatCollection(out IEnumerable<float> Output, IEnumerable<float> Collection, float ItemValue = 0)
    {
        var updatedCollection = Collection.ToList(); // Convertissez en une liste pour pouvoir la modifier.
        updatedCollection.Add(ItemValue);
        Output = updatedCollection;
        return true;
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for float collection item.</summary>

    /// <param name="Collection">Source Reference</param>
    /// <param name="ItemID">Source Reference</param>
    /// <param name="ItemValue">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool SetVarFloatCollectionItem(out IEnumerable<float> Output, IEnumerable<float> Collection, int ItemID = 0, float ItemValue = 0)
    {
        var updatedCollection = Collection.ToList(); // Convertissez en une liste pour pouvoir la modifier.
        if (ItemID >= 0 && ItemID < updatedCollection.Count)
        {
            updatedCollection[ItemID] = ItemValue;
            Output = updatedCollection;
            return true;
        }
        Output = default;
        return false; // Retournez FAILURE si l'index est invalide.
    }
    /// <summary>Sets OutputRef with the value of Input collection item. This version is for float collection item.</summary>

    /// <param name="Collection">Source Reference</param>
    /// <param name="ItemID">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool GetVarFloatCollectionItem(out float Output, IEnumerable<float> Collection, int ItemID = 0)
    {
        if (ItemID >= 0 && ItemID < Collection.Count())
        {
            Output = Collection.ElementAt(ItemID);
            return true;
        }
        Output = default;
        return false; // Retournez FAILURE si l'index est invalide.
    }




    //todo : need understand what is FuzzyFunctor
    // fuzzyfunctor is an function of "approx evaluation" , 
    // normaly league has them in Fuzzy_NoTower_NoCC.txt	 
    // but these file seem never leaked 


    /// <summary>Sets OutputRef with the value of Input. This version is for float collection item.</summary>

    /// <param name="FunctorTag">Functor tag</param>
    /// <param name="ResultRank">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool CreateFuzzyFunctor(out FuzzyFunctor Output, string FunctorTag, int ResultRank)
    {
        Output = new FuzzyFunctor(FunctorTag, ResultRank, inputs =>
        {
            var inputsList = inputs.ToList();

            if (FunctorTag == "NoTowerNoCC")
            {
                // Conservative rules (no CC)
                return CalculateNoTowerNoCC(inputsList);
            }
            else if (FunctorTag == "NoTowerCC")
            {
                // Aggressive rules (with CC)
                return CalculateNoTowerCC(inputsList);
            }

            // Fallback
            return new[] { 0f, 0f, 0f };
        });

        return true;
    }
    public bool MinValue(
         out float _minValue,
     float Value1,
     float Value2,
     float Value3,
     float Value4,
     float Value5
        )
    {

        float minValue = default;
        bool result =
                    // Sequence name :FindTheLowestValue

                    SetVarFloat(
                          out minValue,
                          1E+09f) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value1) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value2) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value3) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value4) &&
                    MinFloat(
                          out minValue,
                          minValue,
                          Value5)

              ;
        _minValue = minValue;
        return result;
    }
    // Function for "No tower, no CC" (conservative)
    private float[] CalculateNoTowerNoCC(List<float> inputs)
    {
        float lowBurst = inputs[0];
        float highBurst = inputs[1];
        float myHealth = inputs[2];
        float myUnhealth = inputs[3];
        float targetHealth = inputs[4];
        float targetUnhealth = inputs[5];
        float safety = inputs[6];
        float unsafety = inputs[7];
        float hasMana = inputs[8];
        float noMana = inputs[9];

        float tempScore, tempScore1, temp;
        float killScore = 0f, pokeScore = 0f, retreatScore = 0f;

        // Calcul du score de kill (conservateur)
        MinValue(
            out tempScore,
            lowBurst,
            myHealth,
            targetUnhealth,
            safety,
            hasMana);
        MaxFloat(
            out killScore,
            tempScore,
            killScore);

        MinValue(
            out tempScore1,
            highBurst,
            myUnhealth,
            targetUnhealth,
            safety,
            noMana);
        MaxFloat(
            out killScore,
            tempScore1,
            killScore);

        // Calcul du score de retraite (complémentaire aux scores précédents)
        MaxFloat(
            out temp,
            tempScore,
            tempScore1);
        SubtractFloat(
            out retreatScore,
            1f,
            temp);

        // Retourner le même nombre d'éléments qu'en entrée (10 éléments)
        return new[] {
            lowBurst,
            highBurst,
            myHealth,
            myUnhealth,
            targetHealth,
            targetUnhealth,
            safety,
            unsafety,
            hasMana,
            noMana
        };
    }

    // Fonction pour "Pas de tour, avec CC" (agressif)
    private float[] CalculateNoTowerCC(List<float> inputs)
    {
        float lowBurst = inputs[0];
        float highBurst = inputs[1];
        float myHealth = inputs[2];
        float myUnhealth = inputs[3];
        float targetHealth = inputs[4];
        float targetUnhealth = inputs[5];
        float safety = inputs[6];
        float unsafety = inputs[7];
        float hasMana = inputs[8];
        float noMana = inputs[9];

        float tempScore, tempScore1, temp;
        float killScore = 0f, pokeScore = 0f, retreatScore = 0f;

        // Calcul du score de kill (agressif - utilise unsafety au lieu de safety)
        MinValue(
            out tempScore,
            lowBurst,
            myHealth,
            targetUnhealth,
            unsafety,  // plus risqué que "safety"
            hasMana);
        MaxFloat(
            out killScore,
            tempScore,
            killScore);

        MinValue(
            out tempScore1,
            highBurst,
            myUnhealth,
            targetUnhealth,
            unsafety,
            noMana);
        MaxFloat(
            out killScore,
            tempScore1,
            killScore);

        // Calcul du score de retraite (complémentaire aux scores précédents)
        MaxFloat(
            out temp,
            tempScore,
            tempScore1);
        SubtractFloat(
            out retreatScore,
            1f,
            temp);

        // Retourner le même nombre d'éléments qu'en entrée (10 éléments)
        return new[] {
            lowBurst,
            highBurst,
            myHealth,
            myUnhealth,
            targetHealth,
            targetUnhealth,
            safety,
            unsafety,
            hasMana,
            noMana
        };
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for fuzzy functor.</summary>

    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool SetVarFuzzyFunctor(out FuzzyFunctor Output, FuzzyFunctor Input)
    {
        // Copier la référence de l'entrée vers la sortie
        Output = Input;

        return true; // Indique la réussite
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for float collection item.</summary>

    /// <param name="Functor">Source Reference</param>
    /// <param name="Argument">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool EvalFuzzyFunctor(out IEnumerable<float> Output, FuzzyFunctor Functor, IEnumerable<float> Argument)
    {
        // Vérification de base
        if (Functor == null || Argument == null)
        {
            Output = null;
            return false; // Échec si functor ou argument est nul
        }

        // Évaluer le functor avec les arguments fournis
        Output = Functor.Evaluate(Argument);
        return true; // Indique la réussite
    }



    /// <summary>Create a Debug Line that is displayed into the world. Outputs an ID to refer to this line again by. If you give it a Unit it will start at that unit, otherwise it will display at the input postion. Same for end position.</summary>

    /// <param name="Viewer">Optional: Start the line at this unit's location</param>
    /// <param name="Position">Optional: The position at which to start the line</param>
    /// <param name="TargetUnit">Optional: The place the line goes; note that target unit is ignored if source unit is NULL (if unit->position line, make source the unit and target the position)</param>
    /// <param name="TargetPosition">Optional: Target location in world for line to end at</param>
    /// <param name="Color">The color of the line</param>
    /// <param name="Time">How long the line will last</param>
    /// <param name="DebugLineId">A Unique identifier for referring back to this Debug Line</param>

    protected bool AddDebugLine(out int DebugLineId, AttackableUnit Viewer, Vector3 Position = default(Vector3), AttackableUnit TargetUnit = default, Vector3 TargetPosition = default(Vector3), Color Color = default(Color), float Time = 0)
    {
        DebugLineId = default;
        return false;
    }
    /// <summary>Create a Debug Circle that is displayed into the world. Outputs an ID to refer to this circle again by. If you give it a Unit it will display at that unit, otherwise it will display at the input postion.</summary>

    /// <param name="Unit">Optional: Display the debug circle at the location of this unit</param>
    /// <param name="Position">Optional: The position at which to display the debug circle.</param>
    /// <param name="Radius">How large is the debug circle</param>
    /// <param name="Color">The Color of the debug circle</param>
    /// <param name="Time">How long the circle will last</param>
    /// <param name="DebugCircleId">A Unique identified to refer back to this Debug Circle</param>

    protected bool AddDebugCircle(out int DebugCircleId, AttackableUnit Unit = default, Vector3 Position = default(Vector3), float Radius = 100.0f, Color Color = default(Color), float Time = 0)
    {
        //todo finc way to add color 
        //Region Debugcircle = AddPosPerceptionBubble(Position.ToVector2(), Radius, Time ,TeamId.TEAM_UNKNOWN)  ;
        string nameparticlecolor = "DebugCircle_green.troy";
        if (Color.R != 0 && Color.G != 255 && Color.B != 0)
        {
            nameparticlecolor = "DebugCircle_red.troy";
        }
        var p1 = ApiFunctionManager.AddParticleTarget(Unit, nameparticlecolor, Unit, Position.ToVector2(), 25000, Radius, Vector3.Zero);

        ModifyDebugCircleRadiusNotify(0, 0, (int)p1.NetId, Radius);

        DebugCircleId = (int)p1.NetId;
        return true;
    }
    /// <summary>Create a Debug Line that is displayed into the world. Outputs an ID to refer to this line again by. If you give it a Unit it will start at that unit, otherwise it will display at the input postion. Same for end position.</summary>

    /// <param name="Position">Optional: The position at which to place the text</param>
    /// <param name="TargetUnit">Optional: The unit to attach the text to</param>
    /// <param name="TargetPosition">The offset that will be applied to the position or unit (in screen space, x y only)</param>
    /// <param name="Color">The color of the line</param>
    /// <param name="Time">How long the line will last</param>
    /// <param name="String">The string to print</param>
    /// <param name="EndFlag">If the new items should go at the top of the stack</param>
    /// <param name="Max">Max number of items in the stack</param>
    /// <param name="DebugCircleId">A Unique identifier for referring back to this Debug Line</param>

    protected bool AddDebugText(out int DebugCircleId, AttackableUnit TargetUnit, Vector3 Position = default(Vector3), Vector3 TargetPosition = default(Vector3), Color Color = default(Color), float Time = 0, string String = "", bool EndFlag = true, int Max = 1)
    {
        DebugCircleId = default;
        return false;
    }
    /// <summary>Delete this Debug Circle</summary>

    /// <param name="DebugCircleId">The ID of the debug circle to remove.</param>

    protected bool RemoveDebugDraw(int DebugCircleId)
    {
        return false;
    }
    /// <summary>Change the radius of a debug circle</summary>

    /// <param name="DebugCircleId">The ID of the debug circle to change.</param>
    /// <param name="Radius">How large is the debug circle</param>

    protected bool ModifyDebugCircleRadius(int DebugCircleId, float Radius = 100)
    {
        return false;
    }
    /// <summary>Change the color of a debug circle</summary>

    /// <param name="DebugCircleId">The ID of the debug circle to change.</param>
    /// <param name="Color">The Color of the debug circle</param>

    protected bool ModifyDebugColor(int DebugCircleId, Color Color = default(Color))
    {
        return false;
    }
    /// <summary>Hide or unhide a debug drawing</summary>

    /// <param name="DebugCircleId">The ID of the debug drawing to change.</param>
    /// <param name="EndFlag">Whether or not to hide the drawing</param>

    protected bool SetDebugHidden(int DebugCircleId, bool EndFlag = true)
    {
        return false;
    }
    /// <summary>Set or push to a given text block</summary>

    /// <param name="DebugCircleId">The ID of the debug drawing to change.</param>
    /// <param name="String">The ID of the debug drawing to change.</param>
    /// <param name="EndFlag">Whether to push the flag or to clear and set the text</param>

    protected bool ModifyDebugText(int DebugCircleId, string String = "", bool EndFlag = false)
    {
        return false;
    }
    /// <summary>Assign an object to be a certain capture point.</summary>

    /// <param name="CapturePoint">The ID of the capture point to set.</param>
    /// <param name="TargetUnit">Unit to be assigned to the point</param>
    /// <param name="PrimaryAbilityResourceType">Primary Ability Resource that holds the value of this capture point</param>

    protected bool CapturePoint_AttachToObject(int CapturePoint, AttackableUnit TargetUnit, PrimaryAbilityResourceType PrimaryAbilityResourceType = PrimaryAbilityResourceType.MANA)
    {
        apimap.NotifyHandleCapturePointUpdate(CapturePoint, TargetUnit.NetId, (byte)PrimaryAbilityResourceType, 0, CapturePointUpdateCommand.AttachToObject);
        //TargetUnit.Stats.PrimaryAbilityResourceType = PrimaryAbilityResourceType;
        TargetUnit.capturepointid = CapturePoint;
        (TargetUnit as Minion).hackloadspell();
        (TargetUnit as Minion).LoadBehaviourTreeTurretOdin();
        return true;
    }
    /// <summary>Sets the game score for a team.</summary>

    /// <param name="Team">The team whose point you want to change.</param>
    /// <param name="Score">The new score for the team.</param>

    protected bool SetGameScore(TeamId Team, float Score = 0)
    {

        if (Game.Map.MapData.MapScoring.ContainsKey(Team))
        {
            Game.Map.MapData.MapScoring[Team] = Score;
        }
        else
        {
            Game.Map.MapData.MapScoring.Add(Team, Score);
        }
        GameScoreNotify(Team, (int)Score);

        return true;
    }
    /// <summary>Create a new quest and display it in the HUD. This should only accept localized strings.</summary>

    /// <param name="String">The localized string to display.</param>
    /// <param name="Team">The player whose quest you want to activate</param>
    /// <param name="QuestType">Quest type; which quest tracker you want the quest to be added to</param>
    /// <param name="HandleRollOver">OPTIONAL. Should we handle the mousing rolling over and rolling out from this quest?</param>
    /// <param name="Tooltip">Optional: The tooltip to display on rollover of the quest.</param>
    /// <param name="Reward">Optional: The reward to display in the reward section of the quest.</param>
    /// <param name="QuestId">Gives a unique identifier to refer back to this quest</param>

    protected bool ActivateQuestForTeam(out int QuestId, string String, TeamId Team, QuestType QuestType = QuestType.Primary, bool HandleRollOver = false, string Tooltip = "", string Reward = "")
    {
        var quest = new Quest(
            String,
            Tooltip,
            Reward,
            QuestType,
            QuestEvent.Press,
            Team);

        QuestId = quest.QuestID;
        return true;
    }
    /// <summary>Plays a quest completion animation and then removes it from the HUD. Used on quest ids returned by the ActivateQuest node.</summary>

    /// <param name="QuestId">Unique identfier used to refer to the quest; returned by ActivateQuest</param>
    /// <param name="Success">Was the quest a success or a failure?</param>

    protected bool CompleteQuest(int QuestId, bool Success = true)
    {
        var quest = Quest.FindByID(QuestId);
        if (Success)
        {
            Quest.winQuest(QuestId);
            return true;
        }
        else
        {
            Quest.loseQuest(QuestId);
            return true;
        }
    }

    /// <summary>Test to see if the quest has the mouse rolled over it. This quest must have been activated with HandleRollOver=true in ActivateQuest.</summary>

    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has the mouse over it; if False, returns SUCCESS if unit does not have the mouse over it, or if the quest doesnt exist, or if the quest doesnt have HandleRollOver=true</param>

    protected bool TestQuestRolledOver(int QuestId, bool ReturnSuccessIf = true)
    {
        return false;
    }
    /// <summary>Test to see if the quest is being clicked right now with the mouse down over it. Tests to see if the quest is being clicked right now, or if the mouse is not clicking it right now.</summary>

    /// <param name="QuestId">Which Quest should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the quest has been clicked by the mouse right now; if False, returns SUCCESS if the quest is not being clicked, or if the quest doesnt exist.</param>

    protected bool TestQuestClicked(int QuestId, bool ReturnSuccessIf = true)
    {
        return false;
    }


    /// <summary>Create a new Respawn Point. If you do not give a spell name, unit will reincarnate at its currently assigned Respawn Point.</summary>

    /// <param name="TargetLocation">3D Point containing the Respawn Point location.</param>
    /// <param name="SpellName">Name of spell to be cast when unit respawns.  It is highly recommended that spell includes the Reincarnate block, or the unit will not come back to life.</param>
    /// <param name="Team">Team ID of units that can use this Respawn Point</param>
    /// <param name="RespawnPointUIID">The UI id of this respawn point if you want it to appear in the UI. indexed from 0. If you do not want to appear in the UI send -1 or do not include this parameter.</param>
    /// <param name="RespawnPointID">Gives a unique identifier to refer back to this RespawnPoint.</param>

    protected bool CreateRespawnPoint(out int RespawnPointID, Vector3 TargetLocation, string SpellName = "", TeamId Team = default, int RespawnPointUIID = -1)
    {
        RespawnPointID = default;
        return false;
    }
    /// <summary>Set a player's Respawn Point. Returns FAILURE if Player is not valid.  If you use an invalid Respawn Point ID, it will clear the player's Respawn Point.</summary>

    /// <param name="Player">Player to set Respawn Point on.</param>
    /// <param name="RespawnPointID">Unique identfier used to refer to the Respawn Point; returned by CreateRespawnPoint.</param>

    protected bool SetRespawnPoint(AttackableUnit Player, int RespawnPointID)
    {
        return false;
    }
    /// <summary>Allows a team to respawn at a Respawn Point. Always returns SUCCESS.  Logs an INFO message if you use an invalid Respawn Point ID (which will not show up in Live).</summary>

    /// <param name="RespawnPointID">Unique identfier used to refer to the Respawn Point; returned by CreateRespawnPoint.</param>
    /// <param name="Team">Team ID of units that can use this Respawn Point</param>

    protected bool AllowTeamRespawnPoint(int RespawnPointID, TeamId Team)
    {
        return false;
    }
    /// <summary>Disallows a team to respawn at a Respawn Point. Always returns SUCCESS.  Logs an INFO message if you use an invalid Respawn Point ID (which will not show up in Live).</summary>

    /// <param name="RespawnPointID">Unique identfier used to refer to the Respawn Point; returned by CreateRespawnPoint.</param>
    /// <param name="Team">Team ID of units that can no longer use this Respawn Point</param>

    protected bool DisallowTeamRespawnPoint(int RespawnPointID, TeamId Team)
    {
        return false;
    }
    /// <summary>Sets the location of a Respawn Point. Always returns SUCCESS.  Logs an INFO message if you use an invalid Respawn Point ID (which will not show up in Live).</summary>

    /// <param name="RespawnPointID">Unique identfier used to refer to the Respawn Point; returned by CreateRespawnPoint.</param>
    /// <param name="TargetLocation">3D Point containing the Respawn Point location.</param>

    protected bool SetLocationRespawnPoint(int RespawnPointID, Vector3 TargetLocation)
    {
        return false;
    }
    /// <summary>Sets the spell used by a Respawn Point. Always returns SUCCESS.  Logs an INFO message if you use an invalid Respawn Point ID (which will not show up in Live).</summary>

    /// <param name="RespawnPointID">Unique identfier used to refer to the Respawn Point; returned by CreateRespawnPoint.</param>
    /// <param name="SpellName">Name of spell to be cast when unit respawns.  It is highly recommended that spell includes the Reincarnate block, or the unit will not come back to life.</param>

    protected bool SetSpellRespawnPoint(int RespawnPointID, string SpellName)
    {
        return false;
    }
    /// <summary>Set a player's Auto Respawn state. Returns FAILURE if Player is not valid.</summary>

    /// <param name="Player">Player to set Auto Respawn flag on.</param>
    /// <param name="Enabled">If true, player will attempt to use the last Respawn Point it had set when it dies again; if false, Respawn Point is cleared and the player must select one to respawn.</param>

    protected bool SetAutoRespawn(AttackableUnit Player, bool Enabled = true)
    {
        return false;
    }

    /// <summary>Creates a color from three   components. If you want to copy a Color, use SetVarColor.</summary>

    /// <param name="R">Red component</param>
    /// <param name="G">Green component</param>
    /// <param name="B">Yellow component</param>
    /// <param name="A">Alpha component</param>
    /// <param name="Color">OutputColor</param>

    protected bool MakeColor(out Color Color, int R = 0, int G = 0, int B = 0, int A = 255)
    {
        Color = new Color(
            (byte)R,
            (byte)G,
            (byte)B,
            (byte)A
        );
        return true;
    }


    /// <summary>Returns three collections of units in the target area, one for units that entered since last update, one for units that entered on this update, and one for units that exited. Always returns SUCCESS.  Uses the reference unit for enemy/ally checks; must be present!  DO NOT use nodes or precedures that return RUNNING as children of this node!</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    /// <param name="OnEnterCollection">Destination Reference; holds a collection of units that entered the area since last update</param>
    /// <param name="OnUpdateCollection">Destination Reference; holds a collection of units that were in the area last update</param>
    /// <param name="OnExitCollection">Destination Reference; holds a collection of units that exited the area since last update</param>

    protected bool AreaTrigger(out IEnumerable<AttackableUnit> OnEnterCollection, out IEnumerable<AttackableUnit> OnUpdateCollection, out IEnumerable<AttackableUnit> OnExitCollection, AttackableUnit Unit, Vector3 TargetLocation, float Radius = 0, SpellDataFlags SpellFlags = SpellDataFlags.AlwaysSelf)
    {
        OnEnterCollection = default;
        OnUpdateCollection = default;
        OnExitCollection = default;
        return false;
    }


    /// <summary>Test to see if unit is targetable. Unit is not targetable if it does not exist; use ReturnSuccessIf to invert the test.</summary>

    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is targetable; if False, returns SUCCESS if unit is not targetable or does not exist</param>

    protected bool TestUnitIsTargetable(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        return false;
    }
    /// <summary>Set a unit to be invulnerable, or strip it of its invulnerability</summary>

    /// <param name="Unit">Unit to have its invulnerability changed</param>
    /// <param name="Enable">If true, sets the unit to be invulnerable. If false, strips unit of its invulnerability</param>

    protected bool SetUnitInvulnerable(AttackableUnit Unit, bool Enable = true)
    {
        return false;
    }
    /// <summary>Create an obj AI turret. An AI turret is the second of two turret pieces. It attaches to a world obj_turret thats automatically created by the level.</summary>

    /// <param name="Name">The name of the obj_turret created in the level automatically from the Maya file</param>
    /// <param name="SkinName">The SkinName of the AI turret to create</param>
    /// <param name="Lane">Lane of the turret.  Check the level script for the enum.</param>
    /// <param name="Position">Position of the turret.  Check the level script for the enum.</param>
    /// <param name="Team">Team ID of this turret.</param>
    /// <param name="Turret">Destination Reference; holds a turret object handle</param>

    protected bool CreateChildTurret(out AttackableUnit Turret, string Name, string SkinName, int Lane = 1, int Position = 1, TeamId Team = TeamId.TEAM_ORDER)
    {
        Turret = default;
        return false;
    }
    /// <summary>Test to see if unit is stealthed. Unit is not stealthed if it does not exist; use ReturnSuccessIf to invert the test.</summary>

    /// <param name="Unit">Unit to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit is stealthed; if False, returns SUCCESS if unit is not stealthed or does not exist</param>

    protected bool TestUnitIsStealthed(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        return false;
    }


    /// <summary>Test to see if unit has a specific buff. Unit does not have buff if it does not exist; use ReturnSuccessIf to invert the test.</summary>

    /// <param name="Unit">Unit to be tested</param>
    /// <param name="BuffType">Name of buff to be tested</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit has buff; if False, returns SUCCESS if unit does not have buff or does not exist</param>

    protected bool TestUnitHasAnyBuffOfType(AttackableUnit Unit, BuffType BuffType, bool ReturnSuccessIf = true)
    {

        return Unit.HasBuffType(BuffType) == ReturnSuccessIf;
    }

    /// <summary>Disabled or Enables a specific user input. Disables or Enables that specific user input type for all users.</summary>

    /// <param name="InputLockingType">Input Locking Type.</param>
    /// <param name="Enabled">If False disables input type for all users. If True, enables it.</param>
    /// InputLockingType class needed for it ToggleSpecificUserInput(InputLockingType InputLockingType , bool Enabled = true)
    protected bool ToggleSpecificUserInput(string InputLockingType, bool Enabled = true)
    {
        return false;
    }

    /// <summary>Disabled or Enables the greyscale when dead for all players.  Commonly used in the end of game ceremony.</summary>

    /// <param name="Enabled">If False disables the texture for all heroes. If True, enables it.</param>

    protected bool SetGreyscaleEnabledWhenDead(bool Enabled = true)
    {
        return false;
    }


    /// <summary>Changes the team of the specified unit. Returns FAILURE if unit is invalid.</summary>

    /// <param name="Unit">Unit to set.</param>
    /// <param name="Team">New team the unit will be.</param>

    protected bool SetUnitTeam(AttackableUnit Unit, TeamId Team = TeamId.TEAM_NEUTRAL)
    {
        Unit.SetTeam(Team);
        return true;
    }
    /// <summary>Changes the minimap icon of the specified unit. Sets a minimap icon based on the minimap.ini file in the HUD folder.</summary>

    /// <param name="Unit">Unit to set minimap icon</param>
    /// <param name="Icon">Name of minimap icon section in the minimap.ini file in the HUD directory</param>

    protected bool SetUnitMinimapIcon(AttackableUnit Unit, string Icon)
    {
        return false;
    }
    /// <summary>Teleports a unit to the targeted location. Only works on Obj_AI_Base units.</summary>

    /// <param name="Unit">Unit to teleport.</param>
    /// <param name="Position">The location to teleport to.</param>

    protected bool TeleportUnitToPosition(AttackableUnit Unit, Vector3 Position)
    {
        return false;
    }

    /// <summary>Ends the game. Obviously be careful when calling this.</summary>

    /// <param name="Team">The winning team.  Has to be order or chaos.</param>
    /// <param name="ChaosVictoryPointTotal">The Victory Point Total for Chaos Team. A negative value means this mode does not use Victory Points.</param>
    /// <param name="OrderVictoryPointTotal">The Victory Point Total for Order Team. A negative value means this mode does not use Victory Points.</param>

    protected bool EndGame(TeamId Team = TeamId.TEAM_ORDER, float ChaosVictoryPointTotal = -1.0f, float OrderVictoryPointTotal = -1.0f)
    {
        return false;
    }

    /// <summary>Sets whether a champion is stealthed or not.</summary>

    /// <param name="Unit">Sets status of this unit.</param>
    /// <param name="Enable">Set to true if the target should be stealthed, false if otherwise.</param>

    protected bool SetUnitStealthed(AttackableUnit Unit, bool Enable = true)
    {
        return false;
    }
    /// <summary>Sets whether a champion is rendered or not.</summary>

    /// <param name="Unit">Sets status of this unit.</param>
    /// <param name="Enable">Set to true if the target should be rendered, false if otherwise.</param>

    protected bool SetUnitRendered(AttackableUnit Unit, bool Enable = true)
    {
        return false;
    }
    /// <summary>Sets whether a can not be targeted by a certain team.  Using this will make them targetable to other teams. This should probably be re-written, the semantics here are bad.  Functionally it called SetUnitTargetableState(true) before excluding the team.</summary>

    /// <param name="Unit">Sets status of this unit.</param>
    /// <param name="Team">The name of the team that can not target you.  Really only listens to see if this is differnt than your own team, as it only affects the ally/enemy spell flags and isn't team specific.</param>

    protected bool SetUnitNotTargetableToTeam(AttackableUnit Unit, TeamId Team = TeamId.TEAM_ORDER)
    {
        return false;
    }
    /// <summary>Sets whether a can be targeted. This is a global toggle that overwrites specific team targeting.</summary>

    /// <param name="Unit">Sets status of this unit.</param>
    /// <param name="Enable">If true, this unit can be targetted by anyone.  False by noone.</param>

    protected bool SetUnitTargetableState(AttackableUnit Unit, bool Enable = true)
    {
        return false;
    }


    /// <summary>A subtree. Collapse</summary>

    /// <param name="TreeName">can not be empty</param>

    protected bool SubTree(string TreeName)
    {
        return false;
    }

    /// <summary>Test if game was surrendered. Tests if game was surrendered. True if game started. False if not.</summary>

    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>
    /// <param name="Team">What team to run the test on?</param>

    protected bool TestTeamSurrendered(bool ReturnSuccessIf = true, TeamId Team = TeamId.TEAM_UNKNOWN)
    {
        return false;
    }

    /// <summary>Restrict the unit to only being able to move within a certain radius of a specified point. Also restricts camera to that area. May only work on players right now. Set Radius to 0 to disable.</summary>

    /// <param name="Player">The player whose movement you want to restrict/unrestrict</param>
    /// <param name="TargetPosition">The CenterPoint of the area you want to restrict movement to</param>
    /// <param name="Radius">The size of the area from the TargetPosition that you want to restrict movement to. Setting to 0 disables the restriction.</param>
    /// <param name="Camera">Should the camera be restricted to this area as well?</param>

    protected bool SetUnitCircularMovementRestriction(AttackableUnit Player, Vector3 TargetPosition, float Radius, bool Camera = true)
    {

        if (Radius != 0)
        {
            // FLS.AddRestrictedZone(TargetPosition.X, TargetPosition.Z, Radius, Radius, TargetPosition.ToVector2(), Radius, TargetPosition);
        }
        else
        {
            //be aware this remove all restricted zone 
            //  FLS.RemoveRestrictedZones();
        }
        SetCircularMovementRestrictionNotify(Player as ObjAIBase, TargetPosition, Radius, Camera);
        return true;
    }


    /// <summary>Cast specified Spell</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>
    /// <param name="Level">Force Override Level. Do we want to force a specific level of the spell other than the one we have actually learned?</param>
    /// <param name="Queue">Allow spell queuing? If another spell is already being cast should we cast thsi one? Cast it if true, dont cast if false.</param>

    protected bool CastUnitSpell(AttackableUnit Unit, SpellbookType Spellbook = SpellbookType.SPELLBOOK_UNKNOWN, int SlotIndex = 0, int Level = 0, bool Queue = true)
    {
        if (Unit == null)
        {
            return false;
        }
        else
        {
            if (Unit is Champion)
            {
                var _unit = Unit as Champion;


                if (_unit.Spells[SlotIndex].Level > 0)
                {
                    //cast spell self ( soraka Q )
                    if (_unit.Spells[SlotIndex].SpellData.Flags.HasFlag(SpellDataFlags.AlwaysSelf) && _unit.Spells[SlotIndex].SpellData.TargetingType == TargetingType.Self) //|| _unit.Spells[SlotIndex].SpellData.Flags.HasFlag(SpellDataFlags.InstantCast)
                    {
                        SpellCast(_unit.Spells[SlotIndex], this.Owner, false);
                        _unit.Spells[SlotIndex].SetCooldown(_unit.Spells[SlotIndex].GetCooldown(_unit.Spells[SlotIndex].Level));
                    }
                    else
                    {
                        //cast spell self ( spell to cast unit  nunu E  )
                        if (_unit.TargetUnit != null)
                        {
                            SpellCast(_unit.Spells[SlotIndex], _unit.TargetUnit, false, isForceCastingOrChanneling: true);
                            //SpellCast(_unit.Spells[SlotIndex], _unit.Position, _unit.TargetUnit.Position, false);
                        }
                        else
                        {
                            //spell to cast pos 
                            if (_unit.SpellPosition != null)
                            {
                                SpellCast(_unit.Spells[SlotIndex], _unit.Position, ((Vector3)_unit.SpellPosition).ToVector2(), false, isForceCastingOrChanneling: true);
                            }
                            else
                            {
                                //spell need use task 
                                if (_unit.AssignedTasks != null && _unit.AssignedTasks.Count() > 0)
                                {
                                    if (_unit.AssignedTasks.First().TargetLocation != null || _unit.AssignedTasks.First().TargetLocation != default)
                                        SpellCast(_unit.Spells[SlotIndex], _unit.Position, _unit.AssignedTasks.First().TargetLocation.ToVector2(), false);
                                    else
                                        SpellCast(_unit.Spells[SlotIndex], _unit.Position, ((Vector3)_unit.Spells[SlotIndex]._CastArgs.EndPos).ToVector2(), false);
                                }
                                else
                                {
                                    if (_unit.TargetUnit != null)
                                    {
                                        SpellCast(_unit.Spells[SlotIndex], _unit.Position, _unit.TargetUnit.Position, false);
                                    }
                                    else
                                    {
                                        //maskfailure
                                        SpellCast(_unit.Spells[SlotIndex], _unit.Position, _unit.Position, false);
                                    }
                                }
                            }


                        }
                    }
                    return true;
                }
            }
            else
            {
                SpellCast(Owner.Spells[SlotIndex], Owner.TargetUnit, false);
                return true;
            }
        }
        return false;
    }


    /// <summary>Set specified Spell target position</summary>

    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>

    protected bool SetAIUnitSpellTargetLocation(Vector3 TargetLocation, SpellbookType Spellbook = SpellbookType.SPELLBOOK_CHAMPION, int SlotIndex = 0)
    {
        return false;
    }

    /// <summary>Set specified Spell target</summary>

    /// <param name="TargetUnit">Target Input.</param>
    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>

    protected bool SetAIUnitSpellTarget(AttackableUnit TargetUnit, SpellbookType Spellbook = SpellbookType.SPELLBOOK_CHAMPION, int SlotIndex = 0)
    {
        // ()
        Owner.SetSpellToCast(Owner.Spells[SlotIndex], new CastArguments(
            TargetUnit,
            Owner.Position3D,
            Owner.Position3D
            ));
        return true;
    }

    /// <summary>Clears specified Spell target</summary>

    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>

    protected bool ClearAIUnitSpellTarget(SpellbookType Spellbook = SpellbookType.SPELLBOOK_CHAMPION, int SlotIndex = 0)
    {
        return false;
    }

    /// <summary>Test validity of specified Spell target</summary>

    /// <param name="Spellbook">Spellbook</param>
    /// <param name="SlotIndex">Spell slot.</param>

    protected bool TestAIUnitSpellTargetValid(SpellbookType Spellbook = SpellbookType.SPELLBOOK_CHAMPION, int SlotIndex = 0)
    {
        return false;
    }


    /// <summary>Unit update awareness sensors</summary>


    protected bool UpdateUnitAISensors()
    {
        return false;
    }



    /// <summary>Spawn a squad of monsters based on the supplied encounter definition. Must match an encounter from this map, returns a handle to the squad that is spawned.</summary>

    /// <param name="EncounterId">Id from a created Encounter.</param>
    /// <param name="Position">Where do you want to spawn the monsters.</param>
    /// <param name="Team">What team is this squad on?</param>
    /// <param name="SquadName">Give this squad a name. Doesn't really do anything, but naming things can be fun! FUCK YOU RIOT </param>
    /// <param name="ExistingSquadId">Use this parameter if you have already spawned this squad once, and only want to respawn dead members of the squad.</param>
    /// <param name="SquadId">Gives a unique identifier to refer back to this squad</param>
    private int SquadAIincrement = 0;

    protected bool SpawnSquadFromEncounter(out int SquadId, int EncounterId, Vector3 Position, TeamId Team = TeamId.TEAM_UNKNOWN, string SquadName = "", int ExistingSquadId = 0)
    {
        Encounter encoutertouse = Game.Map.MapData.encounterManager.GetEncounterByID(EncounterId);
        IEnumerable<Minion> listminion = Enumerable.Empty<Minion>();
        string icontouse = "";
        var monsterWithIcon = encoutertouse.EncounterDefinition.MonsterGroups
        .First()
        .Monsters
        .FirstOrDefault(x => !string.IsNullOrEmpty(x.MinimapIcon));

        // Si un monstre avec MinimapIcon est trouv�, utilise son ic�ne
        if (monsterWithIcon != null)
        {
            icontouse = monsterWithIcon.MinimapIcon;
        }
        foreach (var monster in encoutertouse.EncounterDefinition.MonsterGroups.First().Monsters)
        {
            bool testtargetable = false;
            if (monster.Targetable != null)
            {
                if (monster.Targetable == 1)
                {
                    testtargetable = true;
                }
            }
            for (int i = 0; i < monster.Count; i++)
            {

                if (monster.SkinName == "")
                {
                    if (Team == TeamId.TEAM_ORDER)
                    {
                        Minion _minion = FLS.SpawnMinion(monster.SkinName, monster.OrderSkinName, monster.runLuaAI, Position, Team, false, false, false, false, false, false, 0.0f, hasbt: true);
                        listminion = listminion.Concat(new[] { _minion });
                        if (Game.Map.MapData.Id == 8 && monster.OrderSkinName.Contains("Blue_Minion"))
                        {
                            // BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}.AI_manager.Minions", SnOdinGolemBomb) ?? new BehaviourTree(_minion);
                            // BehaviourTree.Owner = _minion;
                        }
                    }
                    else
                    {
                        Minion _minion = FLS.SpawnMinion(monster.SkinName, monster.ChaosSkinName, monster.runLuaAI, Position, Team, false, false, false, false, false, false, 0.0f, hasbt: true);
                        listminion = listminion.Concat(new[] { _minion });
                        if (Game.Map.MapData.Id == 8 && monster.ChaosSkinName.Contains("Red_Minion"))
                        {
                            // BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}.AI_manager.Minions", SnOdinGolemBomb) ?? new BehaviourTree(_minion);
                            // BehaviourTree.Owner = _minion;
                        }
                    }
                }
                else
                {
                    if (SquadName.Contains("CaptureGuardian"))
                    {
                        Minion _minion = FLS.SpawnMinion(monster.SkinName, monster.SkinName, monster.runLuaAI, Position, Team, false, false, false, false, false, false, 0.0f, hasbt: true);
                        listminion = listminion.Concat(new[] { _minion });

                    }
                    else
                    {
                        Minion _minion = FLS.SpawnMinion(monster.SkinName, monster.SkinName, monster.runLuaAI, Position, Team, false, false, false, false, false, false, 0.0f); // testtargetable
                                                                                                                                                                                //hack 

                        listminion = listminion.Concat(new[] { _minion });
                    }

                }
            }

        }
        ;
        if (listminion.Any())
        {

            var squad = new AISquadClass(SquadName, 99, this.aimanagerTeam);
            squad.AddUnitListtoSquad(listminion.Cast<AttackableUnit>());
            AISquadList aisquadlist = new(icontouse, SquadName, squad);
            Game.Map.MapData.aisquadlistmanager.AddAISquadList(aisquadlist);
            SquadId = aisquadlist.AISquadListID;

            AISquadList byName = Game.Map.MapData.aisquadlistmanager.GetAISquadListBySquadname(SquadName); // Remplacez par le nom appropri�c
            return true;
        }
        SquadId = default;
        return true;
    }

    void SpawnDefaultbt(CampData data)
    {
        FLS.SpawnNeutralMinion_CS(data, data.GroupNumber, 0, 0);
    }
    /// <summary>Spawn a camp of neutral monsters based on the supplied encounter definition. Must match an encounter from this map, returns a handle to the squad that is spawned.</summary>

    /// <param name="EncounterId">Id from a created Encounter.</param>
    /// <param name="Position">Where do you want to spawn the monsters.</param>
    /// <param name="Icon">What icon in the minimaps.ini file should we use to represent this camp?</param>
    /// <param name="CampId">The id of this neutral camp.  Every squad spawned  under the same ID will share the same neutral camp icon, and be considered the same 'camp'.</param>
    /// <param name="ExistingSquadId">Use this parameter if you have already spawned this squad once, and only want to respawn dead members of the squad.</param>
    /// <param name="SquadName">Give this squad a name. Doesn't really do anything, but naming things can be fun!</param>
    /// <param name="SquadId">Gives a unique identifier to refer back to this squad</param>

    protected bool SpawnNeutralCampFromEncounter(out int SquadId, int EncounterId, Vector3 Position, string Icon = "Camp", int CampId = 0, int ExistingSquadId = 0, string SquadName = "")
    {
        // FLS
        if (NeutralCampManager.FindCamp(CampId)! == null)
        {
            Encounter encoutertouse = Game.Map.MapData.encounterManager.GetEncounterByID(EncounterId);
            List<CampData> NeutralCamps = new();
            float delay = 0;
            if (encoutertouse.EncounterDefinition.SpawnDelay != null)
            {
                delay = (float)encoutertouse.EncounterDefinition.SpawnDelay;
            }
            List<Monster> Monsterinencounter = encoutertouse.EncounterDefinition.MonsterGroups.First().Monsters;
            List<List<KeyValuePair<string, string>>> Grouptocreate = new();
            List<List<string>> uniquenamestocreate = new();
            foreach (var monstre in Monsterinencounter)
            {
                List<KeyValuePair<string, string>> groupe = new()
                {
              new KeyValuePair<string, string>(monstre.SkinName, "")
            };
                List<string> nomMonstre = new()
                {
              monstre.SkinName

            };
                Grouptocreate.Add(groupe);
                uniquenamestocreate.Add(nomMonstre);
            }



            CampData camp = new()
            {
                Timer = null,
                Positions = new() { Position },
                FacePositions = new() { Position },
                GroupBuffSide = TeamId.TEAM_NEUTRAL,
                MinimapIcon = Icon,
                GroupNumber = CampId,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = delay,
                RespawnTime = delay,
                Groups = Grouptocreate,
                UniqueNames = uniquenamestocreate,
                AIScript = ""
            };

            for (int i = 0; i < camp.UniqueNames.Count; i++)
            {
                camp.AliveTracker.Add(new());
                foreach (string uniqueName in camp.UniqueNames[i])
                {
                    camp.AliveTracker[i][uniqueName] = false;
                }
            }
            //data.GroupDelaySpawnTime -= UNK_TIME_CONST_OFFSET;
            camp.Timer = () => SpawnDefaultbt(camp);
            FLS.CreateNeutralCamp_CS(camp, camp.GroupNumber);
        }

        NeutralMinionCamp camp2 = NeutralCampManager.FindCamp(CampId)!;

        List<AttackableUnit> listminion = camp2.Minions.ConvertAll(m => (AttackableUnit)m);
        List<AttackableUnit> listempty = new();
        if (listminion.Any())
        {
            if (Game.Map.MapData.aisquadlistmanager.GetAISquadListByiconcamp(Icon) != null && Game.Map.MapData.aisquadlistmanager.GetAISquadListBySquadname(SquadName) != null)
            {

                AISquadList aisquadlist = Game.Map.MapData.aisquadlistmanager.GetAISquadListByiconcamp(SquadName);
                if (aisquadlist._aisquad.Members == listempty)
                {
                    aisquadlist._aisquad.Members = listminion;
                }
                SquadId = aisquadlist.AISquadListID;
                return true;
            }
            else
            {
                var squad = new AISquadClass(SquadName, 99, listminion.First().Team);
                squad.AddUnitListtoSquad(listminion);
                AISquadList aisquadlist = new("", SquadName, squad);

                Game.Map.MapData.aisquadlistmanager.AddAISquadList(aisquadlist);
                SquadId = aisquadlist.AISquadListID;
                return true;
            }

        }
        else
        {
            if (Game.Map.MapData.aisquadlistmanager.GetAISquadListByiconcamp(Icon) != null && Game.Map.MapData.aisquadlistmanager.GetAISquadListBySquadname(SquadName) != null)
            {
                AISquadList aisquadlist = Game.Map.MapData.aisquadlistmanager.GetAISquadListByiconcamp(SquadName);
                SquadId = aisquadlist.AISquadListID;
                return true;
            }
            else
            {
                // Quand il n'y a pas de monstres, utiliser TeamId.TEAM_NEUTRAL par défaut
                var squad = new AISquadClass(SquadName, 99, TeamId.TEAM_NEUTRAL);
                squad.AddUnitListtoSquad(listempty);
                AISquadList aisquadlist = new("", SquadName, squad);

                Game.Map.MapData.aisquadlistmanager.AddAISquadList(aisquadlist);
                SquadId = aisquadlist.AISquadListID;
                return true;
            }
        }
        SquadId = default;
        return true;
    }
    /// <summary>Create an Encounter  from an encounter definition file. An encounter is used to spawn squads, the first encounterDefinition defined here defines the parameters, and add on encounter can add new monsterTypes.</summary>

    /// <param name="EncounterDefinition">Filename of an encounter definition</param>
    /// <param name="Count">How many of these encounters definitions should we insantiate?</param>
    /// <param name="Tags">What tags should we add to this encounter definition?</param>
    /// <param name="EncounterId">Gives a unique identifier to refer back to this encounter</param>
    ///  todo : VariantList class =  CreateEncounterFromDefinition(out .... VariantList Tags = "" need create ) 
    protected bool CreateEncounterFromDefinition(out int EncounterId, string EncounterDefinition, int Count = 1, string Tags = "")
    {
        if (Game.Map.MapData.EncounterList.ContainsKey(EncounterDefinition))
        {

            Encounter encounter1 = new(Game.Map.MapData.EncounterList[EncounterDefinition]);
            Game.Map.MapData.encounterManager.AddEncounter(encounter1);
            EncounterId = encounter1.EncounterID;
            return true;
        }

        EncounterId = default;
        return true;
    }
    /// <summary>Add On an Encounter Definition to an existing encounter. Add new monsterTypes to an existing encounter, ignores all other encounter parameters.</summary>

    /// <param name="EncounterDefinition">Filename of an encounter definition</param>
    /// <param name="Count">How many of these encounters definitions should we insantiate?</param>
    /// <param name="Tags">What tags should we add to this encounter definition?</param>
    /// <param name="EncounterId">Which encounter should we add onto?</param>
    ///  todo : VariantList class =  AddOnEncounter(.... VariantList Tags = ""  .... ) 
    protected bool AddOnEncounter(string EncounterDefinition, int Count = 1, string Tags = "", int EncounterId = 0)
    {
        return false;
    }
    /// <summary>Add On a Mutator Definition to an existing encounter. This mutator will apply to any future squads that are spawned from the encounter.</summary>

    /// <param name="EncounterId">Encounter Id of which encounter to apply the mutation to.</param>
    /// <param name="MutatorDefinition">Filename of the mutator definition to apply to the encounter</param>
    /// <param name="Tags">Which tags should this mutator apply to? If you input nothing it will apply to all.</param>
    /// <param name="MutationIndex">An Index into the MutatorDefinition telling us which specific stats to apply</param>
    ///  todo : VariantList class =  AddMutatorToEncounter(.... VariantList Tags = ""  .... ) 
    protected bool AddMutatorToEncounter(int EncounterId, string MutatorDefinition, string Tags = "", int MutationIndex = 0)
    {

        Encounter foundEncounter = Game.Map.MapData.encounterManager.GetEncounterByID(EncounterId);

        if (Game.Map.MapData.MutatorList.ContainsKey(MutatorDefinition))
        {
            Dictionary<int, MutatorDefinition> mutatorlisting = new();
            Mutator mutator = new(Game.Map.MapData.MutatorList[MutatorDefinition]);
            mutatorlisting[mutator.MutatorID] = mutator.MutatorDefinition;
            foundEncounter.Addmutator(mutatorlisting);
            Game.Map.MapData.mutatorManager.AddMutatorIndex(mutator);
            return true;
        }
        return true;
    }
    /// <summary>Update an Existing Mutator In an Existing Encounter, with a new mutationIndex. Give the mutator a new value to mutate on, must already exist in an existing encounter.</summary>

    /// <param name="EncounterId">Encounter Id of which encounter to update the mutation of</param>
    /// <param name="MutatorDefinition">Filename of the mutator definition of the type of mutation we are updating</param>
    /// <param name="MutationIndex">An Index into the MutatorDefinition telling us which specific stats to apply</param>

    protected bool UpdateMutator(int EncounterId, string MutatorDefinition, int MutationIndex = 0)
    {

        if (Game.Map.MapData.mutatorManager.GetMutatorByID(MutationIndex) != null)
        {
            Game.Map.MapData.encounterManager.GetEncounterByID(EncounterId).ReplaceMutator(MutationIndex, Game.Map.MapData.mutatorManager.GetMutatorByID(MutationIndex).MutatorDefinition);
        }
        return true;
    }
    /// <summary>Add a list of locations for squads to spawn at, tagged to effect only certain tags. Having no tags makes it affect all monsters. You can call ClearSquadSpawningLocations to erase these locations. Calling more than once adds more locations onto these.</summary>

    /// <param name="Tags">What tags should these positions affect? No tags, means they affect every monster.</param>
    /// <param name="EncounterId">Which encounter should we add to?</param>
    /// <param name="Position">List of named positions to spawn at. Names from the NamedLocationsManager</param>
    ///  todo : VariantList class =  AddLocationsToSquadSpawn(VariantList Tags = ""  .... VariantList Position = ) 
    protected bool AddLocationsToSquadSpawn(string Tags = "", int EncounterId = 0, string Position = "")
    {
        return false;
    }
    /// <summary>Removes all preset spawning locations from the target encounter. Erases everything added with AddLocationsToSquadSpawn.</summary>

    /// <param name="EncounterId">Which encounter should we reset locations on?</param>

    protected bool ClearSquadSpawningLocations(int EncounterId = 0)
    {
        return false;
    }
    /// <summary>Cleans up an encounter so that it can no longer be used to spawn squads. Ok to call on an encounter that is already deleted or does not exist.</summary>

    /// <param name="EncounterId">Which enoounter should we add cleanup?</param>

    protected bool DeleteEncounter(int EncounterId)
    {
        return false;
    }
    /// <summary>Tell the squad to push the specific lane. Adds the AI task to push a lane.</summary>

    /// <param name="SquadId">Which Squad should get this AI task</param>
    /// <param name="Lane">Which Lane should be pushed?</param>
    /// <param name="Reverse">Should this lane be pushed in the opposite direction as normal?</param>
    /// <param name="PatrolMode">If true, units will return to the first nav point after reaching the last nav point; if false, units will stop pathing after reaching the last nav point.</param>

    protected bool SquadPushLane(int SquadId = 0, int Lane = 0, bool Reverse = false, bool PatrolMode = false)
    {
        //todo : patrolmode
        var squad = Game.Map.MapData.aisquadlistmanager.GetAISquadListByID(SquadId);
        Lane test = (Lane)Lane;
        foreach (var m in squad._aisquad.Members)
        {
            if (m is Minion or LaneMinion)
            {
                (m as Minion).setupSquadPushLane(test, Reverse);
            }
            else if (m is Champion)
            {
                (m as Champion).setupSquadPushLane(test, Reverse);
            }

        }

        return true;
    }
    /// <summary>Tell the squad to defend a certain unit</summary>

    /// <param name="SquadId">Which Squad should get this AI task</param>
    /// <param name="Lane">Which Lane should be pushed?</param>

    protected bool SquadDefendUnit(int SquadId = 0, int Lane = 0)
    {
        return false;
    }
    /// <summary>Test to see if all members of the squad are still alive. Assumed dead if the squad does not exist.</summary>

    /// <param name="SquadId">Which Squad should we check?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if any members of the squad are alive; if False, returns SUCCESS if all members of the squad are dead, or if the squad doesnt exist</param>

    protected bool TestSquadIsAlive(int SquadId = 0, bool ReturnSuccessIf = true)
    {
        if (Game.Map.MapData.aisquadlistmanager.GetAISquadListByID(SquadId) != null)
        {
            if (ReturnSuccessIf)
            {
                return Game.Map.MapData.aisquadlistmanager.GetAISquadListByID(SquadId)._aisquad.Isonealive();
            }
            else
            {
                return !Game.Map.MapData.aisquadlistmanager.GetAISquadListByID(SquadId)._aisquad.Isonealive();
            }
        }
        if (ReturnSuccessIf)
        {
            return false;
        }
        else
        {
            return true;
        }


    }
    /// <summary>If the unit was spawned from a Squad Encounter, get the name that was given to that Squad. Squad Names are optional so this can often return an empty string, also multiple squads can have the same name.</summary>

    /// <param name="TargetUnit">Which Unit do we want to check for the squad name?</param>
    /// <param name="SquadName">Gives us the name of the squad of this unit.  Gives an empty string if the unit is gone, if it doesnt have a squad, or if the squad doesnt have a name</param>

    protected bool GetSquadNameOfUnit(out string SquadName, AttackableUnit TargetUnit)
    {
        SquadName = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMinion(TargetUnit as Minion).Squadname;
        return true;
    }
    /// <summary>Kills the target unit. Dead. Gives credit to the Killer. If no killer is supplied, the unit suicides.</summary>

    /// <param name="Unit">Which Unit do we want to kill?</param>
    /// <param name="Killer">Optionial: Who should get credit for killing this unit? If no unit is suplied the unit suicides.</param>

    protected bool KillUnit(AttackableUnit Unit, AttackableUnit Killer = default)
    {
        FLS.ApplyDamage(Killer as ObjAIBase, Unit, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)Killer);
        return true;
    }
    /// <summary>Kills all members of the squad and prevents any more members to spawn from that squad.</summary>

    /// <param name="SquadId">Which Squad do we want to kill?</param>

    protected bool KillSquad(int SquadId = 0)
    {
        var squad = Game.Map.MapData.aisquadlistmanager.GetAISquadListByID(SquadId);
        foreach (var m in squad._aisquad.Members)
        {
            // FLS.ApplyDamage(m, m, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
        }


        return true;
    }
    /// <summary>Kills all members of all squads with the input squad name and prevents any more members of those squads from spawning</summary>

    /// <param name="SquadName">Which Squad do we want to kill?</param>

    protected bool KillSquadsByName(string SquadName = "")
    {
        return false;
    }
    /// <summary>Returns a collection of units in squads with the target name. Always returns SUCCESS.</summary>

    /// <param name="SquadName">Which Squad Name do we want to look for?</param>
    /// <param name="Output">Destination Reference; holds a collection of units discovered</param>

    protected bool GetUnitsBySquadName(out IEnumerable<AttackableUnit> Output, string SquadName)
    {
        Output = Game.Map.MapData.aisquadlistmanager.GetAISquadListBySquadname(SquadName)._aisquad.Members;
        return true;
    }
    /// <summary>Stops any further units from spawning from this squad. Leaves all currently spawned squad members alone, but stops it from spawning anymore. Does nothing if its already done spawning everything.</summary>

    /// <param name="SquadId">Which Squad do we want to stop from spawning?</param>

    protected bool StopSquadFromSpawning(int SquadId)
    {
        return false;
    }

    /// <summary>Plays one of the Start Game announcements. Can play one of five Start Game annoucements (use message number values one through five).</summary>

    /// <param name="MessageNumber">Plays StartGame announcement number one through five</param>

    protected bool Announcement_OnStartGame(int MessageNumber = 1, bool successif = true)
    {
        gameannouncement.AnnounceStartGameMessage(MessageNumber, Game.Map.Id);
        return true;
    }
    /// <summary>Announces that a point has become neutral</summary>

    /// <param name="Unit">Unit on the team that neutralized the point.</param>
    /// <param name="CapturePoint">The index of the capture point that went neutral</param>

    protected bool Announcement_OnCapturePointNeutralized(AttackableUnit Unit, int CapturePoint = 0)
    {
        gameannouncement.AnnounceCapturePointNeutralized(Unit as LaneTurret, GetCharFromInt(CapturePoint));
        return true;
    }




    /// <summary>Announces that a point has become captured by a team</summary>

    /// <param name="TargetUnit">Point that was captured.</param>
    /// <param name="Unit">Unit on the team that captured the point.</param>
    /// <param name="CapturePoint">The index of the capture point that was captured</param>

    protected bool Announcement_OnCapturePointCaptured(AttackableUnit TargetUnit, AttackableUnit Unit, int CapturePoint = 0)
    {
        if (Unit != null)
        {
            gameannouncement.AnnounceCapturePointCaptured(TargetUnit as LaneTurret, GetCharFromInt(CapturePoint), Unit as Champion);

        }
        else
        {
            gameannouncement.AnnounceCapturePointCaptured(TargetUnit as LaneTurret, GetCharFromInt(CapturePoint));
        }
        return true;
    }
    /// <summary>Announces that a specific victory point threshold has been reached by a team. Three threshold announcements currently exist, with 1 being the highest threshold and 4 being the lowest.  Values outside that range will default to threshold 1.</summary>

    /// <param name="Unit">Unit on the team that reached the threshold.</param>
    /// <param name="Threshold">The index of the threshold announcement.  Values outside 1-4 will default to 1.</param>

    protected bool Announcement_OnVictoryPointThreshold(AttackableUnit Unit, int Threshold = 1)
    {
        gameannouncement.AnnounceVictoryPointThreshold(Unit as LaneTurret, Threshold);
        return true;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for PAR References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool EqualPARType(PrimaryAbilityResourceType LeftHandSide, PrimaryAbilityResourceType RightHandSide)
    {
        return LeftHandSide == RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is equal. This version is for PAR References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool NotEqualPARType(PrimaryAbilityResourceType LeftHandSide, PrimaryAbilityResourceType RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is equal. This version is for Unit type References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool NotEqualUnitType(UnitType LeftHandSide, UnitType RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for Creature References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool EqualCreatureType(CreatureType LeftHandSide, CreatureType RightHandSide)
    {
        return LeftHandSide == RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is equal. This version is for Creature References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool NotEqualCreatureType(CreatureType LeftHandSide, CreatureType RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for Creature References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool EqualSpellbookType(SpellbookType LeftHandSide, SpellbookType RightHandSide)
    {
        return LeftHandSide == RightHandSide;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is equal. This version is for Creature References.</summary>
    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    protected bool NotEqualSpellbookType(SpellbookType LeftHandSide, SpellbookType RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }

    /// <summary>Issue Use item Order</summary>

    /// <param name="ItemID">Item ID</param>
    /// <param name="TargetUnit">Target</param>

    protected bool IssueUseItemOrder(int ItemID, AttackableUnit TargetUnit)
    {
        try
        {
            if (TargetUnit == default || TargetUnit == null)
            {
                TargetUnit = Owner.TargetUnit;
                if (TargetUnit == null)
                {
                    TargetUnit = Owner;
                }
            }

            Owner.SetSpell(TargetUnit.CharData.HeroUseSpell, (byte)SpellSlotType.UseSpellSlot, true, isitemuse: true);

            var s = Owner.Spells[(short)SpellSlotType.UseSpellSlot + Owner.ItemInventory.GetItemSlotbyItemId(ItemID)];

            //todo fix this part 
            if (s == null)
                return false;
            if (s.TryCast(TargetUnit, Owner.Position3D, TargetUnit.Position3D))
            {
                Owner.ItemInventory.ClearUndoHistory();
                //TODO: Move to Spell.TryCast?
                if (s.IsItem)
                {
                    var item = s.Caster.ItemInventory.GetItem(s.SpellName);
                    if (item != null && item.ItemData.Consumed)
                    {
                        var inventory = Owner.ItemInventory;
                        inventory.RemoveItem(inventory.GetItemSlot(item));
                    }
                }
                return false;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }




    }


    /// <summary>Returns the number of instances of a given buff, regardless of caster. Buffs that do not exist will return 0, but empty buff names will cause the node to return FAILURE.</summary>

    /// <param name="TargetUnit">Unit to poll.</param>
    /// <param name="BuffName">Name of the buff to look for.</param>
    /// <param name="Output">Destination reference; contains the number of stacks of the polled buff</param>

    protected bool GetUnitBuffCount(out int Output, AttackableUnit TargetUnit, string BuffName = "")
    {

        Output = TargetUnit.Buffs.Count(BuffName);
        return true;



    }
    /// <summary>Clears all buffs with the same name on the unit</summary>

    /// <param name="Unit">Unit with the buff on it</param>
    /// <param name="BuffName">Buff name</param>

    protected bool SpellBuffClear(AttackableUnit Unit, string BuffName)
    {
        Unit.RemoveBuff(BuffName);
        return true;
    }


    /// <summary>Permanently modifies a target unit's vision bubble. This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.</summary>

    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>

    protected bool IncPermanentPercentBubbleRadiusMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }

    /// <summary>Permanently modifies a target unit's AP. This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.</summary>

    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>

    protected bool IncPermanentFlatMagicDamageMod(AttackableUnit Unit, float Delta)
    {
        return false;
    }
    /// <summary>Permanently modifies a target unit's respawn time modifier. This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.</summary>

    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>

    protected bool IncPermanentPercentRespawnTimeMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.RespawnTimer += Unit.Stats.RespawnTimer * Delta / 100;
        return true;
    }
    /// <summary>Permanently modifies a target unit's flat respawn time modifier. This modifies the permanent CharInter, so be sure to pair it with a decrement of equal value later.</summary>

    /// <param name="Unit">Unit to modify.</param>
    /// <param name="Delta">Delta</param>

    protected bool IncPermanentFlatRespawnTimeMod(AttackableUnit Unit, float Delta)
    {
        Unit.Stats.RespawnTimer += Delta;
        return true;
    }
    /// <summary>Permanently modifies the global wave respawn time.</summary>

    /// <param name="Delta">Delta</param>
    /// <param name="Team">Team ID to be affected</param>

    protected bool AdjustWaveSpawnTime(float Delta, TeamId Team)
    {
        //todo : 
        return true;
    }


    /// <summary>Heals the target unit. Unlike the Block Builder, this node does NOT properly track healing for the purposes of Death Recap.</summary>

    /// <param name="TargetUnit">Unit to heal.</param>
    /// <param name="HealerUnit">Unit that is doing the healing.</param>
    /// <param name="Delta">Amount to heal</param>

    protected bool IncHealth(AttackableUnit TargetUnit, AttackableUnit HealerUnit, float Delta)
    {
        return false;
    }
    /// <summary>Returns position specified by name.</summary>

    /// <param name="Input">Location to retrieve.</param>
    /// <param name="Output">World position reference</param>

    protected bool GetWorldLocationByName(out Vector3 Output, string Input = "")
    {
        if (Game.Map.MapData.LocationList.ContainsKey(Input))
        {

            Output = Game.Map.MapData.LocationList[Input].Position3D;
            return true;
        }
        else
        {
            Output = default;
            return false;
        }

    }
    /// <summary>Returns position specified by name.</summary>

    /// <param name="Input">Position to check.</param>
    /// <param name="Output">World position reference</param>

    protected bool GetLocationNearPosition(out Vector3 Output, Vector3 Input)
    {
        Output = default;
        return false;
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position.</summary>

    /// <param name="Input">New Name of Position to add</param>
    /// <param name="Vector">New World position to add</param>

    protected bool AddNamedLocation(string Input, Vector3 Vector)
    {
        return false;
    }
    /// <summary>Finds a level prop by its name.</summary>

    /// <param name="PropName"></param>
    /// <param name="Output"></param>

    protected bool GetPropByName(out GameObject Output, string PropName = "")
    {
        GameObject _output = default;
        _output = LevelProp.ListLevelprop.FirstOrDefault(x => x.Name == PropName);
        if (_output != null)
        {
            Output = _output;
            return true;
        }
        else
        {
            Output = default;
            return true;
        }

    }


    public string Extractmodelname(string input)
    {
        var _input = input.TrimEnd("0123456789".ToCharArray());
        Match match = Regex.Match(_input, @"_(\w+)");

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        // Si aucune correspondance n'est trouv�e, retourne la cha�ne d'entr�e
        return input;
    }

    /// <summary></summary>

    /// <param name="Position"></param>
    /// <param name="PropName"></param>
    /// <param name="Team">Team to spawn prop on.</param>
    /// <param name="Output">The prop that you just spawned</param>

    protected bool SpawnAILevelProp(out AttackableUnit Output, Vector3 Position, string PropName, TeamId Team = TeamId.TEAM_ORDER)
    {
        Output = default;
        return false;
    }
    /// <summary></summary>

    /// <param name="Unit">The Object you want to play the animation on</param>
    /// <param name="Animation">The Name of the Animation to play</param>
    /// <param name="Lock">Should the anim be anim locked</param>
    /// <param name="Scale">What should the scaletime be</param>
    /// <param name="Loop">Should the anim be looped</param>
    /// <param name="Blend">Should the anim blend from the last one</param>

    protected bool PlayAnimationOnUnit(AttackableUnit Unit, string Animation, bool Lock = false, float Scale = 0, bool Loop = false, bool Blend = false)
    {
        return false;
    }
    /// <summary></summary>

    /// <param name="LevelProp">The Level Prop Object you want to play the animation on</param>
    /// <param name="Animation">The Name of the Animation to play</param>
    /// <param name="Loop">Should this animation loop when its done?</param>
    /// <param name="Duration">If this value is greater than 0, than it time syncs this animation across all clients to play for this exact duration</param>

    protected bool PlayAnimationOnProp(GameObject LevelProp, string Animation, bool Loop = true, float Duration = 0.0f)
    {
        //todo loop 
        if (LevelProp != null)
        {
            if (LevelProp.Name.Contains("Odin_SoG") && Animation != "Open")
            {
                apimap.NotifyPropAnimation(LevelProp as LevelProp, Animation, (AnimationFlags)2, Duration, false);
            }
            else
            {
                apimap.NotifyPropAnimation(LevelProp as LevelProp, Animation, (AnimationFlags)1, Duration, false);
            }
            return true;
        }
        else
        {
            return true;
        }
    }
    /// <summary></summary>

    /// <param name="LevelProp">The Level Prop Object you want to polymorph</param>
    /// <param name="SkinName">The Name of the character to change to</param>

    protected bool PolymorphProp(GameObject LevelProp, string SkinName)
    {
        return false;
    }
    /// <summary>Attach a capture point status, to the the flex values of the idle particles of target level prop. Only does anything if the target level prop has idle particle with flex values</summary>

    /// <param name="LevelProp">The Level Prop Object you want to sync the partickles of</param>
    /// <param name="FlexValueId">The ID value that the artist is using for these flex values</param>
    /// <param name="CapturePoint">The capture point to sync to the particles</param>

    protected bool AttachCapturePointToIdleParticles(GameObject LevelProp, int FlexValueId = 0, int CapturePoint = 0)
    {
        //todo : 
        AttachCapturePointToIdleParticlesNotify(LevelProp as ObjAIBase, (byte)FlexValueId, (byte)CapturePoint, (uint)(FXFlags)304);
        return true;
    }
    /// <summary>Attach the idle particles to be based on the PAR value of the unit</summary>

    /// <param name="LevelProp">The  Object you want to sync the partickles of</param>
    /// <param name="FlexValueId">The ID value that the artist is using for these flex values</param>

    protected bool AttachPARToIdleParticles(GameObject LevelProp, int FlexValueId = 0)
    {
        //todo:
        AttachCapturePointToIdleParticlesNotify(LevelProp as ObjAIBase, (byte)FlexValueId, 0, (uint)(FXFlags)304);
        return true;
    }
    /// <summary>Display floating text over unit's head. Displays only for the team specified</summary>

    /// <param name="TargetObject">Unit to show floating text over.</param>
    /// <param name="String">The localized string to display.</param>
    /// <param name="Team">Which team to send to? Set To neutral to send to all teams.</param>
    /// <param name="Param1">Param to fill into localized string. Put @IntParam1@ in the localization to display this value.</param>

    protected bool PlayFloatingTextOnUnitForTeam(AttackableUnit TargetObject, string String = "", TeamId Team = TeamId.TEAM_ORDER, int Param1 = 0)
    {
        //todo : 
        //replace this $"+{(int)Param1} Points"  by  String example :  Put @IntParam1@ in the localization to display this value :  game_floating_friendly_kill
        FloatingTextData floatTextData = new(TargetObject, $"+{Param1} Points", FloatTextType.Score, 1073741833);
        if (TargetObject != null)
        {
            DisplayFloatingTextNotify(floatTextData, Team, (int)TargetObject.NetId);
        }

        return true;
    }
    /// <summary>Gets unit id that is our call for help target. 0 means we don't have one. This version is for id.</summary>

    /// <param name="Output">Destination id.</param>

    protected bool GetUnitCallForHelpTargetId(out int Output)
    {
        Output = default;
        return false;
    }
    /// <summary>Gets unit that is our call for help target. This version is for AttackableUnit References.</summary>

    /// <param name="Output">Destination reference</param>

    protected bool GetUnitCallForHelpTargetUnit(out AttackableUnit Output)
    {
        Output = default;
        return false;
    }
    /// <summary>Test if unit has a call for help target. True if it does. False if not</summary>

    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>

    protected bool TestHaveCallForHelpTarget(bool ReturnSuccessIf = true)
    {
        return false;
    }
    /// <summary>Gets unit that is our call for help caller. This version is for AttackableUnit References.</summary>

    /// <param name="Output">Destination reference</param>

    protected bool GetUnitCallForHelpCallerUnit(out AttackableUnit Output)
    {
        Output = default;
        return false;
    }
    /// <summary>Gets the target acquisition range of the caller. This version returns a float.</summary>

    /// <param name="Output">Destination value.</param>

    protected bool GetUnitTargetAcquistionRange(out float Output)
    {
        Output = Owner.Stats.AcquisitionRange.Total;
        return true;
    }
    /// <summary>Gets the difficulty level for this entity. This version returns a enum.</summary>

    /// <param name="Output">Destination value.</param>
    /// todo=  create an class AIDifficultyType    === protected   bool GetEntityDifficultyLevel(out AIDifficultyType Output = , )
    protected bool GetEntityDifficultyLevel(out string Output)
    {
        Output = default;
        return false;
    }
    /// <summary>Gets the difficulty level for this entity. This version returns a enum.</summary>

    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>
    /// <param name="AIDifficultyType">Difficulty level to test against.</param>

    protected bool TestEntityDifficultyLevel(bool ReturnSuccessIf = true, EntityDiffcultyType AIDifficultyType = EntityDiffcultyType.DIFFICULTY_NEWBIE)
    {
        // Vérifier que l'owner existe et est un Champion
        if (Owner == null)
        {
            return false;
        }

        if (Owner is not Champion champion)
        {
            return false;
        }

        bool difficultyMatches = champion.AIDifficulty == AIDifficultyType;

        // Si la difficulté correspond ET qu'on veut retourner true en cas de succès
        // OU si la difficulté ne correspond PAS ET qu'on veut retourner false en cas d'échec
        return difficultyMatches == ReturnSuccessIf;
    }
    /// <summary>Returns the closest unit target area. Returns SUCCESS if a target is found</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="IgnoreUnit">Handle of the unit that should be ignored in this check.</param>
    /// <param name="UseVisibilityCheck">If True, uses visibility checks</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    /// <param name="Output">Destination Reference; Target found</param>

    protected bool GetUnitAIClosestTargetInArea(out AttackableUnit Output, AttackableUnit Unit, AttackableUnit IgnoreUnit, bool UseVisibilityCheck = true, Vector3 TargetLocation = default(Vector3), float Radius = 0, SpellDataFlags SpellFlags = SpellDataFlags.AlwaysSelf)
    {
        Output = null;

        if (Unit == null || Radius <= 0)
        {
            return false;
        }

        IEnumerable<AttackableUnit> closestUnits = default;

        if (UseVisibilityCheck)
        {
            closestUnits = FCS.GetClosestVisibleUnitsInArea(Unit, TargetLocation, Radius, SpellFlags, 1, unittoignore: IgnoreUnit);
        }
        else
        {
            closestUnits = FCS.GetClosestUnitsInArea(Unit, TargetLocation, Radius, SpellFlags, 1, unitToIgnore: IgnoreUnit);
        }

        if (closestUnits.Any())
        {
            Output = closestUnits.First();
            return true;
        }
        return false;
    }
    /// <summary>Returns the closest unit target area. Returns SUCCESS if a target is found.</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="IgnoreUnit">Handle of the unit that should be ignored in this check.</param>
    /// <param name="UseVisibilityCheck">If True, uses visibility checks</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="HealthThreshold">Health threshold</param>
    /// <param name="AboveThreshold">If True, find above threshold. If false finds below</param>
    /// <param name="UseRatio">If True, uses ratio. If false uses flat HP</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    /// <param name="Output">Destination Reference; Target found</param>

    protected bool GetUnitAILowestHPTargetInArea(out AttackableUnit Output, AttackableUnit Unit, AttackableUnit IgnoreUnit, bool UseVisibilityCheck = true, Vector3 TargetLocation = default(Vector3), float Radius = 0, float HealthThreshold = 0, bool AboveThreshold = false, bool UseRatio = true, SpellDataFlags SpellFlags = SpellDataFlags.AlwaysSelf)
    {
        try
        {
            Output = null;

            if (Unit == null || Radius <= 0)
            {
                return false;
            }

            var allUnits = FCS.GetUnitsInArea(Unit, TargetLocation, Radius, SpellFlags);
            var filteredUnits = allUnits
                .Where(unit => unit != IgnoreUnit)
                .Where(unit => unit != null)
                .Where(unit =>
                {
                    if (HealthThreshold <= 0) return true;

                    float healthValue = UseRatio ? unit.Stats.CurrentHealth : unit.Stats.HealthPoints.Total;

                    if (AboveThreshold)
                        return healthValue >= HealthThreshold;
                    else
                        return healthValue <= HealthThreshold;
                })
                .OrderBy(unit => UseRatio ? unit.Stats.CurrentHealth : unit.Stats.HealthPoints.Total)
                .ToList();

            if (filteredUnits.Any())
            {
                Output = filteredUnits.First();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            Output = null;
            return false;
        }
    }
    /// <summary>Returns the closest unit target area. Returns SUCCESS if a target is found</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="Collection">What collection the unit must be inside to be selected</param>
    /// <param name="Output">Destination Reference; Target found</param>

    protected bool GetUnitAIClosestTargetInCollection(out AttackableUnit Output, AttackableUnit Unit, IEnumerable<AttackableUnit> Collection)
    {
        Output = default;
        return false;
    }

    /// <summary>Returns the closest unit target area. Returns SUCCESS if a target is found</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="Collection">What collection the unit must be inside to be selected</param>
    /// <param name="Output">Destination Reference; Target found</param>

    protected bool FindClosestTargetwithBuffBT(out AttackableUnit Output, AttackableUnit Unit, IEnumerable<AttackableUnit> Collection)
    {
        Output = default;
        return false;
    }
    /// <summary>Gets ref to mission. This version returns a ref to a mission.</summary>

    /// <param name="Output">Destination reference.</param>
    /// todo = AIMission class === protected   bool GetAIMissionSelf(out AIMission Output = , ) and other 

    protected bool GetAIMissionSelf(out AIMissionClass Output)
    {
        try
        {

            Output = (this as AImission_bt).missionowner;
            return true;
        }
        catch (Exception e)
        {
            Output = null;
            return false;
        }
    }
    /// <summary>Gets ref to squad. This version returns a ref to a squad.</summary>

    /// <param name="AIMission">Unit to show floating text over.</param>
    /// <param name="Output">Ref to the squad.</param>

    protected bool GetAIMissionSquad(out AISquadClass Output, AIMissionClass AIMission)
    {
        try
        {
            Output = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMission(AIMission)._aisquad;
            return true;
        }
        catch (Exception e)
        {
            Output = null;
            return false;
        }
    }
    /// <summary>Assign an AI Mission to a squad.</summary>

    /// <param name="AISquad"> AISquadClass AISquad  Squad we want to assign the mission to.</param>
    /// <param name="AIMission">AIMissionClass The mission we want to assign.</param>

    /// <summary>
    /// Assigns an AI mission to a squad with automatic behavior tree execution
    /// </summary>
    protected bool AssignAIMission(AISquadClass AISquad, AIMissionClass AIMission)
    {
        try
        {
            if (AISquad == null || AIMission == null)
            {
                return false;
            }

            AISquad.AssignedMission = AIMission;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    /// <summary>
    /// Executes the behavior tree of a mission for a squad
    /// </summary>
    private void ExecuteMissionBehaviorTree(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            if (!_missionBehaviorTrees.ContainsKey(mission) || !_missionBehaviorTrees[mission].ContainsKey("BehaviorTreeInstance"))
            {
                return;
            }

            var behaviorTreeInstance = _missionBehaviorTrees[mission]["BehaviorTreeInstance"];
            var behaviorTreeTag = _missionBehaviorTrees[mission].ContainsKey("BehaviorTreeTag") ? _missionBehaviorTrees[mission]["BehaviorTreeTag"] as string : "Unknown";

            // Use reflection to call the ExecuteForSquad method if it exists
            var executeMethod = behaviorTreeInstance.GetType().GetMethod("ExecuteForSquad");
            if (executeMethod != null)
            {
                bool result = (bool)executeMethod.Invoke(behaviorTreeInstance, new object[] { squad, mission });
            }
            else
            {
                // Fallback to simple Execute method
                var executeSimpleMethod = behaviorTreeInstance.GetType().GetMethod("Execute");
                if (executeSimpleMethod != null)
                {
                    bool result = (bool)executeSimpleMethod.Invoke(behaviorTreeInstance, null);
                }
            }
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
        }
    }

    /// <summary>
    /// Notifie les membres du squad de la nouvelle mission
    /// </summary>
    private void NotifySquadMembersOfNewMission(AISquadClass squad, AIMissionClass mission)
    {
        try
        {
            if (squad.Members == null || !squad.Members.Any())
            {
                return;
            }

            foreach (var member in squad.Members)
            {
                if (member != null)
                {
                    // Update the unit's mission (if the property exists)
                    // member.CurrentMission = mission; // Commented because the property doesn't exist

                    // Optional: Send a message to the unit's behavior tree
                }
            }
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
        }
    }


    /// <summary>
    /// Retire la mission d'un AISquadClass
    /// </summary>
    protected bool UnassignAIMission(AISquadClass AISquad)
    {
        try
        {
            if (AISquad == null)
            {
                return false;
            }

            AISquad.AssignedMission = null;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }



    /// <summary>
    /// Executes the behavior tree of a mission for a champion
    /// </summary>
    private void ExecuteMissionBehaviourTree(AIMissionClass mission, Champion champion)
    {
        try
        {
            if (mission?.BehaviourMissionAI == null)
            {
                return;
            }

            // ✅ Configure the behavior tree with the champion
            mission.BehaviourMissionAI.SetOwner(champion);

            // ✅ Execute the behavior tree
            mission.BehaviourMissionAI.Update();

        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
        }
    }

    /// <summary>Test to see if team has visibility of another unit. If either unit does not exist, then they are not visible; use ReturnSuccessIf to invert the test.</summary>

    /// <param name="TargetUnit">Is this unit visible to the viewer team?</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the target is visible to the team; if False, returns SUCCESS if unit is not visible to the team or does not exist.</param>

    protected bool TestAIManagerHasVisibility(AttackableUnit TargetUnit, bool ReturnSuccessIf = true)
    {
        return false;
    }

    /// <summary>Gets ref to a collection of members of a squad. This version returns ref to a collection of members of a squad.</summary>

    /// <param name="AISquad">The Squad we want the members of.</param>
    /// <param name="Output">Ref to collection of units.</param>

    protected bool GetAISquadMembers(out IEnumerable<AttackableUnit> Output, AISquad AISquad)
    {
        Output = default;
        return false;
    }
    /// <summary>Gets ref to a collection of members of a squad on a mission. This version returns ref to a collection of members of a squad.</summary>

    /// <param name="AIMission">The Mission we want the members of.</param>
    /// <param name="Output">Ref to collection of units.</param>

    protected bool GetAIMissionSquadMembers(out IEnumerable<AttackableUnit> Output, AIMissionClass AIMission)
    {
        try
        {
            if (AIMission == null)
            {
                Output = new List<AttackableUnit>();
                return false;
            }


            var squad = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMission(AIMission)._aisquad;

            if (squad != null)
            {
                Output = squad.Members;
                return true;
            }

            // No squad found for this mission
            Output = new List<AttackableUnit>();
            return false;
        }
        catch (Exception e)
        {

            Output = new List<AttackableUnit>();
            return false;
        }
    }
    /// <summary>Create an AI Task. This version returns a ref to the created task.</summary>

    /// <param name="AITaskTopic">Task to create.</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="LaneID">Lane ID</param>
    /// <param name="Output">Ref to the created task.</param>

    /// <summary>
    /// Creates an AI task dynamically using .DAT files and behavior trees
    /// </summary>
    protected bool CreateAITask(out AITask Output, AITaskTopicType AITaskTopic, AttackableUnit TargetUnit, Vector3 TargetLocation, int LaneID = 0)
    {
        try
        {
            Output = new AITask(AITaskTopic, TargetUnit, TargetLocation, LaneID);
            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }


    /// <summary>Set the status of an AI Task.</summary>

    /// <param name="AITask">The task we want to set status for.</param>
    /// <param name="LogicResult">Status we want set on a task.</param>

    protected bool SetAITaskStatus(AITask AITask, LogicResultType LogicResult)
    {
        return false;
    }
    /// <summary>Assign an AI Task to a unit.</summary>

    /// <param name="Unit">Unit we want to assign the task to.</param>
    /// <param name="AITask">The task we want to assign.</param>

    /// <summary>
    /// Assigns an AI task to a unit with automatic behavior tree execution
    /// </summary>
    protected bool AssignAITask(AttackableUnit Unit, AITask AITask)
    {
        try
        {
            if (Unit == null || AITask == null)
            {
                return false;
            }

            if (Unit is Champion champion)
            {

                Unit.CurrentTask = AITask;
                return true;
            }
            else
            {
                Unit.CurrentTask = AITask;
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }



    /// <summary>Unassign an AI Task to a unit.</summary>

    /// <param name="Unit">Unit that should unassign a task.</param>

    protected bool UnassignAITask(AttackableUnit Unit)
    {
        try
        {
            if (Unit is Champion champion && _assignedTasks.ContainsKey(champion.Model))
            {
                _assignedTasks[champion.Model].Clear();
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }
    /// <summary>Get a task from a unit. This version returns a ref to the task.</summary>

    /// <param name="Unit">Position to check.</param>
    /// <param name="Output">Destination reference.</param>

    protected bool GetAITaskFromEntity(out AITask Output, AttackableUnit Unit)
    {
        try
        {
            Output = Unit.CurrentTask;
            return true;
        }
        catch (Exception)
        {
            Output = default;
            return false;
        }
    }
    /// <summary>Get a unit from a task. This version returns a ref to the unit.</summary>

    /// <param name="AITask">Task we want to get the unit from.</param>
    /// <param name="Output">Destination reference.</param>

    protected bool GetAIEntityFromTask(out AttackableUnit Output, AITask AITask)
    {
        Output = default;
        return false;
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position.</summary>

    /// <param name="AITask">Task we want to get the unit from.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>

    protected bool TestAITaskIsAssigned(AITask AITask, bool ReturnSuccessIf = true)
    {
        return false;
    }
    /// <summary>Get topic from a Task.</summary>

    /// <param name="AITask">The Task we want get topic from</param>
    /// <param name="Output">Topic from Task.</param>

    protected bool GetAITaskTopic(out AITaskTopicType Output, AITask AITask)
    {
        try
        {
            if (AITask != null)
            {
                Output = AITask.Topic;
                return true;
            }

            Output = default;
            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }
    /// <summary>Get target from a Task.</summary>

    /// <param name="AITask">The Task we want get target from</param>
    /// <param name="Output">Target from Task.</param>

    protected bool GetAITaskTarget(out AttackableUnit Output, AITask AITask)
    {
        try
        {
            if (AITask != null)
            {
                Output = AITask.TargetUnit;
                return true;
            }

            Output = default;
            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }
    /// <summary>Get position from a Task.</summary>

    /// <param name="AITask">The Task we want get positiom from</param>
    /// <param name="Output">Position from Task.</param>

    protected bool GetAITaskPosition(out Vector3 Output, AITask AITask)
    {
        try
        {
            if (AITask != null)
            {
                Output = AITask.TargetLocation;
                return true;
            }

            Output = default;
            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }
    /// <summary>Get Index from a Task.</summary>

    /// <param name="AITask">The Task we want get index from</param>
    /// <param name="Output">Index from Task.</param>

    protected bool GetAITaskIndex(out int Output, AITask AITask)
    {
        try
        {
            // Search for the task in all assigned lists
            foreach (var kvp in _assignedTasks)
            {
                var index = kvp.Value.IndexOf(AITask);
                if (index != -1)
                {
                    Output = index;
                    return true;
                }
            }

            Output = -1; // Task not found
            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = -1;
            return false;
        }
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position.</summary>

    /// <param name="Unit">Position to check.</param>
    /// <param name="AITaskTopic">Task we want to get the unit from.</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="LaneID">Lane ID</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>
    protected bool TestAIEntityHasTask(AttackableUnit Unit, AITaskTopicType AITaskTopic, AttackableUnit TargetUnit, Vector3 TargetLocation, int LaneID = 0, bool ReturnSuccessIf = true)
    {
        var tasktotest = Unit.CurrentTask;
        if (tasktotest != null)
        {
            if (ReturnSuccessIf ==
                (
                tasktotest.Topic == AITaskTopic
                && (tasktotest.TargetUnit == TargetUnit || TargetUnit == default)
                && (tasktotest.TargetLocation == TargetLocation || TargetLocation == default)
                && (tasktotest.LaneID == LaneID || LaneID == default)
                )
              )

            {
                return true;
            }
        }
        return false;

    }


    /// <summary>Test if this is the firs time AI has run.</summary>

    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>

    protected bool TestAIFirstTime(bool ReturnSuccessIf = true)
    {
        // This method must return true the first time it is called
        // to allow initialization of the behavior tree
        // We use a static flag for each instance

        if (!_firstTimeExecuted)
        {
            _firstTimeExecuted = true;
            return ReturnSuccessIf;
        }

        return !ReturnSuccessIf; // Returns the inverse after the first time
    }

    /// <summary>Get a task from a unit. This version returns a ref to the task.</summary>

    /// <param name="Output">Destination reference.</param>

    protected bool GetAIManagerEntities(out IEnumerable<AttackableUnit> Output)
    {

        Output = AIEntitiesAssociated;
        return true;
    }
    /// <summary>Get capturepointteam with their ID </summary>
    /// only used in minionbt 
    /// <param name="Output">Destination reference.</param>
    protected bool GetCapturePointOwnerBT(out TeamId Output, int id)
    {


        Output = Game.ObjectManager.GetAllCapturePoint()[id].Team;
        //  DebugAction($"{Game.ObjectManager.GetAllCapturePoint()[id].Name} {Game.ObjectManager.GetAllCapturePoint()[id].Team} {Game.ObjectManager.GetAllCapturePoint()[id].Position} ");
        return true;
    }


    /// <summary>Get a task from a unit. This version returns a ref to the task.</summary>

    /// <param name="Output">Destination reference.</param>

    protected bool GetAIManagerAvailableEntities(out IEnumerable<AttackableUnit> Output)
    {
        // Retourner tous les champions disponibles pour l'AIManager
        var champions = Game.ObjectManager.GetAllChampions();
        Output = champions.Cast<AttackableUnit>();
        return true;
    }

    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="MaxMemberNum">Position to check.</param>
    /// <param name="Output">Task we want to get the unit from.</param>


    protected bool CreateAISquad(out AISquadClass Output, string squadString = "", int MaxMemberNum = 0)
    {
        try
        {
            // Créer un nouveau squad avec SquadId.Empty par défaut
            Output = new AISquadClass(squadString, MaxMemberNum, this.aimanagerTeam);

            AISquadList aisquadlist = new("", squadString, Output);

            Game.Map.MapData.aisquadlistmanager.AddAISquadList(aisquadlist);
            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }







    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="AISquad">Position to check.</param>

    protected bool DeleteAISquad(AISquad AISquad)
    {
        return false;
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position.</summary>

    /// <param name="Output">Task we want to get the unit from.</param>

    protected bool GetAISquadAry(out AISquadCollection Output)
    {

        Output = default;
        return false;

    }
    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="Unit">Position to check.</param>
    /// <param name="Output">Task we want to get the unit from.</param>

    protected bool GetAISquadFromEntity(out AISquadClass Output, AttackableUnit Unit)
    {
        try
        {

            if (Game.Map.MapData.aisquadlistmanager.GetAISquadListByMinion(Unit) != null)
            {

                var squad = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMinion(Unit)._aisquad;
                if (squad != null)
                {
                    Output = squad;

                    return true;
                }
                Output = default;
                return false;

            }
            else
            {
                Output = default;

                return false;
            }
        }
        catch (Exception e)
        {

            Output = default;
            return false;
        }
    }
    /// <summary>Test if this is the firs time AI has run.</summary>

    /// <param name="Unit">Position to check.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>

    protected bool TestAIEntityHasSquad(AttackableUnit Unit, bool ReturnSuccessIf = true)
    {
        try
        {
            if (Unit == null)
            {
                return !ReturnSuccessIf;
            }

            // ✅ Utiliser le manager pour vérifier si l'unité est dans un squad
            var squadClass = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMinion(Unit)._aisquad;

            if (squadClass != null)
            {

                return ReturnSuccessIf;
            }
            else
            {

                return !ReturnSuccessIf;
            }
        }
        catch (Exception e)
        {

            return !ReturnSuccessIf;
        }
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="Unit">Position to check.</param>
    /// <param name="AISquad">AISquadClass  Task we want to get the unit from.</param>

    protected bool AddAIEntityToSquad(AttackableUnit Unit, AISquadClass AISquad)
    {
        try
        {

            AISquadList aisquadlist = Game.Map.MapData.aisquadlistmanager.GetAISquadListByMinion(Unit);

            if (aisquadlist != null && aisquadlist._aisquad != AISquad)
            {

                aisquadlist._aisquad.Members.Remove(Unit);
                Unit.Waypoints.Clear();

            }




            if (AISquad.Members.Contains(Unit))
            {

                return false;
            }

            // Vérifier si le squad n'est pas plein
            if (AISquad.MaxMemberNum > 0 && AISquad.Members.Count >= AISquad.MaxMemberNum)
            {
                return false;
            }


            AISquad.Members.Add(Unit);


            if (AISquad.AssignedMission.LaneID != null)
            {

                //Unit.Waypoints.Add(Unit.Position);
                Unit.setupSquadPushLane((Lane)AISquad.AssignedMission.LaneID, Unit.Team == TeamId.TEAM_ORDER);


            }
            else
            {
                Unit.Waypoints.Add(Unit.Position);
            }



            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }


    /// <summary>Remove an entity from a squad. This version works with AISquadList.</summary>

    /// <param name="Unit">Unit to remove from the squad.</param>
    /// <param name="AISquad">AISquadList to remove the unit from.</param>

    /// <summary>Remove an entity from a squad. This version works with AISquadClass.</summary>
    /// <param name="Unit">Unit to remove from the squad.</param>
    /// <param name="AISquad">AISquadClass to remove the unit from.</param>
    protected bool RemoveAIEntityFromSquad(AttackableUnit Unit, AISquadClass AISquad)
    {
        try
        {

            if (Unit == null || AISquad == null)
            {
                return false;
            }


            if (AISquad.Members == null)
            {
                return false;
            }


            if (!AISquad.Members.Contains(Unit))
            {
                return false;
            }


            bool removed = AISquad.Members.Remove(Unit);

            if (removed)
            {


                if (Unit is Champion champion)
                {
                    UnassignAITask(champion);
                }


                if (AISquad.Members.Count == 0)
                {
                    if (AISquad.AssignedMission != null)
                    {
                        UnassignAIMission(AISquad);
                    }
                }

                return true;
            }
            else
            {

                return false;
            }
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="AIMissionTopic">Position to check.</param>
    /// <param name="TargetUnit">Unit to be highlighted.</param>
    /// <param name="TargetLocation">Location to be targeted.</param>
    /// <param name="LaneID">Lane ID</param>
    /// <param name="Output">AIMissionClass Task we want to get the unit from.</param>

    /// <summary>
    /// Crée une mission AI dynamiquement en utilisant les .DAT et les behavior trees
    /// </summary>
    protected bool CreateAIMission(out AIMissionClass Output, AIMissionTopicType AIMissionTopic, AttackableUnit TargetUnit, Vector3 TargetLocation, int LaneID = 0, TeamId team = TeamId.TEAM_CHAOS)
    {
        try
        {

            string behaviorTreeTag = ResolveMissionTopicToBehaviorTree(AIMissionTopic, team);
            var newmission = new AIMissionClass(AIMissionTopic, TargetUnit, TargetLocation, LaneID);
            AssignBehaviorTreeToMission(newmission, behaviorTreeTag);
            Output = newmission;
            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = default;
            return false;
        }
    }

    /// <summary>
    /// Résout un topic de mission vers un tag de behavior tree en utilisant les .DAT
    /// </summary>
    private string ResolveMissionTopicToBehaviorTree(AIMissionTopicType missionTopic, TeamId team = TeamId.TEAM_CHAOS)
    {
        try
        {
            Dictionary<string, string> missionData = team switch
            {
                TeamId.TEAM_ORDER => ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.ContentManager.TeamOrderAiMissionData,
                TeamId.TEAM_CHAOS => ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.ContentManager.TeamChaosAiMissionData,
                TeamId.TEAM_NEUTRAL => ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.ContentManager.TeamNeutralAiMissionData,
                _ => ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content.ContentManager.TeamChaosAiMissionData
            };

            if (missionData == null || !missionData.Any())
            {
                return GetDefaultBehaviorTreeForMission(missionTopic);
            }
            string topicString = missionTopic.ToString();
            foreach (var kvp in missionData)
            {
                if (kvp.Key.Contains(topicString))
                {
                    string behaviorTreeTag = kvp.Value;
                    return behaviorTreeTag;
                }
            }
            return GetDefaultBehaviorTreeForMission(missionTopic);
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return GetDefaultBehaviorTreeForMission(missionTopic);
        }
    }

    /// <summary>
    /// Mapping par défaut des topics de mission vers les behavior trees
    /// </summary>
    private string GetDefaultBehaviorTreeForMission(AIMissionTopicType missionTopic)
    {
        return missionTopic switch
        {
            AIMissionTopicType.PUSH => "PushMission",
            AIMissionTopicType.KILL => "KillChampionMission",
            AIMissionTopicType.SUPPORT => "PushMission",
            AIMissionTopicType.DEFEND => "ReturnToBaseMission",
            //    AIMissionTopicType.CAPTURE => "CapturePointMission",
            //   AIMissionTopicType.JUNGLING => "MissionJungling",
            //    AIMissionTopicType.NEUTRAL_MINION => "Mission_NeutralMinion",
            //    AIMissionTopicType.ODIN_GUARDIAN => "OdinGuardianMission",
            //   AIMissionTopicType.BASIC_TUTORIAL_PUSH => "BasicTutorialPushMission",
            _ => "PushMission" // Fallback par défaut
        };
    }

    /// <summary>
    /// Assigne un behavior tree à une mission en utilisant la propriété BehaviourMissionAI
    /// </summary>
    private void AssignBehaviorTreeToMission(AIMissionClass mission, string behaviorTreeTag)
    {
        try
        {
            if (mission == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(behaviorTreeTag))
            {
                return;
            }

            mission.BehaviourMissionAI = Game.ScriptEngine.CreateObject<AImission_bt>("BehaviourTrees", behaviorTreeTag);
            //uggly
            mission.BehaviourMissionAI.InitOwner(mission);

            if (mission.BehaviourMissionAI == null)
            {
                mission.BehaviourMissionAI = Game.ScriptEngine.CreateObject<AImission_bt>("ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Scripts", behaviorTreeTag);
            }

        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
        }
    }



    /// <summary>
    /// Obtient le nom de classe C# correspondant au tag de behavior tree
    /// </summary>
    private string GetMissionClassName(string behaviorTreeTag)
    {
        return behaviorTreeTag switch
        {
            "PushMission" => "PushMission",
            "KillChampionMission" => "KillChampionMission",
            "WaitInBase" => "WaitInBase",
            "ReturnToBaseMission" => "ReturnToBaseMission",
            "CapturePointMission" => "CapturePointMission",
            "MissionJungling" => "MissionJungling",
            "Mission_NeutralMinion" => "Mission_NeutralMinion",
            "OdinGuardianMission" => "OdinGuardianMission",
            "BasicTutorialPushMission" => "BasicTutorialPushMission",
            _ => "PushMission" // Fallback par défaut
        };
    }

    /// <summary>Gets the difficulty level for this team. Returns SUCCESS IF. (Needs sentence closure right about now)</summary>

    /// <param name="GameDifficultyType">Difficulty level to test against.</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if</param>

    protected bool TestAITeamDifficulty(GameDifficultyType GameDifficultyType = GameDifficultyType.GAME_DIFFICULTY_NEWBIE, bool ReturnSuccessIf = true)
    {
        try
        {


            // Convertir GameDifficultyType en AIDifficultyType
            EntityDiffcultyType expectedDifficulty = GameDifficultyType switch
            {
                GameDifficultyType.GAME_DIFFICULTY_TUTORIAL => EntityDiffcultyType.DIFFICULTY_NEWBIE,
                GameDifficultyType.GAME_DIFFICULTY_NEWBIE => EntityDiffcultyType.DIFFICULTY_NEWBIE,
                GameDifficultyType.GAME_DIFFICULTY_INTERMEDIATE => EntityDiffcultyType.DIFFICULTY_INTERMEDIATE,
                GameDifficultyType.GAME_DIFFICULTY_ADVANCED => EntityDiffcultyType.DIFFICULTY_ADVANCED,
                GameDifficultyType.GAME_DIFFICULTY_UBER => EntityDiffcultyType.DIFFICULTY_UBER,
                _ => EntityDiffcultyType.DIFFICULTY_NEWBIE
            };

            // Vérifier si au moins un bot a la difficulté attendue
            bool hasMatchingDifficulty = AIEntitiesAssociated.Any(bot => (bot as Champion).AIDifficulty == expectedDifficulty);


            return hasMatchingDifficulty == ReturnSuccessIf;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false == ReturnSuccessIf;
        }
    }
    /// <summary>Add a named location to the game world. Specify a name and a world position. (Revisit this description)</summary>

    /// <param name="Output">Task we want to get the unit from.</param>

    protected bool GetAIManagerMissions(out AIMissionCollection Output)
    {
        Output = default;
        return false;
    }
    /// <summary>Set the status of an AI Task.</summary>

    /// <param name="AIMission">The mission we want to set status for.</param>
    /// <param name="LogicResult">Status we want set on a mission.</param>

    protected bool SetAIMissionStatus(AIMissionClass AIMission, LogicResultType LogicResult)
    {
        try
        {
            if (AIMission == null)
            {
                return false;
            }

            AIMission.Status = LogicResult;
            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }
    /// <summary>Get topic from a mission.</summary>

    /// <param name="AIMission">The mission we want get topic from</param>
    /// <param name="Output">Topic from mission.</param>

    protected bool GetAIMissionTopic(out AIMissionTopicType Output, AIMissionClass AIMission)
    {
        try
        {
            if (AIMission == null)
            {
                Output = AIMissionTopicType.PUSH; // Valeur par défaut

                return false;
            }

            Output = AIMission.Topic;
            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = AIMissionTopicType.PUSH;
            return false;
        }
    }
    /// <summary>Get target from a mission.</summary>

    /// <param name="AIMission">The mission we want get target from</param>
    /// <param name="Output">Target from mission.</param>

    protected bool GetAIMissionTarget(out AttackableUnit Output, AIMissionClass AIMission)
    {
        try
        {
            if (AIMission == null)
            {
                Output = null;
                return false;
            }

            Output = AIMission.TargetUnit;

            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = null;
            return false;
        }
    }
    /// <summary>Get position from a mission.</summary>

    /// <param name="AIMission">The mission we want get positiom from</param>
    /// <param name="Output">Position from mission.</param>

    protected bool GetAIMissionPosition(out Vector3 Output, AIMissionClass AIMission)
    {
        try
        {
            if (AIMission == null)
            {
                Output = Vector3.Zero;
                return false;
            }

            Output = AIMission.TargetLocation;

            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = Vector3.Zero;
            return false;
        }
    }
    /// <summary>Get Index from a Mission.</summary>
    /// <summary>FALSE , is laneID .</summary>
    /// <param name="AIMission">The Mission we want get index from</param>
    /// <param name="Output">Index from Mission.</param>

    protected bool GetAIMissionIndex(out int Output, AIMissionClass AIMission)
    {
        try
        {
            if (AIMission == null)
            {
                Output = -1;
                return false;
            }

            Output = AIMission.LaneID;

            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = -1;
            return false;
        }
    }
    /// <summary>Retrieve a mission by topic and other parameters.</summary>

    /// <param name="AIMissionTopic">Mission topic.</param>
    /// <param name="TargetUnit">Unit .</param>
    /// <param name="TargetLocation">Location .</param>
    /// <param name="LaneID">Lane ID</param>
    /// <param name="Output">Ritrieved mission.</param>

    protected bool RetrieveAIMission(out AIMissionClass Output, AIMissionTopicType AIMissionTopic, AttackableUnit TargetUnit, Vector3 TargetLocation, int LaneID = 0)
    {
        try
        {
            // ✅ Utiliser le nouveau manager pour chercher une mission existante
            var mission = Game.Map.MapData.aisquadlistmanager.RetrieveAIMission(AIMissionTopic, TargetUnit, TargetLocation, LaneID);

            if (mission != null)
            {
                Output = mission;

                return true;
            }

            // Aucune mission trouvée
            Output = null;
            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = null;
            return false;
        }
    }

    /// <summary>Closest Lane ID.</summary>

    /// <param name="Unit">Unit to be highlighted.</param>
    /// <param name="Output">Ref to the lane ID</param>

    //todo : in s1 ( aimesh_ngrid doesn't contain  ) 

    protected bool GetUnitAIClosestLaneID(out int Output, AttackableUnit Unit)
    {
        try
        {
            if (Unit == null)
            {
                Output = -1;
                return false;
            }

            float closestDistance = float.MaxValue;
            int closestLaneID = -1;
            Vector3 unitPosition = Unit.Position3D;



            if (Unit is LaneTurret or BaseTurret)
            {
                Output = (int)(Unit as BaseTurret).Lane;
                return true;
            }
            if (Unit is Inhibitor)
            {
                Output = (int)(Unit as Inhibitor).Lane;
                return true;
            }
            if (Unit is LaneMinion)
            {
                Output = (int)(Unit as LaneMinion).BarrackSpawn.Lane;
                return true;

            }
            /*    if(Unit is Minion ) TODO : check for dominion 
                {
                    Output = 
                }*/
            Output = -1;
            return false;
            //the method bellow is too cost ,in aimesh ngrid , after the S3 approx , the aimesh n grid containe the lane ID directly 

            /*
                        foreach (var lanelistwaypoint in Game.Map.NavigationPoints)
                        {
                            int currentLaneID = (int)lanelistwaypoint.Key;

                            foreach (var waypoint in lanelistwaypoint.Value)
                            {
                                Vector3 waypointPosition = waypoint.Position3D;
                                float distance = Vector3.Distance(unitPosition, waypointPosition);

                           

                                if (distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    closestLaneID = currentLaneID;
                                  
                                }
                            }
                        }

                        if (closestLaneID != -1)
                        {
                            Output = closestLaneID;
                         
                            return true;
                        }
                        else
                        {
                        
                            Output = -1;
                            return false;
                        }*/
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            Output = -1;
            return false;
        }
    }
    /// <summary>Closest Lane Point</summary>

    /// <param name="Unit">Unit to be highlighted.</param>
    /// <param name="LaneID">Lane ID</param>
    /// <param name="Output">Ref to closest point.</param>

    protected bool GetUnitAIClosestLanePoint(out Vector3 Output, AttackableUnit Unit, int LaneID = 0)
    {
        Output = default;
        return false;
    }
    /// <summary>Increment a player's score. Use a positive Point Value.</summary>

    /// <param name="Player">Player's score you want to increment.</param>
    /// <param name="ScoreCategory">Player score category.  All categories will also increase Total Score.</param>
    /// <param name="ScoreEvent">Player score event.</param>
    /// <param name="Value">Amount you want to increment a player's score by.</param>
    /// <param name="Show">Should we show this score to the player in the UI score callouts?</param>

    protected bool IncrementPlayerScore(AttackableUnit Player, ScoreCategory ScoreCategory, ScoreEvent ScoreEvent, float Value = 0, bool Show = true)
    {
        (Player as Champion).IncrementScore(Value, ScoreCategory, ScoreEvent, Show);
        return true;
    }
    /// <summary>Increment a player's stat by one</summary>

    /// <param name="Player">Player's stat you want to increment.</param>
    /// <param name="StatEvent">Player stat event.</param>

    protected bool IncrementPlayerStat(AttackableUnit Player, StatEvent StatEvent)
    {
        //todo
        return true;
    }
    /// <summary>Prepares a Music Cue to be triggered in the future.  For our FMOD interactive music system.</summary>

    /// <param name="Player">The player whose Music Cue you want to prepare.</param>
    /// <param name="CueID">Cue IDs are set by FMOD; talk to a sound designer for valid cue ids.</param>

    protected bool PrepareMusicCue(AttackableUnit Player, uint CueID)
    {
        Player.musicCuetobeplayed = CueID;
        return true;
    }
    /// <summary>Begins a Music Cue.  If the cue does not exist, it creates it.  For our FMOD interactive music system.</summary>

    /// <param name="Player">The player whose Music Cue you want to begin.</param>
    /// <param name="CueID">Cue IDs are set by FMOD; talk to a sound designer for valid cue ids.</param>

    protected bool BeginMusicCue(AttackableUnit Player, uint CueID)
    {
        Player.musicCuetobeplayed = CueID;
        //todo : understand how use them 
        InteractiveMusicCommandNotify(Player as ObjAIBase, 1, CueID, 0);
        return true;
    }
    /// <summary>Ends a Music Cue.  For our FMOD interactive music system.</summary>

    /// <param name="Player">The player whose Music Cue you want to end</param>
    /// <param name="CueID">Cue IDs are set by FMOD; talk to a sound designer for valid cue ids.</param>

    protected bool EndMusicCue(AttackableUnit Player, uint CueID)
    {
        Player.musicCuetobeplayed = 0;
        //todo : understand how use them 
        InteractiveMusicCommandNotify(Player as ObjAIBase, 0, CueID, 0);
        return true;
    }
    /// <summary>Disables the HUD used for the end of game.  Not guaranteed to work anywhere else.</summary>


    protected bool DisableHUDForEndOfGame()
    {
        return false;
    }
    /// <summary>Prevents all units from moving for the end of game.  Not guaranteed to work anywhere else.</summary>


    protected bool HaltAllUnits()
    {
        return false;
    }
    /// <summary></summary>

    /// <param name="Team">Team to find player for.  If undefined returns all players.</param>
    /// <param name="Output">Destination Reference; holds the number of players.  Note currently searches for players (potential performance issue if called a lot).</param>

    protected bool GetNumberOfPlayers(out int Output, TeamId Team = TeamId.TEAM_ORDER)
    {
        Output = default;
        return false;
    }
    /// <summary></summary>

    /// <param name="Team">Team to find player for.  If undefined returns all connected players.</param>
    /// <param name="Output">Destination Reference; holds the number connected of players.  Note currently searches for players (potential performance issue if called a lot).</param>

    protected bool GetNumberOfConnectedPlayers(out int Output, TeamId Team = TeamId.TEAM_ORDER)
    {
        int count = 0;
        var players = Game.PlayerManager.GetPlayers(false);
        foreach (var player in players)
        {
            if (player.Team == Team)
            {
                count++;
            }
        }
        Output = count;
        return true;
    }
    /// <summary>Query safe location from heat map</summary>

    /// <param name="Radius">Queray cycle area radius</param>
    /// <param name="HeatMapFlag">If hero allies are taken into account.</param>
    /// <param name="Output">Safe location</param>

    protected bool GetUnitAISafePositionFromHeatMap(out Vector3 Output, float Radius, HeatMapFlag HeatMapFlag = HeatMapFlag.CONSIDER_TURRETS | HeatMapFlag.CONSIDER_HEROES | HeatMapFlag.CONSIDER_MINIONS)
    {
        Output = default;
        return false;
    }
    /// <summary>Get spell missile speed.</summary>

    /// <param name="Unit">The unit who own this spell.</param>
    /// <param name="SlotIndex">Slot number of this spell.</param>
    /// <param name="Output">Spell missile speed.</param>

    protected bool GetSpellMissileSpeed(out float Output, AttackableUnit Unit, int SlotIndex = 0)
    {
        Output = default;
        return false;
    }
    /// <summary>Predict line missile cast position for a target.</summary>

    /// <param name="TargetUnit">Target unit.</param>
    /// <param name="SourcePoint">Spell cast position</param>
    /// <param name="SkillSpeed">Line missile speed.</param>
    /// <param name="DelayTime">Line missile delay time before casting.</param>
    /// <param name="Output">Line missile target position.</param>

    protected bool PredictLineMissileCastPosition(out Vector3 Output, AttackableUnit TargetUnit, Vector3 SourcePoint, float SkillSpeed, float DelayTime = 0)
    {
        Output = default;

        if (TargetUnit == null || SkillSpeed <= 0)
            return false;

        Output = TargetUnit.Position3D;
        return true;


        /*    try
            {
                Vector3 targetPosition = TargetUnit.Position3D;
                Vector3 directionToTarget = (targetPosition - SourcePoint).Normalized();

                Vector3 predictedTargetPosition = targetPosition;

                if (TargetUnit is ObjAIBase objAI && objAI.CanMove())
                {
                    float moveSpeed = Math.Clamp(objAI.Stats.MoveSpeed.Total, 0, 1000); // sécurité
                    Vector3 moveDirection = objAI.Direction;

                    if (moveDirection == Vector3.Zero || !IsValid(moveDirection))
                    {
                        // fallback : direction du projectile
                        Vector3 upVector = new Vector3(0, 0, 1);
                        moveDirection = Vector3.Cross(directionToTarget, upVector).Normalized();
                    }
                    else
                    {
                        moveDirection = moveDirection.Normalized();
                    }

                    // Boucle d'approximation pour la convergence
                    Vector3 lastPrediction = predictedTargetPosition;
                    const int maxIterations = 5;

                    for (int i = 0; i < maxIterations; i++)
                    {
                        float distance = Vector3.Distance(SourcePoint, predictedTargetPosition);
                        float flightTime = distance / SkillSpeed;

                        float totalTime = flightTime + DelayTime;
                        predictedTargetPosition = targetPosition + (moveDirection * moveSpeed * totalTime);

                        // Si la nouvelle prédiction est très proche de l’ancienne, on arrête
                        if ((predictedTargetPosition - lastPrediction).LengthSquared() < 1.0f)
                            break;

                        lastPrediction = predictedTargetPosition;
                    }
                }

                Output = predictedTargetPosition;
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }*/
    }

    private bool IsValid(Vector3 vec)
    {
        return !(
            float.IsNaN(vec.X) || float.IsInfinity(vec.X) ||
            float.IsNaN(vec.Y) || float.IsInfinity(vec.Y) ||
            float.IsNaN(vec.Z) || float.IsInfinity(vec.Z)
        );
    }

    /// <summary>Generate a random position between two points.</summary>

    /// <param name="Point">Point 1.</param>
    /// <param name="Radius">Point 2.</param>
    /// <param name="Output">Random point between two points.</param>

    protected bool GetRandomPositionInCircle(out Vector3 Output, Vector3 Point, float Radius)
    {
        Output = FLS.GetRandomPointInAreaPosition(Point, Radius, 0);
        return true;
    }
    /// <summary>Count number of units in the target area. Always returns SUCCESS.</summary>

    /// <param name="Unit">Handle of the unit that serves as the reference for team flags.</param>
    /// <param name="TargetLocation">Center of the test</param>
    /// <param name="Radius">Radius of the unit test</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>
    /// <param name="BuffName">Cound units with this buff. Disregard buff if this is empty.</param>
    /// <param name="Output">Number of units discovered</param>

    protected bool CountUnitsInTargetArea(out int Output, AttackableUnit Unit, Vector3 TargetLocation, float Radius = 0, SpellDataFlags SpellFlags = SpellDataFlags.AlwaysSelf, string BuffName = "")
    {
        if (BuffName != "")
        {
            Output = FCS.GetUnitsInArea(Unit, TargetLocation, Radius, SpellFlags, BuffName, true).Count();
        }
        else
        {
            var list = FCS.GetUnitsInArea(Unit, TargetLocation, Radius, SpellFlags);
            Output = list.Count();
        }


        return true;
    }
    /// <summary>
    /// Interpolate between two points, calculate y based on input x. NOTE: X1 cannot be equal to X2!
    /// </summary>
    /// <param name="X1">X coordinate for point 1.</param>
    /// <param name="X2">X coordinate for point 2.</param>
    /// <param name="Y1">Y coordinate for point 1.</param>
    /// <param name="Y2">Y coordinate for point 2.</param>
    /// <param name="Min">You can give a lower bound value for result.</param>
    /// <param name="Max">You can give an upper bound value for result.</param>
    /// <param name="Input">X value of output Y coordinate. If input is 0, output will be Y1; if input is 1, output will be Y2. In the end, the result will be clipped if optional parameters are provided.</param>
    /// <param name="Output">Y value interpolated by input X.</param>
    /// <returns>Returns true if the calculation was successful, otherwise false.</returns>
    protected bool InterpolateLine(out float Output, float X1 = 0, float X2 = 1, float Y1 = 0, float Y2 = 1, float Min = 0, float Max = 1, float Input = 0)
    {
        if (X1 == X2)
        {
            Output = default;
            return false; // Cannot interpolate if X1 is equal to X2
        }

        float t = (Input - X1) / (X2 - X1); // Calculate interpolation parameter
        Output = Y1 + (t * (Y2 - Y1)); // Interpolate Y value based on input X

        // Clip the result between Min and Max
        Output = MathF.Max(Min, MathF.Min(Max, Output));

        return true;
    }

    /// <summary>Test if a capture point is able to capture</summary>

    /// <param name="Unit">Reference of a user unit.</param>
    /// <param name="TargetUnit">reference of target unit which is going to used.</param>

    protected bool TestCanUseObject(AttackableUnit Unit, AttackableUnit TargetUnit)
    {
        return false;
    }
    /// <summary>Use object</summary>

    /// <param name="TargetUnit">reference of target unit which is going to used.</param>

    protected bool IssueUseObjectOrder(AttackableUnit TargetUnit)
    {
        return false;
    }
    /// <summary>Sets the game score for a team.</summary>

    /// <param name="Team">The team whose point you want to change.</param>
    /// <param name="Output">The new score for the team.</param>

    protected bool GetGameScore(out float Output, TeamId Team)
    {

        Output = Game.Map.MapData.MapScoring[Team];
        return true;
    }
    /// <summary>Test if a line missile to given target can be blocked by enemy champion or minion. Returns SUCCESS if can hit, otherwise return FAILURE.</summary>

    /// <param name="Unit">Reference of bot.</param>
    /// <param name="TargetUnit">Reference to target unit.</param>
    /// <param name="CollisionWidth">Collision width of skillshot.</param>
    /// <param name="SpellFlags">Associated spell flags for target filtering of the unit gathering check.</param>

    protected bool TestLineMissilePathIsClear(AttackableUnit Unit, AttackableUnit TargetUnit, float CollisionWidth = 0, SpellDataFlags SpellFlags = SpellDataFlags.NotAffectSelf)
    {
        //todo , replace pathfindinder by witdh and take in account actorlist 
        return Game.Map.NavigationGrid.HasClearLineOfSight(Unit.Position, TargetUnit.Position, Unit);
    }
    /// <summary>Finds the closest encounter with living units to a given location.</summary>

    /// <param name="EncounterId">What encounter to look for</param>
    /// <param name="Position">Where do you want to search from</param>
    /// <param name="SquadId">The Squad ID of the closest encounter</param>
    /// <param name="TargetLocation">Where the encounter is located</param>

    protected bool GetClosestEncounterToPoint(out int SquadId, out Vector3 TargetLocation, int EncounterId, Vector3 Position)
    {
        SquadId = default;
        TargetLocation = default;
        return false;
    }
    /// <summary>Send a message from entity to team manager.</summary>

    /// <param name="SourceUnit">Sender unit.</param>
    /// <param name="TargetUnit">Reference to sender's target unit.</param>
    /// <param name="AITaskTopic">AI topic of sender unit.</param>
    /// <param name="String">String message sent to manager.</param>

    /// <summary>
    /// Envoie un message au gestionnaire AI pour déclencher des actions d'équipe
    /// </summary>
    protected bool SendMessageToManager(AttackableUnit SourceUnit, AttackableUnit TargetUnit, AITaskTopicType AITaskTopic, string String)
    {
        try
        {
            // ✅ JUSTE sauvegarder le message
            var teamMessage = new TeamMessage
            {
                SourceUnit = SourceUnit,
                TargetUnit = TargetUnit,
                AITaskTopic = AITaskTopic,
                Message = String,
                Timestamp = DateTime.Now
            };
            Game.ObjectManager._aiManagers[SourceUnit.Team]._teamMessageQueue.Enqueue(teamMessage);

            return true;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }
    /// <summary>Get first message in this team's queue. You can keep or remove this message after peek it by setting 'Discard'.</summary>

    /// <param name="Discard">If the message should be discarded after peek.</param>
    /// <param name="SourceUnit">Sender unit.</param>
    /// <param name="TargetUnit">Reference to sender's target unit.</param>
    /// <param name="AITaskTopic">AI topic of sender unit.</param>
    /// <param name="String">String message sent to manager.</param>

    /// <summary>
    /// Récupère le premier message de l'équipe depuis la file d'attente
    /// </summary>
    protected bool PeekTeamMessage(out AttackableUnit SourceUnit, out AttackableUnit TargetUnit, out string AITaskTopic, out string String, bool Discard = true)
    {
        SourceUnit = null;
        TargetUnit = null;
        AITaskTopic = null;
        String = null;

        if (_teamMessageQueue.Count > 0)
        {
            var message = _teamMessageQueue.Peek();

            SourceUnit = message.SourceUnit;
            TargetUnit = message.TargetUnit;
            AITaskTopic = message.AITaskTopic.ToString();
            String = message.Message;

            if (Discard)
            {
                _teamMessageQueue.Dequeue();
            }

            return true;
        }

        return false;
    }
    /// <summary>Remove the first message in this team's queue.</summary>


    /// <summary>
    /// Supprime le premier message de la file d'attente de l'équipe
    /// </summary>
    protected bool PopTeamMessage()
    {
        try
        {
            if (_teamMessageQueue.Count > 0)
            {
                var message = _teamMessageQueue.Dequeue();
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.Warn(e.Message);
            return false;
        }
    }
    /// <summary>Get first spell message in this team's queue. You can keep or remove this message after peek it by setting 'Discard'.</summary>

    /// <param name="Discard">If the message should be discarded after peek.</param>
    /// <param name="SourceUnit">Casting unit.</param>
    /// <param name="TargetUnit">Reference to sender's target unit (may be NULL)</param>
    /// <param name="TargetPosition">Where the ability is being aimed</param>
    /// <param name="IgnoreUnit">What level of unit the ability hits. 0 = passthrough, 1 = stop on champs, 2 = stop on minions</param>
    /// <param name="EndFlag">The ability only matters on the 'end' (e.g. Chogath Rupture, Karthus Lay Waste)</param>
    /// <param name="Lifetime">How long the ability last before it expries</param>
    /// <param name="Radius">The radius (or width) of the effect</param>
    /// <param name="Range">How far the ability goes</param>
    /// <param name="Speed">How fast the ability goes</param>

    protected bool PeekSpellMessage(out AttackableUnit SourceUnit, out AttackableUnit TargetUnit, out Vector3 TargetPosition, out int IgnoreUnit, out bool EndFlag, out float Lifetime, out float Radius, out float Range, out float Speed, bool Discard = true)
    {
        SourceUnit = default;
        TargetUnit = default;
        TargetPosition = default;
        IgnoreUnit = default;
        EndFlag = default;
        Lifetime = default;
        Radius = default;
        Range = default;
        Speed = default;
        return false;
    }


    /// <summary>Remove the first message in this team's queue.</summary>
    protected bool PopSpellMessage()
    {
        //todo : 
        return true;
    }




    /// <summary>Returns the current magic resist of a specific unit.</summary>
    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current magic resist of the unit.</param>

    protected bool GetUnitMagicResist(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.MagicResist.Total;
        return false;
    }
    /// <summary>Returns the current attack damage of a specific unit.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current attack damage of the unit.</param>

    protected bool GetUnitAttackDamage(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.AttackDamage.Total;
        return false;
    }
    /// <summary>Returns the current ability power of a specific unit.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current ability power of the unit.</param>

    protected bool GetUnitAbilityPower(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.AbilityPower.Total;
        return true;
    }
    /// <summary>Returns the current attack speed of a specific unit.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current attack speed of the unit.</param>

    protected bool GetUnitAttackSpeed(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.GetTotalAttackSpeed();
        return true;
    }
    /// <summary>Returns the current move speed of a specific unit.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current move speed of the unit.</param>

    protected bool GetUnitMoveSpeed(out float Output, AttackableUnit Unit)
    {
        Output = Unit.Stats.MoveSpeed.Total;
        return true;
    }
    /// <summary>Test if an unit can be seen by viewer team.</summary>

    /// <param name="Viewer">Viewer Unit</param>
    /// <param name="TargetUnit">Target  Unit</param>
    /// <param name="ReturnSuccessIf">If True, returns SUCCESS if the unit can see target</param>

    protected bool TestUnitIsVisibleToTeam(AttackableUnit Viewer, AttackableUnit TargetUnit, bool ReturnSuccessIf = true)
    {
        //hacktest 
        if (TargetUnit is LaneTurret or BaseTurret)
            return ReturnSuccessIf == true;


        if (TargetUnit != null && Viewer != null)
        {
            return ReturnSuccessIf == VisionManager.UnitHasVisionOn(Viewer, TargetUnit);
        }


        return false;
    }
    /// <summary>Returns damage accounting for armor and other misc defenses to a target.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="TargetUnit">Unit being targetted.</param>
    /// <param name="Output">Destination reference; contains the current move speed of the unit.</param>

    protected bool GetUnitActualDamage(out float Output, AttackableUnit Unit, AttackableUnit TargetUnit)
    {
        Output = default;
        return false;
    }
    /// <summary>
    /// Finds a point at a given distance from a point along the same line as another point.
    /// </summary>
    /// <param name="Position">The position to target around.</param>
    /// <param name="TargetLocation">The colinear point we are extending along.</param>
    /// <param name="Range">How far to project along the line from Position to TargetLocation</param>
    /// <param name="TargetPosition">The calculated point</param>
    /// <returns>Returns true if the calculation was successful, otherwise false.</returns>
    protected bool CalculatePointOnLine(out Vector3 TargetPosition, Vector3 Position, Vector3 TargetLocation, float Range)
    {
        Vector3 direction = TargetLocation - Position;
        TargetPosition = Position + (direction * Range);
        return true;
    }

    /// <summary>
    /// Generate a random position between two points.
    /// </summary>
    /// <param name="SourcePoint">Point 1.</param>
    /// <param name="TargetPoint">Point 2.</param>
    /// <param name="UseRatio">This ratio can move source point from input position to target point.</param>
    /// <param name="Output">Random point between two points. Point 1 can be moved by ratio to point 2.</param>
    /// <returns>Returns true if the calculation was successful, otherwise false.</returns>
    protected bool GetRandomPositionBetweenTwoPoints(out Vector3 Output, Vector3 SourcePoint, Vector3 TargetPoint, float UseRatio = 0.0f)
    {
        if (UseRatio < 0.0f || UseRatio > 1.0f)
        {
            Output = default;
            return false;
        }

        Vector3 direction = TargetPoint - SourcePoint;
        float distance = Vector3.Distance(SourcePoint, TargetPoint) * UseRatio;
        Output = SourcePoint + (direction * distance);
        return true;
    }
    /// <summary>Find the first ally minion near a given unit.</summary>

    /// <param name="Unit">Reference to an unit.</param>
    /// <param name="Radius">Radius of searching area.</param>
    /// <param name="Output">The first ally minion near the given unit.</param>

    protected bool FindFirstAllyMinionNearby(out AttackableUnit Output, AttackableUnit Unit, float Radius = 0.0f)
    {

        var SpellFlags = SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions;

        var allUnits = FCS.GetClosestVisibleUnitsInArea(Unit, Unit.Position3D, Radius, SpellFlags, 1);

        if (allUnits.Any())
        {
            Output = allUnits.First();
            return true;
        }
        Output = null;
        return false;
    }
    /// <summary>Find the last ally minion near a given unit.</summary>

    /// <param name="Unit">Reference to an unit.</param>
    /// <param name="Radius">Radius of searching area.</param>
    /// <param name="Output">The last ally minion near the given unit.</param>

    protected bool FindLastAllyMinionNearby(out AttackableUnit Output, AttackableUnit Unit, float Radius = 0.0f)
    {
        var SpellFlags = SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions;

        var allUnits = FCS.GetClosestVisibleUnitsInArea(Unit, Unit.Position3D, Radius, SpellFlags, 1);

        if (allUnits.Any())
        {
            Output = allUnits.Last();
            return true;
        }
        Output = null;
        return false;
    }
    /// <summary>Returns the total amount of gold the unit has earned. Returns FAILURE if the unit is not valid.</summary>

    /// <param name="Unit">Unit to poll. This unit has to be a hero, otherwise this note will return FAILURE.</param>
    /// <param name="Output">Gold earned in this game.</param>

    protected bool GetUnitGoldEarned(out float Output, AttackableUnit Unit)
    {
        Output = Unit.GoldOwner.TotalGoldEarned;
        return true;
    }
    /// <summary>Sell item</summary>

    /// <param name="ItemID">Item to Sell.</param>

    protected bool UnitAISellItem(int ItemID = 0)
    {

        return false;
    }
    /// <summary>Calculate Flee destination Point</summary>

    /// <param name="Output">Flee destination Point.</param>

    protected bool GetUnitAIFleePoint(out Vector3 Output)
    {
        Output = default;
        return false;
    }


    /// <summary>Decorator that will iterate through a collection, looping its children for each entry. Right now this only supports AttackableUnit collections.  This will always return SUCCESS.</summary>

    /// <param name="Collection">The collection that the iterator should loop over.</param>
    /// <param name="Output">Output reference for each individual iteration of the node.  This should only be referenced by children!</param>

    protected bool IterateOverAllDecorator(out AttackableUnit Output, ICollection<AttackableUnit> Collection)
    {
        Output = default;
        return false;
    }
    /// <summary>Display floating text over unit's head. For DEBUG only.  Does not support localization!  Returns SUCCESS regardless even if Unit does not exist.</summary>

    /// <param name="Unit">Unit to show floating text over.</param>
    /// <param name="String">The string to display.</param>

    protected bool Say(AttackableUnit Unit, string String = "")
    {
        //todo : 
        return true;
    }
    /// <summary>Checks the metadata of the unit and returns the associated value.  If the unit does not have an entry for the key the node will return FAILURE.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Key">The key to check for.</param>
    /// <param name="Output">Destination reference; contains the value for the metadata key.</param>

    protected bool GetUnitMetadataValue(out string Output, AttackableUnit Unit, string Key)
    {
        Output = default;
        return false;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for string References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualString(string LeftHandSide, string RightHandSide)
    {
        return false;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide without considering case, and FAILURE if it is not. This version is for string References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualStringCaseInsensitive(string LeftHandSide, string RightHandSide)
    {
        return false;
    }

    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for string References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualString(string LeftHandSide, string RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }

    /// <summary>A Procedure call</summary>


    protected bool Procedure()
    {
        return false;
    }
    // Registre des fonctions dynamiques pour CallProcedureVariable
    private static readonly Dictionary<string, Delegate> _procedureRegistry = new();

    /// <summary>
    /// Enregistre une fonction dans le registre des procédures
    /// </summary>
    /// <param name="procedureName">Nom de la procédure</param>
    /// <param name="procedure">Délégué de la fonction</param>
    public static void RegisterProcedure(string procedureName, Delegate procedure)
    {
        _procedureRegistry[procedureName] = procedure;
    }

    /// <summary>
    /// Obtient le nombre de procédures enregistrées
    /// </summary>
    /// <returns>Nombre de procédures enregistrées</returns>
    public static int GetRegisteredProcedureCount()
    {
        return _procedureRegistry.Count;
    }

    /// <summary>
    /// Appelle une procédure dynamique avec des paramètres de sortie
    /// Cette version correspond à l'utilisation dans les behaviour trees existants
    /// </summary>
    /// <summary>
    /// Méthode helper générique pour appeler les procédures
    /// </summary>
    protected bool CallProcedureVariable(out object[] outputs, object procedureObject, params object[] inputs)
    {
        outputs = null;

        if (procedureObject == null)
        {
            return false;
        }


        string methodName = procedureObject.GetType().Name.Replace("Class", "");

        var method = procedureObject.GetType().GetMethod(methodName);
        if (method == null)
        {
            return false;
        }


        var parameters = method.GetParameters();

        if (parameters.Length < inputs.Length)
        {
            return false;
        }

        object[] args = new object[parameters.Length];
        int inIndex = 0;

        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].IsOut)
            {
                args[i] = GetDefault(parameters[i].ParameterType.GetElementType());
            }
            else
            {
                if (inIndex >= inputs.Length)
                {
                    return false;
                }

                args[i] = inputs[inIndex++];
            }
        }

        try
        {
            var result = method.Invoke(procedureObject, args);


            outputs = parameters
                .Where(p => p.IsOut)
                .Select(p => args[Array.IndexOf(parameters, p)])
                .ToArray();


            if (result is bool b)
            {
                return b;
            }


            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private object GetDefault(Type t)
    {
        return t.IsValueType ? Activator.CreateInstance(t) : null;
    }





    /// <summary>Set a DynamicProcedure Variable - creates an instance of the procedure class</summary>
    protected bool SetProcedureVariable(out object thing, string procedurename)
    {
        thing = default;



        try
        {

            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1);
            var callingMethod = callingFrame?.GetMethod();



            // Obtenir le nom du champion depuis le propriétaire
            string championName = "";
            if (Owner is Champion champion)
            {
                championName = champion.Model;

            }


            // Logique de mapping : chercher d'abord dans le dossier du champion, puis dans le dossier global
            string championSpecificClass = $"{championName}_{procedurename}Class";
            string globalClass = $"{procedurename}Class";



            // Essayer de créer l'instance spécifique au champion d'abord
            try
            {
                var championInstance = Game.ScriptEngine.CreateObject<object>("BehaviourTrees.all", championSpecificClass);
                if (championInstance != null)
                {

                    // Propager le propriétaire à l'instance créée
                    if (championInstance is BehaviourTree btInstance)
                    {
                        // Utiliser l'owner du contexte appelant (this.Owner) ou chercher dans la hiérarchie
                        var ownerToPropagate = Owner ?? FindOwnerInHierarchy();
                        if (ownerToPropagate != null)
                        {
                            btInstance.Owner = ownerToPropagate;
                            // Propager l'owner aux enfants (sous-instances)
                            btInstance.PropagateOwnerToChildren(ownerToPropagate);
                        }

                    }

                    thing = championInstance;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message);
            }


            try
            {
                var globalInstance = Game.ScriptEngine.CreateObject<object>("BehaviourTrees.all", globalClass);
                if (globalInstance != null)
                {

                    if (globalInstance is BehaviourTree btInstance)
                    {

                        var ownerToPropagate = Owner ?? FindOwnerInHierarchy();
                        if (ownerToPropagate != null)
                        {
                            btInstance.Owner = ownerToPropagate;

                            btInstance.PropagateOwnerToChildren(ownerToPropagate);

                        }

                    }

                    thing = globalInstance;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.Warn(ex.Message);
            return true;
        }
    }



    /// <summary>Returns the current health percentage of a specific unit.</summary>

    /// <param name="Unit">Unit to poll.</param>
    /// <param name="Output">Destination reference; contains the current health percentage of the unit (0.0->1.0).</param>

    protected bool GetUnitCurrentHealthPercentage(out float Output, AttackableUnit Unit)
    {
        Output = default;
        return false;
    }
    /// <summary>Generate random value</summary>

    /// <param name="MinValue">lower bound</param>
    /// <param name="MaxValue">upper bound</param>
    /// <param name="Output">Random value</param>

    protected bool GenerateRandomFloat(out float Output, float MinValue, float MaxValue)
    {
        Output = (float)((new Random().NextDouble() * (MaxValue - MinValue)) + MinValue);
        return true;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not. This version is for float References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {

        return LeftHandSide < RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not. This version is for float References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessEqualFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {
        return LeftHandSide <= RightHandSide;
    }


    /// <summary>Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool GreaterInt(int LeftHandSide = 0, int RightHandSide = 0)
    {
        return LeftHandSide > RightHandSide;
    }
    /// <summary>Sets OutputRef with the value of Input. This version is for UnitTeam References</summary>

    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool SetVarUnitTeam(out TeamId Output, TeamId Input = TeamId.TEAM_UNKNOWN)
    {
        Output = Input;
        return true;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for Unit team.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualUnitTeam(TeamId LeftHandSide = TeamId.TEAM_UNKNOWN, TeamId RightHandSide = TeamId.TEAM_UNKNOWN)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for Unit team.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualUnitTeam(TeamId LeftHandSide = TeamId.TEAM_UNKNOWN, TeamId RightHandSide = TeamId.TEAM_UNKNOWN)
    {

        return LeftHandSide != RightHandSide;
    }


    protected bool NotEqualneutralTeam(TeamId LeftHandSide = TeamId.TEAM_UNKNOWN)
    {

        return LeftHandSide != TeamId.TEAM_NEUTRAL;
    }



    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualInt(int LeftHandSide = 0, int RightHandSide = 0)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for bool References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualBool(bool LeftHandSide = true, bool RightHandSide = true)
    {
        return LeftHandSide == RightHandSide;
    }

    /// <summary>Concatenates the LeftHandSide to the RightHandSide and places the result in Output. This version is for Strings.  This will always return SUCCESS.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="RightHandSide">RightHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>

    protected bool AddString(out string Output, string LeftHandSide = "", string RightHandSide = "")
    {
        Output = LeftHandSide + RightHandSide;
        return true;
    }

    /// <summary>Returns an int containing the number of kills the team has. Always returns SUCCESS.</summary>

    /// <param name="Team">Which team to poll</param>
    /// <param name="Output">Destination Reference; holds the number of champions killed by the team</param>

    protected bool GetTeamKills(out int Output, TeamId Team = TeamId.TEAM_ORDER)
    {
        Output = default;
        return false;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for bool References.</summary>

    /// <param name="LeftHandSide">LeftHandide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualBool(bool LeftHandSide = true, bool RightHandSide = true)
    {
        return LeftHandSide != RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>
    /// -1 seem the default here , because : clarityslot notequalint -1 
    protected bool NotEqualInt(int LeftHandSide = -1, int RightHandSide = -1)
    {
        return LeftHandSide != RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessInt(int LeftHandSide = 0, int RightHandSide = 0)
    {
        return LeftHandSide < RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessEqualInt(int LeftHandSide = 0, int RightHandSide = 0)
    {
        return LeftHandSide <= RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not. This version is for int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool GreaterEqualInt(int LeftHandSide = 0, int RightHandSide = 0)
    {
        return LeftHandSide >= RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide != RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than the RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide < RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is less than or equal to RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool LessEqualUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide <= RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool Minioncheckifcorrectgardian(AttackableUnit unit)
    {

        if (unit.capturepointid != -1 && !unit.Model.Contains("Shrine"))
        {
            return true;
        }
        else { return false; }
        ;


    }


    protected bool GreaterUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide > RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not. This version is for Unsigned int References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool GreaterEqualUnsignedInt(uint LeftHandSide = 0, uint RightHandSide = 0)
    {
        return LeftHandSide >= RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for flooat References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for float References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {
        return LeftHandSide != RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is greater than RightHandSide, and FAILURE if it is not. This version is for float References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool GreaterFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {
        return LeftHandSide > RightHandSide;

    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for Unit References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualUnit(AttackableUnit LeftHandSide, AttackableUnit RightHandSide)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is NOT equal to RightHandSide, and FAILURE if it is not. This version is for Unit References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool NotEqualUnit(AttackableUnit LeftHandSide, AttackableUnit RightHandSide)
    {
        return LeftHandSide != RightHandSide;
    }
    /// <summary>Takes a value which is meant to be updated at the constant tick rate of the behavior tree, and normalizes it to match. If you expect your value you to be updated 10 times per second, but the behavior tree may not actually update at this rate, so this causes us to match to that rate.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>

    protected bool NormalizeFloatToTickRate(out float Output, float LeftHandSide = 0)
    {
        Output = LeftHandSide; // For now, just assign the same value
        return true;
    }
    /// <summary>Returns SUCCESS if LeftHandSide is equal to RightHandSide, and FAILURE if it is not. This version is for Unit type References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool EqualUnitType(UnitType LeftHandSide = UnitType.UNKNOWN_UNIT, UnitType RightHandSide = UnitType.UNKNOWN_UNIT)
    {
        return LeftHandSide == RightHandSide;
    }
    /// <summary>Sequence blocks will tick their children in order until one returns a FAILURE, at which point the node will return FAILURE.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return SUCCESS the node will return SUCCESS. Generally alternate these with selector nodes.</summary>


    protected bool Sequence()
    {
        return false;
    }
    /// <summary>Selector blocks will tick their children in order until one returns a SUCCESS, at which point the node will return SUCCESS.  If a child return RUNNING then the node will return RUNNING and execute that child first next tick.  If all children return FAILURE the node will return FAILURE. Generally alternate these with sequence nodes.</summary>


    protected bool Selector()
    {
        return false;
    }
    /// <summary>Decorator that masks FAILURE. Running still returns running.</summary>


    protected bool MaskFailure()
    {
        return true;
    }

    /// <summary>Sets OutputRef with the value of Input. This version is for Unsigned Int References</summary>

    /// <param name="Input">Source Reference</param>
    /// <param name="Output">Destination Reference</param>

    protected bool SetVarUnsignedInt(out uint Output, uint Input = 0)
    {
        Output = default;
        return false;
    }


    /// <summary>Returns SUCCESS if LeftHandSide is greater than or equal to RightHandSide, and FAILURE if it is not. This version is for float References.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the comparison</param>
    /// <param name="RightHandSide">RightHandSide Reference of the comparison</param>

    protected bool GreaterEqualFloat(float LeftHandSide = 0, float RightHandSide = 0)
    {

        return LeftHandSide >= RightHandSide;
    }


    /// <summary>Takes the LeftHandSide and places its absolute value in Output. This version is for Ints.  This will always return SUCCESS.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>

    protected bool AbsInt(out int Output, int LeftHandSide = 0)
    {
        Output = Math.Abs(LeftHandSide);
        return true;
    }


    /// <summary>Takes the LeftHandSide and places its absolute value in Output. This version is for Floats.  This will always return SUCCESS.</summary>

    /// <param name="LeftHandSide">LeftHandSide Reference of the operation</param>
    /// <param name="Output">Output reference of the operation</param>

    protected bool AbsFloat(out float Output, float LeftHandSide = 0)
    {
        Output = Math.Abs(LeftHandSide);
        return true;
    }


    protected bool FloorFloat(out int Output, float Left = 0)
    {

        Output = (int)MathF.Floor(Left);
        return false;
    }


    protected bool SpawnAttackableLevelProp(Vector3 pos, string stringofprop = "")
    {
        return false;
    }
    protected bool CapturePoint_SetValue(int currentid, float progress, TeamId teamowner)
    {
        return false;
    }

    protected bool CapturePoint_SetRate(int CapturePointID, float CaptureRate, int AttackerCount)
    {
        return false;
    }
    protected bool Announcement_OnCapturePointCaptured_odin(TeamId team, int CapturePointID)
    {
        return false;
    }

    protected bool Announcement_OnCapturePointNeutralized_odin(TeamId team, int CapturePointID)
    {
        return false;
    }

    protected bool ModifyDebugCircleColor(int currentid, Color thecolor)
    {
        return false;
    }
    protected bool RemoveDebugCircle(int theid)
    {

        var p1 = Game.ObjectManager.GetObjectById((uint)theid);
        if (p1 != null)
        {
            ModifyDebugCircleRadiusNotify(0, 0, (int)p1.NetId, 0);
            (p1 as Particle).SetToRemove();

            return true;
        }
        else
        {
            return true;
        }
    }

    protected bool IncPARbt(AttackableUnit theunit, float delta, PrimaryAbilityResourceType par_neccesary)
    {
        FLS.IncPAR(theunit, delta, par_neccesary);
        return true;
    }


    ///hack
    ///we use this for give buff for annie or she never attack 
    /// <summary>Disables the HUD used for the end of game.  Not guaranteed to work anywhere else.</summary>


    protected bool Addbuffhack(AttackableUnit theunit)
    {
        FLS.SpellBuffAdd(theunit as ObjAIBase, theunit, "Pyromania_particle", 5, 1, 25000.0f, null, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        return true;
    }


    protected bool IsTargetInFrontfunc(out bool IsTargetInFront, AttackableUnit self, AttackableUnit theunit)
    {
        //todo understand this shit 
        IsTargetInFront = false;//( FCS.IsInFront(self, theunit) && (self == (theunit as ObjAIBase).TargetUnit) );
        return IsTargetInFront;
    }

    /// <summary>
    /// Cherche l'owner dans la hiérarchie des classes parentes
    /// </summary>
    private ObjAIBase? FindOwnerInHierarchy()
    {
        return Owner;
    }

    /// <summary>
    /// Vérifie si un champion est près d'une tour ennemie
    /// </summary>
    private bool IsNearEnemyTower(Champion champ)
    {
        try
        {
            // Logique simplifiée - à adapter selon vos besoins
            IEnumerable<LaneTurret> enemyTurrets = default;
            GetTurretCollection(out enemyTurrets);
            if (enemyTurrets != null)
            {
                foreach (var turret in enemyTurrets)
                {
                    if (turret.Team != champ.Team &&
                        Vector3.Distance(champ.Position3D, turret.Position3D) < 800f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Vérifie si un champion est près d'une tour alliée
    /// </summary>
    private bool IsNearAllyTower(Champion champ)
    {
        try
        {
            // Logique simplifiée - à adapter selon vos besoins
            IEnumerable<LaneTurret> enemyTurrets = default;
            GetTurretCollection(out enemyTurrets);
            if (enemyTurrets != null)
            {
                foreach (var turret in enemyTurrets)
                {
                    if (turret.Team == champ.Team &&
                        Vector3.Distance(champ.Position3D, turret.Position3D) < 800f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Vérifie si un champion est dans la jungle
    /// </summary>
    private bool IsInJungle(Champion champ)
    {

        //todo integrate jungle zone from ainavgrid 
        return false;
        /* try
         {
             // Logique simplifiée - à adapter selon vos besoins
             // Vérifier si le champion est dans une zone de jungle
             var position = champ.Position3D;

             // Zones de jungle approximatives (à ajuster selon votre map)
             bool inTopJungle = position.X > 4000 && position.X < 8000 && position.Z > 4000 && position.Z < 8000;
             bool inBotJungle = position.X > 4000 && position.X < 8000 && position.Z > 12000 && position.Z < 16000;

             return inTopJungle || inBotJungle;
         }
         catch
         {
             return false;
         }*/
    }

    /// <summary>
    /// Récupère le champion ennemi le plus proche
    /// </summary>
    private AttackableUnit GetNearestEnemyChampion(Champion champ)
    {
        try
        {
            IEnumerable<Champion> champions = default;
            GetChampionCollection(out champions);
            if (champions != null)
            {
                AttackableUnit nearest = null;
                float minDistance = float.MaxValue;

                foreach (var enemy in champions)
                {
                    if (enemy.Team != champ.Team && !enemy.Stats.IsDead)
                    {
                        float distance = Vector3.Distance(champ.Position3D, enemy.Position3D);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearest = enemy;
                        }
                    }
                }

                return nearest;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}


