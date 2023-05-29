using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProEnemyDetector : MonoBehaviour
{
    ProEnemyUnit unit;

    private void Awake()
    {
        unit = transform.parent.GetComponent<ProEnemyUnit>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("PlayerBuilding"))
        {
            unit.SetAttack(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            unit.SetIdle();
        }
    }
}
