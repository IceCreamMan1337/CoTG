namespace AIScripts;

using ChildrenOfTheGraveEnumNetwork.Enums;

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

//Status: 100% Identical to Lua script
public class IdleAIold: CAIScript
{
    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        return false;
    }
}