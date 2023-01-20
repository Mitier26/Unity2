using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime;


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
        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.left;

        if(transform.position.x < maxOffset)
        {
            Destroy(gameObject);
        }
    }

    public void OnGameEnded()
    {
        GetComponent<Collider2D>().enabled = false;
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
}
