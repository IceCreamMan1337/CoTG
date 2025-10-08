#nullable enable

using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.AttackableUnits.AI;

namespace CoTG.CoTGServer.Scripting.Lua
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