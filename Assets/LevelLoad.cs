using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {

	public int levelToLoad;
	public Texture levelPicture;
	public Texture introTextPicture;
	public string aboveText;
	public Transform text;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.mainTexture = levelPicture;
		Transform myText = Instantiate (text, Vector3.zero, Quaternion.identity) as Transform;
		myText.parent = this.transform;
		myText.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
		myText.localPosition = new Vector3 (0.0f, 0.0f, -6.81f);
		myText.Rotate (new Vector3 (180f, 180f, 0f));
		myText.GetComponent<TextMesh> ().text = aboveText;
	}


	public Texture getTexture(){
		return introTextPicture;
	}

	public int getLevel(){
		return levelToLoad;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
