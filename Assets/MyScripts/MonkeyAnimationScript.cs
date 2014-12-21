using UnityEngine;
using System.Collections;

public class MonkeyAnimationScript : MonoBehaviour {
	public Texture2D[] monkeyRunAnimation = new Texture2D[7];
	public Texture2D[] monkeyDieAnimation = new Texture2D[7];
	public Texture2D[] monkeyJumpAnimation = new Texture2D[7];
	public Texture2D[] monkeyBananaThrowAnimation = new Texture2D[7];
	public Texture2D[] monkeySlidingAnimation = new Texture2D[7];
	int monkeyPresentState,monkeyRunCount,monkeyJumpCount,monkeyDieCount,monkeyBananaThrowCount,monkeySlidingCount;
	// Use this for initialization
	void Start () 
	{
		monkeySlidingCount = monkeyBananaThrowCount = monkeyRunCount = monkeyJumpCount = monkeyDieCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void AnimationStateSetter(int temp)
	{
		monkeyPresentState = temp;
		if (monkeyPresentState == 0) 
		{
			if(monkeyRunCount == 0)
			{
				StartCoroutine ("MonkeyRunAnimation");
				StopCoroutine("MonkeyJumpAnimation");	
				StopCoroutine("MonkeyDieAnimation");
				StopCoroutine("MonkeyBananaThrowAnimation");
				StopCoroutine("MonkeySlidingAnimation");
			}
		}
		if (monkeyPresentState == 1) 
		{
			StartCoroutine ("MonkeyJumpAnimation");
			StopCoroutine("MonkeyRunAnimation");	
			StopCoroutine("MonkeyDieAnimation");
			StopCoroutine("MonkeyBananaThrowAnimation");
			StopCoroutine("MonkeySlidingAnimation");
		}
		if (monkeyPresentState == 2) 
		{
			if(monkeyDieCount == 0)
			{
				StartCoroutine ("MonkeyDieAnimation");
				StopCoroutine("MonkeyJumpAnimation");	
				StopCoroutine("MonkeyRunAnimation");
				StopCoroutine("MonkeyBananaThrowAnimation");
				StopCoroutine("MonkeySlidingAnimation");
			}
		}

		if (monkeyPresentState == 3) 
		{
			monkeyBananaThrowCount = 0;
			StopCoroutine("MonkeyBananaThrowAnimation");
			StartCoroutine ("MonkeyBananaThrowAnimation");
			StopCoroutine("MonkeyJumpAnimation");	
			StopCoroutine("MonkeyRunAnimation");	
			StopCoroutine("MonkeyDieAnimation");
			StopCoroutine("MonkeySlidingAnimation");
		}

		if (monkeyPresentState == 4) 
		{
			monkeySlidingCount = 0;
			StopCoroutine("MonkeyBananaThrowAnimation");
			StopCoroutine ("MonkeyBananaThrowAnimation");
			StopCoroutine("MonkeyJumpAnimation");	
			StopCoroutine("MonkeyRunAnimation");	
			StopCoroutine("MonkeyDieAnimation");
			StartCoroutine("MonkeySlidingAnimation");
		}
	}

	IEnumerator MonkeyRunAnimation()
	{
		while(monkeyPresentState == 0)
		{
			renderer.material.mainTexture = monkeyRunAnimation[monkeyRunCount];
			monkeySlidingCount = monkeyBananaThrowCount = monkeyDieCount = monkeyJumpCount = 0;
			monkeyRunCount++;
		yield return new WaitForSeconds(0.03f);
		if(monkeyRunCount == monkeyRunAnimation.GetLength(0))
			monkeyRunCount = 0;
		}
	}

	IEnumerator MonkeyBananaThrowAnimation()
	{
		while(monkeyPresentState == 3)
		{
			renderer.material.mainTexture = monkeyBananaThrowAnimation[monkeyBananaThrowCount];
			monkeySlidingCount = monkeyRunCount = monkeyDieCount = monkeyJumpCount = 0;
			monkeyBananaThrowCount++;
			yield return new WaitForSeconds(0.05f);
			if(monkeyBananaThrowCount == monkeyRunAnimation.GetLength(0))
			{
				monkeyBananaThrowCount = 0;
				monkeyPresentState = 0;	
			}
		}
	}

	IEnumerator MonkeyJumpAnimation()
	{
		while(monkeyPresentState == 1)
		{
			yield return new WaitForSeconds(0.07f);
			renderer.material.mainTexture = monkeyJumpAnimation[monkeyJumpCount];
			monkeySlidingCount = monkeyBananaThrowCount = monkeyDieCount = monkeyRunCount = 0;
			monkeyJumpCount++;
			if(monkeyJumpCount == monkeyJumpAnimation.GetLength(0))
				break;
		}
	}

	IEnumerator MonkeyDieAnimation()
	{
		while(monkeyPresentState == 2)
		{
			renderer.material.mainTexture = monkeyDieAnimation[monkeyDieCount];
			monkeySlidingCount = monkeyBananaThrowCount = monkeyJumpCount = monkeyRunCount = 0;
			monkeyDieCount++;
			yield return new WaitForSeconds(0.2f);
			if(monkeyDieCount >= monkeyDieAnimation.GetLength(0))
				break;
		}
		renderer.material.mainTexture = monkeyDieAnimation[monkeyDieAnimation.GetLength(0)-1];
	}

	IEnumerator MonkeySlidingAnimation()
	{
		while(monkeyPresentState == 4)
		{
			print ("-----Sliding----"+monkeySlidingCount);
			renderer.material.mainTexture = monkeySlidingAnimation[monkeySlidingCount];
			monkeyDieCount = monkeyBananaThrowCount = monkeyJumpCount = monkeyRunCount = 0;
			monkeySlidingCount++;
			yield return new WaitForSeconds(0.2f);
			if(monkeySlidingCount >= (monkeySlidingAnimation.GetLength(0)-2))
				break;
		}
		monkeySlidingCount++;
		yield return new WaitForSeconds(0.4f);
		renderer.material.mainTexture = monkeySlidingAnimation[monkeySlidingAnimation.GetLength(0)-1];
		PlayerManager.Instance.ResetColliderAfterSliding ();
	}

	public int MonkeyPresentStateGetter()
	{
		return monkeyPresentState;
	}
}
