using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class GodOpening : MonoBehaviour
{
    public int id;                                      // ������ �ɸ��� ��ȣ
    public GameObject player;

    [SerializeField]
    private Sprite[] characterImages;                   // ĳ������ �⺻ �׸�
    [SerializeField]
    private Sprite[] ballImages;                        // ���� �Ǿ��� �� �׸� 
    [SerializeField]
    private Color[] colors;                             // ����Ʈ�� ��

    [SerializeField]
    private SpriteShapeController shapeController;      // �ٴ� ������

    [SerializeField]
    private GameObject obj;                             // �����׿� �÷��̾� ������Ʈ
    private SpriteRenderer objRenderer;                 // �����׿� �÷��̾� �׸� �����
    private Vector3 originScale;                        // �����׿� �÷��̾� ũ��

    [SerializeField]
    Transform leftDoor, rightDoor;                      // �¿� ��

    [Header("��ƼŬ")]
    [SerializeField]
    private ParticleSystem appearParticle;              // ���� �� �� ����ϴ� ����Ʈ
    [SerializeField]
    private ParticleSystem openParticle;                // ���� ���� �� ����ϴ� ����Ʈ
    [SerializeField]
    private ParticleSystem falldownParticle;            // ������ �� ����ϴ� ����Ʈ
    [SerializeField]
    private ParticleSystem falldownParticle2;            // ������ �� ����ϴ� ����Ʈ
    [SerializeField]
    private ParticleSystem impactParticle;
    private ParticleSystem.MainModule mainModule;       // ����Ʈ�� �⺻�� ������ ���� ��



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
        // �÷��̾ ������ ��ȭ�� ����
        yield return StartCoroutine(MakingBall());
        // ���� �翷���� ������ ����
        yield return StartCoroutine(OpenDoor());
        // ���� �������� ����
        yield return StartCoroutine(FallDown());
        // ī�޶� �̵��ϴ� ����
        yield return StartCoroutine(MoveCamera());
        // ���� �ٴڰ� �浹�ϴ� ����
        yield return StartCoroutine(Impact());

        yield break;
    }

    private IEnumerator MakingBall()
    {
        float appearTime = 0.5f;
        float disappearTime = 1f;
        float currentScale = 1f;

        // ũ�Ⱑ �۾�����.
        while (currentScale > 0f)
        {
            currentScale = Mathf.MoveTowards(currentScale, 0f, Time.deltaTime / disappearTime);
            obj.transform.localScale = originScale * currentScale;
            yield return null;
        }

        objRenderer.sprite = ballImages[id];

        // ����Ʈ ���
        appearParticle.gameObject.SetActive(true);
        appearParticle.Play();

        currentScale = 0f;

        // ũ�Ⱑ Ŀ����.
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

        // ī�޶��� �ʱ� ��ġ�� �����Ѵ�.
        Vector3 originalCameraPos = Camera.main.transform.position;
        float shakeDuration = 0.5f;
        float shakeMagnitude = 0.1f;
        float elapsed = 0f;

        // ī�޶� ����.
        while(elapsed < shakeDuration)
        {
            float x = originalCameraPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalCameraPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            Camera.main.transform.position = new Vector3(x, y, originalCameraPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ī�޶��� ��ġ�� �ʱ���ġ�� �Ѵ�.
        Camera.main.transform.position = originalCameraPos;
        yield return new WaitForSeconds(0.5f);

        // ���� ��, ��� ����.
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
        // �ö󰡴� �ִϸ��̼�
        Vector3 targetPos = startPos + Vector3.up * 3f;
        float moveTime = 1.5f;
        yield return MoveToPosition(targetPos, moveTime, false);

        // �������� �ִϸ��̼�
        
        targetPos = startPos + Vector3.down * 2.5f;
        moveTime = 2.5f;
        yield return MoveToPosition(targetPos, moveTime, true);

        // ���� ��ġ�� ���ƿ��� �ִϸ��̼�
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

        // �������� ��ƼŬ Ȱ��ȭ
        if (isParticleActive)
        {
            falldownParticle.gameObject.SetActive(true);
            falldownParticle.Play();
        }

        while (elapsedTime < moveTime)
        {
            // ��ġ �̵�
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);

            // �������� ��ƼŬ ũ�� ����
            if (isParticleActive)
            {
                float particleScale = Mathf.Lerp(0f, 1f, elapsedTime / moveTime);
                falldownParticle.transform.localScale = Vector3.one * particleScale;

                // ȭ�� ����
                float shakeAmount = Mathf.Lerp(shakeMagnitude, 0f, elapsedTime / moveTime);
                Vector3 cameraPos = originalCameraPos + Random.insideUnitSphere * shakeAmount;
                cameraPos.z = originalCameraPos.z;
                Camera.main.transform.position = cameraPos;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��ġ ����
        transform.position = targetPos;

        // ȭ�� ���� �ʱ�ȭ
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
        // �⺻ ��ġ�� �����Ѵ�.
        transform.position = Vector3.up * 10f;

        // �̵� �غ�
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = Vector3.down * 2f;
        float moveTime = 0.5f;

        // �̵�
        yield return StartCoroutine(MoveToPosition(targetPosition, moveTime, false));

        // ��ƼŬ ����
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
