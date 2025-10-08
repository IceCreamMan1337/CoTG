using ChildrenOfTheGraveEnumNetwork.Enums;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.SpellNS.Missile;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp.Converted;
using static ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.Lua.Functions;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

public class SpellScriptEmptyAutoAttack : CSpellScript
{
    public override void TargetExecute(AttackableUnit target, SpellMissile? missileNetworkID, ref HitResult hitResult)
    {

        float baseAttackDamage = GetBaseAttackDamage(owner);
        ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);

    }
}