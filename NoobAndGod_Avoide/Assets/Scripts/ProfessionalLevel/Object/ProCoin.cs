using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCoin : ProBox
{
    [SerializeField]
    private float coinLifeTime = 8f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(coinLifeTime);
        base.DestroyObject();
    }
}
