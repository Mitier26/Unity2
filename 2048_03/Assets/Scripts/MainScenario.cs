using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScenario : MonoBehaviour
{
    [SerializeField]
    private Image imageMatrix;
    [SerializeField]
    private TextMeshProUGUI textMatrix;
    [SerializeField]
    private Sprite[] spriteMatrix;

    private int matrixIndex = 0;

    public void OnClickGameStart()
    {
        PlayerPrefs.SetInt("BlockCount", matrixIndex + 3);
        SceneManager.LoadScene("02Game");
    }

    public void OnClickGameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit()
        #endif
    }

    public void OnClickLeft()
    {
        matrixIndex = matrixIndex > 0 ? matrixIndex - 1 : spriteMatrix.Length - 1;

        imageMatrix.sprite = spriteMatrix[matrixIndex];
        textMatrix.text = spriteMatrix[matrixIndex].name;
    }

    public void OnClickRight()
    {
        matrixIndex = matrixIndex < spriteMatrix.Length -1 ? matrixIndex + 1 : 0;

        imageMatrix.sprite = spriteMatrix[matrixIndex];
        textMatrix.text = spriteMatrix[matrixIndex].name;
    }
}
