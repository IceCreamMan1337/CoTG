using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map6;

class Shen : BehaviourTree
{
    AttackableUnit Self;
    float TotalUnitStrength;
    IEnumerable<AttackableUnit> TargetCollection;
    bool ValueChanged;
    Vector3 SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3 AggroPosition;
    float DeaggroDistance;
    float AccumulatedDamage;
    float PrevHealth;
    float PrevTime;
    float StrengthRatioOverTime;
    bool AggressiveKillMode;
    bool LowThreatMode;
    int PotionsToBuy;
    bool TeleportHome;
    Vector3 AssistPosition;
    float CurrentLowestHealth;
    AttackableUnit CurrentLowestHealthTarget;
    float Damage_Ratio;

    public Shen()
    {

    }
    public Shen(Champion owner) : base(owner)
    {

    }
    public override void Update()
    {

        base.Update();
        ShenBehavior();
    }
    bool ShenBehavior()
    {
        AttackableUnit Taunter;
        return

            ShenInit() &&
            (

                (
                    TestUnitHasBuff(this.Self, null, "Taunt") &&
                    GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                    SetUnitAIAttackTarget(Taunter) &&
                    ShenAutoAttackTarget()
                ) ||
                ShenAtBaseHealAndBuy() ||
                ShenLevelUp() ||
                ShenGameNotStarted() ||
                SaveAnAlly() ||
                ShenReduceDamageTaken() ||
                ShenHighThreatManagement() ||
                ShenReturnToBase() ||
                ShenKillChampion() ||
                ShenLowThreatManagement() ||
                ShenHeal() ||
                ShenAttack() ||
                ShenPushLane()
            )
        ;
    }

    bool ShenStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        return

            SetVarFloat(out this.TotalUnitStrength, 1) &&
            ForEach(TargetCollection, Unit =>
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
        ;
    }

    bool ShenFindClosestTarget()
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

    bool ShenDeaggroChecker()
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

    bool ShenInit()
    {
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
                    SetVarBool(out this.AggressiveKillMode, false) &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    SetVarInt(out this.PotionsToBuy, 4) &&
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
                            ShenStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            ShenStrengthEvaluator() &&
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

    bool ShenAtBaseHealAndBuy()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        float temp;
        return

            GetUnitAIBasePosition(out BaseLocation, this.Self) &&
            DistanceBetweenObjectAndPoint(out Distance, this.Self, BaseLocation) &&
            Distance <= 450 &&
            SetVarBool(out this.TeleportHome, false) &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f
                ) ||

                    (
                        !TestChampionHasItem(this.Self, 1054) &&
                        TestUnitAICanBuyItem(1054) &&
                        UnitAIBuyItem(1054)
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
                        !TestChampionHasItem(this.Self, 3105) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1028) &&
                                TestUnitAICanBuyItem(1028) &&
                                UnitAIBuyItem(1028)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3105) &&
                                TestUnitAICanBuyItem(3105) &&
                                UnitAIBuyItem(3105)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3009) &&
                        TestUnitAICanBuyItem(3009) &&
                        UnitAIBuyItem(3009)
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        !TestChampionHasItem(this.Self, 3068) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1011) &&
                                TestUnitAICanBuyItem(1011) &&
                                UnitAIBuyItem(1011)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3068) &&
                                TestUnitAICanBuyItem(3068) &&
                                UnitAIBuyItem(3068)
                            )
                        )
                    ) ||
                    (
                        GetUnitGold(out temp, this.Self) &&
                        temp > 0 &&
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3068) &&
                        !TestChampionHasItem(this.Self, 3026) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1029) &&
                                TestUnitAICanBuyItem(1029) &&
                                UnitAIBuyItem(1029)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1033) &&
                                TestUnitAICanBuyItem(1033) &&
                                UnitAIBuyItem(1033)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1031) &&
                                TestUnitAICanBuyItem(1031) &&
                                UnitAIBuyItem(1031)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3026) &&
                                TestUnitAICanBuyItem(3026) &&
                                UnitAIBuyItem(3026)
                            )
                        )
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 3105) &&
                        TestChampionHasItem(this.Self, 3009) &&
                        TestChampionHasItem(this.Self, 3068) &&
                        TestChampionHasItem(this.Self, 3026) &&
                        !TestChampionHasItem(this.Self, 3109) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 1057) &&
                                TestUnitAICanBuyItem(1057) &&
                                UnitAIBuyItem(1057)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1007) &&
                                TestUnitAICanBuyItem(1007) &&
                                UnitAIBuyItem(1007)
                            ) ||
                            (
                                TestUnitAICanBuyItem(3109) &&
                                UnitAIBuyItem(3109)
                            )
                        )
                    ) ||
                    (
                        this.PotionsToBuy > 0 &&
                        !TestChampionHasItem(this.Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )

            )
        ;
    }

    bool ShenLevelUp()
    {
        int SkillPoints;
        int Ability0Level;
        int Ability1Level;
        int Ability2Level;
        return

            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            SkillPoints > 0 &&
            GetUnitSpellLevel(out Ability0Level, this.Self, SPELLBOOK_UNKNOWN, 0) &&
            GetUnitSpellLevel(out Ability1Level, this.Self, SPELLBOOK_UNKNOWN, 1) &&
            GetUnitSpellLevel(out Ability2Level, this.Self, SPELLBOOK_UNKNOWN, 2) &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    Ability0Level < 2 &&
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    Ability0Level >= 2 &&
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 2) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                )
            )
        ;
    }

    bool ShenGameNotStarted()
    {

        return

            !TestGameStarted()
        ;
    }

    bool ShenAttack()
    {

        return

            ShenAcquireTarget() &&
            ShenAttackTarget()
        ;
    }

    bool ShenAcquireTarget()
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
                TestUnitIsVisible(this.Self, this.AggroTarget) &&
                SetVarVector(out this.AggroPosition, this.AssistPosition) &&
                ShenDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    ShenFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                ) &&
                this.ValueChanged == true
            ) ||
            (
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
                SetVarVector(out this.AssistPosition, this.SelfPosition)
            )
        ;
    }

    bool ShenAttackTarget()
    {
        AttackableUnit Target;
        TeamId SelfTeam;
        TeamId TargetTeam;
        PrimaryAbilityResourceType PAR_Type;
        float CurrentPAR;
        float Max_PAR;
        float PAR_Ratio;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        return

            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, this.Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitPARType(out PAR_Type, this.Self) &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    GetUnitMaxPAR(out Max_PAR, this.Self, PAR_Type) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, Max_PAR) &&
                    PAR_Ratio > 0.95f &&
                    (
                        (
                            GetUnitType(out UnitType, Target) &&
                            UnitType == MINION_UNIT &&
                            GetUnitCurrentHealth(out currentHealth, Target) &&
                            GetUnitMaxHealth(out MaxHealth, Target) &&
                            DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                            HP_Ratio < 0.2f &&

                                ShenCanCastAbility0() &&
                                ShenCastAbility0()

                        ) ||
                        (
                            GetUnitType(out UnitType, Target) &&
                            UnitType == HERO_UNIT &&

                                ShenCanCastAbility0() &&
                                ShenCastAbility0()

                        )
                    )
                ) ||
                ShenAutoAttackTarget()
            )
        ;
    }

    bool ShenReturnToBase()
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
                    ShenFindClosestTarget() &&
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

    bool ShenHighThreatManagement()
    {
        bool SuperHighThreat;
        float MaxHealth;
        float Health;
        float Health_Ratio;

        Vector3 TauntPosition;
        return

            (
                (
                    SetVarBool(out SuperHighThreat, false) &&
                    TestUnitUnderAttack(this.Self) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f &&
                    SetVarBool(out SuperHighThreat, true)
                ) ||
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
                    (
                        (
                            this.AggressiveKillMode == true &&
                            Damage_Ratio > 0.15f
                        ) ||
                        (
                            this.AggressiveKillMode == false &&
                            Damage_Ratio > 0.02f
                        )
                    )
                )
            ) &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    SuperHighThreat == true &&
                    ShenCanCastAbility1() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    Damage_Ratio > 0.15f &&
                    ShenCanCastAbility2() &&
                    SetVarFloat(out this.CurrentClosestDistance, 2000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 2000, AffectEnemies | AffectHeroes) &&
                    ShenFindClosestTarget() &&
                    this.ValueChanged == true &&
                    ComputeUnitAISpellPosition(this.Self, this.CurrentClosestTarget, 450, false) &&
                    GetUnitAISpellPosition(out TauntPosition) &&
                    SetUnitAISpellTargetLocation(TauntPosition, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                    ClearUnitAISpellPosition()
                ) ||
                ShenMicroRetreat()
            )
        ;
    }

    bool ShenLowThreatManagement()
    {

        return

            (
                (
                    this.StrengthRatioOverTime > 8 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    this.LowThreatMode == true &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    this.StrengthRatioOverTime > 6 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    !DebugAction("ForcedFailure")
                )
            ) &&
            ShenMicroRetreat()
        ;
    }

    bool ShenKillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        bool Aggressive;
        float MyHealthRatio;
        return

            SetVarBool(out this.AggressiveKillMode, false) &&
            (
                (
                    this.StrengthRatioOverTime < 3 &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, unit =>
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, false)
                ) ||
                (
                    this.StrengthRatioOverTime < 5.1f &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.4f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection, unit =>
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        TestUnitIsVisible(this.Self, unit) &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    ) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, true) &&
                    SetVarBool(out this.AggressiveKillMode, true)
                )
            ) &&
            (
                (
                    ShenCanCastAbility1() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    ShenCanCastAbility2() &&
                    ShenCastAbility2()
                ) ||
                (
                    ShenCanCastAbility0() &&
                    ShenCastAbility0()
                ) ||
                ShenAutoAttackTarget()
            )
        ;
    }

    bool ShenMicroRetreat()
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

    bool ShenAutoAttackTarget()
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
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder()
                ) ||
                (
                    ClearUnitAIAttackTarget() &&
                    IssueMoveToUnitOrder(Target)
                )
            )
        ;
    }

    bool ShenCanCastAbility0()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float CurrentPAR;
        float PAR_Cost;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&

                GetUnitPARType(out PAR_Type, this.Self) &&
                GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                GetUnitSpellCost(out PAR_Cost, this.Self, SPELLBOOK_CHAMPION, 0) &&
                PAR_Cost <= CurrentPAR
             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        ;
    }

    bool ShenCanCastAbility1()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float CurrentPAR;
        float PAR_Cost;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&

                GetUnitPARType(out PAR_Type, this.Self) &&
                GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                GetUnitSpellCost(out PAR_Cost, this.Self, SPELLBOOK_CHAMPION, 1) &&
                PAR_Cost <= CurrentPAR
             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        ;
    }

    bool ShenCanCastAbility2()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float CurrentPAR;
        float PAR_Cost;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&

                GetUnitPARType(out PAR_Type, this.Self) &&
                GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                GetUnitSpellCost(out PAR_Cost, this.Self, SPELLBOOK_CHAMPION, 2) &&
                PAR_Cost <= CurrentPAR
             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        ;
    }

    bool ShenCanCastAbility3()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float CurrentPAR;
        float PAR_Cost;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&

                GetUnitPARType(out PAR_Type, this.Self) &&
                GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                GetUnitSpellCost(out PAR_Cost, this.Self, SPELLBOOK_CHAMPION, 3) &&
                PAR_Cost <= CurrentPAR
             &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        ;
    }

    bool ShenCastAbility0()
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
                    SetUnitAISpellTarget(Target, 0) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool ShenCastAbility1()
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
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool ShenCastAbility2()
    {
        AttackableUnit Target;
        float Range;
        float Distance;
        Vector3 TauntPosition;
        return

            GetUnitAIAttackTarget(out Target) &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
            SetVarFloat(out Range, 400) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    ComputeUnitAISpellPosition(Target, this.Self, Range, false) &&
                    GetUnitAISpellPosition(out TauntPosition) &&
                    SetUnitAISpellTargetLocation(TauntPosition, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                    ClearUnitAISpellPosition()
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool ShenCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            SetVarFloat(out Range, 30000) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||

                    IssueMoveToUnitOrder(Target)

            )
        ;
    }

    bool ShenPushLane()
    {
        Vector3 TaskPosition;
        return

            ClearUnitAIAttackTarget() &&
            (
                (
                    TestUnitAIHasTask() &&
                    GetUnitAITaskPosition(out TaskPosition) &&
                    IssueMoveToPositionOrder(TaskPosition)
                ) ||
                IssueAIEnableTaskOrder()
            )
            //hack
            &&
            IssueMoveOrder()
        ;
    }

    bool ShenHeal()
    {
        float Health;
        float MaxHealth;
        float HP_Ratio;
        return

            GetUnitCurrentHealth(out Health, this.Self) &&
            GetUnitMaxHealth(out MaxHealth, this.Self) &&
            DivideFloat(out HP_Ratio, Health, MaxHealth) &&

                HP_Ratio < 0.5f &&
                TestUnitAICanUseItem(2003) &&
                IssueUseItemOrder(2003, this.Self)

        ;
    }

    bool ShenReduceDamageTaken()
    {
        float MaxHealth;
        float Damage_Ratio;
        return

            TestUnitUnderAttack(this.Self) &&
            GetUnitMaxHealth(out MaxHealth, this.Self) &&
            DivideFloat(out Damage_Ratio, this.AccumulatedDamage, MaxHealth) &&
            Damage_Ratio >= 0.1f &&

                ShenCanCastAbility1() &&
                SetUnitAISpellTarget(this.Self, 1) &&
                CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)

        ;
    }

    bool SaveAnAlly()
    {
        Vector3 UnitPosition;
        return

            (
                ShenCanCastAbility3() &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends | AffectHeroes | NotAffectSelf) &&
                SetVarFloat(out this.CurrentLowestHealth, 0.2f) &&
                ShenFindLowestHealthTarget() &&
                this.ValueChanged == true &&
                SetUnitAIAttackTarget(this.CurrentLowestHealthTarget) &&
                SetUnitAISpellTarget(this.CurrentLowestHealthTarget, 3) &&
                SetUnitSpellIgnoreVisibity(this.Self, SPELLBOOK_CHAMPION, 3, true) &&
                CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                ClearUnitAIAssistTarget() &&
                ClearUnitAIAttackTarget() &&
                IssueAIDisableTaskOrder()
            ) ||
            (
                ShenCanCastAbility2() &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 2000, AffectFriends | AffectHeroes | NotAffectSelf) &&
                SetVarFloat(out this.CurrentLowestHealth, 0.24f) &&
                ShenFindLowestHealthTarget() &&
                this.ValueChanged == true &&
                GetUnitPosition(out UnitPosition, this.CurrentLowestHealthTarget) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.CurrentLowestHealthTarget, UnitPosition, 2000, AffectEnemies | AffectHeroes) &&
                SetVarFloat(out this.CurrentClosestDistance, 2000) &&
                ShenFindClosestVisibleTarget() &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                ShenCastAbility2()
            )
        ;
    }

    bool ShenFindLowestHealthTarget()
    {
        AttackableUnit Unit;
        float UnitHealth;
        float MaxHealth;
        float HP_Ratio;
        return

            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection, Unit =>
                GetUnitCurrentHealth(out UnitHealth, Unit) &&
                GetUnitMaxHealth(out MaxHealth, Unit) &&
                DivideFloat(out HP_Ratio, UnitHealth, MaxHealth) &&
                HP_Ratio < this.CurrentLowestHealth &&
                SetVarFloat(out this.CurrentLowestHealth, HP_Ratio) &&
                SetVarAttackableUnit(out this.CurrentLowestHealthTarget, Unit) &&
                SetVarBool(out this.ValueChanged, true)
            )
        ;
    }

    bool ShenFindClosestVisibleTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return

            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection, Attacker =>
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                TestUnitIsVisible(this.Self, Attacker) &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            )
        ;
    }
}