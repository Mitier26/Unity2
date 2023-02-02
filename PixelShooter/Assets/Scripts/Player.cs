using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public int power;
    public int maxPower;
    public int maxBoom;
    public int boom;
    public float maxShotDelay;
    public float currentShotDelay;

    public int life;
    public int score;

    public bool isTouchTop;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isTouchBottom;
    public bool isBoomTime;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject boomEffect;

    public GameManager gameManager;
    public ObjectManager objectManager;

    public GameObject[] followers;

    public bool isHit;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
        {
            v = 0;
        }

        anim.SetInteger("Input", (int)h);


        Vector3 position = transform.position;
        Vector3 direction = new Vector3(h, v, 0).normalized;

        position += direction * moveSpeed * Time.deltaTime;

        transform.position = position;
    }

    private void Fire()
    {
        if (!Input.GetButton("Fire1")) return;

        if (currentShotDelay < maxShotDelay) return;

        switch(power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObject("BulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObject("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletL = objectManager.MakeObject("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            default:
                GameObject bulletRR = objectManager.MakeObject("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.25f;
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletLL = objectManager.MakeObject("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                GameObject bulletCC = objectManager.MakeObject("BulletPlayerB");
                bulletCC.transform.position = transform.position ;
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

        }

        currentShotDelay = 0;
    }

    private void Boom()
    {
        if (!Input.GetButton("Fire2")) return;

        if (isBoomTime) return;

        if (boom == 0) return;

        boom--;
        isBoomTime = true;

        gameManager.UpdateBoomIcon(boom);

        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        GameObject[] enmeyL = objectManager.GetPool("EnemyL");
        GameObject[] enmeyM = objectManager.GetPool("EnemyM");
        GameObject[] enmeyS = objectManager.GetPool("EnemyS");
        for (int i = 0; i < enmeyL.Length; i++)
        {
            if (enmeyL[i].activeSelf)
            {
                Enemy enemyLogic = enmeyL[i].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }
        for (int i = 0; i < enmeyM.Length; i++)
        {
            if (enmeyM[i].activeSelf)
            {
                Enemy enemyLogic = enmeyM[i].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }
        for (int i = 0; i < enmeyS.Length; i++)
        {
            if (enmeyS[i].activeSelf)
            {
                Enemy enemyLogic = enmeyS[i].GetComponent<Enemy>();
                enemyLogic.OnHit(100);
            }
        }

        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
        for (int i = 0; i < bulletsA.Length; i++)
        {
            bulletsA[i].SetActive(false);
        }
        for (int i = 0; i < bulletsB.Length; i++)
        {
            bulletsB[i].SetActive(false);
        }
    }

    private void Reload()
    {
        currentShotDelay += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop= true;
                    break;
                case "Bottom":
                    isTouchBottom= true; 
                    break;
                case "Left":
                    isTouchLeft= true; 
                    break;
                case "Right":
                    isTouchRight= true;
                    break;

            }
        }
        else if(collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            if (isHit) return;

            isHit = true;

            life--;
            gameManager.UpdateLifeIcon(life);

            if(life <= 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.ResapwnPlayer();
            }

            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();

            switch(item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if(power == maxPower)
                    {
                        score += 500;
                    }
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if(boom == maxBoom)
                        score += 500;
                    else
                        boom++;
                    gameManager.UpdateBoomIcon(boom);
                    break;
            }
            collision.gameObject.SetActive(false);
        }

    }

    private void AddFollower()
    {
        if(power == 4)
        {
            followers[0].SetActive(true);
        }
        else if(power == 5)
        {
            followers[1].SetActive(true);
        }
        else if (power == 6)
        {
            followers[2].SetActive(true);
        }
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime= false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;

            }
        }
    }
}
