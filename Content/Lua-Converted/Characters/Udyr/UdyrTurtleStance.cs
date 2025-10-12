namespace Spells
{
    public class UdyrTurtleStance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 100, 140, 180, 220 };
        public override void SelfExecute()
        {
            //hack: added an clear for avoid missthetiming buff 
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                // SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), owner);
                SpellBuffClear(owner, nameof(Buffs.UdyrBearStance));
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) > 0)
            {
                // SpellBuffRemove(owner, nameof(Buffs.UdyrTigerStance), owner);
                SpellBuffClear(owner, nameof(Buffs.UdyrTigerStance));
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrPhoenixStance)) > 0)
            {
                // SpellBuffRemove(owner, nameof(Buffs.UdyrPhoenixStance), owner);
                SpellBuffClear(owner, nameof(Buffs.UdyrPhoenixStance));
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleActivation)) > 0)
            {
                // SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleActivation), owner);
                SpellBuffClear(owner, nameof(Buffs.UdyrTurtleActivation));
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            float currentCD = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            AddBuff(owner, owner, new Buffs.UdyrTurtleStance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            float aPAmount = GetFlatMagicDamageMod(owner);
            aPAmount *= 0.5f;
            float shieldAmount = effect0[level - 1];
            shieldAmount += aPAmount;
            float nextBuffVars_ShieldAmount = shieldAmount;
            AddBuff(owner, owner, new Buffs.UdyrTurtleActivation(nextBuffVars_ShieldAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UdyrTurtleStance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTurtleStance",
            BuffTextureName = "Udyr_TurtleStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
        int casterID; // UNUSED
        Particle turtle;
        Particle turtleparticle;
        public override void OnActivate()
        {
            casterID = PushCharacterData("UdyrTurtle", owner, false);
            SpellEffectCreate(out turtle, out _, "turtlepelt.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            SpellEffectCreate(out turtleparticle, out _, "TurtleStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(turtle);
            SpellEffectRemove(turtleparticle);
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            float critMod = GetFlatCritChanceMod(owner);
            critMod *= -1;
            critMod += charVars.BaseCritChance;
            IncFlatCritChanceMod(owner, critMod);
        }
    }
}
namespace PreLoads
{
    public class UdyrTurtleStance : IPreLoadScript
    {
        public void Preload()
        {
            PreloadCharacter("udyrturtle");
            PreloadParticle("turtlepelt.troy");
            PreloadParticle("turtlestance.troy");
            PreloadSpell("udyrbearstance");
            PreloadSpell("udyrtigerstance");
            PreloadSpell("udyrphoenixstance");
            PreloadSpell("udyrturtleactivation");
        }
    }
}