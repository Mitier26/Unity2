using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float DISTANCE_CENTER = 4.25f;                 // 중심에서 떨어진 거리
    private const float ANGLE_CHANGE_SPEED = 100f;               // 이동 속도

    private float mAngle = 270f;                                // 처음 시작 위치


    private const float SELF_ANGLE_CHANGE_SPEED = 100f;
    private const float RETURE_TO_NORMAL_SPEED = 175f;
    private const float MAX_ANGLE = 55f;

    private float mSelfRotationAngle = 0f;


    private void Update()
    {
        if(Input.GetKey("left"))
        {
            MoveLeft();
        }
        else if(Input.GetKey("right"))
        {
            MoveRight();
        }
        else if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.position.x < 0.3f * Screen.width)
            {
                MoveLeft();
            }
            else if (touch.position.x > 0.7f * Screen.width)
            {
                MoveRight();
            }
            else
            {
                ReturnToNeutrla();
            }
            // 화면 크기에서 왼쪽 30% 부분과 오른고 70% 이상인 부분
            // 테스트하기 위해서는 시뮬레이터로 변경
        }
        else
        {
            ReturnToNeutrla();
        }
        // 입력을 받으면 각도를 증가 하거나 감소 시킨다.

        KeepAnglesInBounds();
        // 각도가 무한하게 감소하거나 증가하는 것을 막는다.

        float angleRads = mAngle / 360f * 2 * Mathf.PI; 
        // float  angleRads = Mathf.Deg2Rad * mAngle;
        // public const float Deg2Rad = (float)Math.PI / 180f;

        Vector3 pos = new Vector3(Mathf.Cos(angleRads) * DISTANCE_CENTER, Mathf.Sin(angleRads) * DISTANCE_CENTER, 0);
        transform.position = pos;
        // 라디안 각도를 이동하여 위치 ( x, y ,z ) 를 구하는 것 

        transform.eulerAngles = new Vector3(Mathf.Cos(angleRads) * mSelfRotationAngle, Mathf.Sin(angleRads) * mSelfRotationAngle, mAngle + 45f);
        // 회전 값을 이용하여 자기 자신을 회전한다.
        // 중앙을 계속 바라 본다, + 45는 처음 회전값
    }

    private void MoveLeft()
    {
        mAngle -= Time.deltaTime * ANGLE_CHANGE_SPEED;
        if (mSelfRotationAngle > 0)
        {
            mSelfRotationAngle -= Time.deltaTime * RETURE_TO_NORMAL_SPEED;
        }
        else
        {
            mSelfRotationAngle -= Time.deltaTime * SELF_ANGLE_CHANGE_SPEED;
        }
        // 키를 입력 받으면 몸통이 회전한다.
        // 키 입력이 끝나면 몸통을 기본회전 값으로 변경한다.
        // 몸통의 기본회번값으로 변경하는 도중 다시 키를 입력 받으면
        // 빠르게 회전
        // 그리고 오른쪽 키를 누르다 왼쪽을 누를경우 빠르게 회전 시키기 위해서
    }

    private void MoveRight()
    {
        mAngle += Time.deltaTime * ANGLE_CHANGE_SPEED;
        if (mSelfRotationAngle < 0)
        {
            mSelfRotationAngle += Time.deltaTime * RETURE_TO_NORMAL_SPEED;
        }
        else
        {
            mSelfRotationAngle += Time.deltaTime * SELF_ANGLE_CHANGE_SPEED;
        }
    }

    private void ReturnToNeutrla()
    {
        if (mSelfRotationAngle > 0f)
        {
            mSelfRotationAngle -= Time.deltaTime * RETURE_TO_NORMAL_SPEED;
            if (mSelfRotationAngle < 0)
            {
                mSelfRotationAngle = 0;
            }
        }
        else
        {
            mSelfRotationAngle += Time.deltaTime * RETURE_TO_NORMAL_SPEED;
            if (mSelfRotationAngle > 0)
            {
                mSelfRotationAngle = 0;
            }
        }
        // 키 입력을 else if 로 만든 이유
        // 키 입렵이 없을 때 기본회전으로 돌아가기 위해
    }

    private void KeepAnglesInBounds()
    {
        if (mSelfRotationAngle > MAX_ANGLE)
        {
            mSelfRotationAngle = MAX_ANGLE;
        }
        else if (mSelfRotationAngle < -MAX_ANGLE)
        {
            mSelfRotationAngle = -MAX_ANGLE;
        }

        if (mAngle > 360f)
        {
            mAngle -= 360f;
        }
        else if (mAngle < 0f)
        {
            mAngle += 360f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
