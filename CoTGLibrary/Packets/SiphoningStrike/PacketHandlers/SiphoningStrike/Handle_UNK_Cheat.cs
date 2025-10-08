namespace CoTG.CoTGServer.Packets.PacketHandlers
{
#if DEBUG_AB || RELEASE_AB
    public class Handle_UNK_Cheat : PacketHandlerBase<UNK_Cheat>
    {
        public override bool HandlePacket(int userId, UNK_Cheat req)
        {
            switch (req.IDCheat)
            {
                case CheatID.Cheat_IncTime:
                    // Augmenter la fréquence de 5 Hz
                    Game.Time.AdjustHz(5);
                    break;

                case CheatID.Cheat_DecTime:
                    // Diminuer la fréquence de 5 Hz
                    Game.Time.AdjustHz(-5);
                    break;

                // Vous pouvez ajouter d'autres cas pour des ajustements plus fins
             /*   case CheatID.Cheat_ResetTime:
                    // Remettre à la fréquence de base
                    Game.Time.SetTicksPerSecond(Game.Time.BaseHz);
                    break;*/

            }
            return true;
        }
    }
#endif
}