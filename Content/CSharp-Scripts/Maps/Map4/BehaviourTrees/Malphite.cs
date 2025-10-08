using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map4;

class Malphite : BehaviourTree
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
    bool IssuedAttackOrder;
    Vector3 AssistPosition;
    AttackableUnit PreviousTarget;

    public Malphite()
    {

    }
    public Malphite(Champion owner) : base(owner)
    {

    }
    public override void Update()
    {

        base.Update();
        MalphiteBehavior();
    }
    bool MalphiteBehavior()
    {
        AttackableUnit Taunter;
        return

            MalphiteInit() &&
            (

                (
                    TestUnitHasBuff(this.Self, null, "Taunt") &&
                    GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                    SetUnitAIAttackTarget(Taunter) &&
                    MalphiteAutoAttackTarget()
                ) ||
                MalphiteAtBaseHealAndBuy() ||
                MalphiteLevelUp() ||
                MalphiteGameNotStarted() ||
                MalphiteHighThreatManagement() ||
                MalphiteReturnToBase() ||
                MalphiteKillChampion() ||
                MalphiteLowThreatManagement() ||
                MalphiteHeal() ||
                MalphiteAttack() ||
                MalphitePushLane()
            )
        ;
    }

    bool MalphiteStrengthEvaluator()
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

    bool MalphiteFindClosestTarget()
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

    bool MalphiteDeaggroChecker()
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

    bool MalphiteInit()
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
                    SetVarInt(out this.PotionsToBuy, 0) &&
                    SetVarBool(out this.TeleportHome, false) &&
                    SetVarBool(out this.IssuedAttackOrder, false)
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
                            MalphiteStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            MalphiteStrengthEvaluator() &&
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

    bool MalphiteAtBaseHealAndBuy()
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
                        !TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3111) &&
                        TestUnitAICanBuyItem(1001) &&
                        UnitAIBuyItem(1001)
                    ) ||
                    (
                        TestChampionHasItem(this.Self, 1001) &&
                        !TestChampionHasItem(this.Self, 3078) &&
                        (
                            (
                                !TestChampionHasItem(this.Self, 3057) &&
                                (
                                    (
                                        !TestChampionHasItem(this.Self, 1027) &&
                                        TestUnitAICanBuyItem(1027) &&
                                        UnitAIBuyItem(1027)
                                    ) ||
                                    (
                                        !TestChampionHasItem(this.Self, 1052) &&
                                        TestUnitAICanBuyItem(1052) &&
                                        UnitAIBuyItem(1052)
                                    ) ||
                                    (
                                        TestUnitAICanBuyItem(3057) &&
                                        UnitAIBuyItem(3057)
                                    )
                                )
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3044) &&
                                (
                                    (
                                        !TestChampionHasItem(this.Self, 1027) &&
                                        TestUnitAICanBuyItem(1027) &&
                                        UnitAIBuyItem(1027)
                                    ) ||
                                    (
                                        !TestChampionHasItem(this.Self, 1028) &&
                                        TestUnitAICanBuyItem(1028) &&
                                        UnitAIBuyItem(1028)
                                    ) ||
                                    (
                                        TestUnitAICanBuyItem(3044) &&
                                        UnitAIBuyItem(3044)
                                    )
                                )
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 3086) &&
                                (
                                    (
                                        !TestChampionHasItem(this.Self, 1042) &&
                                        TestUnitAICanBuyItem(1042) &&
                                        UnitAIBuyItem(1042)
                                    ) ||
                                    (
                                        !TestChampionHasItem(this.Self, 1051) &&
                                        TestUnitAICanBuyItem(1051) &&
                                        UnitAIBuyItem(1051)
                                    ) ||
                                    (
                                        TestUnitAICanBuyItem(3086) &&
                                        UnitAIBuyItem(3086)
                                    )
                                )
                            ) ||
                            (
                                TestUnitAICanBuyItem(3078) &&
                                UnitAIBuyItem(3078)
                            )
                        )
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 3096) &&
                        (
                            (
                                TestUnitAICanBuyItem(3096) &&
                                UnitAIBuyItem(3096)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1007) &&
                                TestUnitAICanBuyItem(1007) &&
                                UnitAIBuyItem(1007)
                            ) ||
                            (
                                !TestChampionHasItem(this.Self, 1005) &&
                                TestUnitAICanBuyItem(1005) &&
                                UnitAIBuyItem(1005)
                            )
                        )
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 3009) &&
                        TestUnitAICanBuyItem(3009) &&
                        UnitAIBuyItem(3009)
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
                        this.PotionsToBuy > 0 &&
                        !TestChampionHasItem(this.Self, 2003) &&
                        TestUnitAICanBuyItem(2003) &&
                        UnitAIBuyItem(2003) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )

            )
        ;
    }

    bool MalphiteLevelUp()
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
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    (
                        (
                            Ability0Level >= 1 &&
                            Ability2Level >= 1 &&
                            Ability1Level <= 0
                        ) ||
                        (
                            Ability0Level >= 4 &&
                            Ability2Level >= 4 &&
                            Ability1Level <= 1
                        )
                    ) &&
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

    bool MalphiteGameNotStarted()
    {

        return

            !TestGameStarted()
        ;
    }

    bool MalphiteAttack()
    {

        return

            MalphiteAcquireTarget() &&
            MalphiteAttackTarget()
        ;
    }

    bool MalphiteAcquireTarget()
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
                MalphiteDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    MalphiteFindClosestVisibleTarget() &&
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

    bool MalphiteAttackTarget()
    {
        AttackableUnit Target;
        TeamId SelfTeam;
        TeamId TargetTeam;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        IEnumerable<AttackableUnit> Enemies;
        int Count;
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
                            this.StrengthRatioOverTime > 3.5f &&
                            GetUnitType(out UnitType, Target) &&
                            UnitType == MINION_UNIT &&
                            GetUnitCurrentHealth(out currentHealth, Target) &&
                            GetUnitMaxHealth(out MaxHealth, Target) &&
                            DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                            HP_Ratio < 0.35f &&

                                MalphiteCanCastAbility2() &&
                                GetUnitsInTargetArea(out Enemies, this.Self, this.SelfPosition, 200, AffectEnemies | AffectHeroes | AffectMinions) &&
                                GetCollectionCount(out Count, Enemies) &&
                                Count >= 2 &&
                                SetUnitAIAttackTarget(this.Self) &&
                                MalphiteCastAbility2()

                        ) ||
                        (
                            GetUnitType(out UnitType, Target) &&
                            UnitType == HERO_UNIT &&
                            (
                                (
                                    MalphiteCanCastAbility0() &&
                                    MalphiteCastAbility0()
                                ) ||
                                (
                                    MalphiteCanCastAbility2() &&
                                    MalphiteCastAbility2()
                                )
                            )
                        )
                    )
                ) ||
                MalphiteAutoAttackTarget()
            )
        ;
    }

    bool MalphiteReturnToBase()
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
                    MalphiteFindClosestTarget() &&
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

    bool MalphiteHighThreatManagement()
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
                    MalphiteCanCastAbility0() &&
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
                    SetVarFloat(out this.CurrentClosestDistance, Range) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                    MalphiteFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetUnitAIAssistTarget(this.Self) &&
                    MalphiteCastAbility0()
                ) ||
                MalphiteMicroRetreat()
            )
        ;
    }

    bool MalphiteLowThreatManagement()
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
            MalphiteMicroRetreat()
        ;
    }

    bool MalphiteKillChampion()
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
                    this.StrengthRatioOverTime < 5 &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
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
                    MalphiteCanCastAbility3() &&
                    MalphiteCastAbility3()
                ) ||
                (
                    MalphiteCanCastAbility0() &&
                    MalphiteCastAbility0()
                ) ||
                (
                    MalphiteCanCastAbility2() &&
                    MalphiteCastAbility2()
                ) ||
                MalphiteAutoAttackTarget()
            )
        ;
    }

    bool MalphiteLastHitMinion()
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
            MalphiteAutoAttackTarget()
        ;
    }

    bool MalphiteMicroRetreat()
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

    bool MalphiteAutoAttackTarget()
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
                            this.IssuedAttackOrder == true &&
                            MultiplyFloat(out AttackRange, AttackRange, 1.5f)
                        ) ||
                        MultiplyFloat(out AttackRange, AttackRange, 0.6f)
                    ) &&
                    Distance <= AttackRange &&
                    ClearUnitAIAttackTarget() &&
                    SetUnitAIAttackTarget(Target) &&
                    IssueAttackOrder() &&
                    SetVarBool(out this.IssuedAttackOrder, true)
                ) ||
                (
                    SetVarBool(out this.IssuedAttackOrder, false) &&
                    IssueMoveToUnitOrder(Target)
                )
            )
        ;
    }

    bool MalphiteCanCastAbility0()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        ;
    }

    bool MalphiteCanCastAbility1()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        ;
    }

    bool MalphiteCanCastAbility2()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        ;
    }

    bool MalphiteCanCastAbility3()
    {
        float Cooldown;
        return

            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        ;
    }

    bool MalphiteCastAbility0()
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

    bool MalphiteCastAbility1()
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

    bool MalphiteCastAbility2()
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
                        SetUnitAISpellTarget(this.Self, 2) &&
                        CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                    ) ||

                        IssueMoveToUnitOrder(Target)

                )

        ;
    }

    bool MalphiteCastAbility3()
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

    bool MalphitePushLane()
    {
        Vector3 TaskPosition;
        return

            ClearUnitAIAttackTarget() &&
            (
                (//todo:
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

    bool MalphiteMisc()
    {
        TeamId SelfTeam;
        TeamId UnitTeam;
        AttackableUnit Assist;
        float Distance;
        Vector3 AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return


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
                            SetVarBool(out this.LostAggro, true)
                        ) || true) &&
                        Distance < this.DeaggroDistance
                    ) ||
                    (
                        this.Self != Assist &&
                        GetUnitPosition(out AssistPosition, Assist) &&
                        DistanceBetweenObjectAndPoint(out Distance, this.PreviousTarget, this.SelfPosition) &&
                        ((
                            Distance >= 1000 &&
                            ClearUnitAIAttackTarget() &&
                            SetVarBool(out this.LostAggro, true)
                        ) || true) &&
                        Distance < 1000
                    )
                ) &&
                SetVarBool(out this.LostAggro, false)
             &&

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
                SetVarVector(out this.AssistPosition, this.SelfPosition)

        ;
    }

    bool MalphiteHeal()
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

    bool MalphiteFindClosestVisibleTarget()
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