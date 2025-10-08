/*namespace Buffs
{
  /*  internal class AscWarpTarget : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.COMBAT_ENCHANCER,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };

        public override StatsModifier StatsModifier { get; protected set; }

        Particle p1;
        public override void OnActivate()
        {
            Target.IconInfo.ChangeBorder("Teleport", "ascwarptarget");
            Target.IconInfo.ChangeIcon("NoIcon");
            p1 = AddParticleTarget(Owner, "global_asc_teleport_target", Target, lifetime: -1);
        }

        public override void OnDeactivate()
        {
            RemoveParticle(p1);
            Target.IconInfo.ResetBorder();
            TeleportTo(Owner, Target.Position);
            if (Owner is Champion ch)
            {
                TeleportCamera(ch, Target);
            }
            Target.Die(CreateDeathData(false, 3, Target, Target, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_RAW, 0.0f));
            Owner.Spells[6 + (byte)SpellSlotType.InventorySlots].SetCooldown(float.MaxValue);
        }
    }
}*/