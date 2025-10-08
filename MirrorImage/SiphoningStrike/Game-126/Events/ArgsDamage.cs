using System;
using System.IO;
using System.Text;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using MirrorImage;

namespace SiphoningStrike.Game.Events
{
    public class ArgsDamage : ArgsForClient
    {
        public float PhysicalDamage { get; set; }
        public float MagicalDamage { get; set; }
        public float TrueDamage { get; set; }
      
        public uint ParentSourceSpellLevel { get; set; }


        public override void ReadArgs(ByteReader reader)
        {
            base.ReadArgs(reader);
            ScriptNameHash = reader.ReadUInt32();
            EventSource = reader.ReadByte();
            Unknown = reader.ReadByte();
            SourceObjectNetID = reader.ReadUInt32();
            PhysicalDamage = reader.ReadFloat();
            MagicalDamage = reader.ReadFloat();
            TrueDamage = reader.ReadFloat();
            ParentScriptNameHash = reader.ReadUInt32();
            ParentCasterNetID = reader.ReadUInt32();
            Bitfield = reader.ReadUInt16();

            /*
            base.ReadArgs(reader);
            ScriptNameHash = reader.ReadUInt32();
          //  EventSource = reader.ReadByte();
          //  Unknown = reader.ReadByte();
            SourceObjectNetID = reader.ReadUInt32();
            PhysicalDamage = reader.ReadFloat();
            MagicalDamage = reader.ReadFloat();
            TrueDamage = reader.ReadFloat();
            ParentScriptNameHash = reader.ReadUInt32();
            ParentScriptNameHash = reader.ReadUInt32();

            ParentCasterNetID = reader.ReadUInt32();

            Bitfield = reader.ReadUInt16();
            Byteleftfromargs = reader.ReadUInt16();
            */
        }
        public override void WriteArgs(ByteWriter writer)
        {
            base.WriteArgs(writer);
            //writer.WriteUInt32(ScriptNameHash);
            //writer.WriteByte(EventSource);
            //writer.WriteByte(Unknown);
           // writer.WriteUInt32(SourceObjectNetID);
            writer.WriteFloat(PhysicalDamage);
            writer.WriteFloat(MagicalDamage);
            writer.WriteFloat(TrueDamage);
            writer.WriteUInt32(EventSource);
            writer.WriteUInt32(ScriptNameHash);
            writer.WriteUInt32(ParentScriptNameHash);
            writer.WriteUInt32(ParentCasterNetID);
            int combinedBits = 0;
            combinedBits |= ((int)EventSource & 0xF) << 00;  // 4 bits for ParentEventSource
            combinedBits |= ((int)ParentTeam & 0xF) << 04;        // 4 bits for ParentTeam
            combinedBits |= ((int)ParentSourceType & 0xF) << 08;  // 4 bits for ParentSourceType
            combinedBits |= ((int)SourceSpellLevel & 0x7) << 11;  // 3 bits for SourceSpellLevel

            // Write the combined 32-bit value to the writer
            writer.WriteInt32(combinedBits);


        }
    }
}


/*

  float mPhysicalDamage;
  float mMagicalDamage;
  float mTrueDamage;
  EventSystem::EventSource mEventSource;
  unsigned int mSourceScriptNameHash;
  unsigned int mParentScriptNameHash;
  unsigned int mParentCaster;
  __int32 mParentEventSource : 4;
  __int32 mParentTeam : 4;
  __int32 mParentSourceType : 4;
  __int32 mSourceSpellLevel : 3;

*/