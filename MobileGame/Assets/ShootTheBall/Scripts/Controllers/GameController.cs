using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class GameController : MonoBehaviour 
{
	public static GameController instance;

	public Camera UICamera;
	public Canvas UICanvas;
	public EventSystem eventSystem;

	[HideInInspector] public bool isGamePaused = false;

	public List<GameObject> GameScreens = new List<GameObject>();
	[HideInInspector] public GameObject LastScreen = null;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
		Destroy (gameObject);
	}

	void Start()
	{
		Application.targetFrameRate = 60;
		LastScreen = SpawnUIScreen ("MainScreen");
	}

	public void FadeInUIScreen(GameObject thisScreen)
	{
		if (thisScreen.GetComponent<CanvasGroup> ()) 
		{
			StartCoroutine (FadeInCanvasGroup (thisScreen.GetComponent<CanvasGroup>()));
		}
	}

	public GameObject SpawnUIScreen(string name)
	{
		GameObject thisScreen = null;

		thisScreen = GameScreens.Where(obj => obj.name == name).SingleOrDefault();

		if (thisScreen == null) 
		{
			thisScreen = (GameObject)Instantiate (Resources.Load ("Prefabs/UIScreens/" + name.ToString ()));
			thisScreen.name = name;
			thisScreen.transform.SetParent (UICanvas.transform);
			thisScreen.transform.localPosition = Vector3.zero;
			thisScreen.transform.localScale = Vector3.one;
			thisScreen.GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
		}
		thisScreen.Init ();
		thisScreen.OnWindowLoad ();
		thisScreen.SetActive (true);
		LastScreen = thisScreen;
		return thisScreen;
	}

	GameObject GetUIScreen(string name)
	{
		GameObject thisScreen = null;
		thisScreen = GameScreens.Where(obj => obj.name == name).SingleOrDefault();
		return thisScreen;
	}

	public void OnBackButtonPressed()
	{
		if (LastScreen.name == "MainScreen") {
			SpawnUIScreen ("QuitConfirm");
		} else if (LastScreen.name == "QuitConfirm") {
			LastScreen.OnWindowRemove ();
			LastScreen = GetUIScreen ("MainScreen");
		} else if (LastScreen.name == "GamePlay") {
			PauseGame ();
		} else if (LastScreen.name == "Pause") {
			LastScreen.OnWindowRemove ();
			LastScreen = GetUIScreen ("GamePlay");
		} else if (LastScreen.name == "GameOver") {
			ExitToMainScreenFromGameOver (LastScreen);
			LastScreen = GetUIScreen ("MainScreen");
		}
	}

	IEnumerator FadeInCanvasGroup(CanvasGroup canvasGroup)
	{
		for(float opacity = 0; opacity <= 1F; opacity += 0.075F)
		{
			yield return new WaitForEndOfFrame();
			canvasGroup.alpha = opacity;
		}
		canvasGroup.alpha = 1F;
	}

	public void FadeOutUIScreen(GameObject thisScreen, bool disableOnFadeOut = false)
	{
		if (thisScreen.GetComponent<CanvasGroup> ()) 
		{
			StartCoroutine (FadeOutCanvasGroup (thisScreen.GetComponent<CanvasGroup> (),disableOnFadeOut));
		}
	}

	IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, bool disableOnFadeOut = false)
	{
		for(float opacity = 1; opacity >= 0F; opacity -= 0.075F)
		{
			yield return new WaitForEndOfFrame();
			canvasGroup.alpha = opacity;
		}
		canvasGroup.alpha = 0F;
		
		if(disableOnFadeOut)
		{
			canvasGroup.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (InputManager.instance.canInput()) {
				OnBackButtonPressed ();
			}
		}
	}

	public void StartGamePlay( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GamePlay");
	}

	public void OnGameOver( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GameOver");
	}

	public void ReloadGame( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GamePlay");
	}

	public void ResumeGame( GameObject currentScreen)
	{
		currentScreen.OnWindowRemove ();
	}

	public void ExitToMainScreenFromPause( GameObject currentScreen)
	{
		currentScreen.OnWindowRemove ();
		GetUIScreen ("GamePlay").SetActive (false);
		SpawnUIScreen ("MainScreen");
	}

	public void PauseGame()
	{
		SpawnUIScreen ("Pause");
	}

	public void ExitToMainScreenFromGameOver( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("MainScreen");
	}
}