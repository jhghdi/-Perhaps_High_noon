using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class csPlayer : MonoBehaviour {
	public GameObject image;
	List<GameObject> list;

	bool isHighNoon = false;

	enum PLATFORM { ANDROID=0 , EDITOR=1};
	PLATFORM platform;
	// Use this for initialization
	void Start () {
		if(Application.platform ==  RuntimePlatform.Android)
			platform = PLATFORM.ANDROID;
		else
			platform = PLATFORM.EDITOR;
		
	}

	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;

		if (platform == PLATFORM.ANDROID) {
			Touch touch = Input.touches [0]; 
			if (isHighNoon) {//석 양 맨
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				if (touch.phase == TouchPhase.Moved) {
					if (Physics.Raycast (ray, out hit)) {//터치 계산
						if (hit.transform.tag.Equals ("Aim")) {
							hit.transform.GetComponent<Renderer> ().material.color = Color.blue;
							hit.transform.tag = "AimLock";

						}
					}
				} else if (touch.phase == TouchPhase.Ended) {

					GameObject[] objs = GameObject.FindGameObjectsWithTag ("AimLock");

					foreach (GameObject g in objs) {
						GameObject.Destroy (g.transform.parent.gameObject);		
					}
				}
			} else {// 총 질 모 드
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				if (touch.phase ==TouchPhase.Began)// 총 질 모 드
				{
					if (Physics.Raycast (ray, out hit)) {//터치 계산
						if (hit.transform.tag.Equals ("Note")) {
							csNote note = hit.transform.GetComponentInParent<csNote> ();
							if (note != null)
								note.OnTrigger ();
						}
					}
				}
			}
		} 
		//PC 모드
		else if (platform == PLATFORM.EDITOR) {
			if (isHighNoon) {//석 양 맨
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Input.GetButton("Fire1")) {
					if (Physics.Raycast (ray, out hit)) {//터치 계산
						if (hit.transform.tag.Equals ("Aim")) {
							hit.transform.GetComponent<Renderer> ().material.color = Color.blue;
							hit.transform.tag = "AimLock";
						}
					}
				} else if (Input.GetButtonUp("Fire1")) {

					GameObject[] objs = GameObject.FindGameObjectsWithTag ("AimLock");

					foreach (GameObject g in objs) {
						GameObject.Destroy (g.transform.parent.gameObject);		
					}
				}
			} else {// 총 질 모 드
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Input.GetButtonDown("Fire1"))// 총 질 모 드
				{
					if (Physics.Raycast (ray, out hit)) {//터치 계산
						if (hit.transform.tag.Equals ("Note")) {
							csNote note = hit.transform.GetComponentInParent<csNote> ();
							if (note != null)
								note.OnTrigger ();
						}
					}
				}
			}
		}
	}


	public void OnHighNoon(){
		isHighNoon = !isHighNoon;
		image.SetActive(isHighNoon);
		if(isHighNoon)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject g in objs) {
			g.SendMessage ("OnChangeNote", isHighNoon);	
		}
	}

}
