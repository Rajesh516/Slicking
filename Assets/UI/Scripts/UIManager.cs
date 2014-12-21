using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	public static UIManager Instance;

	public GameObject MainMenuScreen;
	public GameObject ShopScreen;
	public GameObject UpgradesScreen;
	public GameObject PurchaseScreen;
	public GameObject PauseScreen;
	public GameObject GameOverScreen;
	public GameObject InGameUIScreen;
	public GameObject SettingScreen;
	public GameObject CharacterScreen;
	public GameObject LanguageScreen;
	public GameObject LeaderBoardScreen;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	// Update is called once per frame
	public void ShowMainMenuScreen () {
		MainMenuScreen.SetActive (true);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	// Update is called once per frame
	public void ShowSettingScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (true);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowShopScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (true);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowPauseScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (true);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowResumeScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (true);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowGameOverScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (true);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowInGameUIScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (true);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowUpgradeScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (true);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
		ShopManager.Instance.OnUpdate ();
	}
	
	public void ShowPurchaseScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (true);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowCharacterScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (true);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowLanguageScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (true);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (false);
	}

	public void ShowLeaderBoardScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
		CharacterScreen.SetActive (false);
		LanguageScreen.SetActive (false);
		UpgradesScreen.SetActive (false);
		PurchaseScreen.SetActive (false);
		SettingScreen.SetActive (false);
		LeaderBoardScreen.SetActive (true);
	}
}
