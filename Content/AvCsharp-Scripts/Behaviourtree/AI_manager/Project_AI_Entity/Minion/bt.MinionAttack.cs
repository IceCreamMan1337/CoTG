using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MinionAttackClass : AI_Characters 
{
    private MinionAcquireTargetClass minionAcquireTarget = new MinionAcquireTargetClass();
    private MinionAttackTargetClass minionAttackTarget = new MinionAttackTargetClass();
    public bool MinionAttack(
         AttackableUnit Self,
      TeamId SelfTeam,
      Vector3 SelfPosition,
      Vector3 AssistPosition
         )
      {
        return
              // Sequence name :MinionAttack
              (
                 minionAcquireTarget.MinionAcquireTarget(
                          Self,
                          AssistPosition,
                          SelfPosition) &&
                minionAttackTarget.MinionAttackTarget(
                          SelfTeam,
                          Self)

              );
      }
}

