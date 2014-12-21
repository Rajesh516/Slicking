using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {
	public static ShopManager Instance;
	public tk2dSprite[] PowerScroll;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Purchase1() {
		BankManager.Instance.OnBuyClickCoin (0);
	}

	void Purchase2() {
		BankManager.Instance.OnBuyClickCoin (1);
	}

	void Purchase3() {
		BankManager.Instance.OnBuyClickCoin (2);
	}

	void Purchase4() {
		BankManager.Instance.OnBuyClickCoin (3);
	}

	
	void OnShieldBuy() {
		SetPowerUp (PowerUpsType.Shield);
	}

	void OnMagnetBuy() {
		SetPowerUp (PowerUpsType.Magnet);
	}

	void OnFastLegsBuy() {
		SetPowerUp (PowerUpsType.FastLegs);
	}

	void OnWingsBuy() {
		SetPowerUp (PowerUpsType.Wings);
	}


	void SetPowerUp(PowerUpsType powerType) {
		if (StaticData.Instance.GetPowerCost (powerType) > DataManager.Instance.GetTotalCoins () || (DataManager.Instance.GetCurrentPowersUpLevel(powerType)+ 1) >= StaticData.Instance.GetEachPowerUpMaxLevel() ){
			return;		
		}
	
		switch(powerType) {
		case PowerUpsType.Shield : break;
		case PowerUpsType.FastLegs : break;
		case PowerUpsType.Magnet : break;
		case PowerUpsType.Wings : break;
		}

		DataManager.Instance.SetCurrentPowersUpLevel (powerType, DataManager.Instance.GetCurrentPowersUpLevel(powerType)+ 1);
		DataManager.Instance.SetTotalCoins (-StaticData.Instance.GetPowerCost (powerType));
		OnUpdate ();
		Debug.Log ("Reminaing Diamonds :" + DataManager.Instance.GetTotalCoins ());
	}

	public void OnUpdate() {
		for (int i=0; i<StaticData.Instance.GetTotalPowerUps(); i++) {
			PowerScroll[i].SetSprite (StaticData.Instance.GetPowerUpPowerScrollSprite ( DataManager.Instance.GetCurrentPowersUpLevel ( ( PowerUpsType) i ) ).ToString() ) ;
		}
	}
}
