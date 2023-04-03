using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGhost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float ghostDelay;
    private float ghostDelta;

    [SerializeField]
    private Color ghostColor;

    public ParticleSystem pp;
    ParticleSystem.MainModule mainModule;
    ParticleSystemRenderer particleRenderer;

    Sprite sprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainModule = pp.main;
        particleRenderer = pp.GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        if(GetComponent<GodPlayer2>().inputX < 0)
        {
            particleRenderer.flip = new Vector3(1, 0, 0);
        }
        else if (GetComponent<GodPlayer2>().inputX > 0)
        {
            particleRenderer.flip = new Vector3(0, 0, 0);
        }

        if(ghostDelta > ghostDelay)
        {
            sprite = spriteRenderer.sprite;
            pp.textureSheetAnimation.SetSprite(0, sprite);
            mainModule.startColor = ghostColor;
            ghostDelta = 0;
            pp.Play();
        }
        else
        {
            ghostDelta += Time.deltaTime;
        }
    }
}
