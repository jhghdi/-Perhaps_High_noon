using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class csStage : MonoBehaviour {
	public int level = 1;
	public int stage = 1;

	string[] phase = { "phase1_EZ" };
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnStage(int s){
		stage = s;
		SceneManager.LoadScene ("managerScene");
	}

	public void OnLevel(int l){
		level = l;
	}

	public void OnBack(){
		SceneManager.LoadScene ("MainScene");
	}

	public string[] getPhases(){
		//parse(level,stage)

		return phase;
	}
}
