namespace CoTGEnumNetwork.Enums
{
    public enum PrimaryAbilityResourceType : byte
    {
        MANA = 0,
        Energy = 1,
        None = 2,//Souls
        Shield = 3,
        Other = 4,
        //hack for other who doesn't exist on .126 
        Battlefury = 12,
        Dragonfury = 5,
        Rage = 6,
        Heat = 7,
        Gnarfury = 8,
        Ferocity = 9,
        BloodWell = 10,
        Wind = 11

    }
}