using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour 
{
	public Button btnSound;
	public Image btnSoundImage; 
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;

	void Start()
	{
		btnSound.onClick.AddListener(() => 
		{
			if (InputManager.instance.canInput ()) {
				InputManager.instance.DisableTouchForDelay ();
				InputManager.instance.AddButtonTouchEffect ();
				AudioManager.instance.ToggleSoundStatus();
			}
		});
	}

	void OnEnable()
	{
		AudioManager.OnSoundStatusChangedEvent += OnSoundStatusChanged;
		initSoundStatus ();
	}

	void OnDisable()
	{
		AudioManager.OnSoundStatusChangedEvent -= OnSoundStatusChanged;
	}

	void initSoundStatus()
	{
		btnSoundImage.sprite = (AudioManager.instance.isSoundEnabled) ? soundOnSprite : soundOffSprite;
	}

	void OnSoundStatusChanged (bool isSoundEnabled)
	{
		btnSoundImage.sprite = (isSoundEnabled) ? soundOnSprite : soundOffSprite;
	}	
}
