using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum STATE
{
    IDLE, SINGLE_PATTERN_1, ADDING_ENEMY, DOUBLE_PATTERN_PREPARE_1, DOUBLE_PATTERN_ACTION_1
}

public class GameManager : MonoBehaviour
{
    public EnemyPuppet enemyPrefab;

    private List<EnemyPuppet> enemies = new List<EnemyPuppet>();

    private STATE mState = STATE.IDLE;
    private float mCurrentStateStartTime;
    private float mNextTransitionTime;

    private float mSpeedModifier = 160f;
    private float mFireRateModifier = 1f;

    private float mGameStartTime;
    private float mTargetSpread;

    private void Start()
    {
        mGameStartTime = Time.time;
        mCurrentStateStartTime = Time.time;
        mNextTransitionTime = mCurrentStateStartTime + 1;

        EnemyPuppet enemy = Instantiate(enemyPrefab, new Vector3(1,0,0), Quaternion.identity);
        enemies.Add(enemy);

        // ������ ���۵Ǹ� �� �ð����� �ʱ�ȭ �Ѵ�.
        // Time.time �� ������ ���۵� ���� ����� �ð��̴�.
        // ������ ���� 1�� �����Ѵ�.
    }

    private void Update()
    {
        // ��� �ð��� ���� �ð� ���� ũ�� �ٸ� ���� �����Ѵ�.
        // �׷��ٴ� �Ŵ� nNextTransitionTime�� Time.time���� ū ��
        // nNextTransitionTime = Time.time + 10f ������ �ɰ��� ������ �� �ִ�.
        if(Time.time > mNextTransitionTime)
        {
            TransitionToNextState();
            // �̰��� ���� ����, �߰�
        }

        // ���� �ൿ ������ ����
        HandleCurrentState();
    }

    private void HandleCurrentState()
    {
        // ������ ���°��� ���� �ٸ����� �����Ѵ�.
        switch (mState)
        {
            case STATE.IDLE:
                HandleStateIdle();
                break;
            case STATE.SINGLE_PATTERN_1:
                HandleSinglePattern1();
                break;
            case STATE.ADDING_ENEMY:
                HandleAddingEnemy();
                break;
            case STATE.DOUBLE_PATTERN_PREPARE_1:
                HandlePrepareDoubleStatePettern1();
                break;
            case STATE.DOUBLE_PATTERN_ACTION_1:
                HandleDoubleStatePattern1();
                break;
        }
    }

    private void TransitionToNextState()
    {
        // ������ ���°� IDLE �̰�
        if (mState == STATE.IDLE)
        {
            // mGameStartTime�� ���� ���� �� 1�� �ʱ�ȭ�Ѵ�. Time.time ����
            // ������ �����ϰ� 15�ʰ� �Ѿ��µ� ���� 1�� �̸� ������ �����Ѵ�.
            if(Time.time - mGameStartTime > 15f && enemies.Count == 1)
            {
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3f;
                // ���¸� [ �� �߰� ] ���·� �����ϰ� 
                // mNextTransitionTime�� ���� �ð����� 3�ʸ� ���� �ð����� �����Ѵ�.
                // mNextTransitionTime�� Update ���� ���
            }
            // ���� 1�� ������ ���Ӱ�� �ð��� 15�� ���� �� ��
            // mNextTransitionTime�� ���� �ð��� 3�� ����
            // ���¸� �����ϴ� ���� 3�� �Ŀ� ����ȴ�.
            else if (enemies.Count == 1)
            {
                TransitionToNextSingleState();
                // mNextTransitionTime �� ���� �ش� �޼��尡 ����ȴ�.
                // ���⼭ ���¸� SINGEL_PATTERN_1�� �����Ѵ�.
                // mNextTransitionTime �ʰ� ������ ���°� SINGLE_PATTERN_1 �� �����ֱ� ������
                // �����ִ� ���� ������� �ʰ� �Ʒ� ���� ����ȴ�.
            }
            // ���� 1�� �϶�
            else if (enemies.Count == 2)
            {
                TransitionToNextDoubleState();
            }
        }
        else if (mState == STATE.ADDING_ENEMY)
        {
            mState = STATE.IDLE;
            mNextTransitionTime = Time.time + Random.Range(0f, 1.5f);
            
            EnemyPuppet enemy = Instantiate(enemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
            int totalEnemies = enemies.Count +1;
            float angle = 360f / totalEnemies;
            enemy.OverrideAngle(360f - angle);
            
            enemies.Add(enemy);
            // ���� 1�� �߰��Ѵ�.
            // enemies.Add(enemy); �� �����ϰ� float angle = 360f / totalEnemies; �̰��� �ϸ�
            // + 1 �� ���ص� �ɰ� ������.
            // ��ġ ���� ������ enemy�� �߰��ϱ����� �̷��� �ߴ�.
            // float angle = 360f / totalEnemies; ���� ���ڿ� ���� ������ �����ϴ°�
            // 1 �� 0��
            // 2�� 360 / 2 = 180 
            // 360 - 180 = 180 ���� 180�� �ȴ�.
        }
        else
        {
            mState = STATE.IDLE;
            mNextTransitionTime = Time.time + Random.Range(0f, 1.5f);
        }
        
        mCurrentStateStartTime = Time.time;
    }

    private void TransitionToNextSingleState()
    {
        mState = STATE.SINGLE_PATTERN_1;
        mNextTransitionTime = Time.time + 5f;
        if(Random.Range(0f , 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(140f, 200f);
            mFireRateModifier = Random.Range(1f, 4f);
        }
        else
        {
            mSpeedModifier = Random.Range(-200f, -140f);
            mFireRateModifier = Random.Range(1f, 4f);
        }
        mCurrentStateStartTime = Time.time;
    }

    private void TransitionToNextDoubleState()
    {
        if(Random.Range(0f, 1f) < 0.2f)
        {
            mState = STATE.DOUBLE_PATTERN_PREPARE_1;
            mNextTransitionTime = Time.time + 3f;
            mTargetSpread= Random.Range(30f, 180f);
        }
        else
        {
            if(Random.Range(0f, 1f) < 0.5)
            {
                mState = STATE.SINGLE_PATTERN_1;
                mNextTransitionTime = Time.time + 5f;
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    mSpeedModifier = Random.Range(100f, 150f);
                    mFireRateModifier = Random.Range(1f, 4f);
                }
                else
                {
                    mSpeedModifier = Random.Range(-150f, -100f);
                    mFireRateModifier = Random.Range(1f, 4f);
                }
            }
            else
            {
                mState = STATE.DOUBLE_PATTERN_ACTION_1;
                mNextTransitionTime = Time.time + 5f;
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    mSpeedModifier = Random.Range(100f, 150f);
                    mFireRateModifier = Random.Range(1f, 2f);
                }
                else
                {
                    mSpeedModifier = Random.Range(-150f, -100f);
                    mFireRateModifier = Random.Range(1f, 2f);
                }
            }
            
        }

        mCurrentStateStartTime = Time.time;
    }


    // �Ʒ��� ����, ����, �̵� �ϴ� �κ�
    private void HandleStateIdle()
    {
        enemies.ForEach(enemy =>
        {
            enemy.SetFireBulletRate(0.0f);
        });
    }

    private void HandleSinglePattern1()
    {
        // ������ ��� �ִ� ����Ʈ�� �ִ� ��� �Ϳ� ����� �����Ѵ�.
        enemies.ForEach(enemy =>
        {
            float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
            // mCurrentStateStartTime�� �ð� ���� ���̶�� ���� �Ѵ�.
            // progress = Time.time / mNextTransitionTime �̶�� ��
            // pecent ���� = ���ϴ°� /  �ִ�
            // ���ϴ� ���� �ִ�� �������� 100%
            if (progress > 0.5f)
            {
                float speed = Mathf.Lerp(mSpeedModifier, 0, 2 * (progress - 0.5f));
                enemy.SetRotationSpeed(speed);
                enemy.SetFireBulletRate((progress - 0.5f) / mFireRateModifier);
                // �� 50%�� �̷��� �����δ�.
            }
            else
            {
                float speed = Mathf.Lerp(0, mSpeedModifier, 2 * progress);
                enemy.SetRotationSpeed(speed);
                enemy.SetFireBulletRate((0.5f - progress) / mFireRateModifier);
            }
        });
    }

    // �̰��� ���� �߰����� ������ �̸� ����Ǵ� ���̴�.
    // ������ �߰��� ���� �����ϰ� �̸� �ڸ��� ��� ��.
    private void HandleAddingEnemy()
    {
        int totalEnemies = enemies.Count +1 ;
        float angle = 360f / totalEnemies;

        for(int i = 0; i < enemies.Count; i++)
        {
            EnemyPuppet enmey = enemies[i];
            enmey.SetTargetAngle(angle * i);
            enmey.SetFireBulletRate(0);
        }
    }
    
    private void HandlePrepareDoubleStatePettern1()
    {
        EnemyPuppet firstEnemy = enemies[0];
        EnemyPuppet secondEnemy = enemies[1];
        float firstAngle = firstEnemy.GetAngle();
        float secondAngle = secondEnemy.GetAngle();
        float firstTargetAngle = firstAngle + mTargetSpread;
        float secondTargetAngle = firstAngle - mTargetSpread;

        float firstDistance = Mathf.Abs(firstTargetAngle - secondAngle);
        if(firstDistance > 360)
        {
            firstDistance -= 360;
        }

        float secondDistance = Mathf.Abs(secondTargetAngle - secondAngle);
        if(secondDistance > 360)
        {
            secondDistance -= 360;
        }

        if(firstDistance < secondDistance)
        {
            secondEnemy.SetTargetAngle(firstTargetAngle);
        }
        else
        {
            secondEnemy.SetTargetAngle(secondTargetAngle);
        }

    }

    private void HandleDoubleStatePattern1()
    {
        float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
        if (progress > 0.5f)
        {
            float speed = Mathf.Lerp(mSpeedModifier, 0, 2 * (progress - 0.5f));
            enemies[0].SetRotationSpeed(speed);
            enemies[1].SetRotationSpeed(-speed);
            enemies[0].SetFireBulletRate((progress - 0.5f) / mFireRateModifier);
            enemies[1].SetFireBulletRate((progress - 0.5f) / mFireRateModifier);
        }
        else
        {
            float speed = Mathf.Lerp(0, mSpeedModifier, 2 * progress);
            enemies[0].SetRotationSpeed(speed);
            enemies[1].SetRotationSpeed(-speed);
            enemies[0].SetFireBulletRate((0.5f - progress) / mFireRateModifier);
            enemies[1].SetFireBulletRate((0.5f - progress) / mFireRateModifier);
        }
    }
}
