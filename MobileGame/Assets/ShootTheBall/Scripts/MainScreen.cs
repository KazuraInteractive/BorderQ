using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;

public class MainScreen : MonoBehaviour {

	public Text txtBest;
    private string leaderbord = "CgkI46y0h6gFEAIQAQ";
    public GameObject rate;

    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderbord);
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                rate.transform.gameObject.SetActive(true);
                Debug.Log("sucsess");
            }
            else
            {
                rate.transform.gameObject.SetActive(false);
                Debug.Log("BUG");
            }
        });
    }

    void OnEnable()
	{
        txtBest.text = "BEST : " + PlayerPrefs.GetInt ("BestScore", 0).ToString("00");

        if (Social.localUser.authenticated)
        {
            Social.ReportScore(PlayerPrefs.GetInt("BestScore", 0), leaderbord, (bool success) => {
                Debug.Log("Leaderboard update success: " + success);
            });
        }
    }

    public void OnPlayButtonPressed()
	{
		if (InputManager.instance.canInput()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.StartGamePlay(gameObject);
		}
	}

    public void OnRateButtonPressed()
    {
        ShowLeaderboards();
    }
}
