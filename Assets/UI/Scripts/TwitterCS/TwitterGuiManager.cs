using UnityEngine;
using System.Collections;

public class TwitterGuiManager : MonoBehaviour {
	public static TwitterGuiManager Instance;
	public static bool IsLoggedIn = false;
	public static bool IsNetworkOn = false;
	public string UserName = "Default";
	public string ConsumerKey = "prJi1B44HgiyOzvymmdzTA";
	public string ConsumerSecret = "IjokU2knqarBQ60FdPeEyhve7ZUw1SQRcX7btaTHUY4";
	// Use this for initialization

	void Awake() {
		//Debug.Log ("TwitterGuiManager ");
			Instance = this;
	}

	void Start () {
		Init ();
	}

	void OnEnable()	{
		TwitterManager.IsUserlogin += HandleIsUserlogin;
		TwitterManager.twitterLogin += HandletwitterLogin;
		TwitterManager.twitterLogoutSucced += HandletwitterLogoutSucced;
		TwitterManager.twitterUserNameCallback += HandletwitterUserNameCallback;
	}
	
	public void Init() {
		TwitterBinding.twitterInit (ConsumerKey,ConsumerSecret);
	}

	IEnumerable Delay() {	
		yield return new WaitForSeconds(1);
		TwitterBinding.twitterIsLoggedIn ();
	}

	public void Logintwitter(){
		if (IsLoggedIn) {
			HandletwitterLogin();
			return;		
		}
		TwitterBinding.twitterLogin (false);
		WallManager.Instance.ShowWall ("Connecting..",25);
	}

	void HandletwitterLogin (){
		IsLoggedIn = true;
		WallManager.Instance.ShowWall ("Getting Data..",25);
		TwitterBinding.twitterLoggedInUsername ();
	}
	
	void HandletwitterUserNameCallback (string obj){
		UserName = obj;
		DataManager.Instance.SetUserName (obj);
		WallManager.Instance.HideWall ();
	}

	void HandletwitterLogoutSucced (){
		IsLoggedIn = false;
		WallManager.Instance.HideWall ();
	}

	void HandleIsUserlogin (string error)	{
		if (error.Equals ("1"))
			IsLoggedIn = true;
		else
			IsLoggedIn = false;
	}

	void OnGUI()
	{
			/*if(GUI.Button (new Rect(100,0,100,100),"Login"))
			TwitterBinding.twitterLogin (false);
			if(GUI.Button (new Rect(100,100,100,100),"Tweet"))
			TwitterBinding.twitterPostStatusUpdate("Helloee");
			if(GUI.Button (new Rect(100,200,100,100),"Logoout"))
			TwitterBinding.twitterLogout ();*/

	}
}
