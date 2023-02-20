using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio;
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField]
    private int damage = 100;           // 폭탄의 공격력

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;

        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }

        animator.SetTrigger("onBoom");
        audioSource.clip = boomAudio;
        audioSource.Play();
    }

    public void OnBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        foreach(GameObject enemy in enemys) enemy.GetComponent<Enemy>().OnDie();
        foreach(GameObject meteorite in meteorites) meteorite.GetComponent<Meteorite>().OnDie();
        foreach(GameObject projectile in projectiles) projectile.GetComponent<EnemyProjectile>().OnDie();
        
        // 보스가 있을 때에만 작동 하도록 한다.
        if(boss != null)
            boss.GetComponent<BossHP>().TakeDamage(damage);

        Destroy(gameObject);
    }
}
