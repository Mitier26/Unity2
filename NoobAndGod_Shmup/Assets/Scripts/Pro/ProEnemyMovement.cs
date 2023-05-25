using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ProEnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    public Transform[] waypoints;
    public int wayIndex;
    public Vector3 destination;
    public float speed;

    public bool isMoved = true;

    public ProEnemyUnit unit;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        for(int i  = 0; i < ProGameManager.instance.points.transform.childCount; i++)
        {
            waypoints[i] = ProGameManager.instance.points.transform.GetChild(i).transform;
        }

    }

    private void Start()
    {
        unit = GetComponent<ProEnemyUnit>();
        unit.action += SetRandomWaypoint;
        unit.stop += StopMove;

        agent.speed = speed;
        wayIndex = Random.Range(0, 3);
        destination = waypoints[wayIndex].position;
        agent.SetDestination(destination);
        unit.state = STATE.Move;
        isMoved = true;
    }

    private void Update()
    {
        if (isMoved)
        {
            if(Vector3.Distance(agent.destination, agent.transform.position) <= 1f)
            {
                if(agent.velocity.sqrMagnitude <= 0.1f)
                {
                    isMoved = false;
                    unit.SetIdle();
                }
               
            }
        }
    }

    private void FixedUpdate()
    {
        spriteRenderer.flipX = agent.velocity.x < 0f;
    }

    private void SetRandomWaypoint()
    {
        agent.isStopped = false;
        // 처음 생성되었을 때 0,1,2 중 하나로 가기위해
        
        wayIndex = Random.Range(0, waypoints.Length);
        destination = waypoints[wayIndex].position;
        
        agent.SetDestination(destination);
        agent.speed = speed;

        unit.state = STATE.Move;

        isMoved = true;
    }

    private void StopMove()
    {
        agent.isStopped = true;
    }
}
