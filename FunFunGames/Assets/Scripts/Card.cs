﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.IO;

public enum Rarity
{
    Normal,
    Rare,
    Epic
}

[CreateAssetMenu(fileName = "New card")]
public class Card : ScriptableObject
{
    public int Id;
    public string CardName;
    public CardType Rarity;
    public string Description;
    public Sprite RaritySpriteBackground;
    public Sprite IconRarity;
    public Sprite CharacterSprite;

    public Card()
    {
        Id = DirectoryCount();
    }

    public static int DirectoryCount()
    {
        int i = 0;
        // Add file sizes.
        FileInfo[] fis = new DirectoryInfo("Assets/Cards").GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("asset"))
                i++;
        }
        return i;
    }
}
