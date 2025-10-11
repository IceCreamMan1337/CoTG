namespace Buffs
{
    public class ChronoRevive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "" },
            BuffName = "Chrono Shift",
            BuffTextureName = "Chronokeeper_Timetwister.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private readonly float healthPlusAbility;

        public ChronoRevive(float healthPlusAbility = default)
        {
            this.healthPlusAbility = healthPlusAbility;
        }

        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);

            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out _, out _, "LifeAura.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);

            // Set a minimum cooldown of 3 seconds if current cooldowns are less than or equal to 6 seconds
            SetMinimumCooldown((ObjAIBase)owner, 0, 6, 3);
            SetMinimumCooldown((ObjAIBase)owner, 1, 6, 3);

            PlayAnimation("Death", 4, owner, false, false, true);
            DisableOwnerCapabilities();

            RemoveNegativeBuffs();
        }

        public override void OnDeactivate(bool expired)
        {
            IncHealth(owner, healthPlusAbility, owner);
            EnableOwnerCapabilities();

            SpellEffectCreate(out _, out _, "GuardianAngel_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);

            PlayIdleAnimation();
        }

        public override void OnUpdateStats()
        {
            DisableOwnerCapabilities();
            IncFlatHPRegenMod(owner, -100);
        }

        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount = 0;
        }

        private void DisableOwnerCapabilities()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
        }

        private void EnableOwnerCapabilities()
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetSuppressCallForHelp(owner, false);
            SetCallForHelpSuppresser(owner, false);
            SetIgnoreCallForHelp(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
        }

        private void RemoveNegativeBuffs()
        {
            BuffType[] debuffs =
            {
                BuffType.POISON, BuffType.SUPPRESSION, BuffType.BLIND, BuffType.COMBAT_DEHANCER,
                BuffType.COMBAT_ENCHANCER, BuffType.STUN, BuffType.INVISIBILITY, BuffType.SILENCE,
                BuffType.TAUNT, BuffType.POLYMORPH, BuffType.SLOW, BuffType.SNARE,
                BuffType.DAMAGE, BuffType.HEAL, BuffType.HASTE, BuffType.SPELL_IMMUNITY,
                BuffType.PHYSICAL_IMMUNITY, BuffType.INVULNERABILITY, BuffType.SLEEP, BuffType.FEAR,
                BuffType.CHARM, BuffType.SHRED
            };

            foreach (var debuff in debuffs)
            {
                SpellBuffRemoveType(owner, debuff);
            }
        }

        private void SetMinimumCooldown(ObjAIBase unit, int slot, float threshold, float minCooldown)
        {
            float currentCooldown = GetSlotSpellCooldownTime(unit, slot, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (currentCooldown <= threshold)
            {
                SetSlotSpellCooldownTime(unit, slot, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, minCooldown);
            }
        }

        private void PlayIdleAnimation()
        {
            UnlockAnimation(owner, false);
            PlayAnimation("idle1", 0, owner, false, false, true);
            UnlockAnimation(owner, false);
        }
    }
}
namespace PreLoads
{
    public class ChronoRevive : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("lifeaura.troy");
            PreloadParticle("guardianangel_tar.troy");
        }
    }
}