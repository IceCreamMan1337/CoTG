/*namespace Buffs
{
  /*  internal class AscRelicSuppression : BuffGameScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.AURA,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
        };

        public override StatsModifier StatsModifier { get; protected set; } = new();

        float timer = 100.0f;
        public override void OnActivate()
        {
            ApiEventManager.OnDeath.AddListener(this, Target, OnDeath, true);
        }

        public void OnDeath(DeathData deathData)
        {
            RemoveBuff(Buff);
        }

        public override void OnDeactivate()
        {
            ApiEventManager.OnDeath.RemoveListener(this);
            RestoreMana(Target, Target.Stats.ManaPoints.Total);
        }

        public override void OnUpdate()
        {
            timer -= Time.DeltaTime;
            if (timer <= 0)
            {
                //Exact values for both Current Mana reduction and timer are unknow, these are approximations.
                RemoveMana(Target, 700.0f);
                timer = 530;
            }
        }
    }
}*/