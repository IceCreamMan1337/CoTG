using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map4;

class Nasus : BehaviourTree
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
    bool IsHighThreat;
    Vector3 AssistPosition;
    int Count;
    bool IssuedAttackOrder;

    public Nasus()
    {

    }
    public Nasus(Champion owner) : base(owner)
    {

    }

    public override void Update()
    {

        base.Update();
        NasusBehavior();
    }
    bool NasusBehavior()
    {
        AttackableUnit Taunter;
        return

            NasusInit() &&
            (

                (
                    TestUnitHasBuff(this.Self, null, "Taunt") &&
                    GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                    SetUnitAIAttackTarget(Taunter) &&
                    NasusAutoAttackTarget()
                ) ||
                NasusAtBaseHealAndBuy() ||
                NasusLevelUp() ||
                NasusGameNotStarted() ||
                NasusHighThreatManagement() ||
                NasusReturnToBase() ||
                NasusKillChampion() ||
                NasusLowThreatManagement() ||
                NasusHeal() ||
                NasusAttack() ||
                NasusPushLane()
            )
        ;
    }

    bool NasusStrengthEvaluator()
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

    bool NasusFindClosestTarget()
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

    bool NasusDeaggroChecker()
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

    bool NasusInit()
    {
        bool IssuedAttackOrder;
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
                    SetVarInt(out this.PotionsToBuy, 0) &&
                    SetVarBool(out this.TeleportHome, false) &&
                    SetVarBool(out this.IsHighThreat, false) &&
                    SetVarBool(out IssuedAttackOrder, false)
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
                            NasusStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            NasusStrengthEvaluator() &&
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

    bool NasusAtBaseHealAndBuy()
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
            SetVarBool(out this.TeleportHome, false) &&
            (
                (
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f
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

            )
        ;
    }

    bool NasusLevelUp()
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
                    Ability1Level < 1 &&
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||

                    (
                        TestUnitCanLevelUpSpell(this.Self, 2) &&
                        Ability2Level <= Ability0Level &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(this.Self, 0) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                    )
                 ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                )
            )
        ;
    }

    bool NasusGameNotStarted()
    {

        return

            !TestGameStarted()
        ;
    }

    bool NasusAttack()
    {

        return

            NasusAcquireTarget() &&
            NasusAttackTarget()
        ;
    }

    bool NasusAcquireTarget()
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
                NasusDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    NasusFindClosestVisibleTarget() &&
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

    bool NasusAttackTarget()
    {
        AttackableUnit Target;
        TeamId SelfTeam;
        TeamId TargetTeam;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
        UnitType UnitType;
        Vector3 UnitPosition;
        IEnumerable<AttackableUnit> Enemies;

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
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio > 0.5f &&
                    (
                        (
                            GetUnitType(out UnitType, Target) &&
                            UnitType == MINION_UNIT &&
                            (
                                (
                                    this.StrengthRatioOverTime > 3.5f &&
                                    NasusCanCastAbility2() &&
                                    GetUnitPosition(out UnitPosition, Target) &&
                                    GetUnitsInTargetArea(out Enemies, this.Self, UnitPosition, 400, AffectEnemies | AffectHeroes | AffectMinions) &&
                                    Count >= 2 &&
                                    NasusCastAbility2()
                                ) ||
                                (
                                    GetUnitCurrentHealth(out currentHealth, Target) &&
                                    GetUnitMaxHealth(out MaxHealth, Target) &&
                                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                                    HP_Ratio < 0.35f &&

                                        NasusCanCastAbility0() &&
                                        SetUnitAISpellTarget(this.Self, 0) &&
                                        CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)

                                )
                            )
                        ) ||
                        (
                            GetUnitType(out UnitType, Target) &&
                            UnitType == HERO_UNIT &&

                                NasusCanCastAbility0() &&
                                SetUnitAISpellTarget(this.Self, 0) &&
                                CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)

                        )
                    )
                ) ||
                NasusAutoAttackTarget()
            )
        ;
    }

    bool NasusReturnToBase()
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
                    NasusFindClosestTarget() &&
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

    bool NasusHighThreatManagement()
    {
        bool SuperHighThreat;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        float Range;
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
                            Damage_Ratio > 0.2f
                        ) ||

                            (
                                this.IsHighThreat == true &&
                                Damage_Ratio > 0.02f
                            ) ||
                            (
                                this.IsHighThreat == false &&
                                this.AggressiveKillMode == false &&
                                Damage_Ratio > 0.15f
                            )

                    )
                )
            ) &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    NasusCanCastAbility1() &&
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
                    SetVarFloat(out this.CurrentClosestDistance, Range) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                    NasusFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetUnitAIAssistTarget(this.Self) &&
                    NasusCastAbility1()
                ) ||
                NasusMicroRetreat()
            )
        ;
    }

    bool NasusLowThreatManagement()
    {

        return

            (
                (
                    this.StrengthRatioOverTime > 7 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    this.LowThreatMode == true &&
                    SetVarBool(out this.LowThreatMode, false) &&
                    this.StrengthRatioOverTime > 5 &&
                    ClearUnitAIAttackTarget() &&
                    SetVarBool(out this.LowThreatMode, true)
                ) ||
                (
                    ClearUnitAISafePosition() &&
                    !DebugAction("ForcedFailure")
                )
            ) &&
            NasusMicroRetreat()
        ;
    }

    bool NasusKillChampion()
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
                    this.StrengthRatioOverTime < 6.5f &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.5f) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes) &&
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
                    Aggressive == true &&
                    NasusCanCastAbility1() &&
                    NasusCastAbility1()
                ) ||
                (
                    Aggressive == true &&
                    NasusCanCastAbility3() &&
                    SetUnitAISpellTarget(this.Self, 3) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    NasusCanCastAbility2() &&
                    NasusCastAbility2()
                ) ||
                (
                    NasusCanCastAbility0() &&
                    SetUnitAISpellTarget(this.Self, 0) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                NasusAutoAttackTarget()
            )
        ;
    }

    bool NasusMicroRetreat()
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

    bool NasusAutoAttackTarget()
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
                    (
                        (
                            IssuedAttackOrder == true &&
                            MultiplyFloat(out AttackRange, AttackRange, 1.5f)
                        ) ||
                        MultiplyFloat(out AttackRange, AttackRange, 0.7f)
                    ) &&
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder() &&
                    SetVarBool(out IssuedAttackOrder, true)
                ) ||
                (
                    SetVarBool(out IssuedAttackOrder, false) &&
                    IssueMoveToUnitOrder(Target)
                )
            )
        ;
    }

    bool NasusCanCastAbility0()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        ;
    }

    bool NasusCanCastAbility1()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        ;
    }

    bool NasusCanCastAbility2()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        ;
    }

    bool NasusCanCastAbility3()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        ;
    }

    bool NasusCastAbility1()
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

    bool NasusCastAbility2()
    {
        AttackableUnit Target;
        float Range;
        float Distance;
        return

            GetUnitAIAttackTarget(out Target) &&

                GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
                SetVarFloat(out Range, 120) &&
                (
                    (
                        GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                        Distance <= Range &&
                        SetUnitAISpellTarget(Target, 2) &&
                        CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                    ) ||

                        IssueMoveToUnitOrder(Target)

                )

        ;
    }

    bool NasusPushLane()
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
            )//hack
            &&
            IssueMoveOrder()
        ;
    }

    bool NasusHeal()
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

    bool NasusFindClosestVisibleTarget()
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