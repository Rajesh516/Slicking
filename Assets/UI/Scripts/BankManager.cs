using UnityEngine;
using System.Collections;
using OnePF;

public class BankManager : MonoBehaviour {
	public static BankManager Instance;
	BankIntializer bankIntializer;

	int CurrentOrder = 0;

	private void OnEnable() {
		// Listen to all events for illustration purposes
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		OpenIABEventManager.restoreSucceededEvent += HandlerestoreSucceededEvent;
		OpenIABEventManager.restoreFailedEvent += HandlerestoreFailedEvent;
		OpenIABEventManager.transactionRestoredEvent += HandletransactionRestoredEvent;
		/*OpenIABEventManager.transactionRestoredEvent += OnTransactionRestored;
		OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceeded;
		OpenIABEventManager.restoreFailedEvent += OnRestoreFailed;*/
	}

	private void OnDisable() {
		// Remove all event handlers
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		OpenIABEventManager.restoreSucceededEvent -= HandlerestoreSucceededEvent;
		OpenIABEventManager.restoreFailedEvent -= HandlerestoreFailedEvent;
		OpenIABEventManager.transactionRestoredEvent -= HandletransactionRestoredEvent;
		/*OpenIABEventManager.transactionRestoredEvent -= OnTransactionRestored;
		OpenIABEventManager.restoreSucceededEvent -= OnRestoreSucceeded;
		OpenIABEventManager.restoreFailedEvent -= OnRestoreFailed;*/
	}

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		bankIntializer = BankIntializer.Instance;
	}

	public void OnRestoreClick() {
		WallManager.Instance.ShowWall ("Restoring..");
		OpenIAB.restoreTransactions ();
	}
	
	public void OnBuyClickCoin(int Order) {
		if (! BankIntializer.Instance.IsBillingReady) {
			BankIntializer.Instance.MakeBillingReady(true);
			return;
		}
		CurrentOrder = Order;

		#if UNITY_ANDROID
		WallManager.Instance.ShowWall (BankIntializer.Instance.AndroidMsg);
		#endif
		#if UNITY_IPHONE
		UIManager.Instance.ShowWall ("Connecting to iTunes..");
		#endif
		#if UNITY_WP8
		UIManager.Instance.ShowWall ("Connecting to Windows Store..");
		#endif
		CallPurchaseCoin (Order);
	}

	void CallPurchaseCoin(int Order) {
		bankIntializer = BankIntializer.Instance;
		#if UNITY_ANDROID
		OpenIAB.purchaseProduct (BankStaticData.Instance.GetSKUs(Order)); 
		#endif
		#if UNITY_IPHONE
		OpenIAB.purchaseProduct (BankStaticData.Instance.GetSKUs(Order)); 
		#endif
		#if UNITY_WP8
		OpenIAB.purchaseProduct (BankStaticData.Instance.GetSKUs(Order));
		#endif
	}

	void OnSuccesfullPurchase() {
		CurrentOrder = 0;
	}

	private void queryInventorySucceededEvent(Inventory inventory) {
		Debug.Log("queryInventorySucceededEvent: " + inventory);
	}

	private void queryInventoryFailedEvent(string error) {
		Debug.Log("queryInventoryFailedEvent: " + error);
	}

	private void purchaseSucceededEvent(Purchase purchase) {
		Debug.Log ("purchaseSucceededEvent "+purchase.OriginalJson);
		OnSuccesfullPurchase ();
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Succesfully Purchased");
	}
	private void purchaseFailedEvent(int Val,string error) {
		Debug.Log ("purchaseFailedEvent "+Val+" " +error);
		CurrentOrder = 0;
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Purchase Failed "+error+" \n Try Again");
	}

	private void consumePurchaseSucceededEvent(Purchase purchase) {
		Debug.Log("consumePurchaseSucceededEvent: " + purchase.OriginalJson);
		OnSuccesfullPurchase ();
		WallManager.Instance.HideWall ();
	}

	private void consumePurchaseFailedEvent(string error) {
		Debug.Log("consumePurchaseFailedEvent: " + error);
		CurrentOrder = 0;
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Purchase Failed "+error+" \n Try Again");
	}

	void HandletransactionRestoredEvent (string obj) {
		Debug.Log("HandletransactionRestoredEvent: " + obj);
		//if (obj.Equals ("")) {
		//}
		CurrentOrder = 0;
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Restored...");
	}
	
	void HandlerestoreFailedEvent (string obj) {
		Debug.Log("HandlerestoreFailedEvent: " + obj);
		WallManager.Instance.HideWall ();
		WallManager.Instance.PopUp ("Network Problem...");
	}
	
	void HandlerestoreSucceededEvent () {
		Debug.Log("HandlerestoreSucceededEvent: ");
		WallManager.Instance.HideWall ();
	}
	
}
