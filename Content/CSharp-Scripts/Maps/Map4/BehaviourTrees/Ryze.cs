using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map4;

class Ryze : BehaviourTree
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
    float AccumulatedDamage;
    float TotalUnitStrength;
    float StrengthRatioOverTime;
    float DeaggroDistance;
    float PrevHealth;
    float PrevTime;
    AttackableUnit PreviousTarget;
    bool LowThreatMode;

    public Ryze()
    {

    }
    public Ryze(Champion owner) : base(owner)
    {

    }


    public override void Update()
    {

        base.Update();
        RyzeBehavior();
    }
    bool RyzeBehavior()
    {

        return

            RyzeInit() &&
            (
                RyzeAtBaseHealAndBuy() ||
                RyzeLevelUp() ||
                RyzeGameNotStarted() ||
                RyzeHighThreatManagement() ||
                RyzeReturnToBase() ||
                RyzeHeal() ||
                RyzeKillChampion() ||
                RyzeLowThreatManagement() ||
                RyzeAttack() ||
                RyzePushLane()
            )
        ;
    }

    bool RyzeAtBaseHealAndBuy()
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
                    DebugAction("Start ----- Heal -----") &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f &&
                    DebugAction("Success ----- Heal -----")
                ) ||

                    (
                        !TestChampionHasItem(this.Self, 1056) &&
                        TestUnitAICanBuyItem(1056) &&
                        UnitAIBuyItem(1056)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3009) &&
                        !TestChampionHasItem(this.Self, 3117) &&
                        !TestChampionHasItem(this.Self, 3020) &&
                        !TestChampionHasItem(this.Self, 3006) &&
                        TestUnitAICanBuyItem(3111) &&
                        UnitAIBuyItem(1001)
                    ) ||
                    (
                        TestUnitAICanBuyRecommendedItem() &&
                        UnitAIBuyRecommendedItem()
                    ) ||
                    (
                        this.PotionsToBuy > 0 &&
                        !TestChampionHasItem(this.Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )

            ) &&
            DebugAction("++++ At Base Heal & Buy +++")
        ;
    }

    bool RyzeLevelUp()
    {
        int SkillPoints;
        return

            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            SkillPoints > 0 &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("levelup 3")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 2) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                    DebugAction("levelup 2")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 1")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("levelup 0")
                )
            ) &&
            DebugAction("++++ Level up ++++")
        ;
    }

    bool RyzeGameNotStarted()
    {

        return

            !TestGameStarted() &&
            DebugAction("+++ Game Not Started +++")
        ;
    }

    bool RyzeFindClosestTarget()
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

    bool RyzeAttack()
    {

        return

            RyzeAcquireTarget() &&
            RyzeAttackTarget() &&
            DebugAction("++++ Attack ++++")
        ;
    }

    bool RyzeAcquireTarget()
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
                RyzeDeaggroChecker() &&
                this.LostAggro == false &&
                DebugAction("+++ Use Previous Target +++")
            ) ||
            (
                DebugAction("EnableOrDisableAllyAggro") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    RyzeFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                ) &&
                this.ValueChanged == true &&
                DebugAction("+++ Acquired Ally under attack +++")
            ) ||
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectBuildings | AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&

                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, unit =>
                        DistanceBetweenObjectAndPoint(out Distance, unit, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
                        TestUnitIsVisible(this.Self, unit) &&
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
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        ;
    }

    bool RyzeAttackTarget()
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

            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, this.Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == MINION_UNIT &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.2f &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    RyzeCanCastAbility2() &&
                    RyzeCastAbility2()
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    (
                        (
                            RyzeCanCastAbility1() &&
                            RyzeCastAbility1()
                        ) ||
                        (
                            RyzeCanCastAbility2() &&
                            RyzeCastAbility2()
                        ) ||
                        (
                            RyzeCanCastAbility0() &&
                            RyzeCastAbility0()
                        )
                    )
                ) ||
                RyzeAutoAttackTarget()
            ) &&
            DebugAction("++ Attack Success ++")
        ;
    }

    bool RyzeReturnToBase()
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
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f
                ) ||
                (
                    !DebugAction("EmptyNode: HighGold")
                )
            ) &&
            (
                (
                    SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends | AffectTurrets) &&
                    RyzeFindClosestTarget() &&
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
                            IssueTeleportToBaseOrder() &&
                            ClearUnitAISpellPosition() &&
                            DebugAction("Yo")
                        ) ||
                        (
                            (
                                (
                                    !TestUnitAISpellPositionValid() &&
                                    ComputeUnitAISpellPosition(this.CurrentClosestTarget, this.Self, 150, false)
                                ) ||
                                TestUnitAISpellPositionValid()
                            ) &&
                            GetUnitAISpellPosition(out TeleportPosition) &&
                            IssueMoveToPositionOrder(TeleportPosition) &&
                            DebugAction("Yo")
                        )
                    )
                ) ||
                (
                    GetUnitAIBasePosition(out BaseLocation, this.Self) &&
                    IssueMoveToPositionOrder(BaseLocation)
                )
            ) &&
            DebugAction("+++ Teleport Home +++")
        ;
    }

    bool RyzeHighThreatManagement()
    {
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        float Range;
        return

            (
                (
                    TestUnitUnderAttack(this.Self) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f &&
                    DebugAction("+++ LowHealthUnderAttack +++")
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
                    Damage_Ratio > 0.1f &&
                    DebugAction("+++ BurstDamage +++")
                )
            ) &&
            DebugAction("+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    RyzeCanCastAbility1() &&
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out this.CurrentClosestDistance, Range) &&
                    RyzeFindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                RyzeMicroRetreat()
            ) &&
            DebugAction("+++ High Threat +++")
        ;
    }

    bool RyzeCanCastAbility1()
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

    bool RyzeCanCastAbility2()
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

    bool RyzeCastAbility2()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 2") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                    DebugAction("Ability 2 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        ;
    }

    bool RyzeStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
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
                        MultiplyFloat(out this.TotalUnitStrength, this.TotalUnitStrength, 90)
                    )
                )
            )
        ;
    }

    bool RyzeLowThreatManagement()
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
                    !DebugAction("DoNotRemoveForcedFail")
                )
            ) &&
            RyzeMicroRetreat() &&
            DebugAction("++++ Low Threat +++")
        ;
    }

    bool RyzeCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        ;
    }

    bool RyzeCanCastAbility0()
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
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        ;
    }

    bool RyzeCanCastAbility3()
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

    bool RyzeCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        ;
    }

    bool RyzeCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    DebugAction("Pareparing to cast ability 1") &&
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    DebugAction("GoingToRangeCheck") &&
                    Distance <= Range &&
                    DebugAction("Range Check Succses") &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("Ability 1 Success ----------------")
                ) ||
                (
                    DebugAction("MoveIntoRangeSequence------------------") &&
                    IssueMoveToUnitOrder(Target) &&
                    DebugAction("Moving To Cast")
                )
            )
        ;
    }

    bool RyzeKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        float MyHealthRatio;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
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
                    this.StrengthRatioOverTime < 4 &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
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
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    DebugAction("+++ Low HP Enemy Champion +++")
                )
            ) &&
            (
                (
                    RyzeCanCastAbility3() &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio > 0.4f &&
                    (
                        RyzeCanCastAbility0() ||
                        RyzeCanCastAbility1() ||
                        RyzeCanCastAbility2()
                    ) &&
                    SetUnitAISpellTarget(this.Self, 3) &&
                    RyzeCastAbility3() &&
                    DebugAction("+++ Use Ultiamte +++")
                ) ||
                (
                    RyzeCanCastAbility1() &&
                    RyzeCastAbility1() &&
                    DebugAction("+++ Use Ability 1 +++")
                ) ||
                (
                    RyzeCanCastAbility2() &&
                    RyzeCastAbility2() &&
                    DebugAction("+++ Use SpellFlux +++")
                ) ||
                (
                    RyzeCanCastAbility0() &&
                    RyzeCastAbility0() &&
                    DebugAction("+++ Use Q+++")
                ) ||
                RyzeAutoAttackTarget() ||
                DebugAction("+++ Attack Champion+++")
            ) &&
            DebugAction("++++ Success: Kill  +++")
        ;
    }

    bool RyzeLastHitMinion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        AttackableUnit Target;
        return


                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 800, AffectEnemies | AffectMinions) &&
                SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(TargetCollection, unit =>
                    GetUnitCurrentHealth(out CurrentHealth, unit) &&
                    GetUnitMaxHealth(out MaxHealth, unit) &&
                    DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                    HP_Ratio < CurrentLowestHealthRatio &&
                    SetVarBool(out this.ValueChanged, true) &&
                    SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                    SetVarAttackableUnit(out this.CurrentClosestTarget, unit)
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                SetVarAttackableUnit(out Target, this.CurrentClosestTarget)
             &&
            RyzeAutoAttackTarget() &&
            DebugAction("+++++++ Last Hit ++++++++")
        ;
    }

    bool RyzeAutoAttackTarget()
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

    bool RyzePushLane()
    {

        return

            ClearUnitAIAttackTarget() &&
            IssueMoveOrder() &&
            DebugAction("+++ Move To Lane +++")
        ;
    }

    bool RyzeInit()
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
                    SetVarInt(out this.PotionsToBuy, 4)
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
                            RyzeStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            RyzeStrengthEvaluator() &&
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

    bool RyzeMisc()
    {
        TeamId SelfTeam;
        TeamId UnitTeam;
        AttackableUnit Assist;
        float Distance;
        Vector3 AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return


                !DebugAction("??? EnableOrDisablePreviousTarget ???") &&
                TestUnitAIAttackTargetValid() &&
                SetVarBool(out this.LostAggro, false) &&
                GetUnitAIAttackTarget(out this.PreviousTarget) &&
                GetUnitTeam(out SelfTeam, this.Self) &&
                GetUnitTeam(out UnitTeam, this.PreviousTarget) &&
                UnitTeam != SelfTeam &&
                GetUnitAIAssistTarget(out Assist) &&
                (
                    (
                        Assist == this.Self &&
                        DistanceBetweenObjectAndPoint(out Distance, this.Self, this.AssistPosition) &&
                        ((
                            Distance >= this.DeaggroDistance &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            DebugAction("+++ Lost Aggro +++")
                        ) || true) &&
                        Distance < this.DeaggroDistance &&
                        DebugAction("+++ In Aggro Range, Use Previous")
                    ) ||
                    (
                        this.Self != Assist &&
                        GetUnitPosition(out AssistPosition, Assist) &&
                        DistanceBetweenObjectAndPoint(out Distance, this.PreviousTarget, this.SelfPosition) &&
                        ((
                            Distance >= 1000 &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true) &&
                            DebugAction("------- Losing aggro from assist ----------")
                        ) || true) &&
                        Distance < 1000 &&
                        DebugAction("============= Use Previous Target: Still close to assist -----------")
                    )
                ) &&
                SetVarBool(out this.LostAggro, false) &&
                DebugAction("++ Use Previous Target ++")
             &&

                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&

                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, Attacker =>
                        (
                            (
                                this.LostAggro == true &&
                                Attacker != this.PreviousTarget
                            ) ||
                            this.LostAggro == false
                        ) &&
                        DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
                        SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                        SetVarBool(out this.ValueChanged, true)
                    )
                 &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")

        ;
    }

    bool RyzeDeaggroChecker()
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

    bool RyzeMicroRetreat()
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
                        ComputeUnitAISafePosition(800, false, false) &&
                        DebugAction("------- At location computed new position --------------")
                    ) ||
                    (
                        IssueMoveToPositionOrder(SafePosition) &&
                        DebugAction("------------ Success: Move to safe position ----------")
                    )
                )
            ) ||
            ComputeUnitAISafePosition(600, false, false)
        ;
    }

    bool RyzeHeal()
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

    bool RyzeFindClosestVisibleTarget()
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