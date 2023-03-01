using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomObstacles : MonoBehaviour
{
    public void Generator()
    {
        ResetSlots();

        int numberOfSpike = Random.Range(0, 5);

        while(numberOfSpike != 0)
        {
            int RandomIndex = Random.Range(0, 5);
            if(transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle != Obstacle.spike)
            {
                transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle = Obstacle.spike;
                numberOfSpike--;
            }
        }

        int numberOfDisc = 1;

        while (numberOfDisc != 0)
        {
            int RandomIndex = Random.Range(0, 5);
            if (transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle == Obstacle.empty)
            {
                transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle = Obstacle.disc;
                numberOfDisc--;
            }
        }

        int numberOfDiamond = 1;
        int chanceOfDiamond = Random.Range(5, 15);
        int count = 0;

        while (numberOfDiamond != 0)
        {
            int RandomIndex = Random.Range(0, 5);
            if (transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle == Obstacle.empty)
            {
                transform.GetChild(RandomIndex).GetComponent<Slot>().Obstacle = Obstacle.diamond;
                numberOfDiamond--;
            }

            count++;
            if(count == 6)
            {
                numberOfDiamond = 0;
            }
        }
    }

    private void ResetSlots()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();
            if(slot != null)
            {
                slot.Obstacle = Obstacle.empty;
            }
        }
    }
}
