using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsHeal : ArgsForClient
    {
        public float Amount { get; set; }


        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            ScriptNameHash = reader.ReadUInt32();
            EventSource = reader.ReadByte();
            Unknown = reader.ReadByte();
            SourceObjectNetID = reader.ReadUInt32();
            Amount = reader.ReadFloat();
            ParentScriptNameHash = reader.ReadUInt32();
            ParentCasterNetID = reader.ReadUInt32();
            Bitfield = reader.ReadUInt16();

        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            writer.WriteUInt32(ScriptNameHash);
            writer.WriteUInt32(EventSource);
            writer.WriteUInt32(SourceObjectNetID);
            writer.WriteFloat(Amount);
            writer.WriteUInt32(ParentScriptNameHash);
            writer.WriteUInt32(ParentCasterNetID);
            writer.WriteUInt16(Bitfield);

        }
    }
}
/* 
  float mAmount;
  EventSystem::EventSource mEventSource;
  unsigned int mSourceScriptNameHash;
  int mSourceSpellLevel;
  EventSystem::EventSource mParentEventSource;
  unsigned int mParentScriptNameHash;
  unsigned int mParentCaster;
  __int32 mParentTeam : 4;
  __int32 mParentSourceType : 4;
 */