using UnityEngine;
using System.Collections;

public class BrokenPlatformScript : MonoBehaviour {
	BoxCollider boxCollider;
	// Use this for initialization
	void Start () 
	{
		boxCollider = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (PlayerManager.Instance.MyTransform().y - transform.position.y  < 0) 
		{
			boxCollider.isTrigger = true;
		}
		else
		{
			if (PlayerManager.Instance.MyVelocity ().y > 0) 
			{
						boxCollider.isTrigger = true;	
			} 
			else
						boxCollider.isTrigger = false;
		}
	}
}
