using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.all;

class Alistar : BehaviourTree
{
    float TotalUnitStrength;
    IEnumerable<AttackableUnit> TargetCollection;
    bool ValueChanged;
    Vector3  SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3  AggroPosition;
    AttackableUnit Self;
    float DeaggroDistance;
    float AccumulatedDamage;
    float PrevHealth;
    float PrevTime;
    float StrengthRatioOverTime;
    bool AggressiveKillMode;
    bool LowThreatMode;
    int PotionsToBuy;
    Vector3  AssistPosition;
    AttackableUnit PreviousTarget;
    float HP_Ratio;
    public Alistar() // Ajoutez un constructeur sans param�tres
    {
        // Initialisez les propri�t�s sp�cifiques � Annie ici, si n�cessaire.
    }
    public Alistar(Champion owner) : base(owner)
    {

    }

    public override void Update()
    {

        base.Update();
        AlisterBehavior();
    }
 
        bool AlisterBehavior()
    {

        return
        (
            Init() &&
            (
                AtBaseHealAndBuy() ||
                LevelUp() ||
                GameNotStarted() ||
                HighThreatManagement() ||
                ReturnToBase() ||
                KillChampion() ||
                LowThreatManagement() ||
                Heal() ||
                Attack() ||
                PushLane()
            )
        );
    }

    bool StrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        return
        (
            SetVarFloat(out this.TotalUnitStrength, 1) &&
            ForEach(TargetCollection, Unit => (
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
            ))
        );
    }

    bool FindClosestTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection,Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }

    bool DeaggroChecker()
    {
        float Distance;
        return
        ((
            SetVarBool(out this.LostAggro, false) &&
            DistanceBetweenObjectAndPoint(out Distance, this.AggroTarget, this.AggroPosition) &&
            Distance > 800 &&
            Distance < 1200 &&
            SetVarBool(out this.LostAggro, true)
        ) || true);
    }

    bool Init()
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
        (
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
                        (
                            MultiplyFloat(out this.AccumulatedDamage, this.AccumulatedDamage, 0.8f) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 0.8f)
                        ) &&
                        (
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                            StrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            StrengthEvaluator() &&
                            SetVarFloat(out FriendStrength, this.TotalUnitStrength) &&
                            DivideFloat(out StrRatio, EnemyStrength, FriendStrength) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, StrRatio) &&
                            GetUnitAIAttackers(out this.TargetCollection, this.Self) &&
                            (this.TargetCollection.Any(Unit => (
                                GetUnitType(out UnitType, Unit) &&
                                UnitType == TURRET_UNIT &&
                                MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 8)
                            )) || true)
                        ) &&
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
        );
    }

    bool AtBaseHealAndBuy()
    {
        Vector3  BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        return
        (
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
                    (
                        !TestChampionHasItem(this.Self, 3037) &&
                        !TestChampionHasItem(this.Self, 3032) &&
                        !TestChampionHasItem(this.Self, 3099) &&
                        TestUnitAICanBuyItem(3037) &&
                        UnitAIBuyItem(3037)
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
                        !TestChampionHasItem(this.Self, 1028) &&
                        !TestChampionHasItem(this.Self, 3105) &&
                        !TestChampionHasItem(this.Self, 3032) &&
                        !TestChampionHasItem(this.Self, 3010) &&
                        TestUnitAICanBuyItem(1028) &&
                        UnitAIBuyItem(1028)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 1027) &&
                        !TestChampionHasItem(this.Self, 3032) &&
                        !TestChampionHasItem(this.Self, 3010) &&
                        TestUnitAICanBuyItem(1027) &&
                        UnitAIBuyItem(1027)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 3032) &&
                        !TestChampionHasItem(this.Self, 3010) &&
                        TestUnitAICanBuyItem(3010) &&
                        UnitAIBuyItem(3010)
                    ) ||
                    (
                        !TestChampionHasItem(this.Self, 3032) &&
                        TestUnitAICanBuyItem(3032) &&
                        UnitAIBuyItem(3032)
                    ) ||
                    (
                        TestUnitAICanBuyRecommendedItem() &&
                        UnitAIBuyRecommendedItem()
                    ) ||
                    (
                        this.PotionsToBuy > 0 &&
                        !TestChampionHasItem(this.Self, 2004) &&
                        TestUnitAICanBuyItem(2004) &&
                        UnitAIBuyItem(2004) &&
                        SubtractInt(out this.PotionsToBuy, this.PotionsToBuy, 1)
                    )
                )
            ) &&
            DebugAction("++++ At Base Heal & Buy +++")
        );
    }

    bool LevelUp()
    {
        int SkillPoints;
        int SpellLevel;
        return
        (
            GetUnitSkillPoints(out SkillPoints, this.Self) &&
            SkillPoints > 0 &&
            (
                (
                    TestUnitCanLevelUpSpell(this.Self, 3) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                    DebugAction("levelup 3")
                ) ||
                (
                    GetUnitSpellLevel(out SpellLevel, this.Self, SPELLBOOK_CHAMPION, 0) &&
                    SpellLevel <= 0 &&
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 2) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    GetUnitSpellLevel(out SpellLevel, this.Self, SPELLBOOK_CHAMPION, 1) &&
                    SpellLevel <= 0 &&
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0) &&
                    DebugAction("levelup 0")
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 1) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    DebugAction("levelup 0")
                )
            ) &&
            DebugAction("++++ Level up ++++")
        );
    }

    bool GameNotStarted()
    {

        return
        (
            !TestGameStarted() &&
            DebugAction("+++ Game Not Started +++")
        );
    }

    bool Attack()
    {

        return
        (
            AcquireTarget() &&
            AttackTarget() &&
            DebugAction("++++ Attack ++++")
        );
    }

    bool AcquireTarget()
    {
        IEnumerable<AttackableUnit> FriendlyUnits;
        AttackableUnit unit;
        int Count;
        float Distance;
        return
        (
            (
                SetVarBool(out this.LostAggro, false) &&
                TestUnitAIAttackTargetValid() &&
                GetUnitAIAttackTarget(out this.AggroTarget) &&
                SetVarVector(out this.AggroPosition, this.AssistPosition) &&
                DeaggroChecker() &&
                this.LostAggro == false &&
                DebugAction("+++ Use Previous Target +++")
            ) ||
            (
                DebugAction("EnableOrDisableAllyAggro") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits,unit => (
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    FindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )) &&
                this.ValueChanged == true &&
                DebugAction("+++ Acquired Ally under attack +++")
            ) ||
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,unit => (
                        DistanceBetweenObjectAndPoint(out Distance, unit, this.SelfPosition) &&
                        Distance < this.CurrentClosestDistance &&
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
                    ))
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }

    bool AttackTarget()
    {
        AttackableUnit Target;
        TeamId  SelfTeam;
        TeamId  TargetTeam;
        UnitType UnitType;
        float currentHealth;
        float MaxHealth;
        float HP_Ratio;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
        return
        (
            GetUnitAIAttackTarget(out Target) &&
            GetUnitTeam(out SelfTeam, this.Self) &&
            GetUnitTeam(out TargetTeam, Target) &&
            SelfTeam != TargetTeam &&
            (
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == MINION_UNIT &&
                    this.StrengthRatioOverTime > 5 &&
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.3f &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    CanCastAbility0() &&
                    CastAbility0()
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.6f &&
                    (
                        (
                            CanCastAbility0() &&
                            CastAbility0()
                        )
                    )
                ) ||
                AutoAttackTarget()
            ) &&
            DebugAction("++ Attack Success ++")
        );
    }

    bool ReturnToBase()
    {
        Vector3  BaseLocation;
        float Distance;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        Vector3  TeleportPosition;
        float DistanceToTeleportPosition;
        return
        (
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
                    FindClosestTarget() &&
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
        );
    }

    bool HighThreatManagement()
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
                (
                    SetVarBool(out SuperHighThreat, false) &&
                    TestUnitUnderAttack(this.Self) &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out Health, this.Self) &&
                    DivideFloat(out Health_Ratio, Health, MaxHealth) &&
                    Health_Ratio <= 0.25f &&
                    DebugAction("+++ LowHealthUnderAttack +++") &&
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
                    ) &&
                    DebugAction("+++ BurstDamage +++")
                )
            ) &&
            DebugAction("+++ High Threat +++") &&
            ClearUnitAIAttackTarget() &&
            (
                (
                    SuperHighThreat == true &&
                    CanCastAbility3() &&
                    SetUnitAIAttackTarget(this.Self) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    CanCastAbility0() &&
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out this.CurrentClosestDistance, Range) &&
                    FindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                MicroRetreat()
            ) &&
            DebugAction("+++ High Threat +++")
        );
    }

    bool LowThreatManagement()
    {

        return
        (
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
                    !DebugAction("DoNotRemoveForcedFail")
                )
            ) &&
            MicroRetreat() &&
            DebugAction("++++ Low Threat +++")
        );
    }

    bool KillChampion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        bool Aggressive;
        float MyHealthRatio;
        AttackableUnit Target;
        float Distance;
        Vector3  SpellPosition;
        float DistanceToSpellPosition;
        return
        (
            SetVarBool(out this.AggressiveKillMode, false) &&
            (
                (
                    this.StrengthRatioOverTime < 2 &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, false) &&
                    DebugAction("PassiveKillChampion")
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
                    ForEach(TargetCollection,unit => (
                        GetUnitCurrentHealth(out CurrentHealth, unit) &&
                        GetUnitMaxHealth(out MaxHealth, unit) &&
                        DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                        HP_Ratio < CurrentLowestHealthRatio &&
                        SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                        SetVarAttackableUnit(out this.CurrentClosestTarget, unit) &&
                        SetVarBool(out this.ValueChanged, true)
                    )) &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                    SetVarBool(out Aggressive, true) &&
                    SetVarBool(out this.AggressiveKillMode, true) &&
                    DebugAction("+++ AggressiveMode +++")
                )
            ) &&
            (
                (
                    CanCastAbility0() &&
                    CastAbility0() &&
                    DebugAction("+++ Use Q+++")
                ) ||
                (
                    CanCastAbility1() &&
                    GetUnitAIAttackTarget(out Target) &&
                    (
                        SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                        GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends | AffectHeroes | AffectTurrets) &&
                        FindClosestTarget() &&
                        this.ValueChanged == true &&
                        (
                            (
                                GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                                Distance < 100 &&
                                (
                                    (
                                        TestUnitAISpellPositionValid() &&
                                        GetUnitAISpellPosition(out SpellPosition) &&
                                        DistanceBetweenObjectAndPoint(out DistanceToSpellPosition, this.Self, SpellPosition) &&
                                        DistanceToSpellPosition < 50
                                    ) ||
                                    !TestUnitAISpellPositionValid()
                                ) &&
                                CastAbility1() &&
                                ClearUnitAISpellPosition() &&
                                DebugAction("Yo")
                            ) ||
                            (
                                (
                                    (
                                        !TestUnitAISpellPositionValid() &&
                                        ComputeUnitAISpellPosition(Target, this.CurrentClosestTarget, 150, false)
                                    ) ||
                                    TestUnitAISpellPositionValid()
                                ) &&
                                GetUnitAISpellPosition(out SpellPosition) &&
                                IssueMoveToPositionOrder(SpellPosition) &&
                                DebugAction("Yo")
                            )
                        )
                    )
                ) ||
                AutoAttackTarget() ||
                DebugAction("+++ Attack Champion+++")
            ) &&
            DebugAction("++++ Success: Kill  +++")
        );
    }

    bool LastHitMinion()
    {
        float CurrentLowestHealthRatio;
        AttackableUnit unit;
        float CurrentHealth;
        float MaxHealth;
        float HP_Ratio;
        AttackableUnit Target;
        return
        (
            (
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 800, AffectEnemies | AffectMinions) &&
                SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(TargetCollection,unit => (
                    GetUnitCurrentHealth(out CurrentHealth, unit) &&
                    GetUnitMaxHealth(out MaxHealth, unit) &&
                    DivideFloat(out HP_Ratio, CurrentHealth, MaxHealth) &&
                    HP_Ratio < CurrentLowestHealthRatio &&
                    SetVarBool(out this.ValueChanged, true) &&
                    SetVarFloat(out CurrentLowestHealthRatio, HP_Ratio) &&
                    SetVarAttackableUnit(out this.CurrentClosestTarget, unit)
                )) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                SetVarAttackableUnit(out Target, this.CurrentClosestTarget)
            ) &&
            AutoAttackTarget() &&
            DebugAction("+++++++ Last Hit ++++++++")
        );
    }

    bool MicroRetreat()
    {
        Vector3  SafePosition;
        float Distance;
        return
        (
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
        );
    }

    bool AutoAttackTarget()
    {
        AttackableUnit Target;
        float Distance;
        float AttackRange;
        return
        (
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
        );
    }

    bool CanCastAbility0()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 0) &&
            (
                (
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost
                )
            ) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 0)
        );
    }

    bool CanCastAbility1()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 1) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 1) &&
            (
                (
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost
                )
            ) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 1)
        );
    }

    bool CanCastAbility2()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 2) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 2) &&
            (
                (
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost
                )
            ) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 2)
        );
    }

    bool CanCastAbility3()
    {
        float Cooldown;
        PrimaryAbilityResourceType PAR_Type;
        float Cost;
        float CurrentPAR;
        return
        (
            GetSpellSlotCooldown(out Cooldown, this.Self, SPELLBOOK_CHAMPION, 0) &&
            Cooldown <= 0 &&
            GetUnitPARType(out PAR_Type, this.Self) &&
            GetUnitSpellCost(out Cost, this.Self, SPELLBOOK_CHAMPION, 3) &&
            (
                (
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PAR_Type) &&
                    CurrentPAR >= Cost
                )
            ) &&
            TestCanCastSpell(this.Self, SPELLBOOK_CHAMPION, 3)
        );
    }

    bool CastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            DebugAction("CastSubTree") &&
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitSpellRadius(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
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
        );
    }

    bool CastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
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
        );
    }

    bool CastAbility2()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
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
        );
    }

    bool CastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
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
        );
    }

    bool PushLane()
    {

        return
        (
            ClearUnitAIAttackTarget() &&
            IssueMoveOrder() &&
            DebugAction("+++ Move To Lane +++")
        );
    }

    bool Misc()
    {
        TeamId  SelfTeam;
        TeamId  UnitTeam;
        AttackableUnit Assist;
        float Distance;
        Vector3  AssistPosition;
        int Count;
        AttackableUnit Attacker;
        return
        (
            (
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
            ) &&
            (
                DebugAction("??? EnableDisableAcquire New Target ???") &&
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies | AffectHeroes | AffectMinions | AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,Attacker => (
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
                    ))
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition) &&
                DebugAction("+++ AcquiredNewTarget +++")
            )
        );
    }

    bool Heal()
    {
        float MaxHealth;
        float Health;

        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;
        return
        (
            (
                GetUnitMaxHealth(out MaxHealth, this.Self) &&
                GetUnitCurrentHealth(out Health, this.Self) &&
                DivideFloat(out HP_Ratio, Health, MaxHealth) &&
                HP_Ratio < 0.8f &&
                CanCastAbility2() &&
                CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
            ) ||
            (
                GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                HP_Ratio < 0.5f &&
                TestUnitAICanUseItem(2004) &&
                IssueUseItemOrder(2004, this.Self)
            )
        );
    }
}