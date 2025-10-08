using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class InitializeGuardianParticlesClass : OdinLayout
{
    private GetGuardianClass getGuardian = new GetGuardianClass();
    public bool InitializeGuardianParticles() {

        return 
            // Sequence name :MaskFailure
            (
                  // Sequence name :Sequence
                  (
                        SetVarBool(
                              out DidInitParticles, 
                              true) &&
                        getGuardian.GetGuardian(
                              out Guardian, 
                              0) &&
                        AttachCapturePointToIdleParticles(
                              Guardian, 
                              0, 
                              0) &&
                        CapturePoint_AttachToObject(
                              0, 
                              Guardian, 
                              PrimaryAbilityResourceType.MANA) &&
                        getGuardian.GetGuardian(
                              out Guardian, 
                              1) &&
                        AttachCapturePointToIdleParticles(
                              Guardian, 
                              0, 
                              1) &&
                        CapturePoint_AttachToObject(
                              1, 
                              Guardian,
                              PrimaryAbilityResourceType.MANA) &&
                        getGuardian.GetGuardian(
                              out Guardian, 
                              2) &&
                        AttachCapturePointToIdleParticles(
                              Guardian, 
                              0, 
                              2) &&
                        CapturePoint_AttachToObject(
                              2, 
                              Guardian,
                              PrimaryAbilityResourceType.MANA) &&
                        getGuardian.GetGuardian(
                              out Guardian, 
                              3) &&
                        AttachCapturePointToIdleParticles(
                              Guardian, 
                              0, 
                              3) &&
                        CapturePoint_AttachToObject(
                              3, 
                              Guardian,
                              PrimaryAbilityResourceType.MANA) &&
                        getGuardian.GetGuardian(
                              out Guardian, 
                              4) &&
                        AttachCapturePointToIdleParticles(
                              Guardian, 
                              0, 
                              4) &&
                        CapturePoint_AttachToObject(
                              4, 
                              Guardian,
                              PrimaryAbilityResourceType.MANA)

                  )
                  ||
                               DebugAction("MaskFailure")
            );
      }
}

