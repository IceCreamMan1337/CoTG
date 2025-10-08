using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Ashe_HighThreatManagementClass : AI_Characters
{

    private CastTargetAbilityClass castTargetAbility = new();
    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private SummonerExhaustClass summonerExhaust = new();

    public bool Ashe_HighThreatManagement(
         out int __CurrentSpellCast,
      out AttackableUnit __CurrentSpellCastTarget,
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

                  GetUnitAttackRange(
                        out AttackRange,
                        Self) &&
                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self,
                        default,
                        true,
                        SelfPosition,
                        AttackRange,
                        AffectEnemies | AffectHeroes) &&
                  GetUnitHealthRatio(
                        out TargetHealthRatio,
                        CurrentClosestTarget) &&
                  GetUnitHealthRatio(
                        out HealthRatio,
                        Self) &&
                  // Sequence name :ManageHighThreat
                  (
                        // Sequence name :W
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
                                   default,
                                    false)
                        ) ||
                        // Sequence name :UseUltimate
                        (
                              LessFloat(
                                    TargetHealthRatio,
                                    0.5f) &&
                              LessFloat(
                                    HealthRatio,
                                    0.35f) &&
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
                                   default,
                                    false) &&
                              SetVarBool(
                                    out SpellStall,
                                    true)
                        ) ||
                        // Sequence name :KiteWithFrostShot
                        (
                              TestEntityDifficultyLevel(
                                    false,
                                    EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "FrostShot",
                                    true) &&
                              TestUnitHasBuff(
                                    CurrentClosestTarget,
                                   default,
                                    "FrostArrow",
                                    false) &&
                              SetUnitAIAttackTarget(
                                    CurrentClosestTarget) &&
                              IssueAttackOrder()
                        ) ||
                        // Sequence name :ToggleSpell
                        (
                              TestEntityDifficultyLevel(
                                    false,
                                    EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              TestUnitHasBuff(
                                    Self,
                                   default,
                                    "FrostShot",
                                    false) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self,
                                    0,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    Self,
                                    PreviousSpellCast,
                                    false,
                                    false) &&
                             castTargetAbility.CastTargetAbility(
                                    out CurrentSpellCast,
                                    out CurrentSpellCastTarget,
                                    out PreviousSpellCastTime,
                                    out CastSpellTimeThreshold,
                                    Self,
                                    Self,
                                    0,
                                    1,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                   default,
                                    false)
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

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __CurrentSpellCast = CurrentSpellCast;
        __CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;



    }
}

