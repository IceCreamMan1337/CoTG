using System.Collections;
using System.Collections.Generic;
using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS
{
    public class SpellInventory : IEnumerable<Spell>
    {
        private ObjAIBase _owner;
        private Spell[] _spells = new Spell[82];

        public Spell this[int slot]
        {
            get
            {
                return _spells[slot];
            }
            set
            {
                _spells[slot] = value;
            }
        }

        public int Count => _spells.Length;

        public SpellInventory(ObjAIBase owner)
        {
            _owner = owner;
        }

        public Spell Q => _spells[(short)SpellSlotType.SpellSlots + 0];

        public Spell W => _spells[(short)SpellSlotType.SpellSlots + 1];

        public Spell E => _spells[(short)SpellSlotType.SpellSlots + 2];

        public Spell R => _spells[(short)SpellSlotType.SpellSlots + 3];

        public Spell D => _spells[(short)SpellSlotType.SummonerSpellSlots + 0];

        public Spell F => _spells[(short)SpellSlotType.SummonerSpellSlots + 1];

        public SpellIndexer Inventory => new(_owner, SpellSlotType.InventorySlots);

        // public Spell Trinket => _spells[(short)SpellSlotType.InventorySlots + 6];

        public Spell BluePill => _spells[(short)SpellSlotType.BluePillSlot];

        public Spell TempItem => _spells[(short)SpellSlotType.TempItemSlot];

        // Fix126
        // public SpellIndexer Rune => new(_owner, SpellSlotType.RuneSlots);

        public SpellIndexer Extra => new(_owner, SpellSlotType.ExtraSlots);

        public Spell Respawn => _spells[(short)SpellSlotType.RespawnSpellSlot];

        public Spell Use => _spells[(short)SpellSlotType.UseSpellSlot];

        // Fix126
        public Spell Passive => _spells[(short)SpellSlotType.PassiveSpellSlot];

        // Fix126
        public SpellIndexer BasicAttack => new(_owner, SpellSlotType.BasicAttackSlots);

        // Fix126
        //public SpellIndexer BasicAttackCritical => new(_owner, SpellSlotType.BasicAttackCriticalSlots);

        public IEnumerator<Spell> GetEnumerator()
        {
            return ((IEnumerable<Spell>)_spells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public readonly struct SpellIndexer
        {
            public readonly ObjAIBase Owner;
            public readonly SpellSlotType SlotType;
            public Spell this[int index] => Owner.Spells[(short)(SlotType + index)];

            public SpellIndexer(ObjAIBase owner, SpellSlotType slotType)
            {
                Owner = owner;
                SlotType = slotType;
            }
        }
    }
}