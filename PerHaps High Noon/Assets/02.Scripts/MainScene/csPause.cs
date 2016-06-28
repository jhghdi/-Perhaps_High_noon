using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class csPause : MonoBehaviour {
	public GameObject pauseCanvas;


	public void OnPause(){
		pauseCanvas.SetActive (true);
		Time.timeScale = 0;
	}

	public void OnCancel(){
		pauseCanvas.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void OnRetire(){
		SceneManager.LoadScene ("StageScene");
	}
}
