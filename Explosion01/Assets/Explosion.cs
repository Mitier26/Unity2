using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public delegate void ExplosionDelegate();
    public event ExplosionDelegate OnExplosionTriggerEvent;

    public float explosionRadius;       // ��ü���� ������ ����
    public float explosionFalloffThresholdRaius;    // �ִ� ����ȿ���� �ִ� ����

    public float maxExplosionForce;
    public float minimumExpolosionforce;

    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private FireTrail explosionFireTrail;

    [SerializeField] private float destroyDelay;
    [SerializeField] private float catchFireChance;
    [SerializeField] private bool triggerOnStart;
    [SerializeField] private bool destroyOnTrigger;
    [SerializeField] private bool triggerOnce;


    private bool wasTreggered;

    private float destroyTimer;

    private List<Vector3> recentHits = new List<Vector3>();

    private float falloffThresholePointValue;

    private void Start()
    {
        if (triggerOnStart) Trigger();
    }

    private void Update()
    {
        if(destroyOnTrigger && destroyTimer >= destroyDelay)
        {
            Destroy(gameObject);
        }

        if(wasTreggered && destroyOnTrigger)
        {
            destroyTimer += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        if (wasTreggered && triggerOnce) return;

        // �浹�� ���� �̺�Ʈ ����
        OnExplosionTriggerEvent?.Invoke();

        // ��ƼŬ ����
        explosionParticleSystem.Play();

        // ����Ʈ�� �ʱ�ȭ
        recentHits.Clear();

        // ���� ������ �ִ� ������Ʈ �迭�� ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // �迭�� �ִ� ���� �� �˻�
        for(int i = 0; i < colliders.Length; i++)
        {
            // �迭�� �ִ� ���� Rigidbody �� ������ true
            if (colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                // ������Ʈ�� ���� ����
                recentHits.Add(rigidbody.transform.position);

                // ���� ���� ���� �ִ� ������Ʈ�� ���� ���� ������ �Ÿ��� ����.
                float distance = Vector3.Distance(rigidbody.transform.position, transform.position);
                // ���� �ִ� ������ ���� ���� ������ ����
                float falloffThresholdPointValue = explosionFalloffThresholdRaius / explosionRadius;
                
                // ���� �������� ������Ʈ�� �Ÿ� ����
                float distancePoint = Mathf.Clamp(distance / explosionRadius, 0, 1);
                float calculatedForce = 0f;

                // ���� ������ 10 �̰� �ִ� ���� �Ŀ� ������ 5 �̸� 0.5
                // ������Ʈ�� 0.5 ���� ���Ͽ� ������ �ִ� ���� �Ŀ��� ���� ������.
                if(distancePoint <= falloffThresholdPointValue)
                {
                    calculatedForce = maxExplosionForce;

                    // �� ����Ʈ�� �����Ű�� ����
                    if(RollFireChance())
                    {
                        Instantiate(explosionFireTrail, rigidbody.transform);
                    }
                }
                else
                {
                    // �ִ� ���� �Ŀ� ���� �ۿ� �ִٸ� 
                    // �Ÿ��� ���Ϸ� ���� ������ �����Ѵ�.
                    // ���Ⱑ �߿��� �κ�
                    float pointBetweenThresholdToMaxDistnce = (distance - explosionFalloffThresholdRaius) / (explosionRadius - explosionFalloffThresholdRaius);
                    calculatedForce = Mathf.Lerp(maxExplosionForce, minimumExpolosionforce, pointBetweenThresholdToMaxDistnce);
                }

                rigidbody.AddForce(((rigidbody.transform.position - transform.position).normalized + (Random.onUnitSphere * 0.001f)) * calculatedForce, ForceMode.Impulse);
                rigidbody.AddTorque(Vector3.RotateTowards((rigidbody.transform.position - transform.position) * calculatedForce, transform.position, 6, 1));
            }
        }

        wasTreggered = true;
    }

    private bool RollFireChance()
    {
        return Random.Range(0f, 1.1f) < catchFireChance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionFalloffThresholdRaius);

        Gizmos.color = Color.red;

        if(recentHits != null)
        {
            for(int i = 0; i < recentHits.Count; i++)
            {
                Gizmos.DrawSphere(recentHits[i], 0.1f);
            }
        }
    }
}
