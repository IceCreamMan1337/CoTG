using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class MinionReferenceUpdateClass : AI_Characters 
{
      

     public bool MinionReferenceUpdate(
         out Vector3 _SelfPosition, //float ?? rito 
      AttackableUnit Self
         )
      {
        Vector3 SelfPosition = default;  //float ?? rito
        bool result =
              // Sequence name :MinionReferenceUpdate
              (
                    GetUnitPosition(
                          out SelfPosition,
                          Self)

              );



        _SelfPosition = SelfPosition;
        return result;
      }
}

