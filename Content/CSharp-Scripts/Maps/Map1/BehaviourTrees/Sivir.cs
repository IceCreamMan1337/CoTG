using static CoTGEnumNetwork.Enums.SpellDataFlags;
using static CoTGEnumNetwork.Enums.SpellbookType;
using static CoTGEnumNetwork.Enums.UnitType;

namespace BehaviourTrees.Map1;

class Sivir : BehaviourTree
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

    public Sivir()
    {

    }
    public Sivir(Champion owner) : base(owner)
    {

    }

    public override void Update()
    {

        base.Update();
        SivirBehavior();
    }

    bool SivirBehavior()

    {
        AttackableUnit Taunter;
        return

            SivirInit() &&
            (

                (
                    TestUnitHasBuff(this.Self, null, "Taunt") &&
                    GetUnitBuffCaster(out Taunter, this.Self, "Taunt") &&
                    SetUnitAIAttackTarget(Taunter) &&
                    SivirAutoAttackTarget()
                ) ||



                SivirAtBaseHealAndBuy() ||



                SivirLevelUp() ||
                SivirGameNotStarted() ||
                SivirHighThreatManagement() ||
                SivirReturnToBase() ||
                SivirHeal() ||
                SivirKillChampion() ||
                SivirLowThreatManagement() ||
                SivirAttack() ||
                SivirPushLane()
            )
        ;
    }

    bool SivirStrengthEvaluator()
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

    bool SivirFindClosestTarget()
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

    bool SivirDeaggroChecker()
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

    bool SivirInit()
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
                            SivirStrengthEvaluator() &&
                            SetVarFloat(out EnemyStrength, this.TotalUnitStrength) &&
                            GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 900, AffectFriends | AffectHeroes | AffectMinions | AffectTurrets) &&
                            SivirStrengthEvaluator() &&
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

    bool SivirAtBaseHealAndBuy()
    {
        Vector3 BaseLocation;
        float Distance;
        float MaxHealth;
        float CurrentHealth;
        float Health_Ratio;
        return

        /*  GetUnitAIBasePosition(out BaseLocation, this.Self) &&
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
                      !TestChampionHasItem(this.Self, 1053) &&
                      !TestChampionHasItem(this.Self, 3050) &&
                      !TestChampionHasItem(this.Self, 3072) &&
                      UnitAIBuyItem(1053)
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
          )*/

        //annie shop hack

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

    bool SivirLevelUp()
    {
        int SkillPoints;
        int Ability2Level;
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
                        TestUnitCanLevelUpSpell(this.Self, 2) &&
                        (
                            (
                                GetUnitSpellLevel(out Ability2Level, this.Self, SPELLBOOK_CHAMPION, 2) &&
                                Ability2Level <= 0
                            ) ||
                            !TestUnitCanLevelUpSpell(this.Self, 1)
                        ) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                    ) ||
                    (
                        TestUnitCanLevelUpSpell(this.Self, 1) &&
                        LevelUpUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                    )

            )
        ;
    }

    bool SivirGameNotStarted()
    {

        return

            !TestGameStarted()
        ;
    }

    bool SivirAttack()
    {

        return

            SivirAcquireTarget() &&
            SivirAttackTarget()
        ;
    }

    bool SivirAcquireTarget()
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
                SivirDeaggroChecker() &&
                this.LostAggro == false
            ) ||
            (
                SetVarFloat(out this.CurrentClosestDistance, 800) &&
                GetUnitsInTargetArea(out FriendlyUnits, this.Self, this.SelfPosition, 800, AffectFriends | AffectHeroes | AlwaysSelf) &&
                SetVarBool(out this.ValueChanged, false) &&
                ForEach(FriendlyUnits, unit =>
                    TestUnitUnderAttack(unit) &&
                    GetUnitAIAttackers(out this.TargetCollection, unit) &&
                    SivirFindClosestVisibleTarget() &&
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

    bool SivirAttackTarget()
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
        bool UseRichochet;
        return

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
                    HP_Ratio < 0.5f &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.5f &&
                    SivirCanCastAbility0() &&
                    SivirCastAbility0()
                ) ||
                (
                    GetUnitType(out UnitType, Target) &&
                    UnitType == HERO_UNIT &&
                    GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                    DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                    PAR_Ratio >= 0.6f &&


                            SivirCanCastAbility0() &&
                            SivirCastAbility0()


                ) ||
                (
                    (
                        (
                            SetVarBool(out UseRichochet, false) &&
                            GetUnitType(out UnitType, Target) &&
                            UnitType == MINION_UNIT &&
                            this.StrengthRatioOverTime > 3 &&
                            GetUnitCurrentPAR(out CurrentPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                            GetUnitMaxPAR(out MaxPAR, this.Self, PrimaryAbilityResourceType.MANA) &&
                            DivideFloat(out PAR_Ratio, CurrentPAR, MaxPAR) &&
                            PAR_Ratio >= 0.3f &&
                            SetVarBool(out UseRichochet, true) &&
                            !TestUnitSpellToggledOn(this.Self, 1) &&
                            SivirCanCastAbility1() &&
                            SetUnitAISpellTarget(this.Self, 1) &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                        ) ||
                        (
                            UseRichochet == false &&
                            TestUnitSpellToggledOn(this.Self, 1) &&
                            SivirCanCastAbility1() &&
                            SetUnitAISpellTarget(this.Self, 1) &&
                            CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 1)
                        )
                     || true) &&
                    SivirAutoAttackTarget()
                )
            )
        ;
    }

    bool SivirReturnToBase()
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
                    SivirFindClosestTarget() &&
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

    bool SivirHighThreatManagement()
    {
        bool SuperHighThreat;
        float MaxHealth;
        float Health;
        float Health_Ratio;
        float Damage_Ratio;
        AttackableUnit Unit;
        UnitType UnitType;
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
                    GetUnitAIAttackers(out this.TargetCollection, this.Self) &&
                    this.TargetCollection.Any(Unit =>
                        GetUnitType(out UnitType, Unit) &&
                        UnitType == HERO_UNIT
                    ) &&
                    SivirCanCastAbility2() &&
                    SetVarFloat(out this.CurrentClosestDistance, 700) &&
                    GetUnitsInTargetArea(out this.TargetCollection, this.Self, this.SelfPosition, 700, AffectEnemies | AffectHeroes) &&
                    SivirFindClosestTarget() &&
                    this.ValueChanged == true &&
                    SetUnitAISpellTarget(this.Self, 2) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 2)
                ) ||
                (
                    SuperHighThreat == true &&
                    SivirCanCastAbility3() &&
                    SetUnitAISpellTarget(this.Self, 3) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                SivirMicroRetreat()
            )
        ;
    }

    bool SivirLowThreatManagement()
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
            SivirMicroRetreat()
        ;
    }

    bool SivirKillChampion()
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
                    this.StrengthRatioOverTime < 2 &&
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
                    this.StrengthRatioOverTime < 4 &&
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
                    SivirCanCastAbility3() &&
                    SetUnitAISpellTarget(this.Self, 3) &&
                    CastUnitSpell(this.Self, SPELLBOOK_CHAMPION, 3)
                ) ||
                (
                    SivirCanCastAbility0() &&
                    SivirCastAbility0()
                ) ||
                SivirAutoAttackTarget()
            )
        ;
    }

    bool SivirMicroRetreat()
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

    bool SivirAutoAttackTarget()
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

    bool SivirCanCastAbility0()
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

    bool SivirCanCastAbility1()
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

    bool SivirCanCastAbility2()
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

    bool SivirCanCastAbility3()
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

    bool SivirCastAbility0()
    {
        float Range;
        AttackableUnit Target;
        float Distance;
        return

            GetUnitSpellCastRange(out Range, this.Self, SPELLBOOK_CHAMPION, 0) &&
            SetVarFloat(out Range, 700) &&
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

    bool SivirPushLane()
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

    bool SivirHeal()
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

    bool SivirFindClosestVisibleTarget()
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