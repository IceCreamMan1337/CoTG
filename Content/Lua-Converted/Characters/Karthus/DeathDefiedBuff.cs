namespace Buffs
{
    //This script was manually fidled with, it is not an accurate to the original lua script.
    public class DeathDefiedBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Death Defied Buff",
            BuffTextureName = "Lich_Defied.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private Particle particle;
        private float cost0;
        private float cost2;
        private float cost3;

        private readonly int[] parCostEffect0 = { -20, -26, -32, -38, -44 };
        private readonly int[] parCostEffect1 = { -30, -42, -54, -66, -78 };
        private readonly int[] parCostEffect2 = { -150, -175, -200 };
        private readonly int[] parCostEffect3 = { 20, 26, 32, 38, 44 };
        private readonly int[] parCostEffect4 = { 30, 42, 54, 66, 78 };
        private readonly int[] parCostEffect5 = { 150, 175, 200 };

        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            // Allow buffs only from the same team or if the type is internal
            return owner.Team == attacker.Team || type == BuffType.INTERNAL;
        }

        public override void OnActivate()
        {
            SealAllSpellSlots();

            SetBasicProperties(false);
            RemoveAllDebuffs();

            CreateParticleEffect();
            StopOwnerChanneling();
            AdjustSpellParCosts();

            IncPAR(owner, 5000, PrimaryAbilityResourceType.MANA);
        }

        public override void OnDeactivate(bool expired)
        {
            ForceDead(owner);
            SetTargetable(owner, true);
            UnsealAllSpellSlots();

            RemoveParticleEffect();
            ShowHealthBar(owner, true);
            SpellBuffRemove(owner, nameof(Buffs.Defile), (ObjAIBase)owner, 0);
            ResetParCosts();
        }

        public override void OnUpdateStats()
        {
            SetBasicProperties(false);

            if (lifeTime >= 3.25f)
            {
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount = 0;
        }

        public override void OnLevelUpSpell(int slot)
        {
            AdjustParCostForSlot(slot);
        }

        private void SealAllSpellSlots()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            for (int i = 0; i < 6; i++)
            {
                SealSpellSlot(i, SpellSlotType.InventorySlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }

        private void UnsealAllSpellSlots()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            for (int i = 0; i < 6; i++)
            {
                SealSpellSlot(i, SpellSlotType.InventorySlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }

        private void SetBasicProperties(bool canAct)
        {
            SetCanAttack(owner, canAct);
            SetCanMove(owner, canAct);
            SetTargetable(owner, canAct);
        }

        private void RemoveAllDebuffs()
        {
            foreach (BuffType debuff in Enum.GetValues(typeof(BuffType)))
            {
                //SpellBuffRemoveType(owner, debuff);
            }
        }

        private void CreateParticleEffect()
        {
            SpellEffectCreate(out particle, out _, "mordekeiser_cotg_skin.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }

        private void RemoveParticleEffect()
        {
            SpellEffectRemove(particle);
        }

        private void StopOwnerChanneling()
        {
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
        }

        private void AdjustSpellParCosts()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                cost0 = parCostEffect0[level - 1];
                SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, cost0, PrimaryAbilityResourceType.MANA);
            }

            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -100, PrimaryAbilityResourceType.MANA);
            }

            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                cost0 = parCostEffect1[level - 1];
                SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, cost0, PrimaryAbilityResourceType.MANA);

                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Defile(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }

                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }

            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                float cost3 = parCostEffect2[level - 1];
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, cost3, PrimaryAbilityResourceType.MANA);
            }
        }

        private void ResetParCosts()
        {
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }

        private void AdjustParCostForSlot(int slot)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, slot, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float costInc;

            switch (slot)
            {
                case 0:
                    cost0 = parCostEffect3[level - 1];
                    costInc = cost0 * -1;
                    SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
                    break;
                case 2:
                    cost2 = parCostEffect4[level - 1];
                    costInc = cost2 * -1;
                    SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
                    break;
                case 3:
                    cost3 = parCostEffect5[level - 1];
                    costInc = cost3 * -1;
                    SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, costInc, PrimaryAbilityResourceType.MANA);
                    break;
            }
        }
    }
}