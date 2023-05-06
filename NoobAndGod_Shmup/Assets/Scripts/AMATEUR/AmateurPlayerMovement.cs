using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        // 플레이어 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World); 
        //transform.position += movement * moveSpeed * Time.deltaTime;

        // 마우스 방향으로 천천히 회전
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouse = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        float clampX = Mathf.Clamp(transform.localPosition.x, -4, 4);
        float clampY = Mathf.Clamp(transform.localPosition.y, -3, 3);
        transform.localPosition = new Vector3(clampX, clampY, 10);
    }
}
