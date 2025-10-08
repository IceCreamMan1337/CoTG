namespace BehaviourTrees.all;


class DominionKillMinionsClass : AI_Characters
{
    private Brand_DominionAttackMinionClass brand_DominionAttackMinion = new();
    private Cassiopeia_DominionAttackMinionClass cassiopeia_DominionAttackMinion = new();
    private Garen_DominionAttackMinionClass garen_DominionAttackMinion = new();
    private Graves_DominionAttackMinionClass graves_DominionAttackMinion = new();
    private Irelia_DominionAttackMinionClass irelia_DominionAttackMinion = new();

    private Kayle_DominionAttackMinionClass kayle_DominionAttackMinion = new();
    private Malphite_DominionAttackMinionClass malphite_DominionAttackMinion = new();
    private Malzahar_DominionAttackMinionClass malzahar_DominionAttackMinion = new();
    private Morgana_DominionAttackMinionClass morgana_DominionAttackMinion = new();
    private Sivir_DominionAttackMinionClass sivir_DominionAttackMinion = new();
    private Shyvana_DominionAttackMinionClass shyvana_DominionAttackMinion = new();
    private Zilean_DominionAttackMinionClass zilean_DominionAttackMinion = new();


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

                     // Sequence name :Brand
                     (
                           ChampionName == "Brand" &&
                    brand_DominionAttackMinion.Brand_DominionAttackMinion(
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
                  cassiopeia_DominionAttackMinion.Cassiopeia_DominionAttackMinion(
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
                 garen_DominionAttackMinion.Garen_DominionAttackMinion(
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
                graves_DominionAttackMinion.Graves_DominionAttackMinion(
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
                   graves_DominionAttackMinion.Graves_DominionAttackMinion(
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
              irelia_DominionAttackMinion.Irelia_DominionAttackMinion(
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
              kayle_DominionAttackMinion.Kayle_DominionAttackMinion(
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
                 malphite_DominionAttackMinion.Malphite_DominionAttackMinion(
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
                   malzahar_DominionAttackMinion.Malzahar_DominionAttackMinion(
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
                morgana_DominionAttackMinion.Morgana_DominionAttackMinion(
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
                 sivir_DominionAttackMinion.Sivir_DominionAttackMinion(
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
         shyvana_DominionAttackMinion.Shyvana_DominionAttackMinion(
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
                 zilean_DominionAttackMinion.Zilean_DominionAttackMinion(
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
               ;

        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        return result;
    }
}

