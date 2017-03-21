using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.IO;

[CreateAssetMenu(fileName = "New card")]
public class Card : ScriptableObject
{
    public int Id;
    public string CardName;
    public string Description;
    public Image BackgroundCardImage; 

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
