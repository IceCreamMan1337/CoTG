using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;
using static Dropbox.Api.TeamPolicies.SmartSyncPolicy;

namespace BehaviourTrees.all;


class ActionsClass : AI_Characters
{
    private HighThreatManagementClass highThreatManagement = new HighThreatManagementClass();
    private SummonerClarityClass summonerClarity = new SummonerClarityClass();
    private GlobalClass global = new GlobalClass();
    private HealClass heal = new HealClass();
    private SummonerCleanseClass summonerCleanse = new SummonerCleanseClass();
    private KillChampionClass killChampion = new KillChampionClass();
    private HelpKillChampionClass helpKillChampion = new HelpKillChampionClass();
    private LowThreatClass lowThreat = new LowThreatClass();
    private ReturnToBaseClass returnToBase = new ReturnToBaseClass();
    private AttackClass attack = new AttackClass();
    private PushLaneClass pushLane = new PushLaneClass();
    private PostActionAndDebugClass postActionAndDebug = new PostActionAndDebugClass();
    private IssueEventClass issueEvent = new IssueEventClass();

    public bool Actions(

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
     object AttackTarget,


     //    AttackableUnit ToAttack,

     object HealAbilities,

     object HighThreatManagementSpells,

     object KillChampionAttackSequence,

     object KillChampionScoreModifier,



     //  AttackableUnit TempTarget,

     object PostActionBehavior,

     //  string ActionPerformed,
     object PushLaneAbilities,

     bool BeginnerScaling,
     int SurgeSlot,
     int CleanseSlot,
     int PromoteSlot,
     int FlashSlot,
     object GlobalAbilities,

     string PreviousActionPerformed,
     float LastIssuedEventTime,
     int ActionDebugText,
     int TaskDebugText
        )
    {
        bool _IssuedAttack = IssuedAttack;
        AttackableUnit _IssuedAttackTarget = IssuedAttackTarget;
        float _PreviousSpellCastTime = PreviousSpellCastTime;
        float _CastSpellTimeThreshold = CastSpellTimeThreshold;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
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
        string _PreviousActionPerformed = PreviousActionPerformed;
        float _LastIssuedEventTime = LastIssuedEventTime;
        int _ActionDebugText = ActionDebugText;
        int _TaskDebugText = TaskDebugText;



        string ActionPerformed = default;

        bool result =
              // Sequence name :Actions
              (
               DebugAction("Actions started") &&
                    SetVarInt(
                          out _CurrentSpellCast,
                          -1) &&
                    SetVarString(
                          out ActionPerformed,
                          "") &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :DebugText
                          (
                                ActionDebugText == -1 &&
                                TestGameStarted(
                                      true) &&
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
                                      out ActionDebugText,
                                      Self,
                                      default,
                                      default,
                                      ActionDebugTextColor,
                                      10000,
                                      default,
                                      false,
                                      1) &&
                                AddDebugText(
                                      out TaskDebugText,
                                      Self,
                                      default,
                                      TextOffset,
                                      TaskDebugTextColor,
                                      10000,
                                      default,
                                      false,
                                      1) &&
                                SetDebugHidden(
                                      ActionDebugText,
                                      true) &&
                                SetDebugHidden(
                                      TaskDebugText,
                                      true)
                          )
                    ) || MaskFailure()
                    &&
                    // Sequence name :MaskFailure
                    (
                          // Sequence name :Sequence
                          (
                                BeginnerScaling == true &&
                                SetVarFloat(
                                      out CastSpellTimeThreshold,
                                      6)
                          )
                    ) || MaskFailure()
                    &&
                    // Sequence name :ChooseOneToPerform
                    (
                          TestGameStarted(
                                false) &&
                          // Sequence name :SpellStall
                          (
                             DebugAction("SpellStall") &&
                                SpellStall == true &&
                                SetVarBool(
                                      out _SpellStall,
                                      false)
                          ) ||
                          // Sequence name :IsChanneling
                          (
                            DebugAction("IsChanneling") &&
                                TeleportHome == false &&
                                TestUnitIsChanneling(
                                      Self,
                                      true) &&
                                SetVarString(
                                      out ActionPerformed,
                                      "Channeling")
                          ) ||
                          // Sequence name :Clarity
                          (
                           DebugAction("SummonerClarity") &&
                                NotEqualInt(
                                      ClaritySlot,
                                      -1) &&
                             summonerClarity.SummonerClarity(
                                      out ActionPerformed,
                                      Self,
                                      ClaritySlot)
                          ) ||
                          DebugAction("Global") && 
                       global.Global(
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
                                GlobalAbilities,
                                SpellStall)
                          ||
                            DebugAction("Heal") &&
                       heal.Heal(
                                out _CastSpellTimeThreshold,
                                out _PreviousSpellCastTime,
                                out _CurrentSpellCast,
                                out _CurrentSpellCastTarget,
                                out ActionPerformed,
                                Self,
                                Champion,
                                HealSlot,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                CastSpellTimeThreshold,
                                PreviousSpellCastTime,
                                IssuedAttack,
                                IssuedAttackTarget,
                                HealAbilities)
                          ||
                          // Sequence name :Cleanse
                          (
                                NotEqualInt(
                                      CleanseSlot,
                                      -1) &&
                            summonerCleanse.SummonerCleanse(
                                      Self,
                                      CleanseSlot)
                          ) ||
                           DebugAction("KillChampion") &&
                        killChampion.KillChampion(
                                out _TargetDeaggro,
                                out _TargetValid,
                                out _Target,
                                out _TargetAcquiredPosition,
                                out _KillChampionScore,
                                out _IssuedAttackTarget,
                                out _IssuedAttack,
                                out CurrentSpellCast,
                                out CurrentSpellCastTarget,
                                out _PrevRetreatTime,
                                out _PreviousSpellCastTime,
                                out _CastSpellTimeThreshold,
                                out _SpellStall,
                                out ActionPerformed,
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
                                Champion,
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
                                KillChampionAttackSequence,
                                KillChampionScoreModifier,
                                SurgeSlot)
                          ||
                          // Sequence name :HighThreatManagement
                          (
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
                                DebugAction("HighThreatManagement" + DamageRatioThreshold) &&
                         highThreatManagement.HighThreatManagement(
                                      out _TargetValid,
                                      out CurrentSpellCast,
                                      out CurrentSpellCastTarget,
                                      out _PreviousSpellCastTime,
                                      out _CastSpellTimeThreshold,
                                        out ActionPerformed,
                                      out _SpellStall,
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
                                      HighThreatManagementSpells,
                                      FlashSlot,
                                      PreviousActionPerformed)
                          ) ||
                          // Sequence name :HelpKillChampion
                          (
                                TestAIEntityHasTask(
                                      Self,
                                 AITaskTopicType.ASSIST,
                                     default,
                                     default,
                                     default,
                                      true) &&
                                TeleportHome == false &&
                                GetUnitHealthRatio(
                                      out SelfHealthRatio,
                                      Self) &&
                                GreaterFloat(
                                      SelfHealthRatio,
                                      0.25f) &&
                                GetAITaskFromEntity(
                                      out AssistTask,
                                      Self) &&
                                GetAITaskTarget(
                                      out EventTarget,
                                      AssistTask) &&
                                       DebugAction("HelpKillChampion") &&
                            helpKillChampion.HelpKillChampion(
                                      out _TargetDeaggro,
                                      out _TargetValid,
                                      out _Target,
                                      out _TargetAcquiredPosition,
                                      out _KillChampionScore,
                                      out _IssuedAttackTarget,
                                      out _IssuedAttack,
                                      out CurrentSpellCast,
                                      out CurrentSpellCastTarget,
                                      out _PrevRetreatTime,
                                      out _PreviousSpellCastTime,
                                      out _CastSpellTimeThreshold,
                                      out _SpellStall,
                                      out ActionPerformed,
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
                                      KillChampionAttackSequence,
                                      KillChampionScoreModifier,
                                      SurgeSlot,
                                      EventTarget)
                          ) ||
                             DebugAction("lowtreat and strength= " + StrengthRatioOverTime) &&
                       lowThreat.LowThreat(
                                out _LowThreatMode,
                                out _TargetValid,
                                out ActionPerformed,
                                StrengthRatioOverTime,
                                LowThreatMode,
                                Self,
                                TargetValid)
                          ||
                      returnToBase.ReturnToBase(
                                out _TeleportHome,
                                out ActionPerformed,
                                Self,
                                TeleportHome,
                                SelfPosition)
                          ||
                          // Sequence name :Attack
                          (
                           DebugAction("Sequence name :Attack ") &&
                                TestAIEntityHasTask(
                                      Self,
                                      AITaskTopicType.GOTO,
                                     default,
                                      default,
                                     default,
                                      false) &&
                              attack.Attack(
                                      out _TargetDeaggro,
                                      out _Target,
                                      out _TargetAcquiredPosition,
                                      out _TargetValid,
                                      out _TargetDeaggroTime,
                                      out _LastKnownPosition,
                                      out _LastKnownTime,
                                      out _IssuedAttack,
                                      out _IssuedAttackTarget,
                                      out _PreviousSpellCastTime,
                                      out CurrentSpellCast,
                                      out CurrentSpellCastTarget,
                                      out _SpellStall,
                                      out ActionPerformed,
                                      out _CastSpellTimeThreshold,
                                      Self,
                                      SelfPosition,
                                      TargetDeaggro,
                                      3.5f,
                                      Target,
                                      TargetAcquiredPosition,
                                      TargetValid,
                                      850,
                                      TargetDeaggroTime,
                                      LastKnownPosition,
                                      LastKnownTime,
                                      LastKnownThreshold,
                                      IssuedAttack,
                                      IssuedAttackTarget,
                                      PreviousSpellCast,
                                      PreviousSpellCastTarget,
                                      PreviousSpellCastTime,
                                      CastSpellTimeThreshold,
                                      Champion,
                                      SpellStall,
                                      AttackTarget,
                                      PromoteSlot)
                          ) ||
                           DebugAction("PushLane " ) &&
                         pushLane.PushLane(
                                out ActionPerformed,
                                out _CastSpellTimeThreshold,
                                out _CurrentSpellCast,
                                out _CurrentSpellCastTarget,
                                out _PreviousSpellCastTime,
                                out _SpellStall,
                                Self,
                                TeleportSlot,
                                CastSpellTimeThreshold,
                                Champion,
                                PreviousSpellCast,
                                PreviousSpellCastTarget,
                                PreviousSpellCastTime,
                                SpellStall,
                                PushLaneAbilities)
                    ) &&
                        DebugAction("PostActionAndDebug") &&
                 postActionAndDebug.PostActionAndDebug(
                          Self,
                          ActionPerformed,
                          PostActionBehavior,
                          _ActionDebugText,
                          _TaskDebugText) &&
                              DebugAction("IssueEvent") &&
                 issueEvent.IssueEvent(
                          out _PreviousActionPerformed,
                          out _LastIssuedEventTime,
                          ActionPerformed,
                          _PreviousActionPerformed,
                          Self,
                          _Target,
                          _LastIssuedEventTime)

              );

        __IssuedAttack = _IssuedAttack;
        __IssuedAttackTarget = _IssuedAttackTarget;
        __PreviousSpellCastTime = _PreviousSpellCastTime;
        __CastSpellTimeThreshold = _CastSpellTimeThreshold;
        _CurrentSpellCast = CurrentSpellCast;
        _CurrentSpellCastTarget = CurrentSpellCastTarget;
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
        __PreviousActionPerformed = _PreviousActionPerformed;
        __LastIssuedEventTime = _LastIssuedEventTime;
        __ActionDebugText = _ActionDebugText;
        __TaskDebugText = _TaskDebugText;
        return result;
    }
}
