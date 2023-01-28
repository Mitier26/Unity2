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
        // ����� �帮�� �����ִ� �̹��� Ȱ��ȭ
        imageBackgroundOverlay.SetActive(true);

        // ���� �Ͻ� ���� �г� Ȱ��ȭ
        gameObject.SetActive(true);

        animator.SetTrigger("onAppear");
    }

    public void OnDisappear()
    {
        // �Ͻ����� �г� ��Ȱ�� ����
        animator.SetTrigger("onDisappear");
    }

    public void EndOfDisappear()
    {
        imageBackgroundOverlay.SetActive(false);

        gameObject.SetActive(false);
    }
}
