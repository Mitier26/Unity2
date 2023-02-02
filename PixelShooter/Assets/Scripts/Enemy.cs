using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public int enemyScore;
    public float speed;
    public int health;
    public int currenthealth;
    public Sprite[] sprites;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public float maxShotDelay;
    public float currentShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject ItemCoin;
    public GameObject ItemBoom;
    public GameObject ItemPower;

    public GameObject player;
    public ObjectManager objectManager;

    Animator anim;

    public int patternIndex;
    public int currentPatternCount;
    public int[] maxPatternCount;

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();

        if(enemyName == "B")
        {
            anim  = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        currenthealth = health;

        if(enemyName == "B")
        {
            Invoke("Stop", 2f);
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf) return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        currentPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        GameObject bulletR = objectManager.MakeObject("BulletEnemyA");
        GameObject bulletRR = objectManager.MakeObject("BulletEnemyA");
        GameObject bulletL = objectManager.MakeObject("BulletEnemyA");
        GameObject bulletLL = objectManager.MakeObject("BulletEnemyA");

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        rigidR.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 6, ForceMode2D.Impulse);

        currentPatternCount++;

        if(currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 2);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    void FireShot()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject bullet = objectManager.MakeObject("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
       


        currentPatternCount++;

        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 2);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    void FireArc()
    {
       
            GameObject bullet = objectManager.MakeObject("BulletEnemyA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 10 * currentPatternCount / maxPatternCount[patternIndex]), -1f);
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        

        currentPatternCount++;

        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireAround()
    {
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = currentPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = objectManager.MakeObject("BulletEnemyC");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 2 * i / roundNum), Mathf.Cos(Mathf.PI * 2 * i / roundNum));
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward*90;
            bullet.transform.Rotate(rotVec);
        }
       

        currentPatternCount++;

        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 2);
        }
    }

    public void OnHit(int damage)
    {
        if (currenthealth <= 0) return;

        currenthealth -= damage;

        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];

            Invoke("ReturnSprite", 0.1f);
        }
        

        if(currenthealth <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);

            if(ran < 5)
            {

            }
            else if( ran < 8)
            {
                GameObject itemCoin = objectManager.MakeObject("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran <9)
            {
                GameObject itemPower = objectManager.MakeObject("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if(ran < 10)
            {
                GameObject itemBoom = objectManager.MakeObject("ItemBoom");
                itemBoom.transform.position = transform.position;
            }

            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (enemyName == "B") return;
        Fire();
        Reload();
    }

    private void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BorderBullet") && enemyName != "B")
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        if(collision.CompareTag("PlayerBullet"))
        {
            OnHit(collision.GetComponent<Bullet>().damage);
            collision.gameObject.SetActive(false);
        }
    }

    private void Fire()
    {
        if (currentShotDelay < maxShotDelay) return;

        Vector3 dirVec = (player.transform.position - transform.position).normalized;

        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObject("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            
            rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObject("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            rigidR.AddForce(dirVec * 3, ForceMode2D.Impulse);

            GameObject bulletL = objectManager.MakeObject("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            rigidL.AddForce(dirVec * 3, ForceMode2D.Impulse);
        }

        currentShotDelay = 0;
    }

    private void Reload()
    {
        currentShotDelay += Time.deltaTime;
    }
}
