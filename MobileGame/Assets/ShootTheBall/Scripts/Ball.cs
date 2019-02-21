using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (!other.name.Contains ("border")) {
			GamePlay.instance.gameObject.GetComponent<ShakeObject> ().StartShake ();
			GamePlay.instance.OnGameOver();
		}
		else 
		{
			GamePlay.instance.OnScoreUpdated(1);
		}

		GameObject ParticleRing = ParticlePool.instance.GetNewRing ();
		ParticleRing.SetActive (true);
		ParticleRing.transform.localPosition = transform.localPosition;

		gameObject.SetActive (false);
		FirePool.instance.CoolPreviousBall (gameObject);
	}
}
