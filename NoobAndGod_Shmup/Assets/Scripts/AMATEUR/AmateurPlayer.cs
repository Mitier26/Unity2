using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurPlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.SetScore(7);
        }
        else if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            UIManager.instance.GameOver();
        }
    }
}
