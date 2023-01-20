using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime;

    [SerializeField] private List<Vector3> spawnPos;

    private Vector3 currentspawnPos;
    private int spawnIndex;

    private bool hasGameFinished;
    private bool isLeft;
    private bool canSwitch;

    private void Start()
    {
        hasGameFinished = false;

        spawnIndex = Random.Range(0, spawnPos.Count);
        currentspawnPos = spawnPos[spawnIndex];
        isLeft = Random.Range(0, 2) == 0;
        currentspawnPos.x *= isLeft ? -1f : 1f;
        transform.position = currentspawnPos;
        canSwitch = Random.Range(0, 4) == 0 && spawnIndex != 0;

        if(canSwitch)
        {
            StartCoroutine(MoveToOppositeDirection());
        }
    }

    private void OnEnable()
    {
        GameManager.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= OnGameEnded;
    }


    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.down;

        if(transform.position.y < maxOffset)
        {
            Destroy(gameObject);
        }

    }

    public void OnGameEnded()
    {
        GetComponent<Collider2D>().enabled = false;
        hasGameFinished = true;
        StartCoroutine(Rescale());
    }

    private IEnumerator Rescale()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        Vector3 scaleOffset = endScale - startScale;
        float timeElapsed = 0;
        float speed = 1 / destroyTime;
        var updateTime = new WaitForFixedUpdate();
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }

    [SerializeField] private float startSwitchPosY, endSwitchPosY;

    private IEnumerator MoveToOppositeDirection()
    {
        float currentSwitchPosY = startSwitchPosY + (endSwitchPosY - startSwitchPosY) * 0.25f * Random.Range(0, 5);

        float currentPosY = transform.position.y;
        float offsetY = currentSwitchPosY - currentPosY;
        float waitTime = offsetY / moveSpeed;
        yield return new WaitForSeconds(waitTime);

        float currentSwitchPosX = transform.position.x;
        float targetSwitchPosX = -currentSwitchPosX;
        float offsetX = targetSwitchPosX - currentSwitchPosX;
        float timeToSwitch = Mathf.Abs(offsetX / moveSpeed);
        float speedMagnitude = offsetX > 0f ? 1f : -1f;

        float timeElapsed = 0f;
        while(timeElapsed < timeToSwitch)
        {
            timeElapsed += Time.fixedDeltaTime;
            transform.position += speedMagnitude * moveSpeed * Time.fixedDeltaTime * Vector3.right;
            yield return new WaitForFixedUpdate();
        }

        Vector3 temp = transform.position;
        temp.x = targetSwitchPosX;
        transform.position = temp;

    }
}
