using System;
using System.IO;
using System.Reflection;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer;

public static class BuildInfo
{
    public static string ServerFlavor { get; } = "Yorick";
    public static string ServerBuild { get; } = "0.2.0";
    public static string ServerVersion { get; } = $"{ServerFlavor} {ServerBuild}";
    public static string ServerBuildTime { get; } = $"League Sandbox Build {BuildDateString}";

    public static string ExecutingDirectory => Path.GetDirectoryName(
        Assembly.GetExecutingAssembly().Location
    );

    private static string BuildDateString => (
        (BuildDateTimeAttribute)Attribute.GetCustomAttribute(
            Assembly.GetExecutingAssembly(),
            typeof(BuildDateTimeAttribute)
        )
    )?.Date;
}

[AttributeUsage(AttributeTargets.Assembly)]
public class BuildDateTimeAttribute : Attribute
{
    public string Date { get; private set; }
    public BuildDateTimeAttribute(string date)
    {
        Date = date;
    }
}
