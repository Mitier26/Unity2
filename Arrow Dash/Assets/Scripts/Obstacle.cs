using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leftSprite, rightSprite;
    [SerializeField] private BoxCollider2D leftBox, rightBox;
    [SerializeField] private float destroyTime, speed;
    [SerializeField] private Vector3 startSpawnPos, endPos;
    [SerializeField] private float maxBarSizeX, minBarSizeX, barResizeTime;

    private float currentSizeX;
    private float horizontalSpeed;

    private Tween moveTween;
    private bool canUpdate;

    private void Awake()
    {
        transform.position = startSpawnPos;
        float timeToMove = -(endPos - startSpawnPos).y / speed;
        moveTween = transform.DOMove(endPos, timeToMove).SetEase(Ease.Linear);
        moveTween.onComplete = DestroySprite;
        moveTween.Play();

        currentSizeX = Random.Range(minBarSizeX, maxBarSizeX);

        canUpdate = Random.Range(0, 5) == 0;

        ResizeBoxes();

        horizontalSpeed = (maxBarSizeX - minBarSizeX) / Random.Range(timeToMove / 2f, timeToMove);

    }

    private void ResizeBoxes()
    {
        Vector2 tempSize = leftSprite.size;
        tempSize.y = currentSizeX;
        leftSprite.size = tempSize;
        leftBox.size = tempSize;
        tempSize.y = maxBarSizeX - currentSizeX;
        rightSprite.size = tempSize;
        rightBox.size = tempSize;

        tempSize = Vector2.zero;
        tempSize.y = currentSizeX / 2f;
        leftBox.offset = tempSize;
        tempSize.y = (maxBarSizeX - currentSizeX) / 2f;
        rightBox.offset = tempSize;
    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += DestroySprite;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= DestroySprite;
    }

    private void FixedUpdate()
    {
        if(!canUpdate) return;

        currentSizeX += horizontalSpeed * Time.fixedDeltaTime;
        ResizeBoxes();
        if(currentSizeX > maxBarSizeX || currentSizeX < minBarSizeX)
        {
            horizontalSpeed *= -1f;
        }
    }

    public void DestroySprite()
    {
        if (moveTween.IsActive()) moveTween.Kill();

        canUpdate = false;
        leftBox.enabled = false;
        rightBox.enabled = false;

        var destroyTween = transform.DOScale(Vector3.zero, destroyTime).SetEase(Ease.InSine);
        destroyTween.onComplete = () =>
        {
            Destroy(gameObject);
        };

        destroyTween.Play();
    }
}
