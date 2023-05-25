using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ProEnemyWorker : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] workerWayPoints;
    public Transform command;

    public Slider slider;

    public float miningTime;
    public float speed;
    public float maxHp;
    public float hp;

    public bool isMined = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        for(int i = 0; i < workerWayPoints.Length; i++)
        {
            workerWayPoints[i] = ProGameManager.instance.minePoints[i];
        }
        command = ProGameManager.instance.command.transform;

        slider.gameObject.SetActive(false);
        SetSpeed();
        SetWayPoint();
    }

    private void SetSpeed()
    {
        agent.speed = isMined ? speed * 0.7f : speed;
    }

    private void SetWayPoint()
    {
        agent.isStopped = false;

        Vector3 point;

        if(isMined)
        {
            point = command.position;
        }
        else
        {
            point = workerWayPoints[Random.Range(0, workerWayPoints.Length)].position;
        }

        agent.SetDestination(point);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mine"))
        {
            StartCoroutine(Mining());
        }
        else if (collision.CompareTag("Center"))
        {
            // 골드 증가 숫자 표시?
            if(isMined)
            {
                ProGameManager.instance.enemyGold += 8;
                isMined = false;
            }
            SetSpeed();
            SetWayPoint();
        }
    }

    private IEnumerator Mining()
    {
        slider.gameObject.SetActive(true);
        float elapsedTime = 0;

        while(elapsedTime < miningTime)
        {
            elapsedTime += Time.deltaTime;
            slider.value = (float)elapsedTime / miningTime;
            yield return null;
        }

        slider.gameObject.SetActive(false);
        isMined = true;
        SetSpeed();
        SetWayPoint();


        yield break;
    }
}
