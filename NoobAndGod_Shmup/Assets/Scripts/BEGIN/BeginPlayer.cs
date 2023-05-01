using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPlayer : MonoBehaviour
{
    [SerializeField] private float maxHp = 10f;
    [SerializeField] private float currentHp = 10f;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            currentHp -= collision.GetComponent<BeginEnemy>().HP / 3;
            
            UIManager.instance.SetSlider(currentHp / maxHp);

            if(currentHp <= 0 )
            {
                
                // 게임 오버
                UIManager.instance.GameOver();
            }
        }
    }
}
