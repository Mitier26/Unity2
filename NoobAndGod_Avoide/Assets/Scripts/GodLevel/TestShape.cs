using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class TestShape : MonoBehaviour
{
    SpriteShapeController spriteShapeController;

    public Vector3[] points;
    public float[] heights;
    public float[] speeds;

    private void Awake()
    {
        spriteShapeController = GetComponent<SpriteShapeController>();

        points = new Vector3[spriteShapeController.spline.GetPointCount() - 4];
        heights = new float[points.Length];
        speeds = new float[points.Length];

        for(int i = 0; i < spriteShapeController.spline.GetPointCount() - 4; i++)
        {
            points[i] = spriteShapeController.spline.GetPosition(i + 2);
            heights[i] = Random.Range(1f, 3f);
            speeds[i] = Random.Range(1f, 3f);
            
        }

    }

    private void Update()
    {
        
        for(int i = 0; i < points.Length; i++)
        {
            Vector3 point = points[i];
            float y = Mathf.PingPong(Time.time * speeds[i], heights[i]);
            point.y += y;
            spriteShapeController.spline.SetPosition(i + 2, point);
        }



    }


}
