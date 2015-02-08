using UnityEngine;
using System.Collections;
using System;

public class AdMobGuiManager : MonoBehaviour {
	string extras = "{\"color_bg\":\"AAAAFF\", \"color_bg_top\":\"FFFFFF\"}";
	public static bool IsAdCreated = false;

	public static AdMobGuiManager Instance;
	public string PubIDAndroid = "ca-app-pub-3312456359446231/5480232104";
	public string PubIDIOS = "ca-app-pub-3312456359446231/2114636506";

	public void Awake(){
		Instance = this;
	}

	void Start() {
		Init ();
	}
	
	void OnEnable(){
		AdMobManager.ReceivedAd += HandleReceivedAd;
		AdMobManager.FailedToReceiveAd += HandleFailedToReceiveAd;
	}
	
	public void Init () {			//Android 595bb129f60b4751 // Iphone 52e5e35b50fd491f
		//CreateInterstitial ();
	}

	
	void CreateInterstitial() {
		#if UNITY_ANDROID	
		AdMobBinding.createInterstitialView (PubIDAndroid);    
		#endif
		#if UNITY_IPHONE	
		AdMobBinding.createInterstitialView (PubIDIOS);
		#endif
	}

	void HandleReceivedAd () {
		IsAdCreated = true;
		AdMobBinding.displayInterstitial ();
	}

	void HandleFailedToReceiveAd (string obj) {
		IsAdCreated = false;
	}

	public void HideInterstitial() {
		IsAdCreated = false;
		AdMobBinding.HideInterstitial ();
	}



	void OnGUI()
	{
		/*try
		{	
			if(GUI.Button (new Rect(100,0,100,100),"CreateInterstitial"))
				AdMobBinding.CreateBannerView ("7da2ad1de485460b", AdMobBinding.AdSize.Banner,true);
			if(GUI.Button (new Rect(100,100,100,100),"RequestBannerAd"))
				AdMobBinding.RequestBannerAd(true,extras);
			if(GUI.Button (new Rect(100,200,100,100),"CreateInterstitial"))
				AdMobBinding.createInterstitialView ("52e5e35b50fd491f");
			if(GUI.Button (new Rect(100,300,100,100),"DisplayInterstitial"))
				AdMobBinding.displayInterstitial();
			
		}
		catch(Exception e) {
			AdMobManager.Error = e.ToString ();
		}*/
	}
}
