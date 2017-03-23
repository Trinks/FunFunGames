using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float Money;
    public int FunPoints;
    public List<Boosterpack> BoosterpackCollection = new List<Boosterpack>();
    public List<Card> CardCollection = new List<Card>();

    /// <summary>
    /// Decrease the player his total amount of money by value.
    /// </summary>
    /// <param name="value">Money</param>
    public void DecreaseMoney(float value)
    {
        if (Money <= 0) return;
        Money -= value;
    }

    /// <summary>
    /// Decrease the amount of Fun Points by value
    /// </summary>
    /// <param name="value">Fun Points</param>
    public void DecreaseFunPoints(int value)
    {
        if (FunPoints <= 0) return;
        FunPoints -= value;
    }

    /// <summary>
    /// Increase the amount of Fun Points by value
    /// </summary>
    /// <param name="value">Fun Points</param>
    public void IncreaseFunPoints(int value)
    {
        FunPoints += value;
    }
}
