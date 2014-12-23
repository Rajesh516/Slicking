using UnityEngine;
using System.Collections;

public class IsTesting : MonoBehaviour {
	public static IsTesting instance;
	public bool isTesting;

	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
