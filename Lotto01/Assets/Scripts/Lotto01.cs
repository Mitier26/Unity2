using Mono.CompilerServices.SymbolWriter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Lotto01 : MonoBehaviour
{
    // 버튼을 클리하면 숫자를 선택.

    public int numberCount = 45;
    public int selectCount = 6;
    private int touchCount = 5;

    private int[] numbers;
    private int[] results;

    public GameObject parent;
    public GameObject line;
    public GameObject numText;

    private void Start()
    {
        numbers = new int[numberCount];
        results = new int[selectCount];
    }

    public void Click1()
    {
        for (int i = 0; i < numberCount; i++)
        {
            numbers[i] = i+1;
        }
       
        for(int i = 0; i < selectCount; i++)
        {
            int index = UnityEngine.Random.Range(0, numberCount);

            results[i] = numbers[index];
            numbers[index] = numbers[numberCount - 1];
            numberCount--;
        }

        numberCount = 45;
        Array.Sort(results);
        SetUI();
    }

    private void SetUI()
    {
        int count = parent.transform.childCount;

        if(count >= touchCount)
        {
            for(int j = 0; j < count; j++)
            {
                for (int i = 0; i < selectCount; i++)
                {
                    Destroy(parent.transform.GetChild(j).transform.GetChild(i).gameObject);
                }
                Destroy(parent.transform.GetChild(j).gameObject);
            }
        }

        GameObject l = Instantiate(line);

        l.transform.SetParent(parent.transform,false);

        for(int i = 0; i < selectCount; i++)
        {
            GameObject n = Instantiate(numText);
            n.transform.SetParent(l.transform, false);

            n.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = results[i].ToString();
        }
    }
}
