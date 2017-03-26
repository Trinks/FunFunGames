using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Player Player;

    public List<CardPanel> CardPanels = new List<CardPanel>();
    public List<CardPanel> AllCardPanelsInInventory = new List<CardPanel>();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCardPanelInInventory()
    {

        for (int i = 0; i < Player.CardCollection.Count; i++)
        {
            Card pc = Player.CardCollection[i];

            if (AllCardPanelsInInventory.Count >= Player.CardCollection.Count) continue;

            CardPanel go = (CardPanel)Instantiate(CardPanels[i], transform.position, Quaternion.identity, this.transform);

            go.CardCharacter.sprite = pc.CharacterSprite;
            go.BackgroundCardRarity.sprite = pc.RaritySpriteBackground;
            go.CardName.text = pc.CardName;
            go.CardDescription.text = pc.Description;
            go.Rarity = pc.Rarity;

            AllCardPanelsInInventory.Add(go);
        }
    }
}
