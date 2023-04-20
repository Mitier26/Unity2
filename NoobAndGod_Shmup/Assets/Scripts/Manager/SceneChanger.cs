using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //private enum SceneName { Title, Stage, Noob, Begin, Amateur, Pro, God }

    [SerializeField] private SceneName sceneName;
    public void SceneChange(SceneChanger sceneName)
    {
        SceneManager.LoadScene(sceneName.sceneName.ToString());
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
