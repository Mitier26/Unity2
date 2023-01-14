using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform centerTransform;
    [SerializeField] private List<float> spawnPosX;

    private void Awake()
    {
        transform.localPosition = Vector3.right * spawnPosX[Random.Range(0, spawnPosX.Count)];
        centerTransform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 37) * 10);
    }

    public void ScoreAdded()
    {
        Destroy(centerTransform.gameObject);
    }
}
