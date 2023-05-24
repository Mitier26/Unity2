using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenderPet : MonoBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameManager.Instance.player.gameObject;
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;

        if (target == null)
        {
            this.target = GameManager.Instance.player.gameObject;
        }
    }
}
