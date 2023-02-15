using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern06 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImage;
    [SerializeField]
    private GameObject doctorKO;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float spawnCycle;
    [SerializeField]
    private int maxCount;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        doctorKO.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        // 경고 이미지 0.5초 동안 활성화
        warningImage[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage[0].SetActive(false);

        // 위에서 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));

        // 총알 발사
        yield return StartCoroutine(nameof(SpawnProjectile));

        // 0.5초 경고
        warningImage[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage[1].SetActive(false);

        // 좌우 이동
        yield return StartCoroutine(nameof(MoveHorizontal));

        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        // 목표 위치
        float destinationY = -2.7f;

        doctorKO.gameObject.SetActive(true);

        while(true)
        {
            if(doctorKO.transform.position.y <= destinationY)
            {
                doctorKO.GetComponent<MovementTransform2D>().MoveTo(Vector3.zero);

                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator SpawnProjectile()
    {
        float minSpeed = 2;
        float maxSpeed = 10;

        int count = 0;
        while(count < maxCount)
        {
            GameObject clone = Instantiate(projectilePrefab, doctorKO.transform.position, Quaternion.identity);
            var movement2D = clone.GetComponent<MovementRigidbody2D>();

            movement2D.MoveSpeed = Random.Range(minSpeed, maxSpeed);
            movement2D.MoveTo(1 - 2 * Random.Range(0, 2));
            movement2D.IsLongJump = Random.Range(0,2) == 0 ? false : true;
            movement2D.JumpTo();

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }
    }

    private IEnumerator MoveHorizontal()
    {
        Vector3 direction = Random.Range(0,2) == 0 ? Vector3.left : Vector3.right;
        doctorKO.GetComponent<MovementTransform2D>().MoveTo(direction);

        while(true)
        {
            if(doctorKO.transform.position.x < Constants.min.x || doctorKO.transform.position.x > Constants.max.x)
            {
                doctorKO.gameObject.SetActive(false);

                yield break;
            }
            yield return null;
        }

    }

}
