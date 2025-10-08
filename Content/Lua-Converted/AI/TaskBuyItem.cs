using CoTG.CoTGServer.Scripting.CSharp.Converted;

namespace AIScripts;
//Status: Modified script, not like original
public class TaskBuyItemAI : CAIScript
{
    /*   int MIN_DIST_TO_SHOP = 500;
       int AI_MOVE = 0;
       int AI_SHOP = 1;

       int[] ITEMS_TO_BUY = new int[] { 1001, 2003, 1006, 1007, 1042, 1029 };

       int BUY_INDEX = 1;

       void ChangeItemToBuy(object h)
       {
           BUY_INDEX += 1;
           if (BUY_INDEX > 6)
           {
               BUY_INDEX = 1;
           }
       }

       int GetNextItemToBuy(object h)
       {
           return ITEMS_TO_BUY[BUY_INDEX - 1];
       }

       void UpdatePriority(object h)
       {
           int gold = GetGold();
           int itemPrice = GetItemPrice(h.ItemToBuy);
           float priority = 0;
           float dist = GetDist(GetRegroupPos(), GetPos());

           if (gold < itemPrice)
           {
               h.Priority = 0;
           }
           else if (gold >= itemPrice)
           {
               float multiplier = 1.0f;
               if (gold > itemPrice * 2)
               {
                   multiplier = 1.1f;
               }

               if (dist < 3000)
               {
                   priority = 0.8f * multiplier;
               }
               else if (dist > 3000 && dist < 6000)
               {
                   priority = (6000 / dist) * 0.2f * multiplier;
               }
               else
               {
                   priority = 0.2f * multiplier;
               }
               h.Priority = Math.Min(priority, 0.9f);
           }

           if (GetItemPrice(h.ItemToBuy) == 0)
           {
               h.Priority = 0;
               h.ItemToBuy = null;
           }
       }

       void BeginTask(object h)
       {
           var regroupPos = GetRegroupPos();
           float dist = GetDist(regroupPos, GetPos());
           int currentState = GetState();

           if (currentState == AI_SHOP && dist > MIN_DIST_TO_SHOP)
           {
               SetStateAndMove(AI_MOVE, regroupPos);
           }
           else if (dist <= MIN_DIST_TO_SHOP)
           {
               if (currentState == AI_MOVE)
               {
                   StopMove();
                   SetState(AI_SHOP);
               }

               int gold = GetGold();
               int itemPrice = GetItemPrice(h.ItemToBuy);

               if (gold > itemPrice)
               {
                   BuyItem(h.ItemToBuy);
                   ChangeItemToBuy(h);
                   h.ItemToBuy = GetNextItemToBuy(h);
                   UpdatePriority(h);
               }
           }
       }

      */

}

