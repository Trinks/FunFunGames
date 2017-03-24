using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Player PlayerObj;
    public UIManager UIObj;

    public void BuyBuyable()
    {
        Buyable product = UIObj.GetCurrentBuyable();
        
        if (product.ProductType == ItemType.FunPoints)
        {
            if (product.ProductType != ItemType.FunPoints) return;

            if (PlayerObj.Money < product.ProductPrice)
            {
                UIObj.SendMessage(MessageType.NotEnoughMoney, 0, product.ProductName);
                return;
            }
            else
            {
                UIObj.SendMessage(MessageType.PurchaseSuccesful, product.ProductAmount, product.ProductName);
            }

            PlayerObj.DecreaseMoney(product.ProductPrice);
            PlayerObj.IncreaseFunPoints(product.ProductAmount);
        }
        else
        {
            if (product.ProductType != ItemType.Boosterpack) return;

            if (PlayerObj.FunPoints < product.ProductPrice)
            {
                UIObj.SendMessage(MessageType.NotEnoughFunPoints, 0, product.ProductName);
                return;
            }
            else
            {
                UIObj.SendMessage(MessageType.PurchaseSuccesful, product.ProductAmount, product.ProductName);
            }

            PlayerObj.DecreaseFunPoints((int)product.ProductPrice);
            PlayerObj.BoosterpackCollection.Add(new Boosterpack { Rarity = product.CardType });
        }

        int correctWalletComponent = (UIObj.SubMenus[4] == UIObj.CurrentSubMenu) ? 0 : 3;
        int correctFunpointsComponent = (UIObj.SubMenus[4] == UIObj.CurrentSubMenu) ? 1 : 4;


        UIObj.UpdateTextComponent(correctWalletComponent, "Wallet: $" + PlayerObj.Money.ToString("F2"));
        UIObj.UpdateTextComponent(correctFunpointsComponent, "Fun Points: " + PlayerObj.FunPoints.ToString());
    }


}