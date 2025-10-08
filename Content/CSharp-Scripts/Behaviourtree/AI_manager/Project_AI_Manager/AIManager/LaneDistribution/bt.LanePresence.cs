namespace BehaviourTrees;


class LanePresenceClass : AI_LaneDistribution
{


    public bool LanePresence(
   out float StrengthIndicator,
   float EnemyStrength,
   float FriendlyStrength)
    {

        float _StrengthIndicator = default;

        bool result =
                  // Sequence name :Calculates_Lane_Presence

                  SubtractFloat(
                        out Presence,
                        FriendlyStrength,
                        EnemyStrength) &&
                  DivideFloat(
                        out Presence,
                        Presence,
                        4000) &&
                  MaxFloat(
                        out Presence,
                        Presence,
                        -1) &&
                  MinFloat(
                        out Presence,
                        Presence,
                        1) &&
                  SetVarFloat(
                        out _StrengthIndicator,
                        Presence)

            ;
        StrengthIndicator = _StrengthIndicator;
        return result;
    }
}

