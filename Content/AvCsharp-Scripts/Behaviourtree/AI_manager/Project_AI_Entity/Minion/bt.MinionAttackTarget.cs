using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MinionAttackTargetClass : AI_Characters 
{

    private MinionAutoAttackTargetClass minionAutoAttackTarget = new MinionAutoAttackTargetClass();


     public bool MinionAttackTarget(
         TeamId SelfTeam,
      AttackableUnit Self
         )
      {
        return
              // Sequence name :MinionAttackTarget
              (
                    GetUnitAIAttackTarget(
                          out Target) &&
                    GetUnitTeam(
                          out TargetTeam,
                          Target) &&
                    NotEqualUnitTeam(
                          TargetTeam,
                          SelfTeam) &&
                  minionAutoAttackTarget.MinionAutoAttackTarget(
                          Target,
                          Self)

              );
      }
}

