using UnityEngine;
using System.Collections;

public class Paused : MonoBehaviour {

	void OnEnable()
	{
        GamePlay.instance.StopCoroutine("LoseTime");

		BGMusicController.instance.StopBGMusic ();
		GameController.instance.isGamePaused = true;
		EGTween.Pause(GamePlay.instance.gameObject,true);
	}

	void OnDisable()
	{
        GamePlay.instance.StartCoroutine("LoseTime");

        BGMusicController.instance.StartBGMusic ();
		EGTween.Resume(GamePlay.instance.gameObject,true);
		GameController.instance.isGamePaused = false;
	}

	public void OnResumeButtonPressed()
	{
		if (InputManager.instance.canInput()) 
		{
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.ResumeGame(gameObject);
		}
	}

	public void OnExitButtonPressed()
	{
		if (InputManager.instance.canInput()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.ExitToMainScreenFromPause(gameObject);
		}
	}
}
