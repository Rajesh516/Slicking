using UnityEngine;
using System.Collections;

public class LanguageManager : MonoBehaviour {
	public static LanguageManager Instance;
	public GameObject[] Languages;
	public int CurrOrder = 0;

	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void OnRightClick () {
		
	}
	
	void OnLeftClick () {
		
	}
	
	void OnSelectClick () {
		
	}
}
