using UnityEngine;
using System.Collections;

public class LeaderBoardManager : MonoBehaviour {

	public float Gap = -1.7f;
	public LeaderBoardBtnProperties[] Blocks;
	public GameObject blockPrefab;
	public Color color1;
	public Color color2;
	public static LeaderBoardManager Instance;
	public tk2dTextMesh MsgText;
	public Vector2 IntialPos;
	float gap;
	// Use this for initialization

	void Awake() {
		if (Instance == null || Instance != null)
			Instance = this;
	}

	void Start () {
	}

	void OnEnable() {
		MsgText.text = "Connecting...";
		FacebookManager.Instance.Login ();
	}

	public void MakeLeaderBoard() {
		Debug.Log ("asd");
		DeleteAlreadyScore ();
		MsgText.text = "";
		Blocks = new LeaderBoardBtnProperties[FBIntegrate.Instance.GetListCount()];
		gap = 0;
		for (int i=0; i<FBIntegrate.Instance.GetListCount(); i++) {
			GameObject Go =  (GameObject) Instantiate (blockPrefab);
			Go.transform.parent = this.gameObject.transform;
			//Go.transform.Translate (new Vector3(0,Gap,0));
			Blocks[i] = Go.GetComponent<LeaderBoardBtnProperties>();
			float y = IntialPos.y - gap;
			Blocks[i].transform.localPosition = new Vector3(IntialPos.x,y,0f);
			Blocks[i].SetData (i);
			gap = Gap +  gap;
		}

	}

	void DeleteAlreadyScore(){
		if (Blocks.Length > 0) {
			for(int i=0;i<Blocks.Length;i++)
				Destroy (Blocks[i].gameObject);
		}
	}

	public void OnLoginFailed() {
		DeleteAlreadyScore ();
		LeaderBoardManager.Instance.MsgText.text = "Login Failed \n Try again";
	}
	

}
