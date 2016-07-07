using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class csGameManager : MonoBehaviour {
	public string level = "EZ";
	public int stage = 0;

	string[][] phase = {new string[]{ "Phase1_","Phase2_" },
		new string[]{ "Phase2_","Phase3_","Phase3_" },
		new string[]{ "Phase1_","Phase2_","Phase3_" }
	};
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

	public void OnLevel(string l){
		level = l;
	}

	public void OnBack(){
		SceneManager.LoadScene ("MainScene");
	}

	public string[] getPhases(){
		for (int i = 0; i < phase [stage].Length; i++) {
			phase [stage] [i] = string.Format ("{0}{1}", phase [stage] [i], level);
		}

		return phase[stage];
	}
}
