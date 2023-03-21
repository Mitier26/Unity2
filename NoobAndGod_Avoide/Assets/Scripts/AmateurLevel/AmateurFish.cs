using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurFish : MonoBehaviour
{
    [SerializeField]
    GameObject indicator, water, fish;      // �۵��� ������Ʈ

    [SerializeField]
    private float fishPower = 600f;

    private float waterTime = 0.4f;         // ������� �ö���� �ð�
    private float timeElapsed;              // �ð��� ���
    private float percent;                  // �۵��� �ۼ�Ʈ

    private float waterStartPos;            // ������� �ʱ� ��ġ
    private float waterEndPos  = -2f;       // ������� ������ ��ġ
    private Vector2 fishStartPosition;      // ������� ���� ��ġ

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
