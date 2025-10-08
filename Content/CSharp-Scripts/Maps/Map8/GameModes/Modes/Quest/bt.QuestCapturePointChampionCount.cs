using static CoTGEnumNetwork.Enums.SpellDataFlags;

namespace BehaviourTrees;


class QuestCapturePointChampionCountClass : OdinLayout
{


    public bool QuestCapturePointChampionCount(
               out int NumAllies,
     out int NumEnemies,
   AttackableUnit Guardian
         )
    {

        int _NumAllies = default;
        int _NumEnemies = default;


        bool result =
                          // Sequence name :MaskFailure

                          // Sequence name :Sequence
                          (
                                SetVarInt(
                                      out _NumAllies,
                                      0) &&
                                SetVarInt(
                                      out _NumEnemies,
                                      0) &&
                                GetUnitTeam(
                                      out GuardianTeam,
                                      Guardian) &&
                                NotEqualUnitTeam(
                                      GuardianTeam,
                                      TeamId.TEAM_NEUTRAL) &&
                                GetUnitPosition(
                                      out GuardianPosition,
                                      Guardian) &&
                                GetUnitsInTargetArea(
                                      out Champions,
                                      Guardian,
                                      GuardianPosition,
                                      1500,
                                      AffectEnemies | AffectFriends | AffectHeroes
                                      ) &&
                                ForEach(Champions, Champion =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out ChampionTeam,
                                                  Champion) &&
                                            // Sequence name :AllyOrEnemy
                                            (
                                                  // Sequence name :Ally
                                                  (
                                                        ChampionTeam == GuardianTeam &&
                                                        AddInt(
                                                              out _NumAllies,
                                                              _NumAllies,
                                                              1)
                                                  ) ||
                                                  // Sequence name :Enemy
                                                  (
                                                        GetDistanceBetweenUnits(
                                                              out Distance,
                                                              Guardian,
                                                              Champion) &&
                                                        LessFloat(
                                                              Distance,
                                                              900) &&
                                                        AddInt(
                                                              out _NumEnemies,
                                                              _NumEnemies,
                                                              1)

                                                  )
                                            )

                                )
                          )
                          ||
                                       DebugAction("MaskFailure")
                    ;
        NumEnemies = _NumEnemies;
        NumAllies = _NumAllies;
        return result;
    }
}

