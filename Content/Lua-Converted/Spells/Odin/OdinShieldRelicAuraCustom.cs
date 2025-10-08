using CoTGEnumNetwork;

namespace Buffs
{
    public class OdinShieldRelicAuraCustom : BuffScript
    {
        //hack full custom buff for map22 ( custom magma chamber ) 
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle buffParticle;
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {


            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            if (owner.Position.Distance(new Vector2(4533, 4668)) <= 50 || owner.Position.Distance(new Vector2(5100, 11582)) <= 50)
            {
                SpellEffectCreate(out buffParticle, out _, "dr_mundo_burning_agony_cas_02.troy", "dr_mundo_burning_agony_cas_02.troy", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, false, default, default, false, false);

            }
            else
            {
                SpellEffectCreate(out buffParticle, out _, "cryo_storm_green_team.troy", "cryo_storm_red_team.troy", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, false, default, default, false, false);

            }
            // SpellEffectCreate(out buffParticle, out _, "odin_heal_rune.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, true, default, false, false);
            killMe = false;
        }
        public override void OnDeactivate(bool expired)
        {

            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);



        }
        public override void OnUpdateStats()
        {
            if (killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinShieldRelicAuraCustom), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {

            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 75, SpellDataFlags.AffectAllSides | SpellDataFlags.AffectAllUnitTypes, 1))//SpellDataFlags.AffectHeroes, 1, default, true))
                {

                    if (!killMe)
                    {
                        if (owner.Position.Distance(new Vector2(4533, 4668)) <= 50)
                        {
                            SpellEffectRemove(buffParticle);
                            var camPos = new Vector3(5100, 50, 11782);
                            TeleportToPosition(unit, camPos);
                            // AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreSmallRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            killMe = true;
                            SetCameraPosition((Champion)owner, camPos);
                        }
                        if (owner.Position.Distance(new Vector2(5100, 11582)) <= 50)
                        {
                            SpellEffectRemove(buffParticle);
                            var camPos = new Vector3(4533, 50, 4468);
                            TeleportToPosition(unit, camPos);
                            //  AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreSmallRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            killMe = true;
                            SetCameraPosition((Champion)owner, camPos);
                        }
                        if (owner.Position.Distance(new Vector2(9783, 4781)) <= 50)
                        {
                            SpellEffectRemove(buffParticle);
                            var camPos = new Vector3(9205, 50, 11782);
                            TeleportToPosition(unit, camPos);
                            //   AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreSmallRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            killMe = true;
                            SetCameraPosition((Champion)owner, camPos);
                        }
                        if (owner.Position.Distance(new Vector2(9205, 11582)) <= 50)
                        {
                            SpellEffectRemove(buffParticle);
                            var camPos = new Vector3(9783, 50, 4781);
                            TeleportToPosition(unit, camPos);
                            // AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreSmallRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            killMe = true;
                            SetCameraPosition((Champion)owner, camPos);
                        }

                        // AddBuff((ObjAIBase)unit, unit, new Buffs.OdinShieldRelicBuffHeal(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);

                    }
                }
            }

        }
    }
}