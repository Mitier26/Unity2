using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasClicked, hasTurnFinished;

    [SerializeField]
    private int index;

    [HideInInspector]
    public int dino;

    [SerializeField]
    private Sprite unrevealed;

    [SerializeField]
    List<Sprite> dinos;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    private void Start()
    {
        hasClicked = false;
        hasTurnFinished = false;
        dino = GameManager.instance.myBoard.GetIndex(index);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = unrevealed;
    }

    public void UpdateTurn()
    {
        hasClicked = true;
        animator.Play("Reveal", -1, 0f);
    }

    public void UpdateImage()
    {
        spriteRenderer.sprite = dinos[dino];
    }

    public void RemoveTurn()
    {
        hasClicked = false;
        animator.Play("Unreveal", -1, 0f);
    }

    public void RemoveImage()
    {
        spriteRenderer.sprite = unrevealed;
    }
}
