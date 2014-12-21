using UnityEngine;
using System.Collections;

public class powerUpManager : MonoBehaviour {
	public static powerUpManager Instance;

	public GameObject PowerUpGameobject;
	public GameObject PowerUpGameobject2;
	public GameObject[] Icons;
	public GameObject[] Icons2;
	public int TotalIcons;
	public float GapIcons;
	public Vector2 IntialPos ;

	bool IsCurrentPowerActive1 = false;	
	bool IsCurrentPowerActive2 = false;
	int CurrentOrder = 0;

	// Use this for initialization

	void Awake () {
		Instance = this;
	}

	void OnEnable() {
		if (!IsCurrentPowerActive1 || !IsCurrentPowerActive2) {
			PowerUpGameobject.SetActive (false);
			PowerUpGameobject2.SetActive (false);
		}
	}

	public void StartPowerLoader(PowerUpsType type) {
		switch (type) {
		case PowerUpsType.Shield : StartLoader(StaticData.Instance.GetPowerUpTimer (type)); break;
		case PowerUpsType.Magnet : StartLoader(StaticData.Instance.GetPowerUpTimer (type)); break;
		case PowerUpsType.FastLegs : StartLoader(StaticData.Instance.GetPowerUpTimer (type)); break;
		case PowerUpsType.Wings : StartLoader(StaticData.Instance.GetPowerUpTimer (type)); break;
		}
	}	
	
	 void StartLoader(float Timer) {
		float TimerPerIcon = Timer / TotalIcons;
		if (!IsCurrentPowerActive1) {
			CallLoader1(TimerPerIcon);
			CurrentOrder = 0;
		} else { 
			CallLoader2(TimerPerIcon);
			CurrentOrder = 1;
		}
	}

	void CallLoader1(float Timer ){
		IsCurrentPowerActive1 = true;
		PowerUpGameobject.SetActive (true);
		ArrangeIcons (Icons);
		StartCoroutine ("Loader", Timer);
	}
	
	void CallLoader2(float Timer ) {
		IsCurrentPowerActive2 = true;
		PowerUpGameobject2.SetActive(true);
		ArrangeIcons (Icons2);
		StartCoroutine ("Loader2", Timer);
	}
	
	public IEnumerator Loader(float Timer)	{
		for (int i=0; i < Icons.Length; i++) {
			yield return new WaitForSeconds(Timer);
			Icons[i].SetActive (false);
		}
		PowerUpGameobject.SetActive (false);
		IsCurrentPowerActive1 = false;
	}
	
	public IEnumerator Loader2(float Timer)	{
		for (int i=0; i < Icons2.Length; i++) {
			yield return new WaitForSeconds(Timer);
			Icons2[i].SetActive (false);
		}
		
		PowerUpGameobject2.SetActive (false);
		IsCurrentPowerActive2 = false;
	}
	
	int Lines ;
	int Line2 ;
	
	public void ResumeLoader1(float RemTime, float TotalTime) {
		Debug.Log ("ResumeLoader1");
		float TimerPerIcon = TotalTime / TotalIcons;
		Lines =  (int)(RemTime / TimerPerIcon) + 1;
		Debug.Log ("RemTime "+RemTime);
		Debug.Log ("TotalTime "+TotalTime);
		Debug.Log ("TimerPerIcon "+TimerPerIcon);
		Debug.Log ("Lines "+Lines);
		for (int i=0; i<Icons.Length - Lines; i++) {
			Icons[i].SetActive (false);	
		}
		Debug.Log ("ResumeLoader11");
		StartCoroutine ("Resumer",TimerPerIcon);
	}
	
	IEnumerator Resumer(float Timer)	{
		for (int i=Icons.Length - Lines; i < Icons.Length; i++) {
			yield return new WaitForSeconds(Timer);
			Icons[i].SetActive (false);
		}
		PowerUpGameobject.SetActive (false);
		IsCurrentPowerActive1 = false;
	}
	
	public void ResumeLoader2(float RemTime, float TotalTime) {
		float TimerPerIcon = TotalTime / TotalIcons;
		Line2 =  (int)(RemTime / TimerPerIcon) + 1;
		for (int i=0; i<Icons2.Length - Line2; i++) {
			Icons2[i].SetActive (false);	
		}
		
		StartCoroutine ("Resumer2",TimerPerIcon);
	}
	
	IEnumerator Resumer2(float Timer)	{
		for (int i=Icons2.Length - Line2; i < Icons2.Length; i++) {
			yield return new WaitForSeconds(Timer);
			Icons2[i].gameObject.SetActive (false);
		}
		PowerUpGameobject2.SetActive (false);
		IsCurrentPowerActive2 = false;
	}
	
	void ArrangeIcons(GameObject[] Icons) {
		float Gapx;
		Gapx = IntialPos.x;
		for (int i=Icons.Length-1; i>=0; i--) {
			Icons[i].SetActive (true);
			Icons[i].transform.localPosition  =  new Vector3(Gapx,0,0);
			Gapx += GapIcons;
		}
	}
	
	public void StopAllPowerUpCoroutines()	{
		HandleGameStartedEvent ();
	}
	
	void HandleGameStartedEvent (){
		StopCoroutine ("Loader2");
		StopCoroutine ("Loader");
		StopCoroutine ("Resumer");
		StopCoroutine ("Resumer2");
		PowerUpGameobject.SetActive (false);
		PowerUpGameobject2.SetActive (false);
		IsCurrentPowerActive1 = false;
		IsCurrentPowerActive2 = false;
	}
		
}
