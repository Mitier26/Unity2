using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    [SerializeField]
    private Node nodePrefab;
    [SerializeField]
    private SpriteRenderer boardPrefab;
    [SerializeField]
    private Block blockPrefab;

    [SerializeField]
    private List<BlockType> types;

    [SerializeField]
    private float travelTime = 0.2f;

    [SerializeField]
    private int winCondition = 2048;


    [SerializeField]
    private GameObject winScreen, loseScreen;

    private List<Node> nodes;
    private List<Block> blocks;

    private GameState state;

    private int round;

    private BlockType GetBlockTypeByValue(int value) => types.First(t => t.value == value);

    private void Start()
    {
        ChangeState(GameState.GenerateLevel);
    }

    private void ChangeState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GenerateLevel:
                GenerateGrid();
                break;
            case GameState.SpawningBlock:
                SpawnBlock(round++ == 0 ? 2 : 1);
                break;
            case GameState.WaitingInput:
                break;
            case GameState.Moving:
                break;
            case GameState.Win:
                winScreen.SetActive(true);
                break;
            case GameState.Lose:
                loseScreen.SetActive(true);
                break;

        }
    }

    private void Update()
    {
        if (state != GameState.WaitingInput) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) Shift(Vector2.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Shift(Vector2.right);
        if (Input.GetKeyDown(KeyCode.UpArrow)) Shift(Vector2.up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) Shift(Vector2.down);

    }

    private void GenerateGrid()
    {
        round = 0;
        nodes = new List<Node>();
        blocks = new List<Block>();

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //var node = Instantiate(nodePrefab, new Vector2(x-(width*0.5f-0.5f), y-(height * 0.5f - 0.5f)), Quaternion.identity);
                var node = Instantiate(nodePrefab, new Vector2(x , y), Quaternion.identity);
                nodes.Add(node);
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        
        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);

        ChangeState(GameState.SpawningBlock);
    }

    void SpawnBlock(int amount)
    {
        var freeNodes = nodes.Where(n => n.OccupieBlock == null).OrderBy(b => Random.value);

        foreach (var node in freeNodes.Take(amount))
        {
            SpawnBlock(node, Random.value > 0.8f ? 4 : 2);
        }

        if(freeNodes.Count() == 1)
        {
            ChangeState(GameState.Lose);
            return;
        }

        ChangeState(blocks.Any(b=>b.Value == winCondition) ? GameState.Win : GameState.WaitingInput);
    }

    void SpawnBlock(Node node, int value)
    {
        var block = Instantiate(blockPrefab, node.Pos, Quaternion.identity);
        block.Init(GetBlockTypeByValue(value));
        block.SetBlock(node);
        blocks.Add(block);
    }

    void Shift(Vector2 dir)
    {
        ChangeState(GameState.Moving);

        var orderedBlocks = blocks.OrderBy(b => b.pos.x).ThenBy(b => b.pos.y).ToList();
        if (dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            var next = block.Node;
            do
            {
                block.SetBlock(next);

                var possibleNode = GetNodeAtPosition(next.Pos + dir);

                if(possibleNode != null)
                {
                    if(possibleNode.OccupieBlock != null && possibleNode.OccupieBlock.CanMerger(block.Value))
                    {
                        block.MergeBlock(possibleNode.OccupieBlock);

                    }
                    else if (possibleNode.OccupieBlock == null) next = possibleNode;
                }
            }
            while (next != block.Node);

            block.transform.DOMove(block.Node.Pos, travelTime);
        }

        var sequence = DOTween.Sequence();

        foreach (var block in orderedBlocks)
        {
            var movePoint = block.MergingBlock != null ? block.MergingBlock.Node.Pos : block.Node.Pos;

            sequence.Insert(0, block.transform.DOMove(movePoint, travelTime));

        }

        sequence.OnComplete(() =>
        {
            foreach (var block in orderedBlocks.Where(b => b.MergingBlock != null))
            {
                MergeBlocks(block.MergingBlock, block);
            }

            ChangeState(GameState.SpawningBlock);
        });
    }

    private void MergeBlocks(Block baseBlock, Block mergingBlock)
    {
        SpawnBlock(baseBlock.Node, baseBlock.Value * 2);

        RemoveBlock(baseBlock);
        RemoveBlock(mergingBlock);
    }

    private void RemoveBlock(Block block)
    {
        blocks.Remove(block);
        Destroy(block.gameObject);
    }

    private Node GetNodeAtPosition(Vector2 pos)
    {
        return nodes.FirstOrDefault(n => n.Pos == pos);
    }

}

[Serializable]
public struct BlockType
{
    public int value;
    public Color color;
}

public enum GameState
{
    GenerateLevel,
    SpawningBlock,
    WaitingInput,
    Moving,
    Win, 
    Lose
}
