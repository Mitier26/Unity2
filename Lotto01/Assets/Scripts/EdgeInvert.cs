using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeInvert : MonoBehaviour
{
    
    public float radius;
    

    [Range(2, 200)]
    public int numEdges;

    private void Start()
    {
        Generate();
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
}