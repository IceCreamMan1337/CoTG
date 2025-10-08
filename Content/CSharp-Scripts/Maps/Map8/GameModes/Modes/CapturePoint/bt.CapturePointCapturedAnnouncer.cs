using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class CapturePointCapturedAnnouncerClass : OdinLayout
{


    public bool CapturePointCapturedAnnouncer(
               int CapturePointIndex,
   AttackableUnit CapturedPoint,
   float Radius,
   AttackableUnit CapturedGuardian
         )
    {
        return
                    // Sequence name :Sequence

                    GetUnitPosition(
                          out ReferencePosition,
                          CapturedGuardian) &&
                    GetUnitsInTargetArea(
                          out PossibleAnnouncers,
                          CapturedGuardian,
                          ReferencePosition,
                          1500,
                          AffectFriends | AffectHeroes) &&
                    GetCollectionCount(
                          out PossibleAnnouncerCount,
                          PossibleAnnouncers) &&
                    // Sequence name :Selector
                    (
                          // Sequence name :Sequence
                          (
                                GreaterInt(
                                      PossibleAnnouncerCount,
                                      0) &&
                                SetVarBool(
                                      out AnnouncementSaid,
                                      false) &&
                                ForEach(PossibleAnnouncers, ReferenceUnit =>
                                            // Sequence name :Sequence

                                            AnnouncementSaid == false &&
                                            GetDistanceBetweenUnits(
                                                  out Distance,
                                                  CapturedGuardian,
                                                  ReferenceUnit) &&
                                            LessEqualFloat(
                                                  Distance,
                                                  Radius) &&
                                            TestUnitHasBuff(
                                                  ReferenceUnit,
                                                  null,
                                                  "OdinCaptureChannel") &&
                                            Announcement_OnCapturePointCaptured(
                                                  CapturedPoint,
                                                  ReferenceUnit,
                                                  CapturePointIndex) &&
                                            SetVarBool(
                                                  out AnnouncementSaid,
                                                  true)

                                ) &&
                                AnnouncementSaid == true
                          ) ||
                                // Sequence name :Sequence

                                Announcement_OnCapturePointCaptured(
                                      CapturedPoint,
                                      null,
                                      CapturePointIndex)


                    )
              ;
    }
}

