using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class KogMaw_HighThreatManagementClass : AI_Characters
{
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerExhaustClass summonerExhaust = new();
    public bool KogMaw_HighThreatManagement(
         out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      Vector3 SelfPosition,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      int ExhaustSlot,
      int GhostSlot
         )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;

        bool result =
                  // Sequence name :RetreatHighThreat

                  // Sequence name :ChampionRange
                  (
                        // Sequence name :HasPassive
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "KogMawIcathianSurprise",
                                    true) &&
                              // Sequence name :HeroOrMinion
                              (


                                    GetUnitAIClosestTargetInArea(
                                          out CurrentClosestTarget,
                                          Self,
                                          default,
                                          true,
                                          SelfPosition,
                                          1400,
                                          AffectEnemies | AffectHeroes)
                                    ||
                                    GetUnitAIClosestTargetInArea(
                                          out CurrentClosestTarget,
                                          Self,
                                          default,
                                          true,
                                          SelfPosition,
                                          1400,
                                          AffectEnemies | AffectMinions)



                              )
                        ) ||
                        GetUnitAIClosestTargetInArea(
                              out CurrentClosestTarget,
                              Self,
                              default,
                              true,
                              SelfPosition,
                              550,
                              AffectEnemies | AffectHeroes)
                  ) &&
                  GetUnitHealthRatio(
                        out TargetHealthRatio,
                        CurrentClosestTarget) &&
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :UsePassive
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "KogMawIcathianSurprise",
                                    true) &&
                              IssueMoveToUnitOrder(
                                    CurrentClosestTarget)
                        ) ||
                        // Sequence name :E
                        (
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    1,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    true,
                                    false) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    CurrentClosestTarget,
                                    SelfPosition,
                                    1400,
                                    0.25f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition,
                                   default) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    CurrentClosestTarget,
                                    1,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :UseUltimate
                        (
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "KogMawLivingArtilleryCost",
                                    true) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    3,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    CurrentClosestTarget,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                              PredictLineMissileCastPosition(
                                    out SkillShotCastPosition,
                                    CurrentClosestTarget,
                                    SelfPosition,
                                    1500,
                                    0.25f) &&
                              GetRandomPositionBetweenTwoPoints(
                                    out SkillShotCastPosition,
                                    TargetPosition,
                                    SkillShotCastPosition, default
                                    ) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    CurrentClosestTarget,
                                    3,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    SkillShotCastPosition,
                                    true)
                        ) ||
                        // Sequence name :UseExhaust
                        (
                              LessFloat(
                                    HealthRatio,
                                    0.35f) &&
                           summonerExhaust.SummonerExhaust(
                                    Self,
                                    CurrentClosestTarget,
                                    ExhaustSlot)

                        )
                  )
            ;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        return result;
    }
}

