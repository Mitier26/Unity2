using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/BulletSpawnData", order = 1)]
public class BulletSpawnData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;

    public float interval;

    public bool isRandom;
    public bool isParent;

    public float cooldown;
    public float bulletSpeed;
    public Vector2 bulletDiection;
}
