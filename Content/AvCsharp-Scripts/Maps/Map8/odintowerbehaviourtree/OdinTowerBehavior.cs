using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.Map8;


class OdinTowerBehaviorClass : OdinLayout 
{

    private OdinTower_CheckForChampionsClass_forscript odinTower_CheckForChampions = new OdinTower_CheckForChampionsClass_forscript();
    private OdinTowerCanCastAbility0Class odinTowerCanCastAbility0 = new OdinTowerCanCastAbility0Class();
    private OdinTowerCastAbility0Class odinTowerCastAbility0 = new OdinTowerCastAbility0Class();

    // Variables persistantes - champs de classe
    private Vector3 AssistPosition;
    private TeamId SelfTeam;
    private float AcquisitionRange = 600f;
    private float AttackRadius = 600f;
    private float DefenseRadius = 700f;
    private Vector3 SelfPosition;
    private int FriendlyChampCount;
    private bool LostAggro = false;
    private AttackableUnit AggroTarget;
    private UnitType AggroTargetType;
    private Vector3 AggroPosition;
    private float Distance;
    private IEnumerable<AttackableUnit> EnemyUnitsNearSelf;
    private float CurrentClosestDistance;
    private int EnemyCount;
    private bool TargetFound = false;
    private AttackableUnit CurrentClosestTarget;
    private AttackableUnit Target;
    private TeamId TargetTeam;

    public bool OdinTowerBehavior(
       ObjAIBase _Self) {

        this.Owner = _Self;
        var Self = _Self;
       

      return 
            // Sequence name :OdinTowerBehavior
            (
                  // Sequence name :Run Once
                  (
                        TestUnitAIFirstTime(
                              true) &&
                        DebugAction("OdinTowerBehavior: Initialization started") &&
                        SetUnitAISelf(
                               Self) &&
                        GetUnitPosition(
                              out AssistPosition, 
                              Self) &&
                        GetUnitTeam(
                              out SelfTeam, 
                              Self) &&
                        SetVarFloat(
                              out AcquisitionRange,
                              600f) &&
                        SetVarFloat(
                              out AttackRadius,
                              600f) &&
                        SetVarFloat(
                              out DefenseRadius,
                              700f) &&
                        GetUnitPosition(
                              out SelfPosition, 
                              Self) &&
                        SetUnitInvulnerable(
                              Self, 
                              true) &&
                        SetUnitTargetableState(
                              Self, 
                              false) &&
                        DebugAction($"OdinTowerBehavior: Initialization completed - Team: {SelfTeam}, Position: {SelfPosition}, AcquisitionRange: {AcquisitionRange}")
                  ) ||
                  // Sequence name :Behavior Update
                  (
                        DebugAction("OdinTowerBehavior: Behavior Update started") &&
                        GetUnitPosition(
                              out SelfPosition, 
                              Self) &&
                        DebugAction($"OdinTowerBehavior: Got position: {SelfPosition}") &&
                        odinTower_CheckForChampions.OdinTower_CheckForChampions(
                              out FriendlyChampCount, 
                              Self, 
                              SelfPosition, 
                              AttackRadius, 
                              DefenseRadius) &&
                        DebugAction($"OdinTowerBehavior: Friendly champions in range: {FriendlyChampCount}") &&
                        // Sequence name :Select Behavior
                        (
                              // Sequence name :Attack Behavior
                              (
                                    DebugAction("OdinTowerBehavior: Attack Behavior started") &&
                                    // Sequence name :Selector
                                    (
                                          // Sequence name :AcquireTargetChampion
                                          (
                                                DebugAction("OdinTowerBehavior: Acquiring champion target") &&
                                                // Sequence name :Use Previous Target
                                                (
                                                      SetVarBool(
                                                            out LostAggro, 
                                                            false) &&
                                                                                                      DebugAction("OdinTowerBehavior: Set LostAggro to false") &&
                                                TestUnitAIAttackTargetValid(
                                                      true) &&
                                                DebugAction("OdinTowerBehavior: TestUnitAIAttackTargetValid completed") &&
                                                GetUnitAIAttackTarget(
                                                      out AggroTarget) &&
                                                DebugAction($"OdinTowerBehavior: Got attack target: {AggroTarget?.Name ?? "null"}") &&
                                                GetUnitType(
                                                      out AggroTargetType, 
                                                      AggroTarget) &&
                                                DebugAction($"OdinTowerBehavior: Target type: {AggroTargetType}") &&
                                                AggroTargetType == HERO_UNIT &&
                                                DebugAction($"OdinTowerBehavior: Using previous champion target: {AggroTarget?.Name ?? "null"}") &&
                                                SetVarVector(
                                                      out AggroPosition,
                                                      AssistPosition) &&
                                                      TestUnitIsVisible(
                                                            Self, 
                                                            AggroTarget, 
                                                            true) &&
                                                      // Sequence name :Check For Deaggro
                                                      (
                                                            DistanceBetweenObjectAndPoint(
                                                                  out Distance, 
                                                                  AggroTarget, 
                                                                  AggroPosition) &&
                                                            DebugAction($"OdinTowerBehavior: Distance to previous target: {Distance}") &&
                                                            // Sequence name :Test For Aggro Loss
                                                            (
                                                                  // Sequence name :Test Target Distance
                                                                  (
                                                                        GreaterFloat(
                                                                              Distance, 
                                                                              AcquisitionRange) &&
                                                                        SetVarBool(
                                                                              out LostAggro, 
                                                                              true) &&
                                                                        DebugAction("OdinTowerBehavior: Target lost due to distance")
                                                                  ) ||
                                                                  SetVarBool(
                                                                        out LostAggro, 
                                                                        false)
                                                            )
                                                      ) &&
                                                      LostAggro == false &&
                                                      DebugAction("OdinTowerBehavior: Previous champion target still valid")
                                                ) ||
                                                // Sequence name :AcquireNewTarget
                                                (
                                                      DebugAction("OdinTowerBehavior: Acquiring new champion target") &&
                                                      GetUnitsInTargetArea(
                                                            out EnemyUnitsNearSelf, 
                                                            Self, 
                                                            SelfPosition, 
                                                            AcquisitionRange, 
                                                            AffectEnemies | AffectHeroes) &&
                                                      SetVarFloat(
                                                            out CurrentClosestDistance,
                                                            AcquisitionRange) &&
                                                      // Sequence name :Find Closest Target
                                                      (
                                                            GetCollectionCount(
                                                                  out EnemyCount, 
                                                                  EnemyUnitsNearSelf) &&
                                                            GreaterInt(
                                                                  EnemyCount, 
                                                                  0) &&
                                                            DebugAction($"OdinTowerBehavior: Found {EnemyCount} enemy champions in range") &&
                                                            SetVarBool(
                                                                  out TargetFound,
                                                                  false) &&
                                                            ForEach(EnemyUnitsNearSelf, EnemyUnit => (
                                                                  // Sequence name :Find Closest Target
                                                                  (
                                                                        DistanceBetweenObjectAndPoint(
                                                                              out Distance, 
                                                                              EnemyUnit, 
                                                                              SelfPosition) &&
                                                                        LessFloat(
                                                                              Distance, 
                                                                              CurrentClosestDistance) &&
                                                                        TestUnitIsVisible(
                                                                              Self, 
                                                                              EnemyUnit, 
                                                                              true) &&
                                                                        // Sequence name :Check Aggro
                                                                        (
                                                                              // Sequence name :HasOldTarget
                                                                              (
                                                                                    LostAggro == true &&
                                                                                    GetUnitAIAttackTarget(
                                                                                          out AggroTarget) &&
                                                                                    NotEqualUnit(
                                                                                          AggroTarget, 
                                                                                          EnemyUnit)
                                                                              ) ||
                                                                              LostAggro == false
                                                                        ) &&
                                                                        SetVarFloat(
                                                                              out CurrentClosestDistance, 
                                                                              Distance) &&
                                                                        SetVarAttackableUnit(
                                                                              out CurrentClosestTarget, 
                                                                              EnemyUnit) &&
                                                                        SetVarBool(
                                                                              out TargetFound, 
                                                                              true) &&
                                                                        DebugAction($"OdinTowerBehavior: New closest champion target: {EnemyUnit.Name} at distance {Distance}")
                                                                  ))
                                                            )
                                                      ) &&
                                                      TargetFound == true &&
                                                      SetUnitAIAssistTarget(
                                                            Self) &&
                                                      SetUnitAIAttackTarget(
                                                            CurrentClosestTarget) &&
                                                      SetVarVector(
                                                            out AssistPosition,
                                                            SelfPosition) &&
                                                      DebugAction("OdinTowerBehavior: New champion target acquired")
                                                )
                                          ) ||
                                          // Sequence name :AcquireTarget
                                          (
                                                DebugAction("OdinTowerBehavior: Acquiring general target") &&
                                                // Sequence name :Use Previous Target
                                                (
                                                      SetVarBool(
                                                            out LostAggro, 
                                                            false) &&
                                                      TestUnitAIAttackTargetValid(
                                                            true) &&
                                                      GetUnitAIAttackTarget(
                                                            out AggroTarget) &&
                                                      DebugAction($"OdinTowerBehavior: Using previous target: {AggroTarget?.Name ?? "null"}") &&
                                                      SetVarVector(
                                                            out AggroPosition, 
                                                            AssistPosition) &&
                                                      TestUnitIsVisible(
                                                            Self, 
                                                            AggroTarget, 
                                                            true) &&
                                                      // Sequence name :Check For Deaggro
                                                      (
                                                            DistanceBetweenObjectAndPoint(
                                                                  out Distance, 
                                                                  AggroTarget, 
                                                                  AggroPosition) &&
                                                            DebugAction($"OdinTowerBehavior: Distance to previous target: {Distance}") &&
                                                            // Sequence name :Test For Aggro Loss
                                                            (
                                                                  // Sequence name :Test Target Distance
                                                                  (
                                                                        GreaterFloat(
                                                                              Distance, 
                                                                              AcquisitionRange) &&
                                                                        SetVarBool(
                                                                              out LostAggro, 
                                                                              true) &&
                                                                        DebugAction("OdinTowerBehavior: Target lost due to distance")
                                                                  ) ||
                                                                  SetVarBool(
                                                                        out LostAggro, 
                                                                        false)
                                                            )
                                                      ) &&
                                                      LostAggro == false &&
                                                      DebugAction("OdinTowerBehavior: Previous target still valid")
                                                ) ||
                                                // Sequence name :AcquireNewTarget
                                                (
                                                      DebugAction("OdinTowerBehavior: Acquiring new general target") &&
                                                      GetUnitsInTargetArea(
                                                            out EnemyUnitsNearSelf, 
                                                            Self, 
                                                            SelfPosition, 
                                                            AcquisitionRange, 
                                                            AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                                                            SetVarFloat(out CurrentClosestDistance , AcquisitionRange) &&
                                   
                                                      // Sequence name :Find Closest Target
                                                      (
                                                            GetCollectionCount(
                                                                  out EnemyCount, 
                                                                  EnemyUnitsNearSelf) &&
                                                            GreaterInt(
                                                                  EnemyCount, 
                                                                  0) &&
                                                            DebugAction($"OdinTowerBehavior: Found {EnemyCount} enemy units in range") &&
                                                            SetVarBool(out TargetFound , false) && 
                                      
                                                            ForEach(EnemyUnitsNearSelf,EnemyUnit => (
                                                                  // Sequence name :Find Closest Target
                                                                  (
                                                                        DistanceBetweenObjectAndPoint(
                                                                              out Distance, 
                                                                              EnemyUnit, 
                                                                              SelfPosition) &&
                                                                        LessFloat(
                                                                              Distance, 
                                                                              CurrentClosestDistance) &&
                                                                        TestUnitIsVisible(
                                                                              Self, 
                                                                              EnemyUnit, 
                                                                              true) &&
                                                                        // Sequence name :Check Aggro
                                                                        (
                                                                              // Sequence name :HasOldTarget
                                                                              (
                                                                                    LostAggro == true &&
                                                                                    GetUnitAIAttackTarget(
                                                                                          out AggroTarget) &&
                                                                                    NotEqualUnit(
                                                                                          AggroTarget, 
                                                                                          EnemyUnit)
                                                                              ) ||
                                                                              LostAggro == false
                                                                        ) &&
                                                                        SetVarFloat(
                                                                              out CurrentClosestDistance, 
                                                                              Distance) &&
                                                                        SetVarAttackableUnit(
                                                                              out CurrentClosestTarget, 
                                                                              EnemyUnit) &&
                                                                        SetVarBool(
                                                                              out TargetFound, 
                                                                              true) &&
                                                                        DebugAction($"OdinTowerBehavior: New closest target: {EnemyUnit.Name} at distance {Distance}")
                                                                  ))
                                                            )
                                                      ) &&
                                                      TargetFound == true &&
                                                      SetUnitAIAssistTarget(
                                                            Self) &&
                                                      SetUnitAIAttackTarget(
                                                            CurrentClosestTarget) &&
                                                            SetVarVector(out AssistPosition, SelfPosition) &&
                                                
                                                      DebugAction("OdinTowerBehavior: New target acquired")
                                                )
                                          )
                                    ) &&
                                    // Sequence name :Attack Target
                                    (
                                          DebugAction("OdinTowerBehavior: Attack Target sequence started") &&
                                          GetUnitAIAttackTarget(
                                                out Target) &&
                                          GetUnitTeam(
                                                out TargetTeam, 
                                                Target) &&
                                          NotEqualUnitTeam(
                                                TargetTeam, 
                                                SelfTeam) &&
                                          DebugAction($"OdinTowerBehavior: Attacking target: {Target?.Name ?? "null"} from team {TargetTeam}") &&
                                          // Sequence name :AttackStrength
                                          (
                                                // Sequence name :Sequence
                                                (
                                                      odinTowerCanCastAbility0.OdinTowerCanCastAbility0(
                                                            Self) &&
                                                      DebugAction("OdinTowerBehavior: Can cast ability 0") &&
                                                      odinTowerCastAbility0.OdinTowerCastAbility0(
                                                            Self) &&
                                                      DebugAction("OdinTowerBehavior: Cast ability 0 executed")
                                                )
                                          )
                                    )
                              )
                        )
                  )
            );
      }
}

