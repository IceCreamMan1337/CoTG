using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class StrengthEvaluation_ChampionHealthClass : AImission_bt 
{


    public  bool StrengthEvaluation_ChampionHealth(
                out float ModifiedChampionValue,
    AttackableUnit ChampionUnit,
    float ChampionValue
          
          )
      {

        float _ModifiedChampionValue = default;
bool result =
            // Sequence name :Sequence
            (
                  GetUnitMaxHealth(
                        out MaxHealth, 
                        ChampionUnit) &&
                  GetUnitCurrentHealth(
                        out CurrentHealth, 
                        ChampionUnit) &&
                  DivideFloat(
                        out HealthRatio, 
                        CurrentHealth, 
                        MaxHealth) &&
                  InterpolateLine(
                        out HealthModifier, 
                        0, 
                        0.6f, 
                        0.5f, 
                        1, 
                        0, 
                        1, 
                        HealthRatio) &&
                  MultiplyFloat(
                        out _ModifiedChampionValue, 
                        HealthModifier, 
                        ChampionValue)

            );
        ModifiedChampionValue = _ModifiedChampionValue;
        return result;
      }
}

