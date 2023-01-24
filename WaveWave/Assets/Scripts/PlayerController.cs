using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement;

    [SerializeField] private StageController stageController;       // ���ӿ��� Ȯ��, �浿 �� �۵�

    [SerializeField] private GameObject playerDieEffect;            // ���ӿ����� ����� ����Ʈ

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

            // ���� ȿ�� ����
            Destroy(GetComponent<Rigidbody2D>());

            stageController.GameOver();
        }
    }
}
