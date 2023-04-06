using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodSelecter : MonoBehaviour
{
    [SerializeField]
    private Image chracterImage;            // �߾ӿ� ǥ�õǴ� �׸�

    [SerializeField]
    private Sprite[] characterImages;       // �ɸ��� �׸��� ����

    public int characterIndex = 0;          // ���� ���õ� �׸��� ��ȣ

    public GodOpening opening;

    private void Start()
    {
        chracterImage.sprite = characterImages[characterIndex];
    }

    public void LeftArrow()
    {
        GodAudioManager.Instance.PlaySoundEffect(GodAudioManager.SFX.Btn);
        characterIndex--;
        if (characterIndex < 0)
            characterIndex = characterImages.Length-1;
        chracterImage.sprite = characterImages[characterIndex];
    }

    public void RighetButton()
    {
        GodAudioManager.Instance.PlaySoundEffect(GodAudioManager.SFX.Btn);
        characterIndex++;
        if(characterIndex > characterImages.Length-1)
            characterIndex = 0;
        chracterImage.sprite = characterImages[characterIndex];
    }

    public void StartButton()
    {
        opening.StartOpening(characterIndex);
    }
}
