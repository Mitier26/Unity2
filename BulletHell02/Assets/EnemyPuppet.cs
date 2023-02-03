using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPuppet : MonoBehaviour
{
    private const float DISTANCE_CENTER = 2f;
    private const float DISTANCE_EDGE = 0.5f;
    private const float AUTO_MOVEMENT_SPEED = 50f;

    public Bullet bulletPrefab;
    private float mAngle;
    private float mRotateSpeed;
    private float mFireRate;
    private float mNextFireTime = -1;
    private float mTargetAngle = -1;

    private void Start()
    {
        
    }
    private void Update()
    {
        // mTargetAngle �� ���� �����Ǵ°�?
        // ó�� ���� 1�� �߰��ɶ� 1�� ����ȴ�.
        // ���� �����̴� �ܰ�, ���� �߰��� �� �Ŀ��� ������� mTargetAngle ������ �ʴ´�.
        if (mTargetAngle >= 0)
        {
            float minAngle = mTargetAngle - 180f;
            if(minAngle < 0)
            {
                if(( mAngle < mTargetAngle) || mAngle > minAngle + 360)
                {
                    mAngle = mAngle + (Time.deltaTime * AUTO_MOVEMENT_SPEED);
                    if(mAngle > mTargetAngle && mAngle < mTargetAngle + 180)
                    {
                        mAngle = mTargetAngle;
                        mTargetAngle = -1;
                    }
                }
                else
                {
                    mAngle = mAngle - (Time.deltaTime * AUTO_MOVEMENT_SPEED);
                    if (mAngle < mTargetAngle)
                    {
                        mAngle = mTargetAngle;
                        mTargetAngle = -1;
                    }
                }
            }
            else
            {
                if(mAngle > minAngle && mAngle < mTargetAngle)
                {
                    mAngle = mAngle + (Time.deltaTime * AUTO_MOVEMENT_SPEED);
                    if (mAngle > mTargetAngle)
                    {
                        mAngle = mTargetAngle;
                        mTargetAngle = -1;
                    }
                }
                else
                {
                    mAngle = mAngle - (Time.deltaTime * AUTO_MOVEMENT_SPEED);
                    if (mAngle < mTargetAngle)
                    {
                        mAngle = mTargetAngle;
                        mTargetAngle = -1;
                    }
                }
            }
        }
        else
        {
            mAngle += mRotateSpeed * Time.deltaTime;
        }

        if (mAngle < 0)
        {
            mAngle += 360;
        }
        else if (mAngle > 360)
        {
            mAngle -= 360;
        }

        float angleRads = mAngle / 360f * 2 * Mathf.PI;

        Vector3 pos = new Vector3(Mathf.Cos(angleRads) * DISTANCE_CENTER, Mathf.Sin(angleRads) * DISTANCE_CENTER, 0);
        transform.position = pos;

        transform.eulerAngles = new Vector3(0, 0, mAngle + 45f);

        if(mFireRate > 0 && mNextFireTime < 0)
        {
            mNextFireTime = mFireRate + Time.time;
        }

        if(Time.time > mNextFireTime && mNextFireTime > 0)
        {
            FireBullet();
            mNextFireTime = -1;
        }
    }

    public void SetRotationSpeed(float speed)
    {
        mRotateSpeed = speed;
    }

    public void SetFireBulletRate(float fireRate)
    {
        mFireRate = fireRate;
    }

    public void SetTargetAngle(float angle)
    {
        mTargetAngle = angle;
    }

    public void OverrideAngle(float angle)
    {
        mAngle = angle;
    }

    public float GetAngle()
    {
        return mAngle;
    }

    private void FireBullet()
    {
        float angleRads = mAngle / 360f * 2 * Mathf.PI;
        Vector3 startPosition = transform.position + new Vector3(Mathf.Cos(angleRads) * DISTANCE_EDGE, Mathf.Sin(angleRads) * DISTANCE_EDGE, 0);

        Bullet bullet = Instantiate(bulletPrefab, startPosition, Quaternion.identity);
        bullet.SetDirection(new Vector3(Mathf.Cos(angleRads), Mathf.Sin(angleRads), 0).normalized);
    }
}
