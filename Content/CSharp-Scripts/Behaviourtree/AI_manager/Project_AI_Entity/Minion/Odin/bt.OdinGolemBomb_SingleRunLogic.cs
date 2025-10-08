using static CoTGEnumNetwork.Enums.SpellbookType;

namespace BehaviourTrees.Map8;


class OdinGolemBomb_SingleRunLogicClass : IODIN_MinionAIBT
{

    public bool OdinGolemBomb_SingleRunLogic(ObjAIBase owner)
    {
        this.Owner = owner;
        //at first view all init need be here
        Self = owner;
        MyTeam = owner.Team;
        TargetPoint = -1;
        var getTargetPoint = new GetTargetPointClass(); // a mettre en private
        var getGuardian = new GetGuardianClass(); // same 
        var getGraveyardPosition = new GetGraveyardPositionClass(); // same
        return

                   // Sequence name :Selector

                   // Sequence name :Init
                   (
                         TestUnitAIFirstTime() &&
                         GetUnitAISelf(
                               out Self) &&
                         SetVarInt(
                               out TargetPoint,
                               -1) &&
                         GetUnitTeam(
                               out MyTeam,
                               Self) &&
                         SetVarFloat(
                               out PreviousCastTime,
                               0)
                   ) ||


                  // Sequence name :Fear
                  (
                        TestUnitHasBuff(
                              Self,
                              null,
                              "Fear") &&
                        IssueWanderOrder(

                              )
                  ) ||
                          // Sequence name :Taunt
                          //moved to down 

                          (TestUnitHasBuff(
                                Self,
                                null,
                                "Taunt") &&
                          GetUnitBuffCaster(
                                out Taunter,
                                Self,
                                "Taunt") /*&&
                          IssueMoveToUnitOrder(
                                Taunter)*/

                     &&
                        // Sequence name :TargetValidTargetAcquisition

                        LessEqualInt(
                              TargetPoint,
                              -1) &&
                        getTargetPoint.GetTargetPoint(
                              out TargetPoint, owner)
                    && //hack
                       // Sequence name :Execution

                        GreaterEqualInt(
                              TargetPoint,
                              0) &&


                        getGuardian.GetGuardian(
                              out Guardian,
                              TargetPoint) &&

                        //  Minioncheckifcorrectgardian(Guardian) &&


                        // Sequence name :Selector
                        (
                              // Sequence name :AttackGuardian
                              (
                                    GetUnitTeam(
                                          out GuardianTeam,
                                          Guardian) &&



                                    NotEqualUnitTeam(
                                          GuardianTeam,
                                          MyTeam) &&

                                    Minioncheckifcorrectgardian(Guardian) &&

                                    SetUnitAIAttackTarget(
                                          Guardian) &&
                                    SetAIUnitSpellTarget(
                                          Guardian,
                                          SPELLBOOK_CHAMPION,
                                          0) &&

                                    DebugAction($"{(Self as ObjAIBase).TargetUnit.Model}") &&


                                    // Sequence name :Goto_Attack
                                    (
                                          // Sequence name :Sequence
                                          (
                                                GetDistanceBetweenUnits(
                                                      out Distance,
                                                      Guardian,
                                                      Self) &&



                                                GetUnitSpellCastRange(
                                                      out SpellRange,
                                                      Self,
                                                      SPELLBOOK_CHAMPION,
                                                      0) &&
                                                MultiplyFloat(
                                                      out SpellRange,
                                                      SpellRange,
                                                      0.8f) &&

                                                LessEqualFloat(
                                                      Distance, //hack 
                                                      SpellRange) &&
                                                // Sequence name :MaskFailure
                                                (
                                                      // Sequence name :Sequence
                                                      (
                                                            GetGameTime(
                                                                  out CurrentGameTime) &&
                                                            SubtractFloat(
                                                                  out TimePassed,
                                                                  CurrentGameTime,
                                                                  PreviousCastTime) && //

                                                            /*  DebugAction($"PreviousCastTime = {PreviousCastTime } TimePassed ={TimePassed} < 1.7f ? ") &&
                                                    DebugCrash() &&*/

                                                            GreaterEqualFloat(
                                                                  TimePassed,
                                                                  1.7f) &&
                                                            SetVarFloat(
                                                                  out PreviousCastTime,
                                                                  CurrentGameTime) &&
                                                            CastUnitSpell(
                                                                  Self,
                                                                  SPELLBOOK_CHAMPION,
                                                                  0,
                                                                  1
                                                                  )
                                                      )
                                                      ||
                                                    //  ClearUnitAIAttackTarget2() &&
                                                    DebugAction("MaskFailure")
                                                ) //&&
                                                  //   StopUnitMovement(
                                                  //      Self)
                                          ) ||
                                               // Sequence name :Sequence

                                               IssueMoveToPositionOrderminion()

                                    )
                              ) ||
                              // Sequence name :GotoGraveyard
                              (




                                /*    DebugAction("forceupdate")  && 
                                    DebugCrash() && */
                                getGraveyardPosition.GetGraveyardPosition(
                                      out TargetPosition,
                                      TargetPoint) &&
                                IssueMoveToPositionOrder(
                                      TargetPosition)

                              )
                        )) //|| DebugAction("forceupdate")

            ;

    }
}