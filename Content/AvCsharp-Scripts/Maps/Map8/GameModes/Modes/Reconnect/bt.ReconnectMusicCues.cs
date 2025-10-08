using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees;


class ReconnectMusicCuesClass : OdinLayout 
{
     

     public bool ReconnectMusicCues(

           uint CurrentMusicCue,
    AttackableUnit ReconnectingPlayer
          )
      {
      return
            // Sequence name :Sequence
            (
                  PrepareMusicCue(
                        ReconnectingPlayer, 
                        CurrentMusicCue) &&
                  BeginMusicCue(
                        ReconnectingPlayer, 
                        CurrentMusicCue)

            );
      }
}

