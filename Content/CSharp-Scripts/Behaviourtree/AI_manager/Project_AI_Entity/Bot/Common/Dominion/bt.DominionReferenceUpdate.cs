namespace BehaviourTrees.all;


class DominionReferenceUpdateClass : AI_Characters
{

    private DominionStrengthEvaluatorClass dominionStrengthEvaluator = new();
    public bool DominionReferenceUpdate(
         out Vector3 _SelfPosition,
     out float __Damage,
     out float __StrengthRatioOverTime,
     out float __PrevHealth,
     out float __PrevTime,
     out bool __TeleportHome,
     float PrevTime,
     float Damage,
     float StrengthRatioOverTime,
     float PrevHealth,
     AttackableUnit Self,
     bool TeleportHome
        )
    {
        Vector3 SelfPosition = default;
        float _PrevTime = PrevTime;
        float _Damage = Damage;
        float _StrengthRatioOverTime = StrengthRatioOverTime;
        float _PrevHealth = PrevHealth;
        bool _TeleportHome = TeleportHome;


        bool result =
                    // Sequence name :ReferenceUpdate

                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                                // Sequence name :MaskFailure

                                // Sequence name :PerHalfSecond

                                GetGameTime(
                                      out CurrentTime) &&
                                SubtractFloat(
                                      out TimeDiff,
                                      CurrentTime,
                                      PrevTime) &&
                                      // Sequence name :TimeGreaterThanOneSecond

                                      GreaterFloat(
                                            TimeDiff,
                                            0.5f)
                                      &&
                                      LessFloat(
                                            TimeDiff,
                                            0)
                                 &&
                                MultiplyFloat(
                                      out _Damage,
                                      Damage,
                                      0.8f) &&
                                MultiplyFloat(
                                      out _StrengthRatioOverTime,
                                      StrengthRatioOverTime,
                                      0.8f) &&
                                            // Sequence name :MaskFailure

                                            // Sequence name :LowThreatEvaluator

                                            GetUnitsInTargetArea(
                                                  out TargetCollection,
                                                  Self,
                                                  SelfPosition,
                                                  1200,
                                                 SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectMinions | SpellDataFlags.AffectTurrets
                                                ) &&
                                            dominionStrengthEvaluator.DominionStrengthEvaluator(
                                                  out EnemyStrength,
                                                  TargetCollection,
                                                  Self) &&
                                            GetUnitsInTargetArea(
                                                  out TargetCollection,
                                                  Self,
                                                  SelfPosition,
                                                  1200,
                                                  SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectMinions | SpellDataFlags.AffectTurrets | SpellDataFlags.AlwaysSelf
                                                  ) &&
                                            dominionStrengthEvaluator.DominionStrengthEvaluator(
                                                  out EnemyStrength,
                                                  TargetCollection,
                                                  Self) &&
                                            DivideFloat(
                                                  out StrRatio,
                                                  EnemyStrength,
                                                  FriendStrength) &&
                                            AddFloat(
                                                  out _StrengthRatioOverTime,
                                                  StrengthRatioOverTime,
                                                  StrRatio) &&
                                            GetUnitAIAttackers(
                                                  out TargetCollection,
                                                  Self) &&
                                                  // Sequence name :MaskFailure

                                                  ForEach(TargetCollection, Unit =>
                                                              // Sequence name :Sequence

                                                              GetUnitBuffCount(
                                                                    out Count,
                                                                    Unit,
                                                                    "OdinGuardianStatsByLevel") &&
                                                              GreaterInt(
                                                                    Count,
                                                                    0) &&
                                                              AddFloat(
                                                                    out _StrengthRatioOverTime,
                                                                    StrengthRatioOverTime,
                                                                    7)

                                                  )


                                 &&
                                SetVarFloat(
                                      out PrevTime,
                                      CurrentTime)

                     &&
                                // Sequence name :MaskFailure

                                // Sequence name :AddDamage

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
                                                  Damage,
                                                  NewDamage) &&
                                            SetVarFloat(
                                                  out PrevHealth,
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

                     &&
                                // Sequence name :MaskFailure

                                // Sequence name :Sequence

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



              ;
        _SelfPosition = SelfPosition;
        __PrevTime = _PrevTime;
        __Damage = _Damage;
        __StrengthRatioOverTime = _StrengthRatioOverTime;
        __PrevHealth = _PrevHealth;
        __TeleportHome = _TeleportHome;
        return result;
    }
}

