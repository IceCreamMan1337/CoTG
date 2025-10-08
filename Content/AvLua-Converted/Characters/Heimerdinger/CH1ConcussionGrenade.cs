namespace Spells
{
    public class HeimerdingerE: CH1ConcussionGrenade {}
    public class CH1ConcussionGrenade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            int level = GetSpellLevelPlusOne(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
        //    Minion other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, false, true, true, 0, default, false, (Champion)attacker);
            Minion other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, false, true, true, 0, default, true, (Champion)attacker);
            //SetNoRender(other2, true);
            other2.SetIsTargetableToTeam(teamID, true);
            //hack

         

            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.CH1ConcussionGrenadeSpell));
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.CH1ConcussionGrenadeUpgrade));
          
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                SpellCast(owner, other2, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
               
            }
            else
            {
                SpellCast(owner, other2, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
              
            }
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CH1ConcussionGrenade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
        };
    }
}