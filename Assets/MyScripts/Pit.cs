using UnityEngine;
using System.Collections;

public class Pit : MonoBehaviour {
	BoxCollider pitBoxCollider;
	// Use this for initialization
	void Start () {
		pitBoxCollider = GetComponent<BoxCollider> ();
		pitBoxCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player")) 
		{
			PlayerManager.Instance.ObstacleCollided(gameObject.collider);	
		}
	}
}
