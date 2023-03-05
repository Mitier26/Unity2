using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject charModel;
    public AudioSource crashThud;
    public GameObject mainCam;
    public GameObject levelControl;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            player.GetComponent<PlayerMove>().enabled = false;
            charModel.GetComponent<Animator>().Play("Stumble Backwards");
            levelControl.GetComponent<LevelDistance>().enabled = false;
            crashThud.Play();
            mainCam.GetComponent<Animator>().enabled = true;
            levelControl.GetComponent<EndRunSequence>().enabled = true;
        }
        
    }
}
