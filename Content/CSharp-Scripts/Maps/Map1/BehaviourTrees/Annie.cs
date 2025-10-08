using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map1;

class Annie : BehaviourTree
{
    AttackableUnit Self;
    int PotionsToBuy;
    bool ValueChanged;
    IEnumerable<AttackableUnit> TargetCollection;
    Vector3 SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3 AggroPosition;
    Vector3 AssistPosition;
    bool TeleportHome;
    float AccumulatedDamage;
    float TotalUnitStrength;
    float StrengthRatioOverTime;
    float DeaggroDistance;
    float PrevHealth;
    float PrevTime;
    bool LowThreatMode;

    public Annie()
    {

    }
    public Annie(Champion owner) : base(owner)
    {

    }

    public override void Update()
    {

        base.Update();
        AnnieBehavior();
    }
    bool AnnieBehavior()
    {
        AttackableUnit Taunter;
        return

            AnnieInit() &&

            (

               (
                    TestUnitHasBuff(this.Self, null, "Taunt") &&
                    GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                    SetUnitAIAttackTarget(Taunter) &&
                    AnnieAutoAttackTarget()
                ) ||
                AnnieAtBaseHealAndBuy() ||
                AnnieLevelUp() ||
                AnnieGameNotStarted() ||
                AnnieHighThreatManagement() ||
                AnnieReturnToBase() ||
                AnnieHeal() ||
                AnnieKillChampion() ||
                AnnieLowThreatManagement() ||
                AnnieAttack() ||
                AnniePushLane()
            )
        ;
    }

    bool AnnieAtBaseHealAndBuy()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        return

            GetUnitAIBasePosition(out BaseLocation, this.Self) &&
            DistanceBetweenObjectAndPoint(out Distance, this.Self, BaseLocation) &&
            Distance <= 450 &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f
                ) ||

                    (
                        TestChampionHasItem(this.Self, 3108, false) &&
                        (
                            (
                                TestChampionHasItem(this.Self, 1052, false) &&
                                TestUnitAICanBuyItem(1052) &&
                                UnitAIBuyItem(1052)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 1005, false) &&
                                TestUnitAICanBuyItem(1005) &&
                                UnitAIBuyItem(1005)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 3108, false) &&
                                TestUnitAICanBuyItem(3108) &&
                                UnitAIBuyItem(3108)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3108) &&
                        TestChampionHasItem(this.Self, 3009, false) &&
                        (
                            (
                                TestChampionHasItem(this.Self, 1001, false) &&
                                TestUnitAICanBuyItem(1001) &&
                                UnitAIBuyItem(1001)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 3009, false) &&
                                TestUnitAICanBuyItem(3009) &&
                                UnitAIBuyItem(3009)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3108) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3136, false) &&
                        (
                            (
                                TestChampionHasItem(this.Self, 1028, false) &&
                                TestUnitAICanBuyItem(1028) &&
                                UnitAIBuyItem(1028)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 1052, false) &&
                                TestUnitAICanBuyItem(1052) &&
                                UnitAIBuyItem(1052)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 3136, false) &&
                                TestUnitAICanBuyItem(3136) &&
                                UnitAIBuyItem(3136)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3108) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3136) &&
                        TestChampionHasItem(this.Self, 3010, false) &&
                        (
                            (
                                TestChampionHasItem(this.Self, 1028, false) &&
                                TestUnitAICanBuyItem(1028) &&
                                UnitAIBuyItem(1028)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 1027, false) &&
                                TestUnitAICanBuyItem(1027) &&
                                UnitAIBuyItem(1027)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 3010, false) &&
                                TestUnitAICanBuyItem(3010) &&
                                UnitAIBuyItem(3010)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3108) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3136) &&
                        TestChampionHasItem(this.Self, 3010) &&
                        TestChampionHasItem(this.Self, 3098, false) &&
                        (
                            (
                                TestChampionHasItem(this.Self, 1052, false) &&
                                TestUnitAICanBuyItem(1052) &&
                                UnitAIBuyItem(1052)
                            ) ||
                            (
                                TestChampionHasItem(this.Self, 3098, false) &&
                                TestUnitAICanBuyItem(3098) &&
                                UnitAIBuyItem(3098)
                            )
                        )
                    ) ||
                    (
                        this.PotionsToBuy > 0 &&
                        TestChampionHasItem(this.Self, 2003, false) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )

            )
        ;
    }

    bool AnnieLevelUp()
    {
        int SkillPoints;
        return

            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            SkillPoints > 0 &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 2) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                )
            )
        ;
    }

    bool AnnieGameNotStarted()
    {

        return

            !TestGameStarted()
        ;
    }

    bool AnnieFindClosestTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return

            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection, Attacker =>
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            )
        ;
    }

    bool AnnieAttack()
    {

        return

                    DebugAction("AnnieAttack") &&
            AnnieAcquireTarget() &&
            AnnieAttackTarget()
        ;
    }

    bool AnnieAcquireTarget()
    {
        IEnumerable<AttackableUnit> FriendlyUnits;
        AttackableUnit unit;
        int Count;
        float Distance;
        return

            (
                SetVarBool(out this.LostAggro, false) &&
                TestUnitAIAttackTargetValid() &&
                GetUnitAIAttackTarget(out this.AggroTarget) &&
                SetVarVector(out this.AggroPosition, this.AssistPosition) &&
                TestUnitIsVisible(this.Self, this.AggroTarget) &&
                AnnieDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    AnnieFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                ) &&
                this.ValueChanged == true
            ) ||
            (

             DebugAction("search target") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&

                  DebugAction("found target") &&
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&

                    ForEach(TargetCollection, unit =>
                        DistanceBetweenObjectAndPoint(out Distance, unit, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
                        TestUnitIsVisible(this.Self, unit) &&
                        DebugAction("target good distance and visible") &&
                        (
                            (
                                this.LostAggro == true &&
                                GetUnitAIAttackTarget(out this.AggroTarget) &&
                                this.AggroTarget != unit
                            ) ||
                            this.LostAggro == false
                        ) &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )
                 &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition)
            )
        ;
    }

    bool AnnieAttackTarget()
    {
        AttackableUnit Target;
        TeamId SelfTeam;
        TeamId TargetTeam;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
        return

            DebugAction("ANNNNNNIE    Attack") &&
            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, this.Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitType(out UnitType, Target) &&

                    DebugAction("UNITTYPE = " + UnitType) &&
                    UnitType == MINION_UNIT &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.3f &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    AnnieCanCastAbility0() &&
                    AnnieCastAbility0()
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    TestUnitHasBuff(this.Self, null, "Pyromania_particle") &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    (
                        (
                            AnnieCanCastAbility3() &&
                            AnnieCastAbility3()
                        ) ||
                        (
                        DebugAction("tRY CAST Q") &&
                            AnnieCanCastAbility0() &&
                            DebugAction("CAN CAST Q") &&
                            AnnieCastAbility0() &&
                            DebugAction("SUCCESS CAST Q")
                        ) ||
                        (
                            AnnieCanCastAbility1() &&
                            AnnieCastAbility1()
                        )
                    )
                ) ||
                AnnieAutoAttackTarget()
            )
        ;
    }

    bool AnnieReturnToBase()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        Vector3 TeleportPosition;
        float DistanceToTeleportPosition;
        return

            GetUnitAIBasePosition(out BaseLocation, this.Self) &&
            DistanceBetweenObjectAndPoint(out Distance, this.Self, BaseLocation) &&
            Distance > 300 &&

                GetUnitMaxHealth(out MaxHealth, this.Self) &&
                GetUnitCurrentHealth(out Health, this.Self) &&
                DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                (
                    (
                        this.TeleportHome == true &&
                        Health_Ratio <= 0.35f
                    ) ||
                    (
                        this.TeleportHome == false &&
                        Health_Ratio <= 0.25f &&
                        SetVarBool(out this.TeleportHome, true)
                    )
                )
             &&
            (
                (
                    SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends | AffectTurrets) &&
                    AnnieFindClosestTarget() &&
                    this.ValueChanged == true &&
                    (
                        (
                            GetDistanceBetweenUnits(out Distance, this.CurrentClosestTarget, this.Self) &&
                            Distance < 125 &&
                            (
                                (
                                    TestUnitAISpellPositionValid() &&
                                    GetUnitAISpellPosition(out TeleportPosition) &&
                                    DistanceBetweenObjectAndPoint(out DistanceToTeleportPosition, this.Self, TeleportPosition) &&
                                    DistanceToTeleportPosition < 50
                                ) ||
                                !TestUnitAISpellPositionValid()
                            ) &&
                            (
                                TestUnitIsChanneling(this.Self) ||
                                IssueTeleportToBaseOrder()
                            ) &&
                            ClearUnitAISpellPosition()
                        ) ||
                        (
                            (
                                (
                                    (
                                        !TestUnitAISpellPositionValid() ||
                                        (
                                            GetUnitAISpellPosition(out TeleportPosition) &&
                                            DistanceBetweenObjectAndPoint(out Distance, this.CurrentClosestTarget, TeleportPosition) &&
                                            Distance > 500
                                        )
                                    ) &&
                                    ComputeUnitAISpellPosition(this.CurrentClosestTarget, this.Self, 150, false)
                                ) ||
                                TestUnitAISpellPositionValid()
                            ) &&
                            GetUnitAISpellPosition(out TeleportPosition) &&
                            IssueMoveToPositionOrder(TeleportPosition)
                        )
                    )
                ) ||
                (
                    GetUnitAIBasePosition(out BaseLocation, this.Self) &&
                    IssueMoveToPositionOrder(BaseLocation)
                )
            )
        ;
    }

    bool AnnieHighThreatManagement()
    {
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        float Range;
        AttackableUnit Unit;
        UnitType UnitType;
        return

            (
                (
                    TestUnitUnderAttack(this.Self) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
                    Damage_Ratio > 0.1f
                )
            ) &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    TestUnitHasBuff(this.Self, null, "Pyromania_particle") &&
                    (
                        (
                            AnnieCanCastAbility0() &&
                            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                            SetVarFloat(out this.CurrentClosestDistance, Range) &&
                            AnnieFindClosestVisibleTarget() &&
                            this.ValueChanged == true &&
                            SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                            AnnieCastAbility0()
                        ) ||
                        (
                            AnnieCanCastAbility1() &&
                            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                            SetVarFloat(out this.CurrentClosestDistance, Range) &&
                            AnnieFindClosestVisibleTarget() &&
                            this.ValueChanged == true &&
                            SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                            AnnieCastAbility1()
                        )
                    )
                ) ||
                (
                    GetUnitAIAttackers(out this.TargetCollection, this.Self) &&
                    this.TargetCollection.Any(Unit =>
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == HERO_UNIT
                    ) &&
                    AnnieCanCastAbility2() &&
                    SetVarFloat(out this.CurrentClosestDistance, 700) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 700, AffectEnemies | AffectHeroes) &&
                    AnnieFindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAISpellTarget(this.Self, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||
                AnnieMicroRetreat()
            )
        ;
    }

    bool AnnieCanCastAbility1()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 1) &&


                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost

             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        ;
    }

    bool AnnieCanCastAbility2()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 2) &&


                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost

             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        ;
    }

    bool AnnieCastAbility2()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
            SetVarFloat(out Range, 350) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool AnnieStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        TeamId MyTeam;
        TeamId UnitTeam;
        return

            SetVarFloat(out this.TotalUnitStrength, 1) &&
            ForEach(TargetCollection, Unit =>
                TestUnitIsVisible(this.Self, Unit) &&
                (
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == MINION_UNIT &&
                        MultiplyFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 20)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == HERO_UNIT &&
                        MultiplyFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 30)
                    ) ||
                    (
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == TURRET_UNIT &&
                        GetUnitTeam(out MyTeam, this.Self) &&
                        GetUnitTeam(out UnitTeam, Unit) &&
                        (
                            (
                                MyTeam == UnitTeam &&
                                MultiplyFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 220)
                            ) ||

                                MultiplyFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 90)

                        )
                    )
                )
            )
        ;
    }

    bool AnnieLowThreatManagement()
    {

        return

            (
                (
                    this.StrengthRatioOverTime > 5.5f &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out LowThreatMode, true)
                ) ||
                (
                    LowThreatMode == true &&
                    SetVarBool(out LowThreatMode, false) &&
                    this.StrengthRatioOverTime > 4.5f &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    !DebugAction("ForcedFailure")
                )
            ) &&
            AnnieMicroRetreat()
        ;
    }

    bool AnnieCanCastAbility0()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 0) &&


                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost

             &&
            DebugAction("TestCanCastSpell") &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        ;
    }

    bool AnnieCanCastAbility3()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 3) &&


                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost

             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        ;
    }

    bool AnnieCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    DebugAction("Distance <= Range") &&
                    SetUnitAISpellTarget(Target, 0) &&
                    DebugAction("SetUnitAISpellTarget SUCCESS") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("CastUnitSpell SUCCESS")
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool AnnieCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    SetUnitAISpellTarget(Target, 1) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool AnnieCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    SetUnitAISpellTarget(Target, 3) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool AnnieKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        float MyHealthRatio;
        return

            (
                (
                    this.StrengthRatioOverTime < 2 &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, unit =>
                        TestUnitIsVisible(this.Self, unit) &&
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                ) ||
                (
                    this.StrengthRatioOverTime < 3.5f &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.35f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, unit =>
                        TestUnitIsVisible(this.Self, unit) &&
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )
            ) &&
            (
                (
                    AnnieCanCastAbility3() &&
                    AnnieCastAbility3()
                ) ||
                (
                    AnnieCanCastAbility1() &&
                    AnnieCastAbility1()
                ) ||
                AnnieAutoAttackTarget()
            )
        ;
    }

    bool AnnieAutoAttackTarget()
    {
        AttackableUnit Target;
        float Distance;
        float AttackRange;
        return

            GetUnitAIAttackTarget(out Target) &&
            TestUnitAIAttackTargetValid() &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    GetUnitAttackRange(out AttackRange, this.Self) &&
                    MultiplyFloat(out AttackRange, AttackRange, 0.9f) &&
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder()
                ) ||
                IssueMoveToUnitOrder(Target)
            )
        ;
    }

    bool AnniePushLane()
    {
        Vector3 TaskPosition;
        return

        DebugAction("im annie et je push batard") &&
            ClearUnitAIAttackTarget() &&
            (
                (//todo:
                    TestUnitAIHasTask() &&
                    GetUnitAITaskPosition(out TaskPosition) &&
                    IssueMoveToPositionOrder(TaskPosition)
                ) ||
                IssueAIEnableTaskOrder()
            ) &&
            //hack 
            IssueMoveOrder()
        ;
    }

    bool AnnieInit()
    {
        bool LowThreatMode;
        float CurrentTime;
        float TimeDiff;
        float EnemyStrength;
        float FriendStrength;
        float StrRatio;
        AttackableUnit Unit;
        UnitType UnitType;
        float CurrentHealth;
        float NewDamage;
        return


            GetUnitAISelf(out this.Self) &&
            Addbuffhack(this.Self) &&
            GetUnitPosition(out this.SelfPosition, this.Self) &&
            SetVarFloat(out this.DeaggroDistance, 1200) &&
            (
                (
                    TestUnitAIFirstTime() &&
                    SetVarFloat(out this.AccumulatedDamage, 0) &&
                    GetUnitCurrentHealth(out this.PrevHealth, this.Self) &&
                    GetGameTime(out this.PrevTime) &&
                    SetVarBool(out this.LostAggro, false) &&
                    SetVarFloat(out this.StrengthRatioOverTime, 1) &&
                    SetVarBool(out LowThreatMode, false) &&
                    SetVarInt(out this.PotionsToBuy, 0) &&
                    SetVarBool(out this.TeleportHome, false)
                ) ||
                (
                    ((
                        GetGameTime(out CurrentTime) &&
                        SubtractFloat(out TimeDiff, CurrentTime, this.PrevTime) &&
                        (
                            TimeDiff > 1 ||
                            TimeDiff < 0
                        ) &&

                            MultiplyFloat(out this.AccumulatedDamage, this.AccumulatedDamage, 0.8f) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 0.8f)
                         &&

                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                            AnnieStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            AnnieStrengthEvaluator() &&
                            SetVarFloat(out FriendStrength, this.TotalUnitStrength) &&
                            DivideFloat(out StrRatio, EnemyStrength, FriendStrength) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, StrRatio) &&
                            GetUnitAIAttackers(out this.TargetCollection, this.Self) &&
                            (this.TargetCollection.Any(Unit =>
                                GetUnitType(out UnitType, Unit) &&
                                UnitType == TURRET_UNIT &&
                                MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 8)
                            ) || true)
                         &&
                        GetGameTime(out this.PrevTime)
                    ) || true) &&
                    ((
                        GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                        SubtractFloat(out NewDamage, this.PrevHealth, CurrentHealth) &&
                        NewDamage > 0 &&
                        MultiplyFloat(out this.AccumulatedDamage, this.AccumulatedDamage, NewDamage)
                    ) || true) &&
                    GetUnitCurrentHealth(out this.PrevHealth, this.Self)
                )
            )
        ;
    }

    bool AnnieDeaggroChecker()
    {
        float Distance;
        return
        (
            SetVarBool(out this.LostAggro, false) &&
            DistanceBetweenObjectAndPoint(out Distance, this.AggroTarget, this.AggroPosition) &&
            Distance > 800 &&
            Distance < 1200 &&
            SetVarBool(out this.LostAggro, true)
        ) || true;
    }

    bool AnnieMicroRetreat()
    {
        Vector3 SafePosition;
        float Distance;
        return

            (
                TestUnitAISafePositionValid() &&
                GetUnitAISafePosition(out SafePosition) &&
                (
                    (
                        DistanceBetweenObjectAndPoint(out Distance, this.Self, SafePosition) &&
                        Distance < 50 &&
                        ComputeUnitAISafePosition(800, false, false)
                    ) ||

                        IssueMoveToPositionOrder(SafePosition)

                )
            ) ||
            ComputeUnitAISafePosition(600, false, false)
        ;
    }

    bool AnnieHeal()
    {
        float Health;
        float MaxHealth;
        float HP_Ratio;
        return

            TestUnitAICanUseItem(2003) &&
            GetUnitCurrentHealth(out Health, this.Self) &&
            GetUnitMaxHealth(out MaxHealth, this.Self) &&
            DivideFloat(out HP_Ratio, Health, MaxHealth) &&
            HP_Ratio < 0.55f &&
            SetUnitAIItemTarget(this.Self, 2003) &&
            IssueUseItemOrder(2003, this.Self)
        ;
    }

    bool AnnieFindClosestVisibleTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return

            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection, Attacker =>
                TestUnitIsVisible(this.Self, Attacker) &&
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            )
        ;
    }
}