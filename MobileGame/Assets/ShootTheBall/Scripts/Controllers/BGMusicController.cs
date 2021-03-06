using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BGMusicController : MonoBehaviour 
{
	public static BGMusicController instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
		Destroy (gameObject);
	}

	public void StartBGMusic()
	{
		if (AudioManager.instance.isMusicEnabled) 
		{
			if (!GetComponent<AudioSource> ().isPlaying) {
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void StopBGMusic()
	{
		GetComponent<AudioSource> ().Stop ();
	}

	void OnEnable()
	{
		AudioManager.OnMusicStatusChangedEvent += OnMusicStatusChanged;
		Invoke ("StartBGMusic", 0.2F);
	}

	void OnDisable()
	{
		AudioManager.OnMusicStatusChangedEvent -= OnMusicStatusChanged;
	}

	void OnMusicStatusChanged (bool isSoundEnabled)
	{
		if (isSoundEnabled) {
			StartBGMusic ();
		} else {
			StopBGMusic ();
		}
	}	
}

