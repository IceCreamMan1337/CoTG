namespace CharScripts
{
    public class CharScriptOdinShieldRelic : CharScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            if (GetmapID() == 22)
            {
                AddBuff(owner, owner, new Buffs.OdinShieldRelicAuraCustom(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.OdinShieldRelicAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);

            }




        }
    }
}
namespace PreLoads
{
    public class CharScriptOdinShieldRelic : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("odinshieldrelic");
            PreloadSpell("odinshieldrelicaura");
            PreloadSpell("odinshieldrelicbuff");
            PreloadSpell("odinshieldrelicbuffheal");
        }
    }
}