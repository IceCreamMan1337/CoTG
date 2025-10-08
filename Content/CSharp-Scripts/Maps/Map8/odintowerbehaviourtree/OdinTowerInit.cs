namespace BehaviourTrees;


class OdinTowerInitClass : OdinLayout
{


    public bool OdinTowerInit(
          out AttackableUnit Self,
     out Vector3 AssistPosition,
     out TeamId SelfTeam
         )
    {

        AttackableUnit _Self = default;
        Vector3 _AssistPosition = default;
        TeamId _SelfTeam = default;

        bool result =
                          // Sequence name :OdinTowerInit

                          GetUnitAISelf(
                                out _Self) &&
                          GetUnitPosition(
                                out _AssistPosition,
                                _Self) &&
                          GetUnitTeam(
                                out _SelfTeam,
                                _Self)

                    ;
        Self = _Self;
        AssistPosition = _AssistPosition;
        SelfTeam = _SelfTeam;
        return result;
    }
}

