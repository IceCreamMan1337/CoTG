using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class ActionsDominionClass : AI_Characters 
{
    private SummonerClarityClass summonerClarity = new SummonerClarityClass();
    private DominionGlobalClass dominionGlobal = new DominionGlobalClass();
    private DominionHealClass dominionHeal = new DominionHealClass();
    private SummonerCleanseClass summonerCleanse = new SummonerCleanseClass();
    private DominionNinjaCapturePointClass dominionNinjaCapturePoint = new DominionNinjaCapturePointClass();
    private SuppressCapturePointClass suppressCapturePoint = new SuppressCapturePointClass();
    private InterruptCaptureClass interruptCapture = new InterruptCaptureClass();
    private SummonerGarrisonClass summonerGarrison = new SummonerGarrisonClass();
    private DominionKillChampionClass dominionKillChampion = new DominionKillChampionClass();
    private DominionHighThreatClass dominionHighThreat = new DominionHighThreatClass();
    private DominionLowThreatClass dominionLowThreat = new DominionLowThreatClass();
    private DominionReturnToBaseClass dominionReturnToBase = new DominionReturnToBaseClass();
    private DominionHelpKillChampionClass dominionHelpKillChampion = new DominionHelpKillChampionClass();
    private DominionMidThreatClass dominionMidThreat = new DominionMidThreatClass();
    private CapturePointClass capturePoint = new CapturePointClass();
    private DominionAttackClass dominionAttack = new DominionAttackClass();
    private DominionRetreatFromEnemyCapturePointClass dominionRetreatFromEnemyCapturePoint = new DominionRetreatFromEnemyCapturePointClass();
    private WanderClass wander = new WanderClass();
    private MoveToCapturePointClass moveToCapturePoint = new MoveToCapturePointClass();
    private PostActionAndDebugClass postActionAndDebug = new PostActionAndDebugClass();
    private IssueEventClass issueEvent = new IssueEventClass();


    public bool ActionsDominion(
 

     



      out bool __TeleportHome,
      out bool __LowThreatMode,
      out bool __TargetDeaggro,
      out float __TargetDeaggroTime,
      out AttackableUnit __Target,
      out Vector3 __TargetAcquiredPosition,
      out bool __TargetValid,
      out Vector3 __LastKnownPosition,
      out float __LastKnownTime,
       out int __KillChampionScore,
         out bool __IssuedAttack,
      out AttackableUnit __IssuedAttackTarget,


      out float __PrevRetreatTime,
        out float __PreviousSpellCastTime,
         out float __CastSpellTimeThreshold,


             out int _CurrentSpellCast,
      out AttackableUnit _CurrentSpellCastTarget,


      out float __LastUseSpellTime,
      out float __RetreatPositionStartTime,
      out Vector3 __RetreatSafePosition,
      out float __RetreatFromCP_RetreatUntilTime,
      out float __WanderUntilTime,
      out AttackableUnit __PrevKillChampTarget,
      out float __PrevKillChampTargetHealth,
      out float __PrevKillChampDamageTime,
      out float __BeginnerWaitInBaseTime,
      out float __NextActionTime,
            out bool __SpellStall,


                 out string __PreviousActionPerformed,
      out float __LastIssuedEventTime,
      out int __ActionDebugText,
      out int __TaskDebugText,







           AttackableUnit Self,
      Vector3 SelfPosition,
      bool TeleportHome,
      bool LowThreatMode,
      float StrengthRatioOverTime,
      float DamageOverTime,
      bool TargetDeaggro,
      float TargetDeaggroTime,
      AttackableUnit Target,
      Vector3 TargetAcquiredPosition,
      bool TargetValid,
      Vector3 LastKnownPosition,
      float LastKnownTime,
      int KillChampionScore,
      bool IssuedAttack,
      AttackableUnit IssuedAttackTarget,
      int PreviousSpellCast,
      AttackableUnit PreviousSpellCastTarget,
      float PrevRetreatTime,
      float PreviousSpellCastTime,


      float CastSpellTimeThreshold,
      string Champion,
      float ValueOfAbility0,
      float ValueOfAbility1,
      float ValueOfAbility2,
      float ValueOfAbility3,
      int CC_AbilityNumber,
      float AbilityReadinessMinScore,
      float AbilityReadinessMaxScore,
      float AbilityNotReadyMinScore,
      float AbilityNotReadyMaxScore,
      int ClaritySlot,
      int ExhaustSlot,
      int GhostSlot,
      int HealSlot,
      int IgniteSlot,
      int TeleportSlot,
      float DamageRatioThreshold,
      float LowHealthPercentThreshold,
      float LastKnownThreshold,
      bool SpellStall,
      bool RunBT_KillChampion,
      bool RunBT_HighThreat,
      bool RunBT_LowThreat,
      bool RunBT_CapturePoint,
      bool RunBT_ReturnToBase,
      bool RunBT_Attack,
      bool RunBT_Move,
      bool RunBT_Wander,
      bool RunBT_SuppressCapturePoint,
      float LastUseSpellTime,
      float RetreatPositionStartTime,
      Vector3 RetreatSafePosition,
      bool RunBT_InterruptCapture,
      bool KillChampionAggressiveState,
      bool RunBT_RetreatFromCapturePoint,
      float RetreatFromCP_RetreatUntilTime,
      float WanderUntilTime,
      AttackableUnit PrevKillChampTarget,
      float PrevKillChampTargetHealth,
      float PrevKillChampDamageTime,
      bool RunBT_MidThreat,
      int GarrisonSlot,
      int PromoteSlot,
      bool RunBT_NinjaCapturePoint,
      float BeginnerWaitInBaseTime,
      float NextActionTime,
      object HealAbilities, //fucntion
      object KillChampionAttackSequence, //function
      object KillChampionScoreModifier,  //function
    
      object HighThreatManagementSpells,//function
      object DominionAttackMinion,//function
      object MoveToCapturePointAbilities,//function
      object PostActionBehavior,//function
     
      int SurgeSlot,
      int CleanseSlot,
      int FlashSlot,
      object GlobalAbilities,//function
          string PreviousActionPerformed,
       float LastIssuedEventTime,
       int ActionDebugText,
       int TaskDebugText

         )
    {

        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
       int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
       bool _SpellStall = SpellStall;
        int _KillChampionScore = KillChampionScore;
       bool _TeleportHome = TeleportHome;
       bool _LowThreatMode = LowThreatMode;
       bool _TargetDeaggro = TargetDeaggro;
       float _TargetDeaggroTime = TargetDeaggroTime;
        AttackableUnit _Target = Target;
       Vector3 _TargetAcquiredPosition = TargetAcquiredPosition;
       bool _TargetValid = TargetValid;
       Vector3 _LastKnownPosition = LastKnownPosition;
       float _LastKnownTime = LastKnownTime;
       float _PrevRetreatTime = PrevRetreatTime;
       float _LastUseSpellTime = LastUseSpellTime;
       float _RetreatPositionStartTime = RetreatPositionStartTime;
       Vector3 _RetreatSafePosition = RetreatSafePosition;
        float _RetreatFromCP_RetreatUntilTime = RetreatFromCP_RetreatUntilTime;
       float _WanderUntilTime = WanderUntilTime;
        AttackableUnit _PrevKillChampTarget = PrevKillChampTarget;
       float _PrevKillChampTargetHealth = PrevKillChampTargetHealth;
       float _PrevKillChampDamageTime = PrevKillChampDamageTime;
       float _BeginnerWaitInBaseTime = BeginnerWaitInBaseTime;
       float _NextActionTime = NextActionTime;


       string _PreviousActionPerformed = PreviousActionPerformed;
       float _LastIssuedEventTime = LastIssuedEventTime;
        int _ActionDebugText = ActionDebugText;
        int _TaskDebugText = TaskDebugText;


        AttackableUnit TempTarget = default;


      
        string ActionPerformed = default;
      



        bool result =
            // Sequence name :Actions
            (
                  SetVarInt(
                        out _CurrentSpellCast,
                        -1) &&
                  SetVarString(
                        out ActionPerformed,
                        "") &&
                  GetGameTime(
                        out GameTime) &&
                  GreaterEqualFloat(
                        GameTime,
                        79.5f) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :DebugText
                        (
                              ActionDebugText == -1 &&
                              MakeColor(
                                    out ActionDebugTextColor,
                                    255,
                                    0,
                                    255,
                                    255) &&
                              MakeColor(
                                    out TaskDebugTextColor,
                                    0,
                                    255,
                                    255,
                                    255) &&
                              MakeVector(
                                    out TextOffset,
                                    0,
                                    15,
                                    0) &&
                              AddDebugText(
                                    out _ActionDebugText,

                                    Self,
                                    default, default,
                                    ActionDebugTextColor,
                                    10000,
                                    "",
                                    false,
                                    1) &&
                              AddDebugText(
                                    out _TaskDebugText,
                                    
                                    Self,
                                    TextOffset, default,
                                    TaskDebugTextColor,
                                    10000,
                                    "",
                                    false,
                                    1) &&
                              SetDebugHidden(
                                    _ActionDebugText,
                                    true) &&
                              SetDebugHidden(
                                    _TaskDebugText,
                                    true)
                        )
                  ) &&
                  // Sequence name :KillDifferential
                  (
                        GetChampionKills(
                              out Kills,
                              Self) &&
                        GetChampionDeaths(
                              out Deaths,
                              Self) &&
                        SubtractInt(
                              out KillDiff,
                              Kills,
                              Deaths)
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              TestEntityDifficultyLevel(
                                    false,
                                  EntityDiffcultyType.DIFFICULTY_ADVANCED) &&
                              DivideInt(
                                    out IntModifier,
                                    KillDiff,
                                    3) &&
                              // Sequence name :Difficulty
                              (
                                    // Sequence name :Beginner
                                    (
                                          TestEntityDifficultyLevel(
                                                true,
                                               EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                                          MinInt(
                                                out IntModifier,
                                                IntModifier,
                                                3) &&
                                          MaxInt(
                                                out IntModifier,
                                                IntModifier,
                                                0) &&
                                          AddFloat(
                                                out _CastSpellTimeThreshold,
                                                3,
                                                IntModifier)
                                    ) ||
                                    // Sequence name :Intermediate
                                    (
                                          TestEntityDifficultyLevel(
                                                true,
                                               EntityDiffcultyType.DIFFICULTY_INTERMEDIATE) &&
                                          MinInt(
                                                out IntModifier,
                                                IntModifier,
                                                1) &&
                                          MaxInt(
                                                out IntModifier,
                                                IntModifier,
                                                0) &&
                                          AddFloat(
                                                out _CastSpellTimeThreshold,
                                                0,
                                                IntModifier)
                                    )
                              )
                        )
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              GetUnitBuffCount(
                                    out CaptureChannelCount,
                                    Self,
                                    "OdinCaptureChannel") &&
                              GreaterInt(
                                    CaptureChannelCount,
                                    0) &&
                              TestUnitIsChanneling(
                                    Self,
                                    true) &&
                              SetVarFloat(
                                    out LastUseSpellTime,
                                    GameTime)
                        )
                  ) &&
                  // Sequence name :MaskFailure
                  (
                        // Sequence name :Sequence
                        (
                              TestEntityDifficultyLevel(
                                    true,
                                    EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              GetUnitAIBasePosition(
                                    out BasePosition,
                                    Self) &&
                              DistanceBetweenObjectAndPoint(
                                    out DistanceToBase,
                                    Self,
                                    BasePosition) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              // Sequence name :Conditions
                              (
                                    GreaterFloat(
                                          DistanceToBase,
                                          1000)
                                    ||
                                    LessFloat(
                                          SelfHealthRatio,
                                          0.95f)
                              ) &&
                              GetGameTime(
                                    out _BeginnerWaitInBaseTime) &&
                              AddFloat(
                                    out _BeginnerWaitInBaseTime,
                                    BeginnerWaitInBaseTime,
                                    4) &&
                              DivideInt(
                                    out IntModifier,
                                    KillDiff,
                                    3) &&
                              MaxInt(
                                    out IntModifier,
                                    IntModifier,
                                    0) &&
                              MinInt(
                                    out IntModifier,
                                    IntModifier,
                                    4) &&
                              AddFloat(
                                    out _BeginnerWaitInBaseTime,
                                    BeginnerWaitInBaseTime,
                                    IntModifier)
                        )
                  ) &&
                  // Sequence name :ChooseOneToPerform
                  (
                        // Sequence name :BeginnerWaitInBase
                        (
                              TestEntityDifficultyLevel(
                                    true,
                                     EntityDiffcultyType.DIFFICULTY_NEWBIE) &&
                              LessFloat(
                                    DistanceToBase,
                                    1000) &&
                              GetUnitHealthRatio(
                                    out SelfHealthRatio,
                                    Self) &&
                              GreaterFloat(
                                    SelfHealthRatio,
                                    0.95f) &&
                              GreaterFloat(
                                    BeginnerWaitInBaseTime,
                                    GameTime)
                        ) ||
                        // Sequence name :SpellStall
                        (
                              SpellStall == true &&
                              SetVarBool(
                                    out SpellStall,
                                    false)
                        ) ||
                        // Sequence name :IsChanneling
                        (
                              TeleportHome == false &&
                              TestUnitIsChanneling(
                                    Self,
                                    true) &&
                              TestUnitHasBuff(
                                    Self,
                                    default,
                                    "OdinCaptureChannel",
                                    false) &&
                              SetVarString(
                                    out ActionPerformed,
                                    "Channeling")
                        ) ||
                        // Sequence name :Clarity
                        (
                              NotEqualInt(
                                    ClaritySlot,
                                    -1) &&
                             summonerClarity.SummonerClarity(
                                    out ActionPerformed,
                                    Self,
                                    ClaritySlot)
                        ) ||
                        dominionGlobal.DominionGlobal(
                              out _CastSpellTimeThreshold,
                              out _PreviousSpellCastTime,
                              out _CurrentSpellCast,
                              out _CurrentSpellCastTarget,
                              out _SpellStall,
                              out ActionPerformed,
                              Self,
                              Champion,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTime,
                              IssuedAttack,
                              IssuedAttackTarget,
                              SpellStall,
                              GlobalAbilities)
                        &&
                        dominionHeal.DominionHeal(
                              out _CastSpellTimeThreshold,
                              out _PreviousSpellCastTime,
                              out _CurrentSpellCast,
                              out _CurrentSpellCastTarget,
                              out ActionPerformed,
                              out _SpellStall,
                              Self,
                              Champion,
                              HealSlot,
                              PreviousSpellCast,
                              PreviousSpellCastTarget,
                              CastSpellTimeThreshold,
                              PreviousSpellCastTime,
                              IssuedAttack,
                              IssuedAttackTarget,
                              HealAbilities,
                              SpellStall)
                        &&
                        // Sequence name :Cleanse
                        (
                              NotEqualInt(
                                    CleanseSlot,
                                    -1) &&
                              summonerCleanse.SummonerCleanse(
                                    Self,
                                    CleanseSlot)
                        ) ||
                        // Sequence name :NinjaCapturePoint
                        (
                              RunBT_NinjaCapturePoint == true &&
                              dominionNinjaCapturePoint.DominionNinjaCapturePoint(
                                    out ActionPerformed,
                                    Self,
                                    LastUseSpellTime,
                                    GameTime)
                        ) ||
                        // Sequence name :SuppressCapturePoint
                        (
                              RunBT_SuppressCapturePoint == true &&
                              suppressCapturePoint.SuppressCapturePoint(
                                    out ActionPerformed,
                                    Self,
                                    LastUseSpellTime,
                                    GameTime)
                        ) ||
                        // Sequence name :InterruptCapture
                        (
                              RunBT_InterruptCapture == true &&
                             interruptCapture.InterruptCapture(
                                    out ActionPerformed,
                                    out _CastSpellTimeThreshold,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _IssuedAttack,
                                    out _IssuedAttackTarget,
                                    out _PreviousSpellCastTime,
                                    out _SpellStall,
                                    Self,
                                    CastSpellTimeThreshold,
                                    "Champion",
                                    ExhaustSlot,
                                    GhostSlot,
                                    IgniteSlot,
                                    IssuedAttack,
                                    IssuedAttackTarget,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    SpellStall,
                                    GarrisonSlot,
                                    LastUseSpellTime,
                                    GameTime,
                                    KillChampionAttackSequence,
                                    FlashSlot)
                        ) ||
                        summonerGarrison.SummonerGarrison(
                              Self,
                              GarrisonSlot,
                              SelfPosition)
                        ||
                        // Sequence name :KillChampionAssault
                        (
                              RunBT_KillChampion == true &&
                              GetGameTime(
                                    out CurrentGameTime) &&
                              SubtractFloat(
                                    out RetreatTimeDiff,
                                    RetreatFromCP_RetreatUntilTime,
                                    CurrentGameTime) &&
                              LessFloat(
                                    RetreatTimeDiff,
                                    0) &&
                              dominionKillChampion.DominionKillChampion(
                                    out _TargetDeaggro,
                                    out _TargetValid,
                                    out _Target,
                                    out _TargetAcquiredPosition,
                                    out _KillChampionScore,
                                    out _IssuedAttackTarget,
                                    out _IssuedAttack,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _PrevRetreatTime,
                                    out _PreviousSpellCastTime,
                                    out _CastSpellTimeThreshold,
                                    out _SpellStall,
                                    out ActionPerformed,
                                    out _PrevKillChampTarget,
                                    out _PrevKillChampTargetHealth,
                                    out _PrevKillChampDamageTime,
                                    StrengthRatioOverTime,
                                    SelfPosition,
                                    Self,
                                    TargetDeaggro,
                                    TargetValid,
                                    Target,
                                    TargetAcquiredPosition,
                                    KillChampionScore,
                                    IssuedAttack,
                                    IssuedAttackTarget,
                                    DamageOverTime,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PrevRetreatTime,
                                    LastKnownPosition,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    _Champion,
                                    1050,
                                   ValueOfAbility0,
                                   ValueOfAbility1,
                                   ValueOfAbility2,
                                   ValueOfAbility3,
                                    CC_AbilityNumber,
                                    AbilityReadinessMinScore,
                                    AbilityReadinessMaxScore,
                                    AbilityNotReadyMinScore,
                                    AbilityNotReadyMaxScore,
                                    ExhaustSlot,
                                    FlashSlot,
                                    GhostSlot,
                                    IgniteSlot,
                                    SpellStall,
                                    KillChampionAggressiveState,
                                    PreviousSpellCastTarget,
                                    PrevKillChampTargetHealth,
                                    PrevKillChampDamageTime,
                                    KillChampionAttackSequence,
                                    KillChampionScoreModifier,
                                    SurgeSlot)
                        ) ||
                        // Sequence name :HighThreatManagement
                        (
                              RunBT_HighThreat == true &&
                              // Sequence name :DamageRatioThresholdAdjustment
                              (
                                    GetUnitLevel(
                                          out SelfLevel,
                                          Self) &&
                                    MultiplyFloat(
                                          out DamageRatioThresholdAdjustment,
                                          SelfLevel,
                                          0.0025f) &&
                                    SubtractFloat(
                                          out DamageRatioThreshold,
                                          DamageRatioThreshold,
                                          DamageRatioThresholdAdjustment)
                               ) &&
                             dominionHighThreat.DominionHighThreat(
                                    out _TargetValid,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _PreviousSpellCastTime,
                                    out _CastSpellTimeThreshold,
                                    out _SpellStall,
                                    out ActionPerformed,
                                    out _RetreatSafePosition,
                                    out _RetreatPositionStartTime,
                                    Self,
                                    DamageOverTime,
                                    DamageRatioThreshold,
                                    LowHealthPercentThreshold,
                                    TargetValid,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    SelfPosition,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    ExhaustSlot,
                                    GhostSlot,
                                    Champion,
                                    SpellStall,
                                    RetreatSafePosition,
                                    TargetDeaggroTime,
                                    HighThreatManagementSpells,
                                    FlashSlot)
                        ) ||
                        // Sequence name :LowThreat
                        (
                              RunBT_LowThreat == true &&
                              dominionLowThreat.DominionLowThreat(
                                    out _LowThreatMode,
                                    out _TargetValid,
                                    out ActionPerformed,
                                    StrengthRatioOverTime,
                                    LowThreatMode,
                                    Self,
                                    TargetValid)
                        ) ||
                        // Sequence name :ReturnToBase
                        (
                              RunBT_ReturnToBase == true &&
                            dominionReturnToBase.DominionReturnToBase(
                                    out _TeleportHome,
                                    out ActionPerformed,
                                    Self,
                                    TeleportHome,
                                    SelfPosition)
                        ) ||
                        // Sequence name :DominionHelpKillChampion
                        (
                              TestAIEntityHasTask(
                                    Self,
                                   AITaskTopicType.ASSIST,
                                    default,
                                    default,
                                    default,
                                    true) &&
                              GetAITaskFromEntity(
                                    out KillTask,
                                    Self) &&
                              GetAITaskTarget(
                                    out Target,
                                    KillTask) &&
                             dominionHelpKillChampion.DominionHelpKillChampion(
                                    out _TargetDeaggro,
                                    out _TargetValid,
                                    out _Target,
                                    out _TargetAcquiredPosition,
                                    out _KillChampionScore,
                                    out _IssuedAttackTarget,
                                    out _IssuedAttack,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _PrevRetreatTime,
                                    out _PreviousSpellCastTime,
                                    out _CastSpellTimeThreshold,
                                    out _SpellStall,
                                    out ActionPerformed,
                                    out _PrevKillChampTarget,
                                    out _PrevKillChampTargetHealth,
                                    out _PrevKillChampDamageTime,
                                    SelfPosition,
                                    Self,
                                    TargetDeaggro,
                                    TargetValid,
                                    Target,
                                    TargetAcquiredPosition,
                                    KillChampionScore,
                                    IssuedAttack,
                                    IssuedAttackTarget,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PrevRetreatTime,
                                    LastKnownPosition,
                                    PreviousSpellCastTime,
                                    CastSpellTimeThreshold,
                                    ExhaustSlot,
                                    FlashSlot,
                                    GhostSlot,
                                    IgniteSlot,
                                    SpellStall,
                                    PrevKillChampTarget,
                                    PrevKillChampTargetHealth,
                                    PrevKillChampDamageTime,
                                    KillChampionAttackSequence,
                                    KillChampionScoreModifier,
                                    SurgeSlot,
                                    Target)
                        ) ||
                        // Sequence name :MidThreat
                        (
                              RunBT_MidThreat == true &&
                             dominionMidThreat.DominionMidThreat(
                                    out ActionPerformed,
                                    out _CastSpellTimeThreshold,
                                    out _IssuedAttack,
                                    out _IssuedAttackTarget,
                                    out _PreviousSpellCastTime,
                                    out _SpellStall,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _RetreatFromCP_RetreatUntilTime,
                                    out _RetreatSafePosition,
                                    out _RetreatPositionStartTime,
                                    Self,
                                    CastSpellTimeThreshold,
                                    Champion,
                                    ExhaustSlot,
                                    GhostSlot,
                                    IgniteSlot,
                                    IssuedAttack,
                                    IssuedAttackTarget,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    SpellStall,
                                    RetreatFromCP_RetreatUntilTime,
                                    RetreatSafePosition,
                                    RetreatPositionStartTime,
                                    KillChampionAttackSequence,
                                    FlashSlot)
                        ) ||
                        // Sequence name :CapturePoint
                        (
                              RunBT_CapturePoint == true &&
                            capturePoint.CapturePoint(
                                    out ActionPerformed,
                                    Self,
                                    GameTime,
                                    LastUseSpellTime)
                        ) ||
                        // Sequence name :AttackMinions
                        (
                              RunBT_Attack == true &&
                            dominionAttack.DominionAttack(
                                    out ActionPerformed,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _PreviousSpellCastTime,
                                    out _CastSpellTimeThreshold,
                                    Self,
                                    SelfPosition,
                                    PromoteSlot,
                                    Champion,
                                    PreviousSpellCast,
                                    PreviousSpellCastTarget,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTime,
                                    DominionAttackMinion)
                        ) ||
                        // Sequence name :RetreatFromCapturePoint
                        (
                              RunBT_RetreatFromCapturePoint == true &&
                             dominionRetreatFromEnemyCapturePoint.DominionRetreatFromEnemyCapturePoint(
                                    out _RetreatFromCP_RetreatUntilTime,
                                    out ActionPerformed,
                                    out _RetreatSafePosition,
                                    out _RetreatPositionStartTime,
                                    Self,
                                    SelfPosition,
                                    RetreatFromCP_RetreatUntilTime,
                                    RetreatSafePosition,
                                    RetreatPositionStartTime)
                        ) ||
                        // Sequence name :Wander
                        (
                              RunBT_Wander == true &&
                             wander.Wander(
                                    out _WanderUntilTime,
                                    out ActionPerformed,
                                    Self,
                                    WanderUntilTime)
                        ) ||
                        // Sequence name :MoveTo
                        (
                              RunBT_Move == true &&
                            moveToCapturePoint.MoveToCapturePoint(
                                    out ActionPerformed,
                                    out PreviousSpellCast,
                                    out _CastSpellTimeThreshold,
                                    out _CurrentSpellCast,
                                    out _CurrentSpellCastTarget,
                                    out _PreviousSpellCastTime,
                                    out _SpellStall,
                                    Self,
                                    GhostSlot,
                                    CastSpellTimeThreshold,
                                    PreviousSpellCastTarget,
                                    PreviousSpellCastTime,
                                    SpellStall,
                                    Champion,
                                    MoveToCapturePointAbilities)
                        )
                  ) &&
               postActionAndDebug.PostActionAndDebug(
                        Self,
                        "ActionPerformed",
                        PostActionBehavior,
                        ActionDebugText,
                        TaskDebugText) &&
               issueEvent.IssueEvent(
                        out _PreviousActionPerformed,
                        out _LastIssuedEventTime,
                        ActionPerformed,
                        PreviousActionPerformed,
                        Self,
                        Target,
                        LastIssuedEventTime)

            )   ;

         __CastSpellTimeThreshold = _CastSpellTimeThreshold;
         __PreviousSpellCastTime = _PreviousSpellCastTime;
         _CurrentSpellCast = CurrentSpellCast;
         _CurrentSpellCastTarget = CurrentSpellCastTarget;
         __IssuedAttack = _IssuedAttack;
         __IssuedAttackTarget = _IssuedAttackTarget;
         __SpellStall = _SpellStall;
         __KillChampionScore = _KillChampionScore;
         __TeleportHome = _TeleportHome;
         __LowThreatMode = _LowThreatMode;
         __TargetDeaggro = _TargetDeaggro;
         __TargetDeaggroTime = _TargetDeaggroTime;
         __Target = _Target;
         __TargetAcquiredPosition = _TargetAcquiredPosition;
         __TargetValid = _TargetValid;
         __LastKnownPosition = _LastKnownPosition;
         __LastKnownTime = _LastKnownTime;
         __PrevRetreatTime = _PrevRetreatTime;
         __LastUseSpellTime = _LastUseSpellTime;
         __RetreatPositionStartTime = _RetreatPositionStartTime;
         __RetreatSafePosition = _RetreatSafePosition;
         __RetreatFromCP_RetreatUntilTime = _RetreatFromCP_RetreatUntilTime;
         __WanderUntilTime = _WanderUntilTime;
         __PrevKillChampTarget = _PrevKillChampTarget;
         __PrevKillChampTargetHealth = _PrevKillChampTargetHealth;
         __PrevKillChampDamageTime = _PrevKillChampDamageTime;
         __BeginnerWaitInBaseTime = _BeginnerWaitInBaseTime;
         __NextActionTime = _NextActionTime;
         __PreviousActionPerformed = _PreviousActionPerformed;
         __LastIssuedEventTime = _LastIssuedEventTime;
         __ActionDebugText = _ActionDebugText;
         __TaskDebugText = _TaskDebugText;
        return result;
    }
}

