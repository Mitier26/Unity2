using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public float widthRate;

    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private bool changingColor;

    public bool specColor;
    public Color setColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();

        MarbleColor();
    }

    public void MarbleColor()
    {
        if (!specColor) setColor = spriteRenderer.color;

        trailRenderer.startColor = setColor;
        trailRenderer.endColor = new Color(setColor.r, setColor.g, setColor.b, 0f);
        trailRenderer.startWidth = base.transform.localScale.x * widthRate;

    }

    private void Update()
    {
        if(changingColor)
        {
            trailRenderer.startColor = spriteRenderer.color;
            trailRenderer.endColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        }
    }
}
