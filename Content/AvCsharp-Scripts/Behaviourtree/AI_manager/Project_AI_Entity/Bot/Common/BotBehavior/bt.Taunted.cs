using static ChildrenOfTheGraveEnumNetwork.Enums.SpellDataFlags;
using static ChildrenOfTheGraveEnumNetwork.Enums.SpellbookType;
using static ChildrenOfTheGraveEnumNetwork.Enums.UnitType;
using AIScripts;

namespace BehaviourTrees.all;


class TauntedClass : AI_Characters 
{

   private  AutoAttackClass autoAttackClass = new AutoAttackClass();
     public bool Taunted(
      out bool _IssuedAttack,
      out AttackableUnit _IssuedAttackTarget,
      AttackableUnit Self,
      bool PrevIssuedAttack,
      AttackableUnit PrevIssuedAttackTarget
         )
      {
        AttackableUnit IssuedAttackTarget = default;
        bool IssuedAttack = default;
        bool result =
              // Sequence name :Taunt
              (
                    TestUnitHasBuff(
                          Self,
                          default,
                          "Taunt",
                          true) &&
                    GetUnitBuffCaster(
                          out Taunter,
                          Self,
                          "Taunt") &&
                    SetUnitAIAttackTarget(
                          Taunter) &&
                    autoAttackClass.AutoAttack(
                          out IssuedAttack,
                          out IssuedAttackTarget,
                          Taunter,
                          Self,
                          PrevIssuedAttack,
                          PrevIssuedAttackTarget)

              );
        _IssuedAttackTarget = IssuedAttackTarget;
        _IssuedAttack = IssuedAttack;
        return result;
      }
}

