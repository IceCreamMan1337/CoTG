namespace TechmaturgicalRepairBot.Game.Events
{
    public abstract class ArgsForClient : ArgsBase
    {
        public uint ScriptNameHash { get; set; }
        public byte EventSource { get; set; }
        // FIXME: new byte appeared here
        public byte Unknown { get; set; }
        public uint SourceObjectNetID { get; set; }
        public uint ParentScriptNameHash { get; set; }
        public uint ParentCasterNetID { get; set; }
        public ushort Bitfield { get; set; }

        public uint Byteleftfromargs { get; set; }


/*        public uint EventSource { get; set; }//
        public uint SourceScriptNameHash { get; set; }//
        public uint SourceSpellLevel { get; set; }//
        public uint ParentEventSource { get; set; }
        public uint ParentScriptNameHash { get; set; }//
        public uint ParentCasterNetID { get; set; }//

        public uint ParentTeam { get; set; }//
        public uint ParentSourceType { get; set; }*/
    }
}