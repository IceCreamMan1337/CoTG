namespace Spells
{
    public class AspectOfTheCougar : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };

        private readonly int[] armorBonusByLevel = { 10, 20, 30 };

        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AspectOfTheCougar)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.AspectOfTheCougar), owner, 0);
            }
            else
            {
                float armorMod = armorBonusByLevel[level - 1];
                AddBuff(attacker, owner, new Buffs.AspectOfTheCougar(armorMod), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}

namespace Buffs
{
    public class AspectOfTheCougar : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AspectOfTheCougar",
            BuffTextureName = "Nidalee_AspectOfTheCougar.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };

        private float armorMod;
        private float originalCooldown0;
        private float originalCooldown1;
        private float originalCooldown2;
        private int cougarID;
        private readonly int[] armorBonusByLevel = { 10, 20, 30 };

        public AspectOfTheCougar(float armorMod = default)
        {
            this.armorMod = armorMod;
        }

        public override void OnActivate()
        {
            originalCooldown0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            originalCooldown1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            originalCooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cougarID = PushCharacterData2("Nidalee_Cougar", owner, true);

            SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);

            ApplyCooldownReduction();
        }

        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Takedown)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Takedown), (ObjAIBase)owner, 0);
            }

            PopCharacterData2(owner, cougarID, true);

            SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);

            ApplyCooldownReduction(false);
            RestoreOriginalCooldowns();
        }

        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
            IncFlatSpellBlockMod(owner, armorMod);
        }

        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                armorMod = armorBonusByLevel[level - 1];
            }
        }

        private void ApplyCooldownReduction(bool isActivate = true)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * 4;

            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, isActivate);
        }

        private void RestoreOriginalCooldowns()
        {
            float adjustedCooldown0 = originalCooldown0 - lifeTime;
            float adjustedCooldown1 = originalCooldown1 - lifeTime;
            float adjustedCooldown2 = originalCooldown2 - lifeTime;

            SetSlotSpellCooldownTimeVer2(adjustedCooldown0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(adjustedCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(adjustedCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}