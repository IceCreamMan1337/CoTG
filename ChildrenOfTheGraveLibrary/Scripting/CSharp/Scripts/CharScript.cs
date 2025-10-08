using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp
{
    public interface ICharScript
    {
    }
    internal interface ICharScriptInternal : ICharScript
    {
        public void Init(ObjAIBase owner, Spell spell);
        public void OnActivate() { }
        public void OnDeactivate() { }
        public void OnUpdate() { }
        public void OnUpdateStats() { }
    }
    public class CharScript : ICharScriptInternal
    {
        public ObjAIBase Owner { get; private set; }
        public Spell Spell { get; private set; }

        public void Init(ObjAIBase owner, Spell spell)
        {
            Owner = owner;
            Spell = spell;
        }

        public virtual void OnActivate()
        {
        }
        public virtual void OnDeactivate()
        {
        }
        public virtual void OnUpdateStats()
        {
        }
        public virtual void OnUpdate()
        {
        }
    }
}
