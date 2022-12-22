using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition2 : MonoBehaviour
{
    private Image transitionImage;

    public float transitionSpeed = 1f;
    private bool isReveal;
    public bool isStart = false;
    public Sprite[] transitionSprites;

    private void Start()
    {
        transitionImage = GetComponent<Image>();
        SwitchTransition();
    }

    private void Update()
    {
        //if (!isStart)
        //    return;

        if (isReveal)
        {
            transitionImage.material.SetFloat("_Progress", Mathf.MoveTowards(transitionImage.material.GetFloat("_Progress"), 1.1f, transitionSpeed * Time.deltaTime));
        }
        else
        {
            transitionImage.material.SetFloat("_Progress", Mathf.MoveTowards(transitionImage.material.GetFloat("_Progress"), -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"), transitionSpeed * Time.deltaTime));

            if (transitionImage.material.GetFloat("_Progress") == -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"))
            {
                // 씬로드
                Debug.Log("씬로드");
            }
        }
       
    }

    public void TestTransition()
    {
        SwitchTransition();
        isReveal = !isReveal;
        //isStart = !isStart;
        //StartCoroutine(SceneTransition());
    }

    private IEnumerator SceneTransition()
    {
        float timer = 0;
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (isReveal)
            {
                transitionImage.material.SetFloat("_Progress", Mathf.MoveTowards(transitionImage.material.GetFloat("_Progress"), 1.1f, transitionSpeed * Time.deltaTime));
            }
            else
            {
                transitionImage.material.SetFloat("_Progress", Mathf.MoveTowards(transitionImage.material.GetFloat("_Progress"), -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"), transitionSpeed * Time.deltaTime));
            
                if(transitionImage.material.GetFloat("_Progress") == -0.1f - transitionImage.material.GetFloat("_EdgeSmoothing"))
                {
                    // 씬로드
                    Debug.Log("씬로드");
                    yield return null;
                    // 종료 조건.
                    
                }
            }

            timer += Time.deltaTime;
        }

        yield return null;
        
    }

    public void SwitchTransition()
    {
        int newTransition = Random.Range(0, transitionSprites.Length);

        //transitionImage.material.SetTexture("_MainTex" ,transitionSprites[newTransition].texture);

        transitionImage.material.mainTexture = transitionSprites[newTransition].texture;
    }
}
