using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class csPlayer : MonoBehaviour {
    //카메라 이펙트.
    public UnityStandardAssets.ImageEffects.ScreenOverlay overImage;

    /// <summary>
    ///리벤지 게이지, 체력 관리 
    /// </summary>
    public GameObject valueManager;

    public csCamPathManager _CamPathManager;
    //주인공의 애니메이터
    Animator animator;

    //주인공 Life
    public int life = 3;

    //ValueManager의 함수 호출용
    csValueManager valueMethod;

    //리벤지 모드 On/Off 변수
    public bool isHighNoon = false;

    //리벤지 모드 시 이어진 Aim 사이의 번개 효과 주기
    public float strikeFrequency = 0.25f;
    float strikeTracker = 10.0f;

    public LineRenderer[] lineRenderers;
    private int line_iterator = 0;



    /// <summary>
    /// 주인공 이동중인시 확인용
    /// </summary>
    bool isMoving = false;

    /// <summary>
    ///번개 효과 경로 저장용.
    ///이 사이즈가 2 이상이면 번개 효과 그림
    /// </summary>
    private List<Vector3> pathPoints;

    //사격 레이저 이펙트
    public GameObject Shot1;
    public GameObject Wave;

    //사격 위치
    public Transform gunPos;
    // Use this for initialization
    void Start () {
        pathPoints = new List<Vector3>();
        
        valueMethod = valueManager.GetComponent<csValueManager>();

        animator = GetComponent<Animator>();
    }

	void Update ()
	{
        //게임 중일때 리벤지 모드일 경우 게이지 감소
		if (Common.isRunning && isHighNoon)
			valueMethod.AddRevengeGuage(-10*Time.unscaledDeltaTime);

        //리벤지 모드일때 게이지가 0이면 강제종료(공격X).
        //강제 종료이므로 AimLock된 Enemy들의 태그도 원래대로 되돌린다.
		if(valueMethod.GetRevengeGuage() == 0 && isHighNoon)
		{
            GameObject[] objs = GameObject.FindGameObjectsWithTag("AimLock");
            foreach (GameObject enemy in objs)
                enemy.tag = "Enemy";
                  
            OnHighNoon();
		}

        //번개 효과 그리기
        if (pathPoints.Count > 1)
        {
            strikeTracker += Time.unscaledDeltaTime;
            if (strikeTracker >= strikeFrequency)
            {
                strikeTracker = 0.0f;

                LightningStrike.Strike(path: pathPoints.ToArray(),
                lineObject: lineRenderers[line_iterator],
                zigZagIntensity: 1,
                zigZagPerMeter: 1,
                smoothness: 0.5f);

                //반복자를 이용해서 일정 갯수(2개)를 돌려 쓴다
                lineRenderers[line_iterator].GetComponent<Animator>().Play("Fade", 0, 0.0f);

                line_iterator = (line_iterator + 1) ;
                if (line_iterator == lineRenderers.Length)
                    line_iterator = 0;
            }
        }
    }
    
    /// <summary>
    /// UI의 리벤지 모드를 눌렀을 때 호출.
    /// 또는 리벤지 모드를 On/Off 전환할 때 호출함.
    /// </summary>
	public void OnHighNoon(){
        //달리는 중이면 사용X
        //모든 적을 처리하면 isMoving = true가 되므로 상태를 초기화 시켜준다.
        if (isMoving)
        {
            isHighNoon = false;
            //카메라의 스크린 오버레이 스크립트 On/Off
            overImage.enabled = isHighNoon;
            overImage.SetHighnoon(isHighNoon);

            //시간을 원래대로 되돌림
            Time.timeScale = 1.0f;
            return;
        }

		isHighNoon = !isHighNoon;
        //카메라의 스크린 오버레이 스크립트 On/Off
        overImage.enabled = isHighNoon;
        overImage.SetHighnoon(isHighNoon);

		if (isHighNoon) {
            //시간이 멈춘다.
            //주인공 애니메이션은 재생된다.
            Time.timeScale = 0.0f;
            //리벤지 대기 모드 애니메이션 전환
            animator.SetInteger("state", 10);
		}
		else{
            //일반 대기모드
            animator.SetInteger("state", 0);
            //끝날때 번갯길 초기화
            pathPoints.Clear();
            Time.timeScale = 1.0f;
		}

        //공격중인 Enemy는 원래의 Aim으로 바꿔준다.
        //리벤지 모드로 조준한 Enemy는 태그가 AimLock 임.
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in objs) {
			enemy.SendMessage ("OnChangeNote", isHighNoon);	
		}
	}

    /// <summary>
    /// Input Manager가 호출한다.
    /// </summary>
    /// <param name="position">스크린을 터치한 위치</param>
    /// <param name="action">터치한 상태(Begin,Move, End,Null)</param>
	public void DoAction(Vector3 position, Common.INPUT action)
	{
		if (isHighNoon && action != Common.INPUT.INPUT_BEGIN)
			Revenge(position, action);
		else if (!isHighNoon && action == Common.INPUT.INPUT_BEGIN)
			Shot(position);
	}
    /// <summary>
    /// 리벤지 모드일대는 드래그로 Enemy를 조준한다.
    /// </summary>
    /// <param name="position">드래그한 위치</param>
    /// <param name="action">터치 상태(Move, End)</param>
	public void Revenge(Vector3 position, Common.INPUT action)
	{
        //드래그한 위치에 Ray를 쏴서 맞은 Enemy를 확인.
        //적이 겹치면 문제 발생!
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

        //드래그 할때는 Enemy를 조준함
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
        //땠을 때는 조준한 Enemy를 죽임
		else if(action == Common.INPUT.INPUT_END)
		{
            StartCoroutine(FinalShot());
        }
	}

    IEnumerator FinalShot()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("AimLock");
        
        if(obj.Length > 0)
        {
            Common.isRunning = false;
            _CamPathManager.StartCameraWorkNum(1);
        }
        for(int i=0;i<obj.Length;i++)
        {
            Laser_Shot(obj[i].transform);

            //사격할 방향으로 회전
            transform.LookAt(obj[i].transform.position);

            //태그 미초기화시 잘린 놈이 또 잘림
            obj[i].tag = "Enemy";

            //조준한 Enemy들의 Aim을 가린다.
            //초기화는 아님.
            obj[i].SendMessage("SetAimVisible", false);

            //메시 커터를 이용
            //기존의 적은 Enemy Manager에서 재활용
            GameObject tempObj = Instantiate(obj[i]);

            //레그돌을 쓰기 위해 Animator 끄기
            tempObj.GetComponent<Animator>().enabled = false;

            //절단용 평면 생성 + 자동 제거
            GameObject goCutPlane = new GameObject("CutPlane");

            Transform tempTransform = goCutPlane.transform;
            tempTransform.position = tempObj.transform.position;
            tempTransform.position += Vector3.up;
            tempTransform.Rotate(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)));

            tempObj.GetComponent<Splitable>().Split(tempTransform);

            Destroy(goCutPlane, 1);

            float pauseEndTime = Time.realtimeSinceStartup + 0.25f;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
        }
        Common.isRunning = true;
        OnHighNoon();
        _CamPathManager.EndCameraWork();
    }
    /// <summary>
    /// 일반 모드시 적을 쏜다
    /// </summary>
    /// <param name="position">터치한 위치</param>
	public void Shot(Vector3 position)
	{
        Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
			return;

		if (hit.transform.tag.Equals("Enemy"))
		{
            Laser_Shot(hit.transform);
		}     
	}

    void Laser_Shot(Transform _transform)
    {
        //사격 애니메이션
        //사격중이더라도 처음부터 시작
        animator.Play("Fire", 0, 0);

        //사격할 방향으로 회전
        transform.LookAt(_transform.position);
        //Fire
        GameObject s1 = (GameObject)Instantiate(Shot1, gunPos.position, gunPos.rotation);
        s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());

        GameObject wav = (GameObject)Instantiate(Wave, gunPos.position, gunPos.rotation);
        wav.transform.localScale *= 0.25f;
        wav.transform.Rotate(Vector3.left, 90.0f);
        wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

        //OnTrigger로 Aim 판정 및 리벤지 게이지 반영
        _transform.SendMessage("OnTrigger");

        //콤보 1 성공
        valueMethod.Combo(1);
    }

    /// <summary>
    /// CamPathManager가 호출.
    /// 이동 애니메이션 재생.
    /// </summary>
    void OnMoveStart()
    {
        transform.localRotation= Quaternion.Euler(Vector3.zero);
        isMoving = true;
        animator.SetInteger("state", 1);
    }

    /// <summary>
    /// CamPathManager가 주인공 이동 완료시 호출.
    /// Fire 애니메이션 종료시에도 호출.
    /// 대기 모드로 전환
    /// </summary>
    void OnMoveEnd()
    {
        isMoving = false;
        animator.SetInteger("state", 0);
    }
}
