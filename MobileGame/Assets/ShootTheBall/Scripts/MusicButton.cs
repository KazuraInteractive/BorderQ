using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour 
{
	public Button btnMusic;
	public Image btnMusicImage; 
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;

	void Start()
	{
		btnMusic.onClick.AddListener(() => {
			if (InputManager.instance.canInput ()) {
				InputManager.instance.DisableTouchForDelay ();
				InputManager.instance.AddButtonTouchEffect ();
				AudioManager.instance.ToggleMusicStatus	();
			}
		});
	}

	void OnEnable()
	{
		AudioManager.OnMusicStatusChangedEvent += OnMusicStatusChanged;
		initMusicStatus ();
	}

	void OnDisable()
	{
		AudioManager.OnMusicStatusChangedEvent -= OnMusicStatusChanged;
	}

	void initMusicStatus()
	{
		btnMusicImage.sprite = (AudioManager.instance.isMusicEnabled) ? musicOnSprite : musicOffSprite;
	}

	void OnMusicStatusChanged (bool isMusicEnabled)
	{
		btnMusicImage.sprite = (isMusicEnabled) ? musicOnSprite : musicOffSprite;
	}	
}
