using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class BuyExtraItemClass : AI_Characters 
{

     public bool BuyExtraItem(

      out string __ExtraItem,
      out bool __ExtraItemPurchased,
      AttackableUnit Self,
      string ExtraItem,
      bool ExtraItemPurchased
         )
    {
        string _ExtraItem = ExtraItem;
        bool _ExtraItemPurchased = ExtraItemPurchased;


        bool result =
              // Sequence name :ExtraItem
              (
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :SetExtraItem
                          (
                                ExtraItem == "NotSet" &&
                                // Sequence name :WhichDoransItem?
                                (
                                      // Sequence name :DoransShield
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1054,
                                                  true) &&
                                            // Sequence name :PickExtraItem
                                            (
                                                  // Sequence name :BansheesVeil
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3102,
                                                              false) &&
                                                        GetUnitPARType(
                                                              out SelfPARType,
                                                              Self) &&
                                                        SelfPARType == PrimaryAbilityResourceType.MANA &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "BansheesVeil")
                                                  ) ||
                                                  // Sequence name :ForceOfNature
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3109,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "ForceOfNature")
                                                  ) ||
                                                  // Sequence name :GuardianAngel
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3026,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "GuardianAngel")
                                                  ) ||
                                                  SetVarString(
                                                        out ExtraItem,
                                                        "WarmogsArmor")
                                            )
                                      ) ||
                                      // Sequence name :DoransBlade
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1055,
                                                  true) &&
                                            // Sequence name :PickExtraItem
                                            (
                                                  // Sequence name :TheBloodthirster
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3072,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "TheBloodthirster")
                                                  ) ||
                                                  // Sequence name :InfinityEdge
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3031,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "InfinityEdge")
                                                  ) ||
                                                  // Sequence name :MawOfMalmortius
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3156,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "MawOfMalmortius")
                                                  ) ||
                                                  SetVarString(
                                                        out ExtraItem,
                                                        "TheBloodthirster")
                                            )
                                      ) ||
                                      // Sequence name :DoransRing
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1056,
                                                  true) &&
                                            // Sequence name :PickExtraItem
                                            (
                                                  // Sequence name :RabadonsDeathcap
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3089,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "RabadonsDeathcap")
                                                  ) ||
                                                  // Sequence name :AbyssalScepter
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3001,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "AbyssalScepter")
                                                  ) ||
                                                  // Sequence name :RylaisCrystalScepter
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3116,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "RylaisCrystalScepter")
                                                  ) ||
                                                  // Sequence name :VoidStaff
                                                  (
                                                        TestChampionHasItem(
                                                              Self,
                                                              3135,
                                                              false) &&
                                                        SetVarString(
                                                              out ExtraItem,
                                                              "VoidStaff")
                                                  ) ||
                                                  SetVarString(
                                                        out ExtraItem,
                                                        "RabadonsDeathcap")
                                            )
                                      ) ||
                                      SetVarString(
                                            out ExtraItem,
                                            "None")
                                )
                          )
                    ) && MaskFailure()
                    &&
                    // Sequence name :BuyExtraItem
                    (
                          // Sequence name :None
                          (
                                ExtraItem == "None" &&
                                SetVarBool(
                                      out ExtraItemPurchased,
                                      true)
                          ) ||
                          // Sequence name :BansheesVeil
                          (
                                ExtraItem == "BansheesVeil" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransShield-&gt,RubyCrystal
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1054,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  300) &&
                                            UnitAISellItem(
                                                  1054) &&
                                            UnitAIBuyItem(
                                                  1028)
                                      ) ||
                                      // Sequence name :RubyCrystal-&gt,CatalystTheProtector
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1028,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3010,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3010)
                                      ) ||
                                      // Sequence name :CatalystTheProtector-&gt,BansheesVeil
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  3010,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3102,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3102) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :GuardianAngel
                          (
                                ExtraItem == "GuardianAngel" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransShield-&gt,ChainVest
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1054,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  500) &&
                                            UnitAISellItem(
                                                  1054) &&
                                            UnitAIBuyItem(
                                                  1031)
                                      ) ||
                                      // Sequence name :ChainVest-&gt,GuardianAngel
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1031,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3026,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3026) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :ForceOfNature
                          (
                                ExtraItem == "ForceOfNature" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransShield-&gt,NegatronCloak
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1054,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  550) &&
                                            UnitAISellItem(
                                                  1054) &&
                                            UnitAIBuyItem(
                                                  1057)
                                      ) ||
                                      // Sequence name :NegatronCloak-&gt,ForceOfNature
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1057,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3109,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3109) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :WarmogsArmor
                          (
                                ExtraItem == "WarmogsArmor" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransShield-&gt,GiantsBelt
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1054,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  900) &&
                                            UnitAISellItem(
                                                  1054) &&
                                            UnitAIBuyItem(
                                                  1011)
                                      ) ||
                                      // Sequence name :GiantsBelt-&gt,WarmogsArmor
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1011,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3083,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3083) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :TheBloodthirster
                          (
                                ExtraItem == "TheBloodthirster" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransBlade-&gt,BFSword
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1055,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  1450) &&
                                            UnitAISellItem(
                                                  1055) &&
                                            UnitAIBuyItem(
                                                  1038)
                                      ) ||
                                      // Sequence name :BFSword-&gt,TheBloodthirster
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1038,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3072,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3072) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :InfinityEdge
                          (
                                ExtraItem == "InfinityEdge" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransBlade-&gt,BFSword
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1055,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  1450) &&
                                            UnitAISellItem(
                                                  1055) &&
                                            UnitAIBuyItem(
                                                  1038)
                                      ) ||
                                      // Sequence name :BFSword-&gt,TheBloodthirster
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1038,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3031,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3031) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :MawOfMalmortius
                          (
                                ExtraItem == "MawOfMalmortius" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransBlade-&gt,Hexdrinker
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1055,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  1200) &&
                                            UnitAISellItem(
                                                  1055) &&
                                            UnitAIBuyItem(
                                                  3155)
                                      ) ||
                                      // Sequence name :Hexdrinker-&gt,Malmortius
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  3155,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3156,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3156) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :RabadonsDeathcap
                          (
                                ExtraItem == "RabadonsDeathcap" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransRing-&gt,NeedlesslyLargeRod
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1056,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  1400) &&
                                            UnitAISellItem(
                                                  1056) &&
                                            UnitAIBuyItem(
                                                  1058)
                                      ) ||
                                      // Sequence name :NeedlesslyLargeRod-&gt,Deathcap
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1058,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3089,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3089) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :AbyssalScepter
                          (
                                ExtraItem == "AbyssalScepter" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransRing-&gt,BlastingWand
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1056,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  650) &&
                                            UnitAISellItem(
                                                  1056) &&
                                            UnitAIBuyItem(
                                                  1026)
                                      ) ||
                                      // Sequence name :BlastingWand-&gt,AbyssalScepter
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1026,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3001,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3001) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :RylaisCrystalScepter
                          (
                                ExtraItem == "RylaisCrystalScepter" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransRing-&gt,GiantsBelt
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1056,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  900) &&
                                            UnitAISellItem(
                                                  1056) &&
                                            UnitAIBuyItem(
                                                  1011)
                                      ) ||
                                      // Sequence name :GiantsBelt-&gt,Rylais
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1011,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3116,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3116) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)
                                      )
                                )
                          ) ||
                          // Sequence name :VoidStaff
                          (
                                ExtraItem == "VoidStaff" &&
                                // Sequence name :BuyComponents
                                (
                                      // Sequence name :DoransRing-&gt,BlastingWand
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1056,
                                                  true) &&
                                            GetUnitGold(
                                                  out CurrentGold,
                                                  Self) &&
                                            GreaterFloat(
                                                  CurrentGold,
                                                  650) &&
                                            UnitAISellItem(
                                                  1056) &&
                                            UnitAIBuyItem(
                                                  1026)
                                      ) ||
                                      // Sequence name :BlastingWand-&gt,VoidStaff
                                      (
                                            TestChampionHasItem(
                                                  Self,
                                                  1026,
                                                  true) &&
                                            TestUnitAICanBuyItem(
                                                  3135,
                                                  true) &&
                                            UnitAIBuyItem(
                                                  3135) &&
                                            SetVarBool(
                                                  out ExtraItemPurchased,
                                                  true)

                                      )
                                )
                          )
                    )
              );
         __ExtraItem = _ExtraItem;
         __ExtraItemPurchased = _ExtraItemPurchased;
        return result;
      }
}

