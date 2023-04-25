using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobEnemyController : MonoBehaviour
{
    [SerializeField] private NoobStageManager noobStageManager;
    [SerializeField] private float score;

    private void Start()
    {
        noobStageManager = FindObjectOfType<NoobStageManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySfx(SFX.E_Explosion);
            //  점수 증가
            GameManager.instance.SetScore(score);
            noobStageManager.DecreaseEnemyCount();
            Destroy(gameObject);
        }
    }
}
