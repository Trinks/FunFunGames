using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public float Money;
    public int FunPoints;

    /// <summary>
    /// Decrease the player his total amount of money by value.
    /// </summary>
    /// <param name="value">Money</param>
    public void DecreaseMoney(float value)
    {
        Money -= value;
    }

    /// <summary>
    /// Decrease the amount of Fun Points by value
    /// </summary>
    /// <param name="value">Fun Points</param>
    public void DecreaseFunPoints(int value)
    {
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
