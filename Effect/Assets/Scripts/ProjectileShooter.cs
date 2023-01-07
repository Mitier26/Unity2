using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public enum Direction
    {
        Left, Right, Up, Down, Random
    }

    public Direction direction = Direction.Random;

    public GameObject projectilePrefab;
    public float lifeTime = 5f;
    public float speed = 10f;
    public float distanceFromCamera = 10f;

    private float clickInterval = 0.1f;
    private float currentClickInterval = 0;

    private void Start()
    {
        poolGo = new GameObject("Fireball");
    }

    private void OnEnable()
    {
        if (projectilePrefab != null) return;
    }

    private void Update()
    {
        if(projectilePrefab== null) return;

        if(currentClickInterval > 0f)
        {
            currentClickInterval -= Time.deltaTime;
            return;
        }

        if(Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot());
            currentClickInterval = clickInterval;
        }
    }

    private IEnumerator Shoot()
    {
        Vector3 moveDir;
        Vector3 worldMove;
        float life = lifeTime;

        switch(direction) 
        {
            case Direction.Left: moveDir = Vector3.left; break;
            case Direction.Right: moveDir = Vector3.right; break;
            case Direction.Up: moveDir = Vector3.up; break;
            case Direction.Down: moveDir = Vector3.down; break;
            case Direction.Random:
            default:
                moveDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized; 
                break;
        }

        worldMove = Camera.main.transform.TransformDirection(moveDir) * speed;

        GameObject psInstance = Spawn();
        Transform psTransform = psInstance.transform;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceFromCamera;
        psTransform.position = Camera.main.ScreenToWorldPoint(mousePos);

        float t = 0f;
        while(t < life)
        {
            psTransform.Translate(worldMove * Time.deltaTime, Space.World);

            t += Time.deltaTime;
            yield return null;
        }

        Despawn(psInstance);
    }

    private Queue<GameObject> poolQueue = new Queue<GameObject>();
    private GameObject poolGo;

    private GameObject Spawn()
    {
        GameObject next;

        if (poolQueue.Count == 0)
            next = Instantiate(projectilePrefab);
        else
            next = poolQueue.Dequeue();

        next.SetActive(true);
        next.transform.SetParent(poolGo.transform);

        return next;
    }

    private void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}
