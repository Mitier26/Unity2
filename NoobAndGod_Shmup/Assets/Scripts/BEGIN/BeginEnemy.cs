using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginEnemy : MonoBehaviour
{
    [SerializeField] private float hp = 3;
    [SerializeField] private float score;

    public float HP
    {
        get { return hp; } 
        set
        {
            hp = value;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BeginProjectile projectile))
        {
            Destroy(projectile.gameObject);

            hp -= projectile.GetDamage();

            if(hp <= 0 )
            {
                AudioManager.instance.PlaySfx(SFX.E_Explosion);
                GameManager.instance.SetScore(score);
                Destroy(gameObject);
            }
        }
    }
}
