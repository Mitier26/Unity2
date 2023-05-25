using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProEnemyWorkerMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] workerWayPoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
