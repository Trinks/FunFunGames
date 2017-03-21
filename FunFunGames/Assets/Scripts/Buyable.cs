using UnityEngine;
using System.Collections;

public enum ItemType
{
    FunPoints,
    Boosterpack
}

public enum CardType
{
    Normal,
    Rare,
    Epic,
    NotACard
}

public class Buyable : MonoBehaviour
{
    public string ProductName;
    public ItemType ProductType;
    public CardType CardType;
    public float ProductPrice;
    public int ProductAmount;
}
