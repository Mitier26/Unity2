using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Ball BallPrefab;

    private int maxCount = 45;
    public float spawnerPower = 20f;
    public float spawnerDelay = 0.3f;
    public float ballSize = 0.5f;
    public bool isShow = true;

    private int[] numbers;

    private void Start()
    {
        numbers = new int[maxCount];
        StartCoroutine(Spawner());
    }
    

    private IEnumerator Spawner()
    {
        int count = 0;

        numbers = Utils.SuffleRandom();

        while(count < maxCount)
        {
            Ball ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

            ball.number = numbers[count];
            ball.gameObject.SetActive(true);
            ball.gameObject.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * spawnerPower);
            ball.gameObject.transform.localScale = Vector3.one * ballSize;
            ball.isShow = isShow;
            count++;
            yield return new WaitForSeconds(spawnerDelay);
        }

        yield return null;
    }
}
