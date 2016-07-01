using UnityEngine;

public class csInputManager : MonoBehaviour {

	/// <summary>
	/// Platform 정의
	/// </summary>
	enum PLATFORM { ANDROID = 0, EDITOR = 1 };

	/// <summary>
	/// Player 객체
	/// </summary>
	public GameObject player;

	/// <summary>
	/// Player의 Method 
	/// </summary>
	csPlayer playerMethod;

	/// <summary>
	/// 현재 platform
	/// </summary>
	PLATFORM platform;

	// Use this for initialization
	void Start () {
		playerMethod = player.GetComponent<csPlayer>();

		if (Application.platform == RuntimePlatform.Android)
			platform = PLATFORM.ANDROID;
		else
			platform = PLATFORM.EDITOR;
	}

	// Update is called once per frame
	void Update() {

		// 플랫폼에 따른 인식 확인
		if (platform == PLATFORM.ANDROID && Input.touches != null)
			DoTouch();

		else if (platform == PLATFORM.EDITOR)
			DoClick();
	}

	/// <summary>
	/// touch의 행동을 체크한다.
	/// </summary>
	private void DoTouch()
	{
		Touch touch = Input.touches[0];
		Vector3 position = touch.position;
		Common.INPUT inputStatus = Common.INPUT.INPUT_BEGIN;

		if (touch.phase == TouchPhase.Began)
			inputStatus = Common.INPUT.INPUT_BEGIN;
		else if (touch.phase == TouchPhase.Moved)
			inputStatus = Common.INPUT.INPUT_MOVE;
		else if (touch.phase == TouchPhase.Ended)
			inputStatus = Common.INPUT.INPUT_END;

		playerMethod.DoAction(position, inputStatus);
	}

	/// <summary>
	/// mouse click이나 drag를 체크한다.
	/// </summary>
	private void DoClick()
	{
		Vector3 position = Input.mousePosition;

		if (Input.GetButtonDown("Fire1"))
			playerMethod.DoAction(position, Common.INPUT.INPUT_BEGIN);
		else if (Input.GetButton("Fire1"))
			playerMethod.DoAction(position, Common.INPUT.INPUT_MOVE);
		else if (Input.GetButtonUp("Fire1"))
			playerMethod.DoAction(position, Common.INPUT.INPUT_END);
		else
			return;
	}

}

