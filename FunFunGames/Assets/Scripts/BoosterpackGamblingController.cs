using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BoosterpackGamblingController : MonoBehaviour
{
    public List<GameObject> Grid = new List<GameObject>();
    public List<GameObject> PanelsSorting = new List<GameObject>();

    public float Timer = 0.0f;
    public float MaxTime = 25.0f;

    public GameObject PanelTemp;

    public Divider Divider;
    public UIManager UIObj;

    public int CardPackTotal = 50;
    private int Numeration = 0;
    private bool _spinning = false;

    public IEnumerator SpawnCard(Player player)
    {
        if (player.BoosterpackCollection.Count <= 0) yield break;
        if (_spinning) yield break;

        DateTime startSpinTime = DateTime.Now;
        float startScrollValue = UIObj.BoosterpackScrollbar.value;
        float currentTime = (float)(DateTime.Now - startSpinTime).TotalSeconds;
        float acceleration = 1 * currentTime;
        float speedSpin = UIObj.AcScrollbar.Evaluate(Timer / MaxTime);

        Numeration = 0;
        _spinning = true;

        while(Numeration < CardPackTotal)
        {
            //speedSpin = UIObj.AcScrollbar.Evaluate(Timer / MaxTime);
            Debug.Log(speedSpin);
            Timer += Time.deltaTime;

            if (Grid[0].GetComponent<GridCollisionDetection>().OnTriggerEnter(PanelsSorting[0].GetComponent<Collider>()))
            {
                for (int i = 0; i < PanelsSorting.Count; i++)
                {
                    GameObject panel = PanelsSorting[i];

                    if (i == 0)
                    {
                        PanelsSorting[PanelsSorting.Count - 1] = panel;
                        panel.transform.position = Grid[10].transform.position;

                        /*
                         * Assign new data card for panel over here.
                         */ 

                    }
                    else
                    {
                        panel.transform.position = Grid[i - 1].transform.position;
                        PanelsSorting[i - 1] = panel;
                    }
                }
                Numeration++;

                yield return new WaitForSeconds(UIObj.AcScrollbar.Evaluate((float)Numeration / (float)CardPackTotal) * (Time.deltaTime / 2));
            }
        }

        // Remove boosterpack from inventory
        player.BoosterpackCollection.RemoveAt(0);
        //player.BoosterpackCollection.Sort();

        // Winning item send to inventory etc........
        GameObject winningItem = PanelsSorting[5];
        Color col = winningItem.GetComponent<UnityEngine.UI.Image>().color;
        Divider.GetComponent<UnityEngine.UI.Image>().color = col;

        _spinning = false;

        yield break;
    }

    public IEnumerator BoosterpackScroller()
    {
        DateTime startSpinTime = DateTime.Now;
        float startScrollValue = UIObj.BoosterpackScrollbar.value;
        float maxScrollValue = 0.9f;

        while (Timer < MaxTime)
        {
            float currentTime = (float)(DateTime.Now - startSpinTime).TotalSeconds;
            float acceleration = 1 * currentTime;
            float value = UIObj.AcScrollbar.Evaluate(Timer / MaxTime) * maxScrollValue;
            UIObj.BoosterpackScrollbar.value = (value + startScrollValue);

            Timer += (Time.deltaTime * acceleration);
            yield return 0;
        }
        UIObj.BoosterpackScrollbar.value = (maxScrollValue + startScrollValue);
    }

}
