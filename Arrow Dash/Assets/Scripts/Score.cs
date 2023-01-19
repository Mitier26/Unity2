using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float destroyTime, speed;
    [SerializeField] private Vector3 startSpawnPos, endPos;

    private Tween moveTween;

    private void Awake()
    {
        transform.position = startSpawnPos;
        float timeToMove = -(endPos - startSpawnPos).y / speed;
        moveTween = transform.DOMove(endPos, timeToMove).SetEase(Ease.Linear);
        moveTween.onComplete = DestroySprite;
        moveTween.Play();
    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += DestroySprite;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= DestroySprite;
    }

    public void DestroySprite()
    {
        GetComponent<Collider2D>().enabled = false;
        var scaleTween = transform.DOScale(Vector3.zero, destroyTime).SetEase(Ease.InSine);
        var destroyTween = sr.DOFade(0, destroyTime).SetEase(Ease.InSine);
        destroyTween.onComplete = () =>
        {
            if (moveTween.IsActive()) moveTween.Kill();
            if (scaleTween.IsActive()) scaleTween.Kill();
            Destroy(gameObject);
        };

        destroyTween.Play();
        scaleTween.Play();
    }
}
