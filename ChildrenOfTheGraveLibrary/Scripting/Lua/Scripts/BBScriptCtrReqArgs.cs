#nullable enable

using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua
{
    public class BBScriptCtrReqArgs
    {
        public string Name;
        public AttackableUnit ScriptOwner;
        public Champion? UnitOwner;
        public BBScriptCtrReqArgs(string name, AttackableUnit scriptOwner, Champion? unitOwner = null)
        {
            Name = name;
            ScriptOwner = scriptOwner;
            UnitOwner = unitOwner;
        }
    }
}