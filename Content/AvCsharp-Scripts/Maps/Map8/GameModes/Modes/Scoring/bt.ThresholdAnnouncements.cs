using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ThresholdAnnouncementsClass : OdinLayout 
{
    float lastTimeExecuted_EP_ThresholdAnnouncements;
    public bool For_EP_ThresholdAnnouncements(
                out uint ActiveMusicCue,
    float ChaosScore,
    float OrderScore
          )
    {
        uint _ActiveMusicCue = default;

        bool result =  // Sequence name :Selector
               (
                     // Sequence name :Initialization
                     (
                           __IsFirstRun == true &&
                           SetVarBool(
                                 out HitOrderThreshold1,
                                 false) &&
                           SetVarBool(
                                 out HitOrderThreshold2,
                                 false) &&
                           SetVarBool(
                                 out HitOrderThreshold3,
                                 false) &&
                           SetVarBool(
                                 out HitOrderThreshold4,
                                 false) &&
                           SetVarBool(
                                 out HitChaosThreshold1,
                                 false) &&
                           SetVarBool(
                                 out HitChaosThreshold2,
                                 false) &&
                           SetVarBool(
                                 out HitChaosThreshold3,
                                 false) &&
                           SetVarBool(
                                 out HitChaosThreshold4,
                                 false) &&
                           SetVarBool(
                                 out HitEndOfGame,
                                 false) &&
                           GetTurret(
                                 out OrderShrineTurret,
                                 TeamId.TEAM_ORDER,
                                 0,
                                 1) &&
                           GetTurret(
                                 out ChaosShrineTurret,
                                 TeamId.TEAM_CHAOS,
                                 0,
                                 1) &&
                           GetChampionCollection(
                                 out HeroCollection) &&
                           ForEach(HeroCollection, LocalHero => (                        
                           // Sequence name :Sequence
                                 (
                                       PrepareMusicCue(
                                             LocalHero,
                                             14) &&
                                       BeginMusicCue(
                                             LocalHero,
                                             14) &&
                                       PrepareMusicCue(
                                             LocalHero,
                                             15) &&
                                       PrepareMusicCue(
                                             LocalHero,
                                             16) &&
                                       PrepareMusicCue(
                                             LocalHero,
                                             17) &&
                                       PrepareMusicCue(
                                             LocalHero,
                                             18) &&
                                       PrepareMusicCue(
                                             LocalHero,
                                             22)
                                 ))
                           ) &&
                           SetVarUnsignedInt(
                                 out _ActiveMusicCue,
                                 14)
                     ) ||
                     // Sequence name :Selector
                     (
                           // Sequence name :OrderAnnouncements
                           (
                                 // Sequence name :Sequence
                                 (
                                       HitOrderThreshold1 == false &&
                                       LessFloat(
                                             OrderScore,
                                             375) &&
                                       Announcement_OnVictoryPointThreshold(
                                             OrderShrineTurret,
                                             1) &&
                                       SetVarBool(
                                             out HitOrderThreshold1,
                                             true) &&
                                       HitChaosThreshold1 == false &&
                                       DebugAction(

                                             "Order1") &&
                                       ForEach(HeroCollection, LocalHero => (                            
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         14) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         15) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         15)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out ActiveMusicCue,
                                             15)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitOrderThreshold2 == false &&
                                       LessFloat(
                                             OrderScore,
                                             250) &&
                                       Announcement_OnVictoryPointThreshold(
                                             OrderShrineTurret,
                                             2) &&
                                       SetVarBool(
                                             out HitOrderThreshold2,
                                             true) &&
                                       HitChaosThreshold2 == false &&
                                       DebugAction(

                                             "Order2") &&
                                       ForEach(HeroCollection, LocalHero => (
                                             // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         15) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         16) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         16)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             16)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitOrderThreshold3 == false &&
                                       LessFloat(
                                             OrderScore,
                                             125) &&
                                       Announcement_OnVictoryPointThreshold(
                                             OrderShrineTurret,
                                             3) &&
                                       SetVarBool(
                                             out HitOrderThreshold3,
                                             true) &&
                                       HitChaosThreshold3 == false &&
                                       DebugAction(

                                             "Order3") &&
                                       ForEach(HeroCollection, LocalHero => (                             
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         16) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         17) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         17)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             17)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitOrderThreshold4 == false &&
                                       LessFloat(
                                             OrderScore,
                                             75) &&
                                       SetVarBool(
                                             out HitOrderThreshold4,
                                             true) &&
                                       HitChaosThreshold4 == true &&
                                       DebugAction(
                                             "Order4") &&
                                       ForEach(HeroCollection, LocalHero => (                       
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         17) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         18) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         18)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             18)
                                 )
                           ) ||
                           // Sequence name :ChaosAnnouncements
                           (
                                 // Sequence name :Sequence
                                 (
                                       HitChaosThreshold1 == false &&
                                       LessFloat(
                                             ChaosScore,
                                             375) &&
                                       Announcement_OnVictoryPointThreshold(
                                             ChaosShrineTurret,
                                             1) &&
                                       SetVarBool(
                                             out HitChaosThreshold1,
                                             true) &&
                                       HitOrderThreshold1 == false &&
                                       DebugAction(
                                             "Chaos1") &&
                                       ForEach(HeroCollection, LocalHero => (                                     
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         14) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         15) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         15)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             15)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitChaosThreshold2 == false &&
                                       LessFloat(
                                             ChaosScore,
                                             250) &&
                                       Announcement_OnVictoryPointThreshold(
                                             ChaosShrineTurret,
                                             2) &&
                                       SetVarBool(
                                             out HitChaosThreshold2,
                                             true) &&
                                       HitOrderThreshold2 == false &&
                                       DebugAction(
                                             "Chaos2") &&
                                       ForEach(HeroCollection, LocalHero => (                                     
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         15) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         16) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         16)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             16)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitChaosThreshold3 == false &&
                                       LessFloat(
                                             ChaosScore,
                                             125) &&
                                       Announcement_OnVictoryPointThreshold(
                                             ChaosShrineTurret,
                                             3) &&
                                       SetVarBool(
                                             out HitChaosThreshold3,
                                             true) &&
                                       HitOrderThreshold3 == false &&
                                       DebugAction(
                                             "Chaos3") &&
                                       ForEach(HeroCollection, LocalHero => (
                                             // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         16) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         17) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         17)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             17)
                                 ) ||
                                 // Sequence name :Sequence
                                 (
                                       HitChaosThreshold4 == false &&
                                       LessFloat(
                                             ChaosScore,
                                             75) &&
                                       SetVarBool(
                                             out HitChaosThreshold4,
                                             true) &&
                                       HitOrderThreshold4 == true &&
                                       DebugAction(
                                             "Chaos4") &&
                                       ForEach(HeroCollection, LocalHero => (                                    
                                       // Sequence name :Sequence
                                             (
                                                   EndMusicCue(
                                                         LocalHero,
                                                         17) &&
                                                   PrepareMusicCue(
                                                         LocalHero,
                                                         18) &&
                                                   BeginMusicCue(
                                                         LocalHero,
                                                         18)
                                             ))
                                       ) &&
                                       SetVarUnsignedInt(
                                             out _ActiveMusicCue,
                                             18)
                                 )
                           ) ||
                           // Sequence name :Sequence
                           (
                                 HitEndOfGame == false &&
                                 // Sequence name :Selector
                                 (
                                       LessFloat(
                                             OrderScore,
                                             0) ||
                                             LessFloat(
                                             ChaosScore,
                                             0)
                                    ) &&
                                    SetVarBool(
                                          out HitEndOfGame,
                                          true) &&
                                    ForEach(HeroCollection, LocalHero => (                               
                                    // Sequence name :Sequence
                                          (
                                                PrepareMusicCue(
                                                      LocalHero,
                                                      22) &&
                                                BeginMusicCue(
                                                      LocalHero,
                                                      22)
                                          ))
                                    ) &&
                                    SetVarUnsignedInt(
                                          out _ActiveMusicCue,
                                          22)

                              )
                        )
                 );

        ActiveMusicCue = _ActiveMusicCue;
        return result;
    }
        public bool ThresholdAnnouncements(
                out uint ActiveMusicCue,
    float ChaosScore,
    float OrderScore
          )
      {

        var for_EP_ThresholdAnnouncements = new ThresholdAnnouncementsClass();


        uint _ActiveMusicCue = default;

        List<Func<bool>> EP_ThresholdAnnouncements = new List<Func<bool>>
{ () =>

        {
            return for_EP_ThresholdAnnouncements.For_EP_ThresholdAnnouncements(
                out _ActiveMusicCue,
                ChaosScore,
                OrderScore);
        }
        };


        bool result =
            ExecutePeriodically(
               ref lastTimeExecuted_EP_ThresholdAnnouncements,
                EP_ThresholdAnnouncements,
                  true,
                  1);

        ActiveMusicCue = _ActiveMusicCue;
        return result;
      }
}

