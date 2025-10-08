namespace Spells
{
    public class BloodScent : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 45f, 40f, 35f, 30f, 25f },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "HyenaWarwick" },
        };

        public override void SelfExecute()
        {
            string buffName = nameof(Buffs.BloodScent_internal);
            if (GetBuffCountFromCaster(owner, owner, buffName) > 0)
            {
                SpellBuffRemove(owner, buffName, owner, 0);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.BloodScent_internal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}

namespace Buffs
{
    public class BloodScent : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "" },
            AutoBuffActivateEffect = new[] { "", "", "", "" },
            BuffName = "Haste",
            BuffTextureName = "Wolfman_Bloodscent.dds",
        };

        private float moveSpeedBuff;
        private Particle speedEffect;
        private Particle bloodBuffRightHand;
        private Particle bloodBuffLeftHand;
        private Particle bloodBuffHead;

        private readonly float[] moveSpeedBuffByLevel = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };

        public BloodScent(float moveSpeedBuff = default)
        {
            this.moveSpeedBuff = moveSpeedBuff;
        }

        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);

            SpellEffectCreate(out speedEffect, out _, "wolfman_bloodscent_activate_speed.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out bloodBuffRightHand, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out bloodBuffLeftHand, out _, "wolfman_bloodscent_activate_blood_buff.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out bloodBuffHead, out _, "wolfman_bloodscent_activate_blood_buff_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);

            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 7)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
        }

        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(speedEffect);
            SpellEffectRemove(bloodBuffRightHand);
            SpellEffectRemove(bloodBuffLeftHand);
            SpellEffectRemove(bloodBuffHead);

            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 7)
            {
                StopCurrentOverrideAnimation("Run", owner, false);
                OverrideAnimation("Run", "Run", owner);
            }
        }

        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedBuff);
        }

        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                moveSpeedBuff = moveSpeedBuffByLevel[level - 1];
            }
        }
    }
}