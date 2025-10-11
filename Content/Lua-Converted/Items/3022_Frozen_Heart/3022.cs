namespace ItemPassives
{
    public class ItemID_3022 : ItemScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase && target is not LaneTurret)
            {
                if (IsRanged(owner))
                {
                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                    {
                        AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)target, target, new Buffs.Internal_40Slow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
                AddBuff(owner, target, new Buffs.ItemSlow(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3022 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
        };
    }
}
namespace PreLoads
{
    public class ItemID_3022 : IPreLoadScript
    {
        public void Preload()
        {
            PreloadSpell("internal_30slow");
            PreloadSpell("judicatorrighteousfury");
            PreloadSpell("internal_40slow");
            PreloadSpell("itemslow");
        }
    }
}