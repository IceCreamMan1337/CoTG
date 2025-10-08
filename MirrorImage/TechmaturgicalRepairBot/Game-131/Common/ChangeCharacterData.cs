using System;
using MirrorImage;
namespace 
TechmaturgicalRepairBot.Game.Common
{
    public class CharacterStackData
    {
        public string SkinName { get; set; }
        public uint SkinID { get; set; }
        public bool OverrideSpells { get; set; }
        public bool ModelOnly { get; set; }
        public bool ReplaceCharacterPackage { get; set; }
        public uint ID { get; set; }
    }
}