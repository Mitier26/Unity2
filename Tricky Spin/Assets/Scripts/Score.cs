using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime, rotateSpeed;

    private bool hasGameFinished;

    private void Start()
    {
        hasGameFinished = false;
        ColorChanged(GameManager.Instance.CurrentColor);
    }

    private void OnEnable()
    {
        GameManager.GameEnded += OnGameEnded;
        GameManager.Instance.ColorChanged += ColorChanged;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= OnGameEnded;
        GameManager.Instance.ColorChanged -= ColorChanged;
    }


    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.left;
        transform.Rotate(rotateSpeed * Time.fixedDeltaTime * Vector3.forward);
        
        if (transform.position.x < maxOffset)
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
        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }

    private void ColorChanged(Color col)
    {
        GetComponent<SpriteRenderer>().color = col;
    }
}
