using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wait = 0, Processing, End
}
public class Board : MonoBehaviour
{
    [SerializeField]
    private NodeSpawner nodeSpawner;
    [SerializeField]
    private TouchController touchController;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Transform blockRect;

    [SerializeField]
    private UIController uiController;

    public List<Node> NodeList { get; private set; }
    public Vector2Int BlockCount { get; private set; }

    private List<Block> blockList;
    private State state = State.Wait;

    private int currentScore;
    private int highScore;
    private float blockSize;

    private void Awake()
    {
        // ũ�⸦ ���ο��� ���� ũ���
        int count = PlayerPrefs.GetInt("BlockCount");
        BlockCount = new Vector2Int(count, count);

        // ��� ũ�� ���� = (����� ��ġ�Ǵ� Boardũ�� - padding - spacing * (��ϼ� -1) / ��� ��
        blockSize = (1080 - 85 - 25 * (BlockCount.x - 1)) / BlockCount.x;

        currentScore= 0;
        uiController.UpdateCurrentScore(currentScore);

        highScore = PlayerPrefs.GetInt("HighScore");
        uiController.UpdateHighScore(highScore);

        //BlockCount = new Vector2Int(4, 4);

        NodeList = nodeSpawner.SpawnNodes(this, BlockCount, blockSize);

        blockList = new List<Block>();
    }

    private void Start()
    {
        // GridLayoutGroup ���� ������� ������ UI�� ���� ��ġ�� ���� ���̾ƴϴ�.
        // GridLayoutGroup�� ���� ��ġ�� �����ȴ�.
        // �ڽ� node���� ��ġ�� ��� ���� ȭ�鿡 ���̴°Ͱ� �ٸ���.
        // ���� �������� ��ġ

        // ������ ��ġ�� ������� ���ġ�Ѵ�.
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach (Node node in NodeList)
        {
            node.localPosition = node.GetComponent<RectTransform>().localPosition;

        }

        SpawnBlockToRandomNode();
        SpawnBlockToRandomNode();
    }

    private void Update()
    {
        // �׽�Ʈ��
        //if (Input.GetKeyDown("1")) SpawnBlockToRandomNode();

        if(state == State.Wait)
        {
            Direction direction = touchController.UpdateTouch();

            if(direction != Direction.None)
            {
                AllBlocksProcess(direction);
            }
            
        }
        else
        {
            UpdateState();
        }
    }

    private void SpawnBlockToRandomNode()
    {
        // ��� ��带 Ž���ؼ� ����� ��ġ�Ǿ� ���� �ʴ� ��� ����� ��ȯ
        List<Node> emptyNodes = NodeList.FindAll(x => x.placeBlock == null);

        // ��� �ִ� ĭ�� �ִٸ�
        if(emptyNodes.Count != 0)
        {
            // ��� �ִ� ���� ���� �ϳ��� �̴´�.
            int index = Random.Range(0, emptyNodes.Count);
            // ������ ���� ��ġ ������ �����´�.
            Vector2Int point = emptyNodes[index].Point;

            SpawnBlock(point.x, point.y);
        }
        else    // ��� ĭ�� ����� �ִ�
        {
            // ���� ����
            if(IsGameOver())
            {
                OnGameOver();
            }
        }
    }

    private void SpawnBlock(int x, int y)
    {
        if (NodeList[y * BlockCount.x + x].placeBlock != null) return;

        GameObject clone = Instantiate(blockPrefab, blockRect);
        Block block = clone.GetComponent<Block>();
        Node node = NodeList[y * BlockCount.x + x];

        clone.GetComponent<RectTransform>().sizeDelta = new Vector2(blockSize, blockSize);

        clone.GetComponent<RectTransform>().localPosition = node.localPosition;

        block.Setup();

        node.placeBlock = block;

        // ��� ����Ʈ�� ��� ����
        blockList.Add(block);
    }

    private void AllBlocksProcess(Direction direction)
    {
        if(direction == Direction.Right)
        {
            for(int y = 0; y < BlockCount.y; ++y)
            {
                for(int x = (BlockCount.x-2); x >= 0; --x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if (direction == Direction.Down)
        {
            for (int y = (BlockCount.y - 2); y >= 0 ; --y)
            {
                for (int x =0 ; x < BlockCount.x; ++x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if (direction == Direction.Left)
        {
            for(int y= 0; y < BlockCount.y; ++y)
            {
                for(int x = 1; x < BlockCount.x; ++ x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }
        else if(direction == Direction.Up)
        {
            for(int y=1; y < BlockCount.y; ++y)
            {
                for(int x = 0; x < BlockCount.x; ++x)
                {
                    BlockProcess(NodeList[y * BlockCount.x + x], direction);
                }
            }
        }

        // blockList�� �ִ� ��� ����� �˻��� Target�� �ִ� ����� �̵�
        foreach (Block block in blockList)
        {
            if(block.Target != null)
            {
                state = State.Processing;
                block.StartMove();
            }
        }

        // ������ ĭ�� ����� �����Ǿ��� ��
        // �̵�, ������ �Ұ��� �� �� �˻�
        if(IsGameOver())
        {
            OnGameOver();
        }
    }

    /// <summary>
    /// node�� ��ġ�Ǿ� �ִ� ����� �̵�
    /// </summary>
    private void BlockProcess(Node node, Direction direction)
    {
        if (node.placeBlock == null) return;

        // �ش� �������� �̵�, ���� �� �� �ִ��� �ش� ������ ��� �˻�
        Node neighborNode = node.FindTarget(node, direction);
        if(neighborNode != null)
        {
            if(node.placeBlock != null && neighborNode.placeBlock != null)
            {
                if(node.placeBlock.Numeric == neighborNode.placeBlock.Numeric)
                {
                    Combine(node, neighborNode);
                }
            }
            // �ش� ���⿡ ��尡 ���ٸ� �̵�
            else if(neighborNode != null && neighborNode.placeBlock == null)
            {
                Move(node, neighborNode);
            }
        }
    }

    /// <summary>
    /// from ��忡 �ִ� ����� to���� �̵�
    /// </summary>
    private void Move(Node from, Node to)
    {
        // ��ǥ ��带 ����
        from.placeBlock.MoveToNode(to);

        if(from.placeBlock != null)
        {
            
            to.placeBlock = from.placeBlock;
            from.placeBlock = null;
        }
    }

    // from ��忡 �ִ� ����� to��忡 �ִ� ��Ͽ� ����
    private void Combine(Node from, Node to)
    {
        from.placeBlock.CombineToNode(to);

        from.placeBlock = null;

        to.combined = true;
    }

    /// <summary>
    /// ���ϵ��� �̵��� �Ϸ� �Ǿ��� ��
    /// </summary>
    private void UpdateState()
    {
        // ��� ����� �̵�, ���� ó���� �޷�Ǹ� true
        bool targetAllNull = true;

        // blockList�� �˻��� Target�� null�� �ƴѰ��� ������ false
        foreach(Block block in blockList)
        {
            if(block.Target != null)
            {
                targetAllNull = false;
                break;
            }
        }

        if(targetAllNull && state == State.Processing)
        {
            List<Block> removeBlocks = new List<Block>();

            foreach (Block block in blockList)
            {
                if(block.NeedDestroy)
                {
                    removeBlocks.Add(block);
                }
            }

            removeBlocks.ForEach(x =>
            {
                currentScore += x.Numeric * 2;      // �������� ������� ��� ���� * 2
                blockList.Remove(x);
                Destroy(x.gameObject);
            });

            state = State.End;
        }

        if(state == State.End)
        {
            state = State.Wait;

            SpawnBlockToRandomNode();

            NodeList.ForEach(x => x.combined = false);

            uiController.UpdateCurrentScore(currentScore);
        }
    }

    private bool IsGameOver()
    {
        foreach(Node node in NodeList)
        {
            // ���� ��忡 ����� ������ ���� ���� ����
            if (node.placeBlock == null) return false;

            // �� ����� �̿� ��� ��ŭ �ݺ�
            for ( int i = 0; i < node.NeighborNodes.Length; ++i)
            {
                // �̿� ��尡 ������ �ǳʶڴ�.
                if (node.NeighborNodes[i] == null) continue;

                Vector2Int point = node.NeighborNodes[i].Value;
                Node neighborNode = NodeList[point.y * BlockCount.x + point.x];

                if(node.placeBlock != null && neighborNode.placeBlock != null)
                {
                    // ���� ���� �̿� ��忡 ����� �ְ�
                    // �� ����� ���ڰ� ������ ���� ���� ����
                    if(node.placeBlock.Numeric == neighborNode.placeBlock.Numeric)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void OnGameOver()
    {
        if(currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        uiController.OnGameOver();
    }
}
