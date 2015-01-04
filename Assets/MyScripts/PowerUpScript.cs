using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour 
{
	public bool isMagnet,isFastLegs,isSheild,isWings;
	// Use this for initialization
	void Start () 
	{
		GetComponent<BoxCollider> ().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player"))
			PlayerManager.Instance.PowerUpsCollected(gameObject.collider);
	}
}
