using UnityEngine;
using System.Collections;

public class BankStaticData : MonoBehaviour {
	public static BankStaticData Instance;
	public int[] Diamonds;
	public string[] Cost;
	public string[] SKU;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public decimal GetCost(int Order) {
		return System.Decimal.Parse (Cost[Order]);
	}

	public int GetDiamonds(int Order) {
		return Diamonds[Order];
	}

	public string GetSKUs(int Order) {
		return SKU[Order];
	}
}
