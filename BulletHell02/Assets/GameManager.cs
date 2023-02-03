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

        // 게임이 시작되면 각 시간들을 초기화 한다.
        // Time.time 은 게임이 시작된 이후 경과된 시간이다.
        // 최초의 적은 1개 생산한다.
    }

    private void Update()
    {
        // 경과 시간이 다음 시간 보다 크면 다른 것을 실행한다.
        // 그렇다는 거는 nNextTransitionTime느 Time.time보다 큰 값
        // nNextTransitionTime = Time.time + 10f 형식이 될것을 예상할 수 있다.
        if(Time.time > mNextTransitionTime)
        {
            TransitionToNextState();
            // 이것은 상태 변경, 추가
        }

        // 적의 행동 실행은 여기
        HandleCurrentState();
    }

    private void HandleCurrentState()
    {
        // 정해진 상태값에 따라 다른것을 실행한다.
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
        // 게임의 상태가 IDLE 이고
        if (mState == STATE.IDLE)
        {
            // mGameStartTime는 게임 시작 시 1번 초기화한다. Time.time 으로
            // 게임을 시작하고 15초가 넘었는데 적의 1개 이면 다음을 실행한다.
            if(Time.time - mGameStartTime > 15f && enemies.Count == 1)
            {
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3f;
                // 상태를 [ 적 추가 ] 상태로 변경하고 
                // mNextTransitionTime를 현재 시간에서 3초를 더한 시간으로 변경한다.
                // mNextTransitionTime는 Update 에서 사용
            }
            // 적이 1개 이지만 게임경과 시간이 15초 이하 일 때
            // mNextTransitionTime이 현재 시간의 3초 이후
            // 상태를 변경하는 것은 3초 후에 실행된다.
            else if (enemies.Count == 1)
            {
                TransitionToNextSingleState();
                // mNextTransitionTime 초 동안 해당 메서드가 실행된다.
                // 여기서 상태를 SINGEL_PATTERN_1로 변경한다.
                // mNextTransitionTime 초가 지나면 상태가 SINGLE_PATTERN_1 로 변해있기 때문에
                // 여기있는 것이 실행되지 않고 아래 것이 실행된다.
            }
            // 적이 1개 일때
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
            // 적을 1개 추가한다.
            // enemies.Add(enemy); 을 먼저하고 float angle = 360f / totalEnemies; 이것을 하면
            // + 1 을 안해도 될것 같지만.
            // 위치 값을 변경한 enemy를 추가하기위해 이렇게 했다.
            // float angle = 360f / totalEnemies; 적의 숫자에 따라 각도를 변경하는것
            // 1 개 0도
            // 2개 360 / 2 = 180 
            // 360 - 180 = 180 으로 180이 된다.
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


    // 아래는 실행, 공격, 이동 하는 부분
    private void HandleStateIdle()
    {
        enemies.ForEach(enemy =>
        {
            enemy.SetFireBulletRate(0.0f);
        });
    }

    private void HandleSinglePattern1()
    {
        // 적들이 들어 있는 리스트에 있는 모든 것에 명령을 전달한다.
        enemies.ForEach(enemy =>
        {
            float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
            // mCurrentStateStartTime은 시간 보정 값이라고 봐야 한다.
            // progress = Time.time / mNextTransitionTime 이라는 뜻
            // pecent 계산법 = 변하는거 /  최대
            // 변하는 것이 최대랑 같아지면 100%
            if (progress > 0.5f)
            {
                float speed = Mathf.Lerp(mSpeedModifier, 0, 2 * (progress - 0.5f));
                enemy.SetRotationSpeed(speed);
                enemy.SetFireBulletRate((progress - 0.5f) / mFireRateModifier);
                // 앞 50%는 이렇게 움직인다.
            }
            else
            {
                float speed = Mathf.Lerp(0, mSpeedModifier, 2 * progress);
                enemy.SetRotationSpeed(speed);
                enemy.SetFireBulletRate((0.5f - progress) / mFireRateModifier);
            }
        });
    }

    // 이것은 적을 추가하지 이전에 미리 실행되는 것이다.
    // 다음에 추가될 적을 예상하고 미리 자리를 잡는 것.
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
