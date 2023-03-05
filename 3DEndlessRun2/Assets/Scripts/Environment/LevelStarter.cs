using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;
    public GameObject countDownGo;
    public GameObject fadeIn;

    public AudioSource readyFX;
    public AudioSource goFX;

    private void Start()
    {
        StartCoroutine(CountSequence());
    }

    private IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(1.5f);
        countDown3.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countDown3.SetActive(false);
        countDown2.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countDown2.SetActive(false);
        countDown1.SetActive(true);
        readyFX.Play();
        yield return new WaitForSeconds(1f);
        countDown1.SetActive(false);
        countDownGo.SetActive(true);
        goFX.Play();
        yield return new WaitForSeconds(1f);
        countDownGo.SetActive(false);
        PlayerMove.canMove = true;
    }
}
