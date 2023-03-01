using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResetTrigger : MonoBehaviour
{
    public GameObject[] steps;


    private void OnTriggerEnter(Collider other)
    {
        // 해당 오브젝트에 계단과 공이 들어 가지만
        // 계단이 들어 갈 때는 작동하지 않고 공이 들어 갈 때만 작동을 한다.
        // 이유 : 공에는 Rigidbody가 있고 계단에는 Rigidbody가 없다.
        GameObject First = GetFirstStep();
        GameObject Lost = GetLostStep();

        // 가장 아래있는 것을 가장 위에 있는 것의 다음으로 보내는 것이다.
        First.transform.position = new Vector3(0, Lost.transform.position.y + 1, Lost.transform.position.z + 2);

        if(SliderController.hasGameStarted == true)
        {
            // 장애물 생성
            First.GetComponent<GenerateRandomObstacles>().Generator();
        }

        // 화면에 있는 모든 오브젝트를 찾아 반환한다.
        Transform[] allTransforms = GetAllRelevantTransforms();

        // 화면에 있는 모든 것을 이동시키는 기능
        foreach (Transform t in allTransforms)
        {
            t.position = new Vector3(t.position.x, t.position.y - 1, t.position.z - 2);
        }
        
        // 충돌마다 FindGameObject가 많이 실행된다.
        // 변경해야 한다.
    }

    private GameObject GetFirstStep()
    {
        // 여기서 FindGameObjectsWithTag를 사용해 모든 것을 찾아낸다.
        // 정상으로 작동을 하지만 치명적인 문제가 있다.
        // FindGameObjectsWithTag를 하면 가장 마지막에 만든 것이 [0] 번 인덱스에 들어간다.
        // 유니티 내부적으로 인덱스를 부여하고 그 순서에 따라 배열에 넣는 것이다.
        // 하지만 내부적은 인덱스를 확인하거나 변경한 방법이 없다.
        // GetInstanceID, GetHashCode를 사용하면 알 수 있다고 하지만 
        // 확인 결과 상관이 없다.
        // 오브젝트를 1 개 씩 만든면 정상으로 작동하는거 처럼 보이지만
        // 한번에 많은 오브젝트를 만들면 다르게 작동한다.
        // 따라서 아래 처럼 만드는 방법은 오류가 생길 수 있다.
        // 해결 방법은 게임이 처음 시작될 때 계단을 만들고 배열에 넣는 방법이 있겠다.
        steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject firstStep = steps[0];    // 여기서는 오브젝트를 순서대로 만들어 상관없지만 보장되지 않는다.
        float z = steps[0].transform.position.z;
        // 여기서 firstStep는 가장 아래 있는 것이다.
        // firstStep이 가장 아래 있을 경우 아래있는 foreach를 작동 하지않고 리턴된다.
        
        // 가장 z 값이 작은 것을 first로 리턴하는 기능
        foreach (GameObject step in steps)
        {
            if (step.transform.position.z < z)
            {
                z = step.transform.position.z;
                firstStep = step;
            }
        }

        return firstStep;
    }

    private GameObject GetLostStep()
    {
        // 가장 위에 있는 계단을 찾는 기능
        GameObject[] steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject lostStep = steps[0];

        float z = steps[0].transform.position.z;

        foreach (GameObject step in steps)
        {
            if (step.transform.position.z > z)
            {
                z = step.transform.position.z;
                lostStep = step;
            }
        }

        return lostStep;
    }

    private Transform[] GetAllRelevantTransforms()
    {
        GameObject[] steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject camera = Camera.main.gameObject;

        List<Transform> allTransform = new List<Transform>();

        foreach(GameObject step in steps)
        {
            allTransform.Add(step.transform);
        }
        allTransform.Add(player.transform);
        allTransform.Add(camera.transform);
        return allTransform.ToArray();
    }
}
