using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 0.3f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.Instance._isPlay)
        {
            speed *= -1;
        }
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(Vector3.forward * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance._isPlay = false;
        SceneManager.LoadScene("MainMenu");
    }
}
