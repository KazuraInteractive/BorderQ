using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ring : MonoBehaviour 
{
	public static Ring instance;

	public float rotateSpeed = 50.0F;
	public float minSpeed = 50.0F;
	public float maxSpeed = 150.0F;
	public float speedIncreaseOnLevelUp = 15.0F;

	public int levelUpOnCount = 5;

	public List<GameObject> Rings = new List<GameObject>();
	GameObject currentRing = null;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	void OnEnable()
	{
		GamePlay.OnScoreUpdatedEvent += OnScoreUpdated;

		if (PlayerPrefs.GetInt ("isRescued", 0) == 1) {
			rotateSpeed =  ((rotateSpeed > minSpeed) ? rotateSpeed : minSpeed);
			if(currentRing == null)
			{
				currentRing = Rings [Random.Range (0, Rings.Count)];
			}
		} else {
			currentRing = Rings [Random.Range (0, Rings.Count)];
			rotateSpeed = minSpeed;
		}
		currentRing.SetActive (true);
		EGTween.Init (gameObject);
		StartRotation ();
	}

	void OnDisable()
	{
		GamePlay.OnScoreUpdatedEvent -= OnScoreUpdated;
		EGTween.Stop (gameObject);
		currentRing.SetActive (false);
	}

	void OnScoreUpdated (int score)
	{
		if (score % levelUpOnCount == 0) {

			rotateSpeed += speedIncreaseOnLevelUp;
			rotateSpeed = Mathf.Clamp(rotateSpeed, minSpeed, maxSpeed);

			UpdateRandomRing();
		}
	}

	void UpdateRandomRing()
	{
		currentRing.SetActive (false);
		currentRing = Rings [Random.Range (0, Rings.Count)];
		currentRing.SetActive (true);
		StartRotation ();
	}

	public void StartRotation()
	{
		EGTween.RotateBy (gameObject, EGTween.Hash ("z", 1.0F, "speed",(rotateSpeed), "easeType", EGTween.EaseType.linear, "loopType", EGTween.LoopType.loop));
	}
}
