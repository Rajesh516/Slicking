using UnityEngine;
using System.Collections;

public class LeaderBoardBtnProperties : MonoBehaviour {
	public static int SerialNo = 0;
	public GameObject ProfilePicture;
	public SpriteRenderer BackGround;
	public TextMesh Serial;
	public TextMesh Name;
	public TextMesh Score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetData(int i){
		Serial.text = (i+1).ToString ();
		Name.text = FBIntegrate.Instance.ScoresNames[i].ToString ();
		if (Name.text.Length > 13) {
			Name.text = Name.text.Substring (0, 12) +"..";	
		}
		Score.text = FBIntegrate.Instance.ScoresScore[i].ToString ();
		if(FBIntegrate.Instance.ProfilePics [i] != null)
			ProfilePicture.renderer.material.mainTexture = FBIntegrate.Instance.ProfilePics [i];
		if (i % 2 == 0) 
			BackGround.color = LeaderBoardManager.Instance.color1;
		else 
			BackGround.color = LeaderBoardManager.Instance.color2;

	}
}
