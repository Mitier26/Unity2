using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class GodOpening : MonoBehaviour
{
    public int id;                                      // 선택한 케릭터 번호
    public GameObject player;

    [SerializeField]
    private Sprite[] characterImages;                   // 캐릭터의 기본 그림
    [SerializeField]
    private Sprite[] ballImages;                        // 볼이 되었을 때 그림 
    [SerializeField]
    private Color[] colors;                             // 이팩트의 색

    [SerializeField]
    private SpriteShapeController shapeController;      // 바닥 변형용

    [SerializeField]
    private GameObject obj;                             // 오프닝용 플레이어 오브젝트
    private SpriteRenderer objRenderer;                 // 오프닝용 플레이어 그림 변경용
    private Vector3 originScale;                        // 오프닝용 플레이어 크기

    [SerializeField]
    Transform leftDoor, rightDoor;                      // 좌우 문

    [Header("파티클")]
    [SerializeField]
    private ParticleSystem appearParticle;              // 볼이 될 때 출력하는 이펙트
    [SerializeField]
    private ParticleSystem openParticle;                // 문이 열릴 때 출력하는 이팩트
    [SerializeField]
    private ParticleSystem falldownParticle;            // 떨어질 때 출력하는 이팩트
    [SerializeField]
    private ParticleSystem falldownParticle2;            // 떨어질 때 출력하는 이팩트
    [SerializeField]
    private ParticleSystem impactParticle;
    private ParticleSystem.MainModule mainModule;       // 이팩트의 기본색 변경을 위한 것



    private void Start()
    {
        objRenderer = obj.GetComponent<SpriteRenderer>();
        originScale = obj.transform.localScale;
        mainModule = appearParticle.main;
    }

    public void StartOpening(int id)
    {
        this.id = id;
        objRenderer.sprite = characterImages[id];
        mainModule.startColor = colors[id];
        StartCoroutine(MainSequence());
    }


    private IEnumerator MainSequence()
    {
        yield return new WaitForSeconds(0.5f);
        // 플레이어가 공으로 변화는 과정
        yield return StartCoroutine(MakingBall());
        // 문이 양옆으로 열리는 과정
        yield return StartCoroutine(OpenDoor());
        // 공이 떨어지는 과정
        yield return StartCoroutine(FallDown());
        // 카메라를 이동하는 과정
        yield return StartCoroutine(MoveCamera());
        // 공이 바닥과 충돌하는 과정
        yield return StartCoroutine(Impact());

        yield break;
    }

    private IEnumerator MakingBall()
    {
        float appearTime = 0.5f;
        float disappearTime = 1f;
        float currentScale = 1f;

        // 크기가 작아진다.
        while (currentScale > 0f)
        {
            currentScale = Mathf.MoveTowards(currentScale, 0f, Time.deltaTime / disappearTime);
            obj.transform.localScale = originScale * currentScale;
            yield return null;
        }

        objRenderer.sprite = ballImages[id];

        // 이팩트 출력
        appearParticle.gameObject.SetActive(true);
        appearParticle.Play();

        currentScale = 0f;

        // 크기가 커진다.
        while (currentScale < 1f)
        {
            currentScale = Mathf.MoveTowards(currentScale, 1f, Time.deltaTime / appearTime);
            obj.transform.localScale = originScale * currentScale;
            yield return null;
        }

        yield break;
    }

    private IEnumerator OpenDoor()
    {
        openParticle.gameObject.SetActive(true);
        openParticle.Play();
        yield return new WaitForSeconds(1f);

        openParticle.Stop();

        float actionTime = 1.5f;
        float actionPlayTime = 0f;

        Vector3 leftDoorPosition = leftDoor.position;
        Vector3 rightDoorPosition = rightDoor.position;

        // 카메라의 초기 위치를 저장한다.
        Vector3 originalCameraPos = Camera.main.transform.position;
        float shakeDuration = 0.5f;
        float shakeMagnitude = 0.1f;
        float elapsed = 0f;

        // 카메라를 흔든다.
        while(elapsed < shakeDuration)
        {
            float x = originalCameraPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalCameraPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            Camera.main.transform.position = new Vector3(x, y, originalCameraPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 카메라의 위치를 초기위치로 한다.
        Camera.main.transform.position = originalCameraPos;
        yield return new WaitForSeconds(0.5f);

        // 문을 좌, 우로 연다.
        while (actionPlayTime < actionTime)
        {
            actionPlayTime += Time.deltaTime;
            float percent = actionPlayTime / actionTime;
            float moveX = Mathf.Lerp(0, 13.5f - 4.43f, percent);
            leftDoor.position = leftDoorPosition + Vector3.left * moveX;
            rightDoor.position = rightDoorPosition + Vector3.right * moveX;
            yield return null;
        }

        leftDoor.gameObject.SetActive(false);
        rightDoor.gameObject.SetActive(false);

        yield break;
    }

    private IEnumerator FallDown()
    {
        Vector3 startPos = transform.position;

        falldownParticle2.gameObject.SetActive(true);
        falldownParticle2.Play();
        // 올라가는 애니메이션
        Vector3 targetPos = startPos + Vector3.up * 3f;
        float moveTime = 1.5f;
        yield return MoveToPosition(targetPos, moveTime, false);

        // 떨어지는 애니메이션
        
        targetPos = startPos + Vector3.down * 2.5f;
        moveTime = 2.5f;
        yield return MoveToPosition(targetPos, moveTime, true);

        // 원래 위치로 돌아오는 애니메이션
        targetPos = startPos;
        moveTime = 1f;
        yield return MoveToPosition(targetPos, moveTime, false);

        falldownParticle2.Stop();
        falldownParticle2.gameObject.SetActive(false);

        yield break;
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, float moveTime, bool isParticleActive)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        Vector3 originalCameraPos = Camera.main.transform.position;
        float shakeMagnitude = 0.1f;

        // 떨어지는 파티클 활성화
        if (isParticleActive)
        {
            falldownParticle.gameObject.SetActive(true);
            falldownParticle.Play();
        }

        while (elapsedTime < moveTime)
        {
            // 위치 이동
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);

            // 떨어지는 파티클 크기 변경
            if (isParticleActive)
            {
                float particleScale = Mathf.Lerp(0f, 1f, elapsedTime / moveTime);
                falldownParticle.transform.localScale = Vector3.one * particleScale;

                // 화면 흔들기
                float shakeAmount = Mathf.Lerp(shakeMagnitude, 0f, elapsedTime / moveTime);
                Vector3 cameraPos = originalCameraPos + Random.insideUnitSphere * shakeAmount;
                cameraPos.z = originalCameraPos.z;
                Camera.main.transform.position = cameraPos;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 위치 보정
        transform.position = targetPos;

        // 화면 흔들기 초기화
        if (isParticleActive)
        {
            Camera.main.transform.position = originalCameraPos;
        }

        yield break;
    }

    private IEnumerator MoveCamera()
    {
        Vector3 startCameraPos = Camera.main.transform.position;
        Vector3 targetCameraPos = new Vector3(0, 0, -10);

        float moveTime = 2f;
        float elapsedTime = 0f;

        while(elapsedTime < moveTime)
        {
            Camera.main.transform.position = Vector3.Lerp(startCameraPos, targetCameraPos, elapsedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetCameraPos;

        yield break;
    }

    private IEnumerator Impact()
    {
        // 기본 위치를 변경한다.
        transform.position = Vector3.up * 10f;

        // 이동 준비
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = Vector3.down * 2f;
        float moveTime = 0.5f;

        // 이동
        yield return StartCoroutine(MoveToPosition(targetPosition, moveTime, false));

        // 파티클 종료
        falldownParticle.Stop();
        falldownParticle.gameObject.SetActive(false);

        float elapsed = 0;

        Vector2 point = shapeController.spline.GetPosition(5);
        while(elapsed < 0.2)
        {
            float percent = elapsed / 0.2f;
            point.y = Mathf.Lerp(0, -1.5f, percent);
            shapeController.spline.SetPosition(5, point);

            elapsed += Time.deltaTime;

            yield return null;
        }

        impactParticle.gameObject.SetActive(true);
        impactParticle.Play();

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.2f;
        Explosion();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;

        elapsed = 0;
        
        appearParticle.gameObject.SetActive(true);
        appearParticle.Play();
        obj.SetActive(false);
        GodGameManager.Instance.GameStart(id, obj.transform.position);

        while (elapsed < 0.2)
        {
            float percent = elapsed / 0.2f;
            point.y = Mathf.Lerp(-1.5f, 0f, percent);
            shapeController.spline.SetPosition(5, point);

            elapsed += Time.deltaTime;

            yield return null;
        }


        yield break;
    }

    public float explosionRadius;
    public float explosionForce;
    private void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius,LayerMask.GetMask("OpeningObject"));

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            collider.isTrigger = true;
            if(rb != null)
            {
                Vector2 dir = (rb.transform.position - transform.position).normalized;
                rb.AddForce(dir * explosionForce);
            }
            Destroy(collider.gameObject, 5f);
        }
    }
}
