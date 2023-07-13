using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NoobPlayer : MonoBehaviour
{
    NoobTarget target = null;
    RaycastHit2D hit;

    [SerializeField]
    private GameObject hitEffect;

    [SerializeField]
    private Transform canvas;
    [SerializeField]
    private GameObject flotingX;
    [SerializeField]
    private GameObject flotingScore;

    private void Update()
    {
        if(!NoobGameManager.instance.isOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Target"))
                {
                    target = hit.collider.GetComponent<NoobTarget>();

                    Instantiate(hitEffect, hit.collider.transform.position, Quaternion.identity);

                    NoobGameManager.instance.ComboUp();
                    int score = GetStepValue(NoobGameManager.instance.Combo);
                    NoobGameManager.instance.Score += score;
                    GameObject scoreText = Instantiate(flotingScore, hit.point, Quaternion.identity, canvas);
                    scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
                    NoobAudioManager.instance.PlaySfx(NoobAudioManager.NOOBSFX.Shoot);
                }
                else
                {
                    NoobGameManager.instance.Combo = 0;
                    //target.Reposition();
                    Instantiate(flotingX, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, canvas);
                    NoobAudioManager.instance.PlaySfx(NoobAudioManager.NOOBSFX.Error);
                }
            }
        }
    }


    public int GetStepValue(float value)
    {
        float rangeStart = 0;     // 시작 범위 값
        float rangeEnd = NoobGameManager.instance.maxCombo;       // 종료 범위 값
        int stepStart = NoobGameManager.instance.minScore;              // 시작 단계 값
        int stepEnd = NoobGameManager.instance.maxScore;                // 종료 단계 값

        // 변화 비율 계산
        float ratio = Mathf.Clamp01((value - rangeStart) / (rangeEnd - rangeStart));

        // 단계 값 계산
        int stepValue = Mathf.RoundToInt(Mathf.Lerp(stepStart, stepEnd, ratio));


        return stepValue;
    }
}
