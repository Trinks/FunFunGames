﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class BoosterpackGamblingController : MonoBehaviour
{
    public static BoosterpackGamblingController Instance;
    public List<GameObject> Grid = new List<GameObject>();
    public List<GameObject> PanelsSorting = new List<GameObject>();

    public List<Card> CardCollection = new List<Card>();

    private List<Card> _randomizedCardSpawnList = new List<Card>();

    public float Timer = 0.0f;
    public float MaxTime = 25.0f;

    public GameObject PanelTemp;

    public Divider Divider;
    public UIManager UIObj;

    public int CardsToSpawn = 50;
    private int Numeration = 0;
    private bool _spinning = false;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator SpawnCard(Player player)
    {
        if (player.BoosterpackCollection.Count <= 0)
        {
            UIObj.SendMessage(MessageType.NotEnoughBoosterpacks, 0, "");
            yield break;
        }
        if (_spinning) yield break;

        DateTime startSpinTime = DateTime.Now;
        float currentTime = (float)(DateTime.Now - startSpinTime).TotalSeconds;
        float acceleration = 1 * currentTime;
        float speedSpin = UIObj.AcScrollbar.Evaluate(Timer / MaxTime);


        Numeration = 0;
        _spinning = true;

        SpawnObjectsFromRarity(player);

        while (Numeration < CardsToSpawn)
        {
            if (Grid[0].GetComponent<GridCollisionDetection>().OnTriggerEnter(PanelsSorting[0].GetComponent<Collider>()))
            {
                for (int i = 0; i < PanelsSorting.Count; i++)
                {
                    GameObject panel = PanelsSorting[i];
                    CardPanel cardPanel = panel.GetComponent<CardPanel>();

                    if (i == 0)
                    {
                        PanelsSorting[PanelsSorting.Count - 1] = panel;
                        panel.transform.position = Grid[10].transform.position;

                        cardPanel.CardCharacter.sprite = _randomizedCardSpawnList[Numeration].CharacterSprite;
                        cardPanel.BackgroundCardRarity.sprite = _randomizedCardSpawnList[Numeration].RaritySpriteBackground;
                        cardPanel.CardName.text = _randomizedCardSpawnList[Numeration].CardName;
                        cardPanel.CardDescription.text = _randomizedCardSpawnList[Numeration].Description;
                        cardPanel.Rarity = _randomizedCardSpawnList[Numeration].Rarity;
                    }
                    else
                    {
                        panel.transform.position = Grid[i - 1].transform.position;
                        PanelsSorting[i - 1] = panel;
                    }
                }
                Numeration++;

                yield return new WaitForSeconds(UIObj.AcScrollbar.Evaluate((float)Numeration / (float)CardsToSpawn) * (Time.deltaTime / 2));
            }
        }

        player.BoosterpackCollection.RemoveAt(0);

        // Winning item send to inventory etc........
        CardPanel winningItem = PanelsSorting[5].GetComponent<CardPanel>();

        Inventory.Instance.CardPanels.Add(winningItem);

        Card card = ScriptableObject.CreateInstance<Card>();
        card.CardName = winningItem.CardName.text;
        card.Description = winningItem.CardDescription.text;
        card.CharacterSprite = winningItem.CardCharacter.sprite;
        card.RaritySpriteBackground = winningItem.BackgroundCardRarity.sprite;
        card.Rarity = winningItem.Rarity;

        player.CardCollection.Add(card);

        _spinning = false;

        yield break;
    }

    /// <summary>
    /// Adds cards to a list with a given rarity to create a roulette rarity effect.
    /// </summary>
    /// <param name="p">Player</param>
    private void SpawnObjectsFromRarity(Player p)
    {
        CardType bp = p.BoosterpackCollection[0].Rarity;

        int normal = 35;
        int rare = 10;
        int epic = 5;

        switch(bp)
        {
            case CardType.Normal:
                epic = 3;
                rare = 10 + epic;
                normal = 35 + epic + rare;
                break;
            case CardType.Rare:
                epic = 8;
                rare = 18 + epic;
                normal = 20 + epic + rare; ;
                break;
            case CardType.Epic:
                epic = 12;
                rare = 21 + epic;
                normal = 15 + epic + rare; ;
                break;
        }


        for (int i = 0; i < CardsToSpawn; i++)
        {
            if (i <= epic)
            {
                _randomizedCardSpawnList.Add(GetRandomCard(CardType.Epic));
            }
            else if (i <= rare)
            {
                _randomizedCardSpawnList.Add(GetRandomCard(CardType.Rare));
            }
            else if (i <= normal)
            {
                _randomizedCardSpawnList.Add(GetRandomCard(CardType.Normal));
            }
        }

        System.Random r = new System.Random();
        _randomizedCardSpawnList = _randomizedCardSpawnList.OrderBy(a => r.Next()).ToList();
    }

    /// <summary>
    /// Selects a random card with a certain card rarity type
    /// </summary>
    /// <param name="type">Card rarity</param>
    /// <returns>Card</returns>
    private Card GetRandomCard(CardType type)
    {
        List<Card> CollectionOfCardType = CardCollection.Where(x => x.Rarity == type).ToList();

        return CollectionOfCardType[UnityEngine.Random.Range(0, CollectionOfCardType.Count)];
    }
}
