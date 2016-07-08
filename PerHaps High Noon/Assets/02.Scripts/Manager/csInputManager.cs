using UnityEngine;
using UnityEngine.SceneManagement;
public class csInputManager : MonoBehaviour {
	public GameObject pauseCanvas;

	/// <summary>
	/// Player 객체
	/// </summary>
	public GameObject player;
    iTween playerTween;
    /// <summary>
    /// Player의 Method 
    /// </summary>
    csPlayer playerMethod;

	// Use this for initialization
	void Start () {
		playerMethod = player.GetComponent<csPlayer>();
	}

	/// <summary>
	/// touch의 행동을 체크한다.
	/// </summary>
	void Update() {
		if (Input.touches.Length > 0) {
			Touch touch = Input.touches [0];
			Vector3 position = touch.position;
			Common.INPUT inputStatus = Common.INPUT.INPUT_NULL;

            if (touch.phase == TouchPhase.Began)
            {
                inputStatus = Common.INPUT.INPUT_BEGIN;
            }
            else if (touch.phase == TouchPhase.Moved)
                inputStatus = Common.INPUT.INPUT_MOVE;
            else if (touch.phase == TouchPhase.Ended)
                inputStatus = Common.INPUT.INPUT_END;


            if(inputStatus != Common.INPUT.INPUT_NULL)
                playerMethod.DoAction(position, inputStatus);

        }
	}

	public void OnPause(){
		pauseCanvas.SetActive (true);
        Common.isRunning = false;
        Time.timeScale = 0;
        playerTween = player.GetComponent<iTween>();
        if(playerTween !=null)
            playerTween.isRunning = false;
	}

	public void OnCancel(){
		pauseCanvas.SetActive (false);
        Common.isRunning = true;
        if (!playerMethod.isHighNoon)
            Time.timeScale = 1.0f;

        if (playerTween != null)
            playerTween.isRunning = true;
    }

	public void OnRetire(){
        Common.isRunning = true;
        Time.timeScale = 1.0f;

        GameObject[] g = GameObject.FindObjectsOfType<GameObject> ();
		foreach(GameObject gg in g){
			Destroy (gg);
		}
       
		SceneManager.LoadScene ("StageScene");
	}
}