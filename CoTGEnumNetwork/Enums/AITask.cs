using System;

namespace CoTGEnumNetwork.Enums
{
    [Flags]
    public enum AITaskTopicType : uint
    {
        DOMINION_RETURN_TO_BASE = 0,
        DONE = 1,
        ASSIST = 2,
        GOTO = 3,
        DOMINION_WAIT = 4,
        DOMINION_GOTO = 5,
        ASSAULT_CAPTURE_POINT = 6,
        PUSH_TO_CAPTURE_POINT = 7,
        NINJA_CAPTURE_POINT = 8,
        DEFEND_CAPTURE_POINT = 9,
        OTHER = 10,
        PUSH = 11,
        KILL = 12,
        PUSHLANE = 13,
        WAIT = 14,
        DEFEND = 15,


    }


    [Flags]
    public enum AITaskCollection : uint
    {

    }


    [Flags]
    public enum LogicResultType : uint
    {
        COMPLETE,
        FAILURE,
        INVALID,
        RUNNING

    }


}

/*   <ParameterDefinition Name="AISquad" SupportedType="String" SubType="" />
   <ParameterDefinition Name="AISquadCollection" SupportedType="String" SubType="" />
   <ParameterDefinition Name="AITask" SupportedType="String" SubType="" />
   <ParameterDefinition Name="AITaskCollection" SupportedType="String" SubType="" />
   <ParameterDefinition Name="AITaskTopicType" SupportedType="Enum" SubType="">
      <Options>
        <Option Value="PUSHLANE" />
		<Option Value="GOTO" />
        <Option Value="DEFEND" />
        <Option Value="SUPPORT" />
		<Option Value="KILL" />
		<Option Value="WAIT" />
		<Option Value="DOMINION_GOTO" />
        <Option Value="NINJA_CAPTURE_POINT" />
        <Option Value="DEFEND_CAPTURE_POINT" />
		<Option Value="PUSH_TO_CAPTURE_POINT" />
		<Option Value="ASSAULT_CAPTURE_POINT" />
        <Option Value="GANKAT_CAPTURE_POINT" />
        <Option Value="CAPTURE_POINT" />
        <Option Value="DOMINION_WAIT" />
		<Option Value="DOMINION_RETURN_TO_BASE" />
		<Option Value="ASSIST" />
      </Options>
    </ParameterDefinition>

 
    <ParameterDefinition Name="LogicResultType" SupportedType="Enum" SubType="">
    <Options>
        <Option Value="COMPLETE" />
        <Option Value="FAILURE" />
        <Option Value="INVALID" />
        <Option Value="RUNNING" />
    </Options>
   </ParameterDefinition>
 
 
 
 */