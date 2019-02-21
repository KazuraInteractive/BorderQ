using UnityEngine;
using System.Collections;

public class DespawnParticle : MonoBehaviour 
{
	public float DespawnDelay = 1.5F;

	void OnEnable()
	{
		Invoke ("Despawn", DespawnDelay);
	}

	void Despawn()
	{
		ParticlePool.instance.CoolParticleRing (gameObject);
	}
}
