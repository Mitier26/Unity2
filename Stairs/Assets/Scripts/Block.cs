using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField]
	private GameObject spikePrefab, discPrefab, diamondPrefab;
	
	[SerializeField]
	private List<Vector3> spawnPoints;
	
	private void Start()
	{
		if(!GameManager.instance.hasGameStarted) return;
		
		GameObject temp = Instantiate(diamondPrefab);
		Vector3 spawnPos = spawnPoints[Random.Range(0,spawnPoints.Count)];
		temp.transform.position = transform.position + spawnPos * transform.localScale.x + discPrefab.transform.position;
		spawnPoints.Remove(spawnPos);
		
		int  numOfSpike = Random.Range(0,5);
		
		while(numOfSpike != 0)
		{
			temp = Instantiate(spikePrefab);
			spawnPos = spawnPoints[Random.Range(0,spawnPoints.Count)];
			temp.transform.position = transform.position + spawnPos * transform.localScale.x + spikePrefab.transform.position;
			spawnPoints.Remove(spawnPos);
			numOfSpike--;
		}
		
		if(spawnPoints.Count == 0) return;
		
		temp = Instantiate(diamondPrefab);
		spawnPos = spawnPoints[Random.Range(0,spawnPoints.Count)];
		temp.transform.position = transform.position + spawnPos * transform.localScale.x + diamondPrefab.transform.position;
		spawnPoints.Remove(spawnPos);
	}
}
