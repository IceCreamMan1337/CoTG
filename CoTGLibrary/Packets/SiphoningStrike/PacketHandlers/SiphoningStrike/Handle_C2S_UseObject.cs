using CoTGEnumNetwork.Enums;
using CoTGEnumNetwork.Packets.Handlers;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using SiphoningStrike.Game;

namespace CoTG.CoTGServer.Packets.PacketHandlers
{
    public class Handle_C2S_UseObject : PacketHandlerBase<C2S_UseObject>
    {
        public override bool HandlePacket(int userId, C2S_UseObject req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            var target = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;

            champion.SetSpell(target.CharData.HeroUseSpell, (byte)SpellSlotType.UseSpellSlot, true, isitemuse: true);

            var s = champion.Spells[(short)SpellSlotType.UseSpellSlot];
            var ownerCastingSpell = champion.CastSpell;

            // Instant cast spells can be cast during other spell casts.
            /*
            if (s != null && champion.CanCast(s)
                && champion.ChannelSpell == null
                && (ownerCastingSpell == null
                || (ownerCastingSpell != null
                    && s.SpellData.Flags.HasFlag(SpellDataFlags.InstantCast))
                    && !ownerCastingSpell.SpellData.CantCancelWhileWindingUp))
            */
            {
                s.TryCast(target, target.Position3D, target.Position3D);
                return true;
            }

            //return false;
        }
    }
}
