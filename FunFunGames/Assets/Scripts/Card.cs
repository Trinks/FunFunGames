using UnityEngine;
using System.IO;

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

    /// <summary>
    /// Counts all of the card objects
    /// </summary>
    /// <returns>Returns the amount of cards</returns>
    public static int DirectoryCount()
    {
        int i = 0;
        // Add file sizes.
        FileInfo[] fis = new DirectoryInfo("Assets/Cards").GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("asset"))
            {
                i++;
                //BoosterpackGamblingController.Instance.CardCollection.Add(Card);
            }
        }
        return i;
    }
}
