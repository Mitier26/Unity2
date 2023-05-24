using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderDetecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GameManager.Instance.player.SetNearEnemies(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GameManager.Instance.player.ExitNearEnemies(collision.gameObject);
    }   
}
