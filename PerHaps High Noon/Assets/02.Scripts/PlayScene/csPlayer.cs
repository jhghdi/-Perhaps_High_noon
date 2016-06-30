using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class csPlayer : MonoBehaviour {

	public GameObject image;
	public GameObject aimLock;
	public GameObject aimArrow;

	Vector3 preAimPos;
	int lock_num =0;
	bool isHighNoon = false;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		//RaycastHit hit;
		////폰 터치 모드
		//if (platform == PLATFORM.ANDROID) {
		//	Touch touch = Input.touches [0]; 
		//	if (isHighNoon) {//석 양 맨
		//		Ray ray = Camera.main.ScreenPointToRay (touch.position);

		//		if (touch.phase == TouchPhase.Moved) {
		//			if (Physics.Raycast (ray, out hit)) {//터치 계산
				
		//				if (hit.transform.tag.Equals ("Aim")) {
		//					hit.transform.tag = "AimLock";
		//					GameObject lockObj = Instantiate (aimLock,hit.transform.position,Quaternion.identity) as GameObject;
		//					lockObj.transform.parent = hit.transform;
				
		//					//선 그 리 기
		//					if (lock_num != 0) {

		//						Vector3 pos = (preAimPos + hit.transform.position) * 0.5f;

		//						Quaternion rot = Quaternion.FromToRotation (Vector3.right, hit.transform.position - preAimPos);

		//						GameObject arrow = Instantiate (aimArrow,pos,rot) as GameObject;

		//						arrow.transform.parent = hit.transform;
		//						arrow.transform.localScale *= Vector3.Distance (hit.transform.position, preAimPos);

		//					}
		//					preAimPos = hit.transform.position;
		//					lock_num++;
		//				}
		//			}
		//		} else if (touch.phase == TouchPhase.Ended) {

		//			GameObject[] objs = GameObject.FindGameObjectsWithTag ("AimLock");

		//			foreach (GameObject g in objs) {
		//				GameObject.Destroy (g.transform.parent.gameObject);		
		//			}
		//		}
		//	} else {// 총 질 모 드
		//		Ray ray = Camera.main.ScreenPointToRay (touch.position);
		//		if (touch.phase ==TouchPhase.Began)// 총 질 모 드
		//		{
		//			if (Physics.Raycast (ray, out hit)) {//터치 계산
		//				if (hit.transform.tag.Equals ("Note")) {
		//					csNote note = hit.transform.GetComponentInParent<csNote> ();
		//					if (note != null)
		//						note.OnTrigger ();
		//				}
		//			}
		//		}
		//	}
		//} 
		////PC 모드
		//else if (platform == PLATFORM.EDITOR) {
		//	if (isHighNoon) {//석 양 맨
		//		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//		if (Input.GetButton("Fire1")) {
		//			if (Physics.Raycast (ray, out hit)) {//터치 계산
		//				if (hit.transform.tag.Equals ("Aim")) {
		//					hit.transform.tag = "AimLock";
		//					GameObject lockObj = Instantiate (aimLock,hit.transform.position,Quaternion.identity) as GameObject;
		//					lockObj.transform.parent = hit.transform;
		//					//선 그 리 기
		//					if (lock_num != 0) {
								
		//						Vector3 pos = (preAimPos + hit.transform.position) * 0.5f;

		//						Quaternion rot = Quaternion.FromToRotation (Vector3.right, hit.transform.position - preAimPos);

		//						GameObject arrow = Instantiate (aimArrow,pos,rot) as GameObject;

		//						arrow.transform.parent = hit.transform;
		//						arrow.transform.localScale *= Vector3.Distance (hit.transform.position, preAimPos);
							
		//					}
		//					preAimPos = hit.transform.position;
		//					lock_num++;
		//				}
		//			}
		//		} else if (Input.GetButtonUp("Fire1")) {

					//GameObject[] objs = GameObject.FindGameObjectsWithTag ("AimLock");

     //   foreach (GameObject g in objs)
     //   {
     //       GameObject.Destroy(g.transform.parent.gameObject);
     //   }
        //		}
        //	} else {// 총 질 모 드
        //		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        //		if (Input.GetButtonDown("Fire1"))// 총 질 모 드
        //		{
        //			if (Physics.Raycast (ray, out hit)) {//터치 계산
        //				if (hit.transform.tag.Equals ("Note")) {
        //					csNote note = hit.transform.GetComponentInParent<csNote> ();
        //					if (note != null)
        //						note.OnTrigger ();
        //				}
        //			}
        //		}
        //	}
        //}
    }

    /// <summary>
    /// Revenge모드를 활성화/비활성화 한다.
    /// </summary>
	public void OnHighNoon(){
		isHighNoon = !isHighNoon;
		image.SetActive(isHighNoon);

		if (isHighNoon) {
			Time.timeScale = 0.0f;
			lock_num = 0;
		}
		else{
			Time.timeScale = 1.0f;
		}
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in objs) {
            enemy.SendMessage ("OnChangeNote", isHighNoon);	
		}
	}

    /// <summary>
    /// Player의 행동을 지정한다.
    /// </summary>
    /// <param name="position">액션을 수행할 위치</param>
    /// <param name="action">입력받은 액션</param>
    public void DoAction(Vector3 position, Common.INPUT action)
    {
        if (isHighNoon && action != Common.INPUT.INPUT_BEGIN)
            Revenge(position, action);
        else if (!isHighNoon && action == Common.INPUT.INPUT_BEGIN)
            Shot(position);
    }

    /// <summary>
    /// Revenge 모드를 수행한다
    /// </summary>
    /// <param name="position">Revenge모드 액션이 실행될 위치</param>
    /// <param name="action">입력받은 액션</param>
    public void Revenge(Vector3 position, Common.INPUT action)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if(action == Common.INPUT.INPUT_MOVE)
        {
            if (!Physics.Raycast(ray, out hit))
                return;

            if (hit.transform.tag.Equals("Aim"))
            {
                hit.transform.tag = "AimLock";
                GameObject lockObj = Instantiate(aimLock, hit.transform.position, Quaternion.identity) as GameObject;
                lockObj.transform.parent = hit.transform;

                //선 그 리 기
                if (lock_num != 0)
                {
                    Vector3 pos = (preAimPos + hit.transform.position) * 0.5f;
                    Quaternion rot = Quaternion.FromToRotation(Vector3.right, hit.transform.position - preAimPos);
                    GameObject arrow = Instantiate(aimArrow, pos, rot) as GameObject;
                    arrow.transform.parent = hit.transform;
                    arrow.transform.localScale *= Vector3.Distance(hit.transform.position, preAimPos);
                }
                preAimPos = hit.transform.position;
                lock_num++;
            }
        }
        else if(action == Common.INPUT.INPUT_END)
        { 
                GameObject[] objs = GameObject.FindGameObjectsWithTag("AimLock");
                foreach (GameObject g in objs)
                    GameObject.Destroy(g.transform.parent.gameObject);
        }
    }

    /// <summary>
    /// shot모드(총 발사)를 수행한다.
    /// </summary>
    /// <param name="position">shot모드 액션이 실행될 위치</param>
    public void Shot(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.transform.tag.Equals("Note"))
        {
            csNote note = hit.transform.GetComponentInParent<csNote>();
             if (note != null)
                 note.OnTrigger();
        }     
    }
}
