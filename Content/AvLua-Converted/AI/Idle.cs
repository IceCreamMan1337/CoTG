using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class IdleAI : CAIScript
{

    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        return false;
    }

    bool OnTargetLost()
    {
        return false;
    }

    void OnCallForHelp(object param1, object param2)
    {
        // Placeholder function
    }

    void OnTauntBegin()
    {
        // Placeholder function
    }

    void OnTauntEnd()
    {
        // Placeholder function
    }

    void OnCanMove()
    {
        // Placeholder function
    }

    void OnCanAttack()
    {
        // Placeholder function
    }

    void TimerFindEnemies()
    {
        // Placeholder function
    }

    void HaltAI()
    {
        // Placeholder function
    }
}

