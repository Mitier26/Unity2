using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public Vector3 distance;
    public float speed;
    public Vector3 startPos;
    public Vector3 endPos;
    public float percent = 0f;
    public float moveTime;
    public float currentTime = 0f;
    public AnimationCurve curve;

    public float timeElapse = 0f;



    public float xMove = 8f;

    private void Start()
    {
        startPos = transform.position;

        speed = 1 / moveTime;
    }
    private void FixedUpdate()
    {
        //transform.position += distance * speed * Time.fixedDeltaTime;

        //transform.Translate(distance);
        // 프레임마다 distance 거리 만큼 이동한다.
        // distance == 1 이면 1 씩 증가

        //transform.Translate(speed * Time.fixedDeltaTime * distance);

        //transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.fixedDeltaTime);

        //transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.fixedDeltaTime);


        //if(percent < moveTime)
        //{
        //    percent = currentTime / moveTime;
        //    currentTime += Time.fixedDeltaTime;

        //    transform.position = Vector3.Lerp(startPos, endPos, percent);
        //}

        //if(percent < moveTime)
        //{
        //    percent = currentTime / moveTime;
        //    currentTime += Time.fixedDeltaTime;

        //    float moveX = Mathf.Lerp(startPos.x, endPos.x, percent);

        //    transform.position = new Vector3(moveX, Mathf.Sin(speed * Time.time), 0f);
        //}

        //transform.position = new Vector3(Mathf.Sin(speed * Time.time), Mathf.Sin(speed * Time.time), 0f);



        //transform.position = new Vector3(xMove * Mathf.Cos(speed * Time.time), 0f, 0f);


        //transform.position = new Vector3( Mathf.Cos(speed * Time.time), Mathf.Sin(speed * Time.time), 0f);


        if(timeElapse < 1f)
        {
            timeElapse += speed * Time.fixedDeltaTime;

            //transform.position = Vector3.Lerp(startPos, endPos, timeElapse);

            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(timeElapse));
        }

    }
}
