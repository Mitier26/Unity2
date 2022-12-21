using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public int number;

    private SpriteRenderer spriteRenderer;

    public TMP_Text text;

    
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text.text = number.ToString();
        SetColor(number);
    }


    private void SetColor(int number)
    {
        if (number > 40) spriteRenderer.color = Color.green;
        else if (number > 30) spriteRenderer.color = Color.black;
        else if (number > 20) spriteRenderer.color = Color.red;
        else if (number > 10) spriteRenderer.color = Color.blue;
        else spriteRenderer.color = Color.yellow;
    }
}
