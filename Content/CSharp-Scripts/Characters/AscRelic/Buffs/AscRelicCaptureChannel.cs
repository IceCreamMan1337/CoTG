/*namespace Buffs
{
  /*  internal class AscRelicCaptureChannel : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.AURA,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
            IsHidden = true
        };

        public override StatsModifier StatsModifier { get; protected set; }

        Particle p1;
        Region r1;
        Region r2;
        float windUpTime = 1500.0f;
        bool castWindUp = false;
        public override void OnActivate()
        {
            castWindUp = true;

            p1 = AddParticleLink(Owner, "OdinCaptureBeam", Buff.SourceUnit, Target, 1.5f, bindBone: "buffbone_glb_channel_loc", targetBone: "spine", flags: (FXFlags)32);
            r1 = AddUnitPerceptionBubble(Target, 0.0f, Buff.Duration, TeamId.TEAM_ORDER, true, Target);
            r2 = AddUnitPerceptionBubble(Target, 0.0f, Buff.Duration, TeamId.TEAM_CHAOS, true, Target);
        }

        public override void OnDeactivate()
        {
            RemoveParticle(p1);
            r1.SetToRemove();
            r2.SetToRemove();
        }

        public override void OnUpdate()
        {
            if (castWindUp)
            {
                windUpTime -= Time.DeltaTime;
                if (windUpTime <= 0)
                {
                    PlayAnimation(Owner, "Channel", flags: (AnimationFlags)224);
                    AddBuff("AscRelicSuppression", 10.0f, 1, Spell, Target, Owner);
                    AddBuff("OdinChannelVision", 20.0f, 1, Spell, Owner, Owner);

                    p1 = AddParticleLink(Buff.SourceUnit, "OdinCaptureBeamEngaged", Buff.SourceUnit, Target, Buff.Duration - Buff.TimeElapsed, bindBone: "BuffBone_Glb_CHANNEL_Loc", targetBone: "spine");

                    string teamBuff = "OdinBombSuppressionOrder";
                    if (Owner.Team == TeamId.TEAM_CHAOS)
                    {
                        teamBuff = "OdinBombSuppressionChaos";
                    }
                    AddBuff(teamBuff, 10.0f, 1, Spell, Target, Owner);

                    castWindUp = false;
                }
            }
        }
    }
}*/