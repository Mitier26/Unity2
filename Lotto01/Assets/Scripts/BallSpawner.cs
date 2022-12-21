using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;

    private int maxCount = 45;
    private float spawnerPower = 20f;
    private float spawnerDelay = 0.3f;

    private int[] numbers = new int[45];

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    

    private IEnumerator Spawner()
    {
        int count = 0;

        numbers = Utils.SuffleRandom();

        while(count < maxCount)
        {
            GameObject ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

            ball.GetComponent<Ball>().number = numbers[count];
            ball.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * spawnerPower);

            count++;
            yield return new WaitForSeconds(spawnerDelay);
        }

        yield return null;
    }
}
