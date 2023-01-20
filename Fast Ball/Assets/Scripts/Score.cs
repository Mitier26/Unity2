using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    [SerializeField] private int id;

    public int Id
    {
        get { return id; }
    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += OnGameEnded;
        GameManager.Instance.ScoreReset += ScoreReset;
        GameManager.Instance.ScoreSet += ScoreSet;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= OnGameEnded;
        GameManager.Instance.ScoreReset -= ScoreReset;
        GameManager.Instance.ScoreSet -= ScoreSet;
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
        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color activeColor, inactiveColor;
    private void ScoreReset()
    {
        sr.color = inactiveColor;
    }

    private void ScoreSet(int scoreId)
    {
        if (scoreId != Id) return;

        sr.color = activeColor;
    }

    [SerializeField] private GameObject effect;
    [SerializeField] private Animator effectAC;
    [SerializeField] private AnimationClip effectClip;

    public void PlayScoreAnimation()
    {
        StartCoroutine(ScoreAnimation());
    }

    private IEnumerator ScoreAnimation()
    {
        StopCoroutine(ScoreAnimation());
        effect.SetActive(true);

        effectAC.Play(effectClip.name, -1, 0f);
        yield return new WaitForSeconds(effectClip.length);

        effect.SetActive(false);
    }
}
