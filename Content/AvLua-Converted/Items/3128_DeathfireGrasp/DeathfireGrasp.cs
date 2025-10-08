namespace Spells
{
    public class DeathfireGrasp : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };

        private const float CooldownTime = 60f;
        private const int ExtraSlotIndex = 7;

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            // Set the DeathfireGraspSpell in the extra spell slot
            SetSpell(owner, ExtraSlotIndex, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathfireGraspSpell));

            // Cast the DeathfireGraspSpell
            SpellCast(owner, target, target.Position3D, target.Position3D, ExtraSlotIndex, SpellSlotType.ExtraSlots, 1, true, true, false);

            // Iterate over inventory slots to find Deathfire Grasp and set its cooldown
            for (int slotIndex = 0; slotIndex < 6; slotIndex++)
            {
                if (GetSlotSpellName(owner, slotIndex, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots) == nameof(Spells.DeathfireGrasp))
                {
                    SetSlotSpellCooldownTimeVer2(CooldownTime, slotIndex, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                }
            }
        }
    }
}

namespace Buffs
{
    public class DeathfireGrasp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deathfire Grasp",
            BuffTextureName = "055_Borses_Staff_of_Apocalypse.tga",
        };

        private const float CooldownReduction = 0.15f;

        public override void OnActivate()
        {
            ApplyCooldownReduction(-CooldownReduction);
        }

        public override void OnDeactivate(bool expired)
        {
            ApplyCooldownReduction(CooldownReduction);
        }

        private void ApplyCooldownReduction(float amount)
        {
            IncPermanentPercentCooldownMod(owner, amount);
        }
    }
}