using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class GetUnitHealthPercentageClass : BehaviourTree 
{


       public bool GetUnitHealthPercentage(
      out float CurrentHealth,
      out float MaxHealth,
      out float HealthPercent,
    AttackableUnit Unit)
      {

        float _MaxHealth = default;
        float _CurrentHealth = default;
        float _HealthPercent = default;
        bool result =
            // Sequence name :Sequence
            (
                  GetUnitMaxHealth(
                        out _MaxHealth, 
                        Unit) &&
                  GetUnitCurrentHealth(
                        out _CurrentHealth, 
                        Unit) &&
                  DivideFloat(
                        out _HealthPercent, 
                        _CurrentHealth, 
                        _MaxHealth)

            );
        MaxHealth = _MaxHealth;
        CurrentHealth = _CurrentHealth;
        HealthPercent = _HealthPercent;

        return result;
      }
}

