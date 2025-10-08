using System.Reflection;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    public static class ServerLibAssemblyDefiningType
    {
        public static Assembly Assembly => typeof(ServerLibAssemblyDefiningType).Assembly;
    }
}
