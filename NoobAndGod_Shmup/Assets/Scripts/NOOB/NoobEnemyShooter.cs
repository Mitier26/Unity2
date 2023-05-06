using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobEnemyShooter : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject bulletPrefab;
    [SerializeField] private Transform playerController;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float shootingTime;
    [SerializeField] private float shootingDelay = 2f;
    [SerializeField] private NoobManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<NoobManager>();
        RandShooting();
    }

    private void Update()
    {
        if (shootingTime > shootingDelay)
        {
            RandShooting();
            Fire();
            shootingTime = 0;
        }
        shootingTime += Time.deltaTime;
    }

    private void RandShooting()
    {
        shootingDelay = Random.Range(2 - (0.3f * stageManager.level), 5f -(0.75f * stageManager.level));
        bulletSpeed = Random.Range(1 + (0.7f * stageManager.level), 5 + (0.5f * stageManager.level));
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        if (bulletPrefab.TryGetComponent(out NoobProjectile projectile))
        {
            projectile.SetProjectile(SetDirection(), bulletSpeed);
        }
    }

    private Vector2 SetDirection()
    {
        float rand = Random.Range(0, 3);

        if (rand == 1)
        {
            float randX = Random.Range(-5f, 5f);
            return new Vector2(randX, playerController.transform.position.y).normalized;
        }
        else if (rand == 2 && playerController != null)
        {
            Vector2 dir = playerController.position - transform.position;
            return dir.normalized;
        }
        else
        {
            return Vector2.down;
        }
    }
}
