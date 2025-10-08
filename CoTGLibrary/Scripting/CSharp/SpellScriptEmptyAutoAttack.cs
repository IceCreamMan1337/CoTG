using CoTGEnumNetwork.Enums;
using CoTG.CoTGServer.GameObjects.AttackableUnits;
using CoTG.CoTGServer.GameObjects.SpellNS.Missile;
using CoTG.CoTGServer.Scripting.CSharp.Converted;
using static CoTG.CoTGServer.Scripting.Lua.Functions;

namespace CoTG.CoTGServer.Scripting.CSharp;

public class SpellScriptEmptyAutoAttack : CSpellScript
{
    public override void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID, ref HitResult hitResult)
    {

        float baseAttackDamage = GetBaseAttackDamage(owner);
        ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);

    }
}