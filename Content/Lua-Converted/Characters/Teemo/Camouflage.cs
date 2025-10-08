namespace Buffs
{
    public class Camouflage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Camouflage",
            BuffTextureName = "Teemo_Camouflage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };

        private Vector3 lastPosition;

        public override void OnActivate()
        {
            AddCamouflageCheckBuff();
            lastPosition = GetUnitPosition(owner);
        }

        public override void OnUpdateActions()
        {
            if (HasMoved() || ShouldRefreshCamouflageCheck())
            {
                AddCamouflageCheckBuff();
            }

            if (CanEnterStealth())
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageStealth(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0.1f, true, false, false);
            }
        }

        private bool HasMoved()
        {
            Vector3 currentPosition = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(currentPosition, lastPosition);

            if (distance != 0)
            {
                lastPosition = currentPosition;
                return true;
            }
            return false;
        }

        private bool ShouldRefreshCamouflageCheck()
        {
            return GetInvulnerable(owner) || IsDead(owner) ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0 ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannel)) > 0 ||
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.SummonerTeleport)) > 0;
        }

        private bool CanEnterStealth()
        {
            return GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageCheck)) == 0 &&
                   GetBuffCountFromCaster(owner, owner, nameof(Buffs.CamouflageStealth)) == 0;
        }

        private void AddCamouflageCheckBuff()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }

        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth || (!spellVars.CastingBreaksStealth && !spellVars.DoesntTriggerSpellCasts))
            {
                AddCamouflageCheckBuff();
            }
        }

        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_DEFAULT)
            {
                AddCamouflageCheckBuff();
            }
        }

        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource, ref HitResult hitResult)
        {


            if (target is not ObjAIBase)
            {
                AddCamouflageCheckBuff();
            }
        }

        public override void OnLaunchAttack(AttackableUnit target)
        {
            AddCamouflageCheckBuff();
        }
    }
}