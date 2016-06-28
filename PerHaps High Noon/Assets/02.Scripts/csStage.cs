using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class csStage : MonoBehaviour {
	int level = 1;
	int stage = 1;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnStage(int s){
		stage = s;
		SceneManager.LoadScene ("PlayScene");
	}

	public void OnLevel(int l){
		level = l;
	}

	public void OnBack(){
		SceneManager.LoadScene ("MainScene");
	}


}
