using UnityEngine;
using System.Collections;
using System.Linq;
public class csCamPathManager : MonoBehaviour {
	public GameObject stageM;

	GameObject[] paths;
	public GameObject player;
    public GameObject playerContainer;

    Camera _Camera;

    public csFadeInOut Fade ;
    
    int step_Num;
    int camera_work_num = 0;
  
    void Update()
    {
        if (camera_work_num == 0)
            return;

        switch (camera_work_num)
        {
            //캐릭터 주위를 빙글빙글
            case 1:
                transform.LookAt(playerContainer.transform);
                transform.RotateAround(playerContainer.transform.position,Vector3.up, 100*Time.unscaledDeltaTime);
                break;
        }
    }

    /// <summary>
    ///스테이지 매니저가 신 로드 완료시 호출
    /// </summary>
    public void LoadInfo () {
        //페이드인
        Fade.StartPhase();

        //카메라 초기화
        _Camera = GetComponent<Camera>();

        //Map에 할당된 Step 오브젝트들을 찾는다.
        //Step 안엔 Spawn Point 존재.
		paths = GameObject.FindGameObjectsWithTag ("Step").OrderBy (g => g.transform.name).ToArray ();
        step_Num = 0;

        if (paths == null) {
			Debug.Log ("load failed");
			return;
		}

		Debug.Log ("load succes");
        //이전 씬에 있는 iTween Move 제거
        iTween i = playerContainer.GetComponent<iTween>();
        if(i != null)
            Destroy(i);

        //처음 시작지점으로 이동
        playerContainer.transform.position = paths [0].GetComponent<iTweenPath> ().nodes [0];
		Move ();
		return;
	}
    /// <summary>
    ///각 step별 경로를 따라 이동한다.
    ///<para>주인공에 대해 움직이도록 함.</para>
    ///(Enemy는 EnemyManager가 명령한다.)
    /// </summary>
    void Move()
    {   
        Hashtable hash = new Hashtable ();

		hash.Add ("path", 
			paths [step_Num].GetComponent<iTweenPath> ().nodes.ToArray());
		//hash.Add ("movetopath", true);
		hash.Add ("orienttopath", true);
		//hash.Add ("looktime", 1.0f);
		hash.Add ("time", 3.0f);
		//hash.Add ("speed", 3.0f);
		hash.Add ("looptype", iTween.LoopType.none);
		//hash.Add ("easetype", iTween.EaseType.easeInExpo);
		hash.Add ("easetype", iTween.EaseType.linear);

		hash.Add ("ignoretimescale", false);

		hash.Add ("oncomplete", "OnCameraEnded");
		hash.Add ("oncompletetarget", gameObject);


		iTween.MoveTo (playerContainer, hash);
        player.SendMessage("OnMoveStart");
        
        //다음 경로를 지정하는 Index
        step_Num++;
        //크기가 같으면 페이드 아웃
        if (step_Num == paths.Length)
            Fade.EndPhase();
	}
    
    /// <summary>
    /// 주인공이 이동 완료 시 호출된다.
    /// </summary>
    void OnCameraEnded()
    {
        //대기 상태로 전환하도록 한다.
        player.SendMessage("OnMoveEnd");
        //이동 완료 후 Stage Manager에게 Enemy Spawn를 시작하도록 함.
        stageM.SendMessage("OnCameraEnded");
    }

    /// <summary>
    /// csPlayer가 호출
    /// 카메라 워크 넘버 지정
    /// </summary>
    /// <param name="n">0 == no work</param>
    public void StartCameraWorkNum(int n)
    {
        _Camera.enabled = true;

        camera_work_num = n;
        switch (n)
        {
            case 1:
                //주인공 우측지점으로
                transform.position = playerContainer.transform.position +Vector3.up- playerContainer.transform.right * 3;
                break;
        }
    }

    /// <summary>
    /// 카메라 워크 종료
    /// </summary>
    public void EndCameraWork()
    {
        _Camera.enabled = false;
        camera_work_num = 0;
    }
}
