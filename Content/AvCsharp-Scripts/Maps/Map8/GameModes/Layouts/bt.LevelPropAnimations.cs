using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class LevelPropAnimationsClass : OdinLayout 
{

    public bool LevelPropAnimations()
    {

    
      return
            // Sequence name :Sequence
            (
                  GetPropByName(
                        out Bird1,
                        "LevelProp_SwainBeam1") &&
                  PlayAnimationOnProp(
                        Bird1,
                        "PeckA", 
                        true 
                        ) &&
                  GetPropByName(
                        out Bird2,
                        "LevelProp_SwainBeam2") &&
                  PlayAnimationOnProp(
                        Bird2,
                        "PeckB", 
                        true
                        ) &&
                  GetPropByName(
                        out Bird3,
                        "LevelProp_SwainBeam3") &&
                  PlayAnimationOnProp(
                        Bird3,
                        "PeckC", 
                        true
                        ) &&
                  GetPropByName(
                        out Saw2,
                        "LevelProp_OdinRockSaw02") &&
                  PlayAnimationOnProp(
                        Saw2,
                        "Idle2", 
                        true
                        ) &&
                  SetBTInstanceStatus(
                        false,
                        "LevelPropAnimations")

            );
      }
}

