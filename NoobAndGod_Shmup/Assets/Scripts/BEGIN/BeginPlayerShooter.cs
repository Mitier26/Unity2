using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingDelay;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float shootingTime;

    void Update()
    {
        shootingTime += Time.deltaTime;

        if(shootingTime > shootingDelay )
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shootingTime = 0;

                AudioManager.instance.PlaySfx(SFX.P_Shoot);

                GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                if (go.TryGetComponent(out BeginProjectile projectile))
                {
                    projectile.SetProjectile(Vector2.up, shootingSpeed);
                    Destroy(go, 5f);
                }
            }
        }

    }
}
