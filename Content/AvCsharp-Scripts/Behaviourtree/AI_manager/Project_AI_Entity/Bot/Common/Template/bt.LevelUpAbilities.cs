using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class LevelUpAbilitiesClass : AI_Characters 
{
     

     public bool LevelUpAbilities(
          AttackableUnit Self,
      int UnitLevel
         )
      {
        return
              // Sequence name :ReturnFailure
              (
                    SetVarBool(
                          out Run,
                          false) &&
                    Run == true

              );
      }
}

