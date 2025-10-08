namespace BehaviourTrees.all;


class MinionInitClass : AI_Characters
{


    public bool MinionInit(
        out AttackableUnit _Self,
     out Vector3 _AssistPosition,
     out TeamId _SelfTeam,
     AttackableUnit Self
        )
    {


        Vector3 AssistPosition = default;
        TeamId SelfTeam = default;
        bool result =
                    // Sequence name :MinionInit

                    /*GetUnitAISelf(
                          out Self) &&*/
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

