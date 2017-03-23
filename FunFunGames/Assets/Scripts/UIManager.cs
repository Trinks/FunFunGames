using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public enum MessageType
{
    PurchaseSuccesful,
    NotEnoughMoney
}

public class UIManager : MonoBehaviour
{
    // A collection with all of the sub menu's. | Index 0 = MainBox | Index 1 = ShowDeckBox | Index 2 = VolumeBox | Index 3 = BuyBoostersPacksBox | Index 4 = BuyFunPointsBox
    public List<GameObject> SubMenus = new List<GameObject>();

    // A collection with all of the text components that have a function. [Fun Points section = | Index 0 = Wallet | Index 1 = FunPoints | Index 2 = Warning message],
    // [Boosterpack section = | Index 3 = Wallet | Index 4 = FunPoints | Index 5 = Warning message][Confirmation Pop up = | Index = 6]
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
        UpdateTextComponent(0, "Wallet: $" + _player.Money.ToString("F2"));
        UpdateTextComponent(1, "Fun Points: " + _player.FunPoints);
        UpdateTextComponent(3, "Wallet: $" + _player.Money.ToString("F2"));
        UpdateTextComponent(4, "Fun Points: " + _player.FunPoints);
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

        if (subMenu == SubMenus[3].gameObject)
        {
            UpdateTextComponent(3, "Wallet: $" + _player.Money.ToString("F2"));
            UpdateTextComponent(4, "Fun Points: " + _player.FunPoints);
            UpdateTextComponent(5, "Please select a package you'd like to buy.");
            TextComponents[5].color = Color.black;
        }
        else if (subMenu == SubMenus[4].gameObject)
        {
            UpdateTextComponent(0, "Wallet: $" + _player.Money.ToString("F2"));
            UpdateTextComponent(1, "Fun Points: " + _player.FunPoints);
            UpdateTextComponent(2, "Please select a package you'd like to buy.");
            TextComponents[5].color = Color.black;
        }
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
        Text text = TextComponents[3];

        if (SubMenus[3] == CurrentSubMenu)
        {
            text = TextComponents[3];
        }
        else if (SubMenus[4] == CurrentSubMenu)
        {
            text = TextComponents[5];
        }

        switch (msgType)
        {
            case MessageType.PurchaseSuccesful:
                text.color = Color.green;
                text.text = "You have succesfully bought " + productAmount.ToString() + " " + productName + "!";
                break;
            case MessageType.NotEnoughMoney:
                text.color = Color.red;
                text.text = "You don't have enough money to buy " + productName + "!";
                break;
            default:
                text.color = Color.grey;
                text.text = "Please contact the developers how you got this message!";
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


        TextComponents[6].text = "You're about to buy " + buyable.ProductName + ". This will cost you " + buyable.ProductPrice + " press 'Buy now!' to complete your purchase!";
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
        StartCoroutine(_gamblingController.SpawnCard());
    }
}
