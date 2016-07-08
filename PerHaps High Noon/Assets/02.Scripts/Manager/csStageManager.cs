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
	void Start () {

		csGameManager cs =  GameObject.Find ("GameManager").GetComponent<csGameManager> ();
        phases = cs.getPhases();
        SceneManager.LoadScene (phases[phaseNum]);

        myPhase = ParseXML();
        phaseNum++;

    }

	void OnLevelWasLoaded(int level){
		if (level > 2) {
			CamPathManager.SendMessage ("LoadInfo");
            CamPathManager.SendMessage("StartPhase");
        }
	}
	void StartSpawn(){
		Debug.Log ("Start Spawn");
		//이 수만큼 적이 죽으면 OnStepEnded 호출
		EnemyManager.SendMessage ("setEnemyCount", myStep.getSpawnCount ());

		while(!myStep.isSpawnEnded ()){//소환 시간이 정적이므로 한번에 step내의 정보를 읽어서 coroutine으로 시작한다.
		//	Debug.Log("spawned");
			StartCoroutine (spawnEnemy ());
		}
	}

	//EnemyManager가 호출
	//카메라 매니저로
	void OnStepEnded(){
		Debug.Log("OnStepEnded");
        if (myPhase.isEnded())
        {
            //다음 phase 로딩
            Debug.Log("phase end");
            if (phaseNum == phases.Length)
            {
                //결과 화면
                Debug.Log("result");
                return;
            }
            else {
                //다음  phase 로딩하기 전에 무브 + 페이드 아웃
                CamPathManager.SendMessage("Move");
                CamPathManager.SendMessage("EndPhase");
                
                
            }

        }
        else {
            //카메라 워크 시작.
            CamPathManager.SendMessage("Move");
        }
	}

	//카메라 매니저가 호출
	void OnCameraEnded(){//step이 끝났다는 뜻
		
		myStep = myPhase.getStep ();
		StartSpawn ();
	}

	Phase ParseXML(){
		Debug.Log("parseXML");
		Phase phase = new Phase ();

        

        // xml 파일 형식 정해지면 추가 구현

        // xml 지정
        string m_strName = string.Format("xml/{0}", phases[phaseNum]);


        XmlDocument document = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load(m_strName, typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        document.LoadXml(textAsset.text);
        
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
			if ( Int32.Parse(step.ChildNodes[0].InnerText) == 0)
				continue;
            bool isStartOfStep = Int32.Parse(step.ChildNodes[0].InnerText) == 1;

            if (isStartOfStep)
                index++;

            SpawnInfo spawn = new SpawnInfo();
            spawn.spawnPos = Int32.Parse(step.ChildNodes[1].InnerText);
            spawn.destinationPos = Int32.Parse(step.ChildNodes[2].InnerText);
            spawn.spawnCoolTime = float.Parse(step.ChildNodes[3].InnerText);
            spawn.SetMoveType(Int32.Parse(step.ChildNodes[4].InnerText));
            spawn.aimTime = float.Parse(step.ChildNodes[5].InnerText);
            spawn.item = (Common.ITEM_TYPE)(Int32.Parse(step.ChildNodes[6].InnerText));

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

    ///카메라패스매니저의 csFadeInOut에서 호출
    ///페이드 아웃이 끝나면 다음 페이즈 로드
    void OnFadeEnded()
    {
        SceneManager.LoadScene(phases[phaseNum]);
        myPhase = ParseXML();
        phaseNum++;
    }
}
//소환 정보 저장용 클래스
public class SpawnInfo{
	
	public enum TYPE {normal=0,slow,fast};
	public enum ITEM {none=0,fever,heal};

	public TYPE typeNum;
	public Common.ITEM_TYPE item;
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
		//Debug.Log (spawnedNum);
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