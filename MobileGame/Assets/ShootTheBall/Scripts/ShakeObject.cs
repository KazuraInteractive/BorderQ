using System.Collections;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
	public Vector3 objectOrigin;
	
	public Vector3 strength;
	private Vector3 strengthDefault;
	
	public float decay = 0.8f;
	
	public float shakeTime = 1f;
	private float shakeTimeDefault;

	public bool isShaking = false;

	void Start()
	{
		objectOrigin = transform.position;
		strengthDefault = strength;
		shakeTimeDefault = shakeTime;
	}

	void Update()
	{
		if( isShaking == true )
		{
			if( shakeTime > 0 )
			{		
				shakeTime -= Time.deltaTime;
				Vector3 tempPosition = transform.position;

				tempPosition.x = objectOrigin.x + Random.Range(-strength.x, strength.x);
				tempPosition.y = objectOrigin.y + Random.Range(-strength.y, strength.y);
				tempPosition.z = objectOrigin.z + Random.Range(-strength.z, strength.z);
				transform.position = tempPosition;
				
				strength *= decay;
			}
			else if( transform.position != objectOrigin )
			{
				shakeTime = 0;
				
				transform.position = objectOrigin;
				isShaking = false;
				strength = strengthDefault;
				shakeTime = shakeTimeDefault;
			}
		}
	}

	public void StartShake()
	{
		isShaking = true;
		strength = strengthDefault;
		shakeTime = shakeTimeDefault;
	}
}