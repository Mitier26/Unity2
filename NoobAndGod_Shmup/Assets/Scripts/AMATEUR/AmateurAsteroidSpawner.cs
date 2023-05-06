using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurAsteroidSpawner : MonoBehaviour
{
    public int spawnAmount = 1;
    public float spawnDistance = 20;
    public float trajectoryVariance = 15;       // 소환 했을 때 각도

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, 1);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            GameObject asteroid = AmateurPoolManager.instance.GetObject(AmateurObject.Asteroid);
            AmateureAsteroid asteroidComponent = asteroid.GetComponent<AmateureAsteroid>();

            asteroid.transform.position = spawnPoint;
            asteroidComponent.size = Random.Range(asteroidComponent.minSize, asteroidComponent.maxSize);
            asteroidComponent.SetAsteroid(rotation * -spawnDirection);
        }
    }
}
