using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSlab : MonoBehaviour
{
    public static MovingSlab slab;
    public static MovingSlab firstSlab;

    public float speed = 5f;
    private float hangover;

    private void Start()
    {
        if (firstSlab == null)
        {
            firstSlab = GameObject.Find("FirstSlab").GetComponent<MovingSlab>();
        }

        slab = this;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Pause()
    {
        speed = 0;
        Debug.Log(transform.position.z);
        Debug.Log(firstSlab.transform.position.z);
        hangover = transform.position.z - firstSlab.transform.position.z;

        float direction = hangover > 0 ? 1f : -1f;

        SetSlabSizes(hangover, direction);
    }

    private void SetSlabSizes(float hangover, float direction)
    {
        float newSize = firstSlab.transform.localScale.z - Mathf.Abs(hangover);
        float hangoverBlockSize = transform.localScale.z - newSize;

        float newZPosition = firstSlab.transform.position.z + (hangover / 2f);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float edge = transform.position.z + (newSize / 2f * direction);
        float newHangoverZPosition = edge + hangoverBlockSize / 2f * direction;

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, hangoverBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, newHangoverZPosition);

        cube.AddComponent<Rigidbody>();

        Destroy(cube, 3f);
    }
}
