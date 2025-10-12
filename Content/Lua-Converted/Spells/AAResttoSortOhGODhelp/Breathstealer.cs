namespace Spells
{
    public class Breathstealer : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            string[] slotNames = new string[6];
            for (int i = 0; i < 6; i++)
            {
                slotNames[i] = GetSlotSpellName(owner, i, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
                if (slotNames[i] == nameof(Spells.Breathstealer))
                {
                    SetSlotSpellCooldownTimeVer2(90, i, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                }
            }

            SetSpell(owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BreathstealerSpell));
            Vector3 targetPos = GetUnitPosition(target);
            FaceDirection(owner, targetPos);
            SpellCast(owner, target, target.Position3D, target.Position3D, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}

namespace Buffs
{
    public class Breathstealer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, null, "head" },
            AutoBuffActivateEffect = new[] { "Summoner_Banish.troy", null, "Global_miss.troy" },
            BuffName = "",
            BuffTextureName = "",
        };

        public override void OnActivate()
        {
            AdjustCooldownMod(-0.15f);
        }

        public override void OnDeactivate(bool expired)
        {
            AdjustCooldownMod(0.15f);
        }

        private void AdjustCooldownMod(float value)
        {
            IncPermanentPercentCooldownMod(owner, value);
        }
    }
}
namespace PreLoads
{
    public class Breathstealer : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("breathstealerspell");
        }
    }
}