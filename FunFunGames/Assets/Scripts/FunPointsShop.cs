using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FunPointsShop : MonoBehaviour
{
    public Player PlayerObj;
    public UIManager UIObj;

    // A list filled with all of the buyables.
    public List<Buyable> Buyables = new List<Buyable>();

    public void BuyFunPoints()
    {
        Buyable product = UIObj.GetCurrentBuyable();
        
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

        UIObj.UpdateTextComponent(0, "WALLET: $" + PlayerObj.Money.ToString("F2"));
        UIObj.UpdateTextComponent(1, "FUN POINTS: " + PlayerObj.FunPoints.ToString());
    }
}