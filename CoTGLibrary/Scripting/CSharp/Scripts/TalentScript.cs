using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.Scripting.CSharp
{
    public interface ITalentScript
    {
    }
    internal interface ITalentScriptInternal : ITalentScript
    {
        public void OnActivate(ObjAIBase owner, byte rank);
        public void OnUpdateStats();
    }
    public class TalentScript : ITalentScriptInternal
    {
        public virtual void OnActivate(ObjAIBase owner, byte rank)
        {
        }
        public virtual void OnUpdateStats()
        {
        }
    }
}