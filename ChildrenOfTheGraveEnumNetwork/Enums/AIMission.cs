using System;

namespace ChildrenOfTheGraveEnumNetwork.Enums
{
    [Flags]
    public enum AIMission : uint
    {
        BARON,
        BOSS,
        DEFEND,
        SUPPORT,
        KILL,
        PUSH
        /* Mission_PushBot = 0,
         Mission_PushMid = 1,
         Mission_PushTop = 2,
         Mission_WaitAtBase = 3,
         CapturePointA = 4,
         CapturePointB = 5,
         CapturePointC = 6,
         CapturePointD = 7,
         CapturePointE = 8,
         WaitInBaseMission = 9*/

    }


    public enum AIMissionTopicType : uint
    {
        BARON,
        BOSS,
        DEFEND,
        SUPPORT,
        KILL,
        PUSH




    }

    public enum AIMissionCollection : uint { }
}

/*
	<ParameterDefinition Name="AIMission" SupportedType="String" SubType="" />
	<ParameterDefinition Name="AIMissionCollection" SupportedType="String" SubType="" />
	<ParameterDefinition Name="AIMissionTopicType" SupportedType="Enum" SubType="">
      <Options>
        <Option Value="BARON" />
		<Option Value="BOSS" />
        <Option Value="DEFEND" />
        <Option Value="SUPPORT" />
		<Option Value="KILL" />
		<Option Value="PUSH" />
      </Options>
*/