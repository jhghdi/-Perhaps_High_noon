using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class csPlayer : MonoBehaviour {

    public GameObject image;
    public GameObject valueManager;

    Animator animator;

    public int life;

    Vector3 preAimPos;
    csValueManager valueMethod;
    public bool isHighNoon = false;


    public float strikeFrequency = 0.5f;
    float strikeTracker = 0.0f;

    public float smoothness = 0.5f;
    public float zigZagIntensity = 5.0f;
    public float zigZagPerMeter = 5.0f;

    public LineRenderer[] lineRenderers;
    private int line_iterator = 0;

    private List<Vector3> pathPoints;


    // Use this for initialization
    void Start () {
        pathPoints = new List<Vector3>();
        life = 3;
        valueMethod = valueManager.GetComponent<csValueManager>();

        animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update ()
	{
        //게임 중일때 리벤지 모드일 경우 게이지 감소
		if (Common.isRunning && isHighNoon)
			valueMethod.AddRevengeGuage(-10*Time.unscaledDeltaTime);

        //리벤지 모드일때 게이지가 0이면 종료
		if(valueMethod.GetRevengeGuage() == 0 && isHighNoon)
		{
			Revenge(Vector3.zero, Common.INPUT.INPUT_END);
			OnHighNoon();
		}

        if (pathPoints.Count > 1)
        {
            strikeTracker += Time.unscaledDeltaTime;
            if (strikeTracker >= strikeFrequency)
            {
                strikeTracker = 0.0f;
                Debug.Log("Light!");

                LightningStrike.Strike(path: pathPoints.ToArray(),
                lineObject: lineRenderers[line_iterator],
                zigZagIntensity: zigZagIntensity,
                zigZagPerMeter: zigZagPerMeter,
                smoothness: smoothness);

                lineRenderers[line_iterator].GetComponent<Animator>().Play("Fade", 0, 0.0f);

                line_iterator = (line_iterator + 1) % lineRenderers.Length;
            }
        }
    }
    
	public void OnHighNoon(){

		isHighNoon = !isHighNoon;
		image.SetActive(isHighNoon);

		if (isHighNoon) {
            Time.timeScale = 0.0f;
		}
		else{
            //끝날때 번갯길 초기화
            pathPoints.Clear();
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

            //드래그한 적 선 그리기
			if (hit.transform.tag.Equals("Enemy"))
			{
				hit.transform.tag = "AimLock";

                pathPoints.Add(hit.transform.position + Vector3.up*1.25f);
			}
		}
		else if(action == Common.INPUT.INPUT_END)
		{ 
			GameObject[] objs = GameObject.FindGameObjectsWithTag("AimLock");
            
            //조준한 적 갯수만큼 콤보 성공
            valueMethod.Combo(objs.Length);

            foreach (GameObject g in objs)
                g.SendMessage("OnHide");

            //리벤지 모드를 끝낸다.
            OnHighNoon();
        }
	}

	public void Shot(Vector3 position)
	{
        Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

		if (!Physics.Raycast(ray, out hit))
			return;

		if (hit.transform.tag.Equals("Enemy"))
		{
            hit.transform.SendMessage("OnTrigger");
            
            //콤보 1 성공
            valueMethod.Combo(1);
		}     
	}

    void OnMoveStart()
    {
        animator.SetInteger("state", 1);
    }

    void OnMoveEnd()
    {
        animator.SetInteger("state", 0);
    }
}
