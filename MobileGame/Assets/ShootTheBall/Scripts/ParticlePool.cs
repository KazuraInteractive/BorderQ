using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticlePool : MonoBehaviour 
{
	public static ParticlePool instance;
	public GameObject ParticleRing;
	
	List<GameObject> ParticleRings = new List<GameObject> ();

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	void Start()
	{
		FillParticlePool ();
	}

	void FillParticlePool()
	{
		foreach (Transform t in transform) {
			ParticleRings.Add(t.gameObject);
		}
	}

	public GameObject GetNewRing()
	{
		GameObject particleInstance = null;
		if (ParticleRings.Count > 0) {
			particleInstance = ParticleRings [ParticleRings.Count - 1];
			ParticleRings.Remove (particleInstance);
		} 
		else 
		{
			particleInstance = (GameObject) Instantiate (ParticleRing) as GameObject;
		}
		return particleInstance;
	}

	public void  CoolParticleRing(GameObject particleRing)
	{
		particleRing.SetActive (false);
		particleRing.transform.localPosition = Vector3.zero;
		particleRing.transform.localEulerAngles = Vector3.zero;
		if (!ParticleRings.Contains (particleRing)) {
			ParticleRings.Add(particleRing);
		}
	}
}
