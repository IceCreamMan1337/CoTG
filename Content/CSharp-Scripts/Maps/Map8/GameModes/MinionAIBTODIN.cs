using BehaviourTrees.all;

namespace BehaviourTrees.Map8.AI_manager;

class MinionAIBTODIN : BehaviourTree
{
    // private DominionAIManager_LogicClass dominionAIManager_LogicClass = new DominionAIManager_LogicClass();
    private OdinGolemBomb_SingleRunLogicClass odinGolemBomb_SingleRunLogicClass = new();
    private MinionBehaviorClass minionBehaviorClass = new();
    private MinionInitClass minionInitClass = new();
    private OdinGolemBombLogicClass odinGolemBombLogicClass = new();
    public MinionAIBTODIN()
    {
    }
    public MinionAIBTODIN(Minion owner) : base(owner)
    {
        bool OdinGolemBomb_SingleRunLogic_firsttime = true;

    }

    AttackableUnit Self;
    Vector3 AssistPosition;
    TeamId SelfTeam;

    public override void Update()
    {
        GetUnitAISelf(out Self);
        if (Self == null)
        {
            return;
        }
        base.Update();
        MinionsBehavior();
    }
    bool MinionsBehavior()
    {
        if
        (
            minionInitClass.MinionInit(out AttackableUnit _Self, out Vector3 _AssistPosition, out TeamId _SelfTeam, this.Owner) &&
            (
                odinGolemBomb_SingleRunLogicClass.OdinGolemBomb_SingleRunLogic(this.Owner) ||
                minionBehaviorClass.MinionBehavior(this.Owner) /* &&
                odinGolemBombLogicClass.OdinGolemBombLogic(this.Owner) */

            )
        )
        {
            Self = _Self;
            AssistPosition = _AssistPosition;
            SelfTeam = _SelfTeam;
            return true;
        }
        else
        {
            Self = _Self;
            AssistPosition = _AssistPosition;
            SelfTeam = _SelfTeam;
            return true;
        }


    }
}