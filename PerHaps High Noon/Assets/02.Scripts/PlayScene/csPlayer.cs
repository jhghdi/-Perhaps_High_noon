using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class csPlayer : MonoBehaviour {

    public GameObject image;
    public GameObject aimLock;
    public GameObject aimArrow;
    public GameObject valueManager;
    public int life;

    Vector3 preAimPos;
    csValueManager valueMethod;
    int lock_num = 0;
    public bool isHighNoon = false;
  
	// Use this for initialization
	void Start () {
        life = 3;
        valueMethod = valueManager.GetComponent<csValueManager>();
    }

	// Update is called once per frame
	void Update ()
	{
		if (Common.isRunning && isHighNoon)
			valueMethod.SetRevengeGuage(-10*Time.unscaledDeltaTime);

		if(valueMethod.GetRevengeGuage() == 0 && isHighNoon)
		{
			Revenge(Vector3.zero, Common.INPUT.INPUT_END);
			OnHighNoon();
		}
	}

    
	public void OnHighNoon(){

		isHighNoon = !isHighNoon;
		image.SetActive(isHighNoon);

		if (isHighNoon) {
			Time.timeScale = 0.01f;
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

	public void DoAction(Vector3 position, Common.INPUT action)
	{
		if (isHighNoon && action != Common.INPUT.INPUT_BEGIN)
			Revenge(position, action);
		else if (!isHighNoon && action == Common.INPUT.INPUT_BEGIN)
			Shot(position);
	}

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
