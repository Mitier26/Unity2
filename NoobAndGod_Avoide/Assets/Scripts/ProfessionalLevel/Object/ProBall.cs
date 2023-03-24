using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProBall : ProObject
{
    [SerializeField]
    private GameObject indicator, ball;

    public float startPositionX;
    public Vector2 direction;

    private void Start()
    {
        ball.transform.localPosition = new Vector3(startPositionX, 0, 0);
    }

    private void OnEnable()
    {
        ball.SetActive(false);
        indicator.SetActive(false);
        ball.transform.localPosition = new Vector3(startPositionX, transform.position.y, 0);
        StartCoroutine(Ball());

    }

    private IEnumerator Ball()
    {
        indicator.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        indicator.SetActive(false);
        ball.SetActive(true);
        ball.GetComponent<Rigidbody2D>().velocity = direction;
    }

    private void LateUpdate()
    {
        if(ball.transform.localPosition.x > ProConstants.max.x || ball.transform.localPosition.x < ProConstants.min.x)
        {
            DestroyObject();
        }
    }
}
