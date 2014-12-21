using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TwitterBinding  : MonoBehaviour
{
	
	public static string Path = "com.example.test.MainActivityTwitter";
	// Create a banner view and add it into the view hierarchy.
	#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void _twitterInit( string consumerKey, string consumerSecret );
	
	[DllImport("__Internal")]
	private static extern void _twitterGetHomeTimeline();
	
	[DllImport("__Internal")]
	private static extern void _twitterIsLoggedIn();
	
	[DllImport("__Internal")]
	private static extern void _twitterLoggedInUsername();
	
	[DllImport("__Internal")]
	private static extern void _twitterLogin( string username, string password );
	
	[DllImport("__Internal")]
	private static extern void _twitterLogout();
	
	[DllImport("__Internal")]
	private static extern void _twitterPostStatusUpdate( string status );

	[DllImport("__Internal")]
	private static extern void _twitterIsFollowed( string ScreenName );

	[DllImport("__Internal")]
	private static extern void _twitterFollowIt( string ScreenName );


	#endif



	// Initializes the Twitter plugin and sets up the required oAuth information
	public static void twitterInit( string consumerKey, string consumerSecret )
	{
		//Debug.Log ("twitterInit");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterInit", new object[3] {activity,consumerKey,consumerSecret});	
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterInit( consumerKey, consumerSecret );
		}
		#endif
	}
	
	
	// Checks to see if there is a currently logged in user
	public static void twitterIsLoggedIn()
	{
		//Debug.Log ("twitterIsLoggedIn");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterIsLoggedIn", new object[1] {activity});	
		#endif

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterIsLoggedIn();
		}
		#endif
	}
	
	
	// Retuns the currently logged in user's username
	public static void twitterLoggedInUsername()
	{
		//Debug.Log ("twitterLoggedInUsername");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterLoggedInUsername", new object[1] {activity});	
		#endif

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterLoggedInUsername();
		}
		
		#endif
		
	}
	
	// Logs in the user using xAuth
	public static void twitterLogin( bool IsTweet )
	{
		//Debug.Log ("twitterLogin");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterLogin", new object[2] {activity,IsTweet});	
		#endif

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterLogin ("","");
		}
		#endif
	}
	
	// Logs out the current user
	public static void twitterLogout()
	{
		//Debug.Log ("twitterLogout");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterLogout", new object[1] {activity});	
		#endif

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterLogout();
		}
		#endif
	}
	
	// Posts the status text.  Be sure status text is less than 140 characters!
	public static void twitterPostStatusUpdate( string status )
	{
		//Debug.Log ("twitterPostStatusUpdate");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterPostStatusUpdate", new object[2] {activity,status});	
		#endif

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterPostStatusUpdate( status );
		}
		#endif
	}
	
	// Receives tweets from the users home timeline
	public static void twitterGetHomeTimeline()
	{
		//Debug.Log ("twitterGetHomeTimeline");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterGetHomeTimeline", new object[1] {activity});	
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterGetHomeTimeline();
		}
		#endif
	}
	
	public static void twitterIsFollowed(string ScreenName)
	{
		//Debug.Log ("twitterIsFollowed");
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("twitterIsFollowed", new object[2] {activity, ScreenName});	
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_twitterIsFollowed(ScreenName);
		}
		#endif
	}
	
	public static void twitterFollowIt(string ScreenName)
	{
		//Debug.Log ("twitterFollowIt");
		#if UNITY_ANDROID
		Application.OpenURL("http://m.twitter.com/"+ScreenName);
		/*AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		Application.OpenURL("http://m.twitter.com/"+ScreenName);
		//pluginClass.CallStatic("twitterGetHomeTimeline", new object[2] {activity, ScreenName});*/	
		#endif
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			//_twitterFollowIt(ScreenName);
			Application.OpenURL("https://m.twitter.com/"+ScreenName);
		}
		#endif
	}

	public static void CheckNetwork()
	{
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("CheckNetworkAvailable", new object[1] {activity});	
		#endif
	}
	
}
