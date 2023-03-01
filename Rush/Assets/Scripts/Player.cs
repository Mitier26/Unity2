using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            GameManager.instance.hasGameStarted = false;
            int passed = int.Parse(other.gameObject.GetComponentInChildren<TMPro.TMP_Text>().text);
            GameManager.instance.UpdateScore(passed);
            Invoke("RestartGame", 2f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
