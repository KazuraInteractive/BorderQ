using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
	public static Cannon instance;

	public GameObject CannonPoint;

	GameObject FiringBall;

	public float travelSpeed = 15F;

	public float rotateSpeed = 150.0F;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	void OnEnable()
	{
		EGTween.Init (gameObject);
		StartRotation ();
	}

	void UpdateSpeed()
	{
		EGTween.Stop (gameObject);
		StartRotation ();
	}

	public void StartRotation()
	{
		EGTween.RotateBy (gameObject, EGTween.Hash ("z", -1.0F, "speed",(rotateSpeed), "easeType", EGTween.EaseType.linear, "loopType", EGTween.LoopType.loop));
	}

	public void EndRotation()
	{
		EGTween.Stop (gameObject);
	}

	public void FireBall()
	{
		FiringBall = FirePool.instance.GetNextBall ();
		FiringBall.SetActive (true);
		Vector2 direction = (CannonPoint.transform.position - transform.position).normalized;
		FiringBall.GetComponent<Rigidbody2D>().AddForce ((direction * travelSpeed), ForceMode2D.Impulse);
	}

	void OnDisable()
	{
		EGTween.Stop (gameObject);
	}
}
