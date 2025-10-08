using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class Karthus_GlobalClass : AI_Characters 
{

    private CanCastChampionAbilityClass canCastChampionAbilityClass = new CanCastChampionAbilityClass();
    private CastTargetAbilityClass castTargetAbility = new CastTargetAbilityClass();
    public bool Karthus_Global(
               out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __CastSpellTimeThreshold,
      out float __PreviousSpellCastTime,
      out bool __SpellStall,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      bool SpellStall
         )
    {

        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        bool _SpellStall = default;
        bool result =
            // Sequence name :Sequence
            (
                  GetUnitPosition(
                        out SelfPosition, 
                        Self) &&
                  GetUnitsInTargetArea(
                        out EnemyChampionsInArea, 
                        Self, 
                        SelfPosition, 
                        25000, 
                        AffectEnemies | AffectHeroes) &&
                  GetUnitSpellLevel(
                        out UltLevel, 
                        Self, 
                        SPELLBOOK_CHAMPION, 
                        3) &&
                  GreaterInt(
                        UltLevel, 
                        0) &&
                  MultiplyFloat(
                        out HPThreshold, 
                        UltLevel, 
                        200) &&
                  AddFloat(
                        out HPThreshold, 
                        HPThreshold, 
                        50) &&
                  ForEach(EnemyChampionsInArea, GlobalTarget => (
                        // Sequence name :Sequence
                        (
                              GetUnitCurrentHealth(
                                    out GlobalTargetHealth, 
                                    GlobalTarget) &&
                              LessFloat(
                                    GlobalTargetHealth, 
                                    HPThreshold) &&
                              GetDistanceBetweenUnits(
                                    out DistanceBetweenUnits, 
                                    Self, 
                                    GlobalTarget) &&
                              // Sequence name :Distance&gt,800OrKarthusPassive
                              (
                                    GreaterFloat(
                                          DistanceBetweenUnits, 
                                          800)                              
                                    ||
                                    TestUnitHasBuff(
                                          Self, 
                                          default , 
                                          "DeathDefiedBuff", 
                                          true)
                              ) &&
                              TestUnitIsVisibleToTeam(
                                    Self, 
                                    GlobalTarget, 
                                    true) &&
                              canCastChampionAbilityClass.CanCastChampionAbility(
                                    Self, 
                                    3, 
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
                                    3, 
                                    1, 
                                    PreviousSpellCast, 
                                    PreviousSpellCastTarget, 
                                    PreviousSpellCastTime, 
                                    CastSpellTimeThreshold, 
                                   default , 
                                    false)

                        ))
                  )
            );


        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __SpellStall = _SpellStall;


        return result;
    }
}

