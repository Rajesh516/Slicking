using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {
	public static CharacterManager Instance;
	public GameObject[] Characters;
	public int CurrOrder = 0;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	void OnEnable() {
		MoveCharacters (DataManager.Instance.GetCurrentCharacter ());
	}
	
	// Update is called once per frame
	void OnRightClick () {
		MoveCharacters (CurrOrder + 1);
	}

	void OnLeftClick () {
		MoveCharacters (CurrOrder - 1);
	}

	void OnSelectClick () {
		DataManager.Instance.SetCurrentCharacter (CurrOrder);
		UIManager.Instance.ShowMainMenuScreen ();
	}

	void MoveCharacters(int Order) {
		if (Order >= Characters.Length || Order < 0)
			return;

		for (int i=0; i<Characters.Length; i++) {
			if(i == Order) {
				Characters[Order].SetActive (true);
			} else
			Characters[i].SetActive (false);
		}
		CurrOrder = Order;

	}
}
