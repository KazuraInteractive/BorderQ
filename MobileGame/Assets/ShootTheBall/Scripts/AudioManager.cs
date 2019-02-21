using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
	public static event Action<bool> OnSoundStatusChangedEvent;
	public static event Action<bool> OnMusicStatusChangedEvent;

	[HideInInspector] public bool isSoundEnabled = true;
	[HideInInspector] public bool isMusicEnabled = true;

	private static AudioManager _instance;	
	public static AudioManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<AudioManager>();
			}
			return _instance;
		}
	}
	void Awake()
	{
		if (_instance != null ) {
			if(_instance.gameObject != gameObject)
			{
				Destroy (gameObject);
				return;
			}
		}
		_instance = GameObject.FindObjectOfType<AudioManager>();
	}

	void OnEnable()
	{
		initAudioStatus ();
	}

	public void initAudioStatus ()
	{
		isSoundEnabled = (PlayerPrefs.GetInt ("isSoundEnabled", 0) == 0) ? true : false;
		isMusicEnabled = (PlayerPrefs.GetInt ("isMusicEnabled", 0) == 0) ? true : false;

		if ((!isSoundEnabled) && (OnSoundStatusChangedEvent != null)) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
		if ((!isMusicEnabled) && (OnMusicStatusChangedEvent != null)) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
	}

	public void ToggleSoundStatus ()
	{
		isSoundEnabled = (isSoundEnabled) ? false : true;
		PlayerPrefs.SetInt ("isSoundEnabled", (isSoundEnabled) ? 0 : 1);

		if (OnSoundStatusChangedEvent != null) {
			OnSoundStatusChangedEvent.Invoke (isSoundEnabled);
		}
	}

	public void ToggleMusicStatus ()
	{
		isMusicEnabled = (isMusicEnabled) ? false : true;
		PlayerPrefs.SetInt ("isMusicEnabled", (isMusicEnabled) ? 0 : 1);

		if (OnMusicStatusChangedEvent != null) {
			OnMusicStatusChangedEvent.Invoke (isMusicEnabled);
		}
	}
}
