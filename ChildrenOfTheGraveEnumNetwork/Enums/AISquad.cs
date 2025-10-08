using System;

namespace ChildrenOfTheGraveEnumNetwork.Enums
{
    [Flags]
    public enum AISquad : uint
    {
        SquadTopLane = 0,
        SquadMidLane = 1,
        SquadBotLane = 2,
        Squad_PushBot = 3,
        Squad_PushMid = 4,
        Squad_PushTop = 5,
        Squad_WaitAtBase = 6,
        CapturePointASquad = 7,
        CapturePointBSquad = 8,
        CapturePointCSquad = 9,
        CapturePointDSquad = 10,
        CapturePointESquad = 11,
        WaitInBaseSquad = 12,
        KillSquad = 13,
        Empty = 99
    }
    [Flags]


    public enum AISquadCollection : uint { }

}

/*
   <ParameterDefinition Name="AISquad" SupportedType="String" SubType="" />
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
*/