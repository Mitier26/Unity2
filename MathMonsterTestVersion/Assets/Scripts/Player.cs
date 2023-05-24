using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float speed;
    private Rigidbody2D rigid;

    [SerializeField] private float defenderRange;
    [SerializeField] private float supporterRange;
    [SerializeField] private float attackerRange;

    [SerializeField] private Transform defenderRangeSprite;
    [SerializeField] private Transform supporterRangeSprite;
    [SerializeField] private Transform attackerRangeSprite;

    public List<GameObject> nearEnemies;
    public List<GameObject> mediumEnemies;
    public List<GameObject> farEnemies;

    public bool isActiveAttacker;
    public bool isActiveSupporter;
    public bool isActiveDefender;

    public AttackerPet attackerPet;
    public SupporterPet supporterPet;
    public DefenderPet defenderPet;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        nearEnemies = new List<GameObject>();
        mediumEnemies = new List<GameObject>();
        farEnemies = new List<GameObject>();
    }

    private void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        rigid.MovePosition(rigid.position + new Vector2 (x, y) * speed * Time.deltaTime);

        defenderRangeSprite.localScale = Vector3.one * defenderRange;
        supporterRangeSprite.localScale = Vector3.one * supporterRange;
        attackerRangeSprite.localScale = Vector3.one * attackerRange;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
            Debug.Log(gameObject.name);
    }


    public void SetFarEnemies(GameObject enemy)
    {
        farEnemies.Add(enemy);
        isActiveAttacker = true;
        attackerPet.SetTarget(enemy);
    }

    public void SetMediumEnemies(GameObject enemy)
    {
        mediumEnemies.Add(enemy);
        isActiveSupporter = true;
        supporterPet.SetTarget(enemy);
    }

    public void SetNearEnemies(GameObject enemy)
    {
        nearEnemies.Add(enemy);
        isActiveDefender = true;
        defenderPet.SetTarget(enemy);
    }

    public void ExitFarEnemies(GameObject enemy)
    {
        if (farEnemies.Contains(enemy))
        {
            farEnemies.Remove(enemy);

            if (farEnemies.Count == 0)
            {
                isActiveAttacker = false;
                attackerPet.SetTarget(this.gameObject);
            }
            else if( farEnemies.Count > 0)
            {
                attackerPet.SetTarget(farEnemies[Random.Range(0, farEnemies.Count)]);
            }
        }
    }

    public void ExitMediumEnemies(GameObject enemy)
    {
        if (mediumEnemies.Contains(enemy))
        {
            mediumEnemies.Remove(enemy);

            if (mediumEnemies.Count == 0)
            {
                isActiveSupporter = false;
                supporterPet.SetTarget(this.gameObject);
            }
            else if (mediumEnemies.Count > 0)
            {
                supporterPet.SetTarget(mediumEnemies[Random.Range(0, mediumEnemies.Count)]);
            }
        }
    }

    public void ExitNearEnemies(GameObject enemy)
    {
        if (nearEnemies.Contains(enemy))
        {
            nearEnemies.Remove(enemy);

            if (nearEnemies.Count == 0)
            {
                isActiveDefender = false;
                defenderPet.SetTarget(this.gameObject);
            }
            else if(nearEnemies.Count > 0)
            {
                defenderPet.SetTarget(nearEnemies[Random.Range(0, nearEnemies.Count)]);
            }
        }
    }

    public void SetDeleteEnemy(GameObject enemy)
    {
        if (farEnemies.Contains(enemy))
        {
            farEnemies.Remove(enemy);
        }
        if (mediumEnemies.Contains(enemy))
        {
            mediumEnemies.Remove(enemy);
        }
        if (nearEnemies.Contains(enemy))
        {
            nearEnemies.Remove(enemy);
        }

        if(farEnemies.Count != 0)
        {
            attackerPet.SetTarget(farEnemies[0]);
        }
        else
        {
            attackerPet.SetTarget(this.gameObject);
        }

        if (mediumEnemies.Count != 0)
        {
            supporterPet.SetTarget(mediumEnemies[0]);
        }
        else
        {
            supporterPet.SetTarget(this.gameObject);
        }

        if (nearEnemies.Count != 0)
        {
            defenderPet.SetTarget(nearEnemies[0]);
        }
        else
        {
            defenderPet.SetTarget(this.gameObject);
        }

    }
}
