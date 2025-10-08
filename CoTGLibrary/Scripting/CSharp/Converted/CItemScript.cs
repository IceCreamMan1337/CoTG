#nullable enable

using CoTG.CoTGServer.API;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.Scripting.CSharp.Converted;

public class CItemScript : CScript, IItemScriptInternal
{
    protected new ObjAIBase owner => _owner;
    protected int slot => 0;

    public virtual void OnActivate()
    {
    }
    public virtual void OnDeactivate()
    {
    }

    private ObjAIBase _owner = null!;
    public virtual void OnActivate(ObjAIBase owner)
    {
        base.Init(owner, owner, null); //TODO: Move to Init
        base.Activate();
        _owner = owner;
        OnActivate();
    }

    public virtual void OnDeactivate(ObjAIBase owner)
    {
        //TODO: Next MR will move this into CScript/CScriptBase to do it auto
        ApiEventManager.RemoveAllListenersForOwner(this);
        OnDeactivate();
    }
}