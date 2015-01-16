using UnityEngine;
using System.Collections;

public class BrokenPlatformJumpReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player")) 
		{
			PlayerManager.Instance.CanJumpSetter(1);
		}
	}
}
