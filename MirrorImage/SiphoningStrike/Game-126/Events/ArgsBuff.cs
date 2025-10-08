using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsBuff : ArgsForClient
    {


        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            ScriptNameHash = reader.ReadUInt32();
            EventSource = reader.ReadByte();
            Unknown = reader.ReadByte();
            SourceObjectNetID = reader.ReadUInt32();
            ParentScriptNameHash = reader.ReadUInt32();
            ParentCasterNetID = reader.ReadUInt32();
            Bitfield = reader.ReadUInt16();
            /*
            need more search 
             base.ReadArgs(reader);
            ScriptNameHash = reader.ReadUInt32();
            EventSource = reader.ReadByte();
            Unknown = reader.ReadByte();
            Bitfield = reader.ReadUInt16();

            SourceObjectNetID = reader.ReadUInt32();
            ParentScriptNameHash = reader.ReadUInt32();
            ParentCasterNetID = reader.ReadUInt32();


            Byteleftfromargs = reader.ReadUInt32();
            */
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);

            writer.WriteUInt32(SourceObjectNetID);


            writer.WriteUInt32(EventSource);
            writer.WriteUInt32(ScriptNameHash);
            writer.WriteUInt32(SourceSpellLevel);
            
            writer.WriteUInt32(SourceObjectNetID);
            writer.WriteUInt32(ParentScriptNameHash);
            writer.WriteUInt32(ParentCasterNetID);
            uint combinedBits = 0;
            combinedBits |= (ParentTeam & 0xF) << 28;  // 4 bits for ParentEventSource
            combinedBits |= (ParentSourceType & 0xF) << 24;
            writer.WriteUInt32(combinedBits);
        }
    }
}
/*  
   EventSystem::EventSource mEventSource;
  unsigned int mSourceScriptNameHash;
  int mSourceSpellLevel;
  EventSystem::EventSource mParentEventSource;
  unsigned int mParentScriptNameHash;
  unsigned int mParentCaster;
  __int32 mParentTeam : 4;
  __int32 mParentSourceType : 4;
 
 */