using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeginnerPlayer : MonoBehaviour
{
    private Vector3 direction;                  // 이동의 방향
    [SerializeField]  
    private float moveSpeed;                    // 이동의 속도

    private Vector2 touchStartPosition;         // 화면을 터치 했을 때 위치

    public Slider slider;

    private void Start()
    {   
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            direction = new Vector3(inputX, 0, 0);
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < touchStartPosition.x)
                    {
                        direction = Vector3.left;
                    }
                    else
                    {
                        direction = Vector3.right;
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    direction = Vector3.zero;
                }
            }
        }
        else if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            float mouseX = Input.mousePosition.x;           // 마우스의 위치값중 X 값을 저장한다.
            float screenCenterX = Screen.width / 2;         // 화면의 절반, 중앙

            if (Input.GetMouseButton(0))
            {
                // 마우스를 클릭 했을 때 마우스의 위치가
                // 중앙 보다 왼쪽에 있으면 왼쪽으로 이동, 오른족에 있으면 오른쪽으로 이동한다.
                if (mouseX < screenCenterX)
                {
                    direction = Vector3.left;
                }
                else if (mouseX > screenCenterX)
                {
                    direction = Vector3.right;
                }
            }
            else
            {
                // 마우스를 땟을 때 멈춘다.
                direction = Vector3.zero;
            }
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            transform.position = new Vector3(slider.value, transform.position.y, 0f);
        }

        //transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            BeginnerManager.instance.AddScore(7);
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Obstacle"))
        {
            BeginnerManager.instance.Death();
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
