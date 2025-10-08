using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class EvaluateCapturePointClass : OdinLayout
{


    bool EvaluateCapturePoint(
              out float __CaptureProgress,
    out TeamId __NewPointOwner,
    out int __DebugCircleID,
    out int __TurretGuardId,
  Vector3 CapturePosition,
    AttackableUnit OrderTurret,
  AttackableUnit ChaosTurret,
  TeamId CurrentPointOwner,
  float CaptureProgress,
  float CaptureRadius,
  String CapPointName,
  int DebugCircleID,
  int CapturePointID,
  int TurretGuardId
        )
    {

        TeamId _NewPointOwner = default;
        float _CaptureProgress = CaptureProgress;
        int _DebugCircleID = DebugCircleID;
        int _TurretGuardId = TurretGuardId;
        var capturePointMath = new CapturePointMathClass();
        var spawnPointGuard = new SpawnPointGuardClass();

        bool result =
                  // Sequence name :Sequence

                  AddString(
                        out DebugString,
                        "Evaluating Point ",
                        CapPointName) &&
                  SetVarInt(
                        out ChampionCountOrder,
                        0) &&
                  SetVarInt(
                        out ChampionCountChaos,
                        0) &&
                  DebugAction(

                        DebugString) &&
                  SetVarFloat(
                        out MinionCaptureRadius,
                        CaptureRadius) &&
                  SetVarFloat(
                        out OrderRegenRate,
                        0.14f) &&
                  MultiplyFloat(
                        out ChaosRegenRate,
                        -1,
                        OrderRegenRate) &&
                  SetVarFloat(
                        out OrderMinionCaptureRate,
                        9) &&
                  MultiplyFloat(
                        out ChaosMinionCaptureRate,
                        -1,
                        OrderMinionCaptureRate) &&
                  SetVarFloat(
                        out CaptureRate,
                        0) &&
                  SetVarInt(
                        out AttackerCount,
                        0) &&
                  SetVarUnitTeam(
                        out _NewPointOwner,
                        CurrentPointOwner) &&
                  GetUnitsInTargetArea(
                        out LocalChampionCollection,
                        OrderTurret,
                        CapturePosition,
                        CaptureRadius,
                        AffectEnemies | AffectFriends | AffectHeroes) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Adjust capture status for champs
                        (
                              ForEach(LocalChampionCollection, LocalHero =>
                                          // Sequence name :Selector

                                          TestUnitIsStealthed(
                                                LocalHero,
                                                true) ||
                                          // Sequence name :Count non-stealthed champions
                                          (
                                                GetUnitTeam(
                                                      out LocalHeroTeam,
                                                      LocalHero) &&
                                                // Sequence name :Selector
                                                (
                                                      // Sequence name :If the team is order....
                                                      (
                                                            LocalHeroTeam == TeamId.TEAM_ORDER &&
                                                            AddInt(
                                                                  out ChampionCountOrder,
                                                                  ChampionCountOrder,
                                                                  1) &&
                                                            SetVarFloat(
                                                                  out MinionCaptureRadius,
                                                                  CaptureRadius)
                                                      ) ||
                                                      AddInt(
                                                            out ChampionCountChaos,
                                                            ChampionCountChaos,
                                                            1)
                                                )
                                          )

                              ) &&
                              // Sequence name :Capture/Degen
                              (
                                          // Sequence name :TestForCapture

                                          // Sequence name :Order control
                                          (
                                                NotEqualInt(
                                                      ChampionCountOrder,
                                                      0) &&
                                                ChampionCountChaos == 0 &&
                                                NotEqualUnitTeam(
                                                      CurrentPointOwner,
                                                      TeamId.TEAM_ORDER) &&
                                                SetVarUnitTeam(
                                                      out AttackingTeam,
                                                      TeamId.TEAM_ORDER) &&
                                                capturePointMath.CapturePointMath(
                                                      out _CaptureProgress,
                                                      out CaptureRate,
                                                      ChampionCountOrder,
                                                      _CaptureProgress,
                                                      AttackingTeam) &&
                                                SetVarInt(
                                                      out AttackerCount,
                                                      ChampionCountOrder) &&
                                                AddString(
                                                      out DebugString,
                                                      "&lt;br&gt;Order players: ",
                                                      $"{ChampionCountOrder}") &&
                                                DebugAction(

                                                      DebugString)
                                          ) ||
                                          // Sequence name :Chaos Control
                                          (
                                                NotEqualInt(
                                                      ChampionCountChaos,
                                                      0) &&
                                                ChampionCountOrder == 0 &&
                                                NotEqualUnitTeam(
                                                      CurrentPointOwner,
                                                      TeamId.TEAM_CHAOS) &&
                                                SetVarUnitTeam(
                                                      out AttackingTeam,
                                                      TeamId.TEAM_CHAOS) &&
                                                capturePointMath.CapturePointMath(
                                                      out _CaptureProgress,
                                                      out CaptureRate,
                                                      ChampionCountChaos,
                                                      _CaptureProgress,
                                                      AttackingTeam) &&
                                                SetVarInt(
                                                      out AttackerCount,
                                                      ChampionCountChaos) &&
                                                AddString(
                                                      out DebugString,
                                                      "&lt;br&gt;Chaos players:",
                                                      $"{ChampionCountOrder}") &&
                                                DebugAction(

                                                      DebugString)
                                          )
                                     ||
                                          // Sequence name :Capture Degen

                                          // Sequence name :Order Regen
                                          (
                                                CurrentPointOwner == TeamId.TEAM_ORDER &&
                                                ChampionCountChaos == 0 &&
                                                SetVarFloat(
                                                      out CaptureRate,
                                                      OrderRegenRate) &&
                                                NormalizeFloatToTickRate(
                                                      out NormalizedRate,
                                                      OrderRegenRate) &&
                                                AddFloat(
                                                      out _CaptureProgress,
                                                      _CaptureProgress,
                                                      NormalizedRate) &&
                                                MinFloat(
                                                      out _CaptureProgress,
                                                      _CaptureProgress,
                                                      100)
                                          ) ||
                                          // Sequence name :Chaos Regen
                                          (
                                                CurrentPointOwner == TeamId.TEAM_CHAOS &&
                                                ChampionCountOrder == 0 &&
                                                SetVarFloat(
                                                      out CaptureRate,
                                                      ChaosRegenRate) &&
                                                NormalizeFloatToTickRate(
                                                      out NormalizedRate,
                                                      ChaosRegenRate) &&
                                                AddFloat(
                                                      out _CaptureProgress,
                                                      _CaptureProgress,
                                                      NormalizedRate) &&
                                                MaxFloat(
                                                      out _CaptureProgress,
                                                      _CaptureProgress,
                                                      -100)
                                          ) ||
                                          // Sequence name :Neutral Degen
                                          (
                                                CurrentPointOwner == TeamId.TEAM_NEUTRAL &&
                                                GetCollectionCount(
                                                      out ChampionCollectionCount,
                                                      LocalChampionCollection) &&
                                                ChampionCollectionCount == 0 &&
                                                // Sequence name :+/- Progress
                                                (
                                                      // Sequence name :Below 0
                                                      (
                                                            LessFloat(
                                                                  _CaptureProgress,
                                                                  0) &&
                                                            SetVarFloat(
                                                                  out CaptureRate,
                                                                  OrderRegenRate) &&
                                                            NormalizeFloatToTickRate(
                                                                  out NormalizedRate,
                                                                  OrderRegenRate) &&
                                                            AddFloat(
                                                                  out _CaptureProgress,
                                                                  NormalizedRate,
                                                                  _CaptureProgress) &&
                                                            MinFloat(
                                                                  out neverusedfloat,
                                                                  _CaptureProgress,
                                                                  0)
                                                      ) ||
                                                      // Sequence name :Above 0
                                                      (
                                                            SetVarFloat(
                                                                  out CaptureRate,
                                                                  ChaosRegenRate) &&
                                                            NormalizeFloatToTickRate(
                                                                  out NormalizedRate,
                                                                  ChaosRegenRate) &&
                                                            AddFloat(
                                                                  out _CaptureProgress,
                                                                  _CaptureProgress,
                                                                  NormalizedRate) &&
                                                            MaxFloat(
                                                                  out _CaptureProgress,
                                                                  _CaptureProgress,
                                                                  0)
                                                      )
                                                )
                                          )

                              )
                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Adjust capture status for minions
                        (
                              DebugAction(

                                    "Starting Minion Test") &&
                              // Sequence name :Poll for the right minions
                              (
                                    // Sequence name :Poll for Chaos
                                    (
                                          CurrentPointOwner == TeamId.TEAM_ORDER &&
                                          SetVarUnitTeam(
                                                out AttackingTeam,
                                                TeamId.TEAM_CHAOS) &&
                                          GetUnitsInTargetArea(
                                                out LocalMinionCollection,
                                                OrderTurret,
                                                CapturePosition,
                                                CaptureRadius,
                                                AffectEnemies | AffectMinions) &&
                                          GetUnitsInTargetArea(
                                                out FriendlyMinionCollection,
                                                OrderTurret,
                                                CapturePosition,
                                                CaptureRadius,
                                                AffectFriends | AffectMinions)
                                    ) ||
                                    // Sequence name :Poll for order
                                    (
                                          CurrentPointOwner == TeamId.TEAM_CHAOS &&
                                          SetVarUnitTeam(
                                                out AttackingTeam,
                                                TeamId.TEAM_ORDER) &&
                                          GetUnitsInTargetArea(
                                                out LocalMinionCollection,
                                                OrderTurret,
                                                CapturePosition,
                                                CaptureRadius,
                                                AffectFriends | AffectMinions) &&
                                          GetUnitsInTargetArea(
                                                out FriendlyMinionCollection,
                                                OrderTurret,
                                                CapturePosition,
                                                CaptureRadius,
                                                AffectEnemies | AffectMinions)
                                    ) ||
                                    // Sequence name :Poll for Neutral
                                    (
                                          SetVarUnitTeam(
                                                out AttackingTeam,
                                                TeamId.TEAM_NEUTRAL) &&
                                          GetUnitsInTargetArea(
                                                out LocalMinionCollection,
                                                OrderTurret,
                                                CapturePosition,
                                                CaptureRadius,
                                                AffectEnemies | AffectFriends | AffectMinions) &&
                                          ForEach(LocalMinionCollection, LocalMinion =>
                                                      // Sequence name :Sequence

                                                      GetUnitTeam(
                                                            out LocalMinionTeam,
                                                            LocalMinion) &&
                                                      SetVarUnitTeam(
                                                            out AttackingTeam,
                                                            LocalMinionTeam)

                                          )
                                    )
                              ) &&
                              // Sequence name :MaskFailure
                              (
                                    // Sequence name :HandleFriendlyMinions
                                    (
                                          DebugAction(

                                                "CheckingForFriendlies") &&
                                          NotEqualUnitTeam(
                                                CurrentPointOwner,
                                                TeamId.TEAM_NEUTRAL) &&
                                          DebugAction(

                                                "NotANeutralPoint") &&
                                          GetCollectionCount(
                                                out FriendlyMinionCollectionCount,
                                                FriendlyMinionCollection) &&
                                          GreaterInt(
                                                FriendlyMinionCollectionCount,
                                                0) &&
                                          DebugAction(

                                                "We have friendly minions") &&
                                          GetChampionCollection(
                                                out ChampionCollection) &&
                                          SetVarInt(
                                                out FriendlyMinionsFromOtherPointsCount,
                                                0) &&
                                          ForEach(FriendlyMinionCollection, FriendlyMinion =>                                                 // Sequence name :Sequence

                                                      GetSquadNameOfUnit(
                                                            out MinionSquadName,
                                                            FriendlyMinion) &&
                                                      NotEqualString(
                                                            MinionSquadName,
                                                            CapPointName) &&
                                                      NotEqualString(
                                                            MinionSquadName,
                                                            "TowerGuard") &&
                                                      NotEqualString(
                                                            MinionSquadName,
                                                            "") &&
                                                      KillUnit(
                                                            FriendlyMinion,
                                                            FriendlyMinion) &&
                                                      AddInt(
                                                            out FriendlyMinionsFromOtherPointsCount,
                                                            FriendlyMinionsFromOtherPointsCount,
                                                            1)

                                          ) &&
                                          GreaterInt(
                                                FriendlyMinionsFromOtherPointsCount,
                                                0) &&
                                          // Sequence name :Change point progress
                                          (
                                                // Sequence name :Chaos
                                                (
                                                      CurrentPointOwner == TeamId.TEAM_CHAOS &&
                                                      AddFloat(
                                                            out _CaptureProgress,
                                                            _CaptureProgress,
                                                            ChaosMinionCaptureRate) &&
                                                      MaxFloat(
                                                            out _CaptureProgress,
                                                            _CaptureProgress,
                                                            -100)
                                                ) ||
                                                // Sequence name :Order
                                                (
                                                      AddFloat(
                                                            out _CaptureProgress,
                                                            _CaptureProgress,
                                                            OrderMinionCaptureRate) &&
                                                      MinFloat(
                                                            out _CaptureProgress,
                                                            _CaptureProgress,
                                                            100)
                                                )
                                          )
                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              GetCollectionCount(
                                    out MinionCollectionCount,
                                    LocalMinionCollection) &&
                              GreaterInt(
                                    MinionCollectionCount,
                                    0) &&
                              DebugAction(

                                    "WeHaveNearbyMinions") &&
                              NotEqualUnitTeam(
                                    AttackingTeam,
                                    TeamId.TEAM_NEUTRAL) &&
                              DebugAction(

                                   " We have minions of the opposing team") &&
                              GetUnitsInTargetArea(
                                    out ChaosMinionCollection,
                                    OrderTurret,
                                    CapturePosition,
                                    CaptureRadius,
                                    AffectEnemies | AffectMinions) &&
                              GetUnitsInTargetArea(
                                    out OrderMinionCollection,
                                    OrderTurret,
                                    CapturePosition,
                                    CaptureRadius,
                                    AffectFriends | AffectMinions) &&
                              GetCollectionCount(
                                    out MinionCollectionCount,
                                    ChaosMinionCollection) &&
                              AddString(
                                    out DebugString,
                                    "Chaos/Order Minions : ",
                                    $"{MinionCollectionCount}") &&
                              GetCollectionCount(
                                    out MinionCollectionCount,
                                    OrderMinionCollection) &&
                              AddString(
                                    out DebugString,
                                    DebugString,
                                    "/") &&
                              AddString(
                                    out DebugString,
                                    DebugString,
                                    $"{MinionCollectionCount}") &&
                              DebugAction(

                                    DebugString) &&
                              SetVarBool(
                                    out NonWaveMinionsPresent,
                                    false) &&
                              // Sequence name :MaskFailure
                              (
                                    ForEach(LocalMinionCollection, LocalMinion =>
                                                // Sequence name :KILL THE MINION

                                                GetSquadNameOfUnit(
                                                      out MinionSquadName,
                                                      LocalMinion) &&
                                                NotEqualString(
                                                      MinionSquadName,
                                                      "TowerGuard") &&
                                                NotEqualString(
                                                      "MinionSquadName",
                                                      "") &&
                                                KillUnit(
                                                      LocalMinion,
                                                      LocalMinion) &&
                                                SetVarBool(
                                                      out NonWaveMinionsPresent,
                                                      true)

                                    )
                                    ||
                               DebugAction("MaskFailure")
                              ) &&
                              NonWaveMinionsPresent == true &&
                              DebugAction(

                                   " We have minions of the opposing team") &&
                              // Sequence name :Change point progress
                              (
                                    // Sequence name :Chaos
                                    (
                                          AttackingTeam == TeamId.TEAM_CHAOS &&
                                          AddFloat(
                                                out _CaptureProgress,
                                                _CaptureProgress,
                                                ChaosMinionCaptureRate) &&
                                          MaxFloat(
                                                out _CaptureProgress,
                                                _CaptureProgress,
                                                -100)
                                    ) ||
                                    // Sequence name :Order
                                    (
                                          AddFloat(
                                                out _CaptureProgress,
                                                _CaptureProgress,
                                                OrderMinionCaptureRate) &&
                                          MinFloat(
                                                out _CaptureProgress,
                                                _CaptureProgress,
                                                100)
                                    )
                              ) &&
                              DebugAction(

                                   " Incremented score") &&
                                    // Sequence name :Count number of defending minions killed

                                    // Sequence name :Get the count of the right team's minions
                                    (
                                          // Sequence name :Check if defenders are Order
                                          (
                                                AttackingTeam == TeamId.TEAM_CHAOS &&
                                                GetCollectionCount(
                                                      out DeadDefendingMinionCount,
                                                      ChaosMinionCollection)
                                          ) ||
                                          GetCollectionCount(
                                                out DeadDefendingMinionCount,
                                                OrderMinionCollection)
                                    )
                               &&
                              MultiplyFloat(
                                    out TotalGoldToSplit,
                                    DeadDefendingMinionCount,
                                    25) &&
                              SetVarFloat(
                                    out IndividualBounty,
                                    TotalGoldToSplit) &&
                              DebugAction(

                                    "GOLD CALC COMMENTED OUT") &&
                              // Sequence name :Split the gold among nearby attacking champions
                              (
                                    // Sequence name :Check if attackers are Order
                                    (
                                          AttackingTeam == TeamId.TEAM_CHAOS &&
                                          GetCollectionCount(
                                                out AttackingChampionCount,
                                                OrderMinionCollection) &&
                                          GreaterInt(
                                                AttackingChampionCount,
                                                0) &&
                                          DivideFloat(
                                                out IndividualBounty,
                                                TotalGoldToSplit,
                                                AttackingChampionCount)
                                    ) ||
                                    // Sequence name :Chaos attackers
                                    (
                                          GetCollectionCount(
                                                out AttackingChampionCount,
                                                ChaosMinionCollection) &&
                                          GreaterInt(
                                                AttackingChampionCount,
                                                0) &&
                                          DivideFloat(
                                                out IndividualBounty,
                                                TotalGoldToSplit,
                                                AttackingChampionCount)
                                    )
                              ) &&
                                    // Sequence name :Sequence

                                    ForEach(LocalChampionCollection, LocalHero =>
                                                // Sequence name :Sequence

                                                GetUnitTeam(
                                                      out LocalHeroTeam,
                                                      LocalHero) &&
                                                LocalHeroTeam == AttackingTeam &&
                                                GiveChampionGold(
                                                      LocalHero,
                                                      0)

                                    )

                        )
                        ||
                               DebugAction("MaskFailure")
                  ) &&
                        // Sequence name :Update Points UI

                        CapturePoint_SetValue(
                              CapturePointID,
                              CaptureProgress,
                              CurrentPointOwner) &&
                        CapturePoint_SetRate(
                              CapturePointID,
                              CaptureRate,
                              AttackerCount)
                   &&
                  // Sequence name :Capture Test
                  (
                        // Sequence name :Order Neutralization
                        (
                              CurrentPointOwner == TeamId.TEAM_ORDER &&
                              LessFloat(
                                    _CaptureProgress,
                                    0) &&
                              SetVarUnitTeam(
                                    out _NewPointOwner,
                                    TeamId.TEAM_NEUTRAL)
                        ) ||
                        // Sequence name :Chaos Neutralization
                        (
                              CurrentPointOwner == TeamId.TEAM_CHAOS &&
                              GreaterFloat(
                                    _CaptureProgress,
                                    0) &&
                              SetVarUnitTeam(
                                    out _NewPointOwner,
                                    TeamId.TEAM_NEUTRAL)
                        ) ||
                        // Sequence name :Capturing
                        (
                              CurrentPointOwner == TeamId.TEAM_NEUTRAL &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Order Capture
                                    (
                                          GreaterFloat(
                                                _CaptureProgress,
                                                99) &&
                                          SetVarUnitTeam(
                                                out _NewPointOwner,
                                                TeamId.TEAM_ORDER)
                                    ) ||
                                    // Sequence name :Chaos Capture
                                    (
                                          LessFloat(
                                                _CaptureProgress,
                                                -99) &&
                                          SetVarUnitTeam(
                                                out _NewPointOwner,
                                                TeamId.TEAM_CHAOS)
                                    )
                              )
                        )
                  ) &&
                  // Sequence name :Change point status
                  (
                        // Sequence name :Order capture
                        (
                              NewPointOwner == TeamId.TEAM_ORDER &&
                              SetUnitRendered(
                                    OrderTurret,
                                    true) &&
                              IncPermanentPercentBubbleRadiusMod(
                                    OrderTurret,
                                    1.5f) &&
                              SetUnitNotTargetableToTeam(
                                    OrderTurret,
                                    TeamId.TEAM_CHAOS) &&
                              Announcement_OnCapturePointCaptured_odin(
                                    TeamId.TEAM_ORDER,
                                    CapturePointID) &&
                              DebugAction(

                                    "Capturing for Order") &&
                              ModifyDebugCircleRadius(
                                    _DebugCircleID,
                                    0) &&
                              MakeColor(
                                    out OrderColor,
                                    75,
                                    75,
                                    225,
                                    60) &&
                              ModifyDebugCircleColor(
                                    _DebugCircleID,
                                    OrderColor) &&
                              spawnPointGuard.SpawnPointGuard(
                                    out _TurretGuardId,
                                    CapturePosition,
                                    TeamId.TEAM_ORDER) &&
                              MakeVector(
                                    out vectornotused,
                                    0,
                                    0,
                                    0)
                        ) ||
                        // Sequence name :Chaos capture
                        (
                              NewPointOwner == TeamId.TEAM_CHAOS &&
                              SetUnitRendered(
                                    ChaosTurret,
                                    true) &&
                              IncPermanentPercentBubbleRadiusMod(
                                    ChaosTurret,
                                    1.5f) &&
                              SetUnitNotTargetableToTeam(
                                    ChaosTurret,
                                    TeamId.TEAM_ORDER) &&
                              Announcement_OnCapturePointCaptured_odin(
                                    TeamId.TEAM_CHAOS,
                                    CapturePointID) &&
                              DebugAction(

                                    "Capturing for Chaos") &&
                              MakeColor(
                                    out ChaosColor,
                                    200,
                                    30,
                                    230,
                                    60) &&
                              ModifyDebugCircleColor(
                                    _DebugCircleID,
                                    ChaosColor) &&
                              spawnPointGuard.SpawnPointGuard(
                                    out _TurretGuardId,
                                    CapturePosition,
                                    TeamId.TEAM_CHAOS)
                        ) ||
                        // Sequence name :Neutralization
                        (
                              NewPointOwner == TeamId.TEAM_NEUTRAL &&
                              DebugAction(

                                    "Neutralizing") &&
                              KillSquad(
                                    _TurretGuardId) &&
                              // Sequence name :Selector
                              (
                                    // Sequence name :Sequence
                                    (
                                          AttackingTeam == TeamId.TEAM_ORDER &&
                                          IncPermanentPercentBubbleRadiusMod(
                                                ChaosTurret,
                                                -1.5f) &&
                                          SetUnitTargetableState(
                                                ChaosTurret,
                                                false) &&
                                          SetUnitRendered(
                                                ChaosTurret,
                                                false) &&
                                          Announcement_OnCapturePointNeutralized_odin(
                                                TeamId.TEAM_ORDER,
                                                CapturePointID)
                                    ) ||
                                    // Sequence name :Sequence
                                    (
                                          SetUnitRendered(
                                                OrderTurret,
                                                false) &&
                                          SetUnitTargetableState(
                                                OrderTurret,
                                                false) &&
                                          IncPermanentPercentBubbleRadiusMod(
                                                OrderTurret,
                                                -1.5f) &&
                                          Announcement_OnCapturePointNeutralized_odin(
                                                TeamId.TEAM_CHAOS,
                                                CapturePointID)
                                    )
                              ) &&
                              MakeColor(
                                    out CaptureRadiusColor,
                                    180,
                                    180,
                                    180,
                                    60) &&
                              ModifyDebugCircleColor(
                                    _DebugCircleID,
                                    CaptureRadiusColor)

                        )
                  )
            ;
        __NewPointOwner = _NewPointOwner;
        __CaptureProgress = _CaptureProgress;
        __DebugCircleID = _DebugCircleID;
        __TurretGuardId = _TurretGuardId;
        return result;
    }
}

