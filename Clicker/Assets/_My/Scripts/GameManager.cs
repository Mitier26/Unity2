using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private Animator anim;

    [Header("Audio")]
    public AudioClip attackClip;
    public AudioClip hitClip;
    private AudioSource audioSource;

    [Header("Enmey")]
    public GameObject[] enemyObjs;
    public GameObject enemySpawn;
    private bool isSpawn = true;

    [Header("UI")]
    public Image imageEnemyHP;
    public Text textGold;
    public Text textPayGold;
    public Text textStageCount;
    public Text textEnemyCount;

    private bool isAttack = false;
    private float attackTime = 0f;
    private float hitTime = 0.3f;

    private Settings setting;

    private void Start()
    {
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setting = GetComponent<Settings>();

        textGold.text = setting.stringGold();
        textPayGold.text = setting.stringPayGold();
        textStageCount.text = setting.stage.ToString();
        textEnemyCount.text = setting.enemyCount.ToString();
    }

    private void Update()
    {
        EnemySpawn();
        MouseOnClick();
        ShowEnemyHP();
    }

    private bool isAttackChk()
    {

        if (isAttack)
        {
            attackTime += Time.deltaTime;

            if (attackTime > hitTime)
            {
                attackTime = 0f;
                isAttack = false;
            }
        }

        return isAttack;
    }

    private void EnemyAttack(RaycastHit2D hit)
    {
        Enemy enemy = hit.collider.GetComponent<Enemy>();

        AudioSource enemyAudio = hit.collider.GetComponent<AudioSource>();

        if (enemy != null)
        {
            enemy.EnemyOnClick();

            enemyAudio.clip = hitClip;
            enemyAudio.volume = 0.5f;
            enemyAudio.Play();

            anim.SetTrigger("Attack");
            audioSource.clip = attackClip;
            audioSource.Play();

            isAttack = true;

            if (setting.IsEnemyDie())
            {
                enemy.EnemyDie();
                setting.GetEnemyHP();

                setting.GetGold();
                textGold.text = setting.stringGold();

                isSpawn = true;
            }
        }
    }


    private void MouseOnClick()
    {

        if (isAttackChk()) return;

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if(hit.collider != null && hit.collider.tag == "Enemy")
            {
               EnemyAttack(hit);
            }
        }
    }

    private void EnemySpawn()
    {
        if(isSpawn)
        {
            StartCoroutine(EnemySpawnTime());
            isSpawn = false;
        }
    }

    IEnumerator EnemySpawnTime()
    {
        yield return new WaitForSeconds(1f);

        setting.enemyCount -= 1;
        if (setting.enemyCount <= 0)
        {
            setting.stage += 1;
            textStageCount.text = setting.stage.ToString();
            setting.enemyCount = 6;
            setting.InitEnemyHP();
        }

        textEnemyCount.text = setting.enemyCount.ToString();
        int ran = Random.Range(0, enemyObjs.Length);
        Instantiate(enemyObjs[ran], enemySpawn.transform.position, Quaternion.identity);
    }

    private void ShowEnemyHP()
    {
        imageEnemyHP.fillAmount = setting.GetEnemyHpVal();
    }

    public void ButtonLvUp()
    {
        setting.LvUpPayGold();
        textGold.text = setting.stringGold();
        textPayGold.text = setting.stringPayGold();
    }

    public void SumGold()
    {
        setting.SumGold();
        textGold.text = setting.stringGold();
    }
}
