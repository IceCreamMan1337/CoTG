using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


class FindGuardianByPositionClass : IODIN_MinionAIBT
{
    private FindClosestTargetClass findClosestTargetclass = new FindClosestTargetClass();

    public bool FindGuardianByPosition(out AttackableUnit Guardian,
    Vector3 Position,
       ObjAIBase self)
    {
        Self = self;
        AttackableUnit _Guardian = default;
        //  var findGuardianByPosition = new FindGuardianByPositionClass();

        bool result =
              // Sequence name :Sequence
              (
                  /* GetUnitAISelf(
                         out Self) && */
                  GetUnitsInTargetArea(
                        out UnitsToSearch,
                        Self,
                        Position,
                        3000,
                        AffectEnemies | AffectFriends | AffectMinions | AffectNeutral | AffectUseable) &&
                  GetCollectionCount(
                        out total,
                        UnitsToSearch) &&
                  findClosestTargetclass.FindClosestTarget(
                        out ClosestEnemyGuardian,
                        UnitsToSearch,
                        Self
                        ,
                        false,
                        null,
                        // Position, 
                        true
                       // OdinGuardianBuff, 
                       // true)
                       )
                  &&
                  SetVarAttackableUnit(
                        out _Guardian,
                        ClosestEnemyGuardian)

            );
        Guardian = _Guardian;
        return result;
    }
}