using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour {
	
	public static WallManager Instance;
		public GameObject Wall;
		public tk2dTextMesh Walltext;
		float Timer = 0;
		bool StartTimer = false;
		public float Customtimer = 8;

		// Use this for initialization
		void Awake() {
			Instance = this;
		}
		
		void Update() {
			if (!StartTimer)
				return;
			
			if (StartTimer && Timer >= Customtimer) {
				HideWall ();    
			}
			else if (StartTimer) {
				Timer += Time.deltaTime;
			}
		}
		
		void OnEnable () {
			
		}
		
		public void ShowWall(string Text) {
			Wall.SetActive (true);
			Walltext.text = Text;
			Timer = 0;
			StartTimer = true;
		}
		
		public void ShowWall(string Text,float TimerVal) {
			Wall.SetActive (true);
			Walltext.text = Text;
			Timer = 0;
			StartTimer = true;
			Customtimer = TimerVal;
		}
		
		public void HideWall(){
			Wall.SetActive (false);
			StartTimer = false;
			Timer = 0;
			Customtimer = 8;
		}
		
		public void PopUp(string Text) {
			Wall.SetActive (true);
			Walltext.text = Text;
			StartCoroutine ("HidePopup");
		}
		
		IEnumerator HidePopup() {
			yield return new WaitForSeconds (1);
			Wall.SetActive (false);
		}
		
}
