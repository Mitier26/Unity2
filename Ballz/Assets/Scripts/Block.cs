using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors;
    private int hitsRemaining;
    private SpriteRenderer spriteRenderer;
    private TMP_Text text;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TMP_Text>();
    }

    private void UpdateVisulaState()
    {
        text.text = hitsRemaining.ToString();
        //text.SetText(hitsRemaining.ToString());
        int colorIndex = hitsRemaining / 10;
        float mix = (hitsRemaining % 10) / 10;
        spriteRenderer.color = Color.Lerp(colors[colorIndex % colors.Count], colors[(colorIndex +1) % colors.Count], mix);

    }

    public void SetHit(int hits)
    {
        hitsRemaining = hits;
        UpdateVisulaState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitsRemaining--;
        if(hitsRemaining > 0)
        {
            UpdateVisulaState();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
