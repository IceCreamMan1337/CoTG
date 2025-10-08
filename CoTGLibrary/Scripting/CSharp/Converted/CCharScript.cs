#nullable enable

using CoTG.CoTGServer.API;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.SpellNS;
using CoTG.CoTGServer.Scripting.Lua;

namespace CoTG.CoTGServer.Scripting.CSharp.Converted;

public class CCharScript : CScript, ICharScriptInternal
{
    protected int level => Functions.GetLevel(owner); //TODO: Make a normal way to get the level
    protected new ObjAIBase owner => _owner;

    private ObjAIBase _owner = null!;
    public void Init(ObjAIBase owner, Spell spell)
    {
        base.Init(owner, owner, null);
        base.Activate(); //TODO: Move to Activate
        _owner = owner;
        //_spell = spell;
        ApiEventManager.OnLevelUp.AddListener(this, owner, unit => SetVarsByLevel()); //TODO: Move to Activate
        SetVarsByLevel();
        //test

    }

    [BBCall("SetVarsByLevel")] public virtual void SetVarsByLevel() { }

    public virtual void OnActivate()
    {
    }

    public virtual void OnDeactivate()
    {
    }
}