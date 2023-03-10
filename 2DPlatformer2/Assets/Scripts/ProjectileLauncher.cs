using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        Vector3 originScalse = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(originScalse.x * transform.localScale.x > 0 ? 1 : -1, originScalse.y, originScalse.z);
    }
}
