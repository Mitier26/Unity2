using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupporterDetecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GameManager.Instance.player.SetMediumEnemies(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GameManager.Instance.player.ExitMediumEnemies(collision.gameObject);
    }
}
