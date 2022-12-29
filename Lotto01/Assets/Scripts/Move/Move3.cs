using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3 : MonoBehaviour
{
    public Transform target;
    public Vector3 startPosition;
    public float duration = 3f;
    public float elapsedTime;

    public AnimationCurve curve;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / duration;

        //transform.position = Vector3.Lerp(startPosition, target.position, percentage);

        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);

        //transform.position = Vector3.Lerp(transform.position, target.position, Mathf.SmoothStep(0,1, percentage));

        transform.position = Vector3.Lerp(startPosition, target.position, curve.Evaluate( percentage ));
    }

}
