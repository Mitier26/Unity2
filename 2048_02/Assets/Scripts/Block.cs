using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Value;
    public Node Node;
    public Block MergingBlock;
    public bool Merging;
    public Vector2 pos => transform.position;

    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private TextMeshPro text;

    public void Init(BlockType type)
    {
        Value= type.value;
        renderer.color = type.color;
        text.text = type.value.ToString();
    }

    public void SetBlock(Node node)
    {
        if (Node != null) Node.OccupieBlock = null;
        Node = node;
        Node.OccupieBlock = this;

    }

    public void MergeBlock(Block blockToMergeWith)
    {
        MergingBlock = blockToMergeWith;

        Node.OccupieBlock = null;

        blockToMergeWith.Merging = true;
    }

    public bool CanMerger(int value) => value == Value && !Merging && MergingBlock == null;
}
