using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoosterpackGamblingController : MonoBehaviour
{
    public List<Card> NormalCards = new List<Card>();
    public List<Card> RareCards = new List<Card>();
    public List<Card> EpicCards = new List<Card>();

    public List<GameObject> Grid = new List<GameObject>();
    public List<GameObject> PanelsSorting = new List<GameObject>();

    public float Timer = 0.0f;
    public float MaxTime = 25.0f;

    public GameObject PanelTemp;

    public GameObject Divider;
    public UIManager UIObj;

    private int CardPackTotal = 25;

    public void SpawnCard()
    {
        if (Grid[0].GetComponent<GridCollisionDetection>().OnTriggerEnter(PanelsSorting[0].GetComponent<Collider>()))
        {
            for (int i = 0; i < PanelsSorting.Count; i++)
            {
                GameObject panel = PanelsSorting[i];
        
                if (i == 0)
                {
                    PanelsSorting[PanelsSorting.Count - 1] = panel;
                    panel.transform.position = Grid[1].transform.position;
                    continue;
                }

                PanelsSorting[i - 1] = panel;
            }
        }
    }

    public void GetItemWin(GameObject obj)
    {
        Debug.Log(obj.name);
    }

    public IEnumerator BoosterpackScroller()
    {
        DateTime startSpinTime = DateTime.Now;
        float startScrollValue = UIObj.BoosterpackScrollbar.value;
        float maxScrollValue = 0.9f;

        while (Timer < MaxTime)
        {
            //float currentTime = (float)(DateTime.Now - startSpinTime).TotalSeconds;
           // float acceleration = 1 * currentTime;
           // float value = UIObj.AcScrollbar.Evaluate(Timer / MaxTime) * maxScrollValue;
           // UIObj.BoosterpackScrollbar.value = (value + startScrollValue);

            //Timer += (Time.deltaTime * acceleration);
            
            yield return 0;
        }
        SpawnCard();
      //  UIObj.BoosterpackScrollbar.value = (maxScrollValue + startScrollValue);
    }

}
