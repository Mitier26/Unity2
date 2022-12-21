using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public BoxCollider2D mixer;
    public float delay = 7f;
    private int selectCount = 0;

    private void Start()
    {
        Invoke(nameof(Timer), delay);
    }

    private void Timer()
    {
        if (selectCount < 6)
            StartCoroutine(ReStart());
        else
            Destroyer();
    }

    private void Destroyer()
    {
        Ball[] balls = FindObjectsOfType<Ball>();

        foreach(Ball ball in balls)
        {
            if(ball.CompareTag("Ball"))
            {
                Destroy(ball.gameObject);
            }
        }
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(delay);
        mixer.enabled = false;
        yield return null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
            if (collision.CompareTag("Ball"))
            {
                collision.tag = "Selected";
                collision.sharedMaterial= null;
                selectCount++;
                Timer();
            }
        
            mixer.enabled = true;
    }
       
}
