using System.Numerics;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp
{

    public interface IAIScript
    {
        public AIScriptMetaData AIScriptMetaData { get; }
    }

    public interface IAIScriptInternal : IAIScript
    {
        public void Init(ObjAIBase owner);
        public void Activate();
        public void HaltAI();
        public void Deactivate(bool expired);
        public void Update();
        public bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position);
        public void TargetLost(LostTargetEvent eventType, AttackableUnit target);
        public void OnAICommand();
        public void OnCollision(AttackableUnit target);
        public void OnCollisionTerrain();
        public void OnStoppedMoving();
        public void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim);
        public void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim);
        public void OnReachedDestinationForGoingToLastLocation();
        public void OnCanAttack();
        public void OnCanMove();
        public void OnStopMove();
        public AttackableUnit? FindTargetInAcR();
    }

    public class AIScript : IAIScriptInternal
    {
        public ObjAIBase Owner { get; internal set; }

        public virtual AIScriptMetaData AIScriptMetaData { get; } = new();

        public virtual void Init(ObjAIBase owner)
        {
        }

        public virtual void Activate()
        {
        }

        public void HaltAI()
        {

        }

        public virtual void Deactivate(bool expired)
        {
        }

        public virtual void Update()
        {
        }

        public virtual bool OnOrder(OrderType orderType, AttackableUnit target, Vector2 position)
        {
            return false;
        }

        public virtual void TargetLost(LostTargetEvent eventType, AttackableUnit target)
        {

        }

        public virtual void OnAICommand()
        {

        }

        public virtual void OnCollision(AttackableUnit target)
        {

        }

        public virtual void OnCollisionTerrain()
        {

        }

        public virtual void OnStoppedMoving()
        {

        }

        public virtual void OnCallForHelp(ObjAIBase attacker, ObjAIBase victim)
        {

        }

        public virtual void OnLeashedCallForHelp(ObjAIBase attacker, ObjAIBase victim)
        {

        }

        public virtual void OnReachedDestinationForGoingToLastLocation()
        {

        }

        public virtual void OnCanAttack()
        {

        }

        public virtual void OnCanMove()
        {

        }

        public virtual void OnStopMove()
        {

        }

        public AttackableUnit? FindTargetInAcR()
        {
            return Owner.TargetAcquisition(GetPosition(), Owner.Stats.AcquisitionRange.Total + Owner.CollisionRadius);
        }


        public Vector2 GetPosition()
        {
            return Owner.Position;
        }
    }
}