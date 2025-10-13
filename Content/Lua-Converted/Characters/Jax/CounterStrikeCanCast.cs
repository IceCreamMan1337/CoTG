namespace Buffs
{
    public class CounterStrikeCanCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = ["",],
            BuffName = "Counter Strike Can Cast",
            BuffTextureName = "Armsmaster_Disarm.dds",
            NonDispellable = true,
        };

        private Particle readyEffect;
        private Particle dodgedEffect;
        private bool isCooledDown;

        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);

            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);

            if (cooldown <= 0)
            {
                SpellEffectCreate(out readyEffect, out _, "CounterStrike_ready.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                isCooledDown = true;
            }
            else
            {
                SpellEffectCreate(out dodgedEffect, out _, "CounterStrike_dodged.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                isCooledDown = false;
            }
        }

        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);

            if (isCooledDown)
            {
                SpellEffectRemove(readyEffect);
            }
            else
            {
                SpellEffectRemove(dodgedEffect);
            }
        }

        public override void OnUpdateStats()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);

            if (!isCooledDown)
            {
                float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    isCooledDown = true;
                    SpellEffectRemove(dodgedEffect);
                    SpellEffectCreate(out readyEffect, out _, "CounterStrike_ready.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
            }
        }
    }
}
namespace PreLoads
{
    public class CounterStrikeCanCast : IPreLoadScript
    {
        public void Preload()
        {
            PreloadParticle("counterstrike_ready.troy");
            PreloadParticle("counterstrike_dodged.troy");
        }
    }
}