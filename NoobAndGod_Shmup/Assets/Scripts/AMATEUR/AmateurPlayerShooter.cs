using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurPlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float bulletSpeed = 8f;

    private float shootTime = 0;

    private void Update()
    {
        shootTime += Time.deltaTime;

        if (shootTime < 0.2f) return;

        if(Input.GetKey(KeyCode.Space))
        {
            AudioManager.instance.PlaySfx(SFX.P_Shoot);

            GameObject go = AmateurPoolManager.instance.GetObject(AmateurObject.P_Bullet);
            go.transform.position = shootPosition.position;
            

            if(go.TryGetComponent(out AmateurBullet bullet) )
            {
                bullet.SetBullet(Vector2.up, bulletSpeed, transform.rotation);
            }

            shootTime = 0;
        }
    }
}
