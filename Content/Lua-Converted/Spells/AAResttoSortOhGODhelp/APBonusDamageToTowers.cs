namespace Buffs
{
    //A lot of logic that wasn't here originally, nor was supposed to
    public class APBonusDamageToTowers : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "APBonusDamageToTowers",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private float lastTimeExecuted;

        public override void OnDisconnect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DisconnectTimer(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }

        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (avatarVars.MasteryJuggernaut && owner.Team != attacker.Team && IsCrowdControlType(type))
            {
                const float cCreduction = 0.9f;
                duration *= cCreduction;
            }
            return true;
        }

        public override void OnActivate()
        {
            CheckAndAddFortifyBuff(0);
            CheckAndAddFortifyBuff(1);
        }

        public override void OnUpdateStats()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (avatarVars.MasteryInitiate && healthPercent > 0.7f)
            {
                IncPercentMovementSpeedMod(owner, avatarVars.MasteryInitiateAmt);
            }
        }

        public override void OnUpdateActions()
        {
            if (avatarVars.MasterySeigeCommander && ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                ApplySiegeCommanderDebuff();
            }
        }

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is LaneTurret)
            {
                CalculateTowerDamage(ref damageAmount);
            }
            else if (target is not ObjAIBase)
            {
                CalculateNonChampionDamage(ref damageAmount);
            }
            ApplyButcherBonus(ref damageAmount, target);
        }

        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if (avatarVars.MasteryBladedArmor && IsNonChampionObjAI(attacker))
            {
                ReflectDamageToAttacker(attacker);
            }
        }

        private void CheckAndAddFortifyBuff(int slot)
        {
            string fortifyCheck = GetSlotSpellName((ObjAIBase)owner, slot, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (fortifyCheck == nameof(Spells.SummonerFortify))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 3, true, false, false);
            }
        }

        private void ApplySiegeCommanderDebuff()
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, default, true))
            {
                AddBuff(attacker, unit, new Buffs.MasterySiegeCommanderDebuff(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }

        private void CalculateTowerDamage(ref float damageAmount)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float abilityDamageToAdd = abilityPower / 2.5f;
            float bonusAttackPower = GetFlatPhysicalDamageMod(owner);
            if (bonusAttackPower <= abilityDamageToAdd)
            {
                damageAmount = damageAmount - bonusAttackPower + abilityDamageToAdd;
            }
            if (avatarVars.MasteryDemolitionist)
            {
                damageAmount += avatarVars.MasteryDemolitionistAmt;
            }
        }

        private void CalculateNonChampionDamage(ref float damageAmount)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float abilityDamageToAdd = abilityPower / 2.5f;
            float bonusAttackPower = GetFlatPhysicalDamageMod(owner);
            if (bonusAttackPower <= abilityDamageToAdd)
            {
                damageAmount = damageAmount - bonusAttackPower + abilityDamageToAdd;
            }
            if (avatarVars.MasteryDemolitionist)
            {
                damageAmount += avatarVars.MasteryDemolitionistAmt;
            }
        }

        private void ApplyButcherBonus(ref float damageAmount, AttackableUnit target)
        {
            if (avatarVars.MasteryButcher && target is ObjAIBase && target is not LaneTurret && target is not Champion)
            {
                damageAmount += avatarVars.MasteryButcherAmt;
            }
        }

        private void ReflectDamageToAttacker(ObjAIBase attacker)
        {
            ApplyDamage((ObjAIBase)owner, attacker, avatarVars.MasteryBladedArmorAmt, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_REACTIVE, 1, 0, 0, false, false, (ObjAIBase)owner);
        }

        private bool IsCrowdControlType(BuffType type)
        {
            return type is BuffType.SNARE or BuffType.SLOW or BuffType.FEAR or BuffType.CHARM or BuffType.SLEEP or BuffType.STUN or BuffType.TAUNT;
        }

        private bool IsNonChampionObjAI(ObjAIBase obj)
        {
            return obj is ObjAIBase && obj is not LaneTurret && obj is not Champion;
        }

        //Commented out this section of logic that doesn't actually exist.
        //I don't know why is it here, but it's throwing off the exp additions for kills

        //public override void OnKill(AttackableUnit target)
        //{
        //    if (target is Champion)
        //    {
        //        GrantKillRewards();
        //    }
        //}

        //public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        //{
        //    if (target is Champion)
        //    {
        //        GrantAssistRewards();
        //    }
        //}

        //This doesn't exist
        //private void GrantKillRewards()
        //{
        //    int expAmount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0 ? 20 : 40;
        //    IncExp(owner, expAmount);

        //    if (avatarVars.MasteryBounty)
        //    {
        //        float goldAmount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0 ? avatarVars.MasteryBountyAmt / 2 : avatarVars.MasteryBountyAmt;
        //        IncGold(owner, goldAmount);
        //    }
        //}

        //private void GrantAssistRewards()
        //{
        //    int expAmount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0 ? 20 : 40;
        //    IncExp(owner, expAmount);

        //    if (avatarVars.MasteryBounty)
        //    {
        //        float goldAmount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0 ? avatarVars.MasteryBountyAmt / 2 : avatarVars.MasteryBountyAmt;
        //        IncGold(owner, goldAmount);
        //    }
        //}
    }
}
namespace PreLoads
{
    public class APBonusDamageToTowers : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("disconnecttimer");
            PreloadSpell("fortifycheck");
        }
    }
}