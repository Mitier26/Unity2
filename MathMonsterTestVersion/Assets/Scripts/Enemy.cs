using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    private NavMeshAgent agent;

    public float maxHp;
    public float hp;
    public Slider hpSlider;

    public GameObject expPrefab;
    public GameObject potionPrefab;
    public GameObject energyPrefab;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        hp = maxHp;
        hpSlider.value = hp / maxHp;
        hpSlider.transform.gameObject.SetActive(false);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Update()
    {
        //direction = (target.transform.position - transform.position).normalized;

        //transform.Translate(direction * speed * Time.deltaTime);

        if(target != null)
            agent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttack"))
        {
            hpSlider.transform.gameObject.SetActive(true);
            hp -= 2;
            hpSlider.value = hp / maxHp;

            if(hp <= 0)
            {
                // 삭제 하면 서 리스트에 있는 자신도 삭제를 해야한다.
                DropItems();
                GameManager.Instance.player.SetDeleteEnemy(this.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void DropItems()
    {
        GameObject expGo = Instantiate(expPrefab);
        GameObject energyGo = Instantiate(energyPrefab);
        
        expGo.transform.position = transform.position;
        energyGo.transform.position = transform.position;
       
        expGo.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 200);
        energyGo.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 200);
        
        if(Random.Range(0,11) < 2)
        {
            GameObject potionGo = Instantiate(potionPrefab);
            potionGo.transform.position = transform.position;
            potionGo.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 200);
        }
    }

}
