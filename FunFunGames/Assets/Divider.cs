using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Divider : MonoBehaviour
{
    public BoosterpackGamblingController Gb;

    public Image RaycastToDivider;

    //public RectTransform DividerObj;

    public void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        Debug.DrawLine(transform.position, RaycastToDivider.transform.position, Color.red);

        if (Physics.Raycast(transform.position, RaycastToDivider.transform.position, out hit))
        {
            print(hit.transform.name);
        }
    }
    //public void CheckIfOverlaps()
    //{

    //    Rect temp = DividerObj.rect;

    //    if (Gb.Timer >= Gb.MaxTime)
    //    {
    //        Debug.Log(GetComponent<Rect>().Overlaps(temp));
    //    }
    //}

    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log(col.gameObject.name);
    //    if (Gb.Timer >= Gb.MaxTime)
    //    {
    //        Gb.GetItemWin(col.gameObject);
    //    }
    //}
}
