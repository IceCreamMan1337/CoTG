#nullable enable

using CoTG.CoTGServer.API;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.Scripting.Lua;

namespace CoTG.CoTGServer.Scripting.CSharp.Converted;

public class CTalentScript : CScript, ITalentScriptInternal
{
    protected int talentLevel => _level;
    protected new ObjAIBase owner => _owner; //TODO:

    private int _level;
    private ObjAIBase _owner = null!; //TODO: Champion?
    public void OnActivate(ObjAIBase owner, byte rank)
    {
        base.Init(owner, owner, null); //TODO: Move to Init
        base.Activate();
        _owner = owner;
        _level = rank;
        ApiEventManager.OnLevelUp.AddListener(this, owner, unit => SetVarsByLevel());
    }

    [BBCall("SetVarsByLevel")] public virtual void SetVarsByLevel() { }
}