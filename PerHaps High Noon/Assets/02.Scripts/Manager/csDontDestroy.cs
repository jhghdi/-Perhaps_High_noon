using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class csDontDestroy : MonoBehaviour {
	public GameObject inputM;
	public GameObject cam_pathM;
	public GameObject enemyM;
	public GameObject stageM;
	public GameObject valueM;
	public GameObject pauseCanvas;
	public GameObject resultCanvas;
	public GameObject canvas;
	public GameObject eventSystem;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (inputM);
		DontDestroyOnLoad (cam_pathM);
		DontDestroyOnLoad (enemyM);
		DontDestroyOnLoad (stageM);
		DontDestroyOnLoad (valueM);
		DontDestroyOnLoad (pauseCanvas);
		DontDestroyOnLoad (resultCanvas);
		DontDestroyOnLoad (canvas);
		DontDestroyOnLoad (eventSystem);
		DontDestroyOnLoad (this);
	}
}
