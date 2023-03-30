using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;                // 잔상 효과의 간격
    public float ghostDelaySeconds;         // 잔상 간격 사이의 경과 시간
    public GameObject ghost;                // 잔상 프리팹
    public bool makeGhost = false;          // 잔상의 ON, OFF

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if(makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                // 잔상 효과로 사용한 오브젝트를 생성한다. ( 오브젝트 풀로 만드는 것이 좋다 )
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                // 자신의 현재 프레임 그림, 에니메이션이 있을 경우
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                // 만들어진 잔상 오브젝트의 방향을 부모의 방향과 같이 한다. 좌, 우
                currentGhost.transform.localScale = this.transform.localScale;
                // 만들어진 잔상 오브젝트의 그림을 현재 플레이어의 그림으로 한다.
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                // 1초후 파괴
                Destroy(currentGhost, 1f);
            }
        }
        
    }
}
