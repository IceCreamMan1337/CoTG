/*namespace Buffs
{
   /* internal class AscBuffTransfer : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING
        };
        public override StatsModifier StatsModifier { get; protected set; }

        float soundTimer = 1000.0f;
        bool hasNotifiedSound = false;
        public override void OnActivate()
        {
            if (Target is ObjAIBase obj)
            {
                if (Target is Champion ch)
                {
                    AnnounceChampionAscended(ch);
                }
                else if (Target is NeutralMinion mo)
                {
                    AnnounceMinionAscended(mo);
                }
                NotifyAscendant(obj);
            }

            Target.PauseAnimation(true);

            AddParticleLink(Target, "EggTimer", Target, Target, Buff.Duration, flags: (FXFlags)32);
            AddParticleBind(Target, "AscTransferGlow", Target, lifetime: Buff.Duration, flags: (FXFlags)32);
            AddParticleBind(Target, "AscTurnToStone", Target, lifetime: Buff.Duration, flags: (FXFlags)32);

            Buff.SetStatusEffect(StatusFlags.Targetable, false);
            Buff.SetStatusEffect(StatusFlags.Stunned, true);
            Buff.SetStatusEffect(StatusFlags.Invulnerable, true);
        }

        public override void OnDeactivate()
        {
            Target.PauseAnimation(false);

            AddParticleLink(Target, "CassPetrifyMiss_tar", Target, Target, scale: 3.0f);
            AddParticleLink(Target, "Rebirth_cas", Target, Target);
            AddParticleLink(Target, "TurnBack", Target, Target);
            AddParticleLink(Target, "LeonaPassive_tar", Target, Target, scale: 2.5f);

            if (Target is ObjAIBase obj)
            {
                AddBuff("AscBuff", 25000.0f, 1, null, Target, obj);
            }
        }

        public override void OnUpdate()
        {
            soundTimer -= Time.DeltaTime;
            if (soundTimer <= 0 && !hasNotifiedSound)
            {
                PlaySound("Play_sfx_ZhonyasRingShield_OnBuffActivate", Target);
                PlaySound("Play_sfx_Cassiopeia_CassiopeiaPetrifyingGazeStun_OnBuffActivate", Target);
                hasNotifiedSound = true;
            }
        }
    }
}*/