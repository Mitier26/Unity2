using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackerPet : MonoBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;

    public GameObject rangeImage;
    public GameObject rangeObject;
    public float attackDelay;
    public float elapsedTime;
    public float attackPower;

    private NavMeshAgent agent;

    public bool isAttack = false;

    public Transform rotatePoint;

    public List<GameObject> targets;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameManager.Instance.player.gameObject;

        rangeObject.SetActive(false);
        rangeImage.SetActive(false);
    }


    private void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    private IEnumerator Attack()
    {
        rangeImage.SetActive(true);

        Vector3 direction = targets[0].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        rotatePoint.transform.rotation = targetRotation;

        yield return new WaitForSeconds(attackDelay);
        rangeObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        rangeImage.SetActive(false);
        rangeObject.SetActive(false);

        if(targets.Count == 0)
        {
            yield break;
        }
        else
        {
            yield return StartCoroutine(Attack());
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;

        if (target == null)
        {
            this.target = GameManager.Instance.player.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);

            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);
        }
    }
}
