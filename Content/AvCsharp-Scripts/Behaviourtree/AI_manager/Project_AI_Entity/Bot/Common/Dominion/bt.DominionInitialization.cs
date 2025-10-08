using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class DominionInitializationClass : AI_Characters
{


    public bool DominionInitialization(
           out float _LastUseSpellTime,
     out string _PreviousTask,
     out float _RetreatPositionStartTime,
     out Vector3 _RetreatSafePosition,
     out float _RetreatFromCP_RetreatUntilTime,
     out float _WanderUntilTime,
     out bool _IsDominionGameMode,
     out float _BeginnerWaitInBaseTime,
     out float _NextActionTime,
     out object _DominionAttackMinion,
   //  out int _CurrentSpellCast,
   //  out AttackableUnit _CurrentSpellCastTarget,
    // out float _PreviousSpellCastTime,
  //   out float _CastSpellTimeThreshold,
     out object _MoveToCapturePointAbilities,
  //   out bool _SpellStall,
     out int _GarrisonSlot,
     AttackableUnit Self


        )
    {
        float LastUseSpellTime = default;
        string PreviousTask = default;
        float RetreatPositionStartTime = default;
        Vector3 RetreatSafePosition = default;
        float RetreatFromCP_RetreatUntilTime = default;
        float WanderUntilTime = default;
        bool IsDominionGameMode = default;
        float BeginnerWaitInBaseTime = default;
        float NextActionTime = default;
        object DominionAttackMinion = default;
        int CurrentSpellCast = default;
        AttackableUnit CurrentSpellCastTarget = default;
        float PreviousSpellCastTime = default;
        float CastSpellTimeThreshold = default;
        object MoveToCapturePointAbilities = default;
        bool SpellStall = default;
        int GarrisonSlot = default;



        bool result =
              // Sequence name :DominionInitialization
              (
                    SetVarFloat(
                          out LastUseSpellTime,
                          -20) &&
                    SetVarString(
                          out PreviousTask,
                          ""

                          ) &&
                    SetVarFloat(
                          out RetreatPositionStartTime,
                          -10) &&
                    GetUnitPosition(
                          out RetreatSafePosition,
                          Self) &&
                    SetVarFloat(
                          out RetreatFromCP_RetreatUntilTime,
                          -100) &&
                    SetVarFloat(
                          out WanderUntilTime,
                          -100) &&
                    SetVarBool(
                          out IsDominionGameMode,
                          true) &&
                    SetVarFloat(
                          out BeginnerWaitInBaseTime,
                          0) &&
                    SetVarFloat(
                          out LastUseSpellTime,
                          0) &&
                    SetVarFloat(
                          out NextActionTime,
                          0) &&
                    SetVarInt(
                          out GarrisonSlot,
                          -1) &&
                    SetProcedureVariable(
                          out DominionAttackMinion, "DominionAttackMinion") &&
                    SetProcedureVariable(
                          out MoveToCapturePointAbilities, "MoveToCapturePoint") &&
                    MakeVector(
                          out OrderBaseTopEntrance,
                          1606,
                          -189,
                          5893) &&
                    MakeVector(
                          out OrderBaseBottomEntrance,
                          2359,
                          -187,
                          5893) &&
                    MakeVector(
                          out ChaosBaseTopEntrance,
                          12315,
                          -189,
                          5881) &&
                    MakeVector(
                          out ChaosBaseBottomEntrance,
                          11537,
                          -187,
                          3325)

              );
        _LastUseSpellTime = LastUseSpellTime;
        _PreviousTask = PreviousTask;
        _RetreatPositionStartTime = RetreatPositionStartTime;
        _RetreatSafePosition = RetreatSafePosition;
        _RetreatFromCP_RetreatUntilTime = RetreatFromCP_RetreatUntilTime;
        _WanderUntilTime = WanderUntilTime;
        _IsDominionGameMode = IsDominionGameMode;
        _BeginnerWaitInBaseTime =  BeginnerWaitInBaseTime;
        _NextActionTime = NextActionTime;
        _DominionAttackMinion = DominionAttackMinion;
       // _CurrentSpellCast = CurrentSpellCast;
       // _CurrentSpellCastTarget = CurrentSpellCastTarget;
       // _PreviousSpellCastTime = PreviousSpellCastTime;
       // _CastSpellTimeThreshold = CastSpellTimeThreshold;
        _MoveToCapturePointAbilities = MoveToCapturePointAbilities;
       // _SpellStall = SpellStall;
        _GarrisonSlot = GarrisonSlot;
        return result;
    }

}

