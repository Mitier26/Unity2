using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDestructor : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if(!player)
        {
            player = GameObject.Find("Player");
            return;
        }

        if(player.transform.position.x - transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }
}
