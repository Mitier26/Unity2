using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestParticlPosition : MonoBehaviour
{
    public Image image;
    public GameObject effect;
    Vector3 position;

    private void Update()
    {
        position = Camera.main.ScreenToWorldPoint(image.transform.position);
        position.z = 0;
        effect.transform.position = position;
    }
}
