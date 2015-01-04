using UnityEngine;
using System.Collections;

public class ActivateChildOfGroupInFourthLayer : MonoBehaviour {
	PlayerManager playerManager;
	// Use this for initialization
	void Start () 
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.name.Equals("Platform_Initial"))
			{
				childTransform.gameObject.AddComponent<PlatformInitial>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerManager = PlayerManager.Instance;
		if (playerManager.InReviveState () || playerManager.IsIncreaseSpeedActive() || IsTesting.instance.isTesting) 
		{
			ActivateInitialPlatform();	
		}
	}

	public void ActivateChildren()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.tag.Equals("Obstacles"))
			{
				childTransform.GetComponent<MeshRenderer>().enabled = true;
				childTransform.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void DeActivateChildren()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.tag.Equals("Obstacles"))
			{
				childTransform.GetComponent<MeshRenderer>().enabled = false;
				childTransform.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}

	public void ActivateInitialPlatform()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.name.Equals("Platform_Initial"))
			{
				childTransform.gameObject.SetActive(true);
			}
			if(childTransform.name.Equals("FlyingPlatform"))
			{
				childTransform.gameObject.SetActive(false);
			}
		}
	}

	public void DeActivateInitialProblem()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.name.Equals("Platform_Initial"))
			{
				childTransform.gameObject.SetActive(false);
			}
			if(childTransform.name.Equals("FlyingPlatform"))
			{
				childTransform.gameObject.SetActive(true);
			}
		}
	}
}
