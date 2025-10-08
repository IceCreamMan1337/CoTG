using System;
using System.Numerics;
using MirrorImage;

namespace CrystalSlash.Game.Common
{
    
    public class FXCreateGroupItem
    {
        public uint TargetNetID { get; set; }  //4
        public uint NetAssignedNetID { get; set; } //8
        public uint BindNetID { get; set; } //12
        public short PositionX { get; set; } //38
        public float PositionY { get; set; } //16
        public short PositionZ { get; set; } //18
        public short TargetPositionX { get; set; } //22
        public float TargetPositionY { get; set; } //24
        public short TargetPositionZ { get; set; } //26 

        //28
        public short OwnerPositionX { get; set; } // 30 
        public float OwnerPositionY { get; set; } // 32
        public short OwnerPositionZ { get; set; } // 34
        public Vector3 OrientationVector { get; set; } //40
        public float TimeSpent { get; set; } //52
    }

    static class FXCreateGroupItemExtension
    {
        public static FXCreateGroupItem ReadFXCreateGroupItem(this ByteReader reader)
        {
            var data = new FXCreateGroupItem();
            data.TargetNetID = reader.ReadUInt32();
            data.NetAssignedNetID = reader.ReadUInt32();
            data.BindNetID = reader.ReadUInt32();
            data.PositionX = reader.ReadInt16();
            data.PositionY = reader.ReadFloat();
            data.PositionZ = reader.ReadInt16();
            data.TargetPositionX = reader.ReadInt16();
            data.TargetPositionY = reader.ReadFloat();
            data.TargetPositionZ = reader.ReadInt16();
            data.OwnerPositionX = reader.ReadInt16();
            data.OwnerPositionY = reader.ReadFloat();
            data.OwnerPositionZ = reader.ReadInt16();
            data.OrientationVector = reader.ReadVector3();
            data.TimeSpent = reader.ReadFloat();
            return data;
        }
        public static void WriteFXCreateGroupItem(this ByteWriter writer, FXCreateGroupItem data)
        {
            if(data == null)
            {
                data = new FXCreateGroupItem();
            }
            writer.WriteUInt32(data.TargetNetID); //4
            writer.WriteUInt32(data.NetAssignedNetID); //8
            writer.WriteUInt32(data.BindNetID); //12 

            writer.WriteInt16(data.PositionX); //38
            writer.WriteFloat(data.PositionY);  //16
            writer.WriteInt16(data.PositionZ);  //18 
          //  writer.WritePad(2);

            writer.WriteInt16(data.TargetPositionX); //22
            writer.WriteFloat(data.TargetPositionY); //24
            writer.WriteInt16(data.TargetPositionZ); //26
          //  writer.WritePad(2);

            writer.WriteInt16(data.OwnerPositionX); //30
            writer.WriteFloat(data.OwnerPositionY); //32
            writer.WriteInt16(data.OwnerPositionZ); //34

         //   writer.WritePad(2);

      



            writer.WriteVector3(data.OrientationVector); //40

         //   writer.WritePad(6);
            writer.WriteFloat(data.TimeSpent); //52
        }
    }
}
