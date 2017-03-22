using UnityEngine;
using System.Collections;

public class GridCollisionDetection : MonoBehaviour
{
    public BoosterpackGamblingController BG;

    public bool OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        return col;
    }
}
