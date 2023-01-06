using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class Lotto3UIManager : MonoBehaviour
{
    public GameObject selectedBallPrefab;
    public Transform parent;
    public GameObject[] balls;
    public int[] numbers;

    private void OnEnable()
    {
        // 렉의 주범
        // 공이 Collecter 에 들어 갈 때 리스트에 저장하는 것으로 바꾸어야 할듯
        // Collecter 가 한 몸이 아니고 각자 인것이 문제
        
        balls = GameObject.FindGameObjectsWithTag("Selected");

        //for(int i = 0; i < balls.Length; i++)
        //{
        //    numbers[i] = balls[i].GetComponent<Ball>().number;
        //}

        for(int i = 0; i < balls.Length; i++)
        {
            GameObject selectedNumber = Instantiate(selectedBallPrefab, parent);

            selectedNumber.GetComponent<ProceduralImage>().color = balls[i].GetComponent<SpriteRenderer>().color;
            selectedNumber.GetComponentInChildren<TextMeshProUGUI>().text = balls[i].GetComponent<Ball>().number.ToString();
        }
    }
    
}
