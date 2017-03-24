using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public enum MessageType
{
    PurchaseSuccesful,
    NotEnoughMoney,
    NotEnoughFunPoints,
    SelectPackage,
    NotEnoughBoosterpacks,
}

public class UIManager : MonoBehaviour
{
    // A collection with all of the sub menu's. | Index 0 = MainBox | Index 1 = ShowInventoryBox | Index 2 = VolumeBox | Index 3 = BuyBoostersPacksBox | Index 4 = BuyFunPointsBox
    public List<GameObject> SubMenus = new List<GameObject>();

    // A collection with all of the text components that have a function. [Fun Points section = | Index 0 = Wallet | Index 1 = FunPoints | Index 2 = Warning message],
    // [Buy boosterpack section = | Index 3 = Wallet | Index 4 = FunPoints | Index 5 = Warning message][Confirmation Pop up = | Index = 6]
    // [Open boosterpack section = | Index 7 = warning message
    public List<Text> TextComponents = new List<Text>();

    public GameObject CurrentSubMenu;
    public GameObject OldSubMenu;

    public GameObject ConfirmationPopUp;

    public Scrollbar BoosterpackScrollbar;

    public AnimationCurve AcScrollbar;
    public GameObject ContentScrollbar;

    private Canvas _canvas;
    private Player _player;
    private BoosterpackGamblingController _gamblingController;

    private bool _confirmSwitch;
    private Buyable product;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        _player = FindObjectOfType<Player>();
        _gamblingController = FindObjectOfType<BoosterpackGamblingController>();

        // This loop will get the current active sub menu.
        for(int i = 0; i < _canvas.transform.childCount; i++)
        {
            if (_canvas.transform.GetChild(i).gameObject.name == "") continue;

            if (_canvas.transform.GetChild(i).gameObject.activeSelf)
            {
                CurrentSubMenu = _canvas.transform.GetChild(i).gameObject;
            }
        }

        // Update all of the active text components.
        UpdateTextComponent(0, "WALLET: $" + _player.Money.ToString("F2"));
        UpdateTextComponent(1, "FUN POINTS: " + _player.FunPoints);
        UpdateTextComponent(3, "WALLET: $" + _player.Money.ToString("F2"));
        UpdateTextComponent(4, "FUN POINTS: " + _player.FunPoints);
    }

    /// <summary>
    /// Switches the sub menu to the next sub menu.
    /// </summary>
    /// <param name="subMenu">Next sub menu</param>
    public void SwitchSubMenu(GameObject subMenu)
    {
        OldSubMenu = CurrentSubMenu;
        CurrentSubMenu = subMenu;

        OldSubMenu.SetActive(false);
        CurrentSubMenu.SetActive(true);

        SendMessage(MessageType.SelectPackage, 0, "");
    }

    /// <summary>
    /// Updates the text component.
    /// </summary>
    /// <param name="index">Index of collection TextComponents</param>
    /// <param name="text">The updated text</param>
    public void UpdateTextComponent(int index, string text)
    {
        TextComponents[index].text = text;
    }

    public void SendMessage(MessageType msgType, int productAmount, string productName)
    {
        Text text = null;

        //Index 3 = BuyBoostersPacksBox | Index 4 = BuyFunPointsBox
        if (SubMenus[3] == CurrentSubMenu)
        {
            text = TextComponents[5];
            UpdateTextComponent(3, "WALLET: $" + _player.Money.ToString("F2"));
            UpdateTextComponent(4, "FUN POINTS: " + _player.FunPoints);
        }
        else if (SubMenus[4] == CurrentSubMenu)
        {
            text = TextComponents[2];
            UpdateTextComponent(0, "WALLET: $" + _player.Money.ToString("F2"));
            UpdateTextComponent(1, "FUN POINTS: " + _player.FunPoints);
        }
        else if(SubMenus[5] == CurrentSubMenu)
        {
            text = TextComponents[7];
        }
        else return;

        switch (msgType)
        {
            case MessageType.PurchaseSuccesful:
                text.color = Color.green;
                text.text = "YOU HAVE SUCCESFULLY BOUGHT " + productAmount.ToString() + " " + productName + "!";
                break;
            case MessageType.NotEnoughMoney:
                text.color = Color.red;
                text.text = "YOU DON'T HAVE ENOUGH MONEY TO BUY " + productName + "!";
                break;
            case MessageType.NotEnoughFunPoints:
                text.color = Color.red;
                text.text = text.text = "YOU DON'T HAVE ENOUGH FUN POINTS TO BUY " + productName + "!";
                break;
            case MessageType.SelectPackage:
                text.color = Color.white;
                text.text = "PLEASE SELECT A PACKAGE THAT YOU'D LIKE TO BUY.";
                break;
            case MessageType.NotEnoughBoosterpacks:
                text.color = Color.red;
                text.text = "YOU DON'T HAVE ANY BOOSTERPACKS TO OPEN!";
                break;
            default:
                break;
        }
    }

    public void ConfirmPopUp(Buyable buyable)
    {
        _confirmSwitch = !_confirmSwitch;
        ConfirmationPopUp.SetActive(_confirmSwitch);

        if (_confirmSwitch == false)
        {
            product = null;
            return;
        }

        string productPrice = (buyable.ProductType != ItemType.FunPoints) ? buyable.ProductPrice + " fun points" :  "$" + buyable.ProductPrice + " dollars";
        TextComponents[6].text = "You're about to buy " + buyable.ProductName + ". This will cost you " + productPrice + " press 'Buy now!' to complete your purchase!";
        product = buyable;
    }

    public Buyable GetCurrentBuyable()
    {
        _confirmSwitch = !_confirmSwitch;
        ConfirmationPopUp.SetActive(_confirmSwitch);

        return product;
    }

    public void OpenPackage()
    {
        StartCoroutine(_gamblingController.SpawnCard(_player));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
