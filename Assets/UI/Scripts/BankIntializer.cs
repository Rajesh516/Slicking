using UnityEngine;
using System.Collections;
using OnePF;
using System;
public class BankIntializer : MonoBehaviour {
	public static BankIntializer Instance;

	public bool iOSBuilt = false;
	public bool WindowsBuilt = false;
	public bool AndroidBuilt = false;

	bool IsBank = false;
	public bool IsBillingReady = false;
	public string AndroidMsg = "Connecting to GooglePlay..";

	private void OnEnable() {
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
	}
	
	private void OnDisable() {
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
	}
	
	void Awake() {
		Instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	void Start() {
		Init ();
	}
	
	// Use this for initialization
	public void Init () {
		Debug.Log("Init");

		#if UNITY_ANDROID
		/*
		for(int i=0;i<BankStaticData.Instance.totalInApps;i++) {
			OpenIAB.mapSku (BankStaticData.Instance.GetSKUs(i), OpenIAB_Android.STORE_GOOGLE, BankStaticData.Instance.GetSKUs(i));
		}
		AndroidMsg = "Connecting to GooglePlay..";
		*/

		// For Testing
		String SKU_Nopopad = "android.test.purchased";
		OpenIAB.mapSku (SKU_Nopopad, OpenIAB_Android.STORE_GOOGLE, SKU_Nopopad);

		#endif

		#if UNITY_IPHONE
		for(int i=0;i<BankStaticData.Instance.totalInApps;i++) {
			OpenIAB.mapSku (BankStaticData.Instance.GetSKUs[i], OpenIAB_Android.STORE_GOOGLE, BankStaticData.Instance.GetSKUs[i]);
		}
		#endif

		#if UNITY_WP8
		for(int i=0;i<BankStaticData.Instance.totalInApps;i++) {
			OpenIAB.mapSku (BankStaticData.Instance.GetSKUs[i], OpenIAB_Android.STORE_GOOGLE, BankStaticData.Instance.GetSKUs[i]);
		}
		#endif

		MakeBillingReady ();
	}
	
	public void MakeBillingReady() {
		CallBilling ();
	}

	public void MakeBillingReady(bool FromBankManager) {
		CallBilling ();
		#if UNITY_ANDROID
		WallManager.Instance.ShowWall (AndroidMsg);
		#endif
		#if UNITY_IPHONE
		WallManager.Instance.ShowWall ("Connecting to iTunes..");
		#endif

		#if UNITY_WP8
		WallManager.Instance.ShowWall ("Connecting to Windows Store..");
		#endif

		IsBank = true;
	}

	void CallBilling(){
		var options = new OnePF.Options();
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		if(AndroidBuilt)
			options.storeKeys.Add(OpenIAB_Android.STORE_GOOGLE, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmccPMzuHWO4I2Db3uQny/9bdbM6yK6ZMKmUy93XKrWDeWg08Gwa1IMP3OzsRiRndo04K3r7VFg96sCEGMzjxWpa/xyI/ELFnlWBKdW6ZsZI6UiFopaypr+otsxG0rHcprdiUcRDZftycX+iJoj3np/l3H/Qwbr05T7n4D4ETQfGszLmwbmoT3KIZCsTL8JakHhFs+hpbSZJrKjDEio2YkCfUDo+vp7UyHnBOA6XAB1GGbmUrkCHvWsRSviXzynS9blx/xRKUOx1BsOdFN8U0Jklr1OVSpoAlF9ZCJ+58Vah/0ylFeyJBoAZYLCRO98k2Lkutgqxkpz3c7+tpmDBXNwIDAQAB");
		
		OpenIAB.init(options);
	}
	
	private void billingSupportedEvent() {
		if (IsBank) {
			IsBank = false;
			IsBillingReady = true;
			WallManager.Instance.HideWall ();
			//UIManager.Instance.PopUp ("Billing Supported");
			return;
		}
	
		IsBillingReady = true;
		Debug.Log("billingSupportedEvent");
	}

	private void billingNotSupportedEvent(string error) {
		if (IsBank) {
			IsBank = false;
			IsBillingReady = false;
			WallManager.Instance.HideWall ();
			WallManager.Instance.PopUp ("Network Problem");
			return;
		}
		IsBillingReady = false;
		Debug.Log("billingNotSupportedEvent: " + error);
	}
}
