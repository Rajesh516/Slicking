using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {
	public static DataManager Instance;
	public bool ClearPrefabs = false;
	public bool GiveDiamonds = false;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Start () {
		if (ClearPrefabs)
			PlayerPrefs.DeleteAll ();

		if (GiveDiamonds)
			SetTotalDiamonds (100);
	}

	public string GetUserName( ) {
		return PlayerPrefs.GetString ("UserName", "Default");
	}
	
	public void SetUserName(string Name) {
		PlayerPrefs.SetString ("UserName", Name);
	}


	public int GetTotalCoins( ) {
		return PlayerPrefs.GetInt ("Coins", 0);
	}

	public void SetTotalCoins(int Coins) {
		PlayerPrefs.SetInt ("Coins", GetTotalCoins() +  Coins);
	}

	public int GetTotalDiamonds( ) {
		return PlayerPrefs.GetInt ("Diamonds", 0);
	}
	
	public void SetTotalDiamonds(int Diamonds) {
		PlayerPrefs.SetInt ("Diamonds", GetTotalDiamonds() +  Diamonds);
	}

	public void SetCurrentCharacter(int Order) {
		PlayerPrefs.SetInt ("Character", Order);
	}

	public int GetCurrentCharacter() {
		return PlayerPrefs.GetInt ("Character", 0);
	}

	public void SetCurrentCoins(int Value) {
		PlayerPrefs.SetInt ("CurrentCoins", GetCurrentCoins() + Value);
	}
	
	public int GetCurrentCoins() {
		return PlayerPrefs.GetInt ("CurrentCoins", 0);
	}

	public void SetBestScore(int Value) {
		PlayerPrefs.SetInt ("BestScore", Value);
	}
	
	public int GetBestScore() {
		return PlayerPrefs.GetInt ("BestScore", 0);
	}
	

	public void SetCurrentPowersUpLevel(PowerUpsType powerType, int Level) {
		PlayerPrefs.SetInt ( string.Format ("CurrentPowersUpLevel{0}", powerType),Level);
	}

	public int GetCurrentPowersUpLevel(PowerUpsType powerType) {
		return PlayerPrefs.GetInt ( string.Format ("CurrentPowersUpLevel{0}", powerType),0);
	}
}
