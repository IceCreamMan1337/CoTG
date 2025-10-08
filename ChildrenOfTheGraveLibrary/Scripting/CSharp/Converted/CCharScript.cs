#nullable enable

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.API;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;

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