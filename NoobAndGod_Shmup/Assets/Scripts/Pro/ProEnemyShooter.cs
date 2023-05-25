using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProEnemyShooter : MonoBehaviour
{
    public GameObject target;

    public float delay;

    private IEnumerator Shoot()
    {
        yield return null;
    }
}
