using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class csDontDestroy : MonoBehaviour {
    public GameObject pauseCanvas;
    public GameObject resultCanvas;
    // Use this for initialization
    void Awake () {
		DontDestroyOnLoad (GameObject.Find("InputManager"));
		DontDestroyOnLoad (GameObject.Find("CamPathManager"));

        DontDestroyOnLoad (GameObject.Find("EnemyManager"));

        DontDestroyOnLoad (GameObject.Find("StageManager"));

        DontDestroyOnLoad (GameObject.Find("ValueManager"));

        DontDestroyOnLoad (pauseCanvas);

        DontDestroyOnLoad (resultCanvas);

        DontDestroyOnLoad (GameObject.Find("Canvas"));

        DontDestroyOnLoad (GameObject.Find("EventSystem"));

        DontDestroyOnLoad (this);

        DontDestroyOnLoad(GameObject.Find("Bolts"));

       // DontDestroyOnLoad(iTweenFade);
    }
}
