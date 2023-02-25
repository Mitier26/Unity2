using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private Ball ballPrefab;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private LaunchPreview launchPreview;
    private BlockSpawner blockSpawner;

    private List<Ball> balls = new List<Ball>();

    private int ballsReady = 0;
    private bool canMove;
    private bool canDrag;

    private void Awake()
    {
        launchPreview = GetComponent<LaunchPreview>();
        blockSpawner = FindObjectOfType<BlockSpawner>();
        canMove = true;
        canDrag = false;
        CreateBalls();
    }

    private void CreateBalls()
    {
        var ball = Instantiate(ballPrefab);
        ball.gameObject.SetActive(false);
        balls.Add(ball);
        ballsReady++;
    }

    public void ReturnBall()
    {
        ballsReady++;
        if(ballsReady == balls.Count)
        {
            blockSpawner.SpawnBlocks();
            CreateBalls();
            canMove = true;
        }
    }

    private void Update()
    {
        if (!canMove) return;

        if(canDrag && Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if(Input.GetMouseButton(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10f;
            ContinueDrag(worldPosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void StartDrag()
    {
        canDrag = false;
        startPosition = transform.position;
        launchPreview.SetStartPoint(startPosition);
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endPosition = worldPosition;
        launchPreview.SetEndPoint(endPosition);
    }
    
    private void EndDrag()
    {
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        canMove = false;
        Vector3 direction = endPosition - startPosition;
        launchPreview.SetEndPoint(endPosition);
        direction.Normalize();

        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(direction);
            ballsReady--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
