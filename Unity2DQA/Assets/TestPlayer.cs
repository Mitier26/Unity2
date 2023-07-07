using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void LeftButton()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void RightButton() 
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void AttackButton()
    {
        animator.SetTrigger("Attack");
    }

}
