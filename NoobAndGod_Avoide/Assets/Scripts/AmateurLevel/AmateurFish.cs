using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurFish : MonoBehaviour
{
    [SerializeField]
    GameObject indicator, water, fish;      // 작동할 오브젝트

    [SerializeField]
    private float fishPower = 600f;

    private float waterTime = 0.4f;         // 물기둥이 올라오는 시간
    private float timeElapsed;              // 시간의 경과
    private float percent;                  // 작동의 퍼센트

    private float waterStartPos;            // 물기둥의 초기 위치
    private float waterEndPos  = -2f;       // 물기둥의 마지막 위치
    private Vector2 fishStartPosition;      // 물고기의 시작 위치

    private void OnEnable()
    {
        indicator.SetActive(false);
        water.SetActive(false);
        fish.SetActive(false);
        timeElapsed = 0;
        percent = 0;
        waterStartPos = water.transform.position.y;
        fishStartPosition = fish.transform.position;
        StartCoroutine(Fish());
    }

    private IEnumerator Fish()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(2);
        indicator.SetActive(false);
        water.SetActive(true);

        fish.SetActive(true);
        fish.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fishPower);

        while (timeElapsed <  waterTime)
        {
            float moveY = Mathf.Lerp(waterStartPos, waterEndPos , percent);
            water.transform.position = new Vector3(transform.position.x, moveY, 0);
            percent = timeElapsed / waterTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
       
        timeElapsed = 0;

        while (timeElapsed < waterTime)
        {
            float moveY = Mathf.Lerp(waterEndPos, waterStartPos, percent);
            water.transform.position = new Vector3(transform.position.x, moveY, 0);
            percent = timeElapsed / waterTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        water.SetActive(false);
    }

    private void LateUpdate()
    {
        if(fish.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            fish.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            fish.GetComponent<SpriteRenderer>().flipY = false;
        }

        if(fish.transform.position.y < -6f)
        {
            fish.transform.position = fishStartPosition;
            gameObject.SetActive(false);
        }
    }
}
