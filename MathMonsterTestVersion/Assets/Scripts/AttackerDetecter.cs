using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerDetecter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
            GameManager.Instance.player.SetFarEnemies(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            GameManager.Instance.player.ExitFarEnemies(collision.gameObject);
    }
}
