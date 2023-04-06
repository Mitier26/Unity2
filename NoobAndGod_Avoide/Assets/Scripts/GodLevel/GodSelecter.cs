using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodSelecter : MonoBehaviour
{
    [SerializeField]
    private Image chracterImage;            // 중앙에 표시되는 그림

    [SerializeField]
    private Sprite[] characterImages;       // 케릭터 그림의 모음

    public int characterIndex = 0;          // 현재 선택된 그림의 번호

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
