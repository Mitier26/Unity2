using UnityEngine;

public class AmateureAsteroid: MonoBehaviour
{
    public float size;
    public float minSize, maxSize;
    public float speed;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polygonCollider;
    public Sprite[] sprites;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void SetAsteroid(Vector2 direction)
    {
        transform.localScale = Vector3.one * size;
        SetSprite();

        polygonCollider.enabled = true;
        rigid.AddForce(direction * speed);

       
    }

    private void OnEnable()
    {
        Invoke(nameof(DisableObject), 30f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    private void CreateHalf()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        GameObject half = AmateurPoolManager.instance.GetObject(AmateurObject.Asteroid);
        half.transform.position = position;

        AmateureAsteroid halfComponent = half.GetComponent<AmateureAsteroid>();
        halfComponent.size = size * 0.5f;
        halfComponent.SetAsteroid(Random.insideUnitCircle.normalized * speed);
    }

    private void CreateCoin()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        GameObject coin = AmateurPoolManager.instance.GetObject(AmateurObject.Coin);
        coin.transform.position = position;

        coin.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * speed);
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            AudioManager.instance.PlaySfx(SFX.P_Explosion);

            polygonCollider.enabled = false;
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);

            // ũ�⿡ ���� �ٸ� ���� ȹ��
            float score = (Mathf.Abs(maxSize - size)) * 10;
            GameManager.instance.SetScore(score);

            // ���༺�� �ı� �� �� ������ ������ �������� �帳�Ѵ�.
            int coinCount = Random.Range(0, 6);

            if(coinCount > 0)
            {
                for (int i = 0; i < coinCount; i++)
                {
                    CreateCoin();
                }
            }
           
            // ���༺�� �ı� �� �� ũ�Ⱑ 0.5���� Ŭ ���� �п� �ϰ� �Ѵ�.
            if(size > 0.5f)
            {
                CreateHalf();
                CreateHalf();
                CreateHalf();
            }
        }
    }
}
