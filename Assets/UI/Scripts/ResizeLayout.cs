using UnityEngine;
using System.Collections;

public class ResizeLayout : MonoBehaviour {
	public bool MoreSize;
	// Use this for initialization
	void Start () 
	{
		Vector3 Bmin = GetComponent<tk2dUILayout> ().GetMinBounds ();
		Vector3 Bmax = GetComponent<tk2dUILayout> ().GetMaxBounds ();
		float Sw = Resolutions.GetWidth ();
		Vector3 Pmin;
		Vector3 Pmax;
		if (MoreSize) 
		{
			Pmin = new Vector3 (-(Sw / 2 + Sw), Bmin.y, Bmin.z);
			Pmax = new Vector3 ((Sw / 2 + Sw), Bmax.y, Bmax.z);
		}
		else 
		{
			Pmin = new Vector3 (-(Sw/2), Bmin.y, Bmin.z);
			Pmax = new Vector3 ((Sw / 2), Bmax.y, Bmax.z);
		}

		GetComponent<tk2dUILayout> ().SetBounds (Pmin, Pmax);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

	}
}
