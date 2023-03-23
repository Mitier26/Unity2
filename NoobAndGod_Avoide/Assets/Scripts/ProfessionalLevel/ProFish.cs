using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProFish : MonoBehaviour
{
    private IObjectPool<GameObject> managedPool;

    [SerializeField]
    GameObject indicator, water, fish;      // 작동할 오브젝트

    [SerializeField]
    private float fishPower = 600f;

    private float waterTime = 0.6f;         // 물기둥이 올라오는 시간
    private float timeElapsed;              // 시간의 경과
    private float percent;                  // 작동의 퍼센트

    private float waterStartPos;            // 물기둥의 초기 위치
    private float waterEndPos = -5.8f;      // 물기둥의 마지막 위치
    private float fishStartPos;             // 물고기의 시작 위치

    private void OnEnable()
    {
        indicator.SetActive(false);
        water.SetActive(false);
        fish.SetActive(false);
        timeElapsed = 0;
        percent = 0;
        waterStartPos = water.transform.position.y;
        fishStartPos = fish.transform.position.y;
        StartCoroutine(Fish());
    }

    public void SetManager(IObjectPool<GameObject> pool)
    {
        managedPool = pool;
    }

    private IEnumerator Fish()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(2);
        indicator.SetActive(false);
        water.SetActive(true);

        fish.SetActive(true);
        fish.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fishPower);

        while (timeElapsed < waterTime)
        {
            float moveY = Mathf.Lerp(waterStartPos, waterEndPos, percent);
            water.transform.position = new Vector3(transform.position.x, moveY, transform.position.z);
            percent = timeElapsed / waterTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0;

        while (timeElapsed < waterTime)
        {
            float moveY = Mathf.Lerp(waterEndPos, waterStartPos, percent);
            water.transform.position = new Vector3(transform.position.x, moveY, transform.position.z);
            percent = timeElapsed / waterTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        water.SetActive(false);
    }

    private void LateUpdate()
    {
        if (fish.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            fish.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            fish.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (fish.transform.position.y < -10)
        {
            fish.transform.localPosition = Vector3.up * fishStartPos;
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        managedPool.Release(gameObject);
    }
}
