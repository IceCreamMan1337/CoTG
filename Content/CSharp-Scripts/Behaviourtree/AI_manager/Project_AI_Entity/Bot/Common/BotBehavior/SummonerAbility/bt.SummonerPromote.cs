using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.all;


class SummonerPromoteClass : AI_Characters
{


    public bool SummonerPromote(
        int PromoteSlot,
     AttackableUnit Self
        )
    {
        return
                    // Sequence name :Sequence

                    // Sequence name :Difficulty
                    (
                          TestEntityDifficultyLevel(
                                true,
                           EntityDiffcultyType.DIFFICULTY_INTERMEDIATE)
                          ||
                          TestEntityDifficultyLevel(
                                true,
                          EntityDiffcultyType.DIFFICULTY_ADVANCED)
                    ) &&
                    GreaterEqualInt(
                          PromoteSlot,
                          0) &&
                    TestCanCastSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          PromoteSlot,
                          true) &&
                    GetUnitPosition(
                          out SelfPosition,
                          Self) &&
                    GetUnitsInTargetArea(
                          out AllyMinions,
                          Self,
                          SelfPosition,
                          900,
                          AffectFriends | AffectMinions) &&
                    SetVarInt(
                          out TotalSuperMinions,
                          0) &&
                    ForEach(AllyMinions, Minion =>
                                // Sequence name :Sequence

                                GetUnitBuffCount(
                                      out BuffCount,
                                      Minion,
                                      "SummonerOdinPromote") &&
                                GreaterInt(
                                      BuffCount,
                                      0) &&
                                AddInt(
                                      out TotalSuperMinions,
                                      TotalSuperMinions,
                                      1)

                    ) &&
                    TotalSuperMinions == 0 &&
                    SetAIUnitSpellTarget(
                          Self,
                          SPELLBOOK_SUMMONER,
                          PromoteSlot) &&
                    CastUnitSpell(
                          Self,
                          SPELLBOOK_SUMMONER,
                          PromoteSlot,
                          default,
                          default)

              ;
    }
}

