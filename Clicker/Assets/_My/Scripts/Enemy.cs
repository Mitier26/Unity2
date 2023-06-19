using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EnemyOnClick()
    {
        anim.SetTrigger("Hit");
    }

    public void EnemyDie()
    {
        Destroy(gameObject);
    }
}
