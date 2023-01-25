using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private float falldownTime = 2f;
    private new Rigidbody rigidbody;

    private TileSpawner tileSpawner = null;         // 타일 재생성 하기 위해

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
        // tileSpawner 는 타일이 생성될 때 추가해 주는 것이다.
        // 미리 만들어져 있는 것에는 없다.
        // 그러므로 TileSpawner 에서 추가해 주어야 한다.
    }
}
