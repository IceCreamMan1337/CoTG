using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class OdinGuardianBehaviorClass : IStructureAI
{
    private FindClosestTargetClass findClosestTargetClass = new FindClosestTargetClass();
    
    // Variables persistantes - champs de classe
    private AttackableUnit Self;
    private AttackableUnit PreviousTarget;
    private bool PreviousTargetValid = false;
    private float TargetAcquisitionRange = 620f;
    private Vector3 SelfPosition;
    private float CurrentTime;
    private float TargetAcquisitionTime;
    private float TimeToFireFirstShot = 1f;
    private float AttackInterval = 1.2f;
    private float PreviousAttackTime = 0f;
    private TeamId MyTeam;
    private TeamId TargetTeam;
    private float Distance;
    private UnitType TargetUnitTeam;
    private float TimePassed;
    private bool Run;
    private IEnumerable<AttackableUnit> Friends;
    private float ClosestEnemyDistance;
    private AttackableUnit ClosestAttacker;
    private AttackableUnit Attacker;
    private float CurrentDistance;
    private AttackableUnit CFHTarget;
    private AttackableUnit CFHCaller;
    private AttackableUnit ClosestMinion;
    private IEnumerable<AttackableUnit> Enemies;
    private AttackableUnit ClosestChampion;
    
    public bool OdinGuardianBehavior(ObjAIBase owner)
    {
        this.Owner = owner;
        return   // Sequence name :Behavior
            (
                  // Sequence name :Initialization
                  (
                        TestUnitAIFirstTime() &&
                        DebugAction("OdinGuardianBehavior: Initialization started") &&
                        GetUnitAISelf(
                              out Self) &&
                        SetVarAttackableUnit(
                              out PreviousTarget,
                              Self) &&
                        SetVarBool(
                              out PreviousTargetValid,
                              false) &&
                        SetVarFloat(
                              out TargetAcquisitionRange,
                              620f) &&
                        GetUnitPosition(
                              out SelfPosition,
                              Self) &&
                        GetGameTime(
                              out CurrentTime) &&
                        AddFloat(
                              out TargetAcquisitionTime,
                              CurrentTime,
                              25000) &&
                        SetVarFloat(
                              out TimeToFireFirstShot,
                              1f) &&
                        SetVarFloat(
                              out AttackInterval,
                              1.2f) &&
                        SetVarFloat(
                              out PreviousAttackTime,
                              0f) &&
                        DebugAction($"OdinGuardianBehavior: Initialization completed - Team: {Self.Team}, Position: {SelfPosition}, AcquisitionRange: {TargetAcquisitionRange}")
                  ) ||
                  // Sequence name :Actions
                  (
                        DebugAction("OdinGuardianBehavior: Actions sequence started") &&
                        GetUnitTeam(
                              out MyTeam,
                              Self) &&
                        DebugAction($"OdinGuardianBehavior: Team = {MyTeam}, PreviousTargetValid = {PreviousTargetValid}") &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    PreviousTargetValid == true &&
                                    DebugAction("OdinGuardianBehavior: Checking previous target validity") &&
                                    // Sequence name :Selector
                                    (
                                          TestUnitCondition(
                                                PreviousTarget, false) &&
                                          DebugAction("OdinGuardianBehavior: Previous target condition failed")
                                          ||
                                          TestUnitIsTargetable(
                                                PreviousTarget,
                                                false) &&
                                          DebugAction("OdinGuardianBehavior: Previous target not targetable")
                                          ||
                                          // Sequence name :SameTeams
                                          (
                                                GetUnitTeam(
                                                      out TargetTeam,
                                                      PreviousTarget) &&
                                                TargetTeam == MyTeam &&
                                                DebugAction("OdinGuardianBehavior: Previous target is same team")
                                          ) ||
                                          // Sequence name :Sequence
                                          (
                                                DistanceBetweenObjectAndPoint(
                                                      out Distance,
                                                      PreviousTarget,
                                                      SelfPosition) &&
                                                GreaterFloat(
                                                      Distance,
                                                      TargetAcquisitionRange) &&
                                                DebugAction($"OdinGuardianBehavior: Previous target too far - Distance: {Distance}, Range: {TargetAcquisitionRange}")
                                          )
                                    ) &&
                                    SetVarBool(out PreviousTargetValid, false) &&
                                    ClearUnitAIAttackTarget() &&
                                    DebugAction("OdinGuardianBehavior: Previous target invalidated")
                              ) ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Previous target check")
                        ) &&
                        DebugAction("OdinGuardianBehavior: After MaskFailure check") &&
                        // Sequence name :Actions
                        (
                              // Sequence name :GuardianNeutral
                              (
                                    MyTeam == TeamId.TEAM_NEUTRAL &&
                                    DebugAction("OdinGuardianBehavior: Neutral guardian - clearing targets") &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          false) &&
                                    ClearUnitAIAttackTarget()
                              ) ||
                              DebugAction($"OdinGuardianBehavior: Not neutral guardian - Team: {MyTeam}") ||
                              // Sequence name :BeingSuppressed
                              (
                                    TestUnitHasBuff(
                                          Self,
                                          null,
                                          "OdinGuardianSuppression") &&
                                    DebugAction("OdinGuardianBehavior: Being suppressed - clearing targets") &&
                                    ClearUnitAIAttackTarget() &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          false) &&
                                    GetGameTime(
                                          out TargetAcquisitionTime) &&
                                    ClearUnitAIAttackTarget()
                              ) ||
                              DebugAction("OdinGuardianBehavior: Not being suppressed") ||
                              // Sequence name :Attack_PreviousTargetIsValid_&amp;&amp;_IsChampion
                              (
                                    PreviousTargetValid == true &&
                                    GetUnitType(
                                          out TargetUnitTeam,
                                          PreviousTarget) &&
                                    TargetUnitTeam == HERO_UNIT &&
                                    DebugAction("OdinGuardianBehavior: Attacking previous champion target") &&
                                    SetAIUnitSpellTarget(
                                          PreviousTarget,
                                          SPELLBOOK_CHAMPION,
                                          0) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetGameTime(
                                                      out CurrentTime) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      TargetAcquisitionTime) &&
                                                GreaterEqualFloat(
                                                      TimePassed,
                                                      TimeToFireFirstShot) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      PreviousAttackTime) &&
                                                GreaterEqualFloat(
                                                      TimePassed,
                                                      AttackInterval) &&
                                                DebugAction($"OdinGuardianBehavior: Firing at champion - TimePassed: {TimePassed}, Interval: {AttackInterval}") &&
                                                CastUnitSpell(
                                                      Self,
                                                      SPELLBOOK_CHAMPION,
                                                      0,
                                                      1
                                                      ) &&
                                                SetVarFloat(
                                                      out PreviousAttackTime,
                                                      CurrentTime)
                                          )
                                          ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Champion attack")
                                    )
                              ) ||
                              // Sequence name :Acquisition_FakeCallForHelp
                              (
                                    DebugAction("OdinGuardianBehavior: Checking for call for help") &&
                                    SetVarBool(
                                          out Run,
                                          true) &&
                                    Run == true &&
                                    GetUnitsInTargetArea(
                                          out Friends,
                                          Self,
                                          SelfPosition,
                                          2000,
                                          AffectFriends | AffectHeroes | NotAffectSelf) &&
                                    SetVarFloat(
                                          out ClosestEnemyDistance,
                                          50000f) &&
                                    SetVarAttackableUnit(
                                          out ClosestAttacker,
                                          Self) &&
                                    ForEach(Friends, Friend => (
                                          // Sequence name :Sequence
                                          (
                                                TestUnitHasBuff(
                                                      Friend,
                                                      null,
                                                      "CallForHelp") &&
                                                GetUnitBuffCaster(
                                                      out Attacker,
                                                      Friend,
                                                      "CallForHelp") &&
                                                TestUnitCondition(
                                                      Attacker) &&
                                                GetDistanceBetweenUnits(
                                                      out CurrentDistance,
                                                      Attacker,
                                                      Self) &&
                                                LessEqualFloat(
                                                      CurrentDistance,
                                                      ClosestEnemyDistance) &&
                                                SetVarFloat(
                                                      out ClosestEnemyDistance,
                                                      CurrentDistance) &&
                                                SetVarAttackableUnit(
                                                      out ClosestAttacker,
                                                      Attacker)
                                          ))
                                    ) &&
                                    LessEqualFloat(
                                          ClosestEnemyDistance,
                                          TargetAcquisitionRange) &&
                                    DebugAction($"OdinGuardianBehavior: Call for help target acquired - Distance: {ClosestEnemyDistance}") &&
                                    SetVarAttackableUnit(
                                          out PreviousTarget,
                                          ClosestAttacker) &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          true) &&
                                    GetGameTime(
                                          out CurrentTime) &&
                                    SubtractFloat(
                                          out TargetAcquisitionTime,
                                          CurrentTime,
                                          30) &&
                                    StopUnitMovement(
                                          Self) &&
                                    ClearUnitAIAttackTarget()
                              ) ||
                              // Sequence name :Acquisition_CallForHelp
                              (
                                    DebugAction("OdinGuardianBehavior: Checking direct call for help") &&
                                    SetVarBool(
                                          out Run,
                                          false) &&
                                    Run == true &&
                                    TestHaveCallForHelpTarget(
                                          true) &&
                                    GetUnitCallForHelpTargetUnit(
                                          out CFHTarget) &&
                                    GetUnitCallForHelpCallerUnit(
                                          out CFHCaller) &&
                                    GetDistanceBetweenUnits(
                                          out Distance,
                                          Self,
                                          CFHTarget) &&
                                    LessEqualFloat(
                                          Distance,
                                          TargetAcquisitionRange) &&
                                    DebugAction($"OdinGuardianBehavior: Direct call for help target - Distance: {Distance}") &&
                                    SetUnitAIAttackTarget(
                                          CFHTarget) &&
                                    IssueAttackOrder() &&
                                    SetVarAttackableUnit(
                                          out PreviousTarget,
                                          CFHTarget) &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          true)
                              ) ||
                              // Sequence name :Attack_PreviousTargetIsValid_&amp;&amp;_IsMinion
                              (
                                    PreviousTargetValid == true &&
                                    GetUnitType(
                                          out TargetUnitTeam,
                                          PreviousTarget) &&
                                    TargetUnitTeam == MINION_UNIT &&
                                    DebugAction("OdinGuardianBehavior: Attacking previous minion target") &&
                                    SetAIUnitSpellTarget(
                                          PreviousTarget,
                                          SPELLBOOK_CHAMPION,
                                          0) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetGameTime(
                                                      out CurrentTime) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      TargetAcquisitionTime) &&
                                                GreaterEqualFloat(
                                                      TimePassed,
                                                      TimeToFireFirstShot) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      PreviousAttackTime) &&
                                                GreaterEqualFloat(
                                                      TimePassed,
                                                      AttackInterval) &&
                                                DebugAction($"OdinGuardianBehavior: Firing at minion - TimePassed: {TimePassed}, Interval: {AttackInterval}") &&
                                                CastUnitSpell(
                                                      Self,
                                                      SPELLBOOK_CHAMPION,
                                                      0,
                                                      1
                                                      ) &&
                                                SetVarFloat(
                                                      out PreviousAttackTime,
                                                      CurrentTime)
                                          )
                                          ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Minion attack")
                                    )
                              ) ||
                              // Sequence name :Acquisition_ClosestEnemyMinion
                              (
                                    DebugAction("OdinGuardianBehavior: Searching for closest enemy minion") &&
                                    GetUnitAIClosestTargetInArea(
                                          out ClosestMinion,
                                          Self,
                                          null,
                                          false,
                                          SelfPosition,
                                          TargetAcquisitionRange,
                                          AffectEnemies | AffectMinions) &&
                                    GetUnitsInTargetArea(
                                          out Enemies,
                                          Self,
                                          SelfPosition,
                                          750,
                                          AffectEnemies | AffectMinions) &&
                                    findClosestTargetClass.FindClosestTarget(
                                          out ClosestMinion,
                                          Enemies,
                                          Self,
                                          false,
                                          null,
                                          false) &&
                                    DebugAction($"OdinGuardianBehavior: Closest minion found at distance {Vector2.Distance(SelfPosition.ToVector2(), ClosestMinion.Position)}") &&
                                    SetVarAttackableUnit(
                                          out PreviousTarget,
                                          ClosestMinion) &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          true) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetGameTime(

                                                      out CurrentTime) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      TargetAcquisitionTime) &&
                                                LessFloat(
                                                      TimePassed,
                                                      0) &&
                                                SubtractFloat(
                                                      out TargetAcquisitionTime,
                                                      CurrentTime,
                                                      30)
                                          )
                                          ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Minion acquisition")
                                    )
                              ) ||
                              // Sequence name :Acquisition_ClosestEnemyChampion
                              (
                                    DebugAction("OdinGuardianBehavior: Searching for closest enemy champion") &&
                                    GetUnitAIClosestTargetInArea(
                                          out ClosestChampion,
                                          Self,
                                          null,
                                          false,
                                          SelfPosition,
                                          TargetAcquisitionRange,
                                          AffectEnemies | AffectHeroes | AffectMinions | NotAffectSelf) &&
                                    GetUnitsInTargetArea(
                                          out Enemies,
                                          Self,
                                          SelfPosition,
                                          750,
                                          AffectEnemies | AffectHeroes) &&
                                    findClosestTargetClass.FindClosestTarget(
                                          out ClosestChampion,
                                          Enemies,
                                          Self,
                                          false,
                                          null,
                                          false) &&
                                    DebugAction($"OdinGuardianBehavior: Closest champion found at distance {Vector2.Distance(SelfPosition.ToVector2(), ClosestChampion.Position)}") &&
                                    SetVarAttackableUnit(
                                          out PreviousTarget,
                                          ClosestChampion) &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          true) &&
                                    // Sequence name :MaskFailure
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetGameTime(

                                                      out CurrentTime) &&
                                                SubtractFloat(
                                                      out TimePassed,
                                                      CurrentTime,
                                                      TargetAcquisitionTime) &&
                                                LessFloat(
                                                      TimePassed,
                                                      0) &&
                                                GetGameTime(

                                                      out TargetAcquisitionTime)
                                          )
                                          ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Champion acquisition")
                                    )
                              ) ||
                              // Sequence name :InvalidatePreviousTarget
                              (
                                    DebugAction("OdinGuardianBehavior: Invalidating previous target") &&
                                    SetVarBool(
                                          out PreviousTargetValid,
                                          false) &&
                                    ClearUnitAIAttackTarget() &&
                                    GetGameTime(

                                          out CurrentTime) &&
                                    AddFloat(
                                          out TargetAcquisitionTime,
                                          CurrentTime,
                                          25000)
                              )
                        ) &&
                        // Sequence name :MaskFailure
                        (
                              // Sequence name :Sequence
                              (
                                    GetUnitsInTargetArea(
                                          out Friends,
                                          Self,
                                          SelfPosition,
                                          TargetAcquisitionRange,
                                          AffectFriends | AffectHeroes
                                          ) &&
                                    ForEach(Friends, Friend => (
                                          SpellBuffClear(
                                                Self,
                                                "CallForHelp"))
                                    )
                              )
                              ||
                               DebugAction("OdinGuardianBehavior: MaskFailure - Clear call for help")
                        )
                  )
            );
    }
}