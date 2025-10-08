namespace Buffs
{
    public class CamouflageStealth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CamouflageStealth",
            BuffTextureName = "Teemo_Camouflage.dds",
        };

        private Fade fadeID;
        private Vector3 lastPosition;

        public override void OnActivate()
        {
            SetStealthed(owner, true);
            fadeID = PushCharacterFade(owner, 0.3f, 0.2f);
            lastPosition = GetUnitPosition(owner);
        }

        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            PopCharacterFade(owner, fadeID);
            AddBuff((ObjAIBase)owner, owner, new Buffs.CamouflageCheck(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, target, new Buffs.CamouflageBuff(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }

        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            Vector3 currentPosition = GetUnitPosition(owner);
            float distanceMoved = DistanceBetweenPoints(currentPosition, lastPosition);

            if (distanceMoved > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }

        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth || (!spellVars.CastingBreaksStealth && !spellVars.DoesntTriggerSpellCasts))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}