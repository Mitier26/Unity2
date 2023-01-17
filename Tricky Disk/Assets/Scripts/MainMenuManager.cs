using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite activeSoundSprite, inactiveSoundSprite;

    private void Start()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTING_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTING_SOUND) : 1) == 1;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        AudioManager.Instance.AddButtonSound();
    }

    public void  ClickPlay()
    {
        SceneManager.LoadScene(Constants.DATA.GAMEPLAY_SCENE);
    }

    public void ClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTING_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTING_SOUND) : 1) == 1;

        sound = !sound;
        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;
        PlayerPrefs.SetInt(Constants.DATA.SETTING_SOUND, sound ? 1 : 0);
        AudioManager.Instance.ToggleSound();
    }
}
