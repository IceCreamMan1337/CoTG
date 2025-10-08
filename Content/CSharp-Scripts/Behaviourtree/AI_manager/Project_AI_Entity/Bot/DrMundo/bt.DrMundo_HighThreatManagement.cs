using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class DrMundo_HighThreatManagementClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private SummonerIgniteClass summonerIgnite = new();
    private AutoAttackClass autoAttack = new();

    public bool DrMundo_HighThreatManagement(
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

                  GetUnitAIClosestTargetInArea(
                        out CurrentClosestTarget,
                        Self, default
                        ,
                        true,
                        SelfPosition,
                        950,
                        AffectEnemies | AffectHeroes) &&
                  canCastChampionAbilityClass.CanCastChampionAbility(
                        Self,
                        0,
                        PreviousSpellCastTime,
                        CastSpellTimeThreshold,
                        PreviousSpellCastTarget,
                        CurrentClosestTarget,
                        PreviousSpellCast,
                        false,
                        false) &&
                  TestLineMissilePathIsClear(
                        Self,
                        CurrentClosestTarget,
                        125,
                        AffectEnemies | AffectHeroes | AffectMinions) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  GetUnitPosition(
                        out TargetPosition,
                        CurrentClosestTarget) &&
                  PredictLineMissileCastPosition(
                        out SkillShotCastPosition,
                        CurrentClosestTarget,
                        SelfPosition,
                        2000,
                        0.3f) &&
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
                        0,
                        1,
                        PreviousSpellCast,
                        PreviousSpellCastTarget,
                        PreviousSpellCastTime,
                        CastSpellTimeThreshold,
                        SkillShotCastPosition,
                        true)

            ;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;

        return result;


    }
}

