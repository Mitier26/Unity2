using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public int spawnAmount = 4;
    public float spawnDistance = 15.0f;
    public float trajactoryVariance = 15.0f;

    public Asteroid asteroidPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            float variance = Random.Range(-trajactoryVariance, trajactoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
