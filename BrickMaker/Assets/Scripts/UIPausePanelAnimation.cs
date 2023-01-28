using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanelAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject imageBackgroundOverlay;
    [SerializeField]
    private Animator animator;

    public void OnAppear()
    {
        // 배경을 흐리게 가려주는 이미지 활성화
        imageBackgroundOverlay.SetActive(true);

        // 게임 일시 정지 패널 활설화
        gameObject.SetActive(true);

        animator.SetTrigger("onAppear");
    }

    public void OnDisappear()
    {
        // 일시정지 패널 비활성 에니
        animator.SetTrigger("onDisappear");
    }

    public void EndOfDisappear()
    {
        imageBackgroundOverlay.SetActive(false);

        gameObject.SetActive(false);
    }
}
