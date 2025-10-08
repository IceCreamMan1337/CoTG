using CoTGEnumNetwork.Packets.Handlers;
using CrystalSlash.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_NPC_UpgradeSpellReq_106 : PacketHandlerBase<C2S_NPC_UpgradeSpellReq>
    {
        public override bool HandlePacket(int userId, C2S_NPC_UpgradeSpellReq req)
        {
            // TODO: Check if can up skill

            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
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
