namespace ChildrenOfTheGraveEnumNetwork.Packets.Enums
{
    public enum Channel : uint
    {
        /// <summary>
        /// CHL_HANDSHAKE 0  = HANDSHAKE
        /// </summary>
        CHL_HANDSHAKE = 0,
        /// <summary>
        /// CHL_c2c 1 = c2s
        /// </summary>
        CHL_C2S = 1,
        /// <summary>
        /// CHL_GAMEPLAY 2 = gameplay
        /// </summary>
        CHL_GAMEPLAY = 2,
        /// <summary>
        /// CHL_S2C 3 = s2c
        /// </summary>
        CHL_S2C = 3,
        /// <summary>
        /// CHL_LOW_PRIORITY 4 = broadcast unreliable
        /// </summary>
        CHL_LOW_PRIORITY = 4,
        /// <summary>
        /// CHL_COMMUNICATION 5  = chat
        /// </summary>
        CHL_COMMUNICATION = 5,
        /// <summary>
        /// CHL_LOADING_SCREEN 6  = CHL_LOADING_SCREEN
        /// </summary>
        CHL_LOADING_SCREEN = 6,
    }
}