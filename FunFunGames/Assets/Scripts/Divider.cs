using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Divider : MonoBehaviour
{
    public BoosterpackGamblingController Gb;

    public GameObject RaycastReceiver;

    public GameObject GetWinningItem()
    {
        RaycastHit hit;
        GameObject raycastedObject = null;

        Debug.DrawLine(transform.position, RaycastReceiver.transform.position, Color.red);

        if (Physics.Raycast(transform.position, RaycastReceiver.transform.position, out hit))
        {
            raycastedObject = hit.transform.gameObject;
            Debug.Log(raycastedObject.name);
        }

        return raycastedObject;
    }
}
