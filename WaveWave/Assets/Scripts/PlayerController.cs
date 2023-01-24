using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement;

    [SerializeField] private StageController stageController;       // 게임오버 확인, 충동 시 작동

    [SerializeField] private GameObject playerDieEffect;            // 게임오버시 출력할 이펙트

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
    }

    private void FixedUpdate()
    {
        if (stageController.IsGameOver == true) return;

        movement.MoveToX();

        if(Input.GetMouseButton(0))
        {
            movement.MoveToY();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            stageController.IncreaseScore(1);

            //Destroy(collision.gameObject);

            collision.GetComponent<Item>().Exit();
        }
        else if(collision.CompareTag("Obstacle"))
        {
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);

            // 물리 효과 제거
            Destroy(GetComponent<Rigidbody2D>());

            stageController.GameOver();
        }
    }
}
