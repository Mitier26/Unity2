using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public GameObject mainProejctile;
    public ParticleSystem mainParticle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mainProejctile.SetActive(true);
        }

        if(mainParticle.IsAlive() == false)
        {
            mainProejctile.SetActive(false);
        }
    }
}
