using UnityEngine;
using System.Collections;

public class SaveMeManager : MonoBehaviour {
	public static SaveMeManager Instance;
	public int Intialval = 1;
	int SaveMeDiamonds ;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameStart() {
		SaveMeDiamonds = Intialval;
	}

	public void OnSkipClick() {
		SaveMeDiamonds++;
		GameOverManager.Instance.hideTimer ();
	}

	public int GetCurrentSaveMeDiamonds() {
		return SaveMeDiamonds;
	}

}
