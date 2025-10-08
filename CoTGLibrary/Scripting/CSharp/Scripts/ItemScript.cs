using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;
using CoTG.CoTGServer.GameObjects.StatsNS;

namespace CoTG.CoTGServer.Scripting.CSharp
{
    public interface IItemScript
    {
    }
    internal interface IItemScriptInternal : IItemScript
    {
        public StatsModifier StatsModifier { get; }

        public void OnActivate(ObjAIBase owner);

        public void OnDeactivate(ObjAIBase owner);

        public void OnUpdate() { }
        public void OnUpdateStats() { }
    }
    public class ItemScript : IItemScriptInternal
    {
        public virtual StatsModifier StatsModifier { get; } = new();

        public virtual void OnActivate(ObjAIBase owner)
        {
        }
        public virtual void OnDeactivate(ObjAIBase owner)
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
