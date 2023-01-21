using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool hasGameFinished;

    private void Start()
    {
        hasGameFinished = false;

        totalMovePos = movePositions.Count;
        moveStartIndex = startIndex;
        moveMagnitude = 1;
        moveEndIndex = (moveStartIndex + 1) % totalMovePos;
        moveStartPos = movePositions[moveStartIndex];
        moveEndPos = movePositions[moveEndIndex];
        moveDirection = (moveEndPos - moveStartPos).normalized;
        moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
        currentMoveDistance = 0f;
        transform.position = moveStartPos + currentMoveDistance * moveDirection;
    }

    private void OnEnable()
    {
        GameManager.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= OnGameEnded;
    }


    [SerializeField] private List<Vector3> movePositions;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int startIndex; 

    private Vector3 moveStartPos, moveEndPos, moveDirection;
    private float moveDistance, moveMagnitude;
    private float currentMoveDistance;
    private int moveStartIndex, moveEndIndex, totalMovePos;

    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        if (currentMoveDistance > moveDistance)
        {
            currentMoveDistance = 0f;
            moveStartIndex = (moveStartIndex + 1) % totalMovePos;
            moveEndIndex = (moveStartIndex + 1) % totalMovePos;
            moveStartPos = movePositions[moveStartIndex];
            moveEndPos = movePositions[moveEndIndex];
            moveDirection = (moveEndPos - moveStartPos).normalized;
            moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
        }
        else if (currentMoveDistance < 0f)
        {
            moveStartIndex = (moveStartIndex - 1 + totalMovePos) % totalMovePos;
            moveEndIndex = (moveStartIndex + 1) % totalMovePos;
            moveStartPos = movePositions[moveStartIndex];
            moveEndPos = movePositions[moveEndIndex];
            moveDirection = (moveEndPos - moveStartPos).normalized;
            moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
            currentMoveDistance = moveDistance;
        }

        currentMoveDistance += moveSpeed * moveMagnitude * Time.fixedDeltaTime;
        transform.position = moveStartPos + currentMoveDistance * moveDirection;

    }

    public void OnGameEnded()
    {
        GetComponent<Collider2D>().enabled = false;
        hasGameFinished = true;
    }

    //private IEnumerator Rescale()
    //{
    //    Vector3 startScale = transform.localScale;
    //    Vector3 endScale = Vector3.zero;
    //    Vector3 scaleOffset = endScale - startScale;
    //    float timeElapsed = 0;
    //    float speed = 1 / destroyTime;
    //    var updateTime = new WaitForFixedUpdate();
    //    while(timeElapsed < 1f)
    //    {
    //        timeElapsed += speed * Time.fixedDeltaTime;
    //        transform.localScale = startScale + timeElapsed * scaleOffset;
    //        yield return updateTime;
    //    }

    //    Destroy(gameObject);
    //}
}
