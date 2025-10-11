namespace Buffs
{
    public class Teleport_DeathRemoval : BuffScript
    {
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string name = GetSpellName(spell);
            if (name == nameof(Spells.TeleportCancel))
            {
                ObjAIBase caster = GetBuffCasterUnit();
                if (caster is LaneTurret)
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Turret), (ObjAIBase)owner);
                }
                else
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Target), (ObjAIBase)owner);
                }
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (caster is ObjAIBase)
            {
                if (caster is LaneTurret)
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Turret), (ObjAIBase)owner);
                }
                else
                {
                    SpellBuffRemove(caster, nameof(Buffs.Teleport_Target), (ObjAIBase)owner);
                }
            }
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace PreLoads
{
    public class Teleport_DeathRemoval : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("teleport_turret");
            PreloadSpell("teleport_target");
        }
    }
}