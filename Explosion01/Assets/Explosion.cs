using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public delegate void ExplosionDelegate();
    public event ExplosionDelegate OnExplosionTriggerEvent;

    public float explosionRadius;       // 전체적인 폭발의 범위
    public float explosionFalloffThresholdRaius;    // 최대 폭발효과를 주는 범위

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

        // 충돌한 것의 이벤트 실행
        OnExplosionTriggerEvent?.Invoke();

        // 파티클 실행
        explosionParticleSystem.Play();

        // 리스트를 초기화
        recentHits.Clear();

        // 폭발 범위에 있는 오브젝트 배열에 저장
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // 배열에 있는 것을 을 검색
        for(int i = 0; i < colliders.Length; i++)
        {
            // 배열에 있는 것중 Rigidbody 가 있으면 true
            if (colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                // 오브잭트에 힘을 가함
                recentHits.Add(rigidbody.transform.position);

                // 폭발 범위 내에 있는 오브젝트와 폭발 지점 사이의 거리를 구함.
                float distance = Vector3.Distance(rigidbody.transform.position, transform.position);
                // 폭발 최대 범위와 폭발 범위 사이의 비율
                float falloffThresholdPointValue = explosionFalloffThresholdRaius / explosionRadius;
                
                // 폭발 범위에서 오브젝트의 거리 비율
                float distancePoint = Mathf.Clamp(distance / explosionRadius, 0, 1);
                float calculatedForce = 0f;

                // 폭발 범위가 10 이고 최대 폭발 파워 범위가 5 이면 0.5
                // 오브젝트가 0.5 가리 이하에 있으면 최대 폭발 파워로 날려 버린다.
                if(distancePoint <= falloffThresholdPointValue)
                {
                    calculatedForce = maxExplosionForce;

                    // 불 이팩트를 방생시키는 비율
                    if(RollFireChance())
                    {
                        Instantiate(explosionFireTrail, rigidbody.transform);
                    }
                }
                else
                {
                    // 최대 폭발 파워 범위 밖에 있다면 
                    // 거리를 비교하려 폭발 위력을 결정한다.
                    // 여기가 중요한 부분
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
