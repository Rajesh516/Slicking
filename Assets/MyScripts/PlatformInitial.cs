using UnityEngine;
using System.Collections;

public class PlatformInitial : MonoBehaviour {
	PlayerManager playerManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		playerManager = PlayerManager.Instance;
		if (playerManager.IsIncreaseSpeedActive ()) 
		{
			;	
		}
	}


}
