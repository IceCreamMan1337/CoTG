namespace CoTGEnumNetwork.Enums
{
    public enum SpellSlotType
    {
        //SpellSlots,
        //SummonerSpellSlots = 4,
        //InventorySlots = 6,
        //BluePillSlot = 13,
        //TempItemSlot = 14,
        //RuneSlots = 15,
        //ExtraSlots = 45,
        //RespawnSpellSlot = 61,
        //UseSpellSlot = 62,
        //PassiveSpellSlot = 63,
        //BasicAttackNormalSlots = 64,
        //// TODO: Verify if we need this
        //BasicAttackCriticalSlots = 73

        /**
         * 
         * AVATARSPELLS_NUMB_OF = 0x2, 2 avatar spells
         * AVATARITEMSLOTS_NUMB_OF = 0x1E, 30 item slots?
         * 64 BUFF SLOTS
         * 
         * NUM_INVENTORY_SLOTS_AND_AVATAR = 0x26(38), 2 avatar spells + 30 avatar item slots + 6 inventory
         */

        // Fix126

        // Offsets, should be confirmed
        SpellSlots = 0x0,
        InventorySlots = 0x4,
        BluePillSlot = 0x10,
        TempItemSlot = 0x11,
        SummonerSpellSlots = 0x14,

        RespawnSpellSlot = 0x12,

        UseSpellSlot = 0x15,
        AvatarItemSlotStart = 0x16,




        ExtraSlots = 0x2A,//0x2A = normally , but it crash 

        // YET TO CONFIRM

        TotalInventorySlots = 0x8,
        InventorySlotsAndAvatar = 0x26,
        SlotsWithCastBits = 0xC,
        MaxSpellbookSlots = 0x34,
        BasicAttackSlots = 0x40,
        HitAttackSlots = 0x48,
        PassiveSpellSlot = 63

        /* SpellSlots,
         SummonerSpellSlots = 4,
         InventorySlots = 50,
         BluePillSlot = 6,
         TempItemSlot = 7,
         RuneSlots = 12,
         ExtraSlots = 20, // normally 42 but crash 
         RespawnSpellSlot = 8,
         UseSpellSlot = 9,
         PassiveSpellSlot = 10,
         BasicAttackSlots = 0x40,
         BasicAttackNormalSlots = 64,*/
    }
}