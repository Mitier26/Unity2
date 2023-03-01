using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Obstacle
{
    empty, spike, diamond, disc
}

public class Slot : MonoBehaviour
{
    private Obstacle obstacle;

    public GameObject spikePrefab;
    public GameObject diamondPrefab;
    public GameObject discPrefab;

    public Obstacle Obstacle
    {
        get
        {
            return obstacle;
        }
        set
        {
            if(transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            obstacle = value;

            if(value == Obstacle.empty)
            {
                return;
            }

            switch(value)
            {
                case Obstacle.spike:
                    GameObject spikeChild = Instantiate(spikePrefab);
                    spikeChild.transform.SetParent(transform, false);
                    spikeChild.transform.localPosition = Vector3.zero;
                    spikeChild.transform.localScale = Vector3.one * 40;
                    break;
                case Obstacle.diamond:
                    GameObject diamondChild = Instantiate(diamondPrefab);
                    diamondChild.transform.SetParent(transform, false);
                    diamondChild.transform.localPosition = new Vector3(0f, 0.25f, 0f);
                    diamondChild.transform.localScale = Vector3.one * 15;
                    break;
                case Obstacle.disc:
                    GameObject discChild = Instantiate(discPrefab);
                    discChild.transform.SetParent(transform, false);
                    discChild.transform.localPosition = new Vector3(0f, -0.4999f, 0f);
                    discChild.transform.localScale = Vector3.one * 50;
                    break;
            }
        }
    }
}
