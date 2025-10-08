using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class DominionHealClass : AI_Characters
{
    private SummonerHealClass summonerHeal = new();

    protected bool TryCallDominionHeal(
               out int CurrentSpellCast,
       out AttackableUnit CurrentSpellCastTarget,
       out float _CastSpellTimeThreshold,
       out float _PreviousSpellCastTime,

       object procedureObject,
       AttackableUnit Self,
       int PreviousSpellCast,
       AttackableUnit PreviousSpellCastTarget,
       float CastSpellTimeThreshold,
       float PreviousSpellCastTime,
       bool IssuedAttack,
        AttackableUnit IssuedAttackTarget)
    {

        CurrentSpellCast = PreviousSpellCast;
        CurrentSpellCastTarget = PreviousSpellCastTarget;
        _PreviousSpellCastTime = PreviousSpellCastTime;
        _CastSpellTimeThreshold = CastSpellTimeThreshold;

        if (procedureObject == null)
        {
            return false;
        }

        bool callSuccess = CallProcedureVariable(
            out object[] outputs,
            procedureObject,

                                  Self,
                                  PreviousSpellCast,
                                  PreviousSpellCastTarget,
                                  CastSpellTimeThreshold,
                                  PreviousSpellCastTime,
                                  IssuedAttack,
                                  IssuedAttackTarget);

        if (callSuccess && outputs != null && outputs.Length >= 2)
        {
            try
            {

                CurrentSpellCast = (int)outputs[0];
                CurrentSpellCastTarget = (AttackableUnit)outputs[1];
                _PreviousSpellCastTime = (float)outputs[2];
                _CastSpellTimeThreshold = (float)outputs[3];
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool DominionHeal(
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out string _ActionPerformed,
      out bool __SpellStall,
      AttackableUnit Self,
      string Champion,
      int HealSlot,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      object HealAbilities, //function
      bool SpellStall

         )
    {
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        string ActionPerformed = default;
        bool _SpellStall = SpellStall;

        bool result =
                    // Sequence name :Sequence

                    TestUnitHasBuff(
                          Self,
                          default,
                          "OdinRecall",
                          false) &&
                    // Sequence name :NotCapturingPoint
                    (
                          TestUnitHasBuff(
                                Self,
                                default,
                                "OdinCaptureChannel",
                                false)
                          ||
                          TestUnitIsChanneling(
                                Self,
                                false)
                    ) &&
                    // Sequence name :Heal
                    (
                         summonerHeal.SummonerHeal(
                                Self,
                                HealSlot)
                            ||
                            TryCallDominionHeal(
                                     out _CurrentSpellCast,
                                  out _CurrentSpellCastTarget,
                                  out _CastSpellTimeThreshold,
                                  out _PreviousSpellCastTime,

                                  HealAbilities,
                                  Self,
                                  PreviousSpellCast,
                                  PreviousSpellCastTarget,
                                  CastSpellTimeThreshold,
                                  PreviousSpellCastTime,
                                  IssuedAttack,
                                  IssuedAttackTarget)
                            ||
                          // Sequence name :UsePotion
                          (
                                TestUnitAICanUseItem(
                                      2003) &&
                                GetUnitHealthRatio(
                                      out HP_Ratio,
                                      Self) &&
                                LessFloat(
                                      HP_Ratio,
                                      0.55f) &&
                                SetUnitAIItemTarget(
                                      Self,
                                      2003) &&
                                IssueUseItemOrder(
                                      2003, default
                                      )
                          ) ||
                          // Sequence name :GetHealthPack
                          (
                                TestEntityDifficultyLevel(
                                      false,
                                     EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                SetVarFloat(
                                      out DetectionRange,
                                      800) &&
                                GetUnitHealthRatio(
                                      out HP_Ratio,
                                      Self) &&
                                LessFloat(
                                      HP_Ratio,
                                      0.7f) &&
                                SubtractFloat(
                                      out DetectionRangeModifier,
                                      1,
                                      HP_Ratio) &&
                                MultiplyFloat(
                                      out DetectionRangeModifier,
                                      DetectionRangeModifier,
                                      1500) &&
                                AddFloat(
                                      out DetectionRange,
                                      DetectionRange,
                                      DetectionRangeModifier) &&
                                GetUnitPosition(
                                      out SelfPosition,
                                      Self) &&
                                SetVarInt(
                                      out HealthPackEncounterId,
                                      4) &&
                                GetClosestEncounterToPoint( // to complete
                                      out SquadId,
                                      out NearestHealthPackPosition,
                                      HealthPackEncounterId,
                                      SelfPosition) &&
                                DistanceBetweenObjectAndPoint(
                                      out DistanceToHealthPack,
                                      Self,
                                      NearestHealthPackPosition) &&
                                // Sequence name :Conditions
                                (
                                      LessFloat(
                                            DistanceToHealthPack,
                                            300) ||
                                      // Sequence name :NotSuperCloseButOtherFavorableConditions
                                      (
                                            LessFloat(
                                                  DistanceToHealthPack,
                                                  DetectionRange) &&
                                            CountUnitsInTargetArea(
                                                  out EnemyHeroesInArea,
                                                  Self,
                                                  NearestHealthPackPosition,
                                                  DetectionRange,
                                                  AffectEnemies | AffectHeroes,
                                                  "") &&
                                            // Sequence name :NoEnemyChampionsNearbyOrHealthPackIsCloserThanNearestEnemyChamp
                                            (
                                                  EnemyHeroesInArea == 0 ||
                                                  // Sequence name :CompareDistanceToClosestEnemyChampAndHealthPack
                                                  (
                                                        GetUnitAIClosestTargetInArea(
                                                              out ClosestEnemyChamp,
                                                              Self,
                                                              default,
                                                              false,
                                                              SelfPosition,
                                                              DetectionRange,
                                                              AffectEnemies | AffectHeroes) &&
                                                        GetDistanceBetweenUnits(
                                                              out DistanceToClosestEnemyChamp,
                                                              Self,
                                                              ClosestEnemyChamp) &&
                                                        LessFloat(
                                                              DistanceToHealthPack,
                                                              DistanceToClosestEnemyChamp)
                                                  )
                                            ) &&
                                            GetUnitsInTargetAreaWithBuff(
                                                  out NonOwnedCPsNearSelf,
                                                  Self,
                                                  SelfPosition,
                                                  DetectionRange,
                                                  AffectEnemies | AffectMinions | AffectNeutral | AffectUseable,
                                                  "OdinGuardianStatsByLevel") &&
                                            GetCollectionCount(
                                                  out NonOwnedCPsNearSelfCount,
                                                  NonOwnedCPsNearSelf) &&
                                            // Sequence name :NoUnownedPointsNearSelfOrHealthPackIsNearerThanPoint
                                            (
                                                  NonOwnedCPsNearSelfCount == 0
                                                 ||    // Sequence name :CompareDistanceToPointAndHealthPack
                                                  (
                                                        GetUnitAIClosestTargetInCollection(
                                                              out Point,
                                                              Self,
                                                              NonOwnedCPsNearSelf) &&
                                                        GetDistanceBetweenUnits(
                                                              out DistanceToPoint,
                                                              Self,
                                                              Point) &&
                                                        LessFloat(
                                                              DistanceToHealthPack,
                                                              DistanceToPoint)
                                                  )
                                            )
                                      )
                                ) &&
                                IssueMoveToPositionOrder(
                                      NearestHealthPackPosition)
                          )
                    ) &&
                    SetVarString(
                          out ActionPerformed,
                          "Heal")

              ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        _ActionPerformed = ActionPerformed;
        __SpellStall = _SpellStall;

        return result;
    }
}

