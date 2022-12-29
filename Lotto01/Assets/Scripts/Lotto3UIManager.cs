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
