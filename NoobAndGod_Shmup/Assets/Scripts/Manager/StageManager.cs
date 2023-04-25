using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Animator animator;
    public Image fadeImage;
    public StageInfo[] stageInfos;

    [Header("UI")]
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI stageNameShadow;
    public TextMeshProUGUI stageBackground;
    public TextMeshProUGUI stageDescription;
    public Image stageImage;
    public TextMeshProUGUI stageStartName;
    public TextMeshProUGUI stageStartNameShadow;
    public GameObject stageConfirm;

    private IEnumerator Start()
    {
        float fadeTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            percent = fadeTime / 1f;
            
            fadeImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0, percent));

            fadeTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    public void StartAnimation()
    {
        StartCoroutine(StartGame());   
    }

    private IEnumerator StartGame()
    {
        animator.SetTrigger("IsStart");
        fadeImage.gameObject.SetActive(true);

        float fadeTime = 0;
        float percent = 0;

        while (percent < 1)
        {
            percent = fadeTime / 1f;

            fadeImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, percent));

            fadeTime += Time.deltaTime;
            yield return null;
        }

        // 선택한 레벨을 저장
        GameManager.instance.SetStage(sceneName);
        // 씬 변경
        SceneManager.LoadScene(sceneName.ToString());
    }


    public ToggleGroup toggleGroup;
    public SceneName sceneName;

    public void SelectStage()
    {
        Toggle activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();

        if (activeToggle == null)
            return;

        sceneName = (SceneName)Enum.Parse(typeof(SceneName), activeToggle.gameObject.name);

        UIChange(sceneName.ToString());
    }

    private void UIChange(string stageName)
    {
        // 화면에 표시되는 정보를 변경해야한다.

        stageConfirm.SetActive(true);

        for(int i = 0; i < stageInfos.Length; i++)
        {
            if (stageInfos[i].name == stageName)
            {
                this.stageName.text = stageInfos[i].stageName;
                stageNameShadow.text = stageInfos[i].stageName;
                stageDescription.text = stageInfos[i].stageDescription;
                stageBackground.text = stageInfos[i].stageBackground;
                stageImage.sprite = stageInfos[i].stageImage;
                stageStartName.text = stageInfos[i].stageName + " 스테이지 시작";
                stageStartNameShadow.text = stageInfos[i].stageName + " 스테이지 시작";
                break;
                // 난이도 표시는 다르게 해야 한다.
            }
        }
    }
}
