using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class HealClass : AI_Characters
{

    protected bool TryCallHeal(
              out int CurrentSpellCast,
      out AttackableUnit CurrentSpellCastTarget,
      out float _CastSpellTimeThreshold,
      out float _PreviousSpellCastTime,

      object procedureObject,
      AttackableUnit Self,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime)
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
            PreviousSpellCastTime);

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
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private SummonerHealClass summonerHeal = new SummonerHealClass();

    public bool Heal(
     out float __CastSpellTimeThreshold,
     out float __PreviousSpellCastTime,
     out int _CurrentSpellCast,
     out AttackableUnit _CurrentSpellCastTarget,
     out string _ActionPerformed,
     AttackableUnit Self,
     string Champion,
     int HealSlot,
     int PreviousSpellCast,
     AttackableUnit PreviousSpellCastTarget,
     float CastSpellTimeThreshold,
     float PreviousSpellCastTime,
     bool IssuedAttack,
     AttackableUnit IssuedAttackTarget,
     object HealAbilities
    )
    {

        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        string ActionPerformed = default;

        bool result =
            // Sequence name :Sequence
            (
                  TestUnitHasBuff(
                        Self,
                        default,
                        "Recall",
                        false) &&
                  // Sequence name :Heal (Selector = ||)
                  (
                     summonerHeal.SummonerHeal(
                              Self,
                              HealSlot)
                     ||
                        TryCallHeal(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out _CastSpellTimeThreshold,
                                 out _PreviousSpellCastTime,
                                 HealAbilities,
                                 Self,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime) ||
                        // Sequence name :UsePotion
                        (
                              TestUnitAICanUseItem(
                                    2003) &&
                              GetUnitCurrentHealth(
                                    out Health,
                                    Self) &&
                              GetUnitMaxHealth(
                                    out MaxHealth,
                                    Self) &&
                              DivideFloat(
                                    out HP_Ratio,
                                    Health,
                                    MaxHealth) &&
                              LessFloat(
                                    HP_Ratio,
                                    0.55f) &&
                              SetUnitAIItemTarget(
                                    Self,
                                    2003) &&
                              IssueUseItemOrder(
                                    2003, default
                                    )
                        )
                  ) &&
                  SetVarString(
                        out ActionPerformed,
                        "Heal")

            );

        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        _ActionPerformed = ActionPerformed;

        return result;
    }
}

