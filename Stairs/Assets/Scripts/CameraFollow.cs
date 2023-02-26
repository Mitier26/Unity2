using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject follower;
	
	private Vector3 offset;
	
	private void Start()
	{
		offset = transform.position - follower.transform.position;
	}
	
	private void FixedUpdate()
	{
		transform.position = follower.transform.position + offset;
	}
}
