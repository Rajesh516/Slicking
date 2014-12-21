using UnityEngine;
using System.Collections;

public class Resolutions : MonoBehaviour {
	public float CameraHeight = 9.0f;
	public static float PerPixel;
	static float Unit;

	void Awake() { 		// Calculate the total width of the playing game Device.
		PerPixel = Screen.width/(Screen.height/CameraHeight);
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static float GetWidth()   //  Return the perpixel Value.
	{
		return  PerPixel;
	}
	

}
