using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private enum State { Title, Stage, NoobLevel, BeginnerLevel, AmateurLevel, ProfessionalLevel, GodLevel };
    [SerializeReference] private State state;
    public void ChangeScene(SceneChanger currState)
    {
        SceneManager.LoadScene(currState.state.ToString());
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
