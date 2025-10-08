using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class OdinTowerAttackTargetClass : OdinLayout 
{


     public bool OdinTowerAttackTarget(
                TeamId SelfTeam,
    AttackableUnit Self
          )
      {
        var odinTowerAutoAttackTarget = new OdinTowerAutoAttackTargetClass();
      return
            // Sequence name :OdinTowerAttackTarget
            (
                  GetUnitAIAttackTarget(
                        out Target) &&
                  GetUnitTeam(
                        out TargetTeam, 
                        Target) &&
                  NotEqualUnitTeam(
                        TargetTeam, 
                        SelfTeam) &&
                  
                        odinTowerAutoAttackTarget.OdinTowerAutoAttackTarget(
                        Target, 
                        Self)

            );
      }
}

