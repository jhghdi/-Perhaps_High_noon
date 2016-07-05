using UnityEngine;
using System.Collections;
using System.Linq;

public class csEnemyManager : MonoBehaviour {
	public GameObject stageM;
	//배열로 미리 만들어 놓을 예정
	public GameObject Enemy1;
	public GameObject Aim;
	public GameObject R_Aim;
	public GameObject AimLock;

	//start 초기 배열 초기화
	public int Enemy1_n;
	public int Aim_n;

	GameObject[] Enemy1s;
	GameObject[] Aims;
	GameObject[] R_Aims;
	GameObject[] AimLocks;

	int enemyCount=-11;
	int enemy1_Index = 0;
	int aim_Index = 0;
	public GameObject[] spawnPoints;
	int step_Count = 1;
	// Use this for initialization
	void Start () {
		Enemy1s = new GameObject[Enemy1_n];
		Aims = new GameObject[Aim_n];
		R_Aims = new GameObject[Aim_n];
		AimLocks = new GameObject[Aim_n];
	}

	void OnLevelWasLoaded(int level){
		if (level > 2) {
			
			for (int i = 0; i < Enemy1_n; i++) {
				Enemy1s [i] = Instantiate (Enemy1, Vector3.zero, Quaternion.identity) as GameObject;
				Enemy1s [i].SetActive (false);
			}

			for (int i = 0; i < Aim_n; i++) {
				Aims [i] = Instantiate (Aim, Vector3.zero, Quaternion.identity)as GameObject;
				Aims [i].SetActive (false);

				R_Aims [i] = Instantiate (R_Aim, Vector3.zero, Quaternion.identity)as GameObject;
				R_Aims [i].SetActive (false);

				AimLocks [i] = Instantiate (AimLock, Vector3.zero, Quaternion.identity)as GameObject;
				AimLocks [i].SetActive (false);
			}
		}
	}


	//enemy가 호출 예정
	void OnEnemyDead(){
		enemyCount--;
		if (enemyCount == 0) {
			stageM.SendMessage ("OnStepEnded");



			enemyCount = -11;
			step_Count++;
			return;
		}
	}

	void Spawn(SpawnInfo si){
		GameObject e;
		//종류 별로
		//switch (si.typeNum) {
		//case 1:
		e = Enemy1s [enemy1_Index];
	
		enemy1_Index++;
		//break;
		//}

		Vector3 start = spawnPoints [si.spawnPos].transform.position;
		Vector3 end = spawnPoints [si.destinationPos].transform.position;
		e.SetActive (true);
		e.transform.position = start;
		e.transform.rotation = Quaternion.Euler (end - start);

		Aims [aim_Index].transform.parent = e.transform;
		R_Aims [aim_Index].transform.parent = e.transform;
		AimLocks [aim_Index].transform.parent = e.transform;
		aim_Index++;

		Hashtable hash = new Hashtable ();
		hash.Add ("path", new Vector3[2]{start,end});
		hash.Add ("orienttopath", true);
		hash.Add ("speed", 3.0f);
		hash.Add ("looptype", iTween.LoopType.none);
		hash.Add ("easetype", iTween.EaseType.linear);
		hash.Add ("ignoretimescale", false);
		hash.Add ("oncomplete", "OnMoveEnded");
		hash.Add ("oncompletetarget", e);

		iTween.MoveTo (e, hash);
	}

	//step별로 초기화 기능 있음.
	void setEnemyCount(int n){
		
		spawnPoints = GameObject.FindGameObjectsWithTag(string.Format("SpawnPoint{0}",step_Count)).OrderBy (g => g.transform.name).ToArray ();
		enemy1_Index = 0;
		aim_Index = 0;


		if (n == 0)
			stageM.SendMessage ("OnStepEnded");
		enemyCount = n;
	}

}
