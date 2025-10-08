using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class CastTargetAbilityClass : AI_Characters 
{
      
    private GetSpellCastDelayClass getSpellCastDelay = new GetSpellCastDelayClass();
     public bool CastTargetAbility(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      AttackableUnit Target,
      int ChampionAbilityNumber,
      float RangeModifier,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      float CastSpellTimeThreshold,
      Vector3 SpellPosition,
      bool UseSpellPosition
         )
    {
       int CurrentSpellCast =default;
      AttackableUnit CurrentSpellCastTarget =default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        if(SpellPosition == default)
        {
            SpellPosition = Target.Position3D;
        }

        bool result =
              // Sequence name :CheckRangeCastAndMove
              (
                    // Sequence name :GetRange
                    (
                          // Sequence name :Ability0
                          (
                                ChampionAbilityNumber == 0 &&
                                GetUnitSpellCastRange(
                                      out _Range,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      0)
                          ) ||
                          // Sequence name :Ability1
                          (
                                ChampionAbilityNumber == 1 &&
                                GetUnitSpellCastRange(
                                      out _Range,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      1)
                          ) ||
                          // Sequence name :Ability2
                          (
                                ChampionAbilityNumber == 2 &&
                                GetUnitSpellCastRange(
                                      out _Range,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      2)
                          ) ||
                          // Sequence name :Ability3
                          (
                                ChampionAbilityNumber == 3 &&
                                GetUnitSpellCastRange(
                                      out _Range,
                                      Self,
                                      SPELLBOOK_CHAMPION,
                                      3)
                          )
                    ) &&
                    MultiplyFloat(
                          out _Range,
                          _Range,
                          RangeModifier) &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Selector
                          (
                                // Sequence name :ModifySpellRangeOnPrevCast
                                (
                                      GreaterEqualInt(
                                            PreviousSpellCast,
                                            0) &&
                                      PreviousSpellCast == ChampionAbilityNumber &&
                                      PreviousSpellCastTarget == Target &&
                                      AddFloat(
                                            out _Range,
                                            _Range,
                                            0)
                                ) ||
                                // Sequence name :Sequence
                                (
                                      MultiplyFloat(
                                            out _Range,
                                            _Range,
                                            0.7f)
                                )
                          ) || MaskFailure()
                    ) &&
                    // Sequence name :CastOrMove
                    (
                          // Sequence name :CastAbility
                          (
                                // Sequence name :DistanceToPositionOrTarget
                                (
                                      // Sequence name :UseTarget
                                      (
                                            UseSpellPosition == false &&
                                            GetDistanceBetweenUnits(
                                                  out Distance,
                                                  Target,
                                                  Self)
                                      ) ||
                                      // Sequence name :UseSpellPosition
                                      (
                                            DistanceBetweenObjectAndPoint(
                                                  out Distance,
                                                  Self,
                                                  SpellPosition)
                                      )
                                ) &&
                                LessEqualFloat(
                                      Distance,
                                      _Range) &&
                                // Sequence name :SetTargetAndCastSpell
                                (
                                      // Sequence name :Ability0
                                      (
                                            ChampionAbilityNumber == 0 &&
                                            // Sequence name :PositionOrTarget
                                            (
                                                  // Sequence name :UseTarget
                                                  (
                                                        UseSpellPosition == false &&
                                                        SetUnitAISpellTarget(
                                                              Target,
                                                              0)
                                                  ) ||
                                                  // Sequence name :UseSpellPosition
                                                  (
                                                        ClearUnitAISpellTarget(
                                                              0) &&
                                                        SetUnitAISpellTargetLocation(
                                                              SpellPosition,
                                                              0)
                                                  )
                                            ) &&
                                            CastUnitSpell(
                                                  Self,
                                                  SPELLBOOK_CHAMPION,
                                                  0,
                                                  default, default
                                                  )
                                      ) ||
                                      // Sequence name :Ability1
                                      (
                                            ChampionAbilityNumber == 1 &&
                                            // Sequence name :PositionOrTarget
                                            (
                                                  // Sequence name :UseTarget
                                                  (
                                                        UseSpellPosition == false &&
                                                        SetUnitAISpellTarget(
                                                              Target,
                                                              1)
                                                  ) ||
                                                  // Sequence name :UseSpellPosition
                                                  (
                                                        ClearUnitAISpellTarget(
                                                              1) &&
                                                        SetUnitAISpellTargetLocation(
                                                              SpellPosition,
                                                              1)
                                                  )
                                            ) &&
                                            CastUnitSpell(
                                                  Self,
                                                  SPELLBOOK_CHAMPION,
                                                  1,
                                                  default, default
                                                  )
                                      ) ||
                                      // Sequence name :Ability2
                                      (
                                            ChampionAbilityNumber == 2 &&
                                            // Sequence name :PositionOrTarget
                                            (
                                                  // Sequence name :UseTarget
                                                  (
                                                        UseSpellPosition == false &&
                                                        SetUnitAISpellTarget(
                                                              Target,
                                                              2)
                                                  ) ||
                                                  // Sequence name :UseSpellPosition
                                                  (
                                                        ClearUnitAISpellTarget(
                                                              2) &&
                                                        SetUnitAISpellTargetLocation(
                                                              SpellPosition,
                                                              2)
                                                  )
                                            ) &&
                                            CastUnitSpell(
                                                  Self,
                                                  SPELLBOOK_CHAMPION,
                                                  2,
                                                  default, default
                                                  )
                                      ) ||
                                      // Sequence name :Ability3
                                      (
                                            ChampionAbilityNumber == 3 &&
                                            // Sequence name :PositionOrTarget
                                            (
                                                  // Sequence name :UseTarget
                                                  (
                                                        UseSpellPosition == false &&
                                                        SetUnitAISpellTarget(
                                                              Target,
                                                              3)
                                                  ) ||
                                                  // Sequence name :UseSpellPosition
                                                  (
                                                        ClearUnitAISpellTarget(
                                                              3) &&
                                                        SetUnitAISpellTargetLocation(
                                                              SpellPosition,
                                                              3)
                                                  )
                                            ) &&
                                            CastUnitSpell(
                                                  Self,
                                                  SPELLBOOK_CHAMPION,
                                                  3,
                                                  default, default
                                                  )
                                      )
                                ) &&
                                SetVarInt(
                                      out CurrentSpellCast,
                                      ChampionAbilityNumber) &&
                                SetVarAttackableUnit(
                                      out CurrentSpellCastTarget,
                                      Target) &&
                                GetGameTime(
                                      out PreviousSpellCastTime) &&
                             getSpellCastDelay.GetSpellCastDelay(
                                      out CastSpellTimeThreshold)
                          ) ||
                          // Sequence name :Move
                          (
                                // Sequence name :UseTarget
                                (
                                      UseSpellPosition == false &&
                                      IssueMoveToUnitOrder(
                                            Target)
                                ) ||
                                IssueMoveToPositionOrder(
                                      SpellPosition)

                          )
                    )
              );

         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;

        return result;

    }
}

