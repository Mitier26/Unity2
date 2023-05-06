using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurBackgroundManager : MonoBehaviour
{
    [SerializeField] private Sprite[] planets;

    // 행성의 랜덤 위치, 랜덤 크기

    private void Start()
    {
        SetPlanets();
        SetCamera();
    }


    private void SetPlanets()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject planet = new GameObject("Planet" + i);
            planet.AddComponent<SpriteRenderer>();
            planet.GetComponent<SpriteRenderer>().sprite = planets[Random.Range(0, planets.Length)];
            planet.GetComponent<SpriteRenderer>().sortingOrder = -2;
            planet.transform.localScale = Vector3.one * Random.Range(0.3f, 1f);
            planet.transform.position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 10);
        }
    }

    private void SetCamera()
    {
        Camera.main.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), -10);
    }
}
