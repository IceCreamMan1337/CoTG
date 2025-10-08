using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using System.Numerics;
using BehaviourTrees.Map8;

namespace BehaviourTrees;


class FindGuardianByIndexClass : IODIN_MinionAIBT
{


    public bool FindGuardianByIndex(
       out AttackableUnit Guardian,
       int GuardianIndex,
       ObjAIBase self)
    {

        AttackableUnit _Guardian = default;
        var findGuardianByPosition = new FindGuardianByPositionClass();
        bool result =
            // Sequence name :Sequence
            (
                  // Sequence name :Selector
                  (
                        // Sequence name :Index0
                        (
                              GuardianIndex == 0 &&
                              GetWorldLocationByName(
                                    out GuardianPosition,
                                    "CapturePointA")
                        ) ||
                        // Sequence name :Index1
                        (
                              GuardianIndex == 1 &&
                              GetWorldLocationByName(
                                    out GuardianPosition,
                                    "CapturePointB")
                        ) ||
                        // Sequence name :Index2
                        (
                              GuardianIndex == 2 &&
                              GetWorldLocationByName(
                                    out GuardianPosition,
                                    "CapturePointC")
                        ) ||
                        // Sequence name :Index3
                        (
                              GuardianIndex == 3 &&
                              GetWorldLocationByName(
                                    out GuardianPosition,
                                    "CapturePointD")
                        ) ||
                        // Sequence name :Index4
                        (
                              GuardianIndex == 4 &&
                              GetWorldLocationByName(
                                    out GuardianPosition,
                                    "CapturePointE")
                        )
                  ) &&
                  findGuardianByPosition.FindGuardianByPosition(
                        out _Guardian,
                        GuardianPosition,
                        self)

            );
        Guardian = _Guardian;
        return result;



    }
}