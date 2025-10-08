namespace BehaviourTrees;


class GetGraveyardPositionClass : IODIN_MinionAIBT
{


    public bool GetGraveyardPosition(
         out Vector3 GraveyardPosition,
         int PointIndex)
    {

        Vector3 _GraveyardPosition = default;
        bool result =
                    // Sequence name :Selector

                    // Sequence name :Index0
                    (
                          PointIndex == 0 &&
                          GetWorldLocationByName(
                                out GraveyardPosition,
                                "MinionGraveyardA")
                    ) ||
                    // Sequence name :Index1
                    (
                          PointIndex == 1 &&
                          GetWorldLocationByName(
                                out GraveyardPosition,
                                "MinionGraveyardB")
                    ) ||
                    // Sequence name :Index2
                    (
                          PointIndex == 2 &&
                          GetWorldLocationByName(
                                out GraveyardPosition,
                                "MinionGraveyardC")
                    ) ||
                    // Sequence name :Index3
                    (
                          PointIndex == 3 &&
                          GetWorldLocationByName(
                                out GraveyardPosition,
                                "MinionGraveyardD")
                    ) ||
                    // Sequence name :Index4
                    (
                          PointIndex == 4 &&
                          GetWorldLocationByName(
                                out GraveyardPosition,
                                "MinionGraveyardE")

                    )
              ;
        GraveyardPosition = _GraveyardPosition;
        return result;
    }
}