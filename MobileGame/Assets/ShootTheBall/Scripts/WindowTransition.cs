using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WindowTransition : MonoBehaviour 
{
	public bool doAnimateOnLoad = true;

	public bool doAnimateOnDestroy = true;

	public bool doFadeInBackLayOnLoad = true;

	public bool doFadeOutBacklayOnDestroy = true;

	public bool DestroyOnFinish = false;

	public Image BackLay;
	public GameObject WindowContent;

	public float TransitionDuration = 0.35F;

	Vector3 initialPosition = Vector3.zero;

	void Awake()
	{
		if (WindowContent) {

		}
	}

	public void OnWindowAdded()
	{
		if(doAnimateOnLoad && (WindowContent != null))
		{
			initialPosition = WindowContent.transform.localPosition;
			WindowContent.MoveFrom(EGTween.Hash("x",-600,"easeType",EGTween.EaseType.easeOutBack,"time",TransitionDuration,"islocal",true,"ignoretimescale",true));
		}

		if(doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0F,"To",TransitionDuration,"Time",0.5F,"onupdate","OnOpacityUpdate","ignoretimescale",true));
		}
	}

	public void OnWindowRemove()
	{
		if((doAnimateOnDestroy && (WindowContent != null)))
		{
			WindowContent.MoveTo(EGTween.Hash("x",600F,"easeType", EGTween.EaseType.easeInBack, "time",TransitionDuration, "islocal",true,"ignoretimescale",true ));

			if(doFadeOutBacklayOnDestroy)
			{
				BackLay.gameObject.ValueTo(EGTween.Hash("From",TransitionDuration,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate","ignoretimescale",true));
			}

			Invoke("OnRemoveTransitionComplete",0.5F);
		}
		else
		{
			if(doFadeOutBacklayOnDestroy)
			{
				BackLay.gameObject.ValueTo(EGTween.Hash("From",TransitionDuration,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
				Invoke("OnRemoveTransitionComplete",0.5F);
			}
			else
			{
				OnRemoveTransitionComplete();
			}
		}

	}

	public void AnimateWindowOnLoad()
	{
		if(doAnimateOnLoad && (WindowContent != null))
		{
			WindowContent.MoveFrom(EGTween.Hash("x",600,"easeType",EGTween.EaseType.easeOutBack,"time",TransitionDuration,"islocal",true));
		}

		FadeInBackLayOnLoad ();
	}

	public void AnimateWindowOnDestroy()
	{
		if(doAnimateOnDestroy && (WindowContent != null))
		{
			WindowContent.MoveTo(EGTween.Hash("x",-600F,"easeType",EGTween.EaseType.easeInBack,"time",TransitionDuration,"islocal",true));
		}

		FadeOutBacklayOnDestroy ();
	}

	public void FadeInBackLayOnLoad()
	{
		if(doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0F,"To",0.5F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
		}
	}

	public void FadeOutBacklayOnDestroy()
	{
		if(doFadeOutBacklayOnDestroy)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0.5F,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
		}
	}

	void OnOpacityUpdate(float Opacity)
	{
		BackLay.color = new Color (BackLay.color.r, BackLay.color.g, BackLay.color.b, Opacity);
	}

	void OnRemoveTransitionComplete()
	{
		if (DestroyOnFinish) {
			Destroy (gameObject);
		} else {
			WindowContent.transform.localPosition = initialPosition;
			gameObject.SetActive(false);
		}
	}
}
