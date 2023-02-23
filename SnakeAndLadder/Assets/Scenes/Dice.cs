using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    int roll;

    [SerializeField]
    List<Sprite> dice;

    public void RandomImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = dice[Random.Range(0, dice.Count)];
    }

    public void SetImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = dice[roll -1];
        GameManager.instance.MovePiece();
    }

    public void Roll(int temp)
    {
        roll = temp;
        Animator animator = GetComponent<Animator>();
        animator.Play("Roll", -1, 0f);
        
    }
}