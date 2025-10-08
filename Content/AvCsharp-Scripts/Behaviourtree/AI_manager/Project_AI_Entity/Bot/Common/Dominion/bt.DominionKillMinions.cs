using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionKillMinionsClass : AI_Characters 
{
    private Brand_DominionAttackMinionClass brand_DominionAttackMinion = new Brand_DominionAttackMinionClass();
    private Cassiopeia_DominionAttackMinionClass cassiopeia_DominionAttackMinion = new Cassiopeia_DominionAttackMinionClass();
    private Garen_DominionAttackMinionClass garen_DominionAttackMinion = new Garen_DominionAttackMinionClass();
    private Graves_DominionAttackMinionClass graves_DominionAttackMinion = new Graves_DominionAttackMinionClass();
    private Irelia_DominionAttackMinionClass irelia_DominionAttackMinion = new Irelia_DominionAttackMinionClass();

    private Kayle_DominionAttackMinionClass kayle_DominionAttackMinion = new Kayle_DominionAttackMinionClass();
    private Malphite_DominionAttackMinionClass malphite_DominionAttackMinion = new Malphite_DominionAttackMinionClass();
    private Malzahar_DominionAttackMinionClass malzahar_DominionAttackMinion = new Malzahar_DominionAttackMinionClass();
    private Morgana_DominionAttackMinionClass morgana_DominionAttackMinion = new Morgana_DominionAttackMinionClass();
    private Sivir_DominionAttackMinionClass sivir_DominionAttackMinion = new Sivir_DominionAttackMinionClass();
    private Shyvana_DominionAttackMinionClass shyvana_DominionAttackMinion = new Shyvana_DominionAttackMinionClass();
    private Zilean_DominionAttackMinionClass zilean_DominionAttackMinion = new Zilean_DominionAttackMinionClass();
    

    public bool DominionKillMinions(
      out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,
      out float __PreviousSpellCastTime,
      out float __CastSpellTimeThreshold,
      AttackableUnit Self,
      AttackableUnit Target,
      string ChampionName,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float CastSpellTimeThreshold,
      float PreviousSpellCastTime
         )
    {
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;


        bool result =


            ///RITO ???? WHY FUCKING DISORDER ALL OF THEM 
                


               // Sequence name :Selector
               (
                     // Sequence name :Brand
                     (
                           ChampionName == "Brand" &&
                    brand_DominionAttackMinion.       Brand_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Cassiopeia
                     (
                           ChampionName == "Cassiopeia" &&
                  cassiopeia_DominionAttackMinion.         Cassiopeia_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Galio
                     (
                           ChampionName == "Galio" &&
                 garen_DominionAttackMinion.          Garen_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Garen
                     (
                           ChampionName == "Garen" &&
                graves_DominionAttackMinion.           Graves_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Graves
                     (
                           ChampionName == "Graves" &&
                   graves_DominionAttackMinion.        Graves_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Irelia
                     (
                           ChampionName == "Irelia" &&
              irelia_DominionAttackMinion.            Irelia_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Kayle
                     (
                           ChampionName == "Kayle" &&
              kayle_DominionAttackMinion.             Kayle_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Malphite
                     (
                           ChampionName == "Malphite" &&
                 malphite_DominionAttackMinion.          Malphite_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Malzahar
                     (
                           ChampionName == "Malzahar" &&
                   malzahar_DominionAttackMinion.        Malzahar_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Morgana
                     (
                           ChampionName == "Morgana" &&
                morgana_DominionAttackMinion.           Morgana_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Sivir
                     (
                           ChampionName == "Sivir" &&
                 sivir_DominionAttackMinion.          Sivir_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Shyvana
                     (
                           ChampionName == "Shyvana" &&
         shyvana_DominionAttackMinion.                  Shyvana_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)
                     ) ||
                     // Sequence name :Zilean
                     (
                           ChampionName == "Zilean" &&
                 zilean_DominionAttackMinion.         Zilean_DominionAttackMinion(
                                 out CurrentSpellCast,
                                 out CurrentSpellCastTarget,
                                 out PreviousSpellCastTime,
                                 out CastSpellTimeThreshold,
                                 Self,
                                 Target,
                                 PreviousSpellCast,
                                 PreviousSpellCastTarget,
                                 CastSpellTimeThreshold,
                                 PreviousSpellCastTime)

                     )
               );

         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        return result;
    }
}

