using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class csMain : MonoBehaviour {
	public GameObject gg;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadStage(){
		SceneManager.LoadScene ("StageScene");
	}

	public void  LoadStore(){

	}

	public void  LoadSetting(){

	}
}
