using System;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits.AI;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Content;
using ChildrenOfTheGrave.ChildrenOfTheGraveServer.GameObjects.AttackableUnits;
using static PacketVersioning.PktVersioning;

namespace ChildrenOfTheGraveLibrary.GameObjects;

public interface IGoldOwner
{
    GoldOwner GoldOwner { get; }
}

public class GoldOwner
{
    const float MAX_GOLD_AMMOUNT = 100000.0f;
    public float Gold { get; internal set; }
    public float TotalGoldEarned { get; internal set; }
    public float TotalGoldSpent { get; internal set; }
    public AttackableUnit Owner { get; init; }

    public GoldOwner(AttackableUnit owner)
    {
        Owner = owner;
        Gold += GlobalData.ObjAIBaseVariables.StartingGold;
        TotalGoldEarned += Gold;
    }

    public void AddGold(float gold, bool notify = true, GameObject? source = null)
    {
        if (gold == 0)
        {
            return;
        }

        IGoldOwner objToAddGold = Owner;
        if (Owner is ObjAIBase obj)
        {
            ObjAIBase? redirect = obj.GetGoldRedirectTarget();
            if (redirect is not null)
            {
                objToAddGold = redirect;
            }
        }

        float oldGold = objToAddGold.GoldOwner.Gold;
        float newGold = Math.Min(objToAddGold.GoldOwner.Gold + gold, MAX_GOLD_AMMOUNT);
        objToAddGold.GoldOwner.Gold = newGold;
        objToAddGold.GoldOwner.TotalGoldEarned += newGold - oldGold;

        // Logs for debugging

        if (notify && objToAddGold is Champion c)
        {
            source ??= c;
            UnitAddGoldNotify(c, source, newGold - oldGold);
        }
    }


    internal void SpendGold(float gold)
    {
        Gold -= gold;
        TotalGoldSpent += gold;
    }
}
