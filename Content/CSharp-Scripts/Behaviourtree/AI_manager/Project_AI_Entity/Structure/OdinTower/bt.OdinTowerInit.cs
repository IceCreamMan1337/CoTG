namespace BehaviourTrees.all;


class OdinTowerInitClass : AI_Characters
{
    public bool OdinTowerInit(
         out AttackableUnit _Self,
     out Vector3 _AssistPosition,
     out TeamId _SelfTeam
        )
    {

        AttackableUnit Self = default;
        Vector3 AssistPosition = default;
        TeamId SelfTeam = default;
        bool result =
                // Sequence name :OdinTowerInit

                GetUnitAISelf(
                      out Self) &&
                GetUnitPosition(
                      out AssistPosition,
                      Self) &&
                GetUnitTeam(
                      out SelfTeam,
                      Self)

          ;
        _Self = Self;
        _AssistPosition = AssistPosition;
        _SelfTeam = SelfTeam;
        return result;



    }
}

