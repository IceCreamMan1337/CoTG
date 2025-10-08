namespace BehaviourTrees;


class RespawnHelperClass : OdinLayout
{


    public bool RespawnHelper(
         out int __Modifier,
   int NewModifier,
   int Modifier,
   TeamId Team
         )
    {

        int _Modifier = Modifier;

        bool result =
                          // Sequence name :MaskFailure

                          // Sequence name :Sequence
                          (
                                NotEqualInt(
                                      Modifier,
                                      NewModifier) &&
                                SubtractInt(
                                      out ModifierDiff,
                                      Modifier,
                                      NewModifier) &&
                                MultiplyFloat(
                                      out ModifierToSet,
                                      ModifierDiff,
                                      1) &&
                                GetChampionCollection(
                                      out AllChampions) &&
                                ForEach(AllChampions, Champ =>
                                            // Sequence name :Sequence

                                            GetUnitTeam(
                                                  out UnitTeam,
                                                  Champ) &&
                                            Team == UnitTeam &&
                                            IncPermanentFlatRespawnTimeMod(
                                                  Champ,
                                                  ModifierToSet)

                                ) &&
                                SetVarInt(
                                      out _Modifier,
                                      NewModifier)

                          )
                          ||
                                       DebugAction("MaskFailure")
                    ;
        __Modifier = _Modifier;
        return result;
    }
}

