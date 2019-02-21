using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirePool : MonoBehaviour 
{
	public static FirePool instance;
	public GameObject ball;

	List<GameObject> FireBalls = new List<GameObject> ();

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	void OnEnable()
	{
		FillFirePool ();
	}

	void FillFirePool()
	{
		foreach (Transform t in transform) {
			if(!FireBalls.Contains(t.gameObject))
			{
				t.gameObject.SetActive(false);
				t.localPosition = Vector3.zero;
				FireBalls.Add(t.gameObject);
			}
		}
	}

	public GameObject GetNextBall()
	{
		GameObject ballInstance = null;
		if (FireBalls.Count > 0) {
			ballInstance = FireBalls [FireBalls.Count - 1];
			FireBalls.Remove (ballInstance);
		} 
		else 
		{
			ballInstance = (GameObject) Instantiate (ball) as GameObject;
		}
		return ballInstance;
	}

	public void  CoolPreviousBall(GameObject firedBall)
	{
		firedBall.SetActive (false);
		firedBall.transform.localPosition = Vector3.zero;
		firedBall.transform.localEulerAngles = Vector3.zero;
		if (!FireBalls.Contains (firedBall)) {
			FireBalls.Add(firedBall);
		}
	}
}
