using UnityEngine;
using System;
using System.Runtime.InteropServices;

// Example script showing how you can easily call into the AdMobPlugin.
public class AdMobManager : MonoBehaviour {
	
	public static string Error ="";
	public static string b = "";
	static string Path = AdMobBinding.Path;
	
		// These are the ad callback events that can be hooked into.
	public static event Action ReceivedAd = delegate {};
	public static event Action<string> FailedToReceiveAd = delegate {};
	public static event Action ShowingOverlay = delegate {};
	public static event Action DismissedOverlay = delegate {};
	public static event Action DismissededOverlay = delegate {};
	public static event Action LeavingApplication = delegate {};

#if UNITY_IPHONE
	[DllImport("__Internal")]
	extern static public void _SetCallbackHandlerNameAdMob(string handlerName);
#endif

	void Awake()
	{
		SetCallbackHandlerNameAdMob(gameObject.name);
		DontDestroyOnLoad(this);
	}
	
	// Set the name of the callback handler so the right component gets ad callbacks.
	public static void SetCallbackHandlerNameAdMob(string callbackHandlerName)
	{
		#if UNITY_ANDROID
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path);
		pluginClass.CallStatic ("setCallbackHandlerNameAdMob", new object[1] {callbackHandlerName});	
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_SetCallbackHandlerNameAdMob (callbackHandlerName);
		}
		#endif
	}
	
	public void OnReceiveAd(string unusedMessage)
	{
		Debug.Log ("OnReceiveAd");
		print("OnReceiveAd event received :");
		Error = "OnReceiveAd";
		//AdMobBinding.displayInterstitial ();
		ReceivedAd();
	}
	
	public void OnFailedToReceiveAd(string message)
	{
		print("OnFailedToReceiveAd event received with message:");
		print(message);
		Error = message;
		FailedToReceiveAd(message);
	}
	
	public void OnPresentScreen(string unusedMessage) {
		ShowingOverlay();
	}
	
	public void OnDismissScreen(string unusedMessage)
	{
		DismissedOverlay();
	}

	public void OnDismissedScreen(string unusedMessage)
	{
		//DismissedOverlay();
	}

	public void OnLeaveApplication(string unusedMessage)
	{
		LeavingApplication();
	}

	public void OnCallHandler(string unusedMessage)
	{
		Debug.Log (unusedMessage);
	}


	void OnGUI ()
	{
		//if(GUI.Button (new Rect(0,500,900,300),Error))
		  // Debug.Log("Error");
	}

}