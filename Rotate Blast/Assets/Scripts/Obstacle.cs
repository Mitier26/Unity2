using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float timeToMove, timeToScale;

    [SerializeField]
    private Vector3 startPosTop, endPosTop, startPosBottom, endPosBottom, startScale, endScale;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void OnEnable()
    {
        GameManager.GameEnded += GameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= GameEnded;
    }

    private IEnumerator Move()
    {

        Vector3 posStart = startPosTop + Random.Range(0f, 1f) * (endPosTop - startPosTop);
        transform.position = posStart;
        Vector3 posEnd = startPosBottom + Random.Range(0f, 1f) * (endPosBottom - startPosBottom);
        transform.rotation = Quaternion.identity;

        yield return Scale(transform, startScale, endScale, timeToScale);

        float timeElapsed = 0f;
        float speed = 1 / timeToMove;

        Vector3 offset = posEnd - posStart;
        Quaternion startRot = Quaternion.Euler(0, 0, 0);
        Quaternion endRot = Quaternion.Euler(0, 0, (Random.Range(0, 2) == 0 ? 1 : -1) * 180);

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;

            transform.SetPositionAndRotation(
                posStart + timeElapsed * offset, Quaternion.Lerp(startRot, endRot, timeToScale)
                );
            yield return new WaitForFixedUpdate();
        }

        yield return Scale(transform, endScale, Vector3.zero, timeToScale);
        Destroy(gameObject);

    }

    public void GameEnded()
    {
        StartCoroutine(Scale(transform, endScale, Vector3.zero, timeToScale));
        Destroy(gameObject, timeToScale);
    }

    public IEnumerator Scale(Transform target, Vector3 startScale, Vector3 endScale, float timeToFinish)
    {
        target.localScale = startScale;

        float timeElapsed = 0;
        float speed = 1 / timeToFinish;

        Vector3 offset = endScale - startScale;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            target.localScale = startScale + timeElapsed * offset;
            yield return null;
        }

        target.localScale = endScale;
    }
}
