using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetCapturePointOwnerClass : IODIN_MinionAIBT
{


    public bool GetCapturePointOwner(
      out TeamId Owner,
      int CapturePointIndex,
                ObjAIBase self)
    {
        var findGuardianByIndex = new FindGuardianByIndexClass();
        TeamId _owner = default;

        bool result =
        // Sequence name :Sequence
        (
              findGuardianByIndex.FindGuardianByIndex(
                    out Guardian,
                    CapturePointIndex,
                    self) &&
              GetUnitTeam(
                    out _owner,
                    Guardian)

        );
        Owner = _owner;
        return result;

    }
}
