using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeInvert : MonoBehaviour
{
    public Collecter collecterPrefab;
    public float radius;
    public float collecterRadius;

    [Range(2, 200)]
    public int numEdges;

    private void Start()
    {
        Generate();
        SetCollecter();
    }

    private void OnValidate()
    {
        Generate();
    }

    private void Generate()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[numEdges + 1];

        for (int i = 0; i < numEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / numEdges;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }

        points[numEdges] = points[0];

        edgeCollider.points = points;
    }

    private void SetCollecter()
    {
        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.PI * 0.5f - i * (Mathf.PI * 2) / 6;
            Collecter collecter = Instantiate(collecterPrefab, this.transform);
            collecter.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * collecterRadius;
            collecter.transform.Rotate(new Vector3(0, 0, i * -60), Space.Self);
            collecter.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}