using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;


// Any methods that Obj-C calls back using UnitySendMessage should be present here
public class TwitterManager : MonoBehaviour
{
	// Events and delegates
	public delegate void TwitterErrorEventHandler( string error );
	public delegate void TwitterStringEventHandler( string result );
	public delegate void TwitterArrayListEventHandler( ArrayList result );
	public delegate void TwitterObjectEventHandler( object result );
	public delegate void TwitterEventHandler();
	public delegate void TwitterIsUserEventHandler(string error);
	

	public static event TwitterEventHandler twitterLogin;
	public static event TwitterErrorEventHandler twitterLoginFailed;
	public static event TwitterEventHandler twitterPost;
	public static event TwitterErrorEventHandler twitterPostFailed;
	public static event TwitterArrayListEventHandler twitterHomeTimelineReceived;
	public static event TwitterErrorEventHandler twitterHomeTimelineFailed;
	public static event TwitterIsUserEventHandler IsUserlogin;


	public static event Action<string> twitterUserFollowsCallBack;
	public static event Action<string> twitterUserNameCallback;
	public static event Action twitterFollowSucced;
	public static event Action twitterLogoutSucced;
	public static event Action<string> IsNetworkon;

	static string Path = TwitterBinding.Path;

/*#if UNITY_IPHONE 
	[DllImport("__Internal")]
	extern static public void _SetCallbackHandlerNameTwitter(string handlerName);
#endif*/

	void Awake()
	{
		// Set the GameObject name to the class name for easy access from Obj-C
		this.gameObject.name = "TwitterManager";
		DontDestroyOnLoad( this );
		SetCallbackHandlerName ("TwitterManager");
		//SetCallbackHandlerName(gameObject.name);
	}

	
	#region Twitter
	
	// Twitter
	public void twitterLoginSucceeded( string empty ){
		if( twitterLogin != null )
			twitterLogin();
	}

	public void OnCallHandler( string OnCallHandlerMsg ){
		Debug.Log ("OnCallHandlerTwitter :"+OnCallHandlerMsg);
	}

	public void twitterUserName( string Name ){
		if (Name.Length == 0)
			return;
		Debug.Log (Name);
		twitterUserNameCallback(Name);
	}
	
	
	public void twitterLoginDidFail( string error ){
		if( twitterLoginFailed != null )
			twitterLoginFailed( error );
		Debug.Log ("twitterLoginDidFail "+error);
	}
	
	
	public void twitterPostSucceeded( string empty ){
		if( twitterPost != null )
			twitterPost();
	}
	
	
	public void twitterPostDidFail( string error ){
		if( twitterPostFailed != null )
			twitterPostFailed( error );
	}
	
	
	public void twitterHomeTimelineDidFail( string error ){
		if( twitterHomeTimelineFailed != null )
			twitterHomeTimelineFailed( error );
	}
	
	
	public void twitterHomeTimelineDidFinish( string results ){
		if( twitterHomeTimelineReceived != null ){
			//ArrayList resultList = (ArrayList)MiniJSON.JsonDecode( results );
			//twitterHomeTimelineReceived( resultList );
		}
	}

	public void twitterIsUserLoginCallBack( string results ){
		Debug.Log (results);
			if(results.Equals("1"))
				Debug.Log ("Login");
			else
				Debug.Log ("Not Login");
		IsUserlogin (results);
	}

	public void twitterLogoutSucceded(){
		Debug.Log ("Logout");
		twitterLogoutSucced ();
	}

	public void twitterUserFollows(string results){
		Debug.Log (results);
		if(results.Equals("1"))
			Debug.Log ("Follows");
		else
			Debug.Log ("Not Follows");

		twitterUserFollowsCallBack (results);
	}

	public void twitterFollowSucceeded(){
		Debug.Log ("twitterFollowSucceeded");
		twitterFollowSucced ();
	}

	public void twitterFollowDidFail(string error){
		Debug.Log (error);
	}

	public void CheckNetworkCallback(string results){
		IsNetworkon (results);
	}

	// Set the name of the callback handler so the right component gets ad callbacks.
	public static void SetCallbackHandlerName(string callbackHandlerName){
		#if UNITY_ANDROID
		AndroidJavaClass playerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject> ("currentActivity");		
		AndroidJavaClass pluginClass = new AndroidJavaClass (Path); //com.finoit.plugin.FinoitPluginJava // com.google.unity.AdMobPlugin
		pluginClass.CallStatic("setCallbackHandlerNameTwitter", new object[2] {activity,callbackHandlerName});	
		#endif
	}
	
	#endregion;
	
}