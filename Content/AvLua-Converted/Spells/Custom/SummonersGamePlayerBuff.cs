using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using System.Linq;

namespace Buffs
{
    public class SummonersGamePlayerBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinPlayerBuff",
            BuffTextureName = "CrystalScarsAura.dds",
            PersistsThroughDeath = true,
        };

        private readonly string[] Allchamp = {
    "Akali", "Alistar", "Amumu", "Annie", "Blitzcrank", "Brand", "Caitlyn", "Cassiopeia", "Corki",
    "Ezreal", "FiddleSticks", "Galio", "Gangplank", "Garen", "Irelia", "Janna", "Kassadin", "Kayle",
    "Kennen", "Kog'Maw", "Leona", "Lux", "Malzahar", "Maokai", "MissFortune", "Mordekaiser", "Morgana",
    "Nunu", "Renekton", "Ryze", "Shen", "Singed", "Sion", "Soraka", "Swain", "Talon", "Taric",
    "Tristana", "Trundle", "Tryndamere", "Udyr", "Vladimir", "Warwick", "Xin Zhao", "Anivia", "Ashe",
    "Dr.Mundo", "Evelynn", "Gragas", "Heimerdinger", "Jarvan IV", "Karma", "Karthus", "LeeSin",
    "Malphite", "MasterYi", "Nasus", "Nidalee", "Nocturne", "Olaf", "Pantheon", "Rammus", "Rumble",
    "Skarner", "Sona", "Teemo", "TwistedFate", "Twitch", "Urgot", "Vayne", "Veigar", "Xerath",
    "Yorick", "Zilean", "Cho'Gath", "Katarina", "Leblanc", "Orianna", "Poppy", "Riven", "Shaco",
    "MonkeyKing", "Sivir", "Jax"
};

        int cooldownVar; // UNUSED
        float totalDamageOT;
        float lastTimeExecuted;


        private float originalCooldown0;
        private float originalCooldown1;
        private float originalCooldown2;
        private float originalCooldown3;

        private int cougarID;

        public override void OnActivate()
        {

           // int number =  Randomint(0, Allchamp.Count());


           // cougarID = PushCharacterData2(Allchamp[number], owner, true);

          // SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);

        }

        private void RestoreOriginalCooldowns()
        {
            float adjustedCooldown0 = originalCooldown0 - lifeTime;
            float adjustedCooldown1 = originalCooldown1 - lifeTime;
            float adjustedCooldown2 = originalCooldown2 - lifeTime;
            float adjustedCooldown3 = originalCooldown3 - lifeTime;

            SetSlotSpellCooldownTimeVer2(adjustedCooldown0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(adjustedCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(adjustedCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetSlotSpellCooldownTimeVer2(adjustedCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        
        }

        public override void OnUpdateStats()
        {
            int level = GetLevel(owner); // UNUSED
            float percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            float percentMissing = 1 - percentMana;
            percentMissing *= 2.1f;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
            IncPercentArmorPenetrationMod(owner, 0.12f);
            IncPercentMagicPenetrationMod(owner, 0.05f);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                totalDamageOT *= 0.9f;
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            totalDamageOT += damageAmount;
            if (attacker is Champion)
            {
                float hP_Percent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float damagePercent = totalDamageOT / maxHealth;
                if (hP_Percent <= 0.4f)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreLowHPAttacker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (damagePercent > 0.2f)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreLowHP(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreLowHP(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
          
        }
        public override void OnKill(AttackableUnit target)
        {
            DebugSay(target, "test");
            if (target is Champion)
            {
                SetGameScoreCS(target.Team, 1);

                originalCooldown0 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                originalCooldown1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                originalCooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                originalCooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);


                SpellEffectCreate(out _, out _, "nidalee_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);

                int number = RandomInt(0, Allchamp.Count());


                cougarID = PushCharacterData3(Allchamp[number], owner, true);

                RestoreOriginalCooldowns();

            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            
            
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
          
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal = 0;
            if (health >= 0)
            {
                effectiveHeal = health * 0.8f;
                returnValue = effectiveHeal;
            }
            if (owner.Team == target.Team && target is Champion && target != owner && effectiveHeal >= 30)
            {
              
            }
            return returnValue;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
       
               

            return returnValue;
        }
    }
}