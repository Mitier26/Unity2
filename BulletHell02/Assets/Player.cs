using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float DISTANCE_CENTER = 4.25f;                 // �߽ɿ��� ������ �Ÿ�
    private const float ANGLE_CHANGE_SPEED = 100f;               // �̵� �ӵ�

    private float mAngle = 270f;                                // ó�� ���� ��ġ


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
            // ȭ�� ũ�⿡�� ���� 30% �κа� ������ 70% �̻��� �κ�
            // �׽�Ʈ�ϱ� ���ؼ��� �ùķ����ͷ� ����
        }
        else
        {
            ReturnToNeutrla();
        }
        // �Է��� ������ ������ ���� �ϰų� ���� ��Ų��.

        KeepAnglesInBounds();
        // ������ �����ϰ� �����ϰų� �����ϴ� ���� ���´�.

        float angleRads = mAngle / 360f * 2 * Mathf.PI; 
        // float  angleRads = Mathf.Deg2Rad * mAngle;
        // public const float Deg2Rad = (float)Math.PI / 180f;

        Vector3 pos = new Vector3(Mathf.Cos(angleRads) * DISTANCE_CENTER, Mathf.Sin(angleRads) * DISTANCE_CENTER, 0);
        transform.position = pos;
        // ���� ������ �̵��Ͽ� ��ġ ( x, y ,z ) �� ���ϴ� �� 

        transform.eulerAngles = new Vector3(Mathf.Cos(angleRads) * mSelfRotationAngle, Mathf.Sin(angleRads) * mSelfRotationAngle, mAngle + 45f);
        // ȸ�� ���� �̿��Ͽ� �ڱ� �ڽ��� ȸ���Ѵ�.
        // �߾��� ��� �ٶ� ����, + 45�� ó�� ȸ����
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
        // Ű�� �Է� ������ ������ ȸ���Ѵ�.
        // Ű �Է��� ������ ������ �⺻ȸ�� ������ �����Ѵ�.
        // ������ �⺻ȸ�������� �����ϴ� ���� �ٽ� Ű�� �Է� ������
        // ������ ȸ��
        // �׸��� ������ Ű�� ������ ������ ������� ������ ȸ�� ��Ű�� ���ؼ�
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
        // Ű �Է��� else if �� ���� ����
        // Ű �Է��� ���� �� �⺻ȸ������ ���ư��� ����
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
