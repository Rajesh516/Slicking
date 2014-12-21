using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour 
{
	public Texture2D[] birdFlyAnimationTexture = new Texture2D[4];
	bool birdFlyAnimation;
	int birdFlyCount;
	void OnEnable()
	{
		birdFlyCount = 0;
		birdFlyAnimation = true;
		StartCoroutine ("BirdFlyAnimation");
	}

	void OnDisable()
	{
		birdFlyCount = 0;
		birdFlyAnimation = false;
		StopCoroutine ("BirdFlyAnimation");
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	IEnumerator BirdFlyAnimation()
	{
		while (birdFlyAnimation) 
		{
			yield return new WaitForSeconds (0.1f);
			renderer.material.mainTexture = birdFlyAnimationTexture[birdFlyCount];
			birdFlyCount++;
			if(birdFlyCount == birdFlyAnimationTexture.GetLength(0))
				birdFlyCount = 0;
		}
	}
}
