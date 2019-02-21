using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour, IPointerDownHandler
{
	public static GamePlay instance;
	public Text txtScore;
    public Text countdown;
    public int timeLeft = 10;
    public SpriteRenderer sp_background;

	public AudioClip SuccessHit;
	public AudioClip RingHit;

	public List<Color> BGColors = new List<Color>();

	[HideInInspector] public int score = 0;
	[HideInInspector] public int bestScore = 0;
	[HideInInspector] public bool isGamePlay; 

	public static event Action<int> OnScoreUpdatedEvent;

    void Awake()
	{
        if (instance == null) {
			instance = this;
			return;
		}
	}

    void Update()
    {
        countdown.text = ("" + timeLeft);

        if(timeLeft<=0)
        {
            instance.gameObject.GetComponent<ShakeObject>().StartShake();
            instance.OnGameOver();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void OnEnable()
	{
        timeLeft = 10;
        StartCoroutine("LoseTime");
        Time.timeScale = 1;

        BGMusicController.instance.StartBGMusic ();
		bestScore = PlayerPrefs.GetInt ("BestScore", 0);
		isGamePlay = true;

		if (PlayerPrefs.GetInt ("isRescued", 0) == 1) {
			score = PlayerPrefs.GetInt ("LastScore", 0);
		} else {
			SetBackgroundColor ();
			score = 0;
		}

		txtScore.text = score.ToString ("00");
		Invoke ("ResetPrefs", 1F);
	}

	void ResetPrefs(){
		PlayerPrefs.DeleteKey ("LastScore");
		PlayerPrefs.DeleteKey ("isRescued");
	}

	public void OnGameOver ()
	{
        StopCoroutine("LoseTime");

		PlayerPrefs.SetInt ("LastScore", score);

		if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource> ().PlayOneShot (RingHit);
		}

		Invoke ("ExecuteGameOver", 1F);
	}

	void ExecuteGameOver()
	{
		GameController.instance.OnGameOver (gameObject);
	}

	public void OnScoreUpdated (int count)
	{
		score += count;
		txtScore.text = score.ToString ("00");
		OnScoreUpdatedEvent.Invoke (score);

        timeLeft = 10;

		if (score % 5 == 0) {
			SetBackgroundColor();
		}

		if (score > bestScore) {
			bestScore = score;
			PlayerPrefs.SetInt ("BestScore", bestScore);
		}
		if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource> ().PlayOneShot (SuccessHit);
		}
	}

	void SetBackgroundColor()
	{
		sp_background.color = BGColors [UnityEngine.Random.Range (0, BGColors.Count)];;
	}


	#region IPointerDownHandler implementation
	public void OnPointerDown (PointerEventData eventData)
	{
		if (isGamePlay) 
		{
			Cannon.instance.FireBall();	
		}
	}
	#endregion

	public void OnPauseButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.PauseGame();
		}
	}
}
