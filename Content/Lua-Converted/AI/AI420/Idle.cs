namespace AIScripts;

using CoTG.CoTGServer.Scripting.CSharp.Converted;
using CoTGEnumNetwork.Enums;

//Status: 100% Identical to Lua script
public class IdleAIold : CAIScript
{
    public override bool OnInit()
    {
        SetState(AIState.AI_HARDIDLE);
        return false;
    }
}