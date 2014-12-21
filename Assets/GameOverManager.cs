using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
	public static GameOverManager Instance;
	public GameObject Timer;
	public TextMesh DiamondText;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	void OnEnable() {
		Timer.SetActive (true);
		DiamondText.text = SaveMeManager.Instance.GetCurrentSaveMeDiamonds().ToString ();
	}

	
	// Update is called once per frame
	void Update () {
	}

	public void hideTimer() {
		Timer.SetActive (false);
	}

	
}
