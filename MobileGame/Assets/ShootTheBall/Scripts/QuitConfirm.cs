using UnityEngine;
using System.Collections;

public class QuitConfirm : MonoBehaviour {

	public void OnCancelButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
		}
	}

	public void OnQuitButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			Application.Quit();
		}
	}
}
