using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPlayer : MonoBehaviour
{
    public int level = 0;

    public GameObject[] playerUnits;
    public float[] speeds = { 1, 2 };
    public float[] maxHps = { 10, 20 };
    public float[] power = { 1, 2 };
    public float[] cameraRange = { 2, 3 };

    public float hp;

    private void Awake()
    {
        StartCoroutine(ChangeUnit());
    }
    
    public void LevelUp()
    {
        level++;
        StartCoroutine(ChangeUnit());
    }

    public IEnumerator ChangeUnit()
    {
        for(int i = 0; i< playerUnits.Length; i++)
        {
            playerUnits[i].SetActive(false);
        }
        playerUnits[level].SetActive(true);
        hp = maxHps[level];

        float duration = 1;
        float elapsedTime = 0;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/duration);
            float size = Mathf.Lerp(Camera.main.orthographicSize, cameraRange[level], t);
            Camera.main.orthographicSize = size;
            yield return null;
        }

        Camera.main.orthographicSize = cameraRange[level];
        
    }
}
