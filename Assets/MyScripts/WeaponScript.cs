using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * Time.deltaTime * 30);
		if (!renderer.isVisible)
						Destroy (gameObject);
	}
}
