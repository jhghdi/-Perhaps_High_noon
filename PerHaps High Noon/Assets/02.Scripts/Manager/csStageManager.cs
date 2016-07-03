using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class csStageManager : MonoBehaviour {
	public GameObject EnemyManager;
	public GameObject CamPathManager;
	Phase myPhase = null;
	Step myStep =null;
	SpawnInfo mySpawnInfo = null;

	int phaseNum = 0;

	// Use this for initialization
	IEnumerator Start () {
		
		csStage cs =  GameObject.Find ("GameManager").GetComponent<csStage> ();
		string[] phases =  cs.getPhases();

		SceneManager.LoadScene (phases[phaseNum]);


		myPhase = ParseXML ();
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
		Debug.Log("paseXML");
		Phase p = new Phase ();

		Step s = new Step ();
		Step s2 = new Step ();
		Step s3 = new Step ();
		Step s4 = new Step ();

		p.AddStep (s);
		p.AddStep (s2);
		p.AddStep (s3);
		p.AddStep (s4);
		//phae 정보 읽기

		//Phase load
		///ddddddddddddddddddd

		//step load

		//step add

		//phase add




		return p;
	}

	IEnumerator spawnEnemy(){
		mySpawnInfo = myStep.getSpawnInfo ();
		yield return new WaitForSeconds( mySpawnInfo.spawnCoolTime);
		EnemyManager.SendMessage ("Spawn",mySpawnInfo);
	}
}
//소환 정보 저장용 클래스
public class SpawnInfo{
	
	public enum TYPE{normal=0,slow,fast};
	public enum ITEM{nul=0,fever,heal};

	public TYPE typeNum;
	public ITEM itemNum;
	public float spawnCoolTime;
	public Vector3 spawnPos;

	public float moveTime;
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