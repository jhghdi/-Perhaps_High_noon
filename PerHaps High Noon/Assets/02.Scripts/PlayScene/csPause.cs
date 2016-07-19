using UnityEngine;
using UnityEngine.SceneManagement;

public class csPause : MonoBehaviour {
	public GameObject pauseCanvas;


	public void OnPause(){
        csSoundManager.Instance().PlayButtonSound();
		pauseCanvas.SetActive (true);
		Time.timeScale = 0;
	}

	public void OnCancel(){
        csSoundManager.Instance().PlayButtonSound();
        pauseCanvas.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void OnRetire(){
        csSoundManager.Instance().PlayButtonSound();
        SceneManager.LoadScene ("StageScene");
	}
}
