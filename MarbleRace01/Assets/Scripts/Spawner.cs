using UnityEngine;

public class Spawner : MonoBehaviour
{
    //this script will spawn gameobject every spawnTime seconds 

    public GameObject ball;
    public float spawnTime = 3f;
 
     void Start () 
    {
     InvokeRepeating ("SpawnBall", spawnTime, spawnTime);
    }
 

    void SpawnBall()
    {  
     Instantiate(ball,transform.position,transform.rotation);
    }
}
