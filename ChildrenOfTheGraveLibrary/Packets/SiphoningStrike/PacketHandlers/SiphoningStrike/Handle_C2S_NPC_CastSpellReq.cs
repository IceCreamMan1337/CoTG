using ChildrenOfTheGraveEnumNetwork;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGraveEnumNetwork.Packets.Handlers;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;
using SiphoningStrike.Game;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Packets.PacketHandlers
{
    public class Handle_C2S_NPC_CastSpellReq : PacketHandlerBase<C2S_NPC_CastSpellReq>
    {
        public override bool HandlePacket(int userId, C2S_NPC_CastSpellReq req)
        {
            AttackableUnit targetUnit = Game.ObjectManager.GetObjectById(req.TargetNetID) as AttackableUnit;
            Champion owner = Game.PlayerManager.GetPeerInfo(userId).Champion;
            if (owner is not null)
            {
                //Spell spell = owner.Spells[req.Slot];
                // Fix126

                Spell spell = owner.Spells[req.IsSummonerSpellSlot ? req.Slot + (int)SpellSlotType.SummonerSpellSlots : req.Slot];
                if (spell is not null)
                {
                    owner.FaceDirection((req.EndPosition - req.Position).Normalized(), true);  //we reactualise direction of player when he cast an spell
                    return TryCast(spell, req, owner, targetUnit);
                }
            }

            return false;
        }

        public bool TryCast(Spell s, C2S_NPC_CastSpellReq req, Champion owner, AttackableUnit targetUnit)
        {
            if (s.TryCast(targetUnit, req.Position, req.EndPosition))
            {
                owner.ItemInventory.ClearUndoHistory();
                //TODO: Move to Spell.TryCast?
                if (s.IsItem)
                {
                    var item = s.Caster.ItemInventory.GetItem(s.SpellName);
                    if (item != null && item.ItemData.Consumed)
                    {
                        var inventory = owner.ItemInventory;
                        inventory.RemoveItem(inventory.GetItemSlot(item));
                    }
                }
                return true;
            }

            return false;
        }
    }
}
