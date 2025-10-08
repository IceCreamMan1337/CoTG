namespace BehaviourTrees;


class OdinTowerReferenceUpdateClass : OdinLayout
{


    public bool OdinTowerReferenceUpdate(
         out float SelfPosition,
   AttackableUnit Self
         )
    {
        /// NANI WHY FUCKING FLOAT FOR SELFPOSITION
        Vector3 _SelfPosition_ = default;
        bool result =
                          // Sequence name :OdinTowerReferenceUpdate

                          GetUnitPosition(
                                out _SelfPosition_,
                                Self)

                    ;

        SelfPosition = _SelfPosition_.X + _SelfPosition_.Y + _SelfPosition_.Z;
        return result;

    }
}

