using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;


public class UnityAds : MonoBehaviour 
{
	#if UNITY_ANDROID || UNITY_IOS
	public static UnityAds instance;
	public string androidAppID;
	public string iOSAppID;

	void Awake()
	{
		if (instance != null) 
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	public void init(bool enableTestMode = false)
	{
		if(!Advertisement.isInitialized)
		{
			#if UNITY_ANDROID
			Advertisement.Initialize (androidAppID, enableTestMode);	
			#elif UNITY_IOS
			Advertisement.Initialize (iOSAppID);
			#endif
		}
	}

	public void init(string androidAppId, string iosAppId, bool enableTestMode = false)
	{
		if (!Advertisement.isInitialized) {
			#if UNITY_ANDROID
			Advertisement.Initialize (androidAppId, enableTestMode);	
			#elif UNITY_IOS
			Advertisement.Initialize (iosAppId, enableTestMode);
			#endif
		}
	}

	public bool IsReady()
	{
		return Advertisement.IsReady ();
	}

	public bool IsReady(string adZone)
	{
		return Advertisement.IsReady (adZone);
	}

	public bool isShowing()
	{
		return Advertisement.isShowing;
	}

	public bool isSupported()
	{
		return Advertisement.isSupported;
	}

	public string getGameID()
	{
		return Advertisement.gameId;
	}

	public void ShowAds()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}

	public void ShowAds(string zoneId)
	{
		if (Advertisement.IsReady (zoneId)) {
			Advertisement.Show (zoneId);
		}
	}

	public void ShowAds(string zoneId, ShowOptions options)
	{
		if (Advertisement.IsReady (zoneId)) {
			Advertisement.Show (zoneId, options);
		}
	}

	public void ShowAds(string zoneId, Action<ShowResult> adResult)
	{
		if (Advertisement.IsReady (zoneId)) {
			Advertisement.Show( zoneId, new ShowOptions {
				resultCallback = result => {
					adResult (result);
				}
			});
		} else {
			adResult (ShowResult.Failed);
		}
	}

	public void ShowAdsWithResult(string zoneId, Action<bool> adResult)
	{
		if (Advertisement.IsReady (zoneId)) {
			Advertisement.Show( zoneId, new ShowOptions {
				resultCallback = result => {
					adResult ((result == ShowResult.Finished) ? true : false);
				}
			});
		} else {
			adResult (false);
		}
	}
	#endif
}
