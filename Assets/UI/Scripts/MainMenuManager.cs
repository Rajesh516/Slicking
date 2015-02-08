using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {
	public static MainMenuManager Instance;
	public GameObject ArrowMenu;
	public GameObject ChangeUserName;
	public tk2dUITextInput input;

	// Use this for initialization
	void Awake() {
		Instance = this;
	}

	void Start () {
		HideArrowMenu ();
	}
	
	// Update is called once per frame
	public void ShowArrowMenu () {
		ArrowMenu.SetActive (true);
	}

	public void HideArrowMenu () {
		ChangeUserName.SetActive (false);
		ArrowMenu.SetActive (false);
	}

	public void ShowChangeUserName() {
		ChangeUserName.SetActive (true);
	}

	public void OnChangeUsernameSubmit() {
		DataManager.Instance.SetUserName (input.Text);
		ChangeUserName.SetActive (false);
	}
}
