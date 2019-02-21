using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameOver : MonoBehaviour 
{
    public Text txtScore;
	public Text txtBestScore;
    public int r = 0;

	public AudioClip GameOverAudio;

    void OnEnable()
	{
        transform.Find("btn-rescue").gameObject.SetActive(true);

        BGMusicController.instance.StopBGMusic ();

        if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource>().PlayOneShot(GameOverAudio);
		}

		txtBestScore.text = PlayerPrefs.GetInt ("BestScore", 0).ToString();
		txtScore.text = PlayerPrefs.GetInt ("LastScore", 0).ToString();

		#if UNITY_ANDROID || UNITY_IOS
		if (!UnityAds.instance.IsReady() || r > 0) {
			transform.Find("btn-rescue").gameObject.SetActive(false);
        }
		#endif
	}

	public void OnReplayButtonPressed ()
	{
		if (InputManager.instance.canInput ()) {
            BGMusicController.instance.StartBGMusic();
            r = 0;
            InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
            GameController.instance.ReloadGame(gameObject);
		}
	}

	public void OnHomeButtonPressed ()
	{
		if (InputManager.instance.canInput ()) {
            BGMusicController.instance.StartBGMusic();
            r = 0;
            InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
            GameController.instance.ExitToMainScreenFromGameOver(gameObject);
        }
	}

	public void OnRescueButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();

			BGMusicController.instance.StopBGMusic ();	

			#if UNITY_ANDROID || UNITY_IOS
			UnityAds.instance.ShowAdsWithResult ("", result => 
			{
				if(result == true)
				{
					PlayerPrefs.SetInt("isRescued",1);
                    r = 1;
                    BGMusicController.instance.StartBGMusic();
                    GameController.instance.ReloadGame(gameObject);
				}
			});
			#endif
		}
	}
}
