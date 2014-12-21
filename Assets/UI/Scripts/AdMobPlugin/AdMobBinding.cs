using System;
using UnityEngine;
using System.Runtime.InteropServices;

// The AdMob Plugin used to call into the AdMob Android Unity library.
public class AdMobBinding : MonoBehaviour {

	public static string Path = "com.finoit.plugin.AdMobPlugin";
	// These are the interface to native implementation calls for iOS.
	#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void _CreateBannerView(string publisherId,  string adSize, bool positionAtTop);

	[DllImport("__Internal")]
	private static extern void _CreateInterstitialView(string publisherId);

	[DllImport("__Internal")]
	private static extern void _ShowInterstitial();

	[DllImport("__Internal")]
	private static extern void _HideInterstitialView();
	
	[DllImport("__Internal")]
	private static extern void _RequestBannerAd(bool isTesting, string extras);
	
	[DllImport("__Internal")]
	private static extern void _HideBannerView();
	
	[DllImport("__Internal")]
	private static extern void _ShowBannerView();

	[DllImport("__Internal")]
	private static extern void _RemoveInterstitialView();

	#endif

    // Create a banner view and add it into the view hierarchy.
     static void CreateBannerView(string publisherId, AdSize adSize, bool positionAtTop)
    {
		#if UNITY_ANDROID
			AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
			AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
			pluginClass.CallStatic ("createBannerView", new object[4] {activity,publisherId,adSize.ToString (),positionAtTop});	
		#endif

		#if UNITY_IPHONE
		//if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//Debug.Log ("Entered");
			_CreateBannerView (publisherId, adSize.ToString (), positionAtTop);
		//}
		#endif
    }

	// Request a new ad for the banner view without any extras.
	 static void RequestBannerAd(bool isTesting)
	{
		#if UNITY_ANDROID
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("requestBannerAd", new object[1] {isTesting});
		#endif
	}
	
	// Request a new ad for the banner view with extras.
	 static void RequestBannerAd(bool isTesting, string extras)
	{	
		#if UNITY_ANDROID
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("requestBannerAd", new object[2] {isTesting, extras});
		#endif
		#if UNITY_IPHONE
		_RequestBannerAd(isTesting, extras);
		#endif
	}

	// Show the banner view on the screen.
	 static void ShowBannerView() {
		#if UNITY_ANDROID
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("showBannerView");
		#endif
		#if UNITY_IPHONE
		_ShowBannerView();
		#endif
	}
	
	// Hide the banner view from the screen.
	 static void HideBannerView()  {
		#if UNITY_ANDROID
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("hideBannerView");
		#endif
		#if UNITY_IPHONE
		_HideBannerView();
		#endif
	}

	public static void createInterstitialView(string publisherId)
	{
		#if UNITY_ANDROID
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("createInterstitialView", new object[2] {activity, publisherId});
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_CreateInterstitialView (publisherId);
		}
		#endif
	}

	public static void displayInterstitial()
	{
		#if UNITY_ANDROID
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("displayInterstitial", new object[1] {activity});
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_ShowInterstitial ();
		}
		#endif
	
	}

	public static void HideInterstitial()
	{
		AdMobGuiManager.IsAdCreated = false;
		#if UNITY_ANDROID
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("HideInterstitial", new object[1] {activity});
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_HideInterstitialView ();
		}

		#endif
	
	}

	public static void RemoveInterstitial()
	{
		//  [UnityGetGLViewController() dismissModalViewControllerAnimated:TRUE];  [UnityGetGLViewController() dismissModalViewControllerAnimated:TRUE];
		AdMobGuiManager.IsAdCreated = false;
		#if UNITY_ANDROID
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass(Path);
		pluginClass.CallStatic("HideInterstitial", new object[1] {activity});
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_RemoveInterstitialView ();
		}
		#endif
	}

	
	// Defines string values for supported ad sizes.
	public class AdSize
	{
		
		private string adSize;
		private AdSize(string value)
		{
			this.adSize = value;
		}
		
		public override string ToString()
		{
			return adSize;
		}
		
		public static AdSize Banner = new AdSize("BANNER");
		public static AdSize MediumRectangle = new AdSize("IAB_MRECT");
		public static AdSize IABBanner = new AdSize("IAB_BANNER");
		public static AdSize Leaderboard = new AdSize("IAB_LEADERBOARD");
		public static AdSize SmartBanner = new AdSize("SMART_BANNER");
	}
	

}