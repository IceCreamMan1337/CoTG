using CoTGEnumNetwork.Packets.Handlers;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_NPC_UpgradeSpellReq : PacketHandlerBase<C2S_NPC_UpgradeSpellReq>
    {
        public override bool HandlePacket(int userId, C2S_NPC_UpgradeSpellReq req)
        {
            // TODO: Check if can up skill

            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            //  Console.WriteLine(Game.PlayerManager.GetPeerInfo(userId).PlayerId);
            //  Console.WriteLine(Game.PlayerManager.GetPeerInfo(userId).Name);
            //  Console.WriteLine(Game.PlayerManager.GetPeerInfo(userId).Champion.Name);
            // Normal level-up:
            if (champion.LevelUpSpell(req.Slot))
            {
                return true;
            }
            // Game.PacketNotifier.NotifyNPC_UpgradeSpellAns(userId, champion.NetId, req.Slot, s.Level, champion.Experience.SpellTrainingPoints.TrainingPoints);

            return false;
            //// Evolve Request:
            //else
            //{
            //    champion.EvolveSpell(req.Slot, champion.Spells[req.Slot].Script.ScriptMetadata.SpellEvolveDesc);
            //    return true;
            //}
        }
    }
}
