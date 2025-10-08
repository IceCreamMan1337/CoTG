using System.Collections.Generic;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer
{
    // TODO: refactor this class

    /// <summary>
    /// Class who permit rapidly find an spell in function of his name
    /// </summary>
    public class SpellManager
    {
        /// <summary>
        /// disctionary with all spell 
        /// </summary>
        private Dictionary<string, Spell> _spells = new();

        /// <summary>
        /// add in dictionarry
        /// </summary>
        /// <param name="spell"></param>
        public void AddSpell(Spell spell)
        {
            if (!_spells.ContainsKey(spell.Name))
            {
                _spells[spell.Name] = spell;
            }
        }

        /// <summary>
        /// find spellbyname
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Spell? GetSpellByName(string name)
        {
            if (_spells.TryGetValue(name, out Spell spell))
            {
                return spell;
            }
            return null; // Retourne null si le sort n'existe pas
        }
    }
}
