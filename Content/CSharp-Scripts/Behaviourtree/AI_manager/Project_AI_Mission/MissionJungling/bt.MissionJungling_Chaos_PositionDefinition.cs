namespace BehaviourTrees;


class MissionJungling_Chaos_PositionDefinitionClass : AImission_bt
{


    public bool MissionJungling_Chaos_PositionDefinition(
    out Vector3 PositionWolf,
    out Vector3 PositionGolem,
    out Vector3 PositionWraith,
    out Vector3 PositionAncientGolem,
    out Vector3 PositionLizardElder

        )
    {
        Vector3 _PositionWolf = default(Vector3);
        Vector3 _PositionGolem = default(Vector3);
        Vector3 _PositionWraith = default(Vector3);
        Vector3 _PositionAncientGolem = default(Vector3);
        Vector3 _PositionLizardElder = default(Vector3);

        bool result =
                  // Sequence name :SetPositions

                  MakeVector(
                        out _PositionWolf,
                        10597,
                        55,
                        8150) &&
                  MakeVector(
                        out _PositionAncientGolem,
                        10596,
                        45,
                        6687) &&
                  MakeVector(
                        out _PositionWraith,
                        7445,
                        46,
                        9299) &&
                  MakeVector(
                        out _PositionLizardElder,
                        6632,
                        45,
                        10664) &&
                  MakeVector(
                        out _PositionGolem,
                        5985,
                        30,
                        12021)

            ;

        PositionLizardElder = _PositionLizardElder;
        PositionGolem = _PositionGolem;
        PositionWraith = _PositionWraith;
        PositionWolf = _PositionWolf;
        PositionAncientGolem = _PositionAncientGolem;
        return result;


    }
}

