using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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


    bool isUITouched = false;

    EventSystem eventSystem;
    // Use this for initialization
    void Start () {
		playerMethod = player.GetComponent<csPlayer>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	/// <summary>
	/// touch의 행동을 체크한다.
	/// </summary>
	void Update() {
        if ( Common.isRunning) {
            Vector3 position;
            Common.INPUT inputStatus;
           
            if (Application.platform == RuntimePlatform.Android && Input.touches.Length > 0) {
                Touch touch = Input.touches[0];
                if (eventSystem.IsPointerOverGameObject(touch.fingerId))
                {
                    // ui touched
                    isUITouched = true;
                }
                position = touch.position;
                inputStatus = Common.INPUT.INPUT_NULL;


                if (touch.phase == TouchPhase.Began)
                {
                    inputStatus = Common.INPUT.INPUT_BEGIN;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    inputStatus = Common.INPUT.INPUT_MOVE;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    inputStatus = Common.INPUT.INPUT_END;
                }

                
            }
            else
            {
                if (eventSystem.IsPointerOverGameObject())
                {
                    // ui touched
                    isUITouched = true;
                }

                position = Input.mousePosition;
                if(Input.GetButtonDown("Fire1"))
                    inputStatus = Common.INPUT.INPUT_BEGIN;
                else if(Input.GetButton("Fire1"))
                    inputStatus = Common.INPUT.INPUT_MOVE;
                else if(Input.GetButtonUp("Fire1"))
                    inputStatus = Common.INPUT.INPUT_END;
                else
                    inputStatus = Common.INPUT.INPUT_NULL;
                
            }
            //UI 터치 분리
            if (inputStatus != Common.INPUT.INPUT_NULL
            && !isUITouched)
            {
                playerMethod.DoAction(position, inputStatus);
            }
            else if (inputStatus == Common.INPUT.INPUT_END)
            {
                isUITouched = false;
            }

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