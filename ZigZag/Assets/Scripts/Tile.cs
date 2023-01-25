using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private float falldownTime = 2f;
    private new Rigidbody rigidbody;

    private TileSpawner tileSpawner = null;         // Ÿ�� ����� �ϱ� ����

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(TileSpawner tileSpawner)
    {
        this.tileSpawner = tileSpawner;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine("FallDownAndRespawnTile");
        }
    }

    private IEnumerator FallDownAndRespawnTile()
    {
        yield return new WaitForSeconds(0.1f);

        rigidbody.isKinematic = false;

        yield return new WaitForSeconds(falldownTime);

        rigidbody.isKinematic = true;

        if(tileSpawner != null)
        {
            tileSpawner.SetTilePosition(this.transform);
        }
        else
        {
            gameObject.SetActive(false);
        }
        // tileSpawner �� Ÿ���� ������ �� �߰��� �ִ� ���̴�.
        // �̸� ������� �ִ� �Ϳ��� ����.
        // �׷��Ƿ� TileSpawner ���� �߰��� �־�� �Ѵ�.
    }
}
