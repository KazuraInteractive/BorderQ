using UnityEngine;
using System.Collections;

public static class ExtentionMethods 
{
	public static bool OnWindowLoad(this GameObject target)
	{
		WindowTransition transition = target.GetComponent<WindowTransition> ();
		if(transition != null)
		{
			transition.OnWindowAdded();
			return true;
		}
		return false;
	}

	public static bool OnWindowRemove(this GameObject target)
	{
		WindowTransition transition = target.GetComponent<WindowTransition> ();
		if(transition != null)
		{
			transition.OnWindowRemove();
			return true;
		}
		return false;
	}
}
