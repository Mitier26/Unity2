using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool firstLineBlueCar, firstLineOrangeCar;
    public bool blueCar;

    public Vector2 xPos;

    private void Update()
    {
        if(blueCar)
        {
            if(firstLineBlueCar)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(-xPos.y, transform.position.y, 0), 0.1f);
                // 0 ~ 1 ���� �� ������ ������ ������ 0.1 10 ������ ���� �۵�
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(-xPos.x, transform.position.y, 0), 0.1f);
            }
        }
        else
        {
            if(firstLineOrangeCar)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xPos.x, transform.position.y, 0), 0.1f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xPos.y, transform.position.y, 0), 0.1f);
            }
        }
    }

    public void LeftButtonPressed()
    {
        if(firstLineBlueCar)
        {
            firstLineBlueCar = false;
        }
        else
        {
            firstLineBlueCar = true;
        }
    }

    public void RightButtonPressed()
    {
        if (firstLineOrangeCar)
        {
            firstLineOrangeCar = false;
        }
        else
        {
            firstLineOrangeCar = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Circle"))
        {
            GameManager.instance.UpdateScore();
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Square"))
        {
            GameManager.instance.GameOver();
        }
    }
}
