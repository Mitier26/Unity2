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
        // 크기를 메인에서 정한 크기로
        int count = PlayerPrefs.GetInt("BlockCount");
        BlockCount = new Vector2Int(count, count);

        // 블록 크기 설정 = (블록이 배치되는 Board크기 - padding - spacing * (블록수 -1) / 블록 수
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
        // GridLayoutGroup 으로 만들어진 정렬은 UI의 실제 위치가 변한 것이아니다.
        // GridLayoutGroup에 의해 위치가 결정된다.
        // 자식 node들의 위치를 찍어 보면 화면에 보이는것과 다르다.
        // 원본 프리팹의 위치

        // 노드들의 위치를 리빌드로 재배치한다.
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
        // 테스트용
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
        // 모든 노드를 탐색해서 블록이 배치되어 있지 않는 노드 목록을 반환
        List<Node> emptyNodes = NodeList.FindAll(x => x.placeBlock == null);

        // 비어 있는 칸이 있다면
        if(emptyNodes.Count != 0)
        {
            // 비어 있는 것중 랜덤 하나를 뽑는다.
            int index = Random.Range(0, emptyNodes.Count);
            // 선택한 것의 위치 정보를 가져온다.
            Vector2Int point = emptyNodes[index].Point;

            SpawnBlock(point.x, point.y);
        }
        else    // 모든 칸에 블록이 있다
        {
            // 게임 오버
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

        // 블록 리스트에 블록 저장
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

        // blockList에 있는 모든 블록을 검사해 Target이 있는 블록은 이동
        foreach (Block block in blockList)
        {
            if(block.Target != null)
            {
                state = State.Processing;
                block.StartMove();
            }
        }

        // 마지막 칸에 블록이 생성되었을 때
        // 이동, 병합이 불가능 할 때 검사
        if(IsGameOver())
        {
            OnGameOver();
        }
    }

    /// <summary>
    /// node에 배치되어 있는 블록을 이동
    /// </summary>
    private void BlockProcess(Node node, Direction direction)
    {
        if (node.placeBlock == null) return;

        // 해당 방향으로 이동, 병합 할 수 있는지 해당 방향의 노드 검사
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
            // 해당 방향에 노드가 없다면 이동
            else if(neighborNode != null && neighborNode.placeBlock == null)
            {
                Move(node, neighborNode);
            }
        }
    }

    /// <summary>
    /// from 노드에 있는 블록을 to노드로 이동
    /// </summary>
    private void Move(Node from, Node to)
    {
        // 목표 노드를 설정
        from.placeBlock.MoveToNode(to);

        if(from.placeBlock != null)
        {
            
            to.placeBlock = from.placeBlock;
            from.placeBlock = null;
        }
    }

    // from 노드에 있는 블록을 to노드에 있는 블록에 병합
    private void Combine(Node from, Node to)
    {
        from.placeBlock.CombineToNode(to);

        from.placeBlock = null;

        to.combined = true;
    }

    /// <summary>
    /// 볼록들의 이동이 완료 되었을 때
    /// </summary>
    private void UpdateState()
    {
        // 모든 블록의 이동, 병합 처리가 왼료되면 true
        bool targetAllNull = true;

        // blockList를 검사해 Target이 null이 아닌것이 있으면 false
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
                currentScore += x.Numeric * 2;      // 병합으로 사라지는 블록 숫자 * 2
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
            // 현재 노드에 블록이 없으면 게임 진행 가능
            if (node.placeBlock == null) return false;

            // 각 노드의 이웃 노드 만큼 반복
            for ( int i = 0; i < node.NeighborNodes.Length; ++i)
            {
                // 이웃 노드가 없으면 건너뛴다.
                if (node.NeighborNodes[i] == null) continue;

                Vector2Int point = node.NeighborNodes[i].Value;
                Node neighborNode = NodeList[point.y * BlockCount.x + point.x];

                if(node.placeBlock != null && neighborNode.placeBlock != null)
                {
                    // 현재 노드와 이웃 노드에 블록이 있고
                    // 두 노드의 숫자가 같으면 게임 진행 가능
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
