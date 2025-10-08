using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class ReferenceUpdateClass : AI_Characters 
{
    private StrengthEvaluatorClass strengthEvaluator = new StrengthEvaluatorClass();

     public bool ReferenceUpdate(
         out Vector3 _SelfPosition,
      out float __Damage,
      out float __StrengthRatioOverTime,
      out float __PrevHealth,
      out float __PrevTime,
      out bool __TeleportHome,
      out bool _BeginnerScaling,
      float PrevTime,
      float Damage,
      float StrengthRatioOverTime,
      float PrevHealth,
      AttackableUnit Self,
      bool TeleportHome
         )
    {
        Vector3 SelfPosition = default;
       float _Damage = Damage;
       float _StrengthRatioOverTime = StrengthRatioOverTime;
       float _PrevHealth = PrevHealth;
       float _PrevTime = PrevTime;
       bool _TeleportHome = TeleportHome;
       bool BeginnerScaling = default;

        bool result =
              // Sequence name :ReferenceUpdate
              (
                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :PerHalfSecond
                          (
                                GetGameTime(
                                      out CurrentTime) &&
                                SubtractFloat(
                                      out TimeDiff,
                                      CurrentTime,
                                      PrevTime) &&
                                // Sequence name :TimeGreaterThanOneSecond
                                (
                                      GreaterFloat(
                                            TimeDiff,
                                            0.5f)
                                      ||
                                      LessFloat(
                                            TimeDiff,
                                            0)
                                ) &&
                                MultiplyFloat(
                                      out _Damage,
                                      Damage,
                                      0.85f) &&
                                MultiplyFloat(
                                      out _StrengthRatioOverTime,
                                      StrengthRatioOverTime,
                                      0.8f) &&
                                // Sequence name :MaskFailure
                                (
                                      // Sequence name :LowThreatEvaluator
                                      (
                                            GetUnitsInTargetArea(
                                                  out TargetCollection,
                                                  Self,
                                                  SelfPosition,
                                                  1200,
                                                  AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                                                  DebugAction("LowThreatEvaluator enemy" + TargetCollection.Count()) && 
                                          strengthEvaluator.StrengthEvaluator(
                                                  out EnemyStrength,
                                                  TargetCollection,
                                                  Self) &&

                                                   DebugAction("StrengthEvaluator " + EnemyStrength) &&
                                            GetUnitsInTargetArea(
                                                  out TargetCollection,
                                                  Self,
                                                  SelfPosition,
                                                  1200,
                                                  AffectFriends | AffectHeroes | AffectMinions | AffectTurrets | AlwaysSelf
                                                  ) &&

                                                    DebugAction("LowThreatEvaluator ally " + TargetCollection.Count()) &&
                                           strengthEvaluator.StrengthEvaluator(
                                                  out FriendStrength,
                                                  TargetCollection,
                                                  Self) &&

                                                       DebugAction("StrengthEvaluator ally " + FriendStrength) &&
                                            DivideFloat(
                                                  out StrRatio,
                                                  EnemyStrength,
                                                  FriendStrength) &&
                                            AddFloat(
                                                  out _StrengthRatioOverTime,
                                                  _StrengthRatioOverTime,
                                                  StrRatio) &&
                                            GetUnitAIAttackers(
                                                  out TargetCollection,
                                                  Self) &&
                                            // Sequence name :MaskFailure
                                            (
                                                  ForEach(TargetCollection, Unit => (
                                                        // Sequence name :Sequence
                                                        (
                                                              GetUnitType(
                                                                    out UnitType,
                                                                    Unit) &&
                                                              UnitType == TURRET_UNIT &&
                                                              AddFloat(
                                                                    out _StrengthRatioOverTime,
                                                                    _StrengthRatioOverTime,
                                                                    7)
                                                        )
                                                  )) || MaskFailure()
                                            )
                                            
                                      )
                                ) || MaskFailure()

                                &&
                                SetVarFloat(
                                      out _PrevTime,
                                      CurrentTime)
                          )
                    ) || MaskFailure()
                    &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :AddDamage
                          (
                                GetUnitCurrentHealth(
                                      out CurrentHealth,
                                      Self) &&
                                SubtractFloat(
                                      out NewDamage,
                                      PrevHealth,
                                      CurrentHealth) &&
                                // Sequence name :HealthDiff
                                (
                                      // Sequence name :LostHealth
                                      (
                                            GreaterFloat(
                                                  NewDamage,
                                                  0) &&
                                            AddFloat(
                                                  out _Damage,
                                                  _Damage,
                                                  NewDamage) &&
                                            SetVarFloat(
                                                  out _PrevHealth,
                                                  CurrentHealth)
                                      ) ||
                                      // Sequence name :GainedHealth
                                      (
                                            LessFloat(
                                                  NewDamage,
                                                  0) &&
                                            SetVarFloat(
                                                  out _PrevHealth,
                                                  CurrentHealth)
                                      )
                                )
                          )
                    ) || MaskFailure()
                    &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Sequence
                          (
                                GetUnitAIBasePosition(
                                      out BaseLocation,
                                      Self) &&
                                DistanceBetweenObjectAndPoint(
                                      out Distance,
                                      Self,
                                      BaseLocation) &&
                                LessEqualFloat(
                                      Distance,
                                      800) &&
                                SetVarBool(
                                      out _TeleportHome,
                                      false)
                          )
                          || MaskFailure()
                    ) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :IsWinning
                          (
                                TestEntityDifficultyLevel(
                                      true,
                                    EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                SetVarInt(
                                      out PlayerKills,
                                      0) &&
                                SetVarInt(
                                      out BotKills,
                                      0) &&
                                GetUnitTeam(
                                      out BotTeam,
                                      Self) &&
                                GetChampionCollection(
                                      out ChampionCollection) &&
                                ForEach(ChampionCollection, Champion => (                           
                                // Sequence name :Sequence
                                      (
                                            GetUnitTeam(
                                                  out ChampTeam,
                                                  Champion) &&
                                            GetChampionKills(
                                                  out Kills,
                                                  Champion) &&
                                            // Sequence name :Selector
                                            (
                                                  // Sequence name :IsABot
                                                  (
                                                        BotTeam == ChampTeam &&
                                                        AddInt(
                                                              out BotKills,
                                                              BotKills,
                                                              Kills)
                                                  ) ||
                                                  // Sequence name :NotABot
                                                  (
                                                        NotEqualUnitTeam(
                                                              BotTeam,
                                                              ChampTeam) &&
                                                        AddInt(
                                                              out PlayerKills,
                                                              PlayerKills,
                                                              Kills)
                                                  )
                                            )
                                      ))
                                ) &&
                                SubtractInt(
                                      out KillDiff,
                                      BotKills,
                                      PlayerKills) &&
                                // Sequence name :SetScaling
                                (
                                      // Sequence name :WinningState
                                      (
                                            GreaterEqualInt(
                                                  KillDiff,
                                                  3) &&
                                            SetVarBool(
                                                  out _BeginnerScaling,
                                                  true)
                                      ) ||
                                      // Sequence name :LosingState
                                      (
                                            LessEqualInt(
                                                  KillDiff,
                                                  -3) &&
                                            SetVarBool(
                                                  out _BeginnerScaling,
                                                  false)

                                      )
                                )
                          )
                    ) || MaskFailure()

              );

         _SelfPosition = SelfPosition;
         __Damage = _Damage;
         __StrengthRatioOverTime = _StrengthRatioOverTime;
         __PrevHealth = _PrevHealth;
         __PrevTime = _PrevTime;
         __TeleportHome = _TeleportHome;
         _BeginnerScaling = BeginnerScaling;

        return result;
    }
}

