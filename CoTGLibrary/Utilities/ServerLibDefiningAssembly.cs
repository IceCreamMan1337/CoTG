using System.Reflection;

namespace CoTG.CoTGServer
{
    public static class ServerLibAssemblyDefiningType
    {
        public static Assembly Assembly => typeof(ServerLibAssemblyDefiningType).Assembly;
    }
}
