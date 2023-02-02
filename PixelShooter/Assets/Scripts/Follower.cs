using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float currentShotDelay;

    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    private void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    private void Watch()
    {
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        if (parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
            followPos = parent.position;
    }
    private void Follow()
    {
        transform.position = followPos;
    }

    private void Fire()
    {
        if (!Input.GetButton("Fire1")) return;

        if (currentShotDelay < maxShotDelay) return;

        GameObject bullet = objectManager.MakeObject("BulletFollower");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
          

        currentShotDelay = 0;
    }

    private void Reload()
    {
        currentShotDelay += Time.deltaTime;
    }
}
