using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobPlayerController : MonoBehaviour
{
    // 플레이어는 좌우 이동만 가능 하다.

    [SerializeField] private NoobProjectile bulletPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bulletMoveSpeed = 5f;
    [SerializeField] private float bulletInterval = 0.3f;
    [SerializeField] private float shootingTime = 0;
    [SerializeField] private float invincibilityTime;
    private SpriteRenderer SpriteRenderer; 
    private bool isInvincibility = false;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        shootingTime = bulletInterval;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) && transform.position.x >= -3.5f)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D) && transform.position.x <= 3.5f)
        { 
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0 , 0); 
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if(shootingTime > bulletInterval)
        {
            shootingTime = 0;
            AudioManager.instance.PlaySfx(SFX.P_Shoot);
            UnityEngine.GameObject go = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);

            if (go.TryGetComponent(out NoobProjectile projectile))
            {
                projectile.SetProjectile(Vector2.up, bulletMoveSpeed);
            }
            
        }
        shootingTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);

            if (isInvincibility) return;
            // 점수 감소
            GameManager.instance.SetScore(-1);
            StartCoroutine(Invincibility());

        }
    }

    private IEnumerator Invincibility()
    {
        isInvincibility = true;
        SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(invincibilityTime);
        SpriteRenderer.color = Color.white;
        isInvincibility = false;
    }
}
