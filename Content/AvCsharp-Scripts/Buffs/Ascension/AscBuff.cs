/*namespace Buffs
{
   /* internal class AscBuff : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };
        public override StatsModifier StatsModifier { get; protected set; } = new();

        Particle p1;
        Particle p2;
        Region r1;
        Region r2;
        public override void OnActivate()
        {
            if (Target is ObjAIBase obj)
            {
                AddBuff("AscBuffIcon", 25000.0f, 1, null, Target, obj);
            }

            PlaySound("Stop_sfx_ZhonyasRingShield_OnBuffActivate", Target);
            PlaySound("Play_sfx_Leona_LeonaSolarFlare_hit", Target);
            PlaySound("Play_sfx_Lux_LuxIlluminationPassive_hit", Target);

            p1 = AddParticleBind(Target, "Global_Asc_Avatar", Target, lifetime: -1);
            p2 = AddParticleBind(Target, "Global_Asc_Aura", Target, lifetime: -1);
            AddParticleLink(Target, "AscForceBubble", Target, Target, scale: 3.3f);

            ApiEventManager.OnDeath.AddListener(Target, Target, OnDeath, true);

            //Unit Perception bubbles seems to be broken
            r1 = AddUnitPerceptionBubble(Target, 0.0f, 25000f, TeamId.TEAM_ORDER, true, Target);
            r2 = AddUnitPerceptionBubble(Target, 0.0f, 25000f, TeamId.TEAM_CHAOS, true, Target);

            //Note: The ascension applies a "MoveAway" knockback debuff on all enemies around.
            //The duration varies based on the distance (yet unknown) you were to whoever ascended. And the PathSpeedOverride and ParabolicGravity varies based on the duration.
            //PathSpeedOverride and ParabolicGravity with 0.5 duration: Speed - 1200 / ParabolicGravity - 10.0
            //PathSpeedOverride and ParabolicGravity with 0.75 duration: Speed - 1600 / ParabolicGravity - 7.0
        }

        public void OnDeath(DeathData deathData)
        {
            if (deathData.Unit is NeutralMinion xerath)
            {
                AddBuff("AscBuffTransfer", 5.7f, 1, null, deathData.Killer, xerath);
            }
            else if (deathData.Unit is Champion)
            {
                AnnounceStartGameMessage(3, 8);
                AnnounceClearAscended();
            }

            RemoveBuff(deathData.Unit.GetBuffWithName("AscBuffIcon"));
            RemoveBuff(Buff);
        }

        public override void OnDeactivate()
        {
            RemoveParticle(p1);
            RemoveParticle(p2);
            r1.SetToRemove();
            r2.SetToRemove();
            Target.PauseAnimation(false);
        }
    }
}*/