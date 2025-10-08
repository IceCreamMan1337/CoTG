using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map1;

class Soraka : BehaviourTree
{
    
    AttackableUnit Self;
    int PotionsToBuy;
    bool ValueChanged;
    IEnumerable<AttackableUnit> TargetCollection;
    Vector3  SelfPosition;
    float CurrentClosestDistance;
    AttackableUnit CurrentClosestTarget;
    bool LostAggro;
    AttackableUnit AggroTarget;
    Vector3  AggroPosition;
    Vector3  AssistPosition;
    bool TeleportHome;
    float AccumulatedDamage;
    float TotalUnitStrength;
    float StrengthRatioOverTime;
    float DeaggroDistance;
    float PrevHealth;
    float PrevTime;
    AttackableUnit PreviousTarget;
    float CurrentLowestHealth;
    AttackableUnit CurrentLowestHealthTarget;
    bool LowThreatMode;
    AttackableUnit CurrentLowestPARTarget;

    public Soraka()
    {

    }
    public Soraka(Champion owner) : base(owner)
    {
       
    }
    public override void Update()
    {

        base.Update();
        SorakaBehavior();
    }

    bool SorakaBehavior()
    {
        AttackableUnit Taunter;
        return
        (
            SorakaInit() &&
            (
    
                    (
                        TestUnitHasBuff(this.Self, null, "Taunt") &&
                        GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                        SetUnitAIAttackTarget(Taunter) &&
                        SorakaAutoAttackTarget()
                    ) || 
                SorakaAtBaseHealAndBuy() ||
                SorakaLevelUp() ||
                SorakaGameNotStarted() ||
                SorakaHighThreatManagement() ||
                SorakaReturnToBase() ||
                SorakaHeal() ||
                SorakaKillChampion() ||
                SorakaLowThreatManagement() ||
                SorakaAttack() ||
                SorakaPushLane()
            )
        );
    }
    
    bool SorakaAtBaseHealAndBuy()
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
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out Health_Ratio, CurrentHealth, MaxHealth) &&
                    Health_Ratio < 0.95f
                ) ||
                (
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
                )
            )
        );
    }
    
    bool SorakaLevelUp()
    {
        int SkillPoints;
        int Ability0Level;
        int Ability1Level;
        int Ability2Level;
        return
        (
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
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    (
                        (
                            Ability1Level >= 1 &&
                            Ability2Level >= 1 &&
                            Ability0Level <= 0
                        ) ||
                        (
                            Ability1Level >= 3 &&
                            Ability2Level >= 3 &&
                            Ability0Level <= 1
                        )
                    ) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    (
                        TestUnitCanLevelUpSpell(this.Self, 2) &&
                        Ability2Level <= Ability1Level &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(this.Self, 1) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                    )
                ) ||
                (
                    TestUnitCanLevelUpSpell(this.Self, 0) &&
                    LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                )
            )
        );
    }
    
    bool SorakaGameNotStarted()
    {
        
        return
        (
            !TestGameStarted()
        );
    }
    
    bool SorakaFindClosestTarget()
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
    
    bool SorakaAttack()
    {
        
        return
        (
            SorakaAcquireTarget() &&
            SorakaAttackTarget()
        );
    }
    
    bool SorakaAcquireTarget()
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
                TestUnitIsVisible(this.Self, this.AggroTarget) &&
                SetVarVector(out this.AggroPosition, this.AssistPosition) &&
                SorakaDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends|AffectHeroes|AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits ,unit => (
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    SorakaFindClosestVisibleTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAIAssistTarget(this.Self) &&
                    SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                    unit == this.Self &&
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )) &&
                this.ValueChanged == true
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectBuildings|AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                (
                    GetCollectionCount(out Count, this.TargetCollection) &&
                    Count > 0 &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,unit => (
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
                    ))
                ) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentClosestTarget) &&
                SetVarVector(out this.AssistPosition, this.SelfPosition)
            )
        );
    }
    
    bool SorakaAttackTarget()
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
                    GetUnitCurrentHealth(out currentHealth, Target) &&
                    GetUnitMaxHealth(out MaxHealth, Target) &&
                    DivideFloat(out HP_Ratio, currentHealth, MaxHealth) &&
                    HP_Ratio < 0.2f &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    SorakaCanCastAbility0() &&
                    SorakaCastAbility0()
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
                            SorakaCanCastAbility2() &&
                            SorakaCastAbility2()
                        ) ||
                        (
                            SorakaCanCastAbility0() &&
                            SorakaCastAbility0()
                        )
                    )
                ) ||
                SorakaAutoAttackTarget()
            )
        );
    }
    
    bool SorakaReturnToBase()
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
            ) &&
            (
                (
                    SetVarFloat(out this.CurrentClosestDistance, 30000) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends|AffectTurrets) &&
                    SorakaFindClosestTarget() &&
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
        );
    }
    
    bool SorakaHighThreatManagement()
    {
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        float Range;
        return
        (
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
                    SorakaCanCastAbility2() &&
                    GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, Range, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out this.CurrentClosestDistance, Range) &&
                    SorakaFindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAISpellTarget(this.CurrentClosestTarget, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||
                (
                    SorakaCanCastAbility1() &&
                    SetUnitAISpellTarget(this.Self, 1) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1) &&
                    ClearUnitAISpellTarget(1)
                ) ||
                SorakaMicroRetreat()
            )
        );
    }
    
    bool SorakaCanCastAbility1()
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
    
    bool SorakaCanCastAbility2()
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
    
    bool SorakaStrengthEvaluator()
    {
        AttackableUnit Unit;
        UnitType UnitType;
        return
        (
            SetVarFloat(out this.TotalUnitStrength, 1) &&
            ForEach(TargetCollection,Unit => (
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
            ))
        );
    }
    
    bool SorakaLowThreatManagement()
    {

        return
        (
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
            SorakaMicroRetreat()
        );
    }
    
    bool SorakaCanCastAbility0()
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
    
    bool SorakaCanCastAbility3()
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
    
    bool SorakaCastAbility3()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 3) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    IssueMoveToUnitOrder(Target)
                )
            )
        );
    }
    
    bool SorakaCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            GetUnitSpellRadius(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            MultiplyFloat(out Range, Range, 0.8f) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    SetUnitAISpellTarget(this.Self, 0) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 0)
                ) ||
                (
                    IssueMoveToUnitOrder(Target)
                )
            )
        );
    }
    
    bool SorakaCastAbility2()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 2) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    SetUnitAISpellTarget(Target, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||
                (
                    IssueMoveToUnitOrder(Target)
                )
            )
        );
    }
    
    bool SorakaCastAbility1()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return
        (
            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 1) &&
            GetUnitAIAttackTarget(out Target) &&
            (
                (
                    GetDistanceBetweenUnits(out Distance, Target, this.Self) &&
                    Distance <= Range &&
                    SetUnitAISpellTarget(Target, 1) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                ) ||
                (
                    IssueMoveToUnitOrder(Target)
                )
            )
        );
    }
    
    bool SorakaKillChampion()
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
                (
                    this.StrengthRatioOverTime < 2 &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.8f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,unit => (
                        TestUnitIsVisible(this.Self, unit) &&
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
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                ) ||
                (
                    this.StrengthRatioOverTime < 4 &&
                    GetUnitMaxHealth(out MaxHealth, this.Self) &&
                    GetUnitCurrentHealth(out CurrentHealth, this.Self) &&
                    DivideFloat(out MyHealthRatio, CurrentHealth, MaxHealth) &&
                    MyHealthRatio > 0.5f &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies|AffectHeroes) &&
                    SetVarFloat(out CurrentLowestHealthRatio, 0.3f) &&
                    SetVarBool(out this.ValueChanged, false) &&
                    ForEach(TargetCollection,unit => (
                        TestUnitIsVisible(this.Self, unit) &&
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
                    SetVarVector(out this.AssistPosition, this.SelfPosition)
                )
            ) &&
            (
                (
                    SorakaCanCastAbility2() &&
                    SorakaCastAbility2()
                ) ||
                (
                    SorakaCanCastAbility0() &&
                    SorakaCastAbility0()
                ) ||
                SorakaAutoAttackTarget()
            )
        );
    }
    
    bool SorakaLastHitMinion()
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
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 800, AffectEnemies|AffectMinions) &&
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
            SorakaAutoAttackTarget()
        );
    }
    
    bool SorakaAutoAttackTarget()
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
    
    bool SorakaPushLane()
    {
        Vector3  TaskPosition;
        return
        (
        DebugAction("im soraka et je push batard") &&
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
        );
    }
    
    bool SorakaInit()
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
                    SetVarBool(out LowThreatMode, false) &&
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
                        (
                            MultiplyFloat(out this.AccumulatedDamage, this.AccumulatedDamage, 0.8f) &&
                            MultiplyFloat(out this.StrengthRatioOverTime, this.StrengthRatioOverTime, 0.8f)
                        ) &&
                        (
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
                            SorakaStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectFriends|AffectHeroes|AffectMinions|AffectTurrets) &&
                            SorakaStrengthEvaluator() &&
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
    
    bool SorakaMisc()
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
            ) &&
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectEnemies|AffectHeroes|AffectMinions|AffectTurrets) &&
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
                SetVarVector(out this.AssistPosition, this.SelfPosition)
            )
        );
    }
    
    bool SorakaDeaggroChecker()
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
    
    bool SorakaMicroRetreat()
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
                        ComputeUnitAISafePosition(800, false, false)
                    ) ||
                    (
                        IssueMoveToPositionOrder(SafePosition)
                    )
                )
            ) ||
            ComputeUnitAISafePosition(600, false, false)
        );
    }
    
    bool SorakaHeal()
    {
        float MP_Ratio;
        AttackableUnit Unit;
        PrimaryAbilityResourceType PARType;
        float CurrentPAR;
        float MaxPAR;
        float PAR_Ratio;

        float Health;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            (
                SorakaCanCastAbility3() &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 30000, AffectFriends|AffectHeroes) &&
                SetVarFloat(out this.CurrentLowestHealth, 0.18f) &&
                SorakaFindLowestHealthTarget() &&
                this.ValueChanged == true &&
                SetUnitAISpellTarget(this.Self, 3) &&
                CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3) &&
                IssueAIChatOrder("Using my ultimate to heal all my allies!", "/all")
            ) ||
            (
                SorakaCanCastAbility1() &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectFriends|AffectHeroes) &&
                SetVarFloat(out this.CurrentLowestHealth, 0.75f) &&
                SorakaFindLowestHealthTarget() &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(this.CurrentLowestHealthTarget) &&
                SorakaCastAbility1()
            ) ||
            (
                SorakaCanCastAbility2() &&
                SetVarFloat(out MP_Ratio, 0.75f) &&
                GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 1000, AffectFriends|AffectHeroes) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(TargetCollection,Unit => (
                    GetUnitPARType(out PARType, Unit) &&
                    PARType == PrimaryAbilityResourceType.MANA &&
                    GetUnitCurrentPAR(out CurrentPAR, Unit, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, Unit, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio < MP_Ratio &&
                    SetVarFloat(out MP_Ratio, PAR_Ratio) &&
                    SetVarAttackableUnit(out CurrentLowestPARTarget, Unit) &&
                    SetVarBool(out this.ValueChanged, true)
                )) &&
                this.ValueChanged == true &&
                SetUnitAIAssistTarget(this.Self) &&
                SetUnitAIAttackTarget(CurrentLowestPARTarget) &&
                SorakaCastAbility2()
            ) ||
            (
                TestUnitAICanUseItem(2003) &&
                GetUnitCurrentHealth(out Health, this.Self) &&
                GetUnitMaxHealth(out MaxHealth, this.Self) &&
                DivideFloat(out HP_Ratio, Health, MaxHealth) &&
                HP_Ratio < 0.55f &&
                SetUnitAIItemTarget(this.Self, 2003) &&
                IssueUseItemOrder(2003, this.Self)
            )
        );
    }
    
    bool SorakaUseUltimate()
    {
        
        return
        (
            SorakaCanCastAbility3()
        );
    }
    
    bool SorakaFindLowestHealthTarget()
    {
        AttackableUnit Unit;
        float UnitHealth;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection,Unit => (
                GetUnitCurrentHealth(out UnitHealth, Unit) &&
                GetUnitMaxHealth(out MaxHealth, Unit) &&
                DivideFloat(out HP_Ratio, UnitHealth, MaxHealth) &&
                HP_Ratio < this.CurrentLowestHealth &&
                SetVarFloat(out this.CurrentLowestHealth, HP_Ratio) &&
                SetVarAttackableUnit(out this.CurrentLowestHealthTarget, Unit) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
    
    bool SorakaFindLowestMPTarget()
    {
        AttackableUnit Unit;
        float UnitHealth;
        float MaxHealth;
        float HP_Ratio;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection,Unit => (
                GetUnitCurrentHealth(out UnitHealth, Unit) &&
                GetUnitMaxHealth(out MaxHealth, Unit) &&
                DivideFloat(out HP_Ratio, UnitHealth, MaxHealth) &&
                HP_Ratio < this.CurrentLowestHealth &&
                SetVarFloat(out this.CurrentLowestHealth, HP_Ratio) &&
                SetVarAttackableUnit(out this.CurrentLowestHealthTarget, Unit) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
    
    bool SorakaFindClosestVisibleTarget()
    {
        AttackableUnit Attacker;
        float Distance;
        return
        (
            SetVarBool(out this.ValueChanged, false) &&
            ForEach(TargetCollection,Attacker => (
                DistanceBetweenObjectAndPoint(out Distance, Attacker, this.SelfPosition) &&
                Distance < this.CurrentClosestDistance &&
                TestUnitIsVisible(this.Self, Attacker) &&
                SetVarFloat(out this.CurrentClosestDistance, Distance) &&
                SetVarAttackableUnit(out this.CurrentClosestTarget, Attacker) &&
                SetVarBool(out this.ValueChanged, true)
            ))
        );
    }
}