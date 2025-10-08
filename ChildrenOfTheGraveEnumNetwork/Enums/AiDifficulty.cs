namespace ChildrenOfTheGraveEnumNetwork.Enums;

public enum EntityDiffcultyType
{
    NEWBIE = 0x0,
    INTERMEDIATE = 0x1,
    ADVANCED = 0x2,
    UBER = 0x3,
    TUTORIAL = 0x4,
    ENTITYBASE = 0x5,
    NUMTYPE = 0x6,
    UNKNOWN = -0x2,

    //To not break scripts
    DIFFICULTY_NEWBIE = 0x0,
    DIFFICULTY_INTERMEDIATE = 0x1,
    DIFFICULTY_ADVANCED = 0x2,
    DIFFICULTY_UBER = 0x3,
    NONE = 0x4 //-0x02?
}
