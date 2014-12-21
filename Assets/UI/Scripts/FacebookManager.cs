using UnityEngine;
using System.Collections;

public class FacebookManager : MonoBehaviour {
	// Use this for initialization
	public static FacebookManager Instance;

	void Awake(){
		Instance = this;
	}

	void OnEnable() {

		FBIntegrate.OnFBLoggedIn += HandleOnFBLoggedIn;
		FBIntegrate.OnFBLoginFailed += HandleOnFBLoginFailed;
	    FBIntegrate.OnGetAppScore += HandleOnGetAppScore;
		FBIntegrate.OnProfilePicsCall += HandleOnProfilePicsCall; 
		FBIntegrate.OnScorePostCall += HandleOnScorePostCall;
		FBIntegrate.OnScorePostFailedCall += HandleOnScorePostFailedCall;
		FBIntegrate.OnTimeExceed += HandleOnTimeExceed;
		FBIntegrate.MyScoreSucced += HandleMyScoreSucced;
		FBIntegrate.MyScoreFailed += HandleMyScoreFailed;
	}
	
	void OnDisable(){
		FBIntegrate.OnFBLoggedIn -= HandleOnFBLoggedIn;
		FBIntegrate.OnGetAppScore -= HandleOnGetAppScore;
		FBIntegrate.OnFBLoginFailed -= HandleOnFBLoginFailed;
		FBIntegrate.OnProfilePicsCall -= HandleOnProfilePicsCall;
		FBIntegrate.OnScorePostCall -= HandleOnScorePostCall;
		FBIntegrate.OnScorePostFailedCall -= HandleOnScorePostFailedCall;
		FBIntegrate.OnTimeExceed -= HandleOnTimeExceed;
		FBIntegrate.MyScoreSucced -= HandleMyScoreSucced;
		FBIntegrate.MyScoreFailed -= HandleMyScoreFailed;
	}

	public void Login(){
		if (!FB.IsLoggedIn) {
			FBIntegrate.Instance.Login ();	
		} 
		else {
			HandleOnFBLoggedIn ();
		}
	}

	void HandleOnFBLoggedIn (){
		WallManager.Instance.ShowWall ("Connecting...", 25);
		FBIntegrate.Instance.PostAppScore (DataManager.Instance.GetBestScore ().ToString ());
	}

	void HandleOnFBLoginFailed (string error){
		FB.Logout ();
		Debug.Log ("HandleOnFBLoginFailed: "+error);
		OnFailedEvents ();
	}
	
	void HandleOnScorePostCall (){
		WallManager.Instance.ShowWall ("Posting Score...", 25);
		FBIntegrate.Instance.GetAppScore ();
	}
	
	void HandleOnScorePostFailedCall (string error)	{
		Debug.Log ("HandleOnScorePostFailedCall: "+error);
		OnFailedEvents ();
	}

	
	void HandleOnGetAppScore () {
		WallManager.Instance.ShowWall ("Getting Data...", 25);
		GetProfilePicture ();
	}

	void GetProfilePicture() {
		WallManager.Instance.ShowWall ("Getting Data...", 25);
		//UIGameManager.Instance.ShowWall ("Getting Data...",25);
		FBIntegrate.Instance.getProfileImage ();
	}

	void HandleOnTimeExceed (){
		Debug.Log ("HandleOnTimeExceed: ");
		OnFailedEvents ();
	}
	
	void HandleOnProfilePicsCall (){
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Making LeaderBoard...");
		Debug.Log ("HandleOnProfilePicsCall: ");
		MakeLeaderBoard ();
	}

	
	void HandleMyScoreSucced () {
	}
	
	void HandleMyScoreFailed (string obj) {
		Debug.Log (obj);
		OnFailedEvents ();
	}


	public void MakeLeaderBoard() {
		LeaderBoardManager.Instance.MakeLeaderBoard ();
	}

	void OnFailedEvents() {
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Login Failed.. \n Try Again");
		LeaderBoardManager.Instance.OnLoginFailed ();
	}

}
