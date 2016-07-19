using UnityEngine;

using UnityEngine.SceneManagement;
public class csMain : MonoBehaviour {
	public GameObject gg;

    public AudioClip bgm;


	// Use this for initialization
	void Start () {
        csSoundManager.Instance().PlayBgm(bgm);
    }
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadStage() {
        csSoundManager.Instance().PlayButtonSound();
        SceneManager.LoadScene ("StageScene");
	}

	public void  LoadStore(){

	}

	public void  LoadSetting(){

	}
}
