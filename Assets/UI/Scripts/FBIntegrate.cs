using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class FBIntegrate : MonoBehaviour {
	public  static FBIntegrate Instance;

	public  List<object>                 scores          = new List<object>();
	public  List<object>                 ScoresIDs         =  new List<object>();
	public  List<object>                 ScoresNames         = new List<object>();
	public  List<object>                 ScoresScore         = new List<object>(); 
	public Dictionary<string,string> FriendsDetails = new Dictionary<string, string>();
	public List<Texture> ProfilePics = new List<Texture>();

	Texture2D MyProfilePic ;
	int MyRank = 0;
	int MyScore = 0;
	int Count ;


	// Use this for initialization
	public static event Action OnFBLoggedIn ;
	public static event Action<string> OnFBLoginFailed ;
	public static event Action OnGetAppScore ;
	public static event Action OnLikePageCall  ;
	public static event Action OnPostStatusCall ;
	public static event Action OnPostStatusFailedCall ;
	public static event Action OnProfilePicsCall ;
	public static event Action OnScorePostCall ;
	public static event Action<string> OnScorePostFailedCall ;
	public static event Action OnTimeExceed ;
	public static event Action MyScoreSucced ;
	public static event Action<string> MyScoreFailed ;

	bool IsGettingData = false;
	float Timer  = 0;
	bool TimeUp = false;

	void Awake(){
		Instance = this;
		InIt ();
	}
	
	public void InIt(){
		//FbDebug.Log("Awake");
		enabled = false;
		FB.Init(SetInit, OnHideUnity);
	}

	private void SetInit(){
		//FbDebug.Log("SetInit");
		enabled = true; // "enabled" is a property inherited from MonoBehaviour
		if (FB.IsLoggedIn) {
			FbDebug.Log("Already logged in");
			//OnLoggedIn ();
		}
	}

	void Update() {
		if (IsGettingData && Timer > 23) {
			//Debug.Log ("Timer "+Timer);
			Timer = 0;
			TimeUp = true;
			IsGettingData = false;
		}
		else if(IsGettingData) {
			Timer += Time.fixedDeltaTime;
		}
	}
	
	private void OnHideUnity(bool isGameShown) {
		//FbDebug.Log("OnHideUnity");
		if (!isGameShown)	{
			// pause the game - we will need to hide
			Time.timeScale = 0;
		}
		else {
			// start the game back up - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void SetFBScore(string Score){
		Instance.Login ();
	}


	public void Login(){
		if (!FB.IsLoggedIn) { // LOGIN
				FB.Login ("email,publish_actions,", LoginCallback); 
		}
		else {
			OnFBLoggedIn ();
		}
	}

	void LoginCallback(FBResult result) {
		if (result.Error != null) {                                                                                                                          
			FbDebug.Error (result.Error); 
			OnFBLoginFailed(result.Error);
			return;                                                                                                                
		} 
		if (!FB.IsLoggedIn) {
			OnFBLoginFailed(result.Error);
			return;	
		}

		OnFBLoggedIn ();
	}

	public void GetmyScore() {
		FB.API("me/score", Facebook.HttpMethod.GET, MyScoreCallback);
	}

	public void MyScoreCallback(FBResult result) {
		Debug.Log (result.Text);
		if (result.Error == null) {
			List<object> Tempscores = Util.DeserializeScores (result.Text, "data");
			int TempCount = Tempscores.Count;
			//{"data":[{"user":{"name":"Abhi Verma","id":"100000902450660"},"score":10000,"application":{"name":"SpellTower","id":"614975348573527"}}]}

			var Con = Tempscores [0] as Dictionary<string, object>;
			Debug.Log ("con" + Con);
			foreach (string key in Con.Keys) {
					switch (key) {				
					case "score": 				
							System.Int32.TryParse (Con [key].ToString (), out MyScore);
							break;					
					}		 
			}
			Debug.Log (MyScore);
			MyScoreSucced ();		
				
		} else {
			MyScoreFailed (result.Error);
		}
	}
	
	public void GetAppScore(){
		FB.API(FB.AppId+"/scores?friends.limit(10)", Facebook.HttpMethod.GET, AppScoreCallback);
		//FB.API("/me?fields=id,first_name,friends.limit(10).fields(first_name,id)", Facebook.HttpMethod.GET, GetMyDetailsCallBack);	// To get the list of friends of a player
	}

	public void AppScoreCallback(FBResult result) {       
		ScoresIDs = new List<object> ();
		ScoresNames = new List<object> ();
		ScoresScore = new List<object> ();
		
		scores = Util.DeserializeScores (result.Text, "data");
		Count = scores.Count;
		Debug.Log ("Total PLayer: "+ scores.Count);
		//{"data":[{"user":{"name":"Abhi Verma","id":"100000902450660"},"score":10000,"application":{"name":"SpellTower","id":"614975348573527"}}]}
		for (int i=0; i<scores.Count; i++) {

			var Con = scores [i] as Dictionary<string, object>;
			object Id = "";
			object name = "";
			object scr = "";
			foreach (string key in Con.Keys) {
				
				switch (key) {				
				case "user": 				
					var temp = (Con [key] as Dictionary<string,object>);				
					name = temp ["name"];				
					Id = temp ["id"];	
					ScoresIDs.Add (Id.ToString ());

					if(Id.Equals (FB.UserId)) {
						MyRank = i;
					}

					ScoresNames.Add (name.ToString ());
					//FriendsDetails.Add (Id.ToString (), name.ToString ());				
					break;			
				case "score": 				
					scr = Con [key].ToString ();
					ScoresScore.Add (scr);	
					break;			
				case "application":
					
					break;			
				}		
			}    
		}

		if(System.Int32.Parse(FBIntegrate.Instance.ScoresScore[MyRank].ToString ()) >= PlayerPrefs.GetInt("HighScore"))
			PlayerPrefs.SetInt("HighScore", System.Int32.Parse(FBIntegrate.Instance.ScoresScore[MyRank].ToString ()));

		OnGetAppScore ();  // Event fires after getting score from Facebook
	}

	public void PostAppScore(string Score){						// Post Score to the Facebook App
		FB.API("/me/scores", Facebook.HttpMethod.POST, SaveScoreCallback,EncryptScore(Score)); // To Push a score in player account
	}
	
	Dictionary<string,string> EncryptScore(string Score){					// Encrypt data so that score and wave can be send in a same string
		Dictionary<string,string> scoreData = new Dictionary<string, string> ();
		scoreData.Add ("score", Score);
		return scoreData;
	}

	void SaveScoreCallback(FBResult result) {     
		if (result.Error != null) {                                                                                                                          
			FbDebug.Error(result.Error);
			OnScorePostFailedCall(result.Error);
			return;                                                                                                                
		} 
		OnScorePostCall ();
	}

	public void IsLikedPage(string PageID) {
		Debug.Log (PageID);
		FB.API("me/likes/"+PageID,Facebook.HttpMethod.GET, LikePageCallback);
	}
	
	void LikePageCallback(FBResult result) {

		Debug.Log (result.Text);
		List<object> o = Util.DeserializeScores (result.Text, "data");
		Debug.Log ("Object o " + o.Count);
		if (o.Count == 0)
			return;
		if (result.Error != null) {                                                                                                                          
			FbDebug.Error (result.Error); 
			return;                                                                                                                
		} 
		
		OnLikePageCall ();
	}

	public void OpenUrl(string AppID) {
		  Application.OpenURL ("https://m.facebook.com/" + AppID);
	}

	IEnumerator OpenFacebookPage(string AppID){
		Application.OpenURL("fb://profile/"+AppID);
		yield return new WaitForSeconds(1);
		if(leftApp){
			leftApp = false;
		}
		else{
			Application.OpenURL("https://m.facebook.com/"+AppID);
		}
	}

	bool leftApp = false;
	
	void OnApplicationPause(bool pauseStatus) {
		leftApp = pauseStatus;
	}

	public void PostStatusOntimeline(string Link, string LinkName, string LinkCaption, string LinkDescription) {
		FB.Feed ( 
			toId : "",
			link: Link,					//"https://facebook.com/YourGameURL",
		    linkName: LinkName,			//"YourGameName!",
			linkCaption: LinkCaption,
			linkDescription:  LinkDescription,
			//picture: "https://example.com/myapp/assets/1/larch.jpg",
			callback: PostStatusOntimelineCallback);
	}

	void PostStatusOntimelineCallback(FBResult response) {
		var result = Facebook.MiniJSON.Json.Deserialize(response.Text) as Dictionary<string, object>;

		if(result.ContainsKey ("cancelled")){
		     bool CancelledResult = (bool) result ["cancelled"];
		if (response.Error != null || CancelledResult) {                                                                                                                          
			OnPostStatusFailedCall();
			return;                                                                                                                
		 } 
		}
			
		OnPostStatusCall ();
	}

	 public void getProfileImage() {
		ProfilePics = new List<Texture> ();
		IsGettingData = true;
		Timer = 0;
		TimeUp = false;
		StartCoroutine ("getProfileImageFromNet");
	}

	public IEnumerator getProfileImageFromNet() {
		for (int i=0; i<ScoresIDs.Count; i++) {

			if (FB.IsLoggedIn) {
				WWW url = new WWW ("https" + "://graph.facebook.com/" + ScoresIDs[i] + "/picture?type=large&redirect=false" +"?access_token=" + FB.AccessToken);

				if(TimeUp) {
					OnTimeExceed();
					TimeUp = false;
					url.Dispose ();
					yield break;
				}
				yield return url;

				Texture2D textFb2 = new Texture2D (128, 128, TextureFormat.ARGB32, false); //TextureFormat must be DXT5
				url.LoadImageIntoTexture (textFb2);
				if(ScoresIDs[i].Equals (FB.UserId))
					MyProfilePic = textFb2;
				ProfilePics.Add (textFb2);
			}
		}

		OnProfilePicsCall ();
	}

	
	void MyPictureCallback(FBResult result) {                                                                                                                              
		if (result.Error != null)                                                                                                  
		{                                                                                                                          
			FbDebug.Error(result.Error);                                                                                                                                                                                         
			FB.API("/me/picture", Facebook.HttpMethod.GET, MyPictureCallback);                                
			return;                                                                                                                
		}                                                                                                                          
	} 

	void GetMyDetailsCallBack(FBResult result){
		if (result.Error != null) {                                                                                                                          
			FbDebug.Error(result.Error);                                                                                                                       
			return;                                                                                                                
		} 
	}

	public int GetListCount() {
		return Count;
	}

	public Texture2D GetSelfProfilePic() {
		return MyProfilePic;
	}
	
	public int GetSelfRankinGame() {
		return MyRank;
	}
	
	public int GetSelfScoreinGame() {
		return MyScore;
	}

}
