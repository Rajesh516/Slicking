using UnityEngine;
using System.Collections;

public class ResetJumpTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli)
	{
		print ("-----" + colli.gameObject.tag.Equals ("Player"));	
		if (colli.gameObject.tag.Equals ("Player"))
		{
			PlayerManager.Instance.CanJumpSetter(1);
		}
	}
}
