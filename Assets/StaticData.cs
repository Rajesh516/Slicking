using UnityEngine;
using System.Collections;

public enum PowerUpsType { Shield , Magnet , FastLegs , Wings};

public class StaticData : MonoBehaviour {
	public static StaticData Instance;
	public int TotalPowerUps;
	public int EachPowerUpMaxLevel;
	public string[] UpgradesPowerScolls;
	public int[] PowerUpCost;
	public float[] powerUpTimers;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public int GetTotalPowerUps( ) {
		return TotalPowerUps;
	}

	public int GetEachPowerUpMaxLevel( ) {
		return EachPowerUpMaxLevel;
	}

	public string GetPowerUpPowerScrollSprite(int Level) {
		return UpgradesPowerScolls[Level];
	}

	public float GetPowerUpTimer(PowerUpsType type) {
		switch (type) {
		case PowerUpsType.Shield : return powerUpTimers[0];
		case PowerUpsType.Magnet : return powerUpTimers[1];
		case PowerUpsType.FastLegs : return powerUpTimers[2];
		case PowerUpsType.Wings : return powerUpTimers[3];
		}
		return powerUpTimers [0];
	}

	public int GetPowerCost(PowerUpsType powerType) {

		switch(powerType) {
		case PowerUpsType.Shield : return PowerUpCost[0]; 	
		case PowerUpsType.Magnet : return PowerUpCost[1]; 
		case PowerUpsType.FastLegs : return PowerUpCost[2]; 
		case PowerUpsType.Wings : return PowerUpCost[3]; 
		}
		return 0;
	}
}
