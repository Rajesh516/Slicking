using UnityEngine;
using System.Collections;

public class CoinsScript : MonoBehaviour {
	Rigidbody rigidBodyCoins;
	public Texture2D[] coinTextures = new Texture2D[8];
	int coinTextureNumber;
	bool isCoroutine;
	bool startLerping;
	Vector3 myLocalLocation;
	// Use this for initialization
	void Start () {
		myLocalLocation = transform.position;
		startLerping = false;
		coinTextureNumber = 0;
		renderer.material.mainTexture = coinTextures [4];
		rigidBodyCoins = GetComponent<Rigidbody> ();
		if (rigidBodyCoins == null)
						rigidBodyCoins = gameObject.AddComponent<Rigidbody> ();
		rigidBodyCoins.isKinematic = true;
		rigidBodyCoins.useGravity = false;
		GetComponent<BoxCollider> ().isTrigger = true;
	}

	void OnEnable()
	{
		if(myLocalLocation != Vector3.zero)
			transform.position = myLocalLocation;
		startLerping = false;
		isCoroutine = true;
		coinTextureNumber = 0;
		//StartCoroutine (CoinRotation ());
	}

	void OnDisable()
	{
		isCoroutine = false;
		StopCoroutine ("CoinRotation");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startLerping) 
		{
			transform.position = Vector3.Lerp(transform.position,PlayerManager.Instance.MyTransform(),Time.deltaTime * 6);	
		}
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.name.Equals("CoinMagnet")){
			startLerping = true;
		}
		if (colli.gameObject.tag.Equals ("Player")) {
			PlayerManager.Instance.CoinCollected(gameObject.collider);	
		}
	}

	IEnumerator CoinRotation()
	{
		while (isCoroutine) {
			renderer.material.mainTexture = coinTextures [coinTextureNumber];
			coinTextureNumber++;
			yield return new WaitForSeconds(0.05f);
			if(coinTextureNumber == coinTextures.GetLength(0))
				coinTextureNumber = 0;
		}
	}
}
