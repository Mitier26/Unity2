using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern10 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningObject;
    [SerializeField]
    private MovementTransform2D unityLogo;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float minX = -2.7f;
    [SerializeField]
    private float maxX = 2.7f;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        warningObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningObject.SetActive(false);

        unityLogo.gameObject.SetActive(true);

        unityLogo.MoveTo(Vector3.right);

        float time = 0;
        while (time < moveTime)
        {
            time += Time.deltaTime;

            if(unityLogo.transform.position.x <= minX)
            {
                unityLogo.MoveTo(Vector3.right);
            }
            else if(unityLogo.transform.position.x >= maxX) 
            {
                unityLogo.MoveTo(Vector3.left);
            }

            yield return null;
        }

        unityLogo.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}
