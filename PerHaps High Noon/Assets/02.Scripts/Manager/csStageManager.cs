using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Xml;
using System;


public class csStageManager : MonoBehaviour {
	public GameObject EnemyManager;
	public GameObject CamPathManager;
	Phase myPhase = null;
	Step myStep =null;
	
    string[] phases;
    int phaseNum = 0;

	// Use this for initialization
	IEnumerator Start () {

        csStage cs =  GameObject.Find ("GameManager").GetComponent<csStage> ();
        phases = cs.getPhases();
        SceneManager.LoadScene (phases[phaseNum]);


        myPhase = ParseXML();
		myStep = myPhase.getStep ();

		yield return new WaitForSeconds (1);
		//처음 적이 0명이면 카메라 워크만 실행
		StartSpawn ();

		CamPathManager.SendMessage ("LoadInfo");


	}

	void StartSpawn(){
		Debug.Log ("Start Spawn");
		//이 수만큼 적이 죽으면 OnStepEnded 호출
		EnemyManager.SendMessage ("setEnemyCount", myStep.getSpawnCount ());

		while(!myStep.isSpawnEnded ()){//소환 시간이 정적이므로 한번에 step내의 정보를 읽어서 coroutine으로 시작한다.
			Debug.Log("spawned");
			StartCoroutine (spawnEnemy ());
		}
	}


	//EnemyManager가 호출
	//카메라 매니저로
	void OnStepEnded(){
		Debug.Log("OnStepEnded");
	
		//카메라 워크 시작.
		CamPathManager.SendMessage("Move");


	}

	//카메라 매니저가 호출
	void OnCameraEnded(){//step이 끝났다는 뜻
		if (myPhase.isEnded ()) {
			//다음 phase 로딩
			Debug.Log ("phase end");

			if (phaseNum > 4) {
				//결과 화면
				Debug.Log ("result");
				return;
			} else {
				//다음  phase 로딩

			}

		} else {
			myStep = myPhase.getStep ();
			StartSpawn ();
		}
	}

	Phase ParseXML(){
		Debug.Log("parseXML");
		Phase phase = new Phase ();

        // xml 파일 형식 정해지면 추가 구현

        // xml 지정
        string strPath = string.Empty;

        // platform별로 다르게 한다
        #if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
                strPath += ("file:///");
                strPath += (Application.dataPath + "/" + string.Format("xml/{0}.xml", phases[phaseNum]) );
        #elif UNITY_ANDROID
                strPath = "jar:file://" + Application.streamingAssetsPath + "!/assets/"+string.Format("xml/{0}.xml", phases[phaseNum]);
        #endif

        //phase 정보 읽기
        XmlDocument document = new XmlDocument();
       document.Load(strPath);

        // phase에 step insert
        int itemCount = document.LastChild.ChildNodes.Count;
        string name = string.Empty;

        for (int i = 0; i < document.LastChild.ChildNodes.Count; ++i)
        {
            if (name != document.LastChild.ChildNodes[i].Name)
            {
                phase.AddStep(new Step());
                name = document.LastChild.ChildNodes[i].Name;
            }
        }

        // step에 spawn insert
        int index = -1;
        for (int i = 0; i < document.LastChild.ChildNodes.Count; ++i)
        {
            //step load
            XmlNode step = document.LastChild.ChildNodes[i];

            bool isStartOfStep = Int32.Parse(step.ChildNodes[0].InnerText) == 1;

            if (isStartOfStep)
                index++;

            SpawnInfo spawn = new SpawnInfo();
            spawn.spawnPos = Int32.Parse(step.ChildNodes[1].InnerText);
            spawn.destinationPos = Int32.Parse(step.ChildNodes[2].InnerText);
            spawn.spawnCoolTime = float.Parse(step.ChildNodes[3].InnerText);
            spawn.SetMoveType(Int32.Parse(step.ChildNodes[4].InnerText));
            spawn.aimTime = float.Parse(step.ChildNodes[5].InnerText);
            spawn.SetItemType(Int32.Parse(step.ChildNodes[6].InnerText));

            //step add
            phase.stepList[index].AddInfo(spawn);
        }

        //phase add

        return phase;
	}


    IEnumerator spawnEnemy(){
        SpawnInfo mySpawnInfo = myStep.getSpawnInfo ();
		yield return new WaitForSeconds( mySpawnInfo.spawnCoolTime);
		EnemyManager.SendMessage ("Spawn",mySpawnInfo);
	}
}
//소환 정보 저장용 클래스
public class SpawnInfo{
	
	public enum TYPE{normal=0,slow,fast};
	public enum ITEM {none=0,fever,heal};

	public TYPE typeNum;
	public ITEM itemNum;
	public float spawnCoolTime;
	public int spawnPos;
    public int destinationPos;
    public float aimTime;
    public float activeTime;

    public void SetMoveType(int index)
    {
        switch (index)
        {
            case 0:
                typeNum = TYPE.normal;
                break;
            case 1:
                typeNum = TYPE.slow;
                break;
            case 2:
                typeNum = TYPE.fast;
                break;
        }   
    }

    public void SetItemType(int index)
    {
        switch (index)
        {
            case 0:
                itemNum = ITEM.none;
                break;
            case 1:
                itemNum = ITEM.fever;
                break;
            case 2:
                itemNum = ITEM.heal;
                break;
        }
    }

}

	

public class Step{
	List<SpawnInfo> spawnInfoList = new List<SpawnInfo>();
	public int spawnedNum=0;

	public bool isSpawnEnded(){
		return spawnedNum == spawnInfoList.Count;
	}

	
	public void AddInfo(SpawnInfo si){
		spawnInfoList.Add (si);
	}

	public SpawnInfo getSpawnInfo(){
		
		return spawnInfoList[spawnedNum++];
	}
	public int getSpawnCount(){
		return spawnInfoList.Count;			
	}
}

public class Phase{
	public int stepNum = 0;
	public string map_Name;
	public List<Step> stepList =new List<Step>();


	public void AddStep(Step s){
		stepList.Add (s);
	}

	public Step getStep(){
		return stepList [stepNum++];
	}

	public void PhaseFinish(){

	}

	public bool isEnded(){
		return stepNum == stepList.Count;
	}
}