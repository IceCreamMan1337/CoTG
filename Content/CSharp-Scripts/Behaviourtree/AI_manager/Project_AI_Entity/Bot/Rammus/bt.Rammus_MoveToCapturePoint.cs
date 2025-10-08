using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees.all;


class Rammus_MoveToCapturePointClass : AI_Characters
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new();
    private CastTargetAbilityClass castTargetAbility = new();
    private AutoAttackClass autoAttack = new();
    private SummonerGhostClass summonerGhost = new();
    private SummonerFlashClass summonerFlash = new();

    public bool Rammus_MoveToCapturePoint(
         out float __CastSpellTimeThreshold,
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      float CastSpellTimeThreshold,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PreviousSpellCastTime,
      AttackableUnit Self,
      bool SpellStall
         )
    {

        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        bool _SpellStall = SpellStall;

        bool result =

                  // Sequence name :CastQ

                  TestUnitHasBuff(
                        Self, default
                        ,
                        "Powerball",
                        false) &&
                  GetUnitAITaskPosition(
                        out TaskPosition) &&
                  DistanceBetweenObjectAndPoint(
                        out DistanceToTaskPosition,
                        Self,
                        TaskPosition) &&
                  GreaterFloat(
                        DistanceToTaskPosition,
                        1500) &&
                  GetUnitPARRatio(
      out SelfPAR_Ratio,
      Self,
  PrimaryAbilityResourceType.MANA) &&
                  GreaterFloat(
                        SelfPAR_Ratio,
                        0.8f) &&
                  GetUnitPosition(
                        out SelfPosition,
                        Self) &&
                  CountUnitsInTargetArea(
                        out EnemiesNearby,
                        Self,
                        SelfPosition,
                        800,
                        AffectEnemies | AffectHeroes | AffectMinions,
                        "") &&
                  EnemiesNearby == 0 &&
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

    ;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __SpellStall = _SpellStall;
        return result;
    }
}

